﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sweeter.DataProviders;
using Sweeter.Models;
using Sweeter.Services.HashService;
using System.IO;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sweeter.Controllers
{
    [Route("/Profile")]
    public class ProfileController : Controller
    {
        private IAccountDataProvider accountDataProvider;
        private IHashService _hasher;
        private ILogger<ProfileController> _logger;

        public ProfileController(IAccountDataProvider accountData, ILogger<ProfileController> logger, IHashService hasher)
        {
            accountDataProvider = accountData;
            _hasher = hasher;
            _logger = logger;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            int id;
            if (HttpContext.User.Claims.Count() != 0)
            {
                id = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            }
            else
                id = 0;
            if (id != 0)
            {
                AccountModel account = accountDataProvider.GetAccount(id);
                ViewData["Style"] = account.Style;
                ViewData["AccountID"] = account.IDuser;
                _logger.LogInformation($"User {account.IDuser} has successful download the Profile page");
                /*if (account.Avatar == null)
                    using (FileStream FS = new FileStream("wwwroot/lib/img/Avatar.jpeg", FileMode.Open))
                    {
                        byte[] ImageData = new byte[FS.Length];
                        FS.Read(ImageData, 0, ImageData.Length);
                        account.Avatar = ImageData;
                    }*/
                return View(account);
            }
            else
            {
                _logger.LogInformation($"Not find id in cookie");
                return RedirectPermanent("/Username");
            }
        }

        [HttpPost]
        public IActionResult Index(string password, AccountViewModel Faccount)
        {
            int id;
            if (HttpContext.User.Claims.Count() != 0)
            {
                id = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            }
            else
                id = 0;
            if (id != 0)
            {
                if (password == null)
                {
                    AccountModel Oldaccount = accountDataProvider.GetAccount(id);
                    if (Faccount.Style == null) Faccount.Style = Oldaccount.Style;
                    AccountModel account = new AccountModel
                    {
                        Name = Faccount.Name,
                        Username = Faccount.Username,
                        Email = Faccount.Email,
                        Style = Faccount.Style,
                        About = Faccount.About
                    };
                    account.Avatar = UploadingPicture(Faccount.Avatar);
                    if (account.Avatar == null)
                        account.Avatar = Oldaccount.Avatar;
                    account.Password = "0";
                    if (ModelState.IsValid)
                    {
                        if (account.Email != Oldaccount.Email)
                        {
                            _logger.LogInformation($"User Email {Faccount.Email} != {Oldaccount.Email}");
                            if (accountDataProvider.GetAccountsByEmail(account.Email).Count() == 0)
                            {

                                return CheckUsername(Oldaccount, account);
                            }
                            else
                            {
                                _logger.LogInformation($"This Email is already in use.");
                                ViewData["Error"] = "This Email is already in use.";
                                return View(account);
                            }
                            //return RedirectToAction("Password", account);
                        }
                        else
                        {
                            _logger.LogInformation($"Maybe {Faccount.IDuser} user's Email {Faccount.Email} = {Oldaccount.Email}");
                            return CheckUsername(Oldaccount, account);
                        }
                    }
                    else
                    {
                        _logger.LogInformation($"Model of user {Faccount.IDuser} not valid");
                        return View(account);
                    }
                }
                else
                {
                    AccountModel CurrentUser = accountDataProvider.GetAccount(id);
                    if (CurrentUser.Password.Equals(_hasher.GetHashString(password)))
                    {
                        accountDataProvider.DeleteAccount(id);
                        return RedirectToAction("Logout", "LogOut");
                    }
                    else
                    {
                        ViewData["DeleteError"] = "Password is not correct!";
                        return View(CurrentUser);
                    }
                }
            }
            else return RedirectToAction("/");
        }

        [HttpGet("/pas")]
        public IActionResult Password(AccountModel account)
        {
            return View(account);
        }

        private IActionResult CheckUsername(AccountModel Oldaccount, AccountModel account)
        {
            if (account.Username != Oldaccount.Username)
            {
                if (accountDataProvider.GetAccountsByUsername(account.Username).Count() == 0)
                {
                    return Update(Oldaccount, account);
                }
                else
                {
                    _logger.LogInformation($"This Username is already in use.");
                    ViewData["Error"] = "This Username is already in use.";
                    return View(account);
                }
            }
            else
            {
                return Update(Oldaccount, account);
            }
        }

        private IActionResult Update(AccountModel Oldaccount, AccountModel account)
        {
            account.IDuser = Oldaccount.IDuser;
            accountDataProvider.UpdateAccount(account);
            _logger.LogInformation($"Update success");
            return RedirectToAction("Index", "MyPage");
        }

        private byte[] UploadingPicture(IFormFile Avatar)
        {
            if (Avatar != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)Avatar.Length);
                }
                // установка массива байтов
                return imageData;
            }
            else
            {
                return null;
            }
        }
    }
}
