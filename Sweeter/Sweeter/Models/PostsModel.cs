using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sweeter.Models
{
    public class PostsModel
    {
        public int IDpost { get; set; }
        public DateTime PublicDate { get; set; }
        public AccountModel Author { get; set; }
        public int LikeNumber { get; set; }
        public List<LikesToPostsModel> Likes { get; set; }
        public int CommentNumber { get; set; }
        public List<CommentModel> Comments { get; set; }
        public string Text { get; set; }
    }
}
