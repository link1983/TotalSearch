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
                Workbook ExcelWorkBook = new Workbook();
                ExcelApp.Visible = false;
                object missing = System.Reflection.Missing.Value;
                ExcelWorkBook = ExcelApp.Workbooks.Open(fullname, missing, true, missing, missing, missing,
                             missing, missing, missing, missing, missing, missing, missing, missing, missing);
                //Open(FileName, UpdateLinks, ReadOnly, Format, Password, WriteResPassword, IgnoreReadOnlyRecommended, Origin, Delimiter, Editable, Notify, Converter, AddToMru, Local, CorruptLoad)
                string content = "";
                for (int i=0;i< ExcelWorkBook.Worksheets.Count; i++)
                {
                    Worksheet ws = ExcelWorkBook.Worksheets[i];
                    for(int r = 0;r<ws.UsedRange.Rows.Count;r++)
                    {
                        for(int c=0;c< ws.UsedRange.Columns.Count;c++)
                        {
                            string str = ((Range)ws.Cells[r, c]).Text;
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
