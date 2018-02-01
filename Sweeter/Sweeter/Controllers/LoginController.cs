using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sweeter.DataProviders;
using Sweeter.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sweeter.Controllers
{
    [Route("/Login")]
    public class LoginController : Controller
    {
        private IAccountDataProvider accountDataProvider;
        public LoginController(IAccountDataProvider accountData)
        {
            this.accountDataProvider = accountData;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
       
       
        /*
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            AccountModel account = accountDataProvider.GetAccountByEmail(email);
            if (account != null)
            {
                if (account.Password == password)
                {
                    return View("All is good");
                }
                return View("Incorrect password");
            }
            return View("There's no such account");
        }
        */
    }
}
