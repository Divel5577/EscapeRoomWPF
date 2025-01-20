using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Bookshelf : Item
    {
        public bool IsMoved { get; private set; }
        public Item HiddenItem { get; private set; }

        public Bookshelf() : base() { }

        public Bookshelf(int positionX, int positionY, Item hiddenItem)
            : base("Półka", "Wygląda, jakby można ją było przesunąć.", false, positionX, positionY, "Assets/Images/bookshelf.png")
        {
            HiddenItem = hiddenItem;
            IsMoved = false;
            InitializeInteractions();
        }

        public override void InitializeInteractions()
        {
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
    }
}
