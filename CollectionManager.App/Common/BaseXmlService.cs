using System;
using System.Collections.Generic;
using System.Text;
using CollectionManager.Domain.Common;
using System.IO;
using System.Xml.Serialization;
using CollectionManager.App.Abstract;

namespace CollectionManager.App.Common
{
    public class BaseXmlService<T> : IXmlService<T> where T : BaseEntity
    {
        public string Path { get; }
        public XmlSerializer xmlSerializer { get; }
        public List<T> readItemsFromFile()
        {
            string xml = File.ReadAllText(Path);
            StringReader sr = new StringReader(xml);
            var xmlItems = (List<T>)xmlSerializer.Deserialize(sr);
            return xmlItems;
        }

        public void saveItemsToFile(List<T> list)
        {
            using (StreamWriter sw = new StreamWriter(Path))
            {
                xmlSerializer.Serialize(sw, list);
            }

        }

        public void DeleteAllData()
        {
            //File.WriteAllText(Path, string.Empty);
            File.Delete(Path);
        }

        public BaseXmlService(string path, XmlRootAttribute root)
        {
            Path = path;
            xmlSerializer = new XmlSerializer(typeof(List<T>), root);
        }
    }
}
