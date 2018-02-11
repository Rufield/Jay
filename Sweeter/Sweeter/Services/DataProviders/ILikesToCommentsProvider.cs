using Sweeter.Models;
using System.Collections.Generic;

namespace Sweeter.DataProviders
{
    public interface ILikesToCommentsProvider
    {
        IEnumerable<LikesToCommentsModel> GetLikes();
        IEnumerable<LikesToCommentsModel> GetLikesOfComment(int idcomment);
        IEnumerable<LikesToCommentsModel> GetLikesOfAuthor(int idauthor);
        LikesToCommentsModel GetLike(int id);

        void AddLike(LikesToCommentsModel like);
        void DeleteLike(int id);
    }
}
