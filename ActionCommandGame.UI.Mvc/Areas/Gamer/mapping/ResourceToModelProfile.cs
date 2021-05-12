using ActionCommandGame.Model;
using ActionCommandGame.UI.Mvc.Areas.Gamer.Models;
using AutoMapper;

namespace ActionCommandGame.UI.Mvc.Areas.Gamer.mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SavePlayerResource, Player>();
        }
    }
}