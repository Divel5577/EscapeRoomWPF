using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Key : Item
    {
        public Key() : base() { }

        public Key(int positionX, int positionY)
            : base("Klucz", "Mały mosiężny klucz. Może pasować do zamka.", true, positionX, positionY, "Assets/Images/key.png")
        {
            InitializeInteractions();
        }

        public override void InitializeInteractions()
        {
            AddInteraction("Oglądaj", inventory =>
            {
                MessageBox.Show("To mały mosiężny klucz. Może pasować do zamka.");
            });
        }
    }
}