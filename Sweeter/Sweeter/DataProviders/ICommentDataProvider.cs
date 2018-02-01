using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sweeter.DataProviders
{
    using Models;
    using Dapper;

  public  interface ICommentDataProvider
    {
        IEnumerable<CommentModel> GetComments();
   IEnumerable<CommentModel> GetCommentsOfPost(int idpost);
       CommentModel GetComment(int id);
        void DeleteComment(int id);
        void AddComment(CommentModel comment);
        void UpdateComment(CommentModel comment);
    }
}
