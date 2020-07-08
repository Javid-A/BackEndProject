using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.DAL;
using BackEndProject.Models;
using BackEndProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject.Controllers
{
    public class EventController : Controller
    {
        public readonly AppDbContext _db;
        public EventController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Event> events = _db.Events.Include(e => e.EventSpeakers).ToList();
            EventVM model = new EventVM
            {
                Events = events,
                BestTheme = _db.Blogs.OrderByDescending(b => b.Id).FirstOrDefault(),
                LatestPosts = _db.Blogs.Include(b => b.AppUser).OrderByDescending(b => b.Id).Take(3).ToList()
            };
            return View(model);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Event _event = await _db.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (_event == null) return NotFound();
            List<EventSpeaker> eventSpeakers = _db.EventSpeakers.Include(es=>es.Speaker).Where(es => es.EventId == _event.Id).ToList();
            EventVM model = new EventVM
            {
                Event = _event,
                EventSpeakers=eventSpeakers,
                BestTheme = _db.Blogs.OrderByDescending(b => b.Id).FirstOrDefault(),
                LatestPosts = _db.Blogs.Include(b => b.AppUser).OrderByDescending(b => b.Id).Take(3).ToList()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Detail")]
        public async Task<IActionResult> SendMessage(ContactVM _message)
        {
            Reply newReply = new Reply
            {
                Name = _message.Name,
                Email = _message.Email,
                Subject = _message.Subject,
                Message = _message.Message
            };
            _db.Replies.Add(newReply);
            await _db.SaveChangesAsync();
            return RedirectToAction("Detail");
        }
        public IActionResult Search(string search)
        {
            List<Event> model = _db.Events.Where(e => e.Title.Contains(search) || e.Venue.Contains(search)).OrderByDescending(c => c.Id).Take(5).ToList();
            return PartialView("_SearchEventPartialView", model);
        }
    }
}