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
    public class CourseController : Controller
    {
        private readonly AppDbContext _db;
        public CourseController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Course course = await _db.Courses.Include(c => c.CourseContent).Include(c => c.CourseFeature).FirstOrDefaultAsync(c=>c.Id==id);
            if (course == null) return NotFound();
            CourseVM model = new CourseVM
            {
                Course = course,
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
            List<Course> model = _db.Courses.Where(c => c.Name.Contains(search)).OrderBy(c => c.Name).ToList();
            return PartialView("_CoursePartialView",model);
        }

        public IActionResult SearchBtn(string search)
        {
            List<Course> model = _db.Courses.Include(c=>c.CourseContent).Include(c=>c.CourseFeature).Where(c => c.Name.Contains(search)).OrderBy(c=>c.Name).ToList();
            if (model.Count == 0) return Content("null");
            return PartialView("_SearchCoursePartialView", model);
        }
    }
}