using System;
using System.Collections.Generic;
using System.Linq;
using ActionCommandGame.Model;
using ActionCommandGame.Repository;
using ActionCommandGame.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ActionCommandGame.Services
{
    public class PlayerService: IPlayerService
    {
        private readonly ActionButtonGameUiDbContext _database;

        public PlayerService(ActionButtonGameUiDbContext database)
        {
            _database = database;
        }

        public Player Get(Guid id)
        {
            return _database.Players
                .Include(p => p.CurrentFuelPlayerItem.Item)
                .Include(p => p.CurrentAttackPlayerItem.Item)
                .Include(p => p.CurrentDefensePlayerItem.Item)
                .SingleOrDefault(p => p.Id == id);
        }

        public IList<Player> Find()
        {
            return _database.Players
                .Include(p => p.CurrentFuelPlayerItem.Item)
                .Include(p => p.CurrentAttackPlayerItem.Item)
                .Include(p => p.CurrentDefensePlayerItem.Item)
                .ToList();
        }

        public Player Create(Player player)
        {
            throw new System.NotImplementedException();
        }

        public Player Update(Guid id, Player player)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new System.NotImplementedException();
        }

        public Player GetByName(string name)
        {
            return _database.Players
                .Include(p => p.CurrentFuelPlayerItem.Item)
                .Include(p => p.CurrentAttackPlayerItem.Item)
                .Include(p => p.CurrentDefensePlayerItem.Item)
                .SingleOrDefault(p => p.Name.Equals(name));
        }
    }
}
