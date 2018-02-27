using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sweeter.Controllers
{
    [Route("/User")]
    public class UserController : Controller
    {
      [HttpGet]
        public IActionResult GetUser(int? idUs)
        {
            int id = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            if (idUs == id) return RedirectPermanent("/MyPage");
            else return RedirectPermanent("/UserPage?id=" + idUs);
        }
    }
}
