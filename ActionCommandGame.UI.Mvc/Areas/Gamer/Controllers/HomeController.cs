
using System;
using System.Linq;
using System.Threading.Tasks;
using ActionCommandGame.Model;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Extensions;
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
        private readonly IPlayerItemService _playerItemService;
        private readonly IGameService _gameService;
        private readonly IItemService _itemService;

        public HomeController(UserManager<IdentityUser> userManager, IPlayerService playerService, IMapper mapper, IPlayerItemService playerItemService, IGameService gameService, IItemService itemService)
        {
            _userManager = userManager;
            _playerService = playerService;
            _mapper = mapper;
            _playerItemService = playerItemService;
            _gameService = gameService;
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(GameModel model = null)
        {
            // get logged in user.
            var user = await GetCurrentUserAsync();
            var player = _playerService.Get(Guid.Parse(user.Id));
            var playerItemList = _playerItemService.Find(player.Id).ToList();

            model ??= new GameModel();

            model.Player = player;
            model.PlayerItems = playerItemList;

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
                ModelState.AddModelError(string.Empty, "This username is already in use. Please choose another.");
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

            return View();
        }

        public IActionResult PerformAction(Guid id)
        {
            var result = _gameService.PerformAction(id);
            var player = result.Data.Player;
            var positiveGameEvent = result.Data.PositiveGameEvent;
            var negativeGameEvent = result.Data.NegativeGameEvent;
            var playerItemList = _playerItemService.Find(player.Id);
            var gameModel = new GameModel
            {
                Player = player,
                PlayerItems = playerItemList
            };

            if (positiveGameEvent != null)
            {
                gameModel.PositiveGameEvent = positiveGameEvent;
                
            }

            if (negativeGameEvent != null)
            {
                gameModel.NegativeGameEvent = negativeGameEvent;
            }

            return View("Index", gameModel);
        }

        public IActionResult Shop(Guid id)
        {
            var player = _playerService.Get(id);
            var playerItemList = _playerItemService.Find(id);
            var itemList = _itemService.Find();
            var model = new GameModel
            {
                Player = player,
                PlayerItems = playerItemList,
                Items = itemList
            };
            return View(model);
        }

        public async Task<IActionResult> BuyItem(Guid itemId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var idResult = Guid.TryParse(user.Id, out var playerId);
            var player = _playerService.Get(playerId);
            var playerItemList = _playerItemService.Find(playerId);
            var itemList = _itemService.Find();
            var model = new GameModel
            {
                Player = player,
                PlayerItems = playerItemList,
                Items = itemList
            };
            if (!idResult)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong getting the player.");
                return View("Shop", model);
            }
            
            var result = _gameService.Buy(playerId, itemId);
            if (!result.IsSuccess)
            {

                foreach (var message in result.Messages)
                {
                    ModelState.AddModelError(string.Empty, message.Message);
                }
                return View("Shop", model);
            }

            return RedirectToAction("Index");
        }

        public IActionResult HighScores()
        {
            var players = _playerService.Find();
            return View(players);
        }

        /*
         * Gets the currently logged in user from HttpContext (cookie).
         */
        private async Task<IdentityUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(HttpContext.User);
    }
}