using Prism.Commands;
using Prism.Dialogs;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

public class NotificationDialogViewModel : BindableBase, IDialogAware
{
    public DelegateCommand CloseDialogCommand
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
    } = "Error";

    public DialogCloseListener RequestClose { get; }

    public bool CanCloseDialog() => true;

    public void OnDialogClosed()
    {
    }

    public void OnDialogOpened(IDialogParameters parameters)
    {
        Message = parameters.GetValue<string>(nameof(Message));
    }

    public NotificationDialogViewModel()
    {
        CloseDialogCommand = new(() => RequestClose.Invoke(ButtonResult.OK));
    }
}
