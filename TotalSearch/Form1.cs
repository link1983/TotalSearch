﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            List<string> FilesList = Utilities.DirTools.GetAllFiles(path);
            foreach(var f in FilesList)
            {
                textBox1.Text = textBox1.Text + f + "\r\n";
            }
            textBox1.Text = textBox1.Text + FilesList.Count.ToString();
        }
    }
}