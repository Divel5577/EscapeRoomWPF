using Microsoft.VisualStudio.TestTools.UnitTesting;
using EscapeRoomWPF.Models.Items;
using EscapeRoomWPF.Models;
using System.Linq;

namespace EscapeRoomWPF_Tests
{
    [TestClass]
    public class RoomTests
    {
        [TestMethod]
        public void Room_Should_Be_Initialized_With_Empty_Item_List()
        {
            // Arrange
            var room = new Room(10, 10);

            // Act
            var items = room.Items;

            // Assert
            Assert.IsNotNull(items);
            Assert.AreEqual(0, items.Count);
        }

        [TestMethod]
        public void Room_Should_Add_Item_To_List()
        {
            // Arrange
            var room = new Room(10, 10);
            var item = new Bookshelf(1, 1);

            // Act
            room.AddItem(item);

            // Assert
            Assert.AreEqual(1, room.Items.Count);
            Assert.AreEqual(item, room.Items.First());
        }

        [TestMethod]
        public void Room_Should_Return_Item_At_Specified_Position()
        {
            // Arrange
            var room = new Room(10, 10);
            var item = new Bookshelf(2, 3);
            room.AddItem(item);

            // Act
            var foundItem = room.GetItemAtPosition(2, 3);

            // Assert
            Assert.IsNotNull(foundItem);
            Assert.AreEqual(item, foundItem);
        }

        [TestMethod]
        public void Room_Should_Return_Null_If_No_Item_At_Position()
        {
            // Arrange
            var room = new Room(10, 10);

            // Act
            var foundItem = room.GetItemAtPosition(5, 5);

            // Assert
            Assert.IsNull(foundItem);
        }

        [TestMethod]
        public void Room_Should_Allow_Multiple_Items_To_Be_Added()
        {
            // Arrange
            var room = new Room(10, 10);
            var item1 = new Bookshelf(1, 1);
            var item2 = new Desk(2, 2);

            // Act
            room.AddItem(item1);
            room.AddItem(item2);

            // Assert
            Assert.AreEqual(2, room.Items.Count);
            Assert.IsTrue(room.Items.Contains(item1));
            Assert.IsTrue(room.Items.Contains(item2));
        }
    }
}
