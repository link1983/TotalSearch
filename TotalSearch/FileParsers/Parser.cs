using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TotalSearch.Utilities;

namespace TotalSearch.FileParsers
{
    class Parser
    {
        public string supportedFileTypies="";
        /// <summary>
        /// 遍历数据库中支持的格式的文件，分析出文本内容然后更新数据库
        /// </summary>
        public void Parse()
        {
            string[] Typies = supportedFileTypies.Split('|');
            List<DataSet> files = new List<DataSet>();
            SqliteHelper sqlHelper = new SqliteHelper();
            //取出所有支持的文件
            foreach (var t in Typies)
            {
                files.Add(sqlHelper.QueryBySQL($"select md5,fullname,parsetime from files where fullname like '%{t}'"));
            }
            //遍历每个ds
            foreach (var ds in files)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //没有索引过的文件才继续下去
                    if (dr[2] is null || dr[2].ToString() == "")
                    {
                        string md5 = dr[0].ToString();
                        string content = GetString(dr[1].ToString()).Replace("'", "''");
                        sqlHelper.ExecuteNonQuery($"update files set parsetime='{DateTime.Now}',content='{content}' where md5='{md5}'");
                    }
                }
            }
        }
        /// <summary>
        /// 由各种文档分析子类去具体实现如何获取文本
        /// </summary>
        /// <param name="fullname"></param>
        /// <returns></returns>
        protected virtual string GetString(string fullname)
        {
            return "Foo";
        }
    }
}
