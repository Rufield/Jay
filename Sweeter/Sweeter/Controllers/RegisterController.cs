using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sweeter.DataProviders;
using Sweeter.Models;
using Sweeter.Services.HashService;
using System.IO;
using System.Linq;

namespace Sweeter.Controllers
{

    [Route("/Register")]
    public class RegisterController : Controller
    {
        private IAccountDataProvider accountDataProvider;
        private IHashService _hasher;

        public RegisterController(IAccountDataProvider accountData, IHashService hasher)
        {
            accountDataProvider = accountData;
            _hasher = hasher;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(AccountModel account, IFormFile avatar)
        {
            if (ModelState.IsValid)
            {
                if (accountDataProvider.GetAccountsByEmail(account.Email).Count() == 0)
                {
                    if (accountDataProvider.GetAccountsByUsername(account.Username).Count() == 0)
                    {
                        byte[] ImageData;
                        var filepath = Path.GetTempFileName();
                        if (avatar != null)
                        {
                            using (Stream fs = avatar.OpenReadStream())
                            {
                                ImageData = new byte[fs.Length];
                                fs.Read(ImageData, 0, ImageData.Length);
                            }
                        }
                        else
                        {
                            
                            using (FileStream FS = new FileStream("wwwroot/lib/img/Avatar.jpeg", FileMode.Open))
                            {
                                ImageData = new byte[FS.Length];
                                FS.Read(ImageData, 0, ImageData.Length);
                            }
                        }
                        account.Avatar = ImageData;
                        account.Password = _hasher.GetHashString(account.Password);
                        accountDataProvider.AddAccount(account);
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        SetBack();
                        ViewData["Error"] = "This Username is already in use.";
                        return View();
                    }
                }
                else
                {
                    SetBack();
                    ViewData["Error"] = "This Email is already in use.";
                    return View();
                }
            }
            else return View();
            void SetBack()
            {
                ViewData["Name"] = account.Name;
                ViewData["Username"] = account.Username;
                ViewData["Email"] = account.Email;
            }
        }
    }
}