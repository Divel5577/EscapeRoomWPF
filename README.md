Escape Room WPF Game
Welcome to the Escape Room WPF Game, an interactive puzzle-solving adventure where you explore, interact with objects, and escape a mysterious room! This project demonstrates the use of the Model-View-Controller (MVC) architecture to create a modular and visually engaging application with a Graphical User Interface (GUI) powered by Windows Presentation Foundation (WPF).

Features
Interactive Gameplay
Move around the room using arrow keys.
Interact with objects by selecting options from an intuitive GUI menu.
Collect and manage items in your inventory to solve puzzles and progress.
Inventory System
View collected items in a dedicated inventory panel.
Use items from your inventory to interact with objects or solve puzzles.
Game Save & Load
Save your progress to a JSON file and continue later by loading the saved game.
Timed Gameplay
Track the duration of your playthrough and view your total playtime upon escaping the room.
Puzzle Solving
Solve puzzles by interacting with objects like bookshelves, paintings, and desks.
Use items such as keys and journals to uncover secrets and unlock doors.
Extensible Object System
Items like Doors, Keys, and Bookshelves inherit from a base Item class, making it easy to add new item types and interactions.
Dynamic GUI
A visually engaging WPF interface with responsive design.
Tooltip hints for buttons and interactive elements to guide the player.
Panels for interactions, inventory, and player status are dynamically updated during gameplay.
Project Structure
Controllers
Manage core game logic, including player movement, interaction management, and game state updates.

GameController: Handles game state, player actions, and room interactions.

Models
Define the core data structures and game logic.

Player: Tracks the player's position and inventory.
Room: Represents the game room, its items, and layout.
Inventory: Manages collected items and provides methods to add or remove items.
Item: A base class for all interactive objects, with specialized subclasses such as Bookshelf, Desk, and Door.
Views
Provide the user interface for interacting with the game.

MainWindow: The primary game window that renders the room, player position, and GUI panels.
EndScreen: Displays the final playtime and congratulatory message upon escaping.
MainMenu: A menu to start a new game or load a saved game.
Helpers
Utility classes for tasks like serialization and custom JSON converters.

GameSaveLoad: Handles saving and loading game state to/from a JSON file.
ItemJsonConverter: Ensures proper serialization and deserialization of abstract and inherited item types.
How to Play
Start the Game
Launch the application and select "Start New Game" from the main menu.
Use the arrow keys to navigate the room.
Interact with Objects
Click on an object to view its interaction options in the interaction panel.
Select an interaction and click "Execute" to perform the action.
Use Inventory Items
Select an item from your inventory and use it to interact with objects in the room.
Save and Load
Save your game at any time using the "Save Game" button.
Load a previously saved game from the main menu.
Complete the Game
Solve puzzles, unlock the door, and escape the room to win. Your total playtime will be displayed on the end screen.
Setup Instructions
Prerequisites
.NET 6.0 or later
Visual Studio (recommended) or a compatible IDE
Running the Game
Clone the repository:
bash
Kopiuj
Edytuj
git clone https://github.com/Divel5577/EscapeRoomWPF.git
cd EscapeRoomWPF
Open the project in Visual Studio.
Build and run the application.
Adding New Items
To extend the game with new items:

Create a new class inheriting from Item.
Implement specific interactions in the InitializeInteractions method.
Add the new item to the room in MainWindow.xaml.cs.
Example:
csharp
Kopiuj
Edytuj
public class Lamp : Item
{
    public Lamp(int positionX, int positionY)
        : base("Lamp", "A small desk lamp. It doesn't seem to work.", false, positionX, positionY, "Assets/Images/lamp.png")
    {
        InitializeInteractions();
    }

    public override void InitializeInteractions()
    {
        AddInteraction("Inspect", inventory =>
        {
            MessageBox.Show("The lamp is dusty but has no bulb.");
        });
    }
}
Known Issues
Some interactions may not correctly refresh the GUI. Ensure saved games are not manually edited to avoid deserialization errors.
Contributors
