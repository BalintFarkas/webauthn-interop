using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// This structure comes from an older version of the API and should only be used by Marshal.Sizeof().
    /// </summary>
    /// <see cref="WebAuthn.CredentialDetails"/>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_DETAILS version 3.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class CredentialDetailsV3
    {
        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.</remarks>
        private CredentialDetailsVersion _version;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.</remarks>
        private int _credentialIdLength;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.</remarks>
        private IntPtr _credentialId;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.</remarks>
        private IntPtr _relyingPartyInformation;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.</remarks>
        private IntPtr _userInformation;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.</remarks>
        private bool _removable;

        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_DETAILS_VERSION_2.</remarks>
        private bool _backedUp;

        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_DETAILS_VERSION_3.</remarks>
        private IntPtr _authenticatorName;

        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_DETAILS_VERSION_3.</remarks>
        private int _authenticatorLogoLength;

        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_DETAILS_VERSION_3.</remarks>
        private IntPtr _authenticatorLogo;

        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_DETAILS_VERSION_3.</remarks>
        private bool _thirdPartyPayment;

        /// <summary>
        /// The instantiation of this class is blocked by this private constructor.
        /// </summary>
        private CredentialDetailsV3() { }
    }
}
