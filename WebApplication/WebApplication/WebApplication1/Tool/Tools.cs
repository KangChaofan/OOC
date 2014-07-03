using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace DBHelper
{
    public class Tools
    {
        public static string Md5(string str)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            Byte[] hashedBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(str));
            return BitConverter.ToString(hashedBytes);
        }
        /// <summary>
        /// 修改url用于分类查询
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetUrlForParam(string url)
        {
            string result = "";
            if (url.IndexOf('?') > 1)
            {
                result = url.Substring(0, url.IndexOf('?'));
            }
            else
            {
                result = url;
            }
            return result;
        }
        /// <summary>
        /// 获取url、主机部分
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetUrlHost(string url)
        {
            string result = "";
            int a = url.LastIndexOf('/');
            result = url.Substring(0, url.LastIndexOf('/')+1);
            return result;
        }

        /// <summary>
        /// 左侧分类
        /// </summary>
        /// <param name="URL">当前页面url</param>
        /// <returns></returns>
        public static string GetBelongTo(string URL)
        {            
            int startInt = URL.LastIndexOf('/') + 1;
            int endInt = URL.Length - startInt;
            string temp = URL.Substring(startInt, endInt);
            switch (temp)
            {
                case "Single_Model.aspx":
                    temp = "Single_Model_Content.aspx";
                    break;
                case "introduce.aspx":
                    temp = "introduce_Content.aspx";
                    break;
                case "ResultShow.aspx":
                    temp = "ResultShow_Content.aspx";
                    break;
            }
            return temp;
        }


    }
}
