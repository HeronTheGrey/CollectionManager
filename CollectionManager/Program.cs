using CollectionManager.App.Abstract;
using CollectionManager.App.Concrete;
using CollectionManager.App.Managers;
using CollectionManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace CollectionManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Collection Manager.");
            MenuActionService actionService = new MenuActionService();
            CollectionService collectionService = new CollectionService();
            List<ItemService> itemServices = new List<ItemService>();
            CollectionManager.App.Managers.CollectionManager collectionManager = new CollectionManager.App.Managers.CollectionManager(actionService, collectionService, itemServices);
            

            bool endLoop = false;
            while (!endLoop)
            {
                var mainMenu = actionService.GetMenuActionsByName("MainMenu");
                for (int i = 0; i < mainMenu.Count; i++)
                {
                    Console.WriteLine($"{mainMenu[i].Id}. {mainMenu[i].Name}");
                }

                var mainMenuChoice = Console.ReadKey();

                Console.Clear();

                switch (mainMenuChoice.KeyChar)
                {
                    case '1':
                        collectionManager.CollectionMenuDisplay();
                        break;
                    case '2':
                        Console.WriteLine("Not implemented yet.");
                        break;
                    case '3':
                        Console.WriteLine("Not implemented yet.");
                        break;
                    case '4':
                        endLoop = true;
                        break;
                    default:
                        Console.WriteLine("Wrong action picked.");
                        break;
                }
            }
        }
    }
}
