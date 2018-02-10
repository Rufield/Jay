using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sweeter.Models
{
    public class LikesToCommentsModel
    {
        public int IDus_com { get; set; }
        public CommentModel Comment { get; set; }
        public AccountModel Author { get; set; }
    }
}
