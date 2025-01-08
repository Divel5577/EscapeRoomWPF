using EscapeRoomWPF.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace EscapeRoomWPF.Models
{
    public class Inventory
    {
        public List<Item> Items { get; private set; }

        public Inventory()
        {
            Items = new List<Item>();
        }
        public void AddItem(Item item)
        {
            if (!Items.Any(existingItem => existingItem.Name == item.Name))
            {
                Items.Add(item);
                Console.WriteLine($"{item.Name} został dodany do ekwipunku.");
            }
            else
            {
                Console.WriteLine($"{item.Name} już znajduje się w ekwipunku.");
            }
        }
        public Item GetItem(string itemName)
        {
            return Items.Find(item => item.Name == itemName);
        }

        public List<Item> GetItems()
        {
            return Items; 
        }

        public void PerformItemInteraction(Item item, string interaction)
        {
            item.OnInteract(interaction, this);
        }


        public bool HasItem(string itemName)
        {
            return Items.Exists(item => item.Name == itemName);
        }

    }
}
