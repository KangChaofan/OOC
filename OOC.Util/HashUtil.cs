using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace OOC.Util
{
    public class HashUtil
    {
        private static MD5 md5 = MD5.Create();

        public static string MD5Hash(string text)
        {
            byte[] source = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                sBuilder.Append(source[i].ToString("x2"));
            }
            return sBuilder.ToString(); 
        }
    }
}