using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.DAL;
using BackEndProject.Models;
using BackEndProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;
        public ContactController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            Bio bio = _db.Bios.FirstOrDefault();
            ContactVM model = new ContactVM
            {
                Bio = bio
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
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
            return RedirectToAction("Index");
        }
    }
}