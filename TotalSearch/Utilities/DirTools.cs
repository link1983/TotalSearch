using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace TotalSearch.Utilities
{
    public class DirTools
    {
        List<string> FilesList = new List<string>();
        List<string> DirectoriesList;

        public List<string> GetAllDirectories(string path)
        {
            DirectoriesList = new List<string>();
            GetAllDirectories(path, ref DirectoriesList);
            return DirectoriesList;
        }

        public List<string> GetAllFiles(string path)
        {
            List<string> DirectoriesList = GetAllDirectories(path);

            int taskNums = 1;
            if (DirectoriesList.Count > 20)
                taskNums = 1;
            Action a = new Action(GetFiles_Action);
            Task[] t = new Task[taskNums];
            for (int i = 0; i < taskNums; i++)
            {
                t[i] = new Task(a);
                t[i].Start();
            }
            Task.WaitAll(t);
            return FilesList;
        }

        void GetFiles_Action()
        {
            while(DirectoriesList.Count!=0)
            {
                    DirectoryInfo di = new DirectoryInfo(DirectoriesList[0]);
                    DirectoriesList.RemoveAt(0); //立刻删除头元素，否则会被其它线程获取到同一元素，没有锁，有隐患。

                    foreach (var f in di.GetFiles())
                    {
                        FilesList.Add(f.FullName);
                    }
            }
        }

        void GetAllDirectories(string path, ref List<string> DirectoriesList)
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
