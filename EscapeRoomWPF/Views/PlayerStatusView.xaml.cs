using EscapeRoomWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EscapeRoomWPF.Views
{
    public partial class PlayerStatusView : UserControl
    {
        public PlayerStatusView()
        {
            InitializeComponent();
        }

        public void UpdateStatus(Player player)
        {
            PlayerPositionText.Text = $"Pozycja: ({player.PositionX}, {player.PositionY})";        }
    }
}
