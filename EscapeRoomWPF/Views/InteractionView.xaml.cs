using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EscapeRoomWPF.Views
{
    public partial class InteractionView : UserControl
    {
        public event Action<string> InteractionSelected;

        public InteractionView()
        {
            InitializeComponent();
        }

        public void UpdateInteractions(List<string> interactions)
        {
            InteractionList.Items.Clear(); // Oczyszczanie listy interakcji
            foreach (var interaction in interactions)
            {
                InteractionList.Items.Add(interaction); // Dodawanie nowych interakcji
            }
        }

        private void OnInteractClick(object sender, RoutedEventArgs e)
        {
            var selectedInteraction = InteractionList.SelectedItem?.ToString();
            if (selectedInteraction != null)
            {
                InteractionSelected?.Invoke(selectedInteraction); // Wywołanie zdarzenia
            }
        }
    }
}
