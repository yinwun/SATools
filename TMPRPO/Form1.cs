using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TMPRPO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateFunction();
        }

        private void GenerateFunction()
        {
            String str = richTextBox1.Text.Trim();
            String[] strs = str.Split('\n');

            StringBuilder sb = new StringBuilder();
            foreach(String s in strs)
            {
                if (String.IsNullOrEmpty(s)) continue;
                String output = String.Format("label {0}\r\npause\r\ngoto 开始\r\nend", s);
                sb.Append(output);
                sb.Append("\r\n");

            }

            richTextBox2.Text = sb.ToString();
        }
    }
}
