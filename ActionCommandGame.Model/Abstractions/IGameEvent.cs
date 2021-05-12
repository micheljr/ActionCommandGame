using System;

namespace ActionCommandGame.Model.Abstractions
{
    public interface IGameEvent : IIdentifiable, IHasProbability
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}