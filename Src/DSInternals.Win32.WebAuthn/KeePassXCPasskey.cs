using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
#if NET5_0_OR_GREATER
using System.Security.Cryptography;
#endif

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Represents a passkey exported from KeePassXC (.passkey JSON format).
/// </summary>
public sealed class KeePassXCPasskey
{
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("relyingParty")]
    public string? RelyingParty { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("userHandle")]
    public string? UserHandle { get; set; }

    [JsonPropertyName("credentialId")]
    public string? CredentialId { get; set; }

    [JsonPropertyName("privateKey")]
    public string? PrivateKey { get; set; }

    /// <summary>
    /// Loads and deserializes a KeePassXC passkey from a JSON file.
    /// </summary>
    public static KeePassXCPasskey LoadFromFile(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize(json, WebAuthnJsonContext.Default.KeePassXCPasskey)
            ?? throw new JsonException("Failed to deserialize passkey file.");
    }

#if NET5_0_OR_GREATER
    /// <summary>
    /// Loads the private key from this passkey as an AsymmetricAlgorithm.
    /// The caller is responsible for disposing the returned key.
    /// </summary>
    public AsymmetricAlgorithm LoadPrivateKey()
    {
        if (PrivateKey == null)
            throw new InvalidOperationException("No private key found in passkey file.");

        return SoftwareAuthenticator.ImportPrivateKeyFromPem(PrivateKey);
    }
#endif
}
