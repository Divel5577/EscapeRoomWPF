using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Painting : Item
    {
        private bool isKeyholeVisible;

        public Painting() : base() { }

        public Painting(int positionX, int positionY)
    : base("Obraz", "Obraz przedstawiający oko. Wygląda, jakby skrywał coś więcej.", false, positionX, positionY, "Assets/Images/painting.jpg")
        {
            isKeyholeVisible = false;

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
                    MessageBox.Show("Użyłeś klucza i odsłoniłeś otwór za obrazem.");
                }
                else
                {
                    MessageBox.Show("Nie masz klucza, aby coś zrobić.");
                }
            });
        }


        public override void OnInteract(string interaction, Inventory inventory)
        {
            if (interaction == "Oglądaj")
            {
                if (isKeyholeVisible)
                {
                }
                else
                {
                }
            }
            else if (interaction == "Użyj klucza")
            {
                if (inventory.HasItem("Klucz"))
                {
                    isKeyholeVisible = true;
                }
                else
                {
                }
            }
            else
            {
            }
        }
    }
}
