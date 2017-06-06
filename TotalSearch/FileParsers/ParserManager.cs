using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalSearch.Utilities;

namespace TotalSearch.FileParsers
{
     class ParserManager
    {
        public static string supportedFileTypies = "";

        public ParserManager()
        {
            supportedFileTypies = TxtParser.supportedFileTypies;
        }
        
        List<string> FilterSupportedFiles(List<string> FilesList)
        {
            List<string> supportedFiles = new List<string>();
            foreach (var f in FilesList)
            {
                if (f.Contains("."))
                {
                    string postfix = f.Substring(f.LastIndexOf("."));
                    if (supportedFileTypies.Contains(postfix))
                    {
                        supportedFiles.Add(f);
                    }
                }
            }
            return supportedFiles;
        }

        public int SaveSupportedFiles(List<string> FilesList)
        {
            List<string> supportedFiles = FilterSupportedFiles(FilesList);
            SqliteHelper sqlHelper = new SqliteHelper();
            int errorNums = 0;
            foreach (var f in supportedFiles)
            {
                try
                {
                    if (f.Contains("'") == false)
                    {
                        sqlHelper.ExecuteNonQuery($"insert into Files(MD5,fullname,gettime) values('{MD5Tools.GetMD5(f)}','{f}','{DateTime.Now}')");
                    }
                }
                catch
                {
                    errorNums = errorNums + 1;
                }
            }
            return errorNums;
        }
    }
}
