using CollectionManager.App.Common;
using CollectionManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CollectionManager.App.Concrete
{
    public class XmlCollectionService : BaseXmlService<Collection>
    {
        public XmlCollectionService(string path, XmlRootAttribute root) : base(path, root)
        {

        }
    }
}
