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
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "ds";




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
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "ds";
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
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "ds";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FilesMonitor fm = new FilesMonitor();
            textBox1.Text = fm.SyncFiles();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
