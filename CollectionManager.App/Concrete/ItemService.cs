using System;
using System.Collections.Generic;
using System.Text;
using CollectionManager.App.Common;
using CollectionManager.Domain.Entity;

namespace CollectionManager.App.Concrete
{
    public class ItemService : BaseService<Item>
    {
        public List<Item> items;

        public int CollectionId { get; set; }

        public ItemService(int id) : base()
        {
            CollectionId = id;
        }
    }
}
