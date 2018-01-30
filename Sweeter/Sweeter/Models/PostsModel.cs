using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sweeter.Models
{
    public class PostsModel
    {
        public int IDnews { get; set; }
        public DateTime PublicDate { get; set; }
        public AccountModel Author { get; set; }
        public int LikesNumber { get; set; }
        public List<LikesToPostsModel> Likes { get; set; }
        public int CommentsNumber { get; set; }
        public List<CommentModel> Comments { get; set; }
        public string Text { get; set; }
    }
}
