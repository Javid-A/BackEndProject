using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject.Areas.AdminEduHome.Controllers
{
    public class DashboardController : Controller
    {
        [Area("AdminEduHome")]
        public IActionResult Index()
        {
            return View();
        }
    }
}