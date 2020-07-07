using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
	public class CourseFeature
	{
		public int Id { get; set; }
		[Required,StringLength(20)]
		public string Starts { get; set; }
		[Required, StringLength(20)]
		public string Duration { get; set; }
		[Required, StringLength(20)]
		public string ClassDuration { get; set; }
		[Required, StringLength(20)]
		public string SkillLevel { get; set; }
		[Required, StringLength(50)]
		public string Langugage { get; set; }
		public int Students { get; set; }
		public string Assesments  { get; set; }
		[Required]
		public double Fee { get; set; }
		public int CourseId { get; set; }
		public virtual Course Course { get; set; }
	}
}
