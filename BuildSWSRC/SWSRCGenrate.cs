using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BuildSWSRC
{
    public partial class SWSRCGenrate : Form
    {
        private string RunningPath = System.AppDomain.CurrentDomain.BaseDirectory;
        public SWSRCGenrate()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Convert();
        }

        private void Convert()
        {
            String input = rtbInput.Text;
            if(String.IsNullOrEmpty(input))
            {
                MessageBox.Show("NO Data!");
                return;
            }

            String[] inputs = input.Split('\n');
            String assaPath = String.Format(@"{0}\{1}", RunningPath, @"Template\ASSA.txt");
            String xzPath = String.Format(@"{0}\{1}", RunningPath, @"Template\XZ.txt");
            String strASSA = File.ReadAllText(assaPath);
            String strXZ = File.ReadAllText(xzPath);
            for (int i = 1; i < 11; i++)
            {
                String[] strinfo = inputs[i - 1].Split('|');
                String nReplace = String.Format("$$${0}N$$$", i);
                String pReplace = String.Format("$$${0}P$$$", i);
                strASSA = strASSA.Replace(nReplace, strinfo[0]);
                strXZ = strXZ.Replace(nReplace, strinfo[0]);

                strASSA = strASSA.Replace(pReplace, strinfo[1]);
                strXZ = strXZ.Replace(pReplace, strinfo[1]);
            }


            rtbASSA.Text = strASSA;
            rtbXZ.Text = strXZ;
        }
    }
}
