using CollectionManager.App.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CollectionManager.App.Concrete
{
    public class XmlItemService : BaseXmlService<Item> 
    {
        public XmlItemService(string path, XmlRootAttribute root) : base(path, root)
        {

        }
    }
}
