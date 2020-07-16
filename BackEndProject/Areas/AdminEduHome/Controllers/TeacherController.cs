using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.DAL;
using BackEndProject.Extentions;
using BackEndProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject.Areas.AdminEduHome.Controllers
{
    [Area("AdminEduHome")]
	[Authorize(Roles = "Admin")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        public TeacherController(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Teachers);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Teacher teacher = await _db.Teachers.Include(t => t.TeacherContact).Include(t => t.TeacherSkill).FirstOrDefaultAsync(c => c.Id == id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            Teacher teacher = await _db.Teachers.Include(t => t.TeacherContact).Include(t => t.TeacherSkill).FirstOrDefaultAsync(c => c.Id == id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,Teacher editedTeacher)
        {
            if (id == null) return NotFound();
            Teacher teacher = await _db.Teachers.Include(t => t.TeacherContact).Include(t => t.TeacherSkill).FirstOrDefaultAsync(c => c.Id == id);
            if (teacher == null) return NotFound();
            if (!editedTeacher.Photo.IsImage())
            {
                ModelState.AddModelError("", "You can choose only image file");
                return View(teacher);
            }
            if (!editedTeacher.Photo.MaxLength(1500))
            {
                ModelState.AddModelError("", "Image must be maximum 1,5 MB");
                return View(teacher);
            }
            if (editedTeacher.Photo.FileName == teacher.ImagePath)
            {
                ModelState.AddModelError("", "You can't change current image with same image name");
                return View(teacher);
            }
            Helpers.Helper.DeleteImg(_env.WebRootPath, "img", "teacher", teacher.ImagePath);
            teacher.ImagePath = await editedTeacher.Photo.SaveImg(_env.WebRootPath, "img", "teacher");
            teacher.Fullname = editedTeacher.Fullname;
            teacher.Profession = editedTeacher.Profession;
            teacher.About = editedTeacher.About;
            teacher.Degree = editedTeacher.Degree;
            teacher.Experience = editedTeacher.Experience;
            teacher.Hobbies = editedTeacher.Hobbies;
            teacher.Faculty = editedTeacher.Faculty;
            teacher.TeacherContact.Email = editedTeacher.TeacherContact.Email;
            teacher.TeacherContact.PhoneNumber = editedTeacher.TeacherContact.PhoneNumber;
            teacher.TeacherContact.Skype = editedTeacher.TeacherContact.Skype;
            teacher.TeacherContact.Facebook = editedTeacher.TeacherContact.Facebook;
            teacher.TeacherContact.Pinterest = editedTeacher.TeacherContact.Pinterest;
            teacher.TeacherContact.Twitter = editedTeacher.TeacherContact.Twitter;
            teacher.TeacherSkill.Language = editedTeacher.TeacherSkill.Language;
            teacher.TeacherSkill.TeamLeader = editedTeacher.TeacherSkill.TeamLeader;
            teacher.TeacherSkill.Development = editedTeacher.TeacherSkill.Development;
            teacher.TeacherSkill.Design = editedTeacher.TeacherSkill.Design;
            teacher.TeacherSkill.Communication = editedTeacher.TeacherSkill.Communication;
            teacher.TeacherSkill.Innovation = editedTeacher.TeacherSkill.Innovation;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Teacher teacher)
        {
            if (!teacher.Photo.IsImage())
            {
                ModelState.AddModelError("", "You can choose only image file");
                return View();
            }
            if (!teacher.Photo.MaxLength(1500))
            {
                ModelState.AddModelError("", "Image must be maximum 1,5 MB");
                return View();
            }
            Teacher newTeacher = new Teacher
            {
                Fullname = teacher.Fullname,
                Profession = teacher.Profession,
                About = teacher.About,
                Degree = teacher.Degree,
                Experience = teacher.Experience,
                Hobbies = teacher.Hobbies,
                Faculty = teacher.Faculty
            };
            newTeacher.ImagePath = await teacher.Photo.SaveImg(_env.WebRootPath, "img", "teacher");
            TeacherContact teacherContact = new TeacherContact
            {
                Email = teacher.TeacherContact.Email,
                PhoneNumber = teacher.TeacherContact.PhoneNumber,
                Skype = teacher.TeacherContact.Skype,
                Facebook = teacher.TeacherContact.Facebook,
                Pinterest = teacher.TeacherContact.Pinterest,
                Twitter = teacher.TeacherContact.Twitter,
            };
            TeacherSkill teacherSkill = new TeacherSkill
            {
                Language=teacher.TeacherSkill.Language,
                TeamLeader=teacher.TeacherSkill.TeamLeader,
                Development=teacher.TeacherSkill.Development,
                Design=teacher.TeacherSkill.Design,
                Communication=teacher.TeacherSkill.Communication,
                Innovation=teacher.TeacherSkill.Innovation,
            };
            newTeacher.TeacherContact = teacherContact;
            newTeacher.TeacherSkill = teacherSkill;
            await _db.Teachers.AddAsync(newTeacher);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Teacher teacher = await _db.Teachers.Include(t => t.TeacherContact).Include(t => t.TeacherSkill).FirstOrDefaultAsync(c => c.Id == id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Teacher teacher = await _db.Teachers.Include(t => t.TeacherContact).Include(t => t.TeacherSkill).FirstOrDefaultAsync(c => c.Id == id);
            if (teacher == null) return NotFound();
            Helpers.Helper.DeleteImg(_env.WebRootPath, "img", "teacher", teacher.ImagePath);
            _db.Teachers.Remove(teacher);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}