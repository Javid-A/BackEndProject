using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.DAL;
using BackEndProject.Extentions;
using BackEndProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject.Areas.AdminEduHome.Controllers
{
    [Area("AdminEduHome")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _db;
		private readonly IHostingEnvironment _env;
        public CourseController(AppDbContext db,IHostingEnvironment env)
        {
            _db = db;
			_env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Courses);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Course course = await _db.Courses.Include(c=>c.CourseContent).Include(c=>c.CourseFeature).FirstOrDefaultAsync(c=>c.Id==id);
            if (course == null) return NotFound();
            return View(course);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            Course course = await _db.Courses.Include(c => c.CourseContent).Include(c => c.CourseFeature).FirstOrDefaultAsync(c => c.Id == id);
            if (course == null) return NotFound();
            return View(course);
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int? id, Course editedCourse)
		{
			if (editedCourse.Photo == null)
			{
				Course course = await _db.Courses.Include(c => c.CourseContent).Include(c => c.CourseFeature).FirstOrDefaultAsync(c => c.Id == id);
				return View(course);
			}
			else
			{
				Course course = await _db.Courses.Include(c => c.CourseContent).Include(c => c.CourseFeature).FirstOrDefaultAsync(c => c.Id == id);
				if (!editedCourse.Photo.IsImage())
				{
					ModelState.AddModelError("", "You can choose only image file");
					return View(course);
				}
				if (!editedCourse.Photo.MaxLength(1500))
				{
					ModelState.AddModelError("", "Image must be maximum 1,5 MB");
					return View(course);
				}
				if (editedCourse.Photo.FileName == course.ImagePath)
				{
					ModelState.AddModelError("", "You can't change current image with same image name");
					return View(course);
				}
				if (editedCourse.CourseFeature.Fee.ToString()=="0")
				{
					ModelState.AddModelError("", "Make sure course fee is correct");
					return View(course);
				}
				Helpers.Helper.DeleteImg(_env.WebRootPath, "img", "course", course.ImagePath);
				course.ImagePath = await editedCourse.Photo.SaveImg(_env.WebRootPath, "img", "course");
				course.Name = editedCourse.Name;
				course.Description = editedCourse.Description;
				course.CourseContent.AboutCourse = editedCourse.CourseContent.AboutCourse;
				course.CourseContent.HTA = editedCourse.CourseContent.HTA;
				course.CourseContent.Certification = editedCourse.CourseContent.Certification;
				course.CourseFeature.Starts = editedCourse.CourseFeature.Starts;
				course.CourseFeature.Duration = editedCourse.CourseFeature.Duration;
				course.CourseFeature.ClassDuration = editedCourse.CourseFeature.ClassDuration;
				course.CourseFeature.SkillLevel = editedCourse.CourseFeature.SkillLevel;
				course.CourseFeature.Langugage = editedCourse.CourseFeature.Langugage;
				course.CourseFeature.Students = editedCourse.CourseFeature.Students;
				course.CourseFeature.Assesments = editedCourse.CourseFeature.Assesments;
				course.CourseFeature.Fee = editedCourse.CourseFeature.Fee;
				await _db.SaveChangesAsync();
				return RedirectToAction("Index");
			}

		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Course course)
		{
			if (course.Photo == null)
			{
				return View();
			}
			if (!course.Photo.IsImage())
			{
				ModelState.AddModelError("", "You can choose only image file");
				return View();
			}
			if (!course.Photo.MaxLength(1500))
			{
				ModelState.AddModelError("", "Image must be maximum 1,5 MB");
				return View();
			}
			if (course.CourseFeature.Fee.ToString() == "0")
			{
				ModelState.AddModelError("", "Make sure course fee is correct");
				return View(course);
			}
			Course newCourse = new Course
			{
				Name = course.Name,
				Description = course.Description
			};
			newCourse.ImagePath = await course.Photo.SaveImg(_env.WebRootPath, "img", "course");
			CourseContent courseContent = new CourseContent
			{
				AboutCourse = course.CourseContent.AboutCourse,
				HTA = course.CourseContent.HTA,
				Certification = course.CourseContent.Certification,
				CourseId = newCourse.Id
			};
			CourseFeature courseFeature = new CourseFeature
			{
				Starts = course.CourseFeature.Starts,
				Duration = course.CourseFeature.Duration,
				ClassDuration = course.CourseFeature.ClassDuration,
				SkillLevel = course.CourseFeature.SkillLevel,
				Langugage = course.CourseFeature.Langugage,
				Students = course.CourseFeature.Students,
				Assesments = course.CourseFeature.Assesments,
				Fee = course.CourseFeature.Fee,
				CourseId = newCourse.Id
			};
			newCourse.CourseContent = courseContent;
			newCourse.CourseFeature = courseFeature;
			await _db.Courses.AddAsync(newCourse);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) return NotFound();
			Course course = await _db.Courses.Include(c => c.CourseContent).Include(c => c.CourseFeature).FirstOrDefaultAsync(c => c.Id == id);
			if (course == null) return NotFound();
			return View(course);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("Delete")]
		public async Task<IActionResult> DeletePost(int? id)
		{
			if (id == null) return NotFound();
			Course course = await _db.Courses.Include(c => c.CourseContent).Include(c => c.CourseFeature).FirstOrDefaultAsync(c => c.Id == id);
			if (course == null) return NotFound();

			Helpers.Helper.DeleteImg(_env.WebRootPath, "img", "course", course.ImagePath);
			_db.Courses.Remove(course);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}