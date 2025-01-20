using Microsoft.VisualStudio.TestTools.UnitTesting;
using EscapeRoomWPF.Models.Items;
using EscapeRoomWPF.Models;

namespace EscapeRoomWPF_Tests
{
    [TestClass]
    public class DeskTests
    {
        [TestMethod]
        public void Desk_Should_Not_Be_Searched_Initially()
        {
            var desk = new Desk(2, 3);
            Assert.IsFalse(desk.IsSearched);
        }

        [TestMethod]
        public void Desk_Should_Be_Searched_After_Interaction()
        {
            var desk = new Desk(2, 3);
            var inventory = new Inventory();

            desk.InitializeInteractions();
            desk.OnInteract("Przeszukaj", inventory);

            Assert.IsTrue(desk.IsSearched);
        }

        [TestMethod]
        public void Desk_Should_Add_Journal_To_Inventory_When_Searched()
        {
            var desk = new Desk(2, 3);
            var inventory = new Inventory();

            desk.InitializeInteractions();
            desk.OnInteract("Przeszukaj", inventory);

            Assert.AreEqual(1, inventory.Items.Count);
            Assert.AreEqual("Dziennik", inventory.Items[0].Name);
        }

        [TestMethod]
        public void Desk_Should_Not_Add_Journal_If_Already_Searched()
        {
            var desk = new Desk(2, 3);
            var inventory = new Inventory();

            desk.InitializeInteractions();
            desk.OnInteract("Przeszukaj", inventory);
            desk.OnInteract("Przeszukaj", inventory);

            Assert.AreEqual(1, inventory.Items.Count); // Only one journal added
        }

        [TestMethod]
        public void Desk_Should_Have_Proper_Interactions()
        {
            var desk = new Desk(2, 3);
            desk.InitializeInteractions();

            Assert.IsTrue(desk.Interactions.ContainsKey("Przeszukaj"));
        }
    }
}
