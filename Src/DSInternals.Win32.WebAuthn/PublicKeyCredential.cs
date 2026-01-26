using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    public class PublicKeyCredential
    {
        [JsonPropertyName("authenticatorAttachment")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AuthenticatorAttachment? AuthenticatorAttachment { get; set; }

        [JsonPropertyName("id")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[] Id { get; set; }

        [JsonPropertyName("rawId")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[] RawId { get; set; }

        [JsonPropertyName("response")]
        public AuthenticatorResponse Response { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = "public-key";

        [JsonPropertyName("clientExtensionResults")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AuthenticationExtensionsClientOutputs? ClientExtensionResults { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
