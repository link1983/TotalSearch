using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using TotalSearch.Utilities;

namespace TotalSearch.FileParsers
{
    class TxtParser:Parser
    {

        public TxtParser()
        {
            supportedFileTypies = ".txt|.py|.cs|.log|.js|.html|.htm|.css|.xml|.sql";
        }

        protected override string GetString(string fullname)
        {
            string str = File.ReadAllText(fullname);
            //默认会使用utf-8读取文件，不管有没有BOM的UTF-8都不会出现乱码，
            //但是遇到ANSI的GBK时会乱码，且肯定会出现�字符，利用这个做判断。
            if (str.Contains("�"))
                str = File.ReadAllText(fullname,Encoding.GetEncoding("GBK"));
            return str;
        }

    }
}
