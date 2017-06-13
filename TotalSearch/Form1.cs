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
using System.IO;

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
            FilesMonitor fm = new FilesMonitor();
            fm.AddDirectories(path);
            DataSet ds = fm.GetDirectories();
            dataGridView1.DataSource = ds.Tables[0];
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
            FilesMonitor fm = new FilesMonitor();
            DataSet ds =  fm.GetDirectories();
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TxtParser tp = new TxtParser();
            tp.Parse();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string dir = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            FilesMonitor fm = new FilesMonitor();
            fm.DeleteDirectories(dir);
            DataSet ds = fm.GetDirectories();
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FilesMonitor fm = new FilesMonitor();
            textBox1.Text = fm.SyncFiles();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Searcher searcher = new Searcher();
            DataSet ds = searcher.Search(textBox3.Text);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[1].Width = 30;
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
        }

        private void 跳转到文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fullName = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            psi.Arguments = "/e,/select," + fullName;
            System.Diagnostics.Process.Start(psi);
        }

    }
}
