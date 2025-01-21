using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Painting : Item
    {
        private bool isKeyholeVisible;

        public Painting() : base() { }

        public Painting(int positionX, int positionY)
            : base("Obraz", "Obraz przedstawiający oko. Wygląda, jakby skrywał coś więcej.", false, true, positionX, positionY, "Assets/Images/painting.png")
        {
            isKeyholeVisible = false;
            InitializeInteractions();
        }

        public override void InitializeInteractions()
        {
            AddInteraction("Oglądaj", inventory =>
            {
                if (isKeyholeVisible)
                {
                    MessageBox.Show("Widzisz otwór na klucz za obrazem.");
                }
                else
                {
                    MessageBox.Show("Obraz przedstawia tajemnicze oko.");
                }
            });

            AddInteraction("Użyj klucza", inventory =>
            {
                if (inventory.HasItem("Klucz"))
                {
                    isKeyholeVisible = true;
                    MessageBox.Show("Użyłeś klucza i odsłoniłeś kod wyryty w drewnie: 1234.");
                }
                else
                {
                    MessageBox.Show("Nie masz klucza, aby coś zrobić.");
                }
            });
        }
    }
}
