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
            int id;
            if (HttpContext.User.Claims.Count() != 0)
            {
                id = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            }
            else
                id = 0;
            if (id != 0)
            {
                //int id = Convert.ToInt32(Request.Cookies["0"]);
                IEnumerable<PostsModel> feeds = postDataProvider.GetPostsOfAuthor(id);
                IEnumerable<PostsModel> feedsnew = feeds.Reverse();

                AccountModel account = accountDataProvider.GetAccount(id);
                if (account.Name != null)
                    ViewData["Name"] = account.Name;
                else
                    ViewData["Name"] = "Mysterious user";
                if (account.About != null)
                    ViewData["About"] = account.About;
                else
                    ViewData["About"] = "\"This person wantn't tell about himself/herself. But I knew he/she good human\" © Jay";
                ViewData["Style"] = account.Style;
                ViewData["UserName"] = account.Username;
                ViewData["Pic"] = "data:image/jpeg;base64," +Convert.ToBase64String(account.Avatar);
                foreach (PostsModel p in feedsnew)
                {
                    p.Author = accountDataProvider.GetAccount(p.IDuser);
                    p.CommentNumber = commentDataProvider.GetCommentsOfPost(p.IDpost).Count();
                }
                return View(feedsnew);
            }
            else return Redirect("/");
        }

        [HttpPost("addfeed")]
        public IActionResult NewPost(string mypost)
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
                AccountModel Author = accountDataProvider.GetAccount(id);
                if (mypost != null)
                {
                    PostsModel Mypost = new PostsModel
                    {
                        Author = Author,
                        LikeNumder = 0,
                        CommentNumber = 0,
                        IDuser = id,
                        Text = mypost
                    };
                    postDataProvider.AddPost(Mypost);
                    return Redirect("/MyPage");
                }
                return Redirect("/MyPage");
            }
            return Redirect("/");
        }

        [HttpPost("DeletePost")]
        public string DeletePost(int? id)
        {
            postDataProvider.DeletePost(id);
            return "";
        }
    }
}
