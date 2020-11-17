using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CollectionManager.App.Abstract
{
    interface IXmlService<T>
    {
        string Path { get; }
        XmlSerializer xmlSerializer { get; }
        List<T> readItemsFromFile();
        void saveItemsToFile(List<T> list);
        void DeleteAllData();
    }
}
