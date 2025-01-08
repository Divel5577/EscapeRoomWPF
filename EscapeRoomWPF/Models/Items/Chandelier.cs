using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Chandelier : Item
    {
        public Chandelier() : base() { }

        public Chandelier(int positionX, int positionY)
    : base("Żyrandol", "Stary, zakurzony żyrandol zwisający z sufitu.", false, positionX, positionY, "Assets/Images/chandelier.jpg")
        {
            AddInteraction("Oglądaj", inventory =>
            {
                MessageBox.Show("Żyrandol jest stary i zakurzony. Nic tu nie znajdziesz.");
            });
        }


        public override void OnInteract(string interaction, Inventory inventory)
        {
            if (interaction == "Oglądaj")
            {
            }
            else
            {
                base.OnInteract(interaction, inventory);
            }
        }
    }
}
