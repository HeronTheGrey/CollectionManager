using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionManager
{
    class MenuActionService
    {
        List<MenuAction> menuActions;

        public MenuActionService()
        {
            menuActions = new List<MenuAction>();
        }

        public void AddMenuAction(int id, string name, string menu)
        {
            menuActions.Add(new MenuAction(id, name, menu));
        }

        public List<MenuAction> GetMenuActionsByName(string name)
        {
            List<MenuAction> list = new List<MenuAction>();
            foreach(var item in menuActions)
            {
                if(item.Menu == name)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public ConsoleKeyInfo MainMenuDisplay()
        {
            var MenuDisplay = GetMenuActionsByName("MainMenu");
            foreach(var tem in MenuDisplay)
            {
                Console.WriteLine($"{tem.Id}. {tem.Name}");
            }

            var pressed = Console.ReadKey();
            return pressed;
        }

        public bool MainMenuChoice(char key, CollectionService collectionService)
        {
            Console.Clear();
            switch (key)
            {
                case '1':
                    bool endLoop = false;
                    while (!endLoop)
                    {
                        collectionService.DisplayAllCollections();
                        Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
                        var collectionChoice = collectionService.CollectionMenuDisplay(this);
                        endLoop = collectionService.CollectionMenuChoice(collectionChoice.KeyChar,this);
                    }
                    return false;
                case '2':
                    Console.WriteLine("Not implemented yet.");
                    return false;
                case '3':
                    Console.WriteLine("Not implemented yet.");
                    return false;
                case '4':
                    return true;
                default:
                    Console.WriteLine("Wrong action picked.");
                    return false;
            }

        }
    }
}
