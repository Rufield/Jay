using Sweeter.Models;
using System.Collections.Generic;

namespace Sweeter.DataProviders
{
    public interface IPostDataProvider
    {
        IEnumerable<PostsModel> GetPosts();
        IEnumerable<PostsModel> GetPostsOfAuthor( int idauthor);
        IEnumerable<PostsModel> GetPostsOfAuthor(int? idauthor);
        IEnumerable<PostsModel> GetPostsByCategory(int ID);
        PostsModel GetPost(int? id);
       
        void DeletePost(int? id);
        void AddPost(PostsModel post);
        void UpdatePost(PostsModel post);
    }
}
