using System.Windows;
using System.Windows.Input;
using EscapeRoomWPF.Models;
using EscapeRoomWPF.Models.Items;
using EscapeRoomWPF.Controllers;
using EscapeRoomWPF.Helpers;
using System.Linq;
using System.Windows.Controls;
using System;
using System.Collections.Generic;

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
            var room = new Room(10, 10);
            var gameMap = new GameMap(player, room);

            gameController = new GameController(gameMap, player);

            // Dodanie przedmiotów do pokoju z ustawieniami kolizji
            room.AddItem(new Bookshelf(1, 0) { IsCollidable = true });
            room.AddItem(new Desk(5, 1) { IsCollidable = true });
            room.AddItem(new Cobweb(9, 9) { IsCollidable = false });
            room.AddItem(new Chandelier(5, 5) { IsCollidable = false });
            room.AddItem(new Door(9, 5, "1234", gameController) { IsExit = true, IsCollidable = true }); // Drzwi wyjściowe
            room.AddItem(new Painting(0, 5) { IsCollidable = true });
            gameController.StartGameTimer();

            // Renderowanie początkowego stanu gry
            RenderRoom();
            UpdatePlayerStatus();
        }

        // Renderowanie pokoju i pozycji gracza
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

        // Aktualizacja listy ekwipunku
        private void UpdateInventoryList()
        {
            InventoryList.Items.Clear();
            foreach (var item in gameController.Player.Inventory.Items)
            {
                InventoryList.Items.Add(item.Name);
            }
        }
        // Aktualizacja statusu gracza
        private void UpdatePlayerStatus()
        {
            PlayerPositionText.Text = $"Pozycja: ({gameController.Player.PositionX}, {gameController.Player.PositionY})";
        }

        // Pobranie przedmiotów w otoczeniu gracza
        private List<Item> GetNearbyItems()
        {
            int playerX = gameController.Player.PositionX;
            int playerY = gameController.Player.PositionY;

            // Filtr przedmiotów w promieniu 1 kratki od gracza
            return gameController.GameMap.CurrentRoom.Items.Where(item =>
                Math.Abs(item.PositionX - playerX) <= 1 &&
                Math.Abs(item.PositionY - playerY) <= 1
            ).ToList();
        }

        // Obsługa ruchu gracza za pomocą klawiszy strzałek
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsEnabled) return; // Jeśli okno jest zablokowane, nic nie rób
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

        // Obsługa kliknięcia na obszar mapy
        private void RoomCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabled) return; // Jeśli okno jest zablokowane, nic nie rób

            var clickPosition = e.GetPosition(RoomCanvas);

            // Konwersja pozycji kliknięcia na współrzędne siatki
            int clickedX = (int)(clickPosition.X / 50);
            int clickedY = (int)(clickPosition.Y / 50);

            var nearbyItems = GetNearbyItems();
            if (nearbyItems.Count > 0)
            {
                InteractionList.Items.Clear();
                foreach (var item in nearbyItems)
                {
                    foreach (var interaction in item.Interactions.Keys)
                    {
                        InteractionList.Items.Add($"{item.Name}: {interaction}");
                    }
                }
                MessageBox.Show("Znaleziono przedmiot(y) w otoczeniu. Wybierz interakcję.");
            }
            else
            {
                MessageBox.Show("Brak przedmiotów w otoczeniu.");
            }
        }

        // Obsługa kliknięcia na przycisk interakcji
        private void OnInteractClick(object sender, RoutedEventArgs e)
        {
            var selectedInteraction = InteractionList.SelectedItem?.ToString();
            if (selectedInteraction != null)
            {
                Item currentItem = null;

                // Sprawdź, czy przedmiot jest wybrany z ekwipunku
                var selectedItemName = InventoryList.SelectedItem?.ToString();
                if (selectedItemName != null)
                {
                    // Znajdź przedmiot w ekwipunku na podstawie nazwy
                    currentItem = gameController.Player.Inventory.Items.FirstOrDefault(i => i.Name == selectedItemName);
                }
                else
                {
                    // Jeśli nie wybrano przedmiotu z ekwipunku, znajdź przedmiot w otoczeniu gracza
                    var parts = selectedInteraction.Split(new[] { ": " }, StringSplitOptions.None);
                    if (parts.Length == 2)
                    {
                        var itemName = parts[0];
                        var interaction = parts[1];

                        currentItem = GetNearbyItems().FirstOrDefault(i => i.Name == itemName);
                        selectedInteraction = interaction; // Przypisz tylko nazwę interakcji
                    }
                }

                if (currentItem != null)
                {
                    // Wykonaj interakcję, jeśli przedmiot i interakcja są prawidłowe
                    if (currentItem.Interactions.ContainsKey(selectedInteraction))
                    {
                        currentItem.OnInteract(selectedInteraction, gameController.Player.Inventory);

                        // Zaktualizuj widok po interakcji
                        RenderRoom();
                        UpdatePlayerStatus();
                        UpdateInventoryList();
                    }
                    else
                    {
                        MessageBox.Show($"Wybrany przedmiot nie obsługuje interakcji '{selectedInteraction}'.");
                    }
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

        // Obsługa kliknięcia na przycisk interakcji z ekwipunku
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

        // Obsługa kliknięcia na przycisk zapisu gry
        private void OnSaveGameClick(object sender, RoutedEventArgs e)
        {
            var gameState = new GameState
            {
                PlayerPositionX = gameController.Player.PositionX,
                PlayerPositionY = gameController.Player.PositionY,
                Inventory = gameController.Player.Inventory.Items,
                RoomItems = gameController.GameMap.CurrentRoom.Items
            };

            GameSaveLoad.SaveGame(gameState);
            MessageBox.Show("Gra została zapisana!");
        }

        // Przywrócenie stanu gry
        public void RestoreGameState(GameState gameState)
        {
            gameController.Player.PositionX = gameState.PlayerPositionX;
            gameController.Player.PositionY = gameState.PlayerPositionY;
            gameController.Player.Inventory.Items.Clear();
            gameController.Player.Inventory.Items.AddRange(gameState.Inventory);
            gameController.GameMap.CurrentRoom.Items.Clear();
            gameController.GameMap.CurrentRoom.Items.AddRange(gameState.RoomItems);

            // Przywrócenie interakcji dla przedmiotów w pokoju
            foreach (var item in gameController.GameMap.CurrentRoom.Items)
            {
                item.InitializeInteractions();
                if (item is Door door)
                {
                    door.SetGameController(gameController); // Przypisanie kontrolera
                }
            }

            // Przywrócenie interakcji dla przedmiotów w pokoju
            foreach (var item in gameController.GameMap.CurrentRoom.Items)
            {
                item.InitializeInteractions();
            }

            // Przywrócenie interakcji dla przedmiotów w ekwipunku
            foreach (var item in gameController.Player.Inventory.Items)
            {
                item.InitializeInteractions();
            }

            RenderRoom();
            UpdatePlayerStatus();
            UpdateInventoryList();
        }

        // Obsługa zamykania aplikacji
        private void OnExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}