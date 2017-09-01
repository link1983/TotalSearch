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
        public string supportedFileTypies = "";

        public ParserManager()
        {
            //每增加一个parser，就在这里添加,最后一个不加“|”，否者会多出一个空的后缀名
            TxtParser tp = new TxtParser();
            WordParser wp = new WordParser();
            ExcelParser ep = new ExcelParser();
            supportedFileTypies += tp.supportedFileTypies + "|";
            supportedFileTypies += wp.supportedFileTypies + "|";
            supportedFileTypies += ep.supportedFileTypies;
        }

        public void ParseAll()
        {
            TxtParser tp = new TxtParser();
            tp.Parse();
            WordParser wp = new WordParser();
            wp.Parse();
            ExcelParser ep = new ExcelParser();
            ep.Parse();
        }

        /// <summary>
        /// 获取各种支持类型的文件数量信息
        /// </summary>
        /// <returns></returns>
        public string GetSupportedFilesCount()
        {
            string[] Typies = supportedFileTypies.Split('|');
            SqliteHelper sqlHelper = new SqliteHelper();

            string result = "";
            foreach (var t in Typies)
            {
                string count1 = sqlHelper.ExecuteScalar($"select count(*) from files where fullname like '%{t}'").ToString();
                string count2 = sqlHelper.ExecuteScalar($"select count(*) from files where fullname like '%{t}' and (parsetime is null or parsetime='')").ToString();

                result = result + t + ":" + count2+"/"+count1+" ";
            }
            return result;
        }

    }
}
