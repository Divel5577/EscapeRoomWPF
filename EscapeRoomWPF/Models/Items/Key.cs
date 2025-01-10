using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Key : Item
    {
        public Key() : base() { }

        public Key(int positionX, int positionY)
    : base("Klucz", "Mały mosiężny klucz. Może pasować do zamka.", true, positionX, positionY, "Assets/Images/key.png")
        {
            AddInteraction("Zbierz", inventory =>
            {
                if (!inventory.HasItem("Klucz"))
                {
                    inventory.AddItem(this);
                    MessageBox.Show("Podniosłeś klucz.");
                }
                else
                {
                    MessageBox.Show("Już masz ten klucz.");
                }
            });

            AddInteraction("Oglądaj", inventory =>
            {
                MessageBox.Show("To mały mosiężny klucz. Może pasować do zamka.");
            });
        }


        public override void OnInteract(string interaction, Inventory inventory)
        {
            if (interaction == "Zbierz")
            {
                if (inventory.HasItem("Klucz"))
                {
                }
                else
                {
                    inventory.AddItem(this);
                }
            }
            else if (interaction == "Oglądaj")
            {
            }
            else
            {
            }
        }
    }
}
