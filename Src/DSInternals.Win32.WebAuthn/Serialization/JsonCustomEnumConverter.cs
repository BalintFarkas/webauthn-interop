using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// A custom JSON converter for enum types that supports <see cref="EnumMemberAttribute"/> for custom serialization names.
    /// </summary>
    /// <typeparam name="TEnum">The enum type to convert.</typeparam>
    /// <remarks>
    /// <para>
    /// Enum members with <c>[EnumMember(Value = null)]</c> are treated as unmapped/default values:
    /// </para>
    /// <list type="bullet">
    /// <item>When serializing, these values cause the property to be skipped (not emitted).</item>
    /// <item>When deserializing, a missing property or null value maps to the unmapped enum value.</item>
    /// </list>
    /// </remarks>
    public sealed class JsonCustomEnumConverter<TEnum> : JsonConverter<TEnum>
        where TEnum : struct, Enum
    {
        private static readonly Dictionary<TEnum, string?> valuesToNames = CreateValueToNameDictionary();
        private static readonly Dictionary<string, TEnum> namesToValues = CreateNameToValueDictionary();
        private static readonly TEnum? unmappedValue = FindUnmappedValue();
        private static readonly bool isFlagsEnum = typeof(TEnum).IsDefined(typeof(FlagsAttribute), false);

        /// <summary>
        /// Reads a JSON token and converts it to a value of <typeparamref name="TEnum"/>.
        /// </summary>
        /// <param name="reader">JSON reader positioned on the enum value.</param>
        /// <param name="typeToConvert">Target CLR type.</param>
        /// <param name="options">Serializer options.</param>
        /// <returns>Deserialized enum value.</returns>
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    // Support for enum value names
                    string? text = reader.GetString();
                    if (text is not null && namesToValues.TryGetValue(text, out TEnum value))
                        return value;
                    else
                        throw new JsonException($"Invalid enum value = \"{text}\"");

                case JsonTokenType.Null:
                    // Null JSON value maps to the unmapped enum value
                    if (unmappedValue.HasValue)
                        return unmappedValue.Value;
                    else
                        throw new JsonException("Invalid enum value = null");

                case JsonTokenType.StartArray:
                    // Support for flags
                    if (!isFlagsEnum)
                        throw new JsonException($"Cannot deserialize array to non-flags enum {typeof(TEnum).Name}");

                    TEnum result = default;

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        // Combine the flags. All interop flags are defined as uint.
                        TEnum arrayItem = Read(ref reader, typeToConvert, options);
                        result = (TEnum)(object)(((uint)(object)result) | ((uint)(object)arrayItem));
                    }

                    return result;

                default:
                    throw new JsonException($"Invalid enum value ({reader.TokenType})");
            }
        }

        /// <summary>
        /// Writes an enum value to JSON using <see cref="EnumMemberAttribute"/> mappings.
        /// </summary>
        /// <param name="writer">JSON writer.</param>
        /// <param name="value">Enum value to write.</param>
        /// <param name="options">Serializer options.</param>
        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            if (writer is null)
            {
                return;
            }

            if (isFlagsEnum)
            {
                // For flags enums, serialize as an array of strings
                uint numericValue = (uint)(object)value;

                if (numericValue == 0)
                {
                    // Zero value (no flags set) - check if there's an unmapped/null value
                    if (unmappedValue.HasValue && EqualityComparer<TEnum>.Default.Equals(value, unmappedValue.Value))
                    {
                        // Write null so the property can be skipped by JsonIgnoreCondition.WhenWritingNull
                        writer.WriteNullValue();
                        return;
                    }
                }

                writer.WriteStartArray();

                foreach (var kvp in valuesToNames)
                {
                    uint flagValue = (uint)(object)kvp.Key;

                    // Skip zero/unmapped values and check if flag is set
                    if (flagValue != 0 && kvp.Value is not null && (numericValue & flagValue) == flagValue)
                    {
                        writer.WriteStringValue(kvp.Value);
                    }
                }

                writer.WriteEndArray();
            }
            else if (valuesToNames.TryGetValue(value, out var name))
            {
                if (name is null)
                {
                    // Unmapped value - write null so the property can be skipped by JsonIgnoreCondition.WhenWritingNull
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStringValue(name);
                }
            }
            else
            {
                // Fallback to enum name
                writer.WriteStringValue(value.ToString());
            }
        }

        /// <summary>
        /// Gets the default/unmapped value for this enum type, if one exists.
        /// </summary>
        /// <returns>The unmapped enum value, or null if no unmapped value is defined.</returns>
        public static TEnum? UnmappedValue => unmappedValue;

        private static Dictionary<TEnum, string?> CreateValueToNameDictionary()
        {
            var dictionary = new Dictionary<TEnum, string?>();

            foreach (var field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var enumMember = field.GetCustomAttribute<EnumMemberAttribute>(false);
                var value = (TEnum)field.GetValue(null)!;

                if (enumMember is not null)
                {
                    // Use the EnumMember value (can be null for unmapped values)
                    dictionary[value] = enumMember.Value;
                }
                else
                {
                    // No EnumMember attribute - use the field name
                    dictionary[value] = field.Name;
                }
            }

            return dictionary;
        }

        private static Dictionary<string, TEnum> CreateNameToValueDictionary()
        {
            var dictionary = new Dictionary<string, TEnum>(StringComparer.OrdinalIgnoreCase);

            foreach (var kvp in valuesToNames)
            {
                if (kvp.Value is not null)
                {
                    dictionary[kvp.Value] = kvp.Key;
                }
            }

            return dictionary;
        }

        private static TEnum? FindUnmappedValue()
        {
            foreach (var kvp in valuesToNames)
            {
                if (kvp.Value is null)
                {
                    return kvp.Key;
                }
            }

            return null;
        }
    }
}
