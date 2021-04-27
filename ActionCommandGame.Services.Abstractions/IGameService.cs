﻿using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Services.Abstractions
{
    public interface IGameService
    {
        ServiceResult<GameResult> PerformAction(int playerId);
        ServiceResult<BuyResult> Buy(int playerId, int itemId);
    }
}
