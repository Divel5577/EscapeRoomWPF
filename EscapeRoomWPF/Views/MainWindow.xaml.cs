using System.Windows;
using System.Windows.Input;
using EscapeRoomWPF.Models;
using EscapeRoomWPF.Controllers;
using EscapeRoomWPF.Models.Items;
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
            var player = new Player(0, 0);
            var room = new Room(10, 10); // Tworzenie pokoju o wymiarach 10x10
            room.AddItem(new Door(5, 5, "1234") { IsExit = true });
            room.AddItem(new Desk(4, 4));
            room.AddItem(new Cobweb(2, 2));
            room.AddItem(new Chandelier(6, 1));

            var gameMap = new GameMap(player, room);

            // Dodajemy drzwi jako wyjście
            gameMap.CurrentRoom.AddItem(new Door(5, 5, "1234") { IsExit = true });

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
                Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("Assets/player.png", UriKind.Relative))
            };

            Canvas.SetLeft(playerImage, gameController.Player.PositionX * 50);
            Canvas.SetTop(playerImage, gameController.Player.PositionY * 50);
            RoomCanvas.Children.Add(playerImage);
        }



        private void UpdatePlayerStatus()
        {
            PlayerPositionText.Text = $"Pozycja: ({gameController.Player.PositionX}, {gameController.Player.PositionY})";
            PlayerInventoryText.Text = $"Ekwipunek: {string.Join(", ", gameController.Player.Inventory.Items.Select(i => i.Name))}";
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
                gameController.InteractWithCurrentItem(selectedInteraction);
                RenderRoom();
                UpdatePlayerStatus();
            }
        }

        private void OnExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
