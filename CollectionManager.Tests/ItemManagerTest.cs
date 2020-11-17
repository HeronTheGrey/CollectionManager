using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CollectionManager.App.Managers;
using CollectionManager.App.Concrete;
using FluentAssertions;

namespace CollectionManager.Tests
{
    public class ItemManagerTest
    {
        [Fact]
        public void DeleteItemTest()
        {
            ItemService itemService = new ItemService(8);
            ItemManager itemManager = new ItemManager(new MenuActionService(), itemService);
            itemService.AddItem(new Item(1, 1, "Item", ""));

            itemManager.DeleteItem(1);

            itemService.Items.Should().BeEmpty();
        }
    }
}
