using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ClickMaster
{
    public partial class Form1 : Form
    {
        private string RunningPath = System.AppDomain.CurrentDomain.BaseDirectory;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClickMe_Click(object sender, EventArgs e)
        {
            DllInvoke dll = new DllInvoke(@"E:\Sandbox\SATools\ClickMaster\DLL\WndEx7_71.dll");
            
        }
    }
}
