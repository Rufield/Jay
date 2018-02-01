using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sweeter.Models;
using Sweeter.DataProviders;

namespace Sweeter.Controllers
{
    
    [Route("/Posts")]
    public class PostsController : Controller
    {
        private IPostDataProvider postDataProvider;
        public PostsController(IPostDataProvider postData)
        {
            this.postDataProvider = postData;
        }
        public IActionResult Index()
        {
            return View();
        }
       

        /*
          [HttpGet]
        public ActionResult Feed()
        {
            IEnumerable<PostsModel> feeds = postDataProvider.GetPosts();

            return PartialView();
        }
             

          */
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
