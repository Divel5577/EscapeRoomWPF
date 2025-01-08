using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Bookshelf : Item
    {
        public bool IsMoved { get; private set; }
        public Item HiddenItem { get; private set; }

        public Bookshelf() : base() { }

        public Bookshelf(int positionX, int positionY, Item hiddenItem)
    : base("Półka", "Wygląda, jakby można ją było przesunąć.", false, positionX, positionY, "Assets/Images/bookshelf.jpg")
        {
            HiddenItem = hiddenItem;
            IsMoved = false;

            AddInteraction("Przesuń", inventory =>
            {
                if (!IsMoved)
                {
                    IsMoved = true;
                    inventory.AddItem(HiddenItem);
                    MessageBox.Show("Przesunąłeś półkę i znalazłeś ukryty przedmiot!");
                }
                else
                {
                    MessageBox.Show("Półka została już przesunięta.");
                }
            });
        }

        public override void OnInteract(string interaction, Inventory inventory)
        {
            if (interaction == "Przesuń" && !IsMoved)
            {
                IsMoved = true;
                inventory.AddItem(HiddenItem);
            }
            else if (interaction == "Przesuń" && IsMoved)
            {
            }
            else
            {
                base.OnInteract(interaction, inventory);
            }
        }
    }
}
