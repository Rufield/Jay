using Dapper;
using Sweeter.Models;
using Sweeter.Services.ConnectionFactory;
using System.Collections.Generic;
using System.Linq;

namespace Sweeter.DataProviders
{
    public class LikesToPostsProvider : ILikesToPostsProvider
    {
        private IConnectionFactory factory;

        public LikesToPostsProvider(IConnectionFactory connection)
        {
            factory = connection;
        }

        public void AddLike(LikesToPostsModel like)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                sqlConnection.Execute(@"insert into LikesToPostTable(IDuser,IDpost)
                values (@IDauthor,@IDpost);",
                new {  IDauthor=like.Author.IDuser, IDpost=like.Post.IDpost });
            }
        }

        public void DeleteLike(int id)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                sqlConnection.Execute(@"delete from LikesToPostTable where IDus_post = @id",new { id = id });
            }
        }

        public LikesToPostsModel GetLike(int id)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var like = sqlConnection.Query<LikesToPostsModel>("select * from LikesToPostTable where IDus_post = @id", new { id = id }).First();
                return like;
            }
        }

        public IEnumerable<LikesToPostsModel> GetLikes()
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var likes = sqlConnection.Query<LikesToPostsModel>("select * from LikesToPostTable").ToList();
                return likes;
            }
        }

        public IEnumerable<LikesToPostsModel> GetLikesOfPost(int idpost)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var likes = sqlConnection.Query<LikesToPostsModel>("select * from LikesToPostTable where IDpost=@idpost", new { idpost = idpost }).ToList();
                return likes;
            }
        }

        public IEnumerable<LikesToPostsModel> GetLikesOfAuthor(int idauthor)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var likes = sqlConnection.Query<LikesToPostsModel>("select * from LikesToPostTable where IDuser=@iduser",new { iduser = idauthor }).ToList();
                return likes;
            }
        }
    }
}
