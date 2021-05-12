using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActionCommandGame.UI.Mvc.Areas.Gamer.Controllers
{
    [Area("Gamer")]
    [Authorize(Roles = "Gamer,Administrator")]
    public class GamerBaseController : Controller
    {
        
    }
}