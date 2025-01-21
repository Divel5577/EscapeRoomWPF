using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Chandelier : Item
    {
        public Chandelier() : base() { }

        public Chandelier(int positionX, int positionY)
            : base("Żyrandol", "Stary, zakurzony żyrandol zwisający z sufitu.", false, false, positionX, positionY, "Assets/Images/chandelier.png")
        {
            InitializeInteractions();
        }

        public override void InitializeInteractions()
        {
            AddInteraction("Oglądaj", inventory =>
            {
                MessageBox.Show("Żyrandol jest stary i zakurzony. Nic tu nie znajdziesz.");
            });
        }
    }
}
