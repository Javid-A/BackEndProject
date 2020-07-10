using BackEndProject.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewModels
{
	public class EventVM
	{
		public Event Event { get; set; }
		public Speaker Speaker { get; set; }
		public IEnumerable<Speaker> Speakers { get; set; }
		public ICollection<EventSpeaker> EventSpeakers { get; set; }
		public ICollection<Event> Events { get; set; }
		public Blog BestTheme { get; set; }
		public IEnumerable<Blog> LatestPosts { get; set; }
		public string Name { get; set; }
		[Required, EmailAddress, DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		public string Subject { get; set; }
		[Required]
		public string Message { get; set; }
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
		[NotMapped]
		[Required]
		public IFormFile Photo { get; set; }
	}
}
