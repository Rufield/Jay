using System;
using System.Collections;
using System.Collections.Generic;

namespace Sweeter.Models
{
    public class PostsModel
    {
        public int IDpost { get; set; }
        public DateTime PublicDate { get; set; }
        public AccountModel Author { get; set; }
        public int LikeNumder { get; set; }
        public int IDuser { get; set; }
        public List<LikesToPostsModel> Likes { get; set; }
        public int CommentNumber { get; set; }
        public List<CommentModel> Comments { get; set; }
        public string Text { get; set; }

       
    }
}
