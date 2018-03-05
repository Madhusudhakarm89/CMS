using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace CMS.Utilities.Common
{
    public static class EncryptDecrypt
    {
        static string sEncryptionKey = "claims";
        public static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

        public static string Decrypt(string stringToDecrypt)
        {
            string DecryptString = "";
            //string sEncryptionKey = "knowledg";
            if (stringToDecrypt != null && stringToDecrypt != "")
            {
                byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
                stringToDecrypt = stringToDecrypt.Replace("^^", "+");
                stringToDecrypt = stringToDecrypt.Replace("___", "/");

                try
                {
                    byte[] key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    inputByteArray = Convert.FromBase64String(stringToDecrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                    DecryptString = encoding.GetString(ms.ToArray());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return DecryptString;
        }

        public static string Encrypt(string stringToEncrypt)
        {
            // string SEncryptionKey = "knowledg";
            string encryptString = "";
            if (stringToEncrypt != null && stringToEncrypt != "")
            {
                try
                {
                    byte[] key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    encryptString = Convert.ToBase64String(ms.ToArray());
                    encryptString = encryptString.Replace("+", "^^");
                    encryptString = encryptString.Replace("%2b", "^^");
                    encryptString = encryptString.Replace("/", "___");
                    if (encryptString == "oN___z+zGObSc%3d")
                    {
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return encryptString;
        }
    }
}
