using Dapper;
using Sweeter.Models;
using Sweeter.Services.ConnectionFactory;
using System.Collections.Generic;
using System.Linq;

namespace Sweeter.DataProviders
{
    public class CommentDataProvider : ICommentDataProvider
    {
        private IConnectionFactory factory;

        public CommentDataProvider(IConnectionFactory connection)
        {
            factory = connection;
        }

        public void AddComment(CommentModel comment)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                sqlConnection.Execute(@"insert into CommentTable(IDpost, IDuser,Text, LikeNumder)
                values (@IDpost, @IDauthor,@Text, @LikeNumber);",
                new { IDpost=comment.Post.IDpost, IDauthor=comment.Author.IDuser, Text=comment.Text, LikeNumber=comment.LikeNumber });
            }
        }

        public CommentModel GetComment(int id)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var comment = sqlConnection.Query<CommentModel>("select * from CommentTable where IDcomment = @id", new { id = id }).First();
                return comment;
            }
        }

        public IEnumerable<CommentModel> GetComments()
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var comments = sqlConnection.Query<CommentModel>("select * from CommentTable").ToList();
                return comments;
            }
        }

        public IEnumerable<CommentModel> GetCommentsOfPost(int? idpost)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var comments = sqlConnection.Query<CommentModel>("select * from CommentTable where IDpost=@idpost",new { idpost = idpost }).ToList();
                return comments;
            }
        }

        public void UpdateComment(CommentModel comment)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                sqlConnection.Execute(@"update CommentTable set IDpost=@IDpost, IDuser=@IDauthor,Text=@Text, LikeNumber=@LikeNumder where IDcomment = @id;",
                new {IDpost= comment.Post.IDpost, IDauthor=comment.Author.IDuser,Text=comment.Text,LikeNumber=comment.LikeNumber, id=comment.IDcomment });
            }
        }

        public void DeleteComment(int id)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                sqlConnection.Execute(@"delete from CommentTable where IDcomment = @id",new { id = id });
            }
        }
    }
}
