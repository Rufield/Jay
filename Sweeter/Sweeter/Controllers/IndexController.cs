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
                return RedirectPermanent("/Username");
            }

            return RedirectPermanent("/Register");

        }
    }
}
