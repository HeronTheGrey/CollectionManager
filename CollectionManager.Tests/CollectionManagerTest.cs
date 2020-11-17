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
            Collection collection = new Collection(1, "Items", "");
            var mock = new Mock<IService<Collection>>();
            mock.Setup(s => s.GetItemById(1)).Returns(collection);
            CollectionManager.App.Managers.CollectionManager collectionManager = new CollectionManager.App.Managers.CollectionManager(new MenuActionService(), mock.Object , new List<ItemService>()) ;

            var result = collectionManager.GetCollectionById(1);

            Assert.Equal(1, result.Id);
        }


        [Fact]
        public void DeleteCollectionAndAnalogousItemServiceTest()
        {
            Collection collection = new Collection(1, "Items", "");
            List<ItemService> itemServices = new List<ItemService>();
            itemServices.Add(new ItemService(1));

            var mock = new Mock<IService<Collection>>();
            mock.Setup(s => s.GetItemById(1)).Returns(collection);
            mock.Setup(s => s.RemoveItem(It.IsAny<Collection>()));
            CollectionManager.App.Managers.CollectionManager collectionManager = new CollectionManager.App.Managers.CollectionManager(new MenuActionService(), mock.Object, itemServices);

            collectionManager.DeleteCollectionAndAnalogousItemService(1);

            mock.Verify(s => s.RemoveItem(collection));
            itemServices.Should().BeEmpty();
        }

    }
}
