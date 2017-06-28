using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace TotalSearch.FileParsers
{
    class WordParser:Parser
    {
        public WordParser()
        {
            supportedFileTypies = ".doc|.docx";
        }
        protected override string GetString(string fullname)
        {
            try
            {
                Application WordApp = new Application();
                Document WordDoc = new Document();
                Object file = fullname;
                Object readOnly = true;
                WordApp.Visible = false;
                object missing = System.Reflection.Missing.Value;
                WordDoc = WordApp.Documents.Open(ref file, ref missing, ref readOnly,
               ref missing, ref missing, ref missing, ref missing, ref missing,
               ref missing, ref missing, ref missing, ref missing, ref missing,
               ref missing, ref missing, ref missing);
                //    m_word.Documents.Open(ref filefullname,
                //ref confirmConversions, ref readOnly, ref addToRecentFiles,
                //ref passwordDocument, ref passwordTemplate, ref revert,
                //ref writePasswordDocument, ref writePasswordTemplate,
                //ref format, ref encoding, ref visible, ref openConflictDocument,
                //ref openAndRepair, ref documentDirection, ref noEncodingDialog
                //);
                string content = WordDoc.Content.Text;
                WordDoc.Close();
                WordApp.Quit();
                return content;
            }
            catch
            {
                return "!false!";
            }
        }
    }
}
