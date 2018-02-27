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
    [Route("/Search")]
    public class SearchController : Controller
    {
        // GET: /<controller>/
        private IPostDataProvider postDataProvider;
        private IAccountDataProvider accountDataProvider;
        private ICommentDataProvider commentDataProvider;
        public SearchController(IPostDataProvider postData, IAccountDataProvider accountData, ICommentDataProvider commentData)
        {
            this.postDataProvider = postData;
            this.accountDataProvider = accountData;
            this.commentDataProvider = commentData;
        }

        [HttpGet]
        public IActionResult Search()
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
                ViewData["Username"] = account.Username;
                ViewData["Email"] = account.Email;
                ViewData["Style"] = account.Style;
                ViewData["Pic"] = "data:image/jpeg;base64," + Convert.ToBase64String(account.Avatar);
                return View();
            }
            else return Redirect("/");
        }

        [HttpPost("search")]
        public IActionResult Search(string searchinfo)
        {
            int id;
            if (HttpContext.User.Claims.Count() != 0)
            {
                id = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            }
            else
                id = 0;
            if (searchinfo != null)
            {
                if (id != 0)
                {
                    IEnumerable<AccountModel> SearchResult = accountDataProvider.SearchAccountsByUsername(searchinfo);
                    AccountModel account = accountDataProvider.GetAccount(id);
                    ViewData["Username"] = account.Username;
                    ViewData["Email"] = account.Email;
                    ViewData["Style"] = account.Style;
                    ViewData["Pic"] = "data:image/jpeg;base64," + Convert.ToBase64String(account.Avatar);

                    return View(SearchResult);
                }
                else return Redirect("/");
            }
            else return Redirect("/");
        }
    }
}
