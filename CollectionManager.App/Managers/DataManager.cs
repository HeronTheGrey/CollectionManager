using CollectionManager.App.Abstract;
using CollectionManager.App.Concrete;
using CollectionManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CollectionManager.App.Managers
{
    public class DataManager
    {
        public XmlCollectionService xmlCollectionService;
        public XmlItemService xmlItemService;
        public IService<Collection> _collectionService;
        public IService<Item> _itemService;
        private XmlRootAttribute collectionRoot;
        private XmlRootAttribute itemRoot;
        private string appDataPath;

        public DataManager(IService<Collection> collectionService, IService<Item> itemService)
        {

            appDataPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CollectionManager");
            System.IO.Directory.CreateDirectory(appDataPath);

            collectionRoot = new XmlRootAttribute();
            collectionRoot.ElementName = "Collections";
            collectionRoot.IsNullable = true;

            itemRoot = new XmlRootAttribute();
            itemRoot.ElementName = "Items";
            itemRoot.IsNullable = true;

            xmlCollectionService = new XmlCollectionService(appDataPath + "\\collections.xml", collectionRoot);
            xmlItemService = new XmlItemService(appDataPath + "\\items.xml", itemRoot);

            _collectionService = collectionService;
            _itemService = itemService;
        }

        public void SaveAllData()
        {
            xmlCollectionService.saveItemsToFile(_collectionService.Items);
            xmlItemService.saveItemsToFile(_itemService.Items);
        }

        public void DeleteAllData()
        {
            _collectionService.Items.Clear();
            _itemService.Items.Clear();
            xmlCollectionService.DeleteAllData();
            xmlItemService.DeleteAllData();
        }

        public void CreateBackup()
        {
            DateTime time = DateTime.Now;
            string timeString = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}",time);
            XmlCollectionService collectionBackup = new XmlCollectionService(appDataPath + "\\collectionBACKUP" + timeString + ".xml", collectionRoot);
            XmlItemService itemBackup = new XmlItemService(appDataPath + "\\itemBACKUP" + timeString + ".xml", itemRoot);
            collectionBackup.saveItemsToFile(_collectionService.Items);
            itemBackup.saveItemsToFile(_itemService.Items);
        }

        public void LoadBackup()
        {
            string[] dates = Directory.GetFiles(appDataPath, "collectionBACKUP*");
            if (dates.Length <= 0)
            {
                Console.WriteLine("There is no backup file.");
            }
            else
            {
                int i = 1;
                foreach (var date in dates)
                {
                    Console.WriteLine($"{i}. { date.Substring(date.Length - 26, 22)}");
                    i++;
                }
                Console.WriteLine("Pick one date from above.");
                var choice = Console.ReadLine();
                int choiceInt;
                int.TryParse(choice, out choiceInt);
                if(choiceInt > 0 && choiceInt < i)
                {
                    XmlCollectionService collectionBackup = new XmlCollectionService(dates[choiceInt-1], collectionRoot);
                    XmlItemService itemBackup = new XmlItemService(dates[choiceInt-1].Substring(0,dates[choiceInt-1].Length-42) + "itemBACKUP" + dates[choiceInt-1].Substring(dates[choiceInt-1].Length - 26), itemRoot);

                    _collectionService.Items = collectionBackup.readItemsFromFile();
                    _itemService.Items = itemBackup.readItemsFromFile();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You picked wrong option.");
                }
                Console.Clear();
            }
        }

        public void ReadAllData()
        {
            if (File.Exists(appDataPath + "\\collections.xml") && File.Exists(appDataPath + "\\items.xml"))
            {
                _collectionService.Items = xmlCollectionService.readItemsFromFile();
                _itemService.Items = xmlItemService.readItemsFromFile();
            }
        }
    }
}
