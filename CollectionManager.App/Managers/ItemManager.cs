using CollectionManager.App.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using CollectionManager.App.Helpers;
using CollectionManager.App.Abstract;
using System.Linq;

namespace CollectionManager.App.Managers
{
    public class ItemManager
    {
        private readonly MenuActionService _actionService;
        IService<Item> _itemService;
        int _collectionId;

        public ItemManager(MenuActionService actionService, IService<Item> itemService, int collectionId)
        {
            _actionService = actionService;
            _itemService = itemService;
            _collectionId = collectionId;
        }

        public void DisplayAllItems()
        {
            if (_itemService.Items.Count == 0)
            {
                Console.WriteLine("There is no item yet.");
            }
            else
            {
                var items = GetListOfItemsByCollectionId(_collectionId);
                Helpers.Helpers.ItemTableDisplay(items);
            }
        }

        public List<Item> GetListOfItemsByCollectionId(int collectionId)
        {
            var items = (from i in _itemService.Items
                         where i.CollectionId == collectionId
                         select i).ToList();
            return items;
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
                        DeleteItemView();
                        break;
                    case '4':
                        ItemDetailsView();
                        break;
                    case '5':
                        endLoop = true;
                        break;
                    default:
                        Console.WriteLine("Wrong action picked.");
                        break;
                }
            }
        }

        public void ItemDetailsView()
        {
            Console.WriteLine("Enter id of item to display");
            int id;
            int.TryParse(Console.ReadLine(), out id);
            Console.Clear();
            var item = GetItemById(id);
            if (item != null)
            {
                Console.WriteLine($"Full name: {item.Name}");
                Console.WriteLine($"Full description: {item.Description}");
                Console.WriteLine($"Created: {item.CreatedDateTime}");
                Console.WriteLine($"Last modified: {item.ModifiedDateTime}\n");

                Console.WriteLine("Press any key to return to menu.");
                Console.ReadKey();
                Console.Clear();
             
            }
            else
            {
                Console.WriteLine("There is no such id");
            }
        }

        public Item GetItemById(int id)
        {
            Item result = null;
            foreach (var item in _itemService.Items)
            {
                if(item.Id == id)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        public void DeleteItemView()
        {
            Console.WriteLine("Enter id of item to remove.");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            Console.Clear();

            foreach (var tem in _itemService.Items)
            {
                if (tem.Id == id)
                {
                    if (tem.CollectionId != _collectionId)
                    {
                        Console.WriteLine("Are you sure you want to delete item from different collection? (y/n)");
                        char read = 'A';
                        do
                        {
                            read = Console.ReadKey().KeyChar;
                        } while (read != 'Y' && read != 'y' && read != 'N' && read != 'n');
                        Console.Clear();
                        if( read == 'n' || read == 'N') { return; }
                    }
                    _itemService.RemoveItem(tem);
                    return;
                }
            }
            Console.WriteLine("There is no such id.");
        }

        public void DeleteItem(int id)
        {
            var itemToDelete = GetItemById(id);
            _itemService.RemoveItem(itemToDelete);
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

            Item item = new Item(id+1, quantity, name, description, _collectionId);
            item.CreatedDateTime = DateTime.Now;
            item.ModifiedDateTime = DateTime.Now;

            _itemService.AddItem(item);
            
            return item.Id;
        }

        public void ModifyItem()
        {
            Console.WriteLine("Enter id of item you want to modify");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            bool idIsCorrect = false;

            int tempCollectionId = _collectionId;

            foreach (var tem in _itemService.Items)
            {
                if (tem.Id == id)
                {
                    if (tem.CollectionId != _collectionId)
                    {
                        tempCollectionId = tem.CollectionId;
                        Console.WriteLine("Are you sure you want to modify item from different collection? (y/n)");
                        char read = 'A';
                        do
                        {
                            read = Console.ReadKey().KeyChar;
                        } while (read != 'Y' && read != 'y' && read != 'N' && read != 'n');
                        Console.Clear();
                        if (read == 'n' || read == 'N') { return; }
                    }

                    idIsCorrect = true;
                }
            }

            if (idIsCorrect)
            {
                Console.WriteLine("Enter new name for item (if you don't want to change certain attribute just push enter)");
                string newName = Console.ReadLine();
                if(newName.Length == 0)
                {
                    newName = _itemService.GetItemById(id).Name;
                }

                Console.WriteLine("Enter new description for item");
                string newDescription = Console.ReadLine();
                if (newDescription.Length == 0)
                {
                    newDescription = _itemService.GetItemById(id).Description;
                }

                Console.WriteLine("Enter new quantity for item");
                int newQuantity;
                string tempString = Console.ReadLine();
                if (tempString.Length == 0)
                {
                    newQuantity = _itemService.GetItemById(id).Quantity;
                }
                else
                {
                    int.TryParse(tempString, out newQuantity);
                }

                Item item = new Item(id, newQuantity, newName, newDescription, tempCollectionId);
                item.ModifiedDateTime = DateTime.Now;

                Console.Clear();

                var itemMod = _itemService.UpdateItem(item);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Wrong id");
            }

        }
    }
}
