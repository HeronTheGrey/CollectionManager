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
        public void GetListOfItemsByCollectionIdTest()
        {
            //Arrange
            ItemService itemService = new ItemService();
            itemService.AddItem(new Item(1, 2, "item", "", 1));
            itemService.AddItem(new Item(2, 2, "item", "", 1));
            itemService.AddItem(new Item(3, 2, "item", "", 1));
            itemService.AddItem(new Item(4, 2, "item", "", 2));
            itemService.AddItem(new Item(5, 2, "item", "", 2));
            ItemManager itemManager = new ItemManager(new MenuActionService(), itemService, 1);

            //Act
            var itemList = itemManager.GetListOfItemsByCollectionId(1);

            //Assert
            itemList.Count.Should().Be(3);
        }

        [Fact]
        public void DeleteItemTest()
        {
            //Arrange
            ItemService itemService = new ItemService();
            itemService.AddItem(new Item(1, 1, "Item", "",1));
            ItemManager itemManager = new ItemManager(new MenuActionService(), itemService,1);
            
            //Act
            itemManager.DeleteItem(1);

            //Assert
            itemService.Items.Should().BeEmpty();
        }
    }
}
