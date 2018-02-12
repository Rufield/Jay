using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sweeter.DataProviders;
using Sweeter.Models;
using Sweeter.Services.HashService;
using System.Linq;

namespace Sweeter.Controllers
{
    [Route("/Register")]
    public class RegisterController : Controller
    {
        private IAccountDataProvider accountDataProvider;
        private IHashService _hasher;
        private ILogger<RegisterController> _logger;

        public RegisterController(IAccountDataProvider accountData, IHashService hasher, ILogger<RegisterController> logger)
        {
            accountDataProvider = accountData;
            _hasher = hasher;
            _logger = logger;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(AccountModel account)
        {
            if (ModelState.IsValid)
            {
                if (accountDataProvider.GetAccountsByEmail(account.Email).Count() == 0)
                {
                    if (accountDataProvider.GetAccountsByUsername(account.Username).Count() == 0)
                    {
                        account.Password = _hasher.GetHashString(account.Password);
                        accountDataProvider.AddAccount(account);
                        _logger.LogInformation($"New user{account.IDuser} {account.Name} register with Email {account.Email} and username {account.Username}.");
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        SetBack();
                        ViewData["Error"] = "This Username is already in use.";
                        _logger.LogInformation($"User {account.IDuser} with Email {account.Email} want to username {account.Username}, but it's alredy in use" );
                        return View();
                    }
                }
                else
                {
                    SetBack();
                    ViewData["Error"] = "This Email is already in use.";
                    _logger.LogInformation($"User {account.IDuser} {account.Name} forgive that Email {account.Email} alredy in use");
                    return View();
                }
            }
            else
            {
                SetBack();
                ViewData["Error"] = "Some other problem has occured. Please, recheck the info.";
                _logger.LogInformation($"User {account.IDuser} {account.Name} with Email {account.Email} had some problems");
                return View();
            }

            void SetBack()
            {
                ViewData["Name"] = account.Name;
                ViewData["Username"] = account.Username;
                ViewData["Email"] = account.Email;
            }
        }
    }
}
                        //byte[] ImageData;
                        //var filepath = Path.GetTempFileName();
                        //if (avatar != null)
                        //{
                        //    using (Stream fs = avatar.OpenReadStream())
                        //    {
                        //        ImageData = new byte[fs.Length];
                        //        fs.Read(ImageData, 0, ImageData.Length);
                        //    }
                        //}
                        //else
                        //{
                            
                        //    using (FileStream FS = new FileStream("wwwroot/lib/img/Avatar.jpeg", FileMode.Open))
                        //    {
                        //        ImageData = new byte[FS.Length];
                        //        FS.Read(ImageData, 0, ImageData.Length);
                        //    }
                        //}
                        //account.Avatar = ImageData;