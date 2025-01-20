using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Desk : Item
    {
        public Desk() : base() { }

        public Desk(int positionX, int positionY)
            : base("Biurko", "Stary drewniany stół.", false, positionX, positionY, "Assets/Images/desk.png")
        {
            InitializeInteractions();
        }

        public override void InitializeInteractions()
        {
            AddInteraction("Przeszukaj", inventory =>
            {
                var journal = new Journal(PositionX, PositionY);
                inventory.AddItem(journal);
                MessageBox.Show("Przeszukując biurko, znajdujesz stary dziennik.");
            });
        }
    }
}
    