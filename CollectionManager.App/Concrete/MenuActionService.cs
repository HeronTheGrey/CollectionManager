﻿using CollectionManager.App.Common;
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
            AddItem(new MenuAction(3, "Exit", "MainMenu"));

            AddItem(new MenuAction(1, "Display collection", "CollectionMenu"));
            AddItem(new MenuAction(2, "Add new collection", "CollectionMenu"));
            AddItem(new MenuAction(3, "Modify collection", "CollectionMenu"));
            AddItem(new MenuAction(4, "Delete collection", "CollectionMenu"));
            AddItem(new MenuAction(5, "View collection detail", "CollectionMenu"));
            AddItem(new MenuAction(6, "Back to main menu", "CollectionMenu"));

            AddItem(new MenuAction(1, "Add new item", "ItemMenu"));
            AddItem(new MenuAction(2, "Modify item", "ItemMenu"));
            AddItem(new MenuAction(3, "Delete item", "ItemMenu"));
            AddItem(new MenuAction(4, "View item detail", "ItemMenu"));
            AddItem(new MenuAction(5, "Back to collections view", "ItemMenu"));

            AddItem(new MenuAction(1, "Erase all data", "OptionMenu"));
            AddItem(new MenuAction(2, "Create backup", "OptionMenu"));
            AddItem(new MenuAction(3, "Load backup", "OptionMenu"));
            AddItem(new MenuAction(4, "Back to main menu", "OptionMenu"));
        }
    }
}
