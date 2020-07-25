using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Delivery.Core
{
    public class Seguridad
    {
        public static string Encriptar(string textoSinEncriptar, string llave)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(textoSinEncriptar);
            MD5CryptoServiceProvider cryptoServiceProvider1 = new MD5CryptoServiceProvider();
            byte[] hash = cryptoServiceProvider1.ComputeHash(Encoding.UTF8.GetBytes(llave));
            cryptoServiceProvider1.Clear();
            TripleDESCryptoServiceProvider cryptoServiceProvider2 = new TripleDESCryptoServiceProvider();
            cryptoServiceProvider2.Key = hash;
            cryptoServiceProvider2.Mode = CipherMode.ECB;
            cryptoServiceProvider2.Padding = PaddingMode.PKCS7;
            byte[] inArray = cryptoServiceProvider2.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            cryptoServiceProvider2.Clear();
            return Convert.ToBase64String(inArray, 0, inArray.Length);
        }

        public static string Desencriptar(string textoEncriptado, string llave)
        {
            byte[] inputBuffer = Convert.FromBase64String(textoEncriptado);
            MD5CryptoServiceProvider cryptoServiceProvider1 = new MD5CryptoServiceProvider();
            byte[] hash = cryptoServiceProvider1.ComputeHash(Encoding.UTF8.GetBytes(llave));
            cryptoServiceProvider1.Clear();
            TripleDESCryptoServiceProvider cryptoServiceProvider2 = new TripleDESCryptoServiceProvider();
            cryptoServiceProvider2.Key = hash;
            cryptoServiceProvider2.Mode = CipherMode.ECB;
            cryptoServiceProvider2.Padding = PaddingMode.PKCS7;
            byte[] bytes = cryptoServiceProvider2.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            cryptoServiceProvider2.Clear();
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
