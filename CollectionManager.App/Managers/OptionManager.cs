using CollectionManager.App.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CollectionManager.App.Managers
{
    public class OptionManager
    {
        private readonly MenuActionService _actionService;
        private DataManager _dataManager;

        public OptionManager(MenuActionService actionService, DataManager dataManager)
        {
            _actionService = actionService;
            _dataManager = dataManager;
        }

        public void OptionMenuDisplay()
        {
            bool endLoop = false;
            while (!endLoop)
            {
                var optionMenu = _actionService.GetMenuActionsByName("OptionMenu");

                for (int i = 0; i < optionMenu.Count; i++)
                {
                    Console.WriteLine($"{optionMenu[i].Id}. {optionMenu[i].Name}");
                }

                var optionMenuChoice = Console.ReadKey();

                Console.Clear();

                switch (optionMenuChoice.KeyChar)
                {
                    case '1':
                        Console.WriteLine("Are you sure you want to delete all of your data? (y/n)");
                        var choice = Console.ReadKey().KeyChar;
                        if(choice == 'Y' || choice == 'y')
                        {
                            _dataManager.DeleteAllData();
                            Console.Clear();
                            Console.WriteLine("All data deleted.\n");
                        }
                        Console.Clear();
                        break;
                    case '2':
                        _dataManager.CreateBackup();
                        Console.Clear();
                        break;
                    case '3':
                        _dataManager.LoadBackup();
                        break;
                    case '4':
                        endLoop = true;
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Wrong action picked.");
                        break;
                }
            }
        }






    }
}
