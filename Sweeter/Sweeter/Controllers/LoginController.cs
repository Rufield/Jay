using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sweeter.DataProviders;
using Sweeter.Models;
using Sweeter.Services.HashService;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sweeter.Controllers
{
    [Route("/Username")]
    public class LoginController : Controller
    {
        private readonly ILogger _logger;
        private IAccountDataProvider accountDataProvider;
        private IHashService _hasher;

        public LoginController(IAccountDataProvider accountData, IHashService hasher, ILogger<LoginController> logger)
        {
            this.accountDataProvider = accountData;
            this._hasher = hasher;
            this._logger = logger;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var user = await AuthenticateUser(email, password);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return RedirectToAction("Index", "Index");
                }

                AccountModel account = accountDataProvider.GetAccountByEmail(user.Email);
                var claims = new List<Claim>
                {
                    new Claim("Current",account.IDuser.ToString())
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


                _logger.LogInformation($"User {account.IDuser} successful login with Email {account.Email}");
                return RedirectToAction("Index", "Posts");
            }
            _logger.LogInformation($"Model is not valid");
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
                if (accs.First().Password.Equals(_hasher.GetHashString(password)))
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
