using EscapeRoomWPF.Controllers;
using EscapeRoomWPF.Models.Items;
using EscapeRoomWPF.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EscapeRoomWPF.Helpers;

namespace EscapeRoomWPF_Tests
{
    [TestClass]
    public class GameSaveAndLoadTests
    {
        [TestMethod]
        public void Game_Should_Restore_State_After_Save_And_Load()
        {
            // Arrange
            var player = new Player(1, 1);
            var room = new Room(10, 10);
            var desk = new Desk(2, 2);
            room.AddItem(desk);
            var gameMap = new GameMap(player, room);
            var gameController = new GameController(gameMap, player);

            // Zapisz stan gry
            desk.InitializeInteractions();
            desk.OnInteract("Przeszukaj", player.Inventory);
            var gameState = new GameState
            {
                PlayerPositionX = player.PositionX,
                PlayerPositionY = player.PositionY,
                Inventory = player.Inventory.Items,
                RoomItems = room.Items
            };
            GameSaveLoad.SaveGame(gameState);

            // Act
            var restoredState = GameSaveLoad.LoadGame();

            // Przywróć stan gry
            player.PositionX = restoredState.PlayerPositionX;
            player.PositionY = restoredState.PlayerPositionY;
            player.Inventory.Items.Clear();
            player.Inventory.Items.AddRange(restoredState.Inventory);
            room.Items.Clear();
            room.Items.AddRange(restoredState.RoomItems);

            // Assert
            Assert.AreEqual(1, player.PositionX, "Pozycja X gracza nie została poprawnie przywrócona.");
            Assert.AreEqual(1, player.PositionY, "Pozycja Y gracza nie została poprawnie przywrócona.");
            Assert.IsTrue(player.Inventory.HasItem("Dziennik"), "Ekwipunek gracza nie zawiera oczekiwanego przedmiotu: Dziennik.");
            Assert.IsTrue(desk.IsSearched, "Stan biurka (IsSearched) nie został poprawnie przywrócony.");
        }

    }
}
