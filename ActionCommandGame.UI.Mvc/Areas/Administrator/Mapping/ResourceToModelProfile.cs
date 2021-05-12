using ActionCommandGame.Model;
using ActionCommandGame.UI.Mvc.Areas.Administrator.Models;
using AutoMapper;

namespace ActionCommandGame.UI.Mvc.Areas.Administrator.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveItemResource, Item>();
            CreateMap<SaveNegativeEventResource, NegativeGameEvent>();
            CreateMap<SavePositiveEventResource, PositiveGameEvent>();
        }
    }
}