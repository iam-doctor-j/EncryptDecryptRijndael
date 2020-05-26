using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptDecrypt
{
    class EncryptDecrypt
    {
        private string key = "bff4c9a7296d865d98e84a5b4f8cbb05";
        private string salt = "3e9b3ac6f082ec46a2d1d3f1f1647456";
        public string Encrypt(string str)
        {
            byte[] encrypted = null;
            byte[] strBytes = Encoding.UTF8.GetBytes(str);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(key);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            using(MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 500);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    using(var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(strBytes, 0, strBytes.Length);
                    }
                }

                encrypted = ms.ToArray();
            }

            return Convert.ToBase64String(encrypted); 
        }

        public string Decrypt(string str)
        {
            byte[] decrypted = null;
            byte[] strBytes = Convert.FromBase64String(str);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(key);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 500);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(strBytes, 0, strBytes.Length);
                    }
                }

                decrypted = ms.ToArray();
            }

            return Encoding.UTF8.GetString(decrypted); 
        }
    }
}
