using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalSearch.Utilities;
using System.IO;
using System.Data;

namespace TotalSearch.FileParsers
{
    class FilesMonitor
    {
        string supportedFileTypies = "";

        public FilesMonitor()
        {
            supportedFileTypies = TxtParser.supportedFileTypies;
        }

        public void AddDirectories(string path)
        {
            SqliteHelper sqlHelper = new SqliteHelper();
            if (sqlHelper.ExecuteScalar($"select count(*) from Directories where dir='{path}'") != 1)
                sqlHelper.ExecuteNonQuery($"insert into Directories(dir) values('{path}')");                
        }

        public DataSet GetDirectories()
        {
            SqliteHelper sqlHelper = new SqliteHelper();
            return sqlHelper.QueryBySQL("select dir from Directories");
        }

        public void DeleteDirectories(string dir)
        {
            SqliteHelper sqlHelper = new SqliteHelper();
            sqlHelper.ExecuteNonQuery($"delete from Directories where dir='{dir}'");
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

        List<FileInfo> GetAllDirectoriesFiles()
        {
            List<FileInfo> allDirectoriesFiles = new List<FileInfo>();

            SqliteHelper sqlHelper = new SqliteHelper();
            DataSet ds = sqlHelper.QueryBySQL("select dir from Directories");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                allDirectoriesFiles.AddRange(DirTools.GetAllFiles(dr[0].ToString()));
            }
            return allDirectoriesFiles;

        }

        public int SyncFiles()
        {
            
            List<FileInfo> ls = new List<FileInfo>();
            ls = GetAllDirectoriesFiles();
            ls = FilterSupportedFiles(ls);
            SqliteHelper sqlHelper = new SqliteHelper();

            //保存最新的所有文件
            sqlHelper.ExecuteNonQuery($"delete from LatestFiles");
            foreach (var f in ls)
            {
                string fileMD5 = MD5Tools.GetMD5(f.FullName);
                sqlHelper.ExecuteNonQuery($"insert into LatestFiles(md5,fullname) values('{fileMD5}','{f.FullName}')");
            }

            //删除过期的文件
            sqlHelper.ExecuteNonQuery($"delete from files where md5 not in (select md5 from LatestFiles)");

            //添加或更新文件
            int errorNums = 0;
            foreach (var f in ls)
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
                            if (sqlHelper.ExecuteScalar($"select count(MD5) from Files where MD5='{fileMD5}' and modifiedtime<>'{fileModifiedtime}'") == 1)
                            {
                                sqlHelper.ExecuteNonQuery($"delete from Files where MD5='{fileMD5}'");
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
