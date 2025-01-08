using System.Windows;

namespace EscapeRoomWPF.Views
{
    public partial class InputDialog : Window
    {
        public string InputText { get; private set; }

        public InputDialog()
        {
            InitializeComponent();
        }

        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            InputText = InputTextBox.Text;
            DialogResult = true;
        }
    }
}
