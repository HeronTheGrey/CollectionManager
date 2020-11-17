using CollectionManager.App.Concrete;
using CollectionManager.Domain.Entity;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace CollectionManager.Tests
{
    public class MenuActionServiceTest
    {
        [Fact]
        public void GetMenuActionByNameTest()
        {
            MenuActionService menuAction = new MenuActionService();
            menuAction.AddItem(new MenuAction(1, "test1", "One"));
            menuAction.AddItem(new MenuAction(2, "test2", "Two"));
            menuAction.AddItem(new MenuAction(3, "test3", "One"));
            menuAction.AddItem(new MenuAction(4, "test4", "Three"));
            menuAction.AddItem(new MenuAction(5, "test5", "Three"));
            menuAction.AddItem(new MenuAction(6, "test6", "Two"));
            menuAction.AddItem(new MenuAction(7, "test7", "Five"));
            menuAction.AddItem(new MenuAction(8, "test8", "One"));
            menuAction.AddItem(new MenuAction(9, "test9", "One"));

            List<MenuAction> menuActions = menuAction.GetMenuActionsByName("One");
            menuActions.Count.Should().Be(4);
            foreach(var item in menuActions)
            {
                item.Menu.Should().Be("One");
            }
        }
    }
}
