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
    public partial class Form1 : Form
    {
        private string RunningPath = System.AppDomain.CurrentDomain.BaseDirectory;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            //BuildSRC();
            BuildSRCFromRTB();
           // MessageBox.Show("Done");
        }

        private void BuildSRC()
        {
            String rawPath = Path.Combine(RunningPath, "data.txt");
            List<int> lstSW = new List<int>();
            using (StreamReader sr = new StreamReader(rawPath, Encoding.Default))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] strTmp = line.Split(']');
                    if (!String.IsNullOrEmpty(strTmp[1]))
                    {
                        int tmpInt = 0;
                        int.TryParse(strTmp[1].Trim(), out tmpInt);
                        lstSW.Add(tmpInt - 2000);
                    }
                }
            }

            //bt 声望,0|1|120000;0|2|110000;0|3|120000;0|4|120000;0|5|110000;0|6|110000;0|7|120000;0|8|100000;0|9|100000;0|10|110000,hi,-1

            String str = String.Format("bt 声望,0|1|{0};0|2|{1};0|3|{2};0|4|{3};0|5|{4};0|6|{5};0|7|{6};0|8|{7};0|9|{8};0|10|{9},hi,-1",
                lstSW[0], lstSW[1], lstSW[2], lstSW[3], lstSW[4], lstSW[5], lstSW[6], lstSW[7], lstSW[8], lstSW[9]);

            rtxtOutPut.Text = str;
        }


        private void BuildSRCFromRTB()
        {
            List<int> lstSW = new List<int>();
            String str = rtbInput.Text.Trim();
            String[] strs = str.Split('\n');
            foreach(String s in strs)
            {
                if (!String.IsNullOrEmpty(s))
                {
                    string[] strTmp = s.Split(']');
                    if (!String.IsNullOrEmpty(strTmp[1]))
                    {
                        int tmpInt = 0;
                        int.TryParse(strTmp[1].Trim(), out tmpInt);
                        lstSW.Add(tmpInt - 2000);
                    }
                }
            }

            //String strRst = String.Format("bt 声望,0|1|{0};0|2|{1};0|3|{2};0|4|{3};0|5|{4};0|6|{5};0|7|{6};0|8|{7};0|9|{8};0|10|{9},hi,-1",
            //   lstSW[0], lstSW[1], lstSW[2], lstSW[3], lstSW[4], lstSW[5], lstSW[6], lstSW[7], lstSW[8], lstSW[9]);
            String strRst = String.Format(@"print /bt;摆摊;0|9|{0}>>0|10|{1}>>0|11|{2}>>0|12|{3}>>0|13|{4}>>0|14|{5}>>0|15|{6}>>0|16|{7}>>0|17|{8}>>0|18|{9}>>欢迎光临",
               lstSW[0], lstSW[1], lstSW[2], lstSW[3], lstSW[4], lstSW[5], lstSW[6], lstSW[7], lstSW[8], lstSW[9]);

            //print /bt;摆摊;0|9|20000>>0|10|20000>>0|11|20000>>0|12|20000>>0|13|20000>>0|14|20000>>0|15|20000>>0|16|20000>>0|17|20000>>0|18|20000>>0|19|20000>>0|20|20000>>0|21|20000>>0|22|20000>>0|23|20000>>欢迎光临
            rtxtOutPut.Text = strRst;
        }

        private void btnSWSRC_Click(object sender, EventArgs e)
        {
            SWSRCGenrate frm = new SWSRCGenrate();
            frm.ShowDialog();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            ASSATOREG frm = new ASSATOREG();
            frm.ShowDialog();
        }
    }
}
