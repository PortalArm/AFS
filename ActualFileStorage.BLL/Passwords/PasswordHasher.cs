using System.Text;
using System.Security.Cryptography;
using System;

namespace ActualFileStorage.BLL.Passwords
{
    public class PasswordHasher : IPasswordHasher
    {
        //!!!!
        private Encoding _enc = Encoding.ASCII;
        public string HashPass(string password, string salt)
        {
            string fullstr = MixStrings(password, salt);
            StringBuilder sb = new StringBuilder();
            byte[] bytes = null;
            using (SHA256 hash = SHA256.Create())
                bytes = hash.ComputeHash(Encoding.ASCII.GetBytes(fullstr));

            foreach (byte b in bytes) sb.Append(b.ToString("x2"));
            return sb.ToString();
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
