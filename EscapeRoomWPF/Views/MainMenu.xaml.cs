using System.Windows;
using EscapeRoomWPF.Helpers;

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
            var gameState = GameSaveLoad.LoadGame();
            if (gameState == null)
            {
                MessageBox.Show("Brak zapisanego stanu gry.");
                return;
            }

            var mainWindow = new MainWindow();

            // Przywrócenie stanu gry
            mainWindow.RestoreGameState(gameState);

            mainWindow.Show();
            this.Close();
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Zamknij aplikację
        }
    }
}
