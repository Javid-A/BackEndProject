using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
	public class Blog
	{
		public int Id { get; set; }
		[Required]
		public string ImagePath { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Date { get; set; }
		public int CommentCount { get; set; } = 0;
		[Required]
		public string Description { get; set; }
		public string AppUserId { get; set; }
		public AppUser AppUser { get; set; }
		[NotMapped]
		[Required]
		public IFormFile Photo { get; set; }
	}
}
