using Sweeter.Models;
using System.Collections.Generic;

namespace Sweeter.DataProviders
{
    public interface ILikesToCommentsProvider
    {
        IEnumerable<LikesToCommentsModel> GetLikes(int iduser, int? idcomment);
        IEnumerable<LikesToCommentsModel> GetLikesOfComment(int idcomment);
        IEnumerable<LikesToCommentsModel> GetLikesOfAuthor(int idauthor);
        LikesToCommentsModel GetLike(int iduser,int? idcomment);

        void AddLike(LikesToCommentsModel like);
        void DeleteLike(int id);
    }
}
