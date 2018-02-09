using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sweeter.Models;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Sweeter.DataProviders
{
    public class LikesToPostsProvider : ILikesToPostsProvider
    {
        //string connectionString = null;
        //System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //private SqlConnection sqlConnection;
        private ConnectionStrings _string;

        public LikesToPostsProvider(IOptions<ConnectionStrings> String)
        {
            _string = String.Value;
        }

        public void AddLike(LikesToPostsModel like)
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                sqlConnection.Execute(@"insert into LikesToPostTable(IDuser,IDpost)
                values (@IDauthor,@IDpost);",
                new {  IDauthor=like.Author.IDuser, IDpost=like.Post.IDpost });
            }
        }

        public void DeleteLike(int id)
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                sqlConnection.Execute(@"delete from LikesToPostTable where IDus_post = @id",new { id = id });
            }
        }

        public LikesToPostsModel GetLike(int id)
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                var like = sqlConnection.Query<LikesToPostsModel>("select * from LikesToPostTable where IDus_post = @id", new { id = id }).First();
                return like;
            }
        }

        public IEnumerable<LikesToPostsModel> GetLikes()
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                var likes = sqlConnection.Query<LikesToPostsModel>("select * from LikesToPostTable").ToList();
                return likes;
            }
        }

        public IEnumerable<LikesToPostsModel> GetLikesOfPost(int idpost)
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                var likes = sqlConnection.Query<LikesToPostsModel>("select * from LikesToPostTable where IDpost=@idpost", new { idpost = idpost }).ToList();
                return likes;
            }
        }

        public IEnumerable<LikesToPostsModel> GetLikesOfAuthor(int idauthor)
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                var likes = sqlConnection.Query<LikesToPostsModel>("select * from LikesToPostTable where IDuser=@iduser",new { iduser = idauthor }).ToList();
                return likes;
            }
        }
    }
}
