using Dapper;
using Sweeter.Models;
using Sweeter.Services.ConnectionFactory;
using System.Collections.Generic;
using System.Linq;

namespace Sweeter.DataProviders
{
    public class PostDataProvider : IPostDataProvider
    {
        private IConnectionFactory factory;

        public PostDataProvider(IConnectionFactory connection)
        {
            factory = connection;
        }

        public void AddPost(PostsModel post)
        {
            post.PublicDate = System.DateTime.Now;
            using (var sqlConnection = factory.CreateConnection)
            {
                sqlConnection.Execute(@"insert into PostTable(IDuser,Text,PublicDate, LikeNumber, CommentNumber)
                values (@IDauthor,@Text,@PublicDate, @LikeNumber, @CommentNumber);",
                new { IDauthor = post.Author.IDuser, Text = post.Text, PublicDate = post.PublicDate, LikesNumber = post.LikeNumber, CommentNumber = post.CommentNumber });
            }
        }

        public void DeletePost(int id)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                sqlConnection.Execute(@"delete from PostTable where IDpost = @id", new { id = id });
            }
        }

        public PostsModel GetPost(int id)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var post = sqlConnection.Query<PostsModel>("select * from PostTable where IDpost = @id",new { id = id }).First();
                return post;
            }
        }

        public IEnumerable<PostsModel> GetPosts()
        {
            using (var sqlConnection = factory.CreateConnection)
            { 
                var  posts = sqlConnection.Query<PostsModel>("select * from PostTable").ToList();
                return  posts ;
            }
        }

        public IEnumerable<PostsModel> GetPostsOfAuthor(int idauthor)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var posts = sqlConnection.Query<PostsModel>("select * from PostTable where IDuser=@IDuser",new { IDuser = idauthor}).ToList();
                return posts;
            }
        }

        public void UpdatePost(PostsModel post)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                sqlConnection.Execute(@"update PostTable set IDuser=@IDauthor,Text=@Text,PublicDate=@PublicDate, LikeNumber=@LikeNumber, CommentNumber=@CommentNumber where IDpost = @id;",
                new { IDauthor=post.Author.IDuser, Text= post.Text, PublicDate= post.PublicDate,LikesNumber=post.LikeNumber, CommentNumber=post.CommentNumber, id=post.IDpost });
            }
        }
    }
}
