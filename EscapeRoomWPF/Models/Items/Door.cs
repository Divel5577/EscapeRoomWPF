using EscapeRoomWPF.Controllers;
using EscapeRoomWPF.Models.Items;
using EscapeRoomWPF.Models;
using EscapeRoomWPF.Views;
using System;
using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Door : Item
    {
        public string Code { get; }
        public bool IsOpen { get; private set; }
        public bool IsExit { get; set; }

        private GameController gameController;

        public Door() : base() { }

        public Door(int positionX, int positionY, string code, GameController controller = null)
            : base("Drzwi", "Metalowe drzwi z klawiaturą numeryczną. Musisz wpisać kod, aby je otworzyć.", false, positionX, positionY, "Assets/Images/door.png")
        {
            Code = code;
            IsOpen = false;
            gameController = controller;
            InitializeInteractions();
        }

        public void SetGameController(GameController controller)
        {
            gameController = controller;
        }
        public override void OnInteract(string interaction, Inventory inventory)
        {
            if (Interactions.ContainsKey(interaction))
            {
                Interactions[interaction](inventory);
            }
        }
        public override void InitializeInteractions()
        {
            AddInteraction("Otwórz", inventory =>
            {
                var dialog = new InputDialog
                {
                    Owner = Application.Current.MainWindow
                };

                if (dialog.ShowDialog() == true)
                {
                    string inputCode = dialog.InputText;
                    if (inputCode == Code)
                    {
                        IsOpen = true;

                        if (IsExit && gameController != null)
                        {
                            gameController.StopGameTimer();
                            var elapsedTime = gameController.GetElapsedTime();

                            // Zamknięcie głównego okna gry
                            Application.Current.MainWindow.Close();

                            // Wyświetlenie ekranu końcowego
                            var endScreen = new EndScreen(elapsedTime.ToString(@"mm\:ss"));
                            endScreen.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Drzwi zostały otwarte!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kod niepoprawny.");
                    }
                }
            });
        }
    }
}
