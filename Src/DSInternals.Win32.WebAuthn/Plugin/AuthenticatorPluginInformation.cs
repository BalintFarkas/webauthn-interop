using System;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Represents information about an authenticator plugin registered in Windows.
    /// </summary>
    /// <remarks>
    /// Authenticator plugins are registered under HKLM\SOFTWARE\Microsoft\FIDO\{UserSID}\Plugins\{PluginGuid}.
    /// This class corresponds to data stored by third-party passkey providers like 1Password, Bitwarden, etc.
    /// </remarks>
    public sealed class AuthenticatorPluginInformation
    {
        /// <summary>
        /// The user SID for which this plugin is registered.
        /// </summary>
        public string? UserSid { get; set; }

        /// <summary>
        /// The user name resolved from the SID.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// The unique identifier (CLSID) of the plugin.
        /// </summary>
        public Guid PluginClsid { get; set; }

        /// <summary>
        /// The display name of the authenticator plugin.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The Relying Party ID associated with the plugin.
        /// </summary>
        public string? RpId { get; set; }

        /// <summary>
        /// The full name of the Windows Store package.
        /// </summary>
        public string? PackageFullName { get; set; }

        /// <summary>
        /// The family name of the Windows Store package.
        /// </summary>
        public string? PackageFamilyName { get; set; }

        /// <summary>
        /// The display name of the publisher.
        /// </summary>
        public string? PublisherDisplayName { get; set; }

        /// <summary>
        /// The signing key algorithm used by the package (e.g., "ECDSA_P384").
        /// </summary>
        public string? SigningKeyAlgorithm { get; set; }

        /// <summary>
        /// The kind of package signature.
        /// </summary>
        public PackageSignatureKind PackageSignatureKind { get; set; }

        /// <summary>
        /// Indicates whether the authenticator plugin is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Indicates whether the state has been toggled.
        /// </summary>
        public bool StateToggled { get; set; }

        /// <summary>
        /// The authenticator attestation GUID (AAGUID).
        /// </summary>
        public Guid AaGuid { get; set; }

        /// <summary>
        /// The number of user verifications performed.
        /// </summary>
        public uint UvCount { get; set; }

        /// <summary>
        /// The CTAP CBOR encoded authenticator information.
        /// </summary>
        /// <remarks>
        /// This is the authenticatorGetInfo response encoded in CBOR format.
        /// </remarks>
        public byte[]? AuthenticatorInfo { get; set; }

        /// <summary>
        /// The plugin authenticator logo for light themes (SVG format).
        /// </summary>
        public string? LightLogo { get; set; }

        /// <summary>
        /// The plugin authenticator logo for dark themes (SVG format).
        /// </summary>
        public string? DarkLogo { get; set; }
    }
}
