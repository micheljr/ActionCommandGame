using ActionCommandGame.Model;
using ActionCommandGame.UI.Mvc.Areas.Administrator.Models;
using AutoMapper;

namespace ActionCommandGame.UI.Mvc.Areas.Administrator.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Item, SaveItemResource>();
            CreateMap<PositiveGameEvent, SavePositiveEventResource>();
            CreateMap<NegativeGameEvent, SaveNegativeEventResource>();
        }
    }
}