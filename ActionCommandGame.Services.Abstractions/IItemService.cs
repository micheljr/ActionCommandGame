using System;
using System.Collections.Generic;
using ActionCommandGame.Model;

namespace ActionCommandGame.Services.Abstractions
{
    public interface IItemService
    {
        Item Get(Guid id);
        IList<Item> Find();
        Item Create(Item item);
        Item Update(Guid id, Item item);
        bool Delete(Guid id);
    }
}
