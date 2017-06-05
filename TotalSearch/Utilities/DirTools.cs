using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TotalSearch.Utilities
{
    public class DirTools
    {
        public static List<string> GetAllDirectories(string path)
        {
            List<string> DirectoriesList = new List<string>();
            GetAllDirectories(path, ref DirectoriesList);
            return DirectoriesList;
        }

        public static List<string> GetAllFiles(string path)
        {
            List<string> FilesList = new List<string>();

            List<string> DirectoriesList = GetAllDirectories(path);
            foreach(var d in DirectoriesList)
            {
                DirectoryInfo di = new DirectoryInfo(d);
                foreach (var f in di.GetFiles())
                {
                    FilesList.Add(f.FullName);
                }
            }
            return FilesList;
        }

        static void GetAllDirectories(string path, ref List<string> DirectoriesList)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.GetDirectories().Length == 0)
            {
                DirectoriesList.Add(di.FullName);
            }
            else
            {
                foreach (var d in di.GetDirectories())
                {
                    GetAllDirectories(d.FullName, ref DirectoriesList);
                }
            }
        }
    }
}
