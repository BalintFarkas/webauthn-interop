using Prism.Commands;
using Prism.Mvvm;
using Prism.Dialogs;

namespace DSInternals.Win32.WebAuthn.Fido2UI;

public class ConfirmationDialogViewModel : BindableBase, IDialogAware
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
        Message = parameters.GetValue<string>(nameof(Message));

        var title = parameters.GetValue<string>(nameof(Title));

        if (!string.IsNullOrEmpty(title))
        {
            Title = title;
        }
    }

    public ConfirmationDialogViewModel()
    {
        ConfirmCommand = new(() => RequestClose.Invoke(ButtonResult.OK));
        CancelCommand = new(() => RequestClose.Invoke(ButtonResult.Cancel));
    }
}
