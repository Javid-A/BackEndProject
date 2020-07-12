using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.DAL;
using BackEndProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject.Areas.AdminEduHome.Controllers
{
    [Area("AdminEduHome")]
    public class BioController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        public BioController(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            Bio bio = _db.Bios.FirstOrDefault();
            return View(bio);
        }
        public async Task<IActionResult> SaveChanges(string logoPath, string aboutPath)
        {
            //if (logoPath == "" && aboutPath == "")
            //{

            //}
            return Json(logoPath + aboutPath);
        }
    }
}