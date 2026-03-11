using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DSInternals.Win32.WebAuthn.PasskeyUI
{
    internal interface IAuthenticatorListViewModel
    {
        ObservableCollection<AuthenticatorDetails> Authenticators { get; }

        ObservableCollection<AuthenticatorPluginInformation> PluginAuthenticators { get; }

        ICommand ListAuthenticatorsCommand { get; }

        ICommand ListPluginAuthenticatorsCommand { get; }
    }
}
