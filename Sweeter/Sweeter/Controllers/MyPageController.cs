using Microsoft.AspNetCore.Mvc;
using Sweeter.DataProviders;
using Sweeter.Models;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sweeter.Controllers
{
    [Route("/MyPage")]
    public class MyPageController : Controller
    {
        // GET: /<controller>/
        private IPostDataProvider postDataProvider;
        private IAccountDataProvider accountDataProvider;
        private ICommentDataProvider commentDataProvider;
        public MyPageController(IPostDataProvider postData, IAccountDataProvider accountData, ICommentDataProvider commentData)
        {
            this.postDataProvider = postData;
            this.accountDataProvider = accountData;
            this.commentDataProvider = commentData;
        }
        [HttpGet]
        public IActionResult Index()
        {
            int id = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            if (id != 0)
            {
                //int id = Convert.ToInt32(Request.Cookies["0"]);
                IEnumerable<PostsModel> feeds = postDataProvider.GetPostsOfAuthor(id);
                IEnumerable<PostsModel> feedsnew = feeds.Reverse();

                AccountModel account = accountDataProvider.GetAccount(id);
                ViewData["Username"] = account.Username;
                ViewData["Email"] = account.Email;
                ViewData["Pic"] = "data:image/jpeg;base64," +Convert.ToBase64String(account.Avatar);
                foreach (PostsModel p in feedsnew)
                {
                    p.Author = accountDataProvider.GetAccount(p.IDuser);
                    p.CommentNumber = commentDataProvider.GetCommentsOfPost(p.IDpost).Count();
                }
                return View(feedsnew);
            }
            else return RedirectPermanent("/Username");
        }
        [HttpPost("addfeed")]
        public IActionResult NewPost(string mypost)
        {
            int id = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            AccountModel Author = accountDataProvider.GetAccount(id);
            PostsModel Mypost = new PostsModel
            {
                Author = Author,
                LikeNumder = 0,
                CommentNumber = 0,
                IDuser = id,
                Text = mypost
            };
            postDataProvider.AddPost(Mypost);
            return RedirectPermanent("/MyPage");
        }
    }
}
