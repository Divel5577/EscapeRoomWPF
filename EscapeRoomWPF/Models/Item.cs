using System;
using System.Collections.Generic;

namespace EscapeRoomWPF.Models.Items
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

        // Nowe pole: interakcje jako słownik
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
                Console.WriteLine($"Interakcja \"{interaction}\" nie jest dostępna dla {Name}.");
            }
        }

        // Dodawanie nowej interakcji
        public void AddInteraction(string interactionName, Action<Inventory> interactionAction)
        {
            Interactions[interactionName] = interactionAction;
        }
    }
}
