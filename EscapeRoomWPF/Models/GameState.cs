using EscapeRoomWPF.Models.Items;
using System.Collections.Generic;

namespace EscapeRoomWPF.Models
{
    public class GameState
    {
        public int PlayerPositionX { get; set; }
        public int PlayerPositionY { get; set; }
        public List<Item> Inventory { get; set; }
        public List<Item> RoomItems { get; set; }
    }
}
