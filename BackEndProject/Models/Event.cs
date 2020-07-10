using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
	public class Event
	{
		public int Id { get; set; }
		[Required]
		public string ImagePath { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string Date { get; set; }
		[Required]
		public string Time { get; set; }
		[Required]
		public string Venue { get; set; }
		public ICollection<EventSpeaker> EventSpeakers { get; set; }
		[NotMapped]
		[Required]
		public IFormFile Photo { get; set; }
	}
}
