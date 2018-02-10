using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sweeter.DataProviders;
using Sweeter.Models;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sweeter.Controllers
{
    
    [Route("/Username")]
    public class LoginController : Controller
    {

        private readonly ILogger _logger;



        private IAccountDataProvider accountDataProvider;
        public LoginController(IAccountDataProvider accountData, ILogger<LoginController> logger)
        {
            this.accountDataProvider = accountData;
            _logger = logger;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        string GetHashString(string s)
        {

            byte[] bytes = Encoding.Unicode.GetBytes(s);


            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();


            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;


            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }

       
      
        
        [HttpPost]
        public async Task<IActionResult> OnPostAsync(string email, string password)
        {
           

            if (ModelState.IsValid)
            {
                

                var user = await AuthenticateUser(email,password);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return RedirectToAction("Index","Index");
                }

                
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email)
                    
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                

                _logger.LogInformation($"User {user.Email} logged in at {DateTime.UtcNow}.");

                return RedirectToAction("Index", "Posts");
            }

            // Something failed. Redisplay the form.
            return RedirectToAction("Index", "Login");
        }

        private async Task<AccountModel> AuthenticateUser(string email, string password)
        {
            // For demonstration purposes, authenticate a user
            // with a static email address. Ignore the password.
            // Assume that checking the database takes 500ms
            
            await Task.Delay(500);
            IEnumerable<AccountModel> accs = accountDataProvider.GetAccountsByEmail(email);

            if (accs.Count() != 0)
            {
                
                    if (accs.First().Password.Equals(GetHashString(password)))
                    {
                    return new AccountModel()
                    {
                        Email = email

                        };

                    }
                return null;
                    
                


               

            }
            return null;
         
        }

       
        
    }
}
