using CollectionManager.App.Abstract;
using CollectionManager.App.Concrete;
using CollectionManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CollectionManager.App.Managers
{
    public class CollectionManager
    {
        private readonly MenuActionService _actionService;
        private IService<Collection> _collectionService;
        private IService<Item> _itemService;
        public CollectionManager(MenuActionService actionService, IService<Collection> collectionService, IService<Item> itemService)
        {
            _collectionService = collectionService;
            _actionService = actionService;
            _itemService = itemService;
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
                        int collectionId = SelectionOfCollection();

                        if(IsThereCollectionWithChosenId(collectionId))
                        {
                            ItemManager itemManager = new ItemManager(_actionService, _itemService, collectionId);
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
                        DeleteCollectionView();
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

        public bool IsThereCollectionWithChosenId(int collectionId)
        {
            int items = (from i in _collectionService.Items
                         where i.Id == collectionId
                         select i).Count();
                
            if (items > 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CollectionDetailsView()
        {
            Console.WriteLine("Enter id of collection to display");
            int id;
            int.TryParse(Console.ReadLine(), out id);
            Console.Clear();
            var collection = GetCollectionById(id);
            if (collection != null)
            {
                Console.WriteLine($"Full name: {collection.Name}");
                Console.WriteLine($"Full description: {collection.Description}");
                Console.WriteLine($"Created: {collection.CreatedDateTime}");
                Console.WriteLine($"Last modified: {collection.ModifiedDateTime}\n");
                Console.WriteLine("Press any key to return to menu.");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("There is no such id");
            } 
        }

        public Collection GetCollectionById(int id)
        {
            return _collectionService.GetItemById(id);
        }

        public int SelectionOfCollection()
        {
            Console.WriteLine("Enter id of collection to display");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            Console.Clear();

            return id;
        }

        public void DeleteCollectionView()
        {
            Console.WriteLine("Enter id of collection to remove.");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            Console.Clear();

            DeleteCollectionAndItsItems(id);
        }

        public void DeleteCollectionAndItsItems(int id)
        {
            var collection = GetCollectionById(id);
            if (collection != null)
            {
                _collectionService.RemoveItem(collection);
                foreach(var item in _itemService.Items.ToList())
                {
                    if(item.CollectionId == id)
                    {
                        _itemService.RemoveItem(item);
                    }
                }
            }
            else
            {
                Console.WriteLine("There is no such id.");
            }
        }

        public void ModifyCollection()
        {
            Console.WriteLine("Enter id of collection you want to modify");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            bool idIsCorrect = false;

            foreach (var tem in _collectionService.Items)
            {
                if (tem.Id == id)
                {
                    idIsCorrect = true;
                }
            }
            if (idIsCorrect)
            {
                Console.WriteLine("Enter new name for collection (if you don't want to change certain attribute just push enter)");
                string newName = Console.ReadLine();
                if (newName.Length == 0)
                {
                    newName = _collectionService.GetItemById(id).Name;
                }

                Console.WriteLine("Enter new description for collection");
                string newDescription = Console.ReadLine();
                if (newDescription.Length == 0)
                {
                    newDescription = _collectionService.GetItemById(id).Description;
                }

                Collection collection = new Collection(id, newName, newDescription);

                Console.Clear();
                collection.ModifiedDateTime = DateTime.Now;
                var colMod = _collectionService.UpdateItem(collection);
            }
            else
            {
                Console.Clear();
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

            return collection.Id;
        }
    }
}
