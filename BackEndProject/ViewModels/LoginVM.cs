using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewModels
{
	public class LoginVM
	{
		[Required, StringLength(30)]
		public string Username { get; set; }
		[Required, DataType(DataType.Password)]
		public string Password { get; set; }
		public bool IsChecked { get; set; }
	}
}
