using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
	public class Notice
	{
		public int Id { get; set; }
		[Required]
		public string Note { get; set; }
		[Required]
		public string Date { get; set; }
	}
}
