using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sweeter.DataProviders;
using Sweeter.Models;
using Sweeter.Services.Comparer;
using Sweeter.Services.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sweeter.Controllers
{
    [Route("/Posts")]
    public class PostsController : Controller
    {
        private IPostDataProvider postDataProvider;
        private IAccountDataProvider accountDataProvider;
        private ICommentDataProvider commentDataProvider;
        private IUnsubscribesDataProvider unsubscribesDataProvider;
        private ILikesToPostsProvider likesToPostsProvider;
        private ICategoriesDataProvider categoriesDataProvider;
        private ILogger<PostsController> _logger;

        public PostsController(IPostDataProvider postData, IAccountDataProvider accountData, ICommentDataProvider commentData, IUnsubscribesDataProvider unsubscribesData, ILogger<PostsController> logger, ILikesToPostsProvider likesToPostsProvider, ICategoriesDataProvider categoriesDataProvider)
        {
            this.postDataProvider = postData;
            this.accountDataProvider = accountData;
            this.commentDataProvider = commentData;
            this.unsubscribesDataProvider = unsubscribesData;
            _logger = logger;
            this.likesToPostsProvider = likesToPostsProvider;
            this.categoriesDataProvider = categoriesDataProvider;
        }

        [HttpGet]
        public IActionResult Index(int? CatID)
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

                IEnumerable<UnsubscribesModel> unsubscribes = unsubscribesDataProvider.GetUnsubscribesOfUser(id);
                //int id = Convert.ToInt32(Request.Cookies["0"]);
                IEnumerable<PostsModel> feeds;
                if (CatID == null)
                {
                    feeds = postDataProvider.GetPosts();
                }
                else
                {
                    feeds = postDataProvider.GetPostsByCategory(CatID.Value);
                }
                IEnumerable<PostsModel> feedsrev = feeds.Reverse();
                AccountModel account = accountDataProvider.GetAccount(id);
                ViewData["Style"] = account.Style;
                ViewData["Username"] = account.Username;
                ViewData["Pic"] = "data:image/jpeg;base64," + Convert.ToBase64String(account.Avatar);
                IEnumerable<PostsModel> deletedposts;
                IEnumerable<PostsModel> feedsnew = feedsrev;
                if (unsubscribes.Count() != 0)
                {
                    deletedposts = postDataProvider.GetPostsOfAuthor(unsubscribes.First().IDus_pas);
                    foreach (UnsubscribesModel un in unsubscribes)
                    {
                        IEnumerable<PostsModel> temp = postDataProvider.GetPostsOfAuthor(un.IDus_pas);
                        deletedposts = deletedposts.Concat(temp);
                    }
                    var Comparer = new PostComparer();
                    feedsnew = feedsrev.Except<PostsModel>(deletedposts, Comparer);
                }
                foreach (PostsModel p in feedsnew)
                {
                    p.Author = accountDataProvider.GetAccount(p.IDuser);
                    p.CommentNumber = commentDataProvider.GetCommentsOfPost(p.IDpost).Count();
                    p.LikeNumder = likesToPostsProvider.GetLikesOfPost(p.IDpost).Count();
                    postDataProvider.UpdatePost(p);
                    p.Category = new CategoriesModel { ID=p.IDCategory, Category= categoriesDataProvider.GetCategoryByID(p.IDCategory).Category };
                }
                ViewData["categories"] = categoriesDataProvider.GetCategories();
                _logger.LogInformation($"All is good, user {account.IDuser} look at new posts");
                return View(feedsnew);
            }
            else return RedirectPermanent("/");
        }

        [HttpPost("addfeed")]
        public IActionResult NewPost(string mypost, string category)
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
                    if (category == null) category = "Flood";

                    PostsModel Mypost = new PostsModel
                    {
                        Author = Author,
                        LikeNumder = 0,
                        CommentNumber = 0,
                        IDuser = id,
                        Text = mypost,
                        IDCategory = categoriesDataProvider.GetCategoryByName(category).ID
                    };
                    postDataProvider.AddPost(Mypost);
                    _logger.LogInformation($"Post {Mypost.IDpost} created by user {Author.IDuser}");
                    return Redirect("/Posts");
                }
                else return Redirect("/Posts");
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
                    ViewData["Style"] = account.Style;
                    ViewData["Username"] = account.Username;
                    ViewData["Email"] = account.Email;
                    ViewData["Pic"] = "data:image/jpeg;base64," + Convert.ToBase64String(account.Avatar);
                    return View("~/Views/Search/Search.cshtml", SearchResult);
                }
                else return Redirect("/");
            }
            else return Redirect("/Posts");
        }

        [HttpPost("DeletePost")]
        public string DeletePost(int? id)
        {
            postDataProvider.DeletePost(id);
            return "";
        }
    }
}