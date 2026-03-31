using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Input;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Interop;
using Prism.Commands;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class AttestationOptionsViewModel : BindableBase, IAttestationOptionsViewModel
{
    private const int RandomUserIdLength = 32;

    public AttestationOptionsViewModel(IAlgorithmSelectorViewModel algorithmSelectorViewModel)
    {
        // Save dependencies
        AlgorithmSelectorViewModel = algorithmSelectorViewModel;

        // Configure default values
        Timeout = ApiConstants.DefaultTimeoutMilliseconds;

        // Initialize commands
        ResetOptionsCommand = new DelegateCommand(OnResetOptions);
        GenerateChallengeCommand = new DelegateCommand(OnGenerateChallenge);
        GenerateUserIdCommand = new DelegateCommand(OnGenerateUserId);
    }

    public IAlgorithmSelectorViewModel AlgorithmSelectorViewModel { get; private set; }

    public ICommand ResetOptionsCommand { get; private set; }
    public ICommand GenerateChallengeCommand { get; private set; }
    public ICommand GenerateUserIdCommand { get; private set; }

    private void OnGenerateChallenge()
    {
        Challenge = GetRandomBytes(ApiConstants.DefaultChallengeLength);
    }

    private void OnGenerateUserId()
    {
        UserId = GetRandomBytes(RandomUserIdLength);
    }

    private void OnResetOptions()
    {
        RpId = null;
        RpName = null;
        UserName = null;
        UserDisplayName = null;
        UserId = null;
        Challenge = null;
        ResidentKey = ResidentKeyRequirement.Discouraged;
        AuthenticatorAttachment = AuthenticatorAttachment.Any;
        UserVerificationRequirement = UserVerificationRequirement.Preferred;
        PublicKeyCredentialParameters = [Algorithm.ES256];
        AttestationConveyancePreference = AttestationConveyancePreference.None;
        EnterpriseAttestation = EnterpriseAttestationType.None;
        Timeout = ApiConstants.DefaultTimeoutMilliseconds;
        CredProtectPolicy = UserVerification.Any;
        EnforceCredProtect = false;
        MinPinLength = false;
        HmacSecret = false;
        EnablePseudoRandomFunction = false;
        LargeBlobSupport = LargeBlobSupport.None;
        CredentialBlob = null;
        IsBrowserPrivateMode = false;
        CredentialHint = PublicKeyCredentialHint.None;
        ThirdPartyPayment = false;
        RemoteWebOrigin = null;
    }

    private string _rpId;
    public string RpId
    {
        get => _rpId;
        set
        {
            if (SetProperty(ref _rpId, value))
                RaisePropertyChanged(nameof(IsFormValid));
        }
    }

    private string _rpName;
    public string RpName
    {
        get => _rpName;
        set => SetProperty(ref _rpName, value);
    }

    private string _userName;
    public string UserName
    {
        get => _userName;
        set
        {
            if (SetProperty(ref _userName, value))
                RaisePropertyChanged(nameof(IsFormValid));
        }
    }

    private string _userDisplayName;
    public string UserDisplayName
    {
        get => _userDisplayName;
        set => SetProperty(ref _userDisplayName, value);
    }

    private byte[] _userId;
    public byte[] UserId
    {
        get => _userId;
        set => SetProperty(ref _userId, value);
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

    private ResidentKeyRequirement _residentKey;
    public ResidentKeyRequirement ResidentKey
    {
        get => _residentKey;
        set => SetProperty(ref _residentKey, value);
    }

    public IList<KeyValuePair<ResidentKeyRequirement?, string>> ResidentKeyRequirements
    => EnumAdapter.GetComboBoxItems<ResidentKeyRequirement>();

    private AuthenticatorAttachment _authenticatorAttachment;
    public AuthenticatorAttachment AuthenticatorAttachment
    {
        get => _authenticatorAttachment;
        set => SetProperty(ref _authenticatorAttachment, value);
    }

    public IList<KeyValuePair<AuthenticatorAttachment?, string>> AuthenticatorAttachments
     => EnumAdapter.GetComboBoxItems<AuthenticatorAttachment>();

    private UserVerificationRequirement _userVerificationRequirement;
    public UserVerificationRequirement UserVerificationRequirement
    {
        get => _userVerificationRequirement;
        set => SetProperty(ref _userVerificationRequirement, value);
    }

    public IList<KeyValuePair<UserVerificationRequirement?, string>> UserVerificationRequirements
    => EnumAdapter.GetComboBoxItems<UserVerificationRequirement>();

    private AttestationConveyancePreference _attestationConveyancePreference;
    public AttestationConveyancePreference AttestationConveyancePreference
    {
        get => _attestationConveyancePreference;
        set => SetProperty(ref _attestationConveyancePreference, value);
    }

    public IList<KeyValuePair<AttestationConveyancePreference?, string>> AttestationTypes
    => EnumAdapter.GetComboBoxItems<AttestationConveyancePreference>();

    private EnterpriseAttestationType _enterpriseAttestation;
    public EnterpriseAttestationType EnterpriseAttestation
    {
        get => _enterpriseAttestation;
        set => SetProperty(ref _enterpriseAttestation, value);
    }

    public IList<KeyValuePair<EnterpriseAttestationType?, string>> EnterpriseAttestationTypes
    => EnumAdapter.GetComboBoxItems<EnterpriseAttestationType>();

    private uint _timeout;
    public uint Timeout
    {
        get => _timeout;
        set => SetProperty(ref _timeout, value);
    }

    public IList<KeyValuePair<UserVerification?, string>> CredProtectPolicies
    => EnumAdapter.GetComboBoxItems<UserVerification>();

    public IList<KeyValuePair<LargeBlobSupport?, string>> LargeBlobSupportPolicies
    => EnumAdapter.GetComboBoxItems<LargeBlobSupport>();

    public RelyingPartyInformation RelyingPartyEntity
    {
        get
        {
            return new RelyingPartyInformation()
            {
                Id = RpId,
                Name = RpName
            };
        }
        set
        {
            if (value != null)
            {
                RpId = value.Id;
                RpName = value.Name;
            }
            else
            {
                // Load default values
                RpId = null;
                RpName = null;
            }
        }
    }

    public UserInformation UserEntity
    {
        get
        {
            return new UserInformation()
            {
                Id = UserId,
                DisplayName = UserDisplayName,
                Name = UserName
            };
        }
        set
        {
            if (value != null)
            {
                UserId = value.Id;
                UserName = value.Name;
                UserDisplayName = value.DisplayName;
            }
            else
            {
                // Load default values
                UserId = null;
                UserName = null;
                UserDisplayName = null;
            }
        }
    }

    public List<Algorithm> PublicKeyCredentialParameters
    {
        get => AlgorithmSelectorViewModel.SelectedAlgorithms;
        set => AlgorithmSelectorViewModel.SelectedAlgorithms = value;
    }

    public AuthenticationExtensionsClientInputs? ClientExtensions
    {
        get
        {
            if (CredProtectPolicy == UserVerification.Any && HmacSecret == false && MinPinLength == false && CredentialBlob == null)
            {
                // No extensions are set
                return null;
            }

            return new AuthenticationExtensionsClientInputs()
            {
                CredProtect = this.CredProtectPolicy,
                EnforceCredProtect = this.EnforceCredProtect,
                HmacCreateSecret = this.HmacSecret,
                MinimumPinLength = this.MinPinLength,
                CredentialBlob = this.CredentialBlob?.Length > 0 ? this.CredentialBlob : null
            };
        }
        set
        {
            if (value != null)
            {
                HmacSecret = value.HmacCreateSecret == true;
                CredProtectPolicy = value.CredProtect;
                EnforceCredProtect = value.EnforceCredProtect == true;
                MinPinLength = value.MinimumPinLength == true;
                CredentialBlob = value.CredentialBlob;
            }
            else
            {
                // Load default values
                CredProtectPolicy = UserVerification.Any;
                HmacSecret = false;
                MinPinLength = false;
                CredentialBlob = null;
            }
        }
    }

    private UserVerification _credProtect;
    public UserVerification CredProtectPolicy
    {
        get => _credProtect;
        set
        {
            SetProperty(ref _credProtect, value);

            if (EnforceCredProtect && value == UserVerification.Any)
            {
                // Uncheck Enforce CredProtect
                this.EnforceCredProtect = false;
            }

            RaisePropertyChanged(nameof(EnforceCredProtectEnabled));
        }
    }

    private bool _enforceCredProtect;
    public bool EnforceCredProtect
    {
        get => _enforceCredProtect;
        set => SetProperty(ref _enforceCredProtect, value);
    }

    // Do not allow enforcement of credProtect if it is not enabled.
    public bool EnforceCredProtectEnabled => CredProtectPolicy != UserVerification.Any;

    private bool _minPinLength;
    public bool MinPinLength
    {
        get => _minPinLength;
        set => SetProperty(ref _minPinLength, value);
    }

    private bool _hmacSecret;
    public bool HmacSecret
    {
        get => _hmacSecret;
        set => SetProperty(ref _hmacSecret, value);
    }

    private bool _enablePseudoRandomFunction;
    public bool EnablePseudoRandomFunction
    {
        get => _enablePseudoRandomFunction;
        set => SetProperty(ref _enablePseudoRandomFunction, value);
    }

    private LargeBlobSupport _largeBlobSupport;
    public LargeBlobSupport LargeBlobSupport
    {
        get => _largeBlobSupport;
        set => SetProperty(ref _largeBlobSupport, value);
    }

    private byte[] _credentialBlob;
    public byte[] CredentialBlob
    {
        get => _credentialBlob;
        set => SetProperty(ref _credentialBlob, value);
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

    private bool _thirdPartyPayment;
    public bool ThirdPartyPayment
    {
        get => _thirdPartyPayment;
        set => SetProperty(ref _thirdPartyPayment, value);
    }

    private string? _remoteWebOrigin;
    public string? RemoteWebOrigin
    {
        get => _remoteWebOrigin;
        set => SetProperty(ref _remoteWebOrigin, value);
    }

    public bool IsFormValid =>
        !string.IsNullOrWhiteSpace(RpId) &&
        Challenge is { Length: > 0 } &&
        !string.IsNullOrWhiteSpace(UserName);

    private static byte[] GetRandomBytes(int count)
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] randomBytes = new byte[count];
        rng.GetBytes(randomBytes);
        return randomBytes;
    }
}
