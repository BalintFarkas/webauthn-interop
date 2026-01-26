using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Windows.Win32;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Defines hints as to how clients might communicate with a particular authenticator in order to obtain an assertion for a specific credential.
    /// </summary>
    /// <see href="https://www.w3.org/TR/webauthn-3/#enum-transport"/>
    [Flags]
    [JsonConverter(typeof(JsonCustomEnumConverter<AuthenticatorTransport>))]
    public enum AuthenticatorTransport : uint
    {
        /// <summary>
        /// No transport restrictions.
        /// </summary>
        [EnumMember(Value = null)]
        NoRestrictions = 0,

        /// <summary>
        /// Universal Serial Bus (USB).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_USB.</remarks>
        [EnumMember(Value = "usb")]
        USB = PInvoke.WEBAUTHN_CTAP_TRANSPORT_USB,

        /// <summary>
        /// Near Field Communication (NFC).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_NFC.</remarks>
        [EnumMember(Value = "nfc")]
        NFC = PInvoke.WEBAUTHN_CTAP_TRANSPORT_NFC,

        /// <summary>
        /// Bluetooth Low Energy (BLE).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_BLE.</remarks>
        [EnumMember(Value = "ble")]
        BLE = PInvoke.WEBAUTHN_CTAP_TRANSPORT_BLE,

        /// <summary>
        /// Reserved for testing.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_TEST.</remarks>
        Test = PInvoke.WEBAUTHN_CTAP_TRANSPORT_TEST,

        /// <summary>
        /// Client device-specific transport (platform authenticator).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_INTERNAL.</remarks>
        [EnumMember(Value = "internal")]
        Internal = PInvoke.WEBAUTHN_CTAP_TRANSPORT_INTERNAL,

        /// <summary>
        /// Hybrid Transport (QR Code / caBLE).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_HYBRID.</remarks>
        [EnumMember(Value = "hybrid")]
        Hybrid = PInvoke.WEBAUTHN_CTAP_TRANSPORT_HYBRID,

        /// <summary>
        /// Smart card transport.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_SMART_CARD.</remarks>
        [EnumMember(Value = "smart-card")]
        SmartCard = PInvoke.WEBAUTHN_CTAP_TRANSPORT_SMART_CARD
    }
}
