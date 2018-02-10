using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sweeter.Controllers
{
    using Models;
    using DataProviders;
    [Route("/Comment")]
    public class CommentController : Controller
    {

        public IActionResult Index()
        {
            return View();
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
