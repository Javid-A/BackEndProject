using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
	public class Speaker
	{
		public int Id { get; set; }
		[Required]
		public string ImagePath { get; set; }
		[Required]
		public string Fullname { get; set; }
		[Required]
		public string Profession { get; set; }
		public ICollection<EventSpeaker> EventSpeakers { get; set; }
		[NotMapped]
		[Required]
		public IFormFile Photo { get; set; }
	}
}
