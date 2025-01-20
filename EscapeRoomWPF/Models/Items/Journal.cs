using System.Windows;

namespace EscapeRoomWPF.Models.Items
{
    public class Journal : Item
    {
        public string Content { get; private set; }

        public Journal() : base() { }

        public Journal(int positionX, int positionY)
            : base("Dziennik", "Stary dziennik z pożółkłymi kartkami, leżący na biurku.", true, positionX, positionY, "Assets/Images/journal.png")
        {
            Content = "Dziennik jest pełen wpisów o tajemniczych wydarzeniach...\n" +
                      "Data: 23 października\n" +
                      "Odkryłem coś niepokojącego za półką w bibliotece...";
            InitializeInteractions();
        }

        public override void InitializeInteractions()
        {
            AddInteraction("Przeczytaj", inventory =>
            {
                MessageBox.Show(Content);
            });
        }
    }
}
