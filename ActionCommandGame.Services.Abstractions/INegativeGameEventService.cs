using System;
using System.Collections.Generic;
using ActionCommandGame.Model;

namespace ActionCommandGame.Services.Abstractions
{
    public interface INegativeGameEventService
    {
        NegativeGameEvent Get(Guid id);
        NegativeGameEvent GetRandomNegativeGameEvent();
        IList<NegativeGameEvent> Find();
        NegativeGameEvent Create(NegativeGameEvent gameEvent);
        NegativeGameEvent Update(Guid id, NegativeGameEvent gameEvent);
        bool Delete(Guid id);
    }
}
