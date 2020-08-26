using CollectionManager.App.Abstract;
using CollectionManager.App.Concrete;
using CollectionManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionManager.App.Managers
{
    public class CollectionManager
    {
        private readonly MenuActionService _actionService;
        private IService<Collection> _collectionService;
        private List<ItemService> _itemServices;
        public CollectionManager(MenuActionService actionService, IService<Collection> collectionService, List<ItemService> itemService)
        {
            _collectionService = collectionService;
            _actionService = actionService;
            _itemServices = itemService;
        }

        public void DisplayAllCollections()
        {
            if (_collectionService.Items.Count == 0)
            {
                Console.WriteLine("There is no collection yet.");
            }
            else
            {
                Console.WriteLine("List of collections:");
                foreach (var collection in _collectionService.Items)
                {
                    Console.WriteLine($"{collection.Id}. {collection.Name}  |  {collection.Description}");
                }
            }
        }

        public void CollectionMenuDisplay()
        {
            bool endLoop = false;
            while (!endLoop)
            {
                DisplayAllCollections();

                Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");

                var collectionMenu = _actionService.GetMenuActionsByName("CollectionMenu");
                for (int i = 0; i < collectionMenu.Count; i++)
                {
                    Console.WriteLine($"{collectionMenu[i].Id}. {collectionMenu[i].Name}");
                }

                var collectionMenuChoice = Console.ReadKey();

                Console.Clear();

                switch (collectionMenuChoice.KeyChar)
                {
                    case '1':
                        ItemService itemService = SelectionOfCollection();
                        if(itemService != null)
                        {
                            ItemManager itemManager = new ItemManager(_actionService, itemService);
                            itemManager.ItemMenuDisplay();
                        }
                        else
                        {
                            Console.WriteLine("Wrong id chosen.");
                        }
                        break;
                    case '2':
                        AddNewCollection();
                        break;
                    case '3':
                        ModifyCollection();
                        break;
                    case '4':
                        DeleteCollection();
                        break;
                    case '5':
                        CollectionDetailsView();
                        break;
                    case '6':
                        endLoop = true;
                        break;
                    default:
                        Console.WriteLine("Wrong action picked.");
                        break;
                }
            }
        }

        public void CollectionDetailsView()
        {
            Console.WriteLine("Enter id of collection to display");
            int id;
            int.TryParse(Console.ReadLine(), out id);
            Console.Clear();
            foreach (var collection in _collectionService.Items)
            {
                if (collection.Id == id)
                {
                    Console.WriteLine($"Full name: {collection.Name}");
                    Console.WriteLine($"Full description: {collection.Description}");
                    Console.WriteLine($"Created: {collection.CreatedDateTime}");
                    Console.WriteLine($"Last modified: {collection.ModifiedDateTime}\n");

                    Console.WriteLine("Press any key to return to menu.");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
            }
            Console.WriteLine("There is no such id");
        }

        public ItemService SelectionOfCollection()
        {
            Console.WriteLine("Enter id of collection to display");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            ItemService itemService = null;

            Console.Clear();

            foreach (var temItem in _itemServices)
            {
                if (temItem.CollectionId == id)
                {
                    itemService = temItem;
                    break;
                }
            }
            return itemService;
        }

        public void DeleteCollection()
        {
            Console.WriteLine("Enter id of collection to remove.");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            Console.Clear();

            foreach (var tem in _collectionService.Items)
            {
                if (tem.Id == id)
                {
                    _collectionService.RemoveItem(tem);
                   
                    foreach (var temItem in _itemServices)
                    {
                        if (temItem.CollectionId == id)
                        {
                            _itemServices.Remove(temItem);
                            return;
                        }
                    }
                    return;
                }
            }
            Console.WriteLine("There is no such id.");
        }

        public void ModifyCollection()
        {
            Console.WriteLine("Enter id of collection you want to modify");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            Console.WriteLine("Enter new name for collection");
            string newName = Console.ReadLine();

            Console.WriteLine("Enter new description for collection");
            string newDescription = Console.ReadLine();

            Collection collection = new Collection(id, newName, newDescription);

            Console.Clear();
            collection.ModifiedDateTime = DateTime.Now;

            var colMod = _collectionService.UpdateItem(collection);
            if (colMod == null)
            {
                Console.WriteLine("Wrong id");
            }
        }

        public int AddNewCollection()
        {
            Console.WriteLine("Enter name of new collection");
            string name = Console.ReadLine();

            Console.WriteLine("Enter description of new collection");
            string description = Console.ReadLine();

            Console.Clear();

            int id = _collectionService.GetLastId();

            Collection collection = new Collection(id+1, name, description);

            collection.CreatedDateTime = DateTime.Now;
            collection.ModifiedDateTime = DateTime.Now;
            _collectionService.AddItem(collection);
            _itemServices.Add(new ItemService(collection.Id));

            return collection.Id;
        }
    }
}
