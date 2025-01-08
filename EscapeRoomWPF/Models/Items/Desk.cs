using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Desk : Item
    {
        public Desk() : base() { }

        public Desk(int positionX, int positionY)
    : base("Biurko", "Stary drewniany stół.", false, positionX, positionY, "Assets/Images/desk.jpg")
        {
            AddInteraction("Przeszukaj", inventory =>
            {
                var journal = new Journal(PositionX, PositionY);
                inventory.AddItem(journal);
                MessageBox.Show("Przeszukując biurko, znajdujesz stary dziennik.");
            });
        }


        public override void OnInteract(string interaction, Inventory inventory)
        {
            if (interaction == "Przeszukaj")
            {
                inventory.AddItem(new Journal(PositionX, PositionY));
            }
            else
            {
                base.OnInteract(interaction, inventory);
            }
        }
    }
}
