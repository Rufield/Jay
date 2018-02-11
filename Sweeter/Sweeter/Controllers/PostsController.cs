using Microsoft.AspNetCore.Mvc;
using Sweeter.DataProviders;
using Sweeter.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sweeter.Controllers
{

    [Route("/Posts")]
    public class PostsController : Controller
    {
        private IPostDataProvider postDataProvider;
        private IAccountDataProvider accountDataProvider;
        public PostsController(IPostDataProvider postData, IAccountDataProvider accountData)
        {
            this.postDataProvider = postData;
            this.accountDataProvider = accountData;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
       

        
        [HttpGet]
        public IActionResult Index()
        {

            int id = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            if (id != 0)
            {


                //int id = Convert.ToInt32(Request.Cookies["0"]);
                IEnumerable<PostsModel> feeds = postDataProvider.GetPosts();
                IEnumerable<PostsModel> feedsnew=feeds.Reverse();

                AccountModel account = accountDataProvider.GetAccount(id);
                byte[] ImageData = account.Avatar;
                string path = "wwwroot/ForPics/av" + id.ToString() + ".jpeg";
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    fs.Write(ImageData, 0, ImageData.Length);
                }
                ViewData["Pic"] = path.Substring(7);
                ViewData["Username"] = account.Username;
                foreach (PostsModel p in feedsnew)
                {
                    p.Author = accountDataProvider.GetAccount(p.IDuser);
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
                LikeNumder=0,
                CommentNumber=0,
                IDuser=id,
                Text=mypost
            };
            postDataProvider.AddPost(Mypost);
            return RedirectPermanent("/Posts");
        }
            // GET api/values
            /*[HttpGet]
             * 
            private IPostDataProvider postDataProvider;
            public PostsController(IPostDataProvider postData)
            {
                this.postDataProvider = postData;
            }
            public async Task<IEnumerable<PostsModel>> Get()
            {
                return await this.postDataProvider.GetPosts();
            }

            // GET api/values/5
            [HttpGet("{id}")]
            public async Task<PostsModel> Get(int id)
            {
                return await this.postDataProvider.GetPost(id); ;
            }

            // POST api/values
            [HttpPost]
            public async void Post([FromBody]PostsModel postsModel)
            {
                await this.postDataProvider.AddPost(postsModel);

            }
            [HttpPut("{id}")]
            public async Task Put(int id, [FromBody]PostsModel post)
            {
                await this.postDataProvider.UpdatePost(post);
            }*/

        }
}
