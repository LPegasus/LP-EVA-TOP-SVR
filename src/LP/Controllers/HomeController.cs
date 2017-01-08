using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LP.Helpers;
using LP.Common.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace LP.Controllers {
    public class HomeController : Common.LPController {
        [NoKeepAlive]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        //[Authorize("Role")]
        public IActionResult TimeoutTest()
        {
            System.Threading.Thread.Sleep(5000);
            return AjaxHelper.Success();
        }
    }
}
