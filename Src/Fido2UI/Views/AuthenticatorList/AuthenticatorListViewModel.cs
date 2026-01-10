using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Prism.Dialogs;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.Fido2UI;

public class AuthenticatorListViewModel : BindableBase, IAuthenticatorListViewModel
{
    private readonly IDialogService _dialogService;

    public AuthenticatorListViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;

        // Initialize commands
        ListAuthenticatorsCommand = new DelegateCommand(OnListAuthenticators);
        ListPluginAuthenticatorsCommand = new DelegateCommand(OnListPluginAuthenticators);
    }

    public ObservableCollection<AuthenticatorDetails> Authenticators { get; } = [];

    public ObservableCollection<AuthenticatorPluginInformation> PluginAuthenticators { get; } = [];

    public ICommand ListAuthenticatorsCommand { get; }

    public ICommand ListPluginAuthenticatorsCommand { get; }

    private void OnListAuthenticators()
    {
        try
        {
            // Clear the results first
            Authenticators.Clear();

            var authenticators = WebAuthnApi.GetAuthenticatorList();

            // Populate the collection for the grid view
            if (authenticators != null)
            {
                foreach (var authenticator in authenticators)
                {
                    Authenticators.Add(authenticator);
                }
            }
        }
        catch (Exception ex)
        {
            DialogParameters parameters = new($"Message={ex.Message}");
            _dialogService.ShowDialog(nameof(NotificationDialog), parameters);
        }
    }

    private void OnListPluginAuthenticators()
    {
        try
        {
            // Clear the results first
            PluginAuthenticators.Clear();

            var plugins = WebAuthnApi.GetPluginAuthenticators();

            // Populate the collection for the grid view
            if (plugins != null)
            {
                foreach (var plugin in plugins)
                {
                    PluginAuthenticators.Add(plugin);
                }
            }
        }
        catch (Exception ex)
        {
            DialogParameters parameters = new($"Message={ex.Message}");
            _dialogService.ShowDialog(nameof(NotificationDialog), parameters);
        }
    }
}
