using System;
using System.Collections.Generic;
using ActionCommandGame.Model.Abstractions;

namespace ActionCommandGame.Model
{
    public class PlayerItem: IIdentifiable
    {
        public PlayerItem()
        {
            FuelPlayers = new List<Player>();
            AttackPlayers = new List<Player>();
            DefensePlayers = new List<Player>();
        }

        public Guid Id { get; set; }

        public Guid PlayerId { get; set; }
        public Player Player { get; set; }

        public Guid ItemId { get; set; }
        public Item Item { get; set; }

        public int RemainingFuel { get; set; }
        public int RemainingAttack { get; set; }
        public int RemainingDefense { get; set; }

        public IList<Player> FuelPlayers { get; set; }
        public IList<Player> AttackPlayers { get; set; }
        public IList<Player> DefensePlayers { get; set; }
    }
}
