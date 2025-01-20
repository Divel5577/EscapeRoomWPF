using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Bookshelf : Item
    {
        public bool IsMoved { get; set; }
        public Bookshelf() : base() { }

        public Bookshelf(int positionX, int positionY)
            : base("Półka", "Wygląda, jakby można ją było przesunąć.", false, positionX, positionY, "Assets/Images/bookshelf.png")
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
                    var key = new Key(PositionX, PositionY);
                    inventory.AddItem(key);
                    MessageBox.Show("Przesunąłeś półkę i znalazłeś klucz.");
                }
                else
                {
                    MessageBox.Show("Półka została już przesunięta.");
                }
            });
        }
    }
}