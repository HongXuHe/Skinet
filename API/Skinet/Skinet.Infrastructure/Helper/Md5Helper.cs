using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Skinet.Infrastructure.Helper
{
   public class Md5Helper
    {
        public static string Encode(string original)
        {
            byte[] encodedBytes;

            using (var md5 = new MD5CryptoServiceProvider()) {
                var originalBytes = Encoding.Default.GetBytes(original);
                encodedBytes = md5.ComputeHash(originalBytes);
            }

            return Convert.ToBase64String(encodedBytes);
        }
    }
}
