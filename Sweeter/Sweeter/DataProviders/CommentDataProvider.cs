using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Sweeter.DataProviders
{
    using Models;
    using Dapper;
    public class CommentDataProvider:ICommentDataProvider
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;



        public void AddComment(CommentModel comment)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Execute(@"insert into CommentTable(IDpost, IDuser,Text, LikeNumber)
      values (@IDpost, @IDauthor,@Text, @LikesNumber);",
 new { IDpost=comment.Post.IDnews, IDauthor=comment.Author.IDaccount, Text=comment.Text, LikesNumber=comment.LikesNumber });
            }
        }

        public CommentModel GetComment(int id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var comment = sqlConnection.Query<CommentModel>("select * from CommentTable where IDcomment = @id", new { id = id }).First();
                return comment;
            }
        }

        public IEnumerable<CommentModel> GetComments()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var comments = sqlConnection.Query<CommentModel>("select * from CommentTable").ToList();
                return comments;
            }
        }
        public IEnumerable<CommentModel> GetCommentsOfPost(int idpost)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var comments = sqlConnection.Query<CommentModel>("select * from CommentTable where IDpost=@idpost",new { idpost = idpost }).ToList();
                return comments;
            }
        }
        public void UpdateComment(CommentModel comment)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Execute(@"update CommentTable set IDpost=@IDpost, IDuser=@IDauthor,Text=@Text, LikeNumber=@LikesNumber where IDcomment = @id;",
                  new {IDpost= comment.Post.IDnews, IDauthor=comment.Author.IDaccount,Text=comment.Text,LikesNumber=comment.LikesNumber, id=comment.IDcomment });
            }
        }

        public void DeleteComment(int id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Execute(@"delete from CommentTable where IDcomment = @id",new { id = id });
            }
        }
    }
}
