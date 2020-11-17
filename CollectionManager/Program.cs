using CollectionManager.App.Abstract;
using CollectionManager.App.Concrete;
using CollectionManager.App.Managers;
using CollectionManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Xml.Serialization;

namespace CollectionManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Collection Manager.");
            MenuActionService actionService = new MenuActionService();
            IService<Collection>  collectionService = new CollectionService();
            IService<Item> itemService = new ItemService();
            CollectionManager.App.Managers.CollectionManager collectionManager = new CollectionManager.App.Managers.CollectionManager(actionService, collectionService, itemService);
            DataManager dataManager = new DataManager(collectionService, itemService);
            OptionManager optionManager = new OptionManager(actionService, dataManager);

            dataManager.ReadAllData();
            
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
                        optionManager.OptionMenuDisplay();
                        break;
                    case '3':
                        endLoop = true;
                        dataManager.SaveAllData();
                        break;
                    default:
                        Console.WriteLine("Wrong action picked.");
                        break;
                }
            }
        }
    }
}
