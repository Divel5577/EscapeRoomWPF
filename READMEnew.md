Escape Room WPF Game - Key Points
General Description
Escape Room WPF Game is an interactive puzzle-solving adventure featuring a mysterious room.
Built with Model-View-Controller (MVC) architecture using Windows Presentation Foundation (WPF) for GUI.
Features
Interactive Gameplay
Navigate the room with arrow keys.
Use an intuitive GUI menu for object interactions.
Collect and manage items to solve puzzles.
Inventory System
View and manage collected items in a dedicated inventory panel.
Use inventory items to interact with objects or solve puzzles.
Game Save & Load
Save your game progress in a JSON file.
Resume gameplay from a saved state.
Timed Gameplay
Track playtime duration and display total time upon escaping.
Puzzle Solving
Interact with objects like bookshelves, paintings, and desks.
Use tools like keys and journals to unlock doors or uncover secrets.
Extensible Object System
Items such as doors, keys, and bookshelves inherit from a base Item class.
Easily add new item types and interactions.
Dynamic GUI
A responsive, visually engaging WPF interface.
Tooltip hints guide players.
Interactive panels update dynamically during gameplay.
Project Structure
Controllers
Handle game logic, player movement, and state updates.
GameController: Manages state, player actions, and room interactions.
Models
Define data structures and logic:
Player: Tracks position and inventory.
Room: Represents the game layout and objects.
Inventory: Manages items and provides interaction methods.
Item: Base class for interactive objects like bookshelves and doors.
Views
Provide the user interface:
MainWindow: Main game window showing the map, player position, and panels.
EndScreen: Displays playtime and congratulatory messages.
MainMenu: Start new games or load saved ones.
Helpers
Utility classes for serialization and custom JSON conversions:
GameSaveLoad: Save/load game state.
ItemJsonConverter: Handles serialization of abstract and inherited items.
How to Play
Start the Game:

Launch the application and select "Start New Game."
Navigate the room using arrow keys.
Interact with Objects:

Click objects to view interaction options.
Select and execute interactions via the interaction panel.
Use Inventory Items:

Select an item from the inventory to interact with objects.
Save and Load:

Save progress via the "Save Game" button.
Resume from the main menu.
Complete the Game:

Solve puzzles, unlock doors, and escape the room.
View your total playtime on the end screen.
Setup Instructions
Prerequisites
.NET 6.0 or later.
Visual Studio (recommended) or a compatible IDE.


Running the Game
Clone the repository:
git clone https://github.com/Divel5577/EscapeRoomWPF.git
cd EscapeRoomWPF
Open the project in Visual Studio.
Build and run the application. (preferably in debug mode)


Extending the Game
Adding New Items
Create a new class inheriting from Item.
Implement interactions in the InitializeInteractions method.
Add the item to the room in MainWindow.xaml.cs.

Contributors:
Gracjan Czyżewski
Patryk Dulkowski


