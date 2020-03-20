using System.Text;
using System.Security.Cryptography;
using System;
using ActualFileStorage.BLL.Hashers;

namespace ActualFileStorage.BLL.Passwords
{
    public class PasswordHasher : IPasswordHasher
    {
        private IHash _hash;
        public PasswordHasher(IHash hash)
        {
            _hash = hash;
        }
        public string HashPass(string password, string salt)
        {
            string fullstr = MixStrings(password, salt);
            //StringBuilder sb = new StringBuilder();
            //byte[] bytes = null;
            //using (SHA256 hash = SHA256.Create())
            //    bytes = hash.ComputeHash(_enc.GetBytes(fullstr));

            //foreach (byte b in bytes) sb.Append(b.ToString("x2"));
            return _hash.Hash(fullstr);//sb.ToString();
        }

        private string MixStrings(string a, string b) // => a + b;
        {
            if (a.Length > b.Length)
                return MixStrings(b, a);

            StringBuilder final = new StringBuilder(a.Length + b.Length);

            for (int i = 0; i < a.Length; ++i) {
                final.Append(a[i]);
                final.Append(b[i]);
            }
            final.Append(b.Substring(a.Length));

            return final.ToString();
        }
    }
}
