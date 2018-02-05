using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sweeter.DataProviders;
using Sweeter.Models;
using System.Security.Cryptography;
using System.Text;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sweeter.Controllers
{
    
    [Route("/Username")]
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
        public string Login(string email, string password)
        {
            IEnumerable<AccountModel> accs = accountDataProvider.GetAccountsByEmail(email);
        
           if(accs.Count()!=0)
            {
                {
                    if (accs.First().Password.Equals(GetHashString(password)))
                    {
                        return "All is good";
                    }
                    return "Incorrect password";
                }
            }
            
            
            return "There's no such account";
            
        }
        
    }
}
