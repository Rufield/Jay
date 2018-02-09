using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Sweeter.Models;
using Dapper;
using Microsoft.Extensions.Options;

namespace Sweeter.DataProviders
{

    public class CommentDataProvider : ICommentDataProvider
    {
        //string connectionString = null;
        //System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private ConnectionStrings _string;

        public CommentDataProvider(IOptions<ConnectionStrings> String)
        {
            _string = String.Value;
        }

        public void AddComment(CommentModel comment)
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                sqlConnection.Execute(@"insert into CommentTable(IDpost, IDuser,Text, LikeNumber)
                values (@IDpost, @IDauthor,@Text, @LikeNumber);",
                new { IDpost=comment.Post.IDpost, IDauthor=comment.Author.IDuser, Text=comment.Text, LikesNumber=comment.LikeNumber });
            }
        }

        public CommentModel GetComment(int id)
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                var comment = sqlConnection.Query<CommentModel>("select * from CommentTable where IDcomment = @id", new { id = id }).First();
                return comment;
            }
        }

        public IEnumerable<CommentModel> GetComments()
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                var comments = sqlConnection.Query<CommentModel>("select * from CommentTable").ToList();
                return comments;
            }
        }

        public IEnumerable<CommentModel> GetCommentsOfPost(int idpost)
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                var comments = sqlConnection.Query<CommentModel>("select * from CommentTable where IDpost=@idpost",new { idpost = idpost }).ToList();
                return comments;
            }
        }

        public void UpdateComment(CommentModel comment)
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                sqlConnection.Execute(@"update CommentTable set IDpost=@IDpost, IDuser=@IDauthor,Text=@Text, LikeNumber=@LikeNumber where IDcomment = @id;",
                new {IDpost= comment.Post.IDpost, IDauthor=comment.Author.IDuser,Text=comment.Text,LikesNumber=comment.LikeNumber, id=comment.IDcomment });
            }
        }

        public void DeleteComment(int id)
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                sqlConnection.Execute(@"delete from CommentTable where IDcomment = @id",new { id = id });
            }
        }
    }
}
