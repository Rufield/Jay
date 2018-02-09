using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sweeter.Services.HashService
{
    public interface IHashService
    {
        string GetHashString(string ForHash);
    }
}
