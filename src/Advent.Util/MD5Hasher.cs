using System.Security.Cryptography;
using System.Text;

namespace Advent.Util
{
    public class MD5Hasher
    {
        private readonly MD5 algorithm = MD5.Create();

        public string Hash(string input)
        {
            byte[] hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(input));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
