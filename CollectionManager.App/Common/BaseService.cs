using CollectionManager.App.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using CollectionManager.Domain.Common;
using System.Linq;

namespace CollectionManager.App.Common
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        public List<T> Items { get; set; }

        public BaseService()
        {
            Items = new List<T>();
        }

        public T GetItemById(int id)
        {
            T result = null;
            foreach (var item in Items)
            {
                if(item.Id == id)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        public int GetLastId()
        {
            int lastId;
            if (Items.Any())
            {
                lastId = Items.OrderBy(p => p.Id).LastOrDefault().Id;
            }
            else
            {
                lastId = 0;
            }
            return lastId;
        }
        public int AddItem(T item)
        {
            Items.Add(item);
            return item.Id;
        }

        public List<T> GetAllItems()
        {
            return Items;
        }

        public void RemoveItem(T item)
        {
            Items.Remove(item);
        }

        public int? UpdateItem(T item)
        {
            var index = Items.FindIndex(p => p.Id == item.Id);
            if (index != -1)
            {
                item.CreatedDateTime = Items[index].CreatedDateTime;
                item.CreatedById = Items[index].CreatedById;
                Items[index] = item;
                return Items[index].Id;
            }
            else
            {
                return null;
            }
        }
    }
}
