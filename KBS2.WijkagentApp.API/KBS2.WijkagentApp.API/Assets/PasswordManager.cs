using System;
using System.Security.Cryptography;

namespace KBS2.WijkagentApp.API.Assets
{
    public class PasswordManager
    {
        public void GenerateSaltedHash(string pw, out string hash, out string salt)
        {
            var saltBytes = new byte[64];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            salt = Convert.ToBase64String(saltBytes);

            var rfc2898 = new Rfc2898DeriveBytes(pw, saltBytes, 10000);
            hash = Convert.ToBase64String(rfc2898.GetBytes(256));
        }

        public bool VerifyPassword(string enteredPw, string hash, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var rfc2898 = new Rfc2898DeriveBytes(enteredPw, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898.GetBytes(256)) == hash;
        }
    }
}
