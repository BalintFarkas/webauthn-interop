#pragma warning disable CA1069 // Enums values should not be duplicated

namespace DSInternals.Win32.WebAuthn.Interop;

/// <summary>
/// Version of the WEBAUTHN_AUTHENTICATOR_DETAILS structure, to allow for modifications in the future.
/// </summary>
internal enum AuthenticatorDetailsVersion : uint
{
    /// <remarks>
    /// Corresponds to WEBAUTHN_AUTHENTICATOR_DETAILS_VERSION_1.
    /// </remarks>
    Version1 = 1, // TODO: PInvoke.WEBAUTHN_AUTHENTICATOR_DETAILS_VERSION_1,

    /// <remarks>
    /// Corresponds to WEBAUTHN_AUTHENTICATOR_DETAILS_CURRENT_VERSION.
    /// </remarks>
    Current = Version1 //TODO: PInvoke.WEBAUTHN_AUTHENTICATOR_DETAILS_CURRENT_VERSION
}
