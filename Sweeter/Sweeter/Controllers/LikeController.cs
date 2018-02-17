using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sweeter.Models;
using Sweeter.Services.DataProviders;
using Sweeter.DataProviders;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sweeter.Controllers
{
    [Route("/Like")]
    public class LikeController : Controller
    {
        private IAccountDataProvider accountDataProvider;
        private IPostDataProvider postDataProvider;
        private ILikesToPostsProvider LikesToPostsProvider;
        private ILikesToCommentsProvider LikesToCommentsProvider;
        private ICommentDataProvider commentDataProvider;
        public LikeController(IAccountDataProvider accountDataProvider, ILikesToPostsProvider likesToPosts, IPostDataProvider postDataProvider, ILikesToCommentsProvider likesToCommentsProvider, ICommentDataProvider commentDataProvider)
        {
            this.accountDataProvider = accountDataProvider;
            this.LikesToPostsProvider = likesToPosts;
            this.postDataProvider = postDataProvider;
            this.LikesToCommentsProvider = likesToCommentsProvider;
            this.commentDataProvider = commentDataProvider;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("ToPost")]
        public string LikesToPosts(int? id)
        {
            int idd = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            AccountModel Author = accountDataProvider.GetAccount(idd);
            PostsModel post = postDataProvider.GetPost(id);
            post.Author = accountDataProvider.GetAccount(post.IDuser);
            IEnumerable<LikesToPostsModel> like = LikesToPostsProvider.GetLikes(idd, id);
            if (like.Count() == 0)
            {
                LikesToPostsProvider.AddLike(new LikesToPostsModel
                {
                    Author = Author,
                    Post = post
                });
                post.LikeNumder++;
                postDataProvider.UpdatePost(post);
                return Convert.ToString(post.LikeNumder);
            }
            else
                LikesToPostsProvider.DeleteLike(like.First().IDus_post);
            post.LikeNumder--;
            postDataProvider.UpdatePost(post);
            return Convert.ToString(post.LikeNumder);
        }
        [HttpPost("ToComment")]
        public string LikesToComments(int? id)
        {
            int idd = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            AccountModel Author = accountDataProvider.GetAccount(idd);
            CommentModel comment = commentDataProvider.GetComment(id);
            PostsModel post = postDataProvider.GetPost(comment.IDpost);
            comment.Post = post;
            comment.Author = accountDataProvider.GetAccount(comment.IDuser);
            IEnumerable<LikesToCommentsModel> like = LikesToCommentsProvider.GetLikes(idd, id);
            if (like.Count() == 0)
            {
                LikesToCommentsProvider.AddLike(new LikesToCommentsModel
                {
                    Author = Author,
                    Comment=comment
                });
                comment.LikeNumder++;
                commentDataProvider.UpdateComment(comment);
                return Convert.ToString(comment.LikeNumder);
            }
            else
            {
                LikesToCommentsProvider.DeleteLike(like.First().IDus_com);
                comment.LikeNumder--;
                commentDataProvider.UpdateComment(comment);
                return Convert.ToString(comment.LikeNumder);
            }

            }
    }
}
