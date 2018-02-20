using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sweeter.DataProviders;
using Sweeter.Models;
using Sweeter.Services.EmailService;
using Sweeter.Services.HashService;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Sweeter.Controllers
{
    [Route("/Register")]
    public class RegisterController : Controller
    {
        private IAccountDataProvider accountDataProvider;
        private IHashService _hasher;
        private ILogger<RegisterController> _logger;
        private IEmailService _emailService;

        public RegisterController(IAccountDataProvider accountData, IHashService hasher, ILogger<RegisterController> logger, IEmailService emailService)
        {
            accountDataProvider = accountData;
            _hasher = hasher;
            _logger = logger;
            _emailService = emailService;
        }

        // GET: /<controller>/
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
        public IActionResult Index(AccountModel account)
        {
            if (ModelState.IsValid)
            {
                if (accountDataProvider.GetAccountsByEmail(account.Email).Count() == 0)
                {
                    if (accountDataProvider.GetAccountsByUsername(account.Username).Count() == 0)
                    {
                        account.Password = _hasher.GetHashString(account.Password);
                        using (FileStream FS = new FileStream("wwwroot/lib/img/Avatar.jpeg", FileMode.Open))
                        {
                            byte[] ImageData = new byte[FS.Length];
                            FS.Read(ImageData, 0, ImageData.Length);
                            account.Avatar = ImageData;
                        }
                        account.Style = "Green";
                        accountDataProvider.AddAccount(account);
                        _logger.LogInformation($"New user{account.IDuser} {account.Name} register with Email {account.Email} and username {account.Username}.");
                        MailMessage mailMessage = new MailMessage("example@gmail.com", account.Email);
                        mailMessage.Subject = "Registration in Jay";
                        mailMessage.Body = "Congratulations!\nYou`ve successfully registered in Jay!";
                        _emailService.SendEmail(mailMessage);
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
                    _logger.LogInformation($"User {account.IDuser} {account.Name} forgive that Email {account.Email} is alredy in use");
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