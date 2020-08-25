using CollectionManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionManager.Domain.Entity
{
    public class MenuAction : BaseEntity
    {
        public string Name { get; set; }
        public string Menu { get; set; }

        public MenuAction(int id, string name, string menu)
        {
            Id = id;
            Name = name;
            Menu = menu;
        }
    }
}
