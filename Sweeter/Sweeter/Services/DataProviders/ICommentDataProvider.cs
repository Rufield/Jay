using Sweeter.Models;
using System.Collections.Generic;

namespace Sweeter.DataProviders
{
    public  interface ICommentDataProvider
    {
        IEnumerable<CommentModel> GetComments();
        IEnumerable<CommentModel> GetCommentsOfPost(int? idpost);
        CommentModel GetComment(int id);

        void DeleteComment(int id);
        void AddComment(CommentModel comment);
        void UpdateComment(CommentModel comment);
    }
}
