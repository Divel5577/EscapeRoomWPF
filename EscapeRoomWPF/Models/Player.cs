using EscapeRoomWPF.Models.Items;
using System.Collections.Generic;
using System.Linq;

namespace EscapeRoomWPF.Models
{
    public class Player
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int LastPositionX { get; set; }
        public int LastPositionY { get; set; }
        public Inventory Inventory { get; set; }
        public Player()
        {
            Inventory = new Inventory();
        }
        public Player(int startX, int startY) : this()
        {
            PositionX = startX;
            PositionY = startY;
            LastPositionX = startX;
            LastPositionY = startY;
        }

        public void Move(int deltaX, int deltaY)
        {
            LastPositionX = PositionX;
            LastPositionY = PositionY;
            PositionX += deltaX;
            PositionY += deltaY;
        }
        public bool CanMoveTo(int newX, int newY, List<Item> items)
        {
            // Sprawdź, czy na danej pozycji znajduje się kolizyjny przedmiot
            var blockingItem = items.FirstOrDefault(item => item.PositionX == newX && item.PositionY == newY && item.IsCollidable);
            // Jeśli nie znaleziono kolizyjnego przedmiotu, ruch jest możliwy
            return blockingItem == null;
        }
    }
}
