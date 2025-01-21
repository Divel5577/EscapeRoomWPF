using System.Diagnostics;
using System;
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

        private void OnShareClick(object sender, RoutedEventArgs e)
        {
            // Pobranie wyniku z aplikacji
            string elapsedTime = ElapsedTimeText.Text.Replace("Twój czas gry: ", "");
            string username = "AnonimowyGracz";

            // Budowa URL z parametrami
            string leaderboardUrl = $"https://graccz103.github.io/leaderboard-wpf/?username={Uri.EscapeDataString(username)}&score={Uri.EscapeDataString(elapsedTime)}";

            try
            {
                // Otwórz URL w domyślnej przeglądarce
                Process.Start(new ProcessStartInfo
                {
                    FileName = leaderboardUrl,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Nie udało się otworzyć strony leaderboardu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnShareToFriendClick(object sender, RoutedEventArgs e)
        {
            {
                // Treść wiadomości do udostępnienia
                string message = $"Uciekłem z pokoju w grze EscapeRoomWPF w czasie: {ElapsedTimeText.Text.Replace("Twój czas gry: ", "")}! Spróbuj mnie pokonać! Link do gry: https://github.com/Divel5577/EscapeRoomWPF";

                // URL do Messengera z zakodowaną wiadomością
                string messengerUrl = $"https://www.messenger.com/t?text={Uri.EscapeDataString(message)}";

                try
                {
                    // Otwórz URL w domyślnej przeglądarce
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = messengerUrl,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nie udało się otworzyć Messengera: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
