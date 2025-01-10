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

            // Dodanie interakcji "Otwórz"
            AddInteraction("Otwórz", inventory =>
            {
                var dialog = new InputDialog
                {
                    Owner = Application.Current.MainWindow // Ustawienie głównego okna jako właściciela
                };

                if (dialog.ShowDialog() == true) // Otwórz okno dialogowe
                {
                    string inputCode = dialog.InputText;
                    if (inputCode == Code)
                    {
                        IsOpen = true;
                        MessageBox.Show("Drzwi zostały otwarte!");

                        // Możesz tutaj wywołać dodatkowe akcje, np. zmianę pokoju
                        gameController?.MoveToNextRoom();
                    }
                    else
                    {
                        MessageBox.Show("Kod niepoprawny.");
                    }
                }
            });
        }


        public void SetGameController(GameController controller)
        {
            gameController = controller;
        }

        public override void OnInteract(string interaction, Inventory inventory)
        {
            if (interaction == "Otwórz")
            {
                var dialog = new InputDialog
                {
                    Owner = Application.Current.MainWindow // Ustawienie głównego okna jako właściciela
                };

                if (dialog.ShowDialog() == true) // Otwórz okno dialogowe
                {
                    string inputCode = dialog.InputText;
                    if (inputCode == Code)
                    {
                        IsOpen = true;
                        MessageBox.Show("Drzwi zostały otwarte!");
                    }
                    else
                    {
                        MessageBox.Show("Kod niepoprawny.");
                    }
                }
            }
        }

    }
}
