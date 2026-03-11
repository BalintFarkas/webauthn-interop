using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Okta
{
    /// <summary>
    /// Wrapper object used by Okta to nest WebAuthn activation options.
    /// </summary>
    [method: JsonConstructor]
    public class Embedded(PublicKeyCredentialCreationOptions options)
    {
        /// <summary>
        /// Defines public key options for the creation of a new WebAuthn public key credential.
        /// </summary>
        [JsonPropertyName("activation")]
        public PublicKeyCredentialCreationOptions PublicKeyOptions { get; set; } = options;
    }

    /// <summary>
    /// Defines the options for creating a new WebAuthn credential in Okta's API.
    /// </summary>
    public class OktaWebauthnCredentialCreationOptions : WebauthnCredentialCreationOptions
    {
        /// <summary>
        /// The factor id of the Okta factor being registered.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// A wrapper around the PublicKeyCredentialCreationOptions
        /// </summary>
        [JsonPropertyName("_embedded")]
        public Embedded Embedded { get; set; }

        /// <summary>
        /// Gets or sets the nested WebAuthn public key credential creation options.
        /// </summary>
        [JsonIgnore]
        public override PublicKeyCredentialCreationOptions PublicKeyOptions
        {
            get => Embedded.PublicKeyOptions;
            set
            {
                if (this.Embedded == null)
                {
                    this.Embedded = new Embedded(value);
                }
                else
                {
                    this.Embedded.PublicKeyOptions = value;
                }
            }
        }

        /// <summary>
        /// Parses a JSON payload returned by Okta into WebAuthn credential creation options.
        /// </summary>
        /// <param name="json">The Okta JSON payload.</param>
        /// <returns>The parsed credential creation options.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="json"/> is null or empty.</exception>
        public static OktaWebauthnCredentialCreationOptions Create(string json)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(json);

            return JsonSerializer.Deserialize(json, WebAuthnJsonContext.Default.OktaWebauthnCredentialCreationOptions)
                ?? throw new JsonException("Unable to deserialize Okta WebAuthn credential creation options.");
        }
    }
}
