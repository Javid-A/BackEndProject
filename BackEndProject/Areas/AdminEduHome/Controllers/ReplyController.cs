using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.DAL;
using BackEndProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject.Areas.AdminEduHome.Controllers
{
    [Area("AdminEduHome")]
    [Authorize(Roles = "Admin")]
    public class ReplyController : Controller
    {
        private readonly AppDbContext _db;
        public ReplyController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Replies);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            Reply reply = _db.Replies.FirstOrDefault(r => r.Id == id);
            if (reply == null) return NotFound();
            return View(reply);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            Reply reply = _db.Replies.FirstOrDefault(r => r.Id == id);
            if (reply == null) return NotFound();
            return View(reply);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Reply reply =_db.Replies.FirstOrDefault(r => r.Id == id);
            if (reply == null) return NotFound();
            _db.Replies.Remove(reply);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}