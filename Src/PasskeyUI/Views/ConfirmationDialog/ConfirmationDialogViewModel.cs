using Prism.Commands;
using Prism.Dialogs;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class ConfirmationDialogViewModel : BindableBase, IDialogAware
{
    private DelegateCommand _confirmCommand;
    public DelegateCommand ConfirmCommand
    {
        get => _confirmCommand;
        private set => SetProperty(ref _confirmCommand, value);
    }

    private DelegateCommand _cancelCommand;
    public DelegateCommand CancelCommand
    {
        get => _cancelCommand;
        private set => SetProperty(ref _cancelCommand, value);
    }

    private string? _message;
    public string? Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }

    private string _title = "Confirm Action";
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
