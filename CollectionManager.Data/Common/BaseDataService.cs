using CollectionManager.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using CollectionManager.Domain.Common;
using System.IO;
using System.Xml.Serialization;

namespace CollectionManager.Data.Common
{
    public class BaseDataService<T> : IDataService<T> where T : BaseEntity
    {
        private readonly string _path;
        public List<T> readItemsFromFile()
        {
            return null;
        }

        public void saveItemsToFile(List<T> list, XmlRootAttribute root)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>), root);
            using (StreamWriter sw = new StreamWriter(_path))
            {
                xmlSerializer.Serialize(sw, list);
            }

        }
        public BaseDataService(string path)
        {
            _path = path; 
        }
            


    }
}
