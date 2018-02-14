using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sweeter.DataProviders;
using Sweeter.Models;
using System.IO;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sweeter.Controllers
{
    [Route("/Profile")]
    public class ProfileController : Controller
    {
        private IAccountDataProvider accountDataProvider;
        private ILogger<ProfileController> _logger;

        public ProfileController(IAccountDataProvider accountData, ILogger<ProfileController> logger)
        {
            accountDataProvider = accountData;
            _logger = logger;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            int id = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            if (id != 0)
            {
                AccountModel account = accountDataProvider.GetAccount(id);
                if (account.Avatar != null)
                {
                    byte[] ImageData = account.Avatar;
                    string path = "wwwroot/ForPics/av" + account.IDuser.ToString() + ".jpeg";
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        fs.Write(ImageData, 0, ImageData.Length);
                    }
                    ViewData["Pic"] = path.Substring(7);
                    return View(account);
                }
                else
                {

                    using (FileStream FS = new FileStream("wwwroot/lib/img/Avatar.jpeg", FileMode.Open))
                    {
                      //  ImageData = new byte[FS.Length];
                     //   FS.Read(ImageData, 0, ImageData.Length);
                    }
                    _logger.LogInformation($"User {account.IDuser} has successful download the Profile page");
                    return View(account);
                }
            }
            else
            {
                _logger.LogInformation($"");
                return RedirectPermanent("/Username");
            }
            }

        [HttpPost]
        public IActionResult Index(AccountModel account, IFormFile avatar)
        {
            if (Valid(account))
            {
                int id = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
                if (id != 0)
                {
                    AccountModel Oldaccount = accountDataProvider.GetAccount(id);
                    if (account.Email != Oldaccount.Email)
                    {
                        _logger.LogInformation($"User Email {account.Email} != {Oldaccount.Email}");
                        return RedirectToAction("Password", new { account });
                    }
                    else
                    {
                        _logger.LogInformation($"Maybe {account.IDuser} user's Email {account.Email} = {Oldaccount.Email}");
                        return CheckUsername(Oldaccount, account, avatar);
                    }
                }
                else
                {
                    _logger.LogInformation($"Model of user {account.IDuser} not valid");
                    ViewData["Pic"] = avatar.FileName;
                    return View(account);
                }
            }
            else return RedirectPermanent("/Username");
        }

        [HttpGet("/pas")]
        public IActionResult Password(AccountModel account)
        {
            return View(account);
        }


        private IActionResult CheckUsername(AccountModel Oldaccount, AccountModel account, IFormFile avatar)
        {
            if (account.Username != Oldaccount.Username)
            {
                if (accountDataProvider.GetAccountsByUsername(account.Username).Count() == 0)
                {
                    return Update(Oldaccount, account, avatar);
                }
                else
                {
                    ViewData["Error"] = "This Username is already in use.";
                    return View(account);
                }
            }
            else
            {
                return Update(Oldaccount, account, avatar);
            }
        }

        private IActionResult Update(AccountModel Oldaccount, AccountModel account, IFormFile avatar)
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
                ImageData = Oldaccount.Avatar;
            }
            account.Avatar = ImageData;
            account.Password = Oldaccount.Password;
            account.IDuser = Oldaccount.IDuser;
            accountDataProvider.UpdateAccount(account);
            return RedirectToAction("Index", "MyPage");
        }

        private bool Valid(AccountModel account)
        {
            if (account.Name == "")
                return false;

            else if (account.Username == "")
                return false;

            else if (account.Email == "")
                return false;

            else
                return true;

        }
    }
}
