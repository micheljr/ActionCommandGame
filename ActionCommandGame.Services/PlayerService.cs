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
            player.Experience = 0;
            player.Money = 0;
            _database.Players.Add(player);
            _database.SaveChanges();

            return Get(player.Id);
        }

        public Player Update(Guid id, Player player)
        {
            var existingPlayer = _database.Players.Find(id);
            if (existingPlayer == null)
            {
                return null;
            }
            _database.Update(player);
            _database.SaveChanges();
            
            return player;
        }

        public bool Delete(Guid id)
        {
            var existingPlayer = _database.Players.Find(id);
            if (existingPlayer == null)
            {
                return false;
            }

            _database.Players.Remove(existingPlayer);
            _database.SaveChanges();
            return true;
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
