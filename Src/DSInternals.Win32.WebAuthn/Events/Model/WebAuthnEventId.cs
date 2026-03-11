namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Known event IDs from the Microsoft-Windows-WebAuthN/Operational event log.
/// </summary>
public enum WebAuthnEventId
{
    /// <summary>
    /// Credential creation operation has started.
    /// </summary>
    MakeCredentialStarted = 1000,

    /// <summary>
    /// Credential creation operation has completed.
    /// </summary>
    MakeCredentialCompleted = 1001,

    /// <summary>
    /// Credential creation operation failed.
    /// </summary>
    MakeCredentialError = 1002,

    /// <summary>
    /// Assertion operation has started.
    /// </summary>
    GetAssertionStarted = 1003,

    /// <summary>
    /// Assertion operation has completed.
    /// </summary>
    GetAssertionCompleted = 1004,

    /// <summary>
    /// Assertion operation failed.
    /// </summary>
    GetAssertionError = 1005,

    /// <summary>
    /// Low-level command execution has started.
    /// </summary>
    SendCommandStarted = 1006,

    /// <summary>
    /// Low-level command execution has completed.
    /// </summary>
    SendCommandCompleted = 1007,

    /// <summary>
    /// Low-level command execution failed.
    /// </summary>
    SendCommandError = 1008,

    /// <summary>
    /// NGC-backed credential creation has started.
    /// </summary>
    NgcMakeCredentialStarted = 1020,

    /// <summary>
    /// NGC-backed credential creation has completed.
    /// </summary>
    NgcMakeCredentialCompleted = 1021,

    /// <summary>
    /// NGC-backed credential creation failed.
    /// </summary>
    NgcMakeCredentialError = 1022,

    /// <summary>
    /// NGC-backed assertion operation has started.
    /// </summary>
    NgcGetAssertionStarted = 1023,

    /// <summary>
    /// NGC-backed assertion operation has completed.
    /// </summary>
    NgcGetAssertionCompleted = 1024,

    /// <summary>
    /// NGC-backed assertion operation failed.
    /// </summary>
    NgcGetAssertionError = 1025,

    /// <summary>
    /// NGC credential creation response payload.
    /// </summary>
    NgcMakeCredentialResponse = 1041,

    /// <summary>
    /// Remote WebAuthn RPC request payload.
    /// </summary>
    RemoteRpcRequest = 1050,

    /// <summary>
    /// Remote WebAuthn RPC response payload.
    /// </summary>
    RemoteRpcResponse = 1052,

    /// <summary>
    /// Platform authenticator availability check result.
    /// </summary>
    IsUserVerifyingPlatformAuthenticatorAvailable = 1070,

    /// <summary>
    /// WebAuthn API version information event.
    /// </summary>
    ApiVersion = 1071,

    /// <summary>
    /// Result of a WebAuthNCancelCurrentOperation call, including the cancellation ID.
    /// </summary>
    CancelCurrentOperation = 1072,

    /// <summary>
    /// CBOR-encoded make-credential request.
    /// </summary>
    CborMakeCredentialRequest = 1101,

    /// <summary>
    /// CBOR-encoded make-credential response.
    /// </summary>
    CborMakeCredentialResponse = 1102,

    /// <summary>
    /// CBOR-encoded get-assertion request.
    /// </summary>
    CborGetAssertionRequest = 1103,

    /// <summary>
    /// CBOR-encoded get-assertion response.
    /// </summary>
    CborGetAssertionResponse = 1104,

    /// <summary>
    /// WebAuthn service has started.
    /// </summary>
    ServiceStarted = 2000,

    /// <summary>
    /// WebAuthn service has stopped.
    /// </summary>
    ServiceStopped = 2001,

    /// <summary>
    /// CTAP command execution has started.
    /// </summary>
    CtapCommandStarted = 2100,

    /// <summary>
    /// CTAP command execution has completed.
    /// </summary>
    CtapCommandCompleted = 2102,

    /// <summary>
    /// CTAP command execution failed.
    /// </summary>
    CtapCommandError = 2103,

    /// <summary>
    /// Device capability and metadata information.
    /// </summary>
    DeviceInfo = 2104,

    /// <summary>
    /// Function-level warning emitted by the platform.
    /// </summary>
    FunctionWarning = 2105,

    /// <summary>
    /// Generic name/value diagnostic payload.
    /// </summary>
    NameValue = 2106,

    /// <summary>
    /// Device state transition information.
    /// </summary>
    DeviceStateInfo = 2110,

    /// <summary>
    /// USB provider operation has started.
    /// </summary>
    UsbProviderStarted = 2200,

    /// <summary>
    /// USB provider operation has completed.
    /// </summary>
    UsbProviderCompleted = 2201,

    /// <summary>
    /// USB provider operation failed.
    /// </summary>
    UsbProviderError = 2202,

    /// <summary>
    /// USB provider warning event.
    /// </summary>
    UsbProviderWarning = 2203,
    /// <summary>
    /// USB device operation has started.
    /// </summary>
    UsbDeviceStarted = 2210,

    /// <summary>
    /// USB device operation has completed.
    /// </summary>
    UsbDeviceCompleted = 2211,

    /// <summary>
    /// USB authenticator has been added.
    /// </summary>
    UsbAddDevice = 2220,

    /// <summary>
    /// USB device change notification.
    /// </summary>
    UsbDeviceChanges = 2222,

    /// <summary>
    /// USB send/receive transport exchange.
    /// </summary>
    UsbSendReceive = 2225,

    /// <summary>
    /// BLE provider operation has started.
    /// </summary>
    BleProviderStarted = 2250,

    /// <summary>
    /// BLE provider warning event.
    /// </summary>
    BleProviderWarning = 2253,

    /// <summary>
    /// BLE function-level warning event.
    /// </summary>
    BleFunctionWarning = 2270,

    /// <summary>
    /// NFC provider operation has started.
    /// </summary>
    NfcProviderStarted = 2300,

    /// <summary>
    /// NFC provider warning event.
    /// </summary>
    NfcProviderWarning = 2303,

    /// <summary>
    /// NFC reader was skipped for the operation.
    /// </summary>
    NfcSkipReader = 2321,

    /// <summary>
    /// Hybrid transport process has started.
    /// </summary>
    HybridProcessStarted = 2329,

    /// <summary>
    /// Hybrid transport process failed.
    /// </summary>
    HybridProcessError = 2331,

    /// <summary>
    /// Hybrid transport write-message operation.
    /// </summary>
    HybridWriteMessage = 2332,

    /// <summary>
    /// Hybrid transport read-message operation.
    /// </summary>
    HybridReadMessage = 2333,

    /// <summary>
    /// Hybrid transport setup has started.
    /// </summary>
    HybridSetupStarted = 2334,

    /// <summary>
    /// Hybrid transport setup has completed.
    /// </summary>
    HybridSetupCompleted = 2335
}
