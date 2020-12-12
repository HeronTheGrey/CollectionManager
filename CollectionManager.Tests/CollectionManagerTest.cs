using CollectionManager.App.Abstract;
using CollectionManager.App.Concrete;
using CollectionManager.Domain.Entity;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CollectionManager.Tests
{
    public class CollectionManagerTest
    {
        [Fact]
        public void GetCollectionByIdTest()
        {
            //Arrange
            Collection collection = new Collection(1, "Items", "");
            CollectionService collectionService = new CollectionService();
            collectionService.AddItem(collection);

            CollectionManager.App.Managers.CollectionManager collectionManager = new CollectionManager.App.Managers.CollectionManager(new MenuActionService(), collectionService , new ItemService()) ;
            //Act
            var result = collectionManager.GetCollectionById(1);
            //Assert
            Assert.Equal(1, result.Id);
        }


        [Fact]
        public void DeleteCollectionAndItsItemsTest()
        {
            //Arrange
            Collection collection = new Collection(1, "Items", "");
            Item item1 = new Item(1, 2, "Item1", "", 1);
            Item item2 = new Item(2, 2, "Item2", "", 1);
            Item item3 = new Item(3, 2, "Item3", "", 1);
            Item item4 = new Item(4, 2, "Item4", "", 1);
            Item item5 = new Item(5, 2, "Item5", "", 2);
            Item item6 = new Item(6, 2, "Item6", "", 2);

            CollectionService collectionService = new CollectionService();
            ItemService itemService = new ItemService();

            collectionService.AddItem(collection);
            itemService.AddItem(item1);
            itemService.AddItem(item2);
            itemService.AddItem(item3);
            itemService.AddItem(item4);
            itemService.AddItem(item5);
            itemService.AddItem(item6);

            CollectionManager.App.Managers.CollectionManager collectionManager = new CollectionManager.App.Managers.CollectionManager(new MenuActionService(), collectionService, itemService);

            //Act
            collectionManager.DeleteCollectionAndItsItems(1);

            //Assert
            collectionService.Items.Count.Should().Be(0);
            itemService.Items.Count.Should().Be(2);

        }

    }
}
