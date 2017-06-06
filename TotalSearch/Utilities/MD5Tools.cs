using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace TotalSearch.Utilities
{
    class MD5Tools
    {

        public static string GetMD5(string input)
        {
            byte[] result = Encoding.Default.GetBytes(input.Trim());    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密文本的文本框
        }
    }
}
