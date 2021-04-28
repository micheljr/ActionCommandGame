using System;

namespace ActionCommandGame.Model.Abstractions
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}
