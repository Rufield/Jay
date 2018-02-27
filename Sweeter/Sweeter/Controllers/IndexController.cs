using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Sweeter.Controllers
{
    [Route("/")]
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            int id;
            if (HttpContext.User.Claims.Count() != 0)
            {
                id = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            }
            else
                id = 0;
            if (id == 0)
            {
                return View();
            }
            else return Redirect("/posts");
        }
        [HttpPost]
        public RedirectResult Index(string action)
        {
            if (action == "signin")
            {
                return RedirectPermanent("/Username");
            }

            return RedirectPermanent("/Register");

        }
    }
}
