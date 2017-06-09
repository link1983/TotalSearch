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
    }
}
