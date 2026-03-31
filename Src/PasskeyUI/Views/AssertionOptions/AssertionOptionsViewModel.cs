using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Input;
using DSInternals.Win32.WebAuthn.Interop;
using Prism.Commands;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class AssertionOptionsViewModel : BindableBase, IAssertionOptionsViewModel
{
    public AssertionOptionsViewModel()
    {
        // Configure default values
        Timeout = ApiConstants.DefaultTimeoutMilliseconds;

        // Initialize commands
        ResetOptionsCommand = new DelegateCommand(OnResetOptions);
        GenerateChallengeCommand = new DelegateCommand(OnGenerateChallenge);
        GenerateHmacSecretSalt1Command = new DelegateCommand(OnGenerateHmacSecretSalt1);
        GenerateHmacSecretSalt2Command = new DelegateCommand(OnGenerateHmacSecretSalt2);
    }

    public ICommand ResetOptionsCommand { get; private set; }
    public ICommand GenerateChallengeCommand { get; private set; }
    public ICommand GenerateHmacSecretSalt1Command { get; private set; }
    public ICommand GenerateHmacSecretSalt2Command { get; private set; }

    private void OnResetOptions()
    {
        RelyingPartyId = string.Empty;
        Challenge = null;
        LargeBlob = null;
        UserVerificationRequirement = UserVerificationRequirement.Preferred;
        AuthenticatorAttachment = AuthenticatorAttachment.Any;
        LargeBlobOperation = CredentialLargeBlobOperation.None;
        Timeout = ApiConstants.DefaultTimeoutMilliseconds;
        AppId = null;
        GetCredentialBlob = false;
        HmacSecretSalt1 = null;
        HmacSecretSalt2 = null;
        IsBrowserPrivateMode = false;
        CredentialHint = PublicKeyCredentialHint.None;
        RemoteWebOrigin = null;
    }

    private void OnGenerateChallenge()
    {
        Challenge = GetRandomBytes(ApiConstants.DefaultChallengeLength);
    }

    private void OnGenerateHmacSecretSalt1()
    {
        HmacSecretSalt1 = GetRandomBytes(ApiConstants.CtapOneHmacSecretLength);
    }

    private void OnGenerateHmacSecretSalt2()
    {
        HmacSecretSalt2 = GetRandomBytes(ApiConstants.CtapOneHmacSecretLength);
    }

    public uint HmacSecretSaltStringLength => 2 * ApiConstants.CtapOneHmacSecretLength; // HEX length

    private string _relyingPartyId;
    public string RelyingPartyId
    {
        get => _relyingPartyId;
        set
        {
            if (SetProperty(ref _relyingPartyId, value))
                RaisePropertyChanged(nameof(IsFormValid));
        }
    }

    private byte[]? _challenge;
    public byte[]? Challenge
    {
        get => _challenge;
        set
        {
            if (SetProperty(ref _challenge, value))
                RaisePropertyChanged(nameof(IsFormValid));
        }
    }

    private byte[]? _largeBlob;
    public byte[]? LargeBlob
    {
        get => _largeBlob;
        set => SetProperty(ref _largeBlob, value);
    }

    private UserVerificationRequirement _userVerificationRequirement;
    public UserVerificationRequirement UserVerificationRequirement
    {
        get => _userVerificationRequirement;
        set => SetProperty(ref _userVerificationRequirement, value);
    }

    public IList<KeyValuePair<UserVerificationRequirement?, string>> UserVerificationRequirements
    => EnumAdapter.GetComboBoxItems<UserVerificationRequirement>();

    private AuthenticatorAttachment _authenticatorAttachment;
    public AuthenticatorAttachment AuthenticatorAttachment
    {
        get => _authenticatorAttachment;
        set => SetProperty(ref _authenticatorAttachment, value);
    }

    public IList<KeyValuePair<AuthenticatorAttachment?, string>> AuthenticatorAttachments
     => EnumAdapter.GetComboBoxItems<AuthenticatorAttachment>();

    private CredentialLargeBlobOperation _largeBlobOperation;
    public CredentialLargeBlobOperation LargeBlobOperation
    {
        get => _largeBlobOperation;
        set => SetProperty(ref _largeBlobOperation, value);
    }

    public IList<KeyValuePair<CredentialLargeBlobOperation?, string>> LargeBlobOperations
     => EnumAdapter.GetComboBoxItems<CredentialLargeBlobOperation>();

    private uint _timeout;
    public uint Timeout
    {
        get => _timeout;
        set => SetProperty(ref _timeout, value);
    }

    private string _appId;
    public string AppId
    {
        get => _appId;
        set => SetProperty(ref _appId, value);
    }

    public AuthenticationExtensionsClientInputs? ClientExtensions
    {
        get
        {
            if (string.IsNullOrEmpty(AppId) && GetCredentialBlob == false && HmacSecretSalt1 == null && HmacSecretSalt2 == null)
            {
                // No extensions are set
                return null;
            }

            var result = new AuthenticationExtensionsClientInputs()
            {
                AppID = this.AppId,
                GetCredentialBlob = this.GetCredentialBlob
            };

            if (this.HmacSecretSalt1 != null || this.HmacSecretSalt2 != null)
            {
                result.HmacGetSecret = new HMACGetSecretInput()
                {
                    Salt1 = this.HmacSecretSalt1,
                    Salt2 = this.HmacSecretSalt2
                };
            }

            return result;
        }
        set
        {
            if (value != null)
            {
                AppId = value.AppID;
                GetCredentialBlob = value.GetCredentialBlob == true;

                if (value.HmacGetSecret != null)
                {
                    HmacSecretSalt1 = value.HmacGetSecret.Salt1;
                    HmacSecretSalt2 = value.HmacGetSecret.Salt2;
                }
            }
            else
            {
                // Load default values
                AppId = null;
                GetCredentialBlob = false;
                HmacSecretSalt1 = null;
                HmacSecretSalt2 = null;
            }
        }
    }

    private bool _getCredentialBlob;
    public bool GetCredentialBlob
    {
        get => _getCredentialBlob;
        set => SetProperty(ref _getCredentialBlob, value);
    }

    private byte[] _hmacSecretSalt1;
    public byte[] HmacSecretSalt1
    {
        get => _hmacSecretSalt1;
        set
        {
            if (SetProperty(ref _hmacSecretSalt1, value))
            {
                RaisePropertyChanged(nameof(HmacSecretSalt1String));
            }
        }
    }

    public string HmacSecretSalt1String
    {
        get => _hmacSecretSalt1?.ToHex(caps: true) ?? string.Empty;
        set
        {
            byte[] binaryValue = value?.HexToBinary();

            if (binaryValue != null && binaryValue.Length != HmacSecretSaltStringLength)
            {
                throw new ArgumentOutOfRangeException(nameof(HmacSecretSalt1String));
            }

            if (SetProperty(ref _hmacSecretSalt1, binaryValue, nameof(HmacSecretSalt1)))
            {
                RaisePropertyChanged(nameof(HmacSecretSalt1String));
            }
        }
    }

    private byte[]? _hmacSecretSalt2;
    public byte[]? HmacSecretSalt2
    {
        get => _hmacSecretSalt2;
        set
        {
            if (SetProperty(ref _hmacSecretSalt2, value))
            {
                RaisePropertyChanged(nameof(HmacSecretSalt2String));
            }
        }
    }

    public string? HmacSecretSalt2String
    {
        get => _hmacSecretSalt2?.ToHex(caps: true) ?? string.Empty;
        set
        {
            byte[]? binaryValue = value?.HexToBinary();

            if (binaryValue != null && binaryValue.Length != HmacSecretSaltStringLength)
            {
                throw new ArgumentOutOfRangeException(nameof(HmacSecretSalt2String));
            }

            if (SetProperty(ref _hmacSecretSalt2, binaryValue, nameof(HmacSecretSalt2)))
            {
                RaisePropertyChanged(nameof(HmacSecretSalt2String));
            }
        }
    }

    private bool _isBrowserPrivateMode;
    public bool IsBrowserPrivateMode
    {
        get => _isBrowserPrivateMode;
        set => SetProperty(ref _isBrowserPrivateMode, value);
    }

    private PublicKeyCredentialHint _credentialHint;
    public PublicKeyCredentialHint CredentialHint
    {
        get => _credentialHint;
        set => SetProperty(ref _credentialHint, value);
    }

    public IList<KeyValuePair<PublicKeyCredentialHint?, string>> CredentialHints
    => EnumAdapter.GetComboBoxItems<PublicKeyCredentialHint>();

    private string? _remoteWebOrigin;
    public string? RemoteWebOrigin
    {
        get => _remoteWebOrigin;
        set => SetProperty(ref _remoteWebOrigin, value);
    }

    public bool IsFormValid =>
        !string.IsNullOrWhiteSpace(RelyingPartyId) &&
        Challenge is { Length: > 0 };

    private static byte[] GetRandomBytes(uint count)
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] randomBytes = new byte[count];
        rng.GetBytes(randomBytes);
        return randomBytes;
    }
}
