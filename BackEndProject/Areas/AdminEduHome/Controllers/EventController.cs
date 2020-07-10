using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.DAL;
using BackEndProject.Extentions;
using BackEndProject.Models;
using BackEndProject.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject.Areas.AdminEduHome.Controllers
{
	[Area("AdminEduHome")]
	public class EventController : Controller
	{
		private readonly AppDbContext _db;
		private readonly IHostingEnvironment _env;
		public EventController(AppDbContext db, IHostingEnvironment env)
		{
			_db = db;
			_env = env;
		}
		public IActionResult Index()
		{
			return View(_db.Events);
		}
		public async Task<IActionResult> Detail(int? id)
		{
			if (id == null) return NotFound();
			Event _event = await _db.Events.FirstOrDefaultAsync(c => c.Id == id);
			IList<EventSpeaker> eventSpeakers = _db.EventSpeakers.Include(e => e.Speaker).Where(e => e.EventId == _event.Id).ToList();
			if (_event == null) return NotFound();
			return View(_event);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null) return NotFound();
			Event _event = await _db.Events.Include(e => e.EventSpeakers).FirstOrDefaultAsync(e => e.Id == id);
			IList<EventSpeaker> eventSpeakers = _db.EventSpeakers.Include(e => e.Event).Include(e => e.Speaker).Where(e => e.EventId == _event.Id).ToList();
			if (_event == null) return NotFound();
			EventVM model = new EventVM
			{
				Event = _event,
				Speakers = _db.Speakers.ToList(),
				EventSpeakers = eventSpeakers
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int? id, EventVM editedEvent)
		{
			Event _event = await _db.Events.Include(e => e.EventSpeakers).FirstOrDefaultAsync(e => e.Id == id);
			IList<EventSpeaker> eventSpeakers = _db.EventSpeakers.Include(e => e.Event).Include(e => e.Speaker).Where(e => e.EventId == _event.Id).ToList();
			EventVM model = new EventVM
			{
				Event = _event,
				Speakers = _db.Speakers.ToList(),
				EventSpeakers = eventSpeakers
			};
			if (!editedEvent.Photo.IsImage())
			{
				ModelState.AddModelError("", "You can choose only image file");
				return View(model);
			}
			if (!editedEvent.Photo.MaxLength(1500))
			{
				ModelState.AddModelError("", "Image must be maximum 1,5 MB");
				return View(model);
			}
			if (editedEvent.Photo.FileName == _event.ImagePath)
			{
				ModelState.AddModelError("", "You can't change current image with same image name");
				return View(model);
			}
			Helpers.Helper.DeleteImg(_env.WebRootPath, "img", "event", _event.ImagePath);
			_event.ImagePath = await editedEvent.Photo.SaveImg(_env.WebRootPath, "img", "event");
			_event.Title = editedEvent.Title;
			_event.Description = editedEvent.Description;
			_event.Date = editedEvent.Date;
			_event.Time = editedEvent.Time;
			_event.Venue = editedEvent.Venue;
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		public IActionResult Create()
		{
			EventVM model = new EventVM
			{
				Speakers = _db.Speakers.ToList()
			};
			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(EventVM _event)
		{
			EventVM model = new EventVM
			{
				Speakers = _db.Speakers.ToList()
			};
			if (!_event.Photo.IsImage())
			{
				ModelState.AddModelError("", "You can choose only image file");
				return View(model);
			}
			if (!_event.Photo.MaxLength(1500))
			{
				ModelState.AddModelError("", "Image must be maximum 1,5 MB");
				return View(model);
			}
			Event newEvent = new Event
			{
				Title = _event.Title,
				Description = _event.Description,
				Date = _event.Date,
				Time = _event.Time,
				Venue = _event.Venue
			};
			newEvent.ImagePath = await _event.Photo.SaveImg(_env.WebRootPath, "img", "event");
			List<EventSpeaker> eventSpeakers = new List<EventSpeaker>();
			var speaker1 = Request.Form["speaker1"];
			Speaker selectedS1 = _db.Speakers.FirstOrDefault(s => s.Id.ToString() == speaker1.ToString());
			var speaker2 = Request.Form["speaker2"];
			Speaker selectedS2 = _db.Speakers.FirstOrDefault(s => s.Id.ToString() == speaker2.ToString());
			eventSpeakers.Add(new EventSpeaker
			{
				EventId = newEvent.Id,
				SpeakerId = selectedS1.Id
			});
			eventSpeakers.Add(new EventSpeaker
			{
				EventId = newEvent.Id,
				SpeakerId = selectedS2.Id
			});
			newEvent.EventSpeakers = eventSpeakers;
			await _db.Events.AddAsync(newEvent);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");

		}
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) return NotFound();
			Event _event = await _db.Events.FirstOrDefaultAsync(c => c.Id == id);
			IList<EventSpeaker> eventSpeakers = _db.EventSpeakers.Include(e => e.Speaker).Where(e => e.EventId == _event.Id).ToList();
			if (_event == null) return NotFound();
			return View(_event);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("Delete")]
		public async Task<IActionResult> DeletePost(int? id)
		{
			if (id == null) return NotFound();
			Event _event = await _db.Events.FirstOrDefaultAsync(c => c.Id == id);
			IList<EventSpeaker> eventSpeakers = _db.EventSpeakers.Include(e => e.Speaker).Where(e => e.EventId == _event.Id).ToList();
			if (_event == null) return NotFound();

			Helpers.Helper.DeleteImg(_env.WebRootPath, "img", "event", _event.ImagePath);
			_db.Events.Remove(_event);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		public IActionResult SearchSpeaker(string search)
		{
			List<Speaker> model = _db.Speakers.Where(e => e.Fullname.Contains(search)).OrderByDescending(c => c.Id).Take(6).ToList();
			return PartialView("_AdminEventSearchPartialView", model);
		}

	}
}