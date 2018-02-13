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
    [Route("/UserPage")]
    public class UserPageController : Controller
    {
        private IPostDataProvider postDataProvider;
        private IAccountDataProvider accountDataProvider;
        private ICommentDataProvider commentDataProvider;
        public UserPageController(IPostDataProvider postData, IAccountDataProvider accountData, ICommentDataProvider commentData)
        {
            this.postDataProvider = postData;
            this.accountDataProvider = accountData;
            this.commentDataProvider = commentData;
        }
        [HttpGet]
        public IActionResult Index(int? id)
        {
         
            int idUs = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            IEnumerable<PostsModel> feeds = postDataProvider.GetPostsOfAuthor(id);
            IEnumerable<PostsModel> feedsnew = feeds.Reverse();

            AccountModel account = accountDataProvider.GetAccount(id);
            ViewData["Username"] = account.Username;
            ViewData["Email"] = account.Email;
            foreach (PostsModel p in feedsnew)
            {
                p.Author = accountDataProvider.GetAccount(p.IDuser);
                p.CommentNumber = commentDataProvider.GetCommentsOfPost(p.IDpost).Count();
            }
            return View(feedsnew);
            
        }
    }
}
