using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.EntraID;
using DSInternals.Win32.WebAuthn.FIDO;
using DSInternals.Win32.WebAuthn.Interop;
using DSInternals.Win32.WebAuthn.Okta;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Source-generated JSON serialization metadata for WebAuthn models.
    /// </summary>
    [JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonSerializable(typeof(CollectedClientData))]
    [JsonSerializable(typeof(PublicKeyCredential))]
    [JsonSerializable(typeof(AuthenticatorAttestationResponse))]
    [JsonSerializable(typeof(AuthenticatorAssertionResponse))]
    [JsonSerializable(typeof(AuthenticationExtensionsClientInputs))]
    [JsonSerializable(typeof(AuthenticationExtensionsClientOutputs))]
    [JsonSerializable(typeof(PublicKeyCredentialCreationOptions))]
    [JsonSerializable(typeof(MicrosoftGraphWebauthnCredentialCreationOptions))]
    [JsonSerializable(typeof(MicrosoftGraphWebauthnAttestationResponse))]
    [JsonSerializable(typeof(OktaWebauthnCredentialCreationOptions))]
    [JsonSerializable(typeof(OktaFido2AuthenticationMethod))]
    [JsonSerializable(typeof(OktaWebauthnAttestationResponse))]
    public partial class WebAuthnJsonContext : JsonSerializerContext
    {
    }
}
