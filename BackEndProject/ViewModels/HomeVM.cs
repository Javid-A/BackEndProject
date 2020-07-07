using BackEndProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewModels
{
	public class HomeVM
	{
		public Bio Bio { get; set; }
		public IEnumerable<Slider> Sliders { get; set; }
		public IEnumerable<Course> Courses { get; set; }
		public IEnumerable<Event> Events { get; set; }
		public IEnumerable<Blog> Blogs { get; set; }

	}
}
