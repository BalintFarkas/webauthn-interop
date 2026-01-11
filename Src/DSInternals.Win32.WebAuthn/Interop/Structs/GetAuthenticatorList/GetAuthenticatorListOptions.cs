using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop;

/// <summary>
/// Options for the WebAuthNGetAuthenticatorList operation.
/// </summary>
/// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_DETAILS_OPTIONS.</remarks>
[StructLayout(LayoutKind.Sequential)]
internal class GetAuthenticatorListOptions
{
    /// <summary>
    /// Version of this structure, to allow for modifications in the future.
    /// </summary>
    public AuthenticatorDetailsOptionsVersion Version
    {
        get;
        private set;
    } = AuthenticatorDetailsOptionsVersion.Version1;
}
