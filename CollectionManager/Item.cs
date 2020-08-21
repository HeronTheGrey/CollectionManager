using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionManager
{
    class Item
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Item(int id, int quantity, string name, string desc)
        {
            Id = id;
            Quantity = quantity;
            Name = name;
            Description = desc;
        }
    }
}
