using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
	public class Testimonial
	{
		public int Id { get; set; }
		[Required]
		public string StudentImage { get; set; }
		[Required]
		public string AboutStudent { get; set; }
		[Required]
		public string StudentFullname { get; set; }
		[Required]
		public string Degree { get; set; }
	}
}
