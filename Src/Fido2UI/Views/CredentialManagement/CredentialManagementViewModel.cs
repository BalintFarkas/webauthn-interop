using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.Fido2UI;

public class CredentialManagementViewModel : BindableBase, ICredentialManagementViewModel
{
    public CredentialManagementViewModel()
    {
        // Initialize commands
        ResetFilterCommand = new DelegateCommand(OnResetFilter);
    }

    public ICommand ResetFilterCommand { get; private set; }

    private void OnResetFilter()
    {
        this.RelyingPartyId = null;
        this.IsBrowserPrivateMode = false;
    }

    public string? RelyingPartyId
    {
        get;
        set => SetProperty(ref field, value);
    }

    public bool IsBrowserPrivateMode
    {
        get;
        set => SetProperty(ref field, value);
    }
}
