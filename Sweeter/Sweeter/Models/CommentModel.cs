using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sweeter.Models
{
    public class CommentModel
    {
        public int IDcomment { get; set; }
        public int IDpost { get; set; }
        public string Text { get; set; }
        public int LikesNumber { get; set; }
        public AccountModel Author { get; set; }
    }
}