using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.DAL;
using BackEndProject.Extentions;
using BackEndProject.Models;
using BackEndProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject.Areas.AdminEduHome.Controllers
{
	[Area("AdminEduHome")]
	public class CourseController : Controller
	{
		private readonly AppDbContext _db;
		private readonly IHostingEnvironment _env;
		private readonly UserManager<AppUser> _userManager;
		public CourseController(AppDbContext db, IHostingEnvironment env, UserManager<AppUser> userManager)
		{
			_db = db;
			_env = env;
			_userManager = userManager;
		}
		[Authorize(Policy = "CourseManager")]
		public async Task<IActionResult> Index()
		{
			AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
			CourseVM model = new CourseVM
			{
				OwnerCourses = _db.Courses.Where(c => c.AppUserId == user.Id).ToList(),
				Courses = _db.Courses.ToList()
			};
			return View(model);
		}
		[Authorize(Policy = "CourseManager")]
		public async Task<IActionResult> Detail(int? id)
		{
			AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
			if (User.IsInRole(Helpers.Helper.Roles.Admin.ToString()))
			{
				if (id == null) return NotFound();
				Course course = await _db.Courses.Include(c => c.CourseContent).Include(c => c.CourseFeature).FirstOrDefaultAsync(c => c.Id == id);
				if (course == null) return NotFound();
				return View(course);
			}
			else
			{
				if (id == null) return NotFound();
				List<Course> courses =_db.Courses.Include(c => c.CourseContent).Include(c => c.CourseFeature).Where(c => c.AppUserId == user.Id).ToList();
				Course ownerCourse = courses.FirstOrDefault(c => c.Id == id);
				if (ownerCourse == null || ownerCourse.Id != id)
				{
					return RedirectToAction("AccessDenied", "Account", new { area = "" });
				}
				else{
					return View(ownerCourse);
				}

			}
		}

		[Authorize(Policy = "CourseManager")]
		public async Task<IActionResult> Edit(int? id)
		{
			AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
			if (User.IsInRole(Helpers.Helper.Roles.Admin.ToString()))
			{
				if (id == null) return NotFound();
				Course course = await _db.Courses.Include(c => c.CourseContent).Include(c => c.CourseFeature).FirstOrDefaultAsync(c => c.Id == id);
				if (course == null) return NotFound();
				return View(course);
			}
			else
			{
				if (id == null) return NotFound();
				List<Course> courses = _db.Courses.Include(c => c.CourseContent).Include(c => c.CourseFeature).Where(c => c.AppUserId == user.Id).ToList();
				Course ownerCourse = courses.FirstOrDefault(c => c.Id == id);
				if (ownerCourse == null || ownerCourse.Id != id)
				{
					return RedirectToAction("AccessDenied", "Account", new { area = "" });
				}
				else
				{
					return View(ownerCourse);
				}

			}
		}
		[Authorize(Policy = "CourseManager")]
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
				if (editedCourse.CourseFeature.Fee.ToString() == "0")
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
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create()
		{
			ViewBag.CourseOwners = await _userManager.GetUsersInRoleAsync(Helpers.Helper.Roles.CourseOwner.ToString());
			return View();
		}
		[Authorize(Roles = "Admin")]
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
			string ownerId = Request.Form["courseOwner"];
			Course newCourse = new Course
			{
				Name = course.Name,
				Description = course.Description,
				AppUserId = ownerId
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

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) return NotFound();
			Course course = await _db.Courses.Include(c => c.CourseContent).Include(c => c.CourseFeature).FirstOrDefaultAsync(c => c.Id == id);
			if (course == null) return NotFound();
			return View(course);
		}
		[Authorize(Roles = "Admin")]
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