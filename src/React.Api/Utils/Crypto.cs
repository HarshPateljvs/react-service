using System.Security.Cryptography;
using System.Text;

namespace React.Api.Utils
{
    public static class Crypto
    {
        // Recommended to store these securely (e.g., appsettings.json, Azure KeyVault, etc.)
        private static readonly string EncryptionKey = "AVerySecretKey123"; // 16/24/32 chars for AES
        private static readonly string Salt = "S@ltValue!"; // Any string, should be random and secure

        public static string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            var key = new Rfc2898DeriveBytes(EncryptionKey, Encoding.UTF8.GetBytes(Salt));
            aes.Key = key.GetBytes(32);
            aes.IV = key.GetBytes(16);

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string encryptedText)
        {
            var buffer = Convert.FromBase64String(encryptedText);
            using var aes = Aes.Create();
            var key = new Rfc2898DeriveBytes(EncryptionKey, Encoding.UTF8.GetBytes(Salt));
            aes.Key = key.GetBytes(32);
            aes.IV = key.GetBytes(16);

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(buffer);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}
