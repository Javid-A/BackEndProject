using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
	public class Teacher
	{
		public int Id { get; set; }
		[Required,StringLength(50)]
		public string Fullname { get; set; }
		[Required]
		public string ImagePath { get; set; }
		public bool IsDeleted { get; set; } = false;
		[Required,StringLength(50)]
		public string Profession { get; set; }
		[Required]
		public string About { get; set; }
		public string Degree { get; set; }
		public string Experience { get; set; }
		public string Hobbies { get; set; }
		public string Faculty { get; set; }
		public virtual TeacherContact TeacherContact { get; set; }
		public virtual TeacherSkill TeacherSkill { get; set; }
		[NotMapped]
		[Required]
		public IFormFile Photo { get; set; }
	}
}
