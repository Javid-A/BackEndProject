using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
	public class Slider
	{
		public int Id { get; set; }
		public string ImagePath { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		[NotMapped]
		[Required]
		public IFormFile Photo { get; set; }
	}
}
