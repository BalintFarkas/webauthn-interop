namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a platform authenticator availability query event (Event ID 1070).
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="value"&gt;true&lt;/Data&gt;
///   &lt;Data Name="Error"&gt;0x0&lt;/Data&gt;
///   &lt;Data Name="HResult"&gt;0&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class IsUserVerifyingPlatformAuthenticatorAvailableEvent : WebAuthnEvent
{
    /// <summary>
    /// Whether a user-verifying platform authenticator is available.
    /// </summary>
    public bool? IsAvailable { get; set; }
}
