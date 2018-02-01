using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sweeter.Controllers
{
    [Route("/")]
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public RedirectResult Index(string action)
        {
            if (action == "signin")
            {

                return RedirectPermanent("/Login");

            }

            return RedirectPermanent("/Register");



        }
    }
}
