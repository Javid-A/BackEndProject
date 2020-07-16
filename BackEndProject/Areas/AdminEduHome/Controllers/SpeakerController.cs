using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.DAL;
using BackEndProject.Extentions;
using BackEndProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject.Areas.AdminEduHome.Controllers
{
    [Area("AdminEduHome")]
    [Authorize(Roles = "Admin")]
    public class SpeakerController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        public SpeakerController(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Speakers);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Speaker speaker = await _db.Speakers.FindAsync(id);
            if (speaker == null) return NotFound();
            return View(speaker);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            Speaker speaker = await _db.Speakers.FindAsync(id);
            if (speaker == null) return NotFound();
            return View(speaker);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,Speaker editedSpeaker)
        {
            if (id == null) return NotFound();
            Speaker speaker = await _db.Speakers.FindAsync(id);
            if (speaker == null) return NotFound();
            if (!editedSpeaker.Photo.IsImage())
            {
                ModelState.AddModelError("", "You can choose only image file");
                return View(speaker);
            }
            if (!editedSpeaker.Photo.MaxLength(1500))
            {
                ModelState.AddModelError("", "Image must be maximum 1,5 MB");
                return View(speaker);
            }
            if (editedSpeaker.Photo.FileName == speaker.ImagePath)
            {
                ModelState.AddModelError("", "You can't change current image with same image name");
                return View(speaker);
            }
            Helpers.Helper.DeleteImg(_env.WebRootPath, "img", "event", speaker.ImagePath);
            speaker.ImagePath = await editedSpeaker.Photo.SaveImg(_env.WebRootPath, "img", "event");
            speaker.Fullname = editedSpeaker.Fullname;
            speaker.Profession = editedSpeaker.Profession;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Speaker speaker)
        {
            if (speaker.Photo == null)
            {
                return View();
            }
            if (!speaker.Photo.IsImage())
            {
                ModelState.AddModelError("", "You can choose only image file");
                return View();
            }
            if (!speaker.Photo.MaxLength(1500))
            {
                ModelState.AddModelError("", "Image must be maximum 1,5 MB");
                return View();
            }
            Speaker newSpeaker = new Speaker
            {
                Fullname = speaker.Fullname,
                Profession = speaker.Profession
            };
            newSpeaker.ImagePath = await speaker.Photo.SaveImg(_env.WebRootPath, "img", "event");
            _db.Speakers.Add(newSpeaker);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Speaker speaker = await _db.Speakers.FindAsync(id);
            if (speaker == null) return NotFound();
            return View(speaker);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Speaker speaker = await _db.Speakers.FindAsync(id);
            if (speaker == null) return NotFound();

            Helpers.Helper.DeleteImg(_env.WebRootPath, "img", "event", speaker.ImagePath);
            _db.Speakers.Remove(speaker);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}