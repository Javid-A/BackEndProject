using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.DAL;
using BackEndProject.Extentions;
using BackEndProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject.Areas.AdminEduHome.Controllers
{
    [Area("AdminEduHome")]
    public class TestimonialController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        public TestimonialController(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Testimoinals.FirstOrDefault());
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            Testimonial testimonial = _db.Testimoinals.FirstOrDefault(t=>t.Id==id);
            if (testimonial == null) return NotFound();
            return View(testimonial);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,Testimonial editedTestimonial)
        {
            Testimonial testimonial = _db.Testimoinals.FirstOrDefault(t => t.Id == id);
            Helpers.Helper.DeleteImg(_env.WebRootPath, "img", "testimonial", testimonial.StudentImage);
            testimonial.StudentImage = await editedTestimonial.Photo.SaveImg(_env.WebRootPath,"img","testimonial");
            testimonial.StudentFullname = editedTestimonial.StudentFullname;
            testimonial.Degree = editedTestimonial.Degree;
            testimonial.AboutStudent = editedTestimonial.AboutStudent;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}