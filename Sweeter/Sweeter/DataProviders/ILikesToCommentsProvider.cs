using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;


namespace Sweeter.DataProviders
{
    using Models;
    interface ILikesToCommentsProvider
    {
        IEnumerable<LikesToCommentsModel> GetLikes();
        IEnumerable<LikesToCommentsModel> GetLikesOfComment(int idcomment);
        IEnumerable<LikesToCommentsModel> GetLikesOfAuthor(int idauthor);
        LikesToCommentsModel GetLike(int id);

        void AddLike(LikesToCommentsModel like);

        

        void DeleteLike(int id);
    }
}
