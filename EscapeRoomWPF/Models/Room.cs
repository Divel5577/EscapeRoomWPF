using EscapeRoomWPF.Models.Items;
using System.Collections.Generic;
using System.Linq;

public class Room
{
    public List<Item> Items { get; set; }
    public char[,] Map { get; set; }

    public Room(int width, int height)
    {
        Items = new List<Item>();
        Map = new char[width, height];

        // Wypełnienie mapy pustymi znakami
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Map[x, y] = ' '; // Symbol pustego pola
            }
        }
    }

    public void AddItem(Item item)
    {
        if (item != null) Items.Add(item);
    }

    public Item GetExit()
    {
        return Items.FirstOrDefault(item => item is Door && ((Door)item).IsExit);
    }

    public Item GetItemAtPosition(int x, int y)
    {
        return Items.FirstOrDefault(item => item.PositionX == x && item.PositionY == y);
    }
}
