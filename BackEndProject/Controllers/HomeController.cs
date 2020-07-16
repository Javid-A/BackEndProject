using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.DAL;
using BackEndProject.Models;
using BackEndProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            Bio bio = _db.Bios.FirstOrDefault();
            IEnumerable<Slider> sliders = _db.Sliders.ToList();
            IEnumerable<Event> events = _db.Events.OrderByDescending(e => e.Id).Take(4).ToList();
            IEnumerable<Course> courses = _db.Courses.Include(c=>c.CourseContent).OrderByDescending(c => c.Id).Take(3).ToList();
            IEnumerable<Blog> blogs = _db.Blogs.Include(b=>b.AppUser).OrderByDescending(b => b.Id).Take(3).ToList();
            IEnumerable<Notice> notices = _db.Notices.OrderByDescending(n => n.Id).Take(6).ToList();
            Testimonial testimonial = _db.Testimoinals.FirstOrDefault();
            HomeVM model = new HomeVM
            {
                Bio = bio,
                Sliders = sliders,
                Events = events,
                Courses = courses,
                Blogs = blogs,
                Notices=notices,
                Testimonial=testimonial
            };
            return View(model);
        }
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}