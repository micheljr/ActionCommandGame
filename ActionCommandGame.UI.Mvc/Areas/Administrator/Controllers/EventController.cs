using System;
using ActionCommandGame.Model;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.UI.Mvc.Areas.Administrator.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ActionCommandGame.UI.Mvc.Areas.Administrator.Controllers
{
    public class EventController : AdminBaseController
    {
        private readonly IPositiveGameEventService _positiveGameEventService;
        private readonly INegativeGameEventService _negativeGameEventService;
        private readonly IMapper _mapper;

        public EventController(INegativeGameEventService negativeGameEventService, IPositiveGameEventService positiveGameEventService, IMapper mapper)
        {
            _negativeGameEventService = negativeGameEventService;
            _positiveGameEventService = positiveGameEventService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = new EventsModel
            {
                NegativeGameEvents = _negativeGameEventService.Find(),
                PositiveGameEvents = _positiveGameEventService.Find()
            };
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult CreatePositiveGameEvent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePositiveGameEvent(SavePositiveEventResource resource)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var posEvent = _mapper.Map<SavePositiveEventResource, PositiveGameEvent>(resource);

            var newEvent = _positiveGameEventService.Create(posEvent);

            if (newEvent == null)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong creating the event.");
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdatePositiveGameEvent(Guid id)
        {
            var posEvent = _positiveGameEventService.Get(id);
            var resource = _mapper.Map<PositiveGameEvent, SavePositiveEventResource>(posEvent);
            return View(resource);
        }

        [HttpPost]
        public IActionResult UpdatePositiveGameEvent(Guid id, SavePositiveEventResource resource)
        {
            if (!ModelState.IsValid)
            {
                var posEvent = _positiveGameEventService.Get(id);
                var eventResource = _mapper.Map<PositiveGameEvent, SavePositiveEventResource>(posEvent);
                return View(eventResource);
            }

            var mappedEvent = _mapper.Map<SavePositiveEventResource, PositiveGameEvent>(resource);

            var updatedEvent = _positiveGameEventService.Update(id, mappedEvent);

            if (updatedEvent == null)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong updating the event.");
                var posEvent = _positiveGameEventService.Get(id);
                var eventResource = _mapper.Map<PositiveGameEvent, SavePositiveEventResource>(posEvent);
                return View(eventResource);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeletePositiveGameEvent(Guid id)
        {
            var deleted = _positiveGameEventService.Delete(id);

            if (deleted)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong deleting the event.");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateNegativeGameEvent()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateNegativeGameEvent(SaveNegativeEventResource resource)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var negEvent = _mapper.Map<SaveNegativeEventResource, PositiveGameEvent>(resource);

            var newEvent = _positiveGameEventService.Create(negEvent);

            if (newEvent == null)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong creating the event.");
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateNegativeGameEvent(Guid id)
        {
            var negEvent = _negativeGameEventService.Get(id);
            var resource = _mapper.Map<NegativeGameEvent, SaveNegativeEventResource>(negEvent);
            return View(resource);
        }

        [HttpPost]
        public IActionResult UpdateNegativeGameEvent(Guid id, SaveNegativeEventResource resource)
        {
            if (!ModelState.IsValid)
            {
                var negEvent = _negativeGameEventService.Get(id);
                var eventResource = _mapper.Map<NegativeGameEvent, SaveNegativeEventResource>(negEvent);
                return View(eventResource);
            }

            var mappedEvent = _mapper.Map<SaveNegativeEventResource, NegativeGameEvent>(resource);

            var updatedEvent = _negativeGameEventService.Update(id, mappedEvent);

            if (updatedEvent == null)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong updating the event.");
                var negEvent = _negativeGameEventService.Get(id);
                var eventResource = _mapper.Map<NegativeGameEvent, SaveNegativeEventResource>(negEvent);
                return View(eventResource);
            }

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult DeleteNegativeGameEvent(Guid id)
        {
            var deleted = _negativeGameEventService.Delete(id);

            if (deleted)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong deleting the event.");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}