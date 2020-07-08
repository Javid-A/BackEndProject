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
    public class AboutController : Controller
    {
        private readonly AppDbContext _db;
        public AboutController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            Bio bio = _db.Bios.FirstOrDefault();
            IEnumerable<Teacher> teachers = _db.Teachers.Include(t => t.TeacherContact).Include(t=>t.TeacherSkill).OrderByDescending(b => b.Id).Take(4).ToList();
            IEnumerable<Notice> notices = _db.Notices.OrderByDescending(n => n.Id).Take(6).ToList();
            Testimonial testimonial = _db.Testimoinals.FirstOrDefault();
            AboutVM model = new AboutVM
            {
                Bio = bio,
                Teachers = teachers,
                Notices = notices,
                Testimonial = testimonial
            };
            return View(model);
        }
    }
}