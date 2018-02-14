using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sweeter.DataProviders;
using Sweeter.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        private ILogger<CommentController> _logger;

        public CommentController(IPostDataProvider postData, IAccountDataProvider accountData, ICommentDataProvider commentData, ILogger<CommentController> logger)
        {
            this.postDataProvider = postData;
            this.accountDataProvider = accountData;
            this.commentDataProvider = commentData;
            _logger = logger;
        }

        public   int? idPost;
        [HttpGet]
        public IActionResult Comments(int? id)
        {
            int idd = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            AccountModel account = accountDataProvider.GetAccount(idd);
            idPost = id;
            ViewData["Username"] = account.Username;
            ViewData["Pic"] = "data:image/jpeg;base64," + Convert.ToBase64String(account.Avatar);
            PostsModel post = postDataProvider.GetPost(id);
            ViewData["PostText"] = post.Text;
            ViewData["PostAuth"] = accountDataProvider.GetAccount(post.IDuser).Username;
            ViewData["PostLike"] = post.LikeNumder;
            ViewData["PostComment"] = post.CommentNumber;
            ViewData["ID"] = id;

            IEnumerable<CommentModel> comments = commentDataProvider.GetCommentsOfPost(id).Reverse();
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
                    LikeNumber=0,
                }});
            }

            _logger.LogInformation($"User {account.IDuser} see all comments of post {post.IDpost}.");
            return View(comments);
        }

        [HttpPost("addcomment")]
        public IActionResult NewComment(int id, string mypost)
        {
           
            int idd = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            AccountModel Author = accountDataProvider.GetAccount(idd);
            PostsModel post = postDataProvider.GetPost(id);
            post.CommentNumber++;
            AccountModel AuthorPost = accountDataProvider.GetAccount(post.IDuser);
          
            post.Author = AuthorPost;
            postDataProvider.UpdatePost(post);
            CommentModel comment=new CommentModel
            {
                Author = Author,
                LikeNumber=0,
                IDuser = idd,
                IDpost=post.IDpost,
                Post=post,
                Text = mypost
            };
            commentDataProvider.AddComment(comment);
            _logger.LogInformation($"User {Author.IDuser} create the comment {comment.IDcomment} on post {comment.IDpost}");
            return  RedirectPermanent("/Comment?id="+id);
        }
        /*  private ICommentDataProvider commentDataProvider;
          public CommentController(ICommentDataProvider commentData)
          {
              this.commentDataProvider = commentData;
          }
          // GET api/values
          [HttpGet]
          public async Task<IEnumerable<CommentModel>> Get()
          {
              return await this.commentDataProvider.GetComments();
          }

          // GET api/values/5
          [HttpGet("{id}")]
          public async Task<CommentModel> Get(int id)
          {
              return await this.commentDataProvider.GetComment(id); 
          }

          // POST api/values
          [HttpPost]
          public async void Comment([FromBody]CommentModel comment)
          {
              await this.commentDataProvider.AddComment(comment);

          }
          [HttpPut("{id}")]
          public async Task Put(int id, [FromBody]CommentModel comment)
          {
              await this.commentDataProvider.UpdateComment(comment);
          }
          */
    }
}
            //byte[] ImageData = account.Avatar;
            //string path = "wwwroot/ForPics/av" + idd.ToString() + ".jpeg";
            //using (FileStream fs = new FileStream(path, FileMode.Create))
            //{
            //    fs.Write(ImageData, 0, ImageData.Length);
            //}
            //ViewData["Pic"] = path.Substring(7);
