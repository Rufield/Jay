using System.Security.Cryptography;
using System.Text;

namespace Sweeter.Services.HashService
{
    public class HashService : IHashService
    {

        public string GetHashString(string ForHash)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(ForHash);

            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();

            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
    }
}
