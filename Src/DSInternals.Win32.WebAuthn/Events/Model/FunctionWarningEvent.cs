namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a CTAP function warning event (Event ID 2105).
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="Function"&gt;HidSilentGetAssertion&lt;/Data&gt;
///   &lt;Data Name="Location"&gt;SendRequest&lt;/Data&gt;
///   &lt;Data Name="Error"&gt;0x8009030d&lt;/Data&gt;
///   &lt;Data Name="Win32Error"&gt;0x8009030d&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class FunctionWarningEvent : WebAuthnEvent
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
