using System.Runtime.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Public key credential hints as defined in the WebAuthn specification.
/// </summary>
/// <remarks>
/// See https://w3c.github.io/webauthn/#enum-hints for more information.
/// </remarks>
public enum PublicKeyCredentialHint
{
    /// <summary>
    /// No hint specified.
    /// </summary>
    None = 0,

    /// <summary>
    /// Indicates that the Relying Party believes that users will satisfy this request with a physical security key.
    /// </summary>
    [EnumMember(Value = "security-key")]
    SecurityKey,

    /// <summary>
    /// Indicates that the Relying Party believes that users will satisfy this request with a platform authenticator built into the client device.
    /// </summary>
    [EnumMember(Value = "client-device")]
    ClientDevice,

    /// <summary>
    /// Indicates that the Relying Party believes that users will satisfy this request with a general-purpose authenticator such as smartphone (hybrid transport).
    /// </summary>
    [EnumMember(Value = "hybrid")]
    Hybrid
}
