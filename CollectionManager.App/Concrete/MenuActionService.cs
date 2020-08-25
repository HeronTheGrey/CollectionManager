using CollectionManager.App.Common;
using CollectionManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionManager.App.Concrete
{
    public class MenuActionService : BaseService<MenuAction>
    {
        public MenuActionService()
        {
            Initialize();
        }


        public List<MenuAction> GetMenuActionsByName(string name)
        {
            List<MenuAction> list = new List<MenuAction>();
            foreach(var item in Items)
            {
                if(item.Menu == name)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        private void Initialize()
        {
            AddItem(new MenuAction(1, "Display collections", "MainMenu"));
            AddItem(new MenuAction(2, "Options", "MainMenu"));
            AddItem(new MenuAction(3, "Create backup", "MainMenu"));
            AddItem(new MenuAction(4, "Exit", "MainMenu"));

            AddItem(new MenuAction(1, "Display collection", "CollectionMenu"));
            AddItem(new MenuAction(2, "Add new collection", "CollectionMenu"));
            AddItem(new MenuAction(3, "Modify collection", "CollectionMenu"));
            AddItem(new MenuAction(4, "Delete collection", "CollectionMenu"));
            AddItem(new MenuAction(5, "Back to main menu", "CollectionMenu"));

            AddItem(new MenuAction(1, "Add new item", "ItemMenu"));
            AddItem(new MenuAction(2, "Modify item", "ItemMenu"));
            AddItem(new MenuAction(3, "Delete item", "ItemMenu"));
            AddItem(new MenuAction(4, "Back to collections view", "ItemMenu"));
        }
    }
}
