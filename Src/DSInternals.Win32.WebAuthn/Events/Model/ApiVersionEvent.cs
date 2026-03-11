namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents an API version query event (Event ID 1071).
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="value"&gt;9&lt;/Data&gt;
///   &lt;Data Name="Error"&gt;0x0&lt;/Data&gt;
///   &lt;Data Name="HResult"&gt;0&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class ApiVersionEvent : WebAuthnEvent
{
    /// <summary>
    /// The WebAuthn API version number.
    /// </summary>
    public uint? Value { get; set; }
}
