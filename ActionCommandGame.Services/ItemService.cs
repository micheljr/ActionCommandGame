using System;
using System.Collections.Generic;
using System.Linq;
using ActionCommandGame.Model;
using ActionCommandGame.Repository;
using ActionCommandGame.Services.Abstractions;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
            item.Id = Guid.NewGuid();
            _database.Items.Add(item);
            _database.SaveChanges();
            
            return item;
        }

        public Item Update(Guid id, Item item)
        {
            _database.Items.Update(item);
            _database.SaveChanges();
            return item;
            
        }

        public bool Delete(Guid id)
        {
            try
            {
                var item = _database.Items.Find(id);
                _database.Items.Remove(item);
                _database.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
