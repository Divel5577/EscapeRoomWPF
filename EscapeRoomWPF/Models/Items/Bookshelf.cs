using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Bookshelf : Item
    {
        public bool IsMoved { get; set; }
        public Bookshelf() : base() { }

        public Bookshelf(int positionX, int positionY)
            : base("Półka", "Wygląda, jakby można ją było przesunąć.", false, true, positionX, positionY, "Assets/Images/bookshelf.png")
        {
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

                    if (!inventory.HasItem("Klucz"))
                    {
                        var key = new Key(PositionX, PositionY);
                        MessageBox.Show("Przesunąłeś półkę i znalazłeś ukryty klucz!");
                        inventory.AddItem(key);
                    }
                    else
                    {
                        MessageBox.Show("Przesunąłeś półkę, ale nic nie znalazłeś.");
                    }
                }
                else
                {
                    MessageBox.Show("Półka została już przesunięta.");
                }
            });
        }
    }
}