using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Sweeter.DataProviders
{
    using Models;
    public class PostDataProvider : IPostDataProvider
    {
        //string connectionString = null;
        //System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private ConnectionStrings _string;

        public PostDataProvider(IOptions<ConnectionStrings> String)
        {
            _string = String.Value;
        }

        public void AddPost(PostsModel post)
        {
            post.PublicDate = DateTime.Now;
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                sqlConnection.Execute(@"insert into PostTable(IDuser,Text,PublicDate, LikeNumber, CommentNumber)
                values (@IDauthor,@Text,@PublicDate, @LikeNumber, @CommentNumber);",
                new { IDauthor=post.Author.IDuser,Text= post.Text, PublicDate=post.PublicDate, LikesNumber= post.LikeNumber, CommentNumber=post.CommentNumber });
            }
        }

        public void DeletePost(int id)
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                sqlConnection.Execute(@"delete from PostTable where IDpost = @id", new { id = id });
            }
        }

        public PostsModel GetPost(int id)
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                var post = sqlConnection.Query<PostsModel>("select * from PostTable where IDpost = @id",new { id = id }).First();
                return post;
            }
        }

        public IEnumerable<PostsModel> GetPosts()
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            { 
                var  posts = sqlConnection.Query<PostsModel>("select * from PostTable").ToList();
                return  posts ;
            }
        }

        public IEnumerable<PostsModel> GetPostsOfAuthor(int idauthor)
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                var posts = sqlConnection.Query<PostsModel>("select * from PostTable where IDuser=@IDuser",new { IDuser = idauthor}).ToList();
                return posts;
            }
        }

        public void UpdatePost(PostsModel post)
        {
            using (var sqlConnection = new SqlConnection(_string.DefaultConnection))
            {
                sqlConnection.Execute(@"update PostTable set IDuser=@IDauthor,Text=@Text,PublicDate=@PublicDate, LikeNumber=@LikeNumber, CommentNumber=@CommentNumber where IDpost = @id;",
                new { IDauthor=post.Author.IDuser, Text= post.Text, PublicDate= post.PublicDate,LikesNumber=post.LikeNumber, CommentNumber=post.CommentNumber, id=post.IDpost });
            }
        }
    }
}
