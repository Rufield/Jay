using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sweeter.DataProviders;
using Sweeter.Models;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sweeter.Controllers
{
    [Route("/Comment")]
    public class CommentController : Controller
    {
        private IAccountDataProvider accountDataProvider;
        private ICommentDataProvider commentDataProvider;
        private IPostDataProvider postDataProvider;
        private ILikesToCommentsProvider likesToCommentsProvider;
        private ILogger<CommentController> _logger;

        public CommentController(IPostDataProvider postData, IAccountDataProvider accountData, ICommentDataProvider commentData, ILogger<CommentController> logger, ILikesToCommentsProvider likesToCommentsProvider)
        {
            this.postDataProvider = postData;
            this.accountDataProvider = accountData;
            this.commentDataProvider = commentData;
            _logger = logger;
            this.likesToCommentsProvider = likesToCommentsProvider;
        }

        public int? idPost;
        [HttpGet]
        public IActionResult Comments(int? id, bool? SortLike)
        {
            int idd;
            if (HttpContext.User.Claims.Count() != 0)
            {
                idd = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            }
            else
                idd = 0;
            if (idd != 0)
            {
                AccountModel account = accountDataProvider.GetAccount(idd);
                idPost = id;
                ViewData["Username"] = account.Username;
                ViewData["Pic"] = "data:image/jpeg;base64," + Convert.ToBase64String(account.Avatar);
                ViewData["Style"] = account.Style;
                PostsModel post = postDataProvider.GetPost(id);
                ViewData["PostText"] = post.Text;
                ViewData["PostAuth"] = accountDataProvider.GetAccount(post.IDuser).Username;
                ViewData["PostLike"] = post.LikeNumder;
                ViewData["PostComment"] = post.CommentNumber;
                ViewData["ID"] = id;
                ViewData["ViewSortLike"] = "Show";
                IEnumerable<CommentModel> comments = commentDataProvider.GetCommentsOfPost(id);
                AccountModel Author = accountDataProvider.GetAccount(post.IDuser);
                post.CommentNumber = comments.Count();
                post.Author = Author;
                postDataProvider.UpdatePost(post);
                if(SortLike == true)
                {
                    comments = comments.OrderByDescending(com => com.LikeNumder);
                    ViewData["ViewSortLike"] = "NotShow";
                }
                foreach (CommentModel com in comments)
                {
                    com.Post = post;
                    com.Author = accountDataProvider.GetAccount(com.IDuser);
                    com.LikeNumder = likesToCommentsProvider.GetLikesOfComment(com.IDcomment).Count();
                    commentDataProvider.UpdateComment(com);
                }
                if (comments.Count() == 0)
                {
                    comments.Concat(new[]{new CommentModel
                {
                    Text="There are no comments here yet",
                    Author=new AccountModel
                    {
                        Username="Admin"
                    },
                    LikeNumder=0,
                }});
                }

                _logger.LogInformation($"User {account.IDuser} see all comments of post {post.IDpost}.");
                return View(comments);
            }
            return Redirect("/");
        }

        [HttpPost("addcomment")]
        public IActionResult NewComment(int id, string mypost)
        {

            int idd;
            if (HttpContext.User.Claims.Count() != 0)
            {
                idd = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            }
            else
                idd = 0;
            if (idd != 0)
            {
                AccountModel Author = accountDataProvider.GetAccount(idd);
                PostsModel post = postDataProvider.GetPost(id);
                post.CommentNumber++;
                AccountModel AuthorPost = accountDataProvider.GetAccount(post.IDuser);
                if (mypost != null)
                {
                    post.Author = AuthorPost;
                    postDataProvider.UpdatePost(post);
                    CommentModel comment = new CommentModel
                    {
                        Author = Author,
                        LikeNumder = 0,
                        IDuser = idd,
                        IDpost = post.IDpost,
                        Post = post,
                        Text = mypost
                    };
                    commentDataProvider.AddComment(comment);
                    _logger.LogInformation($"User {Author.IDuser} create the comment {comment.IDcomment} on post {comment.IDpost}");
                    return Redirect("/Comment?id=" + id);
                }
                return Redirect("/Comment?id=" + id);
            }
            else return Redirect("/");
        }

        [HttpPost("DeleteComment")]
        public string DeleteComment(int? id)
        {
            commentDataProvider.DeleteComment(id);
            return "";
        }
        [HttpPost("SortbyLikes")]
        public IActionResult SortbyLikes(int? id)
        {
            int idd;
            if (HttpContext.User.Claims.Count() != 0)
            {
                idd = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            }
            else
                idd = 0;
            if (idd != 0)
            {
                AccountModel account = accountDataProvider.GetAccount(idd);
                idPost = id;
                ViewData["Username"] = account.Username;
                ViewData["Pic"] = "data:image/jpeg;base64," + Convert.ToBase64String(account.Avatar);
                ViewData["Style"] = account.Style;
                PostsModel post = postDataProvider.GetPost(id);
                ViewData["PostText"] = post.Text;
                ViewData["PostAuth"] = accountDataProvider.GetAccount(post.IDuser).Username;
                ViewData["PostLike"] = post.LikeNumder;
                ViewData["PostComment"] = post.CommentNumber;
                ViewData["ID"] = id;

                IEnumerable<CommentModel> comments = commentDataProvider.GetCommentsOfPost(id);
                AccountModel Author = accountDataProvider.GetAccount(post.IDuser);
                post.CommentNumber = comments.Count();
                post.Author = Author;
                postDataProvider.UpdatePost(post);
                foreach (CommentModel com in comments)
                {
                    com.Post = post;
                    com.Author = accountDataProvider.GetAccount(com.IDuser);
                }
                if (comments.Count() == 0)
                {
                    comments.Concat(new[]{new CommentModel
                {
                    Text="There are no comments here yet",
                    Author=new AccountModel
                    {
                        Username="Admin"
                    },
                    LikeNumder=0,
                }});
                }

                _logger.LogInformation($"User {account.IDuser} see all comments of post {post.IDpost}.");
                return RedirectToAction("/Comment?id=" + id, new {comments });
            }
            return Redirect("/");
        }

    }
}
