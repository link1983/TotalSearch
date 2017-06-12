using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalSearch.Utilities;
using System.Data;
using System.Text.RegularExpressions;

namespace TotalSearch
{
    class Searcher
    {
        public DataSet Search(string keyword)
        {
            SqliteHelper sqlHelper = new SqliteHelper();
            DataSet ds = sqlHelper.QueryBySQL($"select fullname,content from files where content like '%{keyword}%'");
            string regPattern = keyword;
            ds.Tables[0].Columns.Add("weight", typeof(System.Int32));

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dr[2] = Regex.Matches(dr[1].ToString(),regPattern).Count;
            }

            ds.Tables[0].Columns.Remove("content");
            return ds;
        }
    }
}
