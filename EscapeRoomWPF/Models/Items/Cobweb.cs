﻿using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Cobweb : Item
    {
        public Cobweb() : base() { }

        public Cobweb(int positionX, int positionY)
            : base("Pajęczyna", "Gęsta pajęczyna pokrywająca róg pokoju.", false, false, positionX, positionY, "Assets/Images/cobweb.png")
        {
            InitializeInteractions();
        }

        public override void InitializeInteractions()
        {
            AddInteraction("Oglądaj", inventory =>
            {
                MessageBox.Show("Pajęczyna jest pełna kurzu i pająków. Nic tu nie znajdziesz.");
            });
        }
    }
}
