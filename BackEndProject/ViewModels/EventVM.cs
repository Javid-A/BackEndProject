using BackEndProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewModels
{
	public class EventVM
	{
		public Event Event { get; set; }
		public ICollection<EventSpeaker> EventSpeakers { get; set; }
		public ICollection<Event> Events { get; set; }
		public Blog BestTheme { get; set; }
		public IEnumerable<Blog> LatestPosts { get; set; }

	}
}
