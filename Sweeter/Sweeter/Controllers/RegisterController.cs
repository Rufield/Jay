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
      [HttpPost]
        public IActionResult Form(string avatar,string fullname, string username, string Email, string password, string password2)
        {
            
                if (accountDataProvider.GetAccountsByEmail(Email).Count() == 0)
                {
                if (accountDataProvider.GetAccountsByUsername(username).Count() == 0)
                {
                    if (password.Equals(password2))
                    {
                        AccountModel account = new AccountModel
                        {
                            FullName = fullname,
                            Login = username,
                            Email = Email,
                            Password = password,
                            Avatar = avatar



                        };
                        accountDataProvider.AddAccount(account);
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        ViewData["UsernameStat"] = "";
                        ViewData["Name"] = fullname;
                        ViewData["Username"] = username;
                        ViewData["Email"] = Email;


                        ViewData["EmailStat"] = "";
                        ViewData["PasswordStat"] = "notright";
                        ViewData["ErrorMessage"] = "Passwords are not equal";
                        return View();
                    }

                }
                else
                {
                    ViewData["UsernameStat"] = "notright";
                    ViewData["Name"] = fullname;
                    ViewData["Username"] = "";
                    ViewData["Email"] = Email;


                    ViewData["EmailStat"] = "";
                    ViewData["PasswordStat"] = "";
                    ViewData["ErrorMessage"] = "Such username already exists";
                    return View();
                }
                }
                else
                {
                    ViewData["UsernameStat"] = "";
                    ViewData["Name"] = fullname;
                    ViewData["Username"] = username;
                    ViewData["Email"] = "";
                 

                    ViewData["EmailStat"] = "notright";
                    ViewData["PasswordStat"] = "";
                    ViewData["ErrorMessage"] = "Such email already exists";
                    return View();
                }
            }
            
        }
      


    }

