using CollectionManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;


namespace CollectionManager.Domain.Entity
{
    public class Collection : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        
        public Collection (int id, string name)
        {
            Id = id;
            Name = name;
            Description = "";
        }
        public Collection (int id, string name, string desc)
        {
            Id = id;
            Name = name;
            Description = desc;
        }

    }
        
}
