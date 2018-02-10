using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Sweeter.DataProviders;
using Sweeter.Models;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Sweeter.Controllers
{
   
    [Route("/Register")]
    public class RegisterController : Controller
    {
        private IAccountDataProvider accountDataProvider;
        public RegisterController(IAccountDataProvider accountData)
        {
            this.accountDataProvider = accountData;
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
                        account.Password = GetHashString(account.Password);
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