namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a CTAP platform command error event (Event ID 2103).
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="Command"&gt;WebAuthN&lt;/Data&gt;
///   &lt;Data Name="TransactionId"&gt;{aabbccdd-1234-5678-9abc-def012345678}&lt;/Data&gt;
///   &lt;Data Name="Error"&gt;0x80090331&lt;/Data&gt;
///   &lt;Data Name="Win32Error"&gt;0x80090331&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// <para>Known command names: MakeCredential, GetAssertion, GetAllPlatformCredentials,
/// GetAuthenticatorList, GetPluginAuthenticatorList, CreateTicket, UpdatePluginAuthenticator,
/// RemoveAllPluginAuthenticatorCredentials, WebAuthN, command.</para>
/// </remarks>
public sealed class CtapCommandErrorEvent : WebAuthnEvent
{
    /// <summary>
    /// The CTAP command name (e.g., "MakeCredential", "GetAssertion", "GetAllPlatformCredentials").
    /// </summary>
    public string? Command { get; set; }
}
