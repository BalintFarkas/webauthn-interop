namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a USB device started event (Event ID 2210).
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="TransactionId"&gt;{aabbccdd-1234-5678-9abc-def012345678}&lt;/Data&gt;
///   &lt;Data Name="DevicePath"&gt;\\?\hid#vid_1050&amp;pid_0407#...&lt;/Data&gt;
///   &lt;Data Name="Manufacturer"&gt;Yubico&lt;/Data&gt;
///   &lt;Data Name="Product"&gt;YubiKey OTP+FIDO+CCID&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class UsbDeviceStartedEvent : WebAuthnEvent
{
    /// <summary>
    /// The HID device path of the USB authenticator.
    /// </summary>
    public string? DevicePath { get; set; }

    /// <summary>
    /// The manufacturer of the USB authenticator device.
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// The product name of the USB authenticator device.
    /// </summary>
    public string? Product { get; set; }
}
