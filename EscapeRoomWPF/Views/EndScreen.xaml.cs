using System.Linq;
using System.Windows;

namespace EscapeRoomWPF.Views
{
    public partial class EndScreen : Window
    {
        public EndScreen(string elapsedTime)
        {
            InitializeComponent();
            ElapsedTimeText.Text = $"Twój czas gry: {elapsedTime}";
        }

        private void OnExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
