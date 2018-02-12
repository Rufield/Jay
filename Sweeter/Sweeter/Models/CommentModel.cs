using System.Collections.Generic;

namespace Sweeter.Models
{
    public class CommentModel
    {
        public int IDcomment { get; set; }
        public int IDuser { get; set; }
        public int IDpost { get; set; }
        public PostsModel Post { get; set; }
        public string Text { get; set; }
        public int LikeNumber { get; set; }
        public List<LikesToCommentsModel> Comments { get; set; }
        public AccountModel Author { get; set; }
}
}
