using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop;

/// <summary>
/// Information about an authenticator.
/// </summary>
/// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_DETAILS.</remarks>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal class AuthenticatorDetailsOut
{
    /// <summary>
    /// Version of this structure, to allow for modifications in the future.
    /// </summary>
    public AuthenticatorDetailsVersion Version { get; private set; }

    /// <summary>
    /// Size of the authenticator ID.
    /// </summary>
    private int _authenticatorIdLength;

    /// <summary>
    /// Authenticator ID bytes.
    /// </summary>
    private ByteArrayOut? _authenticatorId;

    /// <summary>
    /// Authenticator Name.
    /// </summary>
    public string? AuthenticatorName { get; private set; }

    /// <summary>
    /// Size of the authenticator logo.
    /// </summary>
    private int _authenticatorLogoLength;

    /// <summary>
    /// Authenticator logo (expected to be in SVG format).
    /// </summary>
    private ByteArrayOut? _authenticatorLogo;

    /// <summary>
    /// Is the authenticator currently locked?
    /// When locked, this authenticator's credentials might not be present or updated in WebAuthNGetPlatformCredentialList.
    /// </summary>
    public bool Locked { get; private set; }

    /// <summary>
    /// Authenticator ID bytes.
    /// </summary>
    public byte[]? AuthenticatorId => _authenticatorId?.Read(_authenticatorIdLength);

    /// <summary>
    /// Authenticator logo (expected to be in SVG format).
    /// </summary>
    public byte[]? AuthenticatorLogo => _authenticatorLogo?.Read(_authenticatorLogoLength);
}
