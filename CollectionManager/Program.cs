using System;
using System.ComponentModel.Design;

namespace CollectionManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Collection Manager.");
            MenuActionService menuActions = new MenuActionService();
            CollectionService collectionService = new CollectionService();
            Initialize(menuActions);
            bool endLoop = false;
            while (!endLoop)
            {
                var mainMenuChoice = menuActions.MainMenuDisplay();
                endLoop = menuActions.MainMenuChoice(mainMenuChoice.KeyChar, collectionService);
            }





        }

        private static void Initialize(MenuActionService menuActions)
        {
            menuActions.AddMenuAction(1, "Display collections", "MainMenu");
            menuActions.AddMenuAction(2, "Options", "MainMenu");
            menuActions.AddMenuAction(3, "Create backup", "MainMenu");
            menuActions.AddMenuAction(4, "Exit", "MainMenu");

            menuActions.AddMenuAction(1, "Display collection", "CollectionMenu");
            menuActions.AddMenuAction(2, "Add new collection", "CollectionMenu");
            menuActions.AddMenuAction(3, "Modify collection", "CollectionMenu");
            menuActions.AddMenuAction(4, "Delete collection", "CollectionMenu");
            menuActions.AddMenuAction(5, "Back to main menu", "CollectionMenu");

            menuActions.AddMenuAction(1, "Add new item", "ItemMenu");
            menuActions.AddMenuAction(2, "Modify item", "ItemMenu");
            menuActions.AddMenuAction(3, "Delete item", "ItemMenu");
            menuActions.AddMenuAction(4, "Back to collections view", "ItemMenu");
        }
    }
}
