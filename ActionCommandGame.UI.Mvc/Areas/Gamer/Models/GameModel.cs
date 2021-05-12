using System.Collections.Generic;
using ActionCommandGame.Model;
using Microsoft.AspNetCore.Identity;

namespace ActionCommandGame.UI.Mvc.Areas.Gamer.Models
{
    public class GameModel
    {
        public Player Player { get; set; }
        public List<Item> Items { get; set; }
        public List<PlayerItem> PlayerItems { get; set; }
        
    }
}