using System.Collections.Generic;
using ActionCommandGame.Model;

namespace ActionCommandGame.UI.Mvc.Areas.Administrator.Models
{
    public class EventsModel
    {
        public IList<PositiveGameEvent> PositiveGameEvents { get; set; }
        public IList<NegativeGameEvent> NegativeGameEvents { get; set; }
    }
}