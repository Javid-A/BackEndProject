using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
	public class TeacherContact
	{
		public int Id { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		public string Skype { get; set; }
		public string Facebook { get; set; }
		public string Pinterest { get; set; }
		public string Twitter { get; set; }
		public int TeacherId { get; set; }
		public virtual Teacher Teacher { get; set; }
	}
}
