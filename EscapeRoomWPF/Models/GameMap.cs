using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EscapeRoomWPF.Models;
using EscapeRoomWPF.Models.Items;

namespace EscapeRoomWPF.Models
{
    public class GameMap
    {
        public Room CurrentRoom { get; set; }
        public Room NextRoom { get; set; }
        private Player player;

        public GameMap(Player player, Room startRoom)
        {
            this.player = player;
            CurrentRoom = startRoom;
        }

        public void MovePlayer(string direction)
        {
            Item exit = CurrentRoom.GetExit();
            if (CurrentRoom != null)
            {
                // Resetuje pozycję gracza w nowym pokoju (przykładowo na początek pokoju)
                player.PositionX = 0;
                player.PositionY = 0;
            }
            else
            {
                MessageBox.Show("Nie możesz pójść w tym kierunku.");
            }
        }
    }

}
