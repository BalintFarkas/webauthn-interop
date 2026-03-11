using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Polymorphic JSON converter for <see cref="AuthenticatorResponse"/>.
    /// Determines the concrete subtype by inspecting property names, because the WebAuthn JSON
    /// format does not include a type discriminator.
    /// </summary>
    internal sealed class AuthenticatorResponseConverter : JsonConverter<AuthenticatorResponse>
    {
        public override AuthenticatorResponse? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Copy the reader to peek ahead without consuming the original position.
            // Utf8JsonReader is a ref struct that explicitly supports this pattern.
            var peekReader = reader;

            while (peekReader.Read())
            {
                if (peekReader.TokenType == JsonTokenType.EndObject)
                    break;

                if (peekReader.TokenType == JsonTokenType.PropertyName)
                {
                    if (peekReader.ValueTextEquals("attestationObject"))
                        return JsonSerializer.Deserialize(ref reader, WebAuthnJsonContext.Default.AuthenticatorAttestationResponse);

                    if (peekReader.ValueTextEquals("authenticatorData"))
                        return JsonSerializer.Deserialize(ref reader, WebAuthnJsonContext.Default.AuthenticatorAssertionResponse);

                    peekReader.Skip(); // skip over the value of unrecognized properties
                }
            }

            throw new JsonException(
                "Cannot determine the AuthenticatorResponse subtype: " +
                "neither 'attestationObject' nor 'authenticatorData' property found.");
        }

        public override void Write(Utf8JsonWriter writer, AuthenticatorResponse value, JsonSerializerOptions options)
        {
            if (value is AuthenticatorAttestationResponse attestation)
                JsonSerializer.Serialize(writer, attestation, WebAuthnJsonContext.Default.AuthenticatorAttestationResponse);
            else if (value is AuthenticatorAssertionResponse assertion)
                JsonSerializer.Serialize(writer, assertion, WebAuthnJsonContext.Default.AuthenticatorAssertionResponse);
            else
                throw new JsonException($"Unsupported AuthenticatorResponse subtype: {value.GetType().Name}");
        }
    }
}
