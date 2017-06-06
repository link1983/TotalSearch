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
using TotalSearch.FileParsers;

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
            ParserManager pm = new ParserManager();
            int errornums=pm.SaveSupportedFiles(FilesList);
            textBox1.Text = errornums.ToString();


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

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + ":"+DESEncrypt.Encrypt(textBox1.Text);            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + ":" + DESEncrypt.Decrypt(textBox1.Text);
        }
    }
}
