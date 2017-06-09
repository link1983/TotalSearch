﻿using System;
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
        public void Parse()
        {
            string[] Typies = supportedFileTypies.Split('|');
            List<DataSet> files = new List<DataSet>();
            SqliteHelper sqlHelper = new SqliteHelper();
            foreach (var t in Typies)
            {
                files.Add(sqlHelper.QueryBySQL($"select md5,fullname,parsetime from files where fullname like '%{t}'"));
            }
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
        public virtual string GetString(string fullname)
        {
            return "Foo";
        }
    }
}
