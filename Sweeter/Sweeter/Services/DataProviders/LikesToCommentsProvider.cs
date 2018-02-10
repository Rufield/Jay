using Dapper;
using Sweeter.Models;
using Sweeter.Services.ConnectionFactory;
using System.Collections.Generic;
using System.Linq;

namespace Sweeter.DataProviders
{
    public class LikesToCommentsProvider : ILikesToCommentsProvider
    {
        private IConnectionFactory factory;

        public LikesToCommentsProvider(IConnectionFactory connection)
        {
            factory = connection;
        }

        public void AddLike(LikesToCommentsModel like)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                sqlConnection.Execute(@"insert into LikesToCommentTable(IDuser,IDcomment) values (@IDauthor, @IDcomment);",
                new { IDauthor=like.Author.IDuser, IDcomment=like.Comment.IDcomment });
            }
        }

        public void DeleteLike(int id)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                sqlConnection.Execute(@"delete from LikesToCommentTable where IDus_com = @id",new { id = id });
            }
        }

        public LikesToCommentsModel GetLike(int id)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var like = sqlConnection.Query<LikesToCommentsModel>("select * from LikesToCommentTable where IDus_com = @id", new { id = id }).First();
                return like;
            }
        }

        public IEnumerable<LikesToCommentsModel> GetLikes()
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var likes = sqlConnection.Query<LikesToCommentsModel>("select * from LikesToCommentTable").ToList();
                return likes;
            }
        }

        public IEnumerable<LikesToCommentsModel> GetLikesOfComment(int idcomment)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var likes = sqlConnection.Query<LikesToCommentsModel>("select * from LikesToCommentTable where IDcomment=@idcomment", new { idcomment = idcomment }).ToList();
                return likes;
            }
        }

        public IEnumerable<LikesToCommentsModel> GetLikesOfAuthor(int idauthor)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var likes = sqlConnection.Query<LikesToCommentsModel>("select * from LikesToCommentTable where IDuser=@iduser", new { iduser = idauthor }).ToList();
                return likes;
            }
        }
    }
}

