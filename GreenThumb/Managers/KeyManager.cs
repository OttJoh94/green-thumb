using System.IO;
using System.Security.Cryptography;

namespace GreenThumb.Managers
{
    internal static class KeyManager
    {
        public static string GetEncryptionKey()
        {

            //string location = Path.Combine(Directory.GetCurrentDirectory(), "key.txt");

            if (File.Exists("C:\\Users\\ottol\\Desktop\\GreenTumbKey.txt"))
            {
                return File.ReadAllText("C:\\Users\\ottol\\Desktop\\GreenTumbKey.txt");
            }
            else
            {
                string key = GenerateEncryptionKey();
                File.WriteAllText("C:\\Users\\ottol\\Desktop\\GreenTumbKey.txt", key);

                return key;
            }
        }

        private static string GenerateEncryptionKey()
        {
            var rng = new RNGCryptoServiceProvider();
            var byteArray = new byte[16];
            rng.GetBytes(byteArray);
            return Convert.ToBase64String(byteArray);
        }
    }
}
