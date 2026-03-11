using System.Windows;
using System.Windows.Controls;

namespace DSInternals.Win32.WebAuthn.PasskeyUI
{
    public partial class AttestationSigningDialog : UserControl
    {
        public AttestationSigningDialog()
        {
            InitializeComponent();
        }

        private void OnLoadPresetClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.ContextMenu != null)
            {
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.IsOpen = true;
            }
        }
    }
}
