using System;
using System.Collections.Generic;
using ActionCommandGame.Model;

namespace ActionCommandGame.Services.Abstractions
{
    public interface IPlayerService
    {
        Player Get(Guid id);
        IList<Player> Find();
        Player Create(Player player);
        Player Update(Guid id, Player player);
        bool Delete(Guid id);
        Player GetByName(string name);
    }
}
