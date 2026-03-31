using Prism.Commands;
using Prism.Dialogs;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class NotificationDialogViewModel : BindableBase, IDialogAware
{
    private DelegateCommand _closeDialogCommand;
    public DelegateCommand CloseDialogCommand
    {
        get => _closeDialogCommand;
        private set => SetProperty(ref _closeDialogCommand, value);
    }

    private string? _message;
    public string? Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }

    private string _title = "Error";
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public DialogCloseListener RequestClose { get; }

    public bool CanCloseDialog() => true;

    public void OnDialogClosed()
    {
    }

    public void OnDialogOpened(IDialogParameters parameters)
    {
        Message = NotificationDialogParameters.From(parameters).Message;
    }

    public NotificationDialogViewModel()
    {
        CloseDialogCommand = new(() => RequestClose.Invoke(ButtonResult.OK));
    }
}
