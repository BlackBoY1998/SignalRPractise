using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAdminLTE.Models;
using TestAdminLTE.MyClass;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace TestAdminLTE.Controllers
{
    
    public class HomeController : Controller
    {
        private ConDB conDB = new ConDB();
        private IHostingEnvironment hostingENv;
        public HomeController(IHostingEnvironment env)
        {
            this.hostingENv = env;
        }

        public IActionResult Index()
        {
            if (ChkLogin() == true)
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }
    
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username,string password)
        {
            DataTable dt = conDB.GetData($"SELECT * FROM `user` WHERE username = '{username}';");
            if (dt.Rows.Count > 0)
            {
                if(dt.Rows[0]["password"].ToString() == EncodeString.MD5HashCryptography(password))
                {
                    HttpContext.Session.SetString("login", "1");
                    TempData["userFullname"] = dt.Rows[0]["fname"].ToString() + " " + dt.Rows[0]["lname"].ToString();
                    return View("Index");
                }
            }
            return View();
        }

        private bool ChkLogin()
        {
            bool result = false;
            if(HttpContext.Session.GetString("login") != null)
            {
                if (HttpContext.Session.GetString("login") == "1")
                {
                    result = true;
                }
            }

            return result;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }

        public IActionResult User(string username)
        {
            DataTable dt = conDB.GetData($"SELECT * FROM `user` where username = '{username}';");

            ViewData["user"] = dt;
            ViewBag.user = dt;

            return View(dt);
        }

        public IActionResult UploadFiles()
        {
            return View();
        }

        [HttpPost]
        public void UploadFiles(List<IFormFile> files)
        {
            if (files.Count > 0)
            {
                string folder = "/files";
                string filePath = hostingENv.WebRootPath + folder;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                foreach(var f in files)
                {
                    using(var stream = new FileStream(filePath + "/" + Path.GetFileName(f.FileName), FileMode.Create))
                    {
                        f.CopyTo(stream);
                    }
                }
            }
        }
    }
}
