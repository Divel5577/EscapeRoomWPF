using Microsoft.VisualStudio.TestTools.UnitTesting;
using EscapeRoomWPF.Models.Items;
using EscapeRoomWPF.Models;

namespace EscapeRoomWPF_Tests
{
    [TestClass]
    public class InventoryTests
    {
        [TestMethod]
        public void Inventory_Should_Be_Initialized_Empty()
        {
            // Arrange
            var inventory = new Inventory();

            // Act
            var items = inventory.Items;

            // Assert
            Assert.IsNotNull(items);
            Assert.AreEqual(0, items.Count);
        }

        [TestMethod]
        public void Inventory_Should_Add_Item()
        {
            // Arrange
            var inventory = new Inventory();
            var item = new Key(1, 1);

            // Act
            inventory.AddItem(item);

            // Assert
            Assert.AreEqual(1, inventory.Items.Count);
            Assert.AreEqual(item, inventory.Items[0]);
        }

        [TestMethod]
        public void Inventory_Should_Not_Add_Duplicate_Items()
        {
            // Arrange
            var inventory = new Inventory();
            var item = new Key(1, 1);

            // Act
            inventory.AddItem(item);
            inventory.AddItem(item); // Attempt to add the same item again

            // Assert
            Assert.AreEqual(1, inventory.Items.Count); // Only one instance of the item should exist
        }

        [TestMethod]
        public void Inventory_Should_Contain_Item_After_Addition()
        {
            // Arrange
            var inventory = new Inventory();
            var item = new Key(1, 1);

            // Act
            inventory.AddItem(item);
            var containsItem = inventory.HasItem("Klucz");

            // Assert
            Assert.IsTrue(containsItem);
        }

        [TestMethod]
        public void Inventory_Should_Not_Contain_Item_Not_Added()
        {
            // Arrange
            var inventory = new Inventory();

            // Act
            var containsItem = inventory.HasItem("Klucz");

            // Assert
            Assert.IsFalse(containsItem);
        }
    }
}
