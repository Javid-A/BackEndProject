using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
	public class CourseContent
	{
		public int Id { get; set; }
		[Required]
		public string AboutCourse { get; set; }
		public string HTA { get; set; }
		public string Certification { get; set; }
		public int CourseId { get; set; }
		public virtual Course Course { get; set; }
	}
}
