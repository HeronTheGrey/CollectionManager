using CollectionManager.App.Concrete;
using CollectionManager.Domain.Entity;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CollectionManager.Tests
{
    public class ItemServiceTest
    {
        [Fact]
        public void UpdateItemTest()
        {
            Item itemBeforeUpdate = new Item(1, 55, "Itme", "");
            Item itemAfterUpdate = new Item(1, 5, "Item", "");
            ItemService itemService = new ItemService(2);
            itemService.Items.Add(itemBeforeUpdate);

            itemService.UpdateItem(itemAfterUpdate);

            itemService.GetItemById(1).Should().Be(itemAfterUpdate);
        }
    }
}
