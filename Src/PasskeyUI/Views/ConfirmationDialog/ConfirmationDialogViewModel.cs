using Prism.Commands;
using Prism.Dialogs;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class ConfirmationDialogViewModel : BindableBase, IDialogAware
{
    public DelegateCommand ConfirmCommand
    {
        get;
        private set => SetProperty(ref field, value);
    }

    public DelegateCommand CancelCommand
    {
        get;
        private set => SetProperty(ref field, value);
    }

    public string? Message
    {
        get;
        set => SetProperty(ref field, value);
    }

    public string Title
    {
        get;
        set => SetProperty(ref field, value);
    } = "Confirm Action";

    public DialogCloseListener RequestClose { get; }

    public bool CanCloseDialog() => true;

    public void OnDialogClosed()
    {
    }

    public void OnDialogOpened(IDialogParameters parameters)
    {
        var p = ConfirmationDialogParameters.From(parameters);
        Message = p.Message;
        if (!string.IsNullOrEmpty(p.Title))
            Title = p.Title;
    }

    public ConfirmationDialogViewModel()
    {
        ConfirmCommand = new(() => RequestClose.Invoke(ButtonResult.OK));
        CancelCommand = new(() => RequestClose.Invoke(ButtonResult.Cancel));
    }
}
