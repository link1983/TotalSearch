using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace TotalSearch.FileParsers
{
    class ExcelParser:Parser
    {
        public ExcelParser()
        {
            supportedFileTypies = ".xls|.xlsx";
        }

        protected override string GetString(string fullname)
        {
            try
            {
                Application ExcelApp = new Application();
                ExcelApp.Visible = false;
                object missing = System.Reflection.Missing.Value;
                Workbook ExcelWorkBook = ExcelApp.Workbooks.Open(fullname, missing, true, missing, missing, missing,
                             missing, missing, missing, missing, missing, missing, missing, missing, missing);
                //Open(FileName, UpdateLinks, ReadOnly, Format, Password, WriteResPassword, IgnoreReadOnlyRecommended, Origin, Delimiter, Editable, Notify, Converter, AddToMru, Local, CorruptLoad)
                string content = "";
                //Excel的索引是从1开始的
                for (int i=1;i<=ExcelWorkBook.Worksheets.Count; i++)
                {
                    Worksheet ws = ExcelWorkBook.Worksheets[i];
                    for(int r = 1;r<=ws.UsedRange.Cells.Rows.Count;r++)
                    {
                        for(int c=1;c<=ws.UsedRange.Cells.Columns.Count;c++)
                        {
                            string str = ((Range)ws.UsedRange.Cells[r, c]).Text;
                            content = content + str;
                        }
                    }
                }
                ExcelWorkBook.Close();
                ExcelApp.Quit();
                return content;
            }
            catch
            {
                return "!false!";
            }
        }
    }
}
