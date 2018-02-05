using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sweeter.Models
{
    public class LikesToPostsModel
    {
        public int IDus_post { get; set; }
        public PostsModel Post { get; set; }
        public AccountModel Author { get; set; }
    }
}
