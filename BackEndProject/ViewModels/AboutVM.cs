using BackEndProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewModels
{
	public class AboutVM
	{
		public Bio Bio { get; set; }
		public IEnumerable<Teacher> Teachers { get; set; }
		public IEnumerable<Notice> Notices { get; set; }
		public Testimonial Testimonial { get; set; }
	}
}
