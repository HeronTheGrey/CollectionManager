using CollectionManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionManager
{
    public class Item : BaseEntity
    {
        public int CollectionId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Item()
        {

        }

        public Item(int id, int quantity, string name, string desc, int collectionId)
        {
            Id = id;
            Quantity = quantity;
            Name = name;
            Description = desc;
            CollectionId = collectionId;
        }
    }
}
