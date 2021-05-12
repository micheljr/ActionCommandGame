
using System;
using System.Threading.Tasks;
using ActionCommandGame.Model;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.UI.Mvc.Areas.Gamer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ActionCommandGame.UI.Mvc.Areas.Gamer.Controllers
{
    public class HomeController : GamerBaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;
        private readonly IPositiveGameEventService _positiveGameEventService;

        public HomeController(UserManager<IdentityUser> userManager, IPlayerService playerService, IMapper mapper, IPositiveGameEventService positiveGameEventService)
        {
            _userManager = userManager;
            _playerService = playerService;
            _mapper = mapper;
            _positiveGameEventService = positiveGameEventService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // get logged in user.
            var user = await GetCurrentUserAsync();
            var player = _playerService.Get(Guid.Parse(user.Id));
            
            var testEvent = _positiveGameEventService.Get(Guid.Parse("B57A8AB3-F8ED-460D-BD86-41303A9B38F0"));
            
            var model = new GameModel
            {
                Player = player,
                Event = testEvent
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SavePlayerResource resource)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            // check if username is already in use.
            var existingPlayer = _playerService.GetByName(resource.Name);
            if (existingPlayer != null)
            {
                ModelState.AddModelError("UsernameInUse", "This username is already in use. Please choose another.");
                return View();
            }
            //get logged in user.
            var user = await GetCurrentUserAsync();
            resource.Id = Guid.Parse(user.Id);
            // map resource to player, then save the player.
            var player = _mapper.Map<SavePlayerResource, Player>(resource);
            var dbPlayer = _playerService.Create(player);
            if (dbPlayer == null)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong creating the player.");
                return View();
            }

            
            
            var model = new GameModel
            {
                Player = dbPlayer
            };

            return View(model);
        }

        /*
         * Gets the currently logged in user from HttpContext (cookie).
         */
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}