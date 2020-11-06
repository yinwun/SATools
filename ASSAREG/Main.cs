using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ASSAREG
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnCWD_Click(object sender, EventArgs e)
        {
            ChangPWD changPWD = new ChangPWD();
            changPWD.ShowDialog();
        }

        private void btnNewACT_Click(object sender, EventArgs e)
        {
            WebIE webIE = new WebIE();
            webIE.ShowDialog();
        }
    }
}
