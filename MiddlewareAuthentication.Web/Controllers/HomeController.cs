using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiddlewareAuthentication.Web.Models;

namespace MiddlewareAuthentication.Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]                                  //Both Administrator and User can access this
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Administrator")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "Administrator,User")]    //Both Administrator and User can access this or can write just [Authorize]   
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
