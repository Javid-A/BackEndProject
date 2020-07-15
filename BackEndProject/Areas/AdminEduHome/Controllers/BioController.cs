using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.DAL;
using BackEndProject.Extentions;
using BackEndProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject.Areas.AdminEduHome.Controllers
{
    [Area("AdminEduHome")]
    public class BioController : Controller
    {
        private readonly AppDbContext _db;
        public BioController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            Bio bio = _db.Bios.FirstOrDefault();
            return View(bio);
        }
        [HttpPost]
        public async Task<IActionResult> SaveChanges(string[] data)
        {
            Bio bio = _db.Bios.FirstOrDefault();
            bio.About = data[0];
            bio.Video = data[1];
            bio.PhoneNumber = data[2];
            bio.PhoneNumber2 = data[3];
            bio.Address = data[4];
            bio.Email = data[5];
            bio.Link = data[6];
            await _db.SaveChangesAsync();
            return PartialView("_AdminBioPartialView", bio);
        }
    }
}