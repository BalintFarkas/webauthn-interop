namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a CBOR-decoded MakeCredential response event (Event ID 1102).
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="TransactionId"&gt;{aabbccdd-1234-5678-9abc-def012345678}&lt;/Data&gt;
///   &lt;Data Name="AttestationFormatType"&gt;none&lt;/Data&gt;
///   &lt;Data Name="RpIdHashLength"&gt;32&lt;/Data&gt;
///   &lt;Data Name="RpIdHash"&gt;3AEB002460381C6F...&lt;/Data&gt;
///   &lt;Data Name="Flags"&gt;0x45&lt;/Data&gt;
///   &lt;Data Name="SignCount"&gt;0x0&lt;/Data&gt;
///   &lt;Data Name="AAGuid"&gt;{de1e552d-db1d-4423-a439-09523d6f04a0}&lt;/Data&gt;
///   &lt;Data Name="CredentialIdLength"&gt;32&lt;/Data&gt;
///   &lt;Data Name="CredentialId"&gt;90613158E4C99370...&lt;/Data&gt;
///   &lt;Data Name="U2fPublicKey"&gt;&lt;/Data&gt;
///   &lt;Data Name="PublicKeyLength"&gt;77&lt;/Data&gt;
///   &lt;Data Name="PublicKey"&gt;A5010203262001...&lt;/Data&gt;
///   &lt;Data Name="ResponseLength"&gt;240&lt;/Data&gt;
///   &lt;Data Name="Response"&gt;A401A26269645820...&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class CborMakeCredentialResponseEvent : MakeCredentialResponseEvent
{
}
