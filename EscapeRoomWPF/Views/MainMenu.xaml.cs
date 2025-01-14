using System.Windows;

namespace EscapeRoomWPF.Views
{
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            // Otwórz główne okno gry
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close(); // Zamknij menu główne
        }

        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funkcja wczytywania gry jest jeszcze w trakcie implementacji.", "Wczytaj Grę");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Zamknij aplikację
        }
    }
}
