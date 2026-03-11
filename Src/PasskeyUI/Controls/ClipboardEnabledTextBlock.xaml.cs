using System.Windows;
using System.Windows.Controls;

namespace DSInternals.Win32.WebAuthn.PasskeyUI
{
    /// <summary>
    /// A TextBlock with an overlay copy button that copies the text to the clipboard.
    /// </summary>
    public partial class ClipboardEnabledTextBlock : UserControl
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(ClipboardEnabledTextBlock),
                new PropertyMetadata(string.Empty));

        public ClipboardEnabledTextBlock()
        {
            InitializeComponent();
        }

        public string? Text
        {
            get => (string?)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                Clipboard.SetText(Text);
            }
        }
    }
}
