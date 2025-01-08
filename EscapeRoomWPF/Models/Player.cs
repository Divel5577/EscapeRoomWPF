namespace EscapeRoomWPF.Models
{
    public class Player
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int LastPositionX { get; set; }
        public int LastPositionY { get; set; }
        public Inventory Inventory { get; set; }
        public Player()
        {
            Inventory = new Inventory();
        }
        public Player(int startX, int startY) : this()
        {
            PositionX = startX;
            PositionY = startY;
            LastPositionX = startX;
            LastPositionY = startY;
        }

        public void Move(int deltaX, int deltaY)
        {
            LastPositionX = PositionX;
            LastPositionY = PositionY;
            PositionX += deltaX;
            PositionY += deltaY;
        }

        public bool CanMove(int newX, int newY, int mapWidth, int mapHeight)
        {
            return newX >= 0 && newX < mapWidth && newY >= 0 && newY < mapHeight;
        }

    }


}
