using System.Collections.Generic;
using ActionCommandGame.Model;
using ActionCommandGame.Model.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace ActionCommandGame.UI.Mvc.Areas.Gamer.Models
{
    public class GameModel
    {
        public Player Player { get; set; }
        
        public IList<Item> Items { get; set; }
        public IList<PlayerItem> PlayerItems { get; set; }
        public PositiveGameEvent PositiveGameEvent { get; set; }
        public NegativeGameEvent NegativeGameEvent { get; set; }
    }
}