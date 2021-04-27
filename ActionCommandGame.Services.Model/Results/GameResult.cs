﻿using System.Collections.Generic;
using ActionCommandGame.Model;
using ActionCommandGame.Services.Model.Core;

namespace ActionCommandGame.Services.Model.Results
{
    public class GameResult
    {
        public Player Player { get; set; }
        public PositiveGameEvent PositiveGameEvent { get; set; }
        public NegativeGameEvent NegativeGameEvent { get; set; }
        public IList<ServiceMessage> NegativeGameEventMessages { get; set; }
    }
}
