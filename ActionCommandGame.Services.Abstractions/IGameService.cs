using System;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Services.Abstractions
{
    public interface IGameService
    {
        ServiceResult<GameResult> PerformAction(Guid playerId);
        ServiceResult<BuyResult> Buy(Guid playerId, Guid itemId);
    }
}
