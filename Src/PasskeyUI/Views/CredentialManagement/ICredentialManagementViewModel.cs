using System.Windows.Input;

namespace DSInternals.Win32.WebAuthn.PasskeyUI
{
    public interface ICredentialManagementViewModel
    {
        ICommand ResetFilterCommand { get; }

        string? RelyingPartyId { get; set; }

        bool IsBrowserPrivateMode { get; set; }
    }
}
