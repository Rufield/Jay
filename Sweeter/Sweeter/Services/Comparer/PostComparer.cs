using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sweeter.Models;

namespace Sweeter.Services.Comparer
{
    public class PostComparer:IEqualityComparer<PostsModel>
    {
        public bool Equals(PostsModel x, PostsModel y)
        {
            if (x.IDpost.Equals(y.IDpost)) return true;
            else return false;
        }



        public int GetHashCode(PostsModel obj)
        {
            return obj.IDpost.GetHashCode();
        }
    }
}
