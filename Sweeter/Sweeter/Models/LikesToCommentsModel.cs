namespace Sweeter.Models
{
    public class LikesToCommentsModel
    {
        public int IDus_com { get; set; }
        public CommentModel Comment { get; set; }
        public AccountModel Author { get; set; }
    }
}
