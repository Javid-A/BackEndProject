using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
	public class Bio
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string LogoPath { get; set; }
		[Required]
		public string AboutPath { get; set; }
		[Required]
		public string About { get; set; }
		[Required]
		public string Video { get; set; }
	}
}
