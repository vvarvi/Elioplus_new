using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace WdS.ElioPlus.Lib.Utils
{
    public class MD5
    {
        static public string Encrypt(string EncString)
        {
            string MD5String;
            byte[] EncStringBytes;
            UTF8Encoding Encoder = new UTF8Encoding();
            MD5CryptoServiceProvider MD5Hasher = new MD5CryptoServiceProvider();

            //Converts the String to bytes
            EncStringBytes = Encoder.GetBytes(EncString);

            //Generates the MD5 Byte Array
            EncStringBytes = MD5Hasher.ComputeHash(EncStringBytes);

            MD5String = BitConverter.ToString(EncStringBytes);
            MD5String = MD5String.Replace("-", "");

            MD5String = MD5String.ToLower();
            //Returns the MD5 encrypted string to the calling object
            return MD5String;
        }
    }
}