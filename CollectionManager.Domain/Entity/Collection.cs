using CollectionManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CollectionManager.Domain.Entity
{
    public class Collection : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Description")]
        public string Description { get; set; }

        public Collection()
        {

        }
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
