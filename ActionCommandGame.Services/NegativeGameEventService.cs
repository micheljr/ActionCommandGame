using System;
using System.Collections.Generic;
using System.Linq;
using ActionCommandGame.Model;
using ActionCommandGame.Repository;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Helpers;

namespace ActionCommandGame.Services
{
    public class NegativeGameEventService: INegativeGameEventService
    {
        private readonly ActionButtonGameUiDbContext _database;

        public NegativeGameEventService(ActionButtonGameUiDbContext database)
        {
            _database = database;
        }

        public NegativeGameEvent Get(Guid id)
        {
            return _database.NegativeGameEvents.Find(id);
        }

        public NegativeGameEvent GetRandomNegativeGameEvent()
        {
            var gameEvents = Find();
            return GameEventHelper.GetRandomNegativeGameEvent(gameEvents);
        }

        public IList<NegativeGameEvent> Find()
        {
            return _database.NegativeGameEvents.ToList();
        }

        public NegativeGameEvent Create(NegativeGameEvent gameEvent)
        {
            var createdEvent = _database.NegativeGameEvents.Add(gameEvent);
            _database.SaveChanges();
            return createdEvent.Entity;
        }

        public NegativeGameEvent Update(Guid id, NegativeGameEvent gameEvent)
        {
            var dbEvent = _database.NegativeGameEvents.Find(id);
            if (dbEvent == null)
            {
                return null;
            }

            dbEvent.Description = gameEvent.Description;
            dbEvent.Name = gameEvent.Name;
            dbEvent.Probability = gameEvent.Probability;
            dbEvent.DefenseLoss = gameEvent.DefenseLoss;
            dbEvent.DefenseWithGearDescription = gameEvent.DefenseWithGearDescription;
            dbEvent.DefenseWithoutGearDescription = gameEvent.DefenseWithoutGearDescription;

            var updatedEvent = _database.NegativeGameEvents.Update(dbEvent);
            _database.SaveChanges();
            return updatedEvent.Entity;
        }

        public bool Delete(Guid id)
        {
            var dbEvent = _database.NegativeGameEvents.Find(id);
            if (dbEvent == null)
            {
                return false;
            }

            var deletedEvent = _database.NegativeGameEvents.Remove(dbEvent);
            _database.SaveChanges();
            return deletedEvent.Entity != null;
        }
    }
}
