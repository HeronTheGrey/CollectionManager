using CollectionManager.App.Concrete;
using CollectionManager.Domain.Entity;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CollectionManager.Tests
{
    public class CollectionServiceTest
    {
        [Fact]
        public void UpdateCollectionTest()
        {
            Collection collectionBeforeUpdate = new Collection(1, "Itmes");
            Collection collectionAfterUpdate = new Collection(1, "Items");
            CollectionService collectionService = new CollectionService();
            collectionService.AddItem(collectionBeforeUpdate);

            collectionService.UpdateItem(collectionAfterUpdate);

            collectionService.GetItemById(1).Should().Be(collectionAfterUpdate);
        }
    }
}
