using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TotalSearch.Utilities;

namespace TotalSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            string path =  folderBrowserDialog1.SelectedPath;
            List<string> FilesList = DirTools.GetAllFiles(path);
            SqliteHelper sqlHelper = new SqliteHelper();
            int error = 0;
            foreach (var f in FilesList)
            {
                try
                {
                    if(f.Contains("'")==false)
                        sqlHelper.ExecuteNonQuery($"insert into Files(fullname,gettime) values('{f}','{DateTime.Now}')");
                }
                catch
                {
                    error = error + 1;
                    textBox1.Text = error.ToString();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            SqliteHelper.connString = "Data Source=" + openFileDialog1.FileName;
            textBox2.Text = SqliteHelper.connString;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.Text = SqliteHelper.connString;
        }
    }
}
