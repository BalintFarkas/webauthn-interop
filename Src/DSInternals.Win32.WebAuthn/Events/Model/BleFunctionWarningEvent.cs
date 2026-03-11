namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a BLE function warning event (Event ID 2270).
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="Function"&gt;BleGetAssertion&lt;/Data&gt;
///   &lt;Data Name="Location"&gt;ConnectDevice&lt;/Data&gt;
///   &lt;Data Name="Error"&gt;0x80070005&lt;/Data&gt;
///   &lt;Data Name="Win32Error"&gt;0x80070005&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class BleFunctionWarningEvent : WebAuthnEvent
{
    /// <summary>
    /// The name of the function that generated the warning.
    /// </summary>
    public string? Function { get; set; }

    /// <summary>
    /// The location within the function where the warning occurred.
    /// </summary>
    public string? Location { get; set; }
}
