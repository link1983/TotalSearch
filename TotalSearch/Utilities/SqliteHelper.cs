using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace TotalSearch.Utilities
{
    class SqliteHelper
    {
        public static string connString = "Data Source=c:\\FileIndex.sqlite3";
        SQLiteConnection myConn = new SQLiteConnection();

        public SqliteHelper()
        {
            myConn.ConnectionString = connString;
        }
        public int ExecuteScalar(string strSQL)
        {
            SQLiteCommand sqlCmd = new SQLiteCommand(strSQL, myConn); 
            myConn.Open();
            int rs = Convert.ToInt32(sqlCmd.ExecuteScalar());
            myConn.Close();
            return rs;
        }

        public DataSet QueryBySQL(string strSQL)
        {
            DataSet ds = new DataSet();
            myConn.Open();
            SQLiteDataAdapter sda = new SQLiteDataAdapter(strSQL, myConn);
            sda.Fill(ds, "ds");
            myConn.Close();
            return ds;
        }

        public int ExecuteNonQuery(string strSQL)
        {
            SQLiteCommand sqlCmd = new SQLiteCommand(strSQL, myConn);
            myConn.Open();
            int rs = sqlCmd.ExecuteNonQuery();
            //cmd必须dispose掉，否者在大量数据循环调用时，占用大量内存，GC在循环完了后才去释放。
            sqlCmd.Dispose(); 
            myConn.Close();
            return rs;
        }

    }
}
