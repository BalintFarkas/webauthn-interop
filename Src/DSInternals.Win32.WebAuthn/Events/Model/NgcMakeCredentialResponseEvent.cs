namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents an NGC MakeCredential response event (Event ID 1041).
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="TransactionId"&gt;{aabbccdd-1234-5678-9abc-def012345678}&lt;/Data&gt;
///   &lt;Data Name="AttestationFormatType"&gt;packed&lt;/Data&gt;
///   &lt;Data Name="RpIdHashLength"&gt;32&lt;/Data&gt;
///   &lt;Data Name="RpIdHash"&gt;49960DE5880E8C68...&lt;/Data&gt;
///   &lt;Data Name="Flags"&gt;0x45&lt;/Data&gt;
///   &lt;Data Name="SignCount"&gt;0x0&lt;/Data&gt;
///   &lt;Data Name="AAGuid"&gt;{de1e552d-db1d-4423-a439-09523d6f04a0}&lt;/Data&gt;
///   &lt;Data Name="CredentialIdLength"&gt;32&lt;/Data&gt;
///   &lt;Data Name="CredentialId"&gt;F2B3A1D4E5C67890...&lt;/Data&gt;
///   &lt;Data Name="U2fPublicKey"&gt;&lt;/Data&gt;
///   &lt;Data Name="PublicKeyLength"&gt;77&lt;/Data&gt;
///   &lt;Data Name="PublicKey"&gt;A5010203262001...&lt;/Data&gt;
///   &lt;Data Name="ResponseLength"&gt;350&lt;/Data&gt;
///   &lt;Data Name="Response"&gt;A301667061636B65...&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class NgcMakeCredentialResponseEvent : MakeCredentialResponseEvent
{
}
