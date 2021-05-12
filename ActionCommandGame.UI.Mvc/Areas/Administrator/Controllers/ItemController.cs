using System;
using ActionCommandGame.Model;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.UI.Mvc.Areas.Administrator.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ActionCommandGame.UI.Mvc.Areas.Administrator.Controllers
{
    public class ItemController : AdminBaseController
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public ItemController(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var items = _itemService.Find();
            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SaveItemResource resource)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var item = _mapper.Map<SaveItemResource, Item>(resource);
            
            var newItem = _itemService.Create(item);

            if (newItem == null)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong creating the item.");
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var item = _itemService.Get(id);
            var resource = _mapper.Map<Item, SaveItemResource>(item);
            return View(resource);
        }

        [HttpPost]
        public IActionResult Update(Guid id, SaveItemResource resource)
        {
            if (!ModelState.IsValid)
            {
                var item = _itemService.Get(id);
                var response = _mapper.Map<Item, SaveItemResource>(item);
                return View(response);
            }

            var mappedItem = _mapper.Map<SaveItemResource, Item>(resource);

            var updatedItem = _itemService.Update(id, mappedItem);

            if (updatedItem == null)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong updating the item.");
                var item = _itemService.Get(id);
                var response = _mapper.Map<Item, SaveItemResource>(item);
                return View(response);
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var deleted = _itemService.Delete(id);

            if (deleted)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong deleting the item.");
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }
    }
}