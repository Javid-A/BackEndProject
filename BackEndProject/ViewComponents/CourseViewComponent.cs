using BackEndProject.DAL;
using BackEndProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewComponents
{
	public class CourseViewComponent:ViewComponent
	{
		private readonly AppDbContext _db;
		public CourseViewComponent(AppDbContext db)
		{
			_db = db;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			List<Course> model = _db.Courses.Include(course => course.CourseContent).Include(course => course.CourseFeature).ToList();
			return View(await Task.FromResult(model));
		}
	}
}
