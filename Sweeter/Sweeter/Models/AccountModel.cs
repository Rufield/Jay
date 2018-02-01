using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;


namespace Sweeter.Models
{
    public class AccountModel
    {
        public int IDaccount { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Password { get; set; }
        public List<PostsModel> MyPosts { get; set; }
    }


}
