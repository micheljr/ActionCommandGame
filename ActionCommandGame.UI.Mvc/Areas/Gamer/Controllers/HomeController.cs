using System;
using System.Threading.Tasks;
using ActionCommandGame.Model;
using ActionCommandGame.Services;
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

        public HomeController(UserManager<IdentityUser> userManager, IPlayerService playerService, IMapper mapper)
        {
            _userManager = userManager;
            _playerService = playerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var player = _playerService.Get(Guid.Parse(user.Id));
            
            return View(player);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SavePlayerResource resource)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            var user = await _userManager.GetUserAsync(HttpContext.User);
            resource.Id = Guid.Parse(user.Id);
            
            var player = _mapper.Map<SavePlayerResource, Player>(resource);
            var dbPlayer = _playerService.Create(player);
            if (dbPlayer == null)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong creating the player.");
                return View();
            }

            return View(dbPlayer);
        }
    }
}