using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalSearch.Utilities;
using System.IO;

namespace TotalSearch.FileParsers
{
     class ParserManager
    {
        public static string supportedFileTypies = "";

        public ParserManager()
        {
            supportedFileTypies = TxtParser.supportedFileTypies;
        }
        
        List<FileInfo> FilterSupportedFiles(List<FileInfo> FilesList)
        {
            List<FileInfo> supportedFiles = new List<FileInfo>();
            foreach (var f in FilesList)
            {
                string fileName = f.FullName;
                if (fileName.Contains("."))
                {
                    string postfix = fileName.Substring(fileName.LastIndexOf("."));
                    if (supportedFileTypies.Contains(postfix))
                    {
                        supportedFiles.Add(f);
                    }
                }
            }
            return supportedFiles;
        }

        public int SaveSupportedFiles(List<FileInfo> FilesList)
        {
            List<FileInfo> supportedFiles = FilterSupportedFiles(FilesList);
            SqliteHelper sqlHelper = new SqliteHelper();
            int errorNums = 0;
            foreach (var f in supportedFiles)
            {
                try
                {
                    //排除含有'符号的文件，否则插入数据库会出错
                    if (f.FullName.Contains("'") == false) 
                    {
                        string fileMD5 = MD5Tools.GetMD5(f.FullName);
                        string fileModifiedtime = f.LastWriteTime.ToString();
                        //如果数据库中不存在，直接添加
                        if (sqlHelper.ExecuteScalar($"select count(MD5) from Files where MD5='{fileMD5}'") != 1)
                            sqlHelper.ExecuteNonQuery($"insert into Files(MD5,fullname,modifiedtime,gettime) values('{fileMD5}','{f.FullName}','{fileModifiedtime}','{DateTime.Now}')");
                        else
                        {
                            //如果数据库中存在，但是修改日期不一样，先删除，后添加
                            if(sqlHelper.ExecuteScalar($"select count(MD5) from Files where MD5='{fileMD5}' and modifiedtime<>'{fileModifiedtime}'")== 1)
                            {
                                sqlHelper.ExecuteNonQuery($"delete from Files MD5='{fileMD5}'");
                                sqlHelper.ExecuteNonQuery($"insert into Files(MD5,fullname,modifiedtime,gettime) values('{fileMD5}','{f.FullName}','{fileModifiedtime}','{DateTime.Now}')");
                            }
                        }
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
