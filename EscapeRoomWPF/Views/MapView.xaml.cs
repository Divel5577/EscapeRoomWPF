using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using EscapeRoomWPF.Models;

namespace EscapeRoomWPF.Views
{
    public partial class MapView : UserControl
    {
        public MapView()
        {
            InitializeComponent();
        }

        public void Render(Room room, Player player)
        {
            RoomCanvas.Children.Clear();

            // Rysowanie mapy
            for (int y = 0; y < room.Map.GetLength(1); y++)
            {
                for (int x = 0; x < room.Map.GetLength(0); x++)
                {
                    var rect = new Rectangle
                    {
                        Width = 50,
                        Height = 50,
                        Stroke = Brushes.Black,
                        Fill = Brushes.White
                    };

                    if (x == player.PositionX && y == player.PositionY)
                    {
                        rect.Fill = Brushes.Blue; // Pozycja gracza
                    }

                    Canvas.SetLeft(rect, x * 50);
                    Canvas.SetTop(rect, y * 50);
                    RoomCanvas.Children.Add(rect);
                }
            }

            // Dodawanie przedmiotów do mapy
            foreach (var item in room.Items)
            {
                var itemRect = new Rectangle
                {
                    Width = 50,
                    Height = 50,
                    Fill = Brushes.Gray
                };

                Canvas.SetLeft(itemRect, item.PositionX * 50);
                Canvas.SetTop(itemRect, item.PositionY * 50);
                RoomCanvas.Children.Add(itemRect);
            }
        }
    }
}