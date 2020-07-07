using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
	public class AppUser:IdentityUser
	{
		public bool IsDeleted { get; set; } = false;
		public ICollection<Blog> Blogs { get; set; }
	}
}
