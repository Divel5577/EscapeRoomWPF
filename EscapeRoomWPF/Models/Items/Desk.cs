﻿using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Desk : Item
    {
        public bool IsSearched { get; set; }
        public Desk() : base() { }

        public Desk(int positionX, int positionY)
            : base("Biurko", "Stary drewniany stół.", false, true, positionX, positionY, "Assets/Images/desk.png")
        {
            IsSearched = false;
            InitializeInteractions();
        }

        public override void InitializeInteractions()
        {
            AddInteraction("Przeszukaj", inventory =>
            {
                if (!IsSearched)
                {
                    IsSearched = true;
                    var journal = new Journal(PositionX, PositionY);
                    MessageBox.Show("Przeszukując biurko, znajdujesz stary dziennik.");
                    inventory.AddItem(journal);
                }
                else
                {
                    MessageBox.Show("Biurko zostało już przeszukane.");
                }
            });
        }
    }
}