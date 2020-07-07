using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.DAL;
using BackEndProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _db;
        public TeacherController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Teacher> model = _db.Teachers.Include(t => t.TeacherContact).Include(t => t.TeacherSkill).ToList();
            return View(model);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Teacher model = await _db.Teachers.Include(t => t.TeacherContact).Include(t => t.TeacherSkill).FirstOrDefaultAsync(t => t.Id == id);
            if (model == null) return NotFound();
            return View(model);
        }
        public IActionResult Search(string search)
        {
            List<Teacher> model = _db.Teachers.Where(c => c.Fullname.Contains(search)).OrderBy(c => c.Fullname).ToList();
            return PartialView("_TeacherPartialView", model);
        }
    }
}