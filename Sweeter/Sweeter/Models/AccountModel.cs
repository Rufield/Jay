using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dapper;


namespace Sweeter.Models
{
    public class AccountModel
    {
        public int IDuser { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public byte[] Avatar { get; set; }
        [Required]
        public string Password { get; set; }
        public List<PostsModel> MyPosts { get; set; }
    }


}
