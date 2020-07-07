using BackEndProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewModels
{
	public class BlogVM
	{
		public Blog Blog { get; set; }
		public Blog BestTheme { get; set; }
		public IEnumerable<Blog> Blogs { get; set; }
		public IEnumerable<Blog> LatestPosts { get; set; }
	}
}
