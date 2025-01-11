using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Cobweb : Item
    {
        public Cobweb() : base() { }

        public Cobweb(int positionX, int positionY)
    : base("Pajęczyna", "Gęsta pajęczyna pokrywająca róg pokoju.", false, positionX, positionY, "Assets/Images/cobweb.png")
        {
            AddInteraction("Oglądaj", inventory =>
            {
                MessageBox.Show("Pajęczyna jest pełna kurzu i pająków. Nic tu nie znajdziesz.");
            });
        }


        public override void OnInteract(string interaction, Inventory inventory)
        {
            if (interaction == "Oglądaj")
            {
                MessageBox.Show("Pajęczyna jest pełna kurzu i pająków. Nic tu nie znajdziesz.");
            }
            else
            {
                base.OnInteract(interaction, inventory);
            }
        }

    }
}
