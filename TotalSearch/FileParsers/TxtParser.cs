using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using TotalSearch.Utilities;

namespace TotalSearch.FileParsers
{
    class TxtParser:Parser
    {

        public TxtParser()
        {
            supportedFileTypies = "|.txt|.py|.cs";
        }

        protected override string GetString(string fullname)
        {
            string str = File.ReadAllText(fullname,Encoding.Default);
            return str;
        }
    }
}
