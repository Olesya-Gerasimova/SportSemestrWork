using System;
using System.Security.Cryptography;

namespace ApiServer.Helpers
{
    public class HashingHelper
    {
        public static string HashUsingPbkdf2(string password)
        {
            using var bytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String("cmR0ZnlndWl4Y3JqdHZ5a3V4d2VjcnZ0eWJ1bnlpbThvLDlwMAo="), 10000, HashAlgorithmName.SHA256);
            var derivedRandomKey = bytes.GetBytes(32);
            var hash = Convert.ToBase64String(derivedRandomKey);
            return hash;
        }
    }
}
