using System;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents the result of a WebAuthNCancelCurrentOperation call (Event ID 1072).
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="value"&gt;{00000000-0000-0000-0000-000000000000}&lt;/Data&gt;
///   &lt;Data Name="Error"&gt;0x0&lt;/Data&gt;
///   &lt;Data Name="HResult"&gt;0&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class CancelCurrentOperationEvent : WebAuthnEvent
{
    /// <summary>
    /// The cancellation ID passed to WebAuthNCancelCurrentOperation.
    /// </summary>
    public Guid? CancellationId { get; set; }
}
