namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a credential creation operation completed event (Event ID 1001).
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="TransactionId"&gt;{005e70a8-ebc3-4a48-a208-f470f2988c3e}&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class MakeCredentialCompletedEvent : WebAuthnEvent
{
}
