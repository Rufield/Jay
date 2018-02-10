using System.Collections.Generic;


namespace Sweeter.DataProviders
{
    using Models;
    interface ILikesToPostsProvider
    {
        IEnumerable<LikesToPostsModel> GetLikes();
        IEnumerable<LikesToPostsModel> GetLikesOfPost(int idpost);
        IEnumerable<LikesToPostsModel> GetLikesOfAuthor(int idauthor);
        LikesToPostsModel GetLike(int id);

        void AddLike(LikesToPostsModel like);



        void DeleteLike(int id);
    }
}
