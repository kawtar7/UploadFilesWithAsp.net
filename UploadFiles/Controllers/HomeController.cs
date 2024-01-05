using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UploadFiles.Models;

namespace UploadFiles.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHost;
        public HomeController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            string uploadfolder = Path.Combine(_webHost.WebRootPath,"uploads");
            if(!Directory.Exists(uploadfolder))
            {
                Directory.CreateDirectory(uploadfolder);
            }
            foreach(var file in files)
            {
                string fileName = Path.GetFileName(file.FileName);
                string fileSavePath = Path.Combine(uploadfolder, fileName);
                using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                ViewBag.Message += string.Format("<br>{0}</br> uploaded successefully . <br/>",fileName) ;
            }
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}