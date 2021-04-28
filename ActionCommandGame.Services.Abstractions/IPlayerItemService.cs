using System;
using System.Collections.Generic;
using ActionCommandGame.Model;
using ActionCommandGame.Services.Model.Core;

namespace ActionCommandGame.Services.Abstractions
{
    public interface IPlayerItemService
    {
        PlayerItem Get(Guid id);
        IList<PlayerItem> Find(Guid? playerId = null);
        ServiceResult<PlayerItem> Create(Guid playerId, Guid itemId);
        PlayerItem Update(Guid id, PlayerItem playerItem);
        ServiceResult Delete(Guid id);
    }
}
