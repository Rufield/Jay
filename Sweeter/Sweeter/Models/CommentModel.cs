using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sweeter.Models
{
    public class CommentModel
    {
        public int IDcomment { get; set; }
        public PostsModel Post { get; set; }
        public string Text { get; set; }
        public int LikeNumber { get; set; }
        public List<LikesToCommentsModel> Comments { get; set; }
        public AccountModel Author { get; set; }
}
}
