using System;
using System.Collections.Generic;
using ActionCommandGame.Model;

namespace ActionCommandGame.Services.Abstractions
{
    public interface IPositiveGameEventService
    {
        PositiveGameEvent Get(Guid id);
        PositiveGameEvent GetRandomPositiveGameEvent(bool hasAttackItem);
        IList<PositiveGameEvent> Find();
        PositiveGameEvent Create(PositiveGameEvent gameEvent);
        PositiveGameEvent Update(Guid id, PositiveGameEvent gameEvent);
        bool Delete(Guid id);
    }
}
