using EscapeRoomWPF.Models.Items;
using EscapeRoomWPF.Models;
using System;
using System.Windows;

namespace EscapeRoomWPF.Controllers
{
    public class GameController
    {
        public GameMap GameMap { get; private set; }
        public Player Player { get; private set; }


        public event Action RoomUpdated; // Wydarzenie dla widoku mapy
        public event Action PlayerStatusUpdated; // Wydarzenie dla widoku statusu gracza
        public event Action<Item> InteractionRequested; // Wydarzenie dla widoku interakcji
        public GameController(GameMap map, Player player)
        {
            this.GameMap = map;
            this.Player = player;
        }

        public void MoveToNextRoom()
        {
            if (GameMap.NextRoom != null)
            {
                GameMap.CurrentRoom = GameMap.NextRoom; // Ustawienie nowego pokoju
                GameMap.NextRoom = null; // Wyzerowanie NextRoom po przejściu
                MessageBox.Show("Przechodzisz do następnego pokoju!");
            }
            else
            {
                MessageBox.Show("Nie ma kolejnego pokoju do odwiedzenia.");
            }
        }

        public void MovePlayer(string direction)
        {
            int deltaX = 0, deltaY = 0;
            switch (direction.ToLower())
            {
                case "up": deltaY = -1; break;
                case "down": deltaY = 1; break;
                case "left": deltaX = -1; break;
                case "right": deltaX = 1; break;
            }

            int newX = Player.PositionX + deltaX;
            int newY = Player.PositionY + deltaY;

            // Sprawdzenie granic mapy
            if (newX >= 0 && newX < GameMap.CurrentRoom.Map.GetLength(0) &&
                newY >= 0 && newY < GameMap.CurrentRoom.Map.GetLength(1))
            {
                // Sprawdź kolizję z przedmiotami
                if (Player.CanMoveTo(newX, newY, GameMap.CurrentRoom.Items))
                {
                    Player.PositionX = newX;
                    Player.PositionY = newY;

                    RoomUpdated?.Invoke();
                    PlayerStatusUpdated?.Invoke();
                }
                else
                {
                    MessageBox.Show("Nie możesz przejść tutaj – coś blokuje drogę.");
                }
            }
            else
            {
                MessageBox.Show("Nie możesz wyjść poza granice mapy!");
            }
        }


        public void InteractWithCurrentItem(string interaction)
        {
            var item = GameMap.CurrentRoom.GetItemAtPosition(Player.PositionX, Player.PositionY);
            if (item != null)
            {
                item.OnInteract(interaction, Player.Inventory);
                RoomUpdated?.Invoke();
                PlayerStatusUpdated?.Invoke();
            }
        }

    }
}
