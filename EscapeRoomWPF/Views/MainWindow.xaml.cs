using System.Windows;
using System.Windows.Input;
using EscapeRoomWPF.Models;
using EscapeRoomWPF.Models.Items;
using EscapeRoomWPF.Controllers;
using System.Linq;
using System.Windows.Controls;
using System;

namespace EscapeRoomWPF.Views
{
    public partial class MainWindow : Window
    {
        private GameController gameController;

        public MainWindow()
        {
            InitializeComponent();

            // Inicjalizacja gry
            var player = new Player(4, 5);
            var room = new Room(10, 10); // Tworzenie pokoju o wymiarach 10x10
            room.AddItem(new Bookshelf(1, 0, new Models.Items.Key(2, 3)));
            room.AddItem(new Desk(5, 1));
            room.AddItem(new Cobweb(9, 9));
            room.AddItem(new Chandelier(5, 5));
            room.AddItem(new Door(9, 5, "1234") { IsExit = true }); // Drzwi wyjściowe
            room.AddItem(new Painting(0, 5));

            var gameMap = new GameMap(player, room);

            gameController = new GameController(gameMap, player);

            // Renderowanie początkowego stanu gry
            RenderRoom();
            UpdatePlayerStatus();
        }

        private void RenderRoom()
        {
            RoomCanvas.Children.Clear();

            // Renderowanie przedmiotów w pokoju
            foreach (var item in gameController.GameMap.CurrentRoom.Items)
            {
                var itemImage = new System.Windows.Controls.Image
                {
                    Width = 50,
                    Height = 50,
                    Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(item.ImagePath, UriKind.Relative))
                };

                Canvas.SetLeft(itemImage, item.PositionX * 50);
                Canvas.SetTop(itemImage, item.PositionY * 50);
                RoomCanvas.Children.Add(itemImage);
            }

            // Renderowanie gracza
            var playerImage = new System.Windows.Controls.Image
            {
                Width = 50,
                Height = 50,
                Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("Assets/Images/player.png", UriKind.Relative))
            };

            Canvas.SetLeft(playerImage, gameController.Player.PositionX * 50);
            Canvas.SetTop(playerImage, gameController.Player.PositionY * 50);
            RoomCanvas.Children.Add(playerImage);
        }

        private void UpdateInventoryList()
        {
            InventoryList.Items.Clear();
            foreach (var item in gameController.Player.Inventory.Items)
            {
                InventoryList.Items.Add(item.Name);
            }
        }
        private void UpdatePlayerStatus()
        {
            PlayerPositionText.Text = $"Pozycja: ({gameController.Player.PositionX}, {gameController.Player.PositionY})";
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Up:
                    gameController.MovePlayer("up");
                    break;
                case System.Windows.Input.Key.Down:
                    gameController.MovePlayer("down");
                    break;
                case System.Windows.Input.Key.Left:
                    gameController.MovePlayer("left");
                    break;
                case System.Windows.Input.Key.Right:
                    gameController.MovePlayer("right");
                    break;
            }

            RenderRoom();
            UpdatePlayerStatus();
        }

        private void RoomCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var clickPosition = e.GetPosition(RoomCanvas);

            // Konwersja pozycji kliknięcia na współrzędne siatki
            int clickedX = (int)(clickPosition.X / 50);
            int clickedY = (int)(clickPosition.Y / 50);

            var item = gameController.GameMap.CurrentRoom.GetItemAtPosition(clickedX, clickedY);
            if (item != null)
            {
                InteractionList.Items.Clear();
                foreach (var interaction in item.Interactions.Keys)
                {
                    InteractionList.Items.Add(interaction);
                }

                // Podświetlenie wybranego elementu
                var highlightRectangle = new System.Windows.Shapes.Rectangle
                {
                    Width = 50,
                    Height = 50,
                    Fill = System.Windows.Media.Brushes.Yellow,
                    Opacity = 0.5
                };

                Canvas.SetLeft(highlightRectangle, clickedX * 50);
                Canvas.SetTop(highlightRectangle, clickedY * 50);
                RoomCanvas.Children.Add(highlightRectangle);

                MessageBox.Show($"Kliknięto na obiekt: {item.Name} ({item.Description})");
            }
            else
            {
                MessageBox.Show("Kliknięto na puste pole.");
            }
        }


        private void OnInteractClick(object sender, RoutedEventArgs e)
        {
            var selectedInteraction = InteractionList.SelectedItem?.ToString();
            if (selectedInteraction != null)
            {
                // Znajdź przedmiot w aktualnej pozycji gracza
                var currentItem = gameController.GameMap.CurrentRoom.GetItemAtPosition(
                    gameController.Player.PositionX,
                    gameController.Player.PositionY
                );

                // Znajdź wybrany przedmiot w ekwipunku lub na mapie
                var selectedItemName = InventoryList.SelectedItem?.ToString();
                if (selectedItemName != null)
                {
                    currentItem = gameController.Player.Inventory.Items.FirstOrDefault(i => i.Name == selectedItemName);
                }
                else
                {
                    currentItem = gameController.GameMap.CurrentRoom.GetItemAtPosition(
                        gameController.Player.PositionX,
                        gameController.Player.PositionY
                    );
                }

                if (currentItem != null)
                {
                    // Wywołaj interakcję dla wybranego przedmiotu
                    currentItem.OnInteract(selectedInteraction, gameController.Player.Inventory);

                    // Zaktualizuj widok mapy i ekwipunku
                    RenderRoom();
                    UpdatePlayerStatus();
                }
                else
                {
                    MessageBox.Show("Nie znaleziono przedmiotu do interakcji.");
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano interakcji.");
            }
        }

        private void OnInventoryInteractClick(object sender, RoutedEventArgs e)
        {
            // Pobierz nazwę wybranego przedmiotu w ekwipunku
            var selectedItemName = InventoryList.SelectedItem?.ToString();

            if (selectedItemName != null)
            {
                // Znajdź przedmiot w ekwipunku gracza
                var selectedItem = gameController.Player.Inventory.Items.FirstOrDefault(i => i.Name == selectedItemName);

                if (selectedItem != null)
                {
                    // Wyświetl dostępne interakcje dla wybranego przedmiotu
                    InteractionList.Items.Clear();
                    foreach (var interaction in selectedItem.Interactions.Keys)
                    {
                        InteractionList.Items.Add(interaction);
                    }

                    MessageBox.Show($"Wybrano przedmiot: {selectedItem.Name} ({selectedItem.Description})");
                }
                else
                {
                    MessageBox.Show("Nie znaleziono przedmiotu w ekwipunku.");
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano przedmiotu z ekwipunku.");
            }
        }



        private void OnExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
