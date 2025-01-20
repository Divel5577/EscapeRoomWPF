using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Windows;

namespace EscapeRoomWPF.Models
{
    public abstract class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCollectible { get; set; }
        public bool IsCollidable { get; set; } = true;
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public string ImagePath { get; set; }

        [JsonIgnore]
        public Dictionary<string, Action<Inventory>> Interactions { get; private set; }

        // Konstruktor bezparametrowy wymagany do deserializacji
        public Item()
        {
            Interactions = new Dictionary<string, Action<Inventory>>();
        }

        // Konstruktor z parametrami
        protected Item(string name, string description, bool isCollectible, int positionX, int positionY, string imagePath)
        {
            Name = name;
            Description = description;
            IsCollectible = isCollectible;
            PositionX = positionX;
            PositionY = positionY;
            ImagePath = imagePath;
            Interactions = new Dictionary<string, Action<Inventory>>();
        }

        // Wirtualna metoda obsługi interakcji
        public virtual void OnInteract(string interaction, Inventory inventory)
        {
            if (Interactions.ContainsKey(interaction))
            {
                Interactions[interaction](inventory); // Wykonaj powiązaną akcję
            }
            else
            {
                MessageBox.Show("Nie można wykonać tej akcji.");
            }
        }
        // Wirtualna metoda do inicjalizacji interakcji
        public virtual void InitializeInteractions()
        {
            // Implementacja do nadpisania w klasach dziedziczących

        }

        // Dodawanie nowej interakcji
        public void AddInteraction(string interactionName, Action<Inventory> interactionAction)
        {
            Interactions[interactionName] = interactionAction;
        }
    }
}
