﻿using System;
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
            //每增加一个parser，就在这里添加
            TxtParser tp = new TxtParser();
            supportedFileTypies += tp.supportedFileTypies;
        }
        
    }
}
