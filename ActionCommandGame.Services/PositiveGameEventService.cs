using System;
using System.Collections.Generic;
using System.Linq;
using ActionCommandGame.Model;
using ActionCommandGame.Repository;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Helpers;

namespace ActionCommandGame.Services
{
    public class PositiveGameEventService: IPositiveGameEventService
    {
        private readonly ActionButtonGameUiDbContext _database;

        public PositiveGameEventService(ActionButtonGameUiDbContext database)
        {
            _database = database;
        }

        public PositiveGameEvent Get(Guid id)
        {
            return _database.PositiveGameEvents.Find(id);
        }

        public PositiveGameEvent GetRandomPositiveGameEvent(bool hasAttackItem)
        {
            var query = _database.PositiveGameEvents.AsQueryable();

            //If we don't have an attack item, we can only get low-reward items.
            if (!hasAttackItem)
            {
                query = query.Where(p => p.Money < 50);
            }

            var gameEvents = query.ToList();

            return GameEventHelper.GetRandomPositiveGameEvent(gameEvents);
        }

        public IList<PositiveGameEvent> Find()
        {
            return _database.PositiveGameEvents.ToList();
        }

        public PositiveGameEvent Create(PositiveGameEvent gameEvent)
        {
            var createdEvent = _database.PositiveGameEvents.Add(gameEvent).Entity;
            _database.SaveChanges();
            return createdEvent;
        }

        public PositiveGameEvent Update(Guid id, PositiveGameEvent gameEvent)
        {
            var dbEvent = _database.PositiveGameEvents.Find(id);
            if (dbEvent == null)
            {
                return null;
            }
            
            dbEvent.Description = gameEvent.Description;
            dbEvent.Name = gameEvent.Name;
            dbEvent.Probability = gameEvent.Probability;
            dbEvent.Money = gameEvent.Money;
            dbEvent.Experience = gameEvent.Experience;

            var updatedEvent = _database.PositiveGameEvents.Update(dbEvent).Entity;
            _database.SaveChanges();
            return updatedEvent;
        }

        public bool Delete(Guid id)
        {
            var dbEvent = _database.PositiveGameEvents.Find(id);
            if (dbEvent == null)
            {
                return false;
            }

            var removedEvent = _database.PositiveGameEvents.Remove(dbEvent).Entity;
            _database.SaveChanges();
            return removedEvent != null;
        }
    }
}
