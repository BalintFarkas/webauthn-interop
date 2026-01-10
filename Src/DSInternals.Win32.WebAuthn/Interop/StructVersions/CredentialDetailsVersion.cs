#pragma warning disable CA1069 // Enums values should not be duplicated

using Windows.Win32;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Credential Details Structure Version Information.
    /// </summary>
    internal enum CredentialDetailsVersion : uint
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.
        /// </remarks>
        Version1 = PInvoke.WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_DETAILS_VERSION_2.
        /// </remarks>
        Version2 = PInvoke.WEBAUTHN_CREDENTIAL_DETAILS_VERSION_2,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_DETAILS_VERSION_3.
        /// </remarks>
        Version3 = 3,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_DETAILS_VERSION_4.
        /// </remarks>
        Version4 = 4,

        /// <summary>
        /// Current version
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_DETAILS_CURRENT_VERSION.
        /// </remarks>
        Current = PInvoke.WEBAUTHN_CREDENTIAL_DETAILS_CURRENT_VERSION
    }
}
