using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;


namespace Sweeter.Models
{
    public class AccountModel
    {
        public int IDuser { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Password { get; set; }
        public List<PostsModel> MyPosts { get; set; }
    }


}
