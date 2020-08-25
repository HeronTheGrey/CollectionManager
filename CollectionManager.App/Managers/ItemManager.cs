using CollectionManager.App.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using CollectionManager.App.Helpers;

namespace CollectionManager.App.Managers
{
    class ItemManager
    {
        private readonly MenuActionService _actionService;
        ItemService _itemService;

        public ItemManager(MenuActionService actionService, ItemService itemService)
        {
            _actionService = actionService;
            _itemService = itemService;
        }

        public void DisplayAllItems()
        {
            if (_itemService.Items.Count == 0)
            {
                Console.WriteLine("There is no item yet.");
            }
            else
            {
                Helpers.Helpers.ItemTableDisplay(_itemService);
            }
        }

        public void ItemMenuDisplay()
        {
            bool endLoop = false;
            while (!endLoop)
            {
                DisplayAllItems();

                Console.WriteLine(@"/*\*/*\*/*\*/*\/*\/*\*/*\*/*\*/*\*/*\/*\*/*\/*\*/*\*/*\*/*\/*\/*\*/*\*/*\*/*\*/*\/*\*/*\");

                var ItemMenu = _actionService.GetMenuActionsByName("ItemMenu");
                for (int i = 0; i < ItemMenu.Count; i++)
                {
                    Console.WriteLine($"{ItemMenu[i].Id}. {ItemMenu[i].Name}");
                }

                var collectionMenuChoice = Console.ReadKey();

                Console.Clear();

                switch (collectionMenuChoice.KeyChar)
                {
                    case '1':
                        AddNewItem();
                        break;
                    case '2':
                        ModifyItem();
                        break;
                    case '3':
                        DeleteItem();
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

        public void DeleteItem()
        {
            Console.WriteLine("Enter id of item to remove.");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            Console.Clear();

            foreach (var tem in _itemService.Items)
            {
                if (tem.Id == id)
                {
                    _itemService.RemoveItem(tem);
                    return;
                }
            }
            Console.WriteLine("There is no such id.");
        }

        public int AddNewItem()
        {
            Console.WriteLine("Enter name of new item");
            string name = Console.ReadLine();

            Console.WriteLine("Enter description of new item");
            string description = Console.ReadLine();

            Console.WriteLine("Enter quantity of new item");
            int quantity;
            int.TryParse(Console.ReadLine(), out quantity);

            Console.Clear();

            int id = _itemService.GetLastId();

            Item item = new Item(id+1, quantity, name, description);

            _itemService.AddItem(item);
            
            return item.Id;
        }

        public void ModifyItem()
        {
            Console.WriteLine("Enter id of item you want to modify");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            Console.WriteLine("Enter new name for item");
            string newName = Console.ReadLine();

            Console.WriteLine("Enter new description for item");
            string newDescription = Console.ReadLine();

            Console.WriteLine("Enter new quantity for item");
            int newQuantity;
            int.TryParse(Console.ReadLine(), out newQuantity);

            Item item = new Item(id, newQuantity, newName, newDescription);

            Console.Clear();
            var itemMod = _itemService.UpdateItem(item);
            if (itemMod == null)
            {
                Console.WriteLine("Wrong id");
            }
        }
    }
}
