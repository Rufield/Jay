using Sweeter.Models;
using System.Collections.Generic;

namespace Sweeter.DataProviders
{
    public interface ILikesToPostsProvider
    {
        IEnumerable<LikesToPostsModel> GetLikes();
        IEnumerable<LikesToPostsModel> GetLikesOfPost(int idpost);
        IEnumerable<LikesToPostsModel> GetLikes(int idauthor,int? idpost);
        LikesToPostsModel GetLike(int id);

        void AddLike(LikesToPostsModel like);
        void DeleteLike(int id);
    }
}
