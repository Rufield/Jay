namespace Sweeter.Models
{
    public class LikesToPostsModel
    {
        public int IDus_post { get; set; }
        public PostsModel Post { get; set; }
        public AccountModel Author { get; set; }
    }
}
