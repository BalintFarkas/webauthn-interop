using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Base type for provider-specific WebAuthn credential creation option payloads.
    /// </summary>
    public abstract class WebauthnCredentialCreationOptions
    {
        /// <summary>
        /// Gets or sets normalized WebAuthn public key credential creation options.
        /// </summary>
        [JsonPropertyName("publicKey")]
        public abstract PublicKeyCredentialCreationOptions PublicKeyOptions { get; set; }
    }
}
