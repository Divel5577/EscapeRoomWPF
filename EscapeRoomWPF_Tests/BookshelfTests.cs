using Microsoft.VisualStudio.TestTools.UnitTesting;
using EscapeRoomWPF.Models.Items;
using EscapeRoomWPF.Models;

namespace EscapeRoomWPF_Tests
{
    [TestClass]
    public class BookshelfTests
    {
        [TestMethod]
        public void Bookshelf_Should_Not_Be_Moved_Initially()
        {
            var bookshelf = new Bookshelf(1, 1);
            Assert.IsFalse(bookshelf.IsMoved);
        }

        [TestMethod]
        public void Bookshelf_Should_Be_Moved_After_Interaction()
        {
            var bookshelf = new Bookshelf(1, 1);
            var inventory = new Inventory();

            bookshelf.InitializeInteractions();
            bookshelf.OnInteract("Przesuń", inventory);

            Assert.IsTrue(bookshelf.IsMoved);
        }

        [TestMethod]
        public void Bookshelf_Should_Not_Be_Moved_Twice()
        {
            var bookshelf = new Bookshelf(1, 1);
            var inventory = new Inventory();

            bookshelf.InitializeInteractions();
            bookshelf.OnInteract("Przesuń", inventory);
            bookshelf.OnInteract("Przesuń", inventory);

            Assert.IsTrue(bookshelf.IsMoved);
        }

        [TestMethod]
        public void Bookshelf_Should_Have_Proper_Interactions()
        {
            var bookshelf = new Bookshelf(1, 1);
            bookshelf.InitializeInteractions();

            Assert.IsTrue(bookshelf.Interactions.ContainsKey("Przesuń"));
        }

        [TestMethod]
        public void Bookshelf_Should_Not_Add_Item_To_Inventory_If_Already_Moved()
        {
            var bookshelf = new Bookshelf(1, 1);
            var inventory = new Inventory();

            bookshelf.InitializeInteractions();
            bookshelf.OnInteract("Przesuń", inventory); // First interaction
            bookshelf.OnInteract("Przesuń", inventory); // Second interaction

            Assert.AreEqual(1, inventory.Items.Count, "Przedmiot został dodany więcej niż raz.");
            Assert.IsTrue(bookshelf.IsMoved, "Stan IsMoved powinien być true.");
        }
    }
}
