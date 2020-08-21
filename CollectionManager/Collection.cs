using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionManager
{
    class Collection
    {
        List<Item> items;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        

        public Collection (int id, string name, string desc)
        {
            Id = id;
            Name = name;
            Description = desc;
            items = new List<Item>();
        }

        public bool ItemMenuChoice(char key)
        {
            Console.Clear();
            switch(key)
            {
                case '1':
                    AddNewItem();
                    return false;
                case '2':
                    ModifyItem();
                    return false;
                case '3':
                    DeleteItem();
                    return false;
                case '4':
                    return true;
                default:
                    Console.WriteLine("Wrong action picked.");
                    return false;
            }

        }

        public void DeleteItem()
        {

            Console.WriteLine("Enter id of item to delete.");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            Console.Clear();

            foreach (var tem in items)
            {
                if (tem.Id == id)
                {
                    items.Remove(tem);
                    Console.Clear();
                    return;
                }
            }

            Console.WriteLine("There is no such id.");

        }

        public void ModifyItem()
        {
            Console.WriteLine("Enter id of item to modify.");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            Item item = null;

            foreach (var tem in items)
            {
                if (tem.Id == id)
                {
                    item = tem;
                    break;
                }
            }

            if (item == null)
            {
                Console.Clear();
                Console.WriteLine("There is no such id.");
            }
            else
            {
                Console.WriteLine("Enter new name.");
                string newName = Console.ReadLine();

                Console.WriteLine("Enter new quantity.");
                int newQuantity;
                int.TryParse(Console.ReadLine(), out newQuantity);

                Console.WriteLine("Enter new description.");
                string newDesc = Console.ReadLine();

                item.Name = newName;
                item.Description = newDesc;
                item.Quantity = newQuantity;

                Console.Clear();

            }
        }

        public void AddNewItem()
        {
            Console.WriteLine("Enter id of new item.");
            int id;
            int.TryParse(Console.ReadLine(), out id);

            foreach (var item in items)
            {
                if (item.Id == id)
                {
                    Console.Clear();
                    Console.WriteLine("That id is already taken.");
                    return;
                }
            }

            Console.WriteLine("Enter name of new item.");
            string name;
            name = Console.ReadLine();

            Console.WriteLine("Enter quantity of new item.");
            int quantity;
            int.TryParse(Console.ReadLine(), out quantity);

            Console.WriteLine("Enter description of new item.");
            string desc;
            desc = Console.ReadLine();

            items.Add(new Item(id, quantity, name, desc));
            Console.Clear();
        }

        public ConsoleKeyInfo ItemMenuDisplay(MenuActionService menuAction)
        {
            var MenuDisplay = menuAction.GetMenuActionsByName("ItemMenu");
            foreach (var tem in MenuDisplay)
            {
                Console.WriteLine($"{tem.Id}. {tem.Name}");
            }

            var pressed = Console.ReadKey();
            return pressed;
        }

        public List<Item> GetListOfItems()
        {
            return items;
        }
    }
}
