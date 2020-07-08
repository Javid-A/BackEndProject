using BackEndProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewModels
{
	public class ContactVM
	{
		public Bio Bio { get; set; }
		public string Name { get; set; }
		[Required,EmailAddress,DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		public string Subject { get; set; }
		[Required]
		public string Message { get; set; }
	}
}
