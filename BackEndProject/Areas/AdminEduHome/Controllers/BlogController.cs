using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.DAL;
using BackEndProject.Extentions;
using BackEndProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject.Areas.AdminEduHome.Controllers
{
    [Area("AdminEduHome")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
		private readonly UserManager<AppUser> _userManager;
        private readonly IHostingEnvironment _env;
        public BlogController(AppDbContext db, IHostingEnvironment env, UserManager<AppUser> userManager)
        {
            _db = db;
            _env = env;
			_userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_db.Blogs);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Blog blog = await _db.Blogs.Include(b=>b.AppUser).FirstOrDefaultAsync(c => c.Id == id);
            if (blog == null) return NotFound();
            return View(blog);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            Blog blog = await _db.Blogs.Include(b => b.AppUser).FirstOrDefaultAsync(c => c.Id == id);
            if (blog == null) return NotFound();

            return View(blog);
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int? id, Blog editedBlog)
		{
			if (id == null) return NotFound();
			Blog blog = await _db.Blogs.Include(b => b.AppUser).FirstOrDefaultAsync(c => c.Id == id);
			if (blog == null) return NotFound();
			if (!editedBlog.Photo.IsImage())
			{
				ModelState.AddModelError("", "You can choose only image file");
				return View(blog);
			}
			if (!editedBlog.Photo.MaxLength(1500))
			{
				ModelState.AddModelError("", "Image must be maximum 1,5 MB");
				return View(blog);
			}
			if (editedBlog.Photo.FileName == blog.ImagePath)
			{
				ModelState.AddModelError("", "You can't change current image with same image name");
				return View(blog);
			}
			Helpers.Helper.DeleteImg(_env.WebRootPath, "img", "blog", blog.ImagePath);
			blog.ImagePath = await editedBlog.Photo.SaveImg(_env.WebRootPath, "img", "blog");
			blog.Title = editedBlog.Title;
			blog.Description = editedBlog.Description;
			blog.Date = editedBlog.Date;
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Blog blog)
		{
			if (!blog.Photo.IsImage())
			{
				ModelState.AddModelError("", "You can choose only image file");
				return View();
			}
			if (!blog.Photo.MaxLength(1500))
			{
				ModelState.AddModelError("", "Image must be maximum 1,5 MB");
				return View();
			}
			AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
			Blog newBlog = new Blog
			{
				Title = blog.Title,
				Description = blog.Description,
				Date = blog.Date,
				AppUserId=user.Id
			};
			newBlog.ImagePath = await blog.Photo.SaveImg(_env.WebRootPath, "img", "blog");
			await _db.Blogs.AddAsync(newBlog);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) return NotFound();
			Blog blog = await _db.Blogs.Include(b => b.AppUser).FirstOrDefaultAsync(c => c.Id == id);
			if (blog == null) return NotFound();
			return View(blog);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("Delete")]
		public async Task<IActionResult> DeletePost(int? id)
		{
			if (id == null) return NotFound();
			Blog blog = await _db.Blogs.Include(b => b.AppUser).FirstOrDefaultAsync(c => c.Id == id);
			if (blog == null) return NotFound();

			Helpers.Helper.DeleteImg(_env.WebRootPath, "img", "event", blog.ImagePath);
			_db.Blogs.Remove(blog);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

	}
}