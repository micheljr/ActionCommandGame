using System;
using System.Collections.Generic;
using System.Linq;
using ActionCommandGame.Model;
using ActionCommandGame.Repository;
using ActionCommandGame.Services.Abstractions;

namespace ActionCommandGame.Services
{
    public class ItemService: IItemService
    {
        private readonly ActionButtonGameUiDbContext _database;

        public ItemService(ActionButtonGameUiDbContext database)
        {
            _database = database;
        }

        public Item Get(Guid id)
        {
            return _database.Items.SingleOrDefault(i => i.Id == id);
        }

        public IList<Item> Find()
        {
            return _database.Items.ToList();
        }

        public Item Create(Item item)
        {
            throw new NotImplementedException();
        }

        public Item Update(Guid id, Item item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
