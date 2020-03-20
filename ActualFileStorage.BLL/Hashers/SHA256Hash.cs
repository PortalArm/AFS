using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Hashers
{
    public class SHA256Hash : IHash
    {
        private static Encoding _enc = Encoding.ASCII;
        public string Hash(byte[] bytes)
        {
            byte[] result;
            using (SHA256 hash = SHA256.Create())
                result = hash.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in result) sb.Append(b.ToString("x2"));

            return sb.ToString();
        }

        public string Hash(string str)
        {
            var bytes = _enc.GetBytes(str);
            return Hash(bytes);
        }
    }
}
