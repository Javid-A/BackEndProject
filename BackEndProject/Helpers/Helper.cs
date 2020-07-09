using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Helpers
{
	public class Helper
	{
        public static void DeleteImg(string root, string folder, string folder2, string image)
        {
            string filePath = Path.Combine(root, folder, folder2, image);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
