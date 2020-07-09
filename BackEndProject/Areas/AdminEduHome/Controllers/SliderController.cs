using System;
using System.Collections.Generic;
using System.IO;
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
	public class SliderController : Controller
	{
		private readonly AppDbContext _db;
		private readonly IHostingEnvironment _env;
		public SliderController(AppDbContext db, IHostingEnvironment env)
		{
			_db = db;
			_env = env;
		}
		public IActionResult Index()
		{
			return View(_db.Sliders);
		}
		public IActionResult Create()
		{
            ViewBag.SliderCount = _db.Sliders.Count();
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Slider slide)
		{
			ViewBag.SliderCount = _db.Sliders.Count();
			if (slide.Photo == null)
			{
				return View();
			}
			if (!slide.Photo.IsImage())
			{
				ModelState.AddModelError("", "You can choose only image file");
				return View();
			}
			if (!slide.Photo.MaxLength(1500))
			{
				ModelState.AddModelError("", "Image must be maximum 1,5 MB");
				return View();
			}
			Slider newSlide = new Slider
			{
				Title = slide.Title,
				Description = slide.Description
			};
			newSlide.ImagePath = await slide.Photo.SaveImg(_env.WebRootPath, "img", "slider");
			_db.Sliders.Add(newSlide);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Detail(int? id)
		{
			if (id == null) return NotFound();
			Slider slide = await _db.Sliders.FindAsync(id);
			if (slide == null) return NotFound();
			return View(slide);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null) return NotFound();
			Slider slide = await _db.Sliders.FindAsync(id);
			if (slide == null) return NotFound();
			return View(slide);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int? id, Slider editedSlide)
		{
			if (editedSlide.Photo == null)
			{
				Slider slide = await _db.Sliders.FindAsync(id);
				return View(slide);
			}
			else
			{
				Slider slide = await _db.Sliders.FindAsync(id);
				if (!editedSlide.Photo.IsImage())
				{
					ModelState.AddModelError("", "You can choose only image file");
					return View(slide);
				}
				if (!editedSlide.Photo.MaxLength(1500))
				{
					ModelState.AddModelError("", "Image must be maximum 1,5 MB");
					return View(slide);
				}
				if (editedSlide.Photo.FileName == slide.ImagePath)
				{
					ModelState.AddModelError("", "You can't change current image with same image name");
					return View(slide);
				}
				Helpers.Helper.DeleteImg(_env.WebRootPath, "img", "slider", slide.ImagePath);
				slide.ImagePath = await editedSlide.Photo.SaveImg(_env.WebRootPath, "img", "slider");
				slide.Title = editedSlide.Title;
				slide.Description = editedSlide.Description;
				await _db.SaveChangesAsync();
				return RedirectToAction("Index");
			}

		}
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) return NotFound();
			Slider slide = await _db.Sliders.FindAsync(id);
			if (slide == null) return NotFound();
			return View(slide);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("Delete")]
		public async Task<IActionResult> DeletePost(int? id)
		{
			if (id == null) return NotFound();
			Slider slide = await _db.Sliders.FindAsync(id);
			if (slide == null) return NotFound();

			Helpers.Helper.DeleteImg(_env.WebRootPath, "img", "slider", slide.ImagePath);
			_db.Sliders.Remove(slide);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}