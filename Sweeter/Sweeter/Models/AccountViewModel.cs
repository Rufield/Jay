using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sweeter.Models
{
    public class AccountViewModel
    {
        public int IDuser { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
        public IFormFile Avatar { get; set; }
        public string Password { get; set; }
        public List<PostsModel> MyPosts { get; set; }
    }
}
