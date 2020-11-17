using CollectionManager.App.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionManager.App.Helpers
{
    public static class Helpers
    {
        public static void ItemTableDisplay(List<Item> items)
        {
            int maxId = 0;
            int maxName = 0;
            int maxQuantity = 0;

            foreach (var item in items)
            {
                if (maxId < item.Id.ToString().Length) { maxId = item.Id.ToString().Length; }
                if (maxName < item.Name.Length) { maxName = item.Name.Length; }
                if (maxQuantity < item.Quantity.ToString().Length) { maxQuantity = item.Quantity.ToString().Length; }
            }

            Console.WriteLine("List of items:");
            Console.Write("Id");
            for(int i = 0 ; i < maxId + 3 ; i++)
            {
                Console.Write(" ");
            }
            Console.Write("Name");
            for (int i = 0 ; i < (maxName + 1) && i < 26 ; i++)
            {
                Console.Write(" ");
            }
            Console.Write("Quantity");
            for (int i = 0; i < maxQuantity + 1; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine("Description");

            foreach (var item in items)
            {
                
                Console.Write($"{item.Id}.");
                for (int i = item.Id.ToString().Length; i < maxId + 4; i++)
                {
                    Console.Write(" ");
                }
                for(int i = 0; i < item.Name.Length && i < 26 ; i++)
                {
                    Console.Write(item.Name[i]);
                }
                if (item.Name.Length > 26)
                {
                    Console.Write("... ");
                }
                else
                {
                    for (int i = item.Name.Length; i < maxName + 5 && i < 30 ; i++)
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write($"{item.Quantity}");
                for (int i = item.Quantity.ToString().Length; i < maxQuantity + 9; i++)
                {
                    Console.Write(" ");
                }
                for (int i = 0; i < item.Description.Length && i < 65; i++)
                {
                    Console.Write(item.Description[i]);
                }
                if(item.Description.Length > 65)
                {
                    Console.Write("...");
                }
                Console.Write("\n");
            }
        }
    }
}
