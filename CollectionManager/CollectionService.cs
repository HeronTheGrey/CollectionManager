using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CollectionManager
{
    class CollectionService
    {
        List<Collection> collections;

        public CollectionService()
        {
            collections = new List<Collection>();
        }

        public bool CollectionMenuChoice(char key, MenuActionService menuAction)
        {
            Console.Clear();
            switch(key)
            {
                case '1':
                    bool endLoop = false;
                    
                    Console.WriteLine("Enter id of collection to display.");
                    int id;
                    int.TryParse(Console.ReadLine(), out id);

                    Console.Clear();

                    while (!endLoop)
                    {
                        Collection collection;
                        if ((collection = DisplayCollection(id)) == null) { break; }
                        Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
                        var option = collection.ItemMenuDisplay(menuAction);
                        endLoop = collection.ItemMenuChoice(option.KeyChar);
                    }
                    return false;
                case '2':
                    AddNewCollection();
                    return false;
                case '3':
                    ModifyCollection();
                    return false;
                case '4':
                    DeleteCollection();
                    return false;
                case '5':
                    return true;
                default:
                    Console.WriteLine("Wrong action picked.");
                    return false;
            }
        }

        public Collection DisplayCollection(int id)
        {
            Collection collection = null;

            foreach (var tem in collections)
            {
                if (tem.Id == id)
                {
                    collection = tem;
                    break;
                }
            }

            if (collection == null)
            {
                Console.WriteLine("There is no such id.");
                return null;
            }
            else
            {
                Console.WriteLine("Id             Quantity       Name           Description");
                foreach(var item in collection.GetListOfItems())
                {
                    Console.Write(item.Id);
                    for(int i = 15; i > item.Id.ToString().Length; i--)
                    { 
                        Console.Write(" ");
                    }
                    Console.Write(item.Quantity);
                    for (int i = 15; i > item.Quantity.ToString().Length; i--)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(item.Name);
                    for (int i = 15; i > item.Name.Length; i--)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(item.Description);
                    Console.WriteLine();
                }
                return collection;
            }


        }

        public void ModifyCollection()
        {
            Console.WriteLine("Enter id of collection to modify.");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            Collection collection = null;
            
            foreach (var tem in collections)
            {
                if (tem.Id == id)
                {
                    collection = tem;
                    break;
                }
            }

            if(collection == null)
            {
                Console.Clear();
                Console.WriteLine("There is no such id.");
            }
            else
            {
                Console.WriteLine("Enter new name.");
                string newName = Console.ReadLine();

                Console.WriteLine("Enter new description.");
                string newDesc = Console.ReadLine();

                collection.Name = newName;
                collection.Description = newDesc;

                Console.Clear();

            }
        }

        public void DeleteCollection()
        {
            Console.WriteLine("Enter id of collection to delete.");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            Console.Clear();

            foreach(var tem in collections)
            {
                if (tem.Id == id)
                {
                    collections.Remove(tem);
                    Console.Clear();
                    break;
                }
            }

            Console.WriteLine("There is no such id.");
        }

        public void AddNewCollection()
        {
            Console.WriteLine("Enter id of new collection.");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            foreach(var tem in collections)
            {
                if(tem.Id == id)
                {
                    Console.Clear();
                    Console.WriteLine("That id is already taken.");
                    return;
                }
            }

            Console.WriteLine("Enter name of new collection.");
            string name;
            name = Console.ReadLine();

            Console.WriteLine("Enter description of new collection.");
            string desc;
            desc = Console.ReadLine();

            collections.Add(new Collection(id, name, desc));
            Console.Clear();
        }

        public ConsoleKeyInfo CollectionMenuDisplay(MenuActionService menuAction)
        {
            var MenuDisplay = menuAction.GetMenuActionsByName("CollectionMenu");
            foreach (var tem in MenuDisplay)
            {
                Console.WriteLine($"{tem.Id}. {tem.Name}");
            }

            var pressed = Console.ReadKey();
            return pressed;
        }

        public void DisplayAllCollections()
        {
            if (collections.Count == 0)
            {
                Console.WriteLine("There is no collection yet.");
            }
            else
            {
                Console.WriteLine("List of collections:");
                foreach (var collection in collections)
                {
                    Console.WriteLine($"{collection.Id}. {collection.Name}  |  {collection.Description}");
                }
            }
        }
    }
}
