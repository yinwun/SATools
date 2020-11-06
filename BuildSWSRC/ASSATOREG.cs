using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildSWSRC
{
    public partial class ASSATOREG : Form
    {
        private string RunningPath = System.AppDomain.CurrentDomain.BaseDirectory;
        public ASSATOREG()
        {
            InitializeComponent();
        }

        private void ASSATOREG_Load(object sender, EventArgs e)
        {

        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            Convert();
        }

        private void Convert()
        {
            String input = rtxtInput.Text.Trim();
            if (String.IsNullOrEmpty(input))
            {
                MessageBox.Show("NO Data!");
                return;
            }

            //OQd2DG05|K0ISLMBWpQ|oZJJGa|X|116.10.184.172|7021|摆摊电信4线shiqi.so
            String[] inputs = input.Split('\n');
            String output = String.Empty;
         
            for (int i = 1; i < 11; i++)
            {
                String safeCode = GenerateCheckCode(8);
                String[] strinfo = inputs[i - 1].Split('|');
                output += String.Format("{0}|{1}|{2}|X|116.10.184.172|7021|摆摊电信4线shiqi.so \n", strinfo[0], strinfo[1], safeCode);
            }


            rtxtOutput.Text = output;
        }

        private void btnASSALogon_Click(object sender, EventArgs e)
        {
            ConvertASSAM();
        }

        private void ConvertASSAM()
        {
            String input = rtxtInput.Text.Trim();
            if (String.IsNullOrEmpty(input))
            {
                MessageBox.Show("NO Data!");
                return;
            }

            //OQd2DG05|K0ISLMBWpQ|oZJJGa|X|116.10.184.172|7021|摆摊电信4线shiqi.so
            String[] inputs = input.Split('\n');
            String output = String.Empty;
            for (int i = 0; i < inputs.Length; i++)
            {
                String[] strinfo = inputs[i].Split('|');
                output += String.Format("{0}|{1}|1|MO_2_C \n", strinfo[0], strinfo[1]);
            }


            rtxtOutput.Text = output;
        }
        private int rep = 0;
        private string GenerateCheckCode(int codeCount)
        {
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + this.rep;
            this.rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> this.rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }

        private void btnPrisionBreak_Click(object sender, EventArgs e)
        {
            ConvertBreak();
        }

        private void ConvertBreak()
        {
            String input = rtxtInput.Text.Trim();
            if (String.IsNullOrEmpty(input))
            {
                MessageBox.Show("NO Data!");
                return;
            }

            String[] inputs = input.Split('\n');
            String output = String.Empty;

            for (int i = 1; i < 11; i++)
            {
                
                String[] strinfo = inputs[i - 1].Split('|');
                String str = String.Format("id{0}={1},{2}\n",i+1, strinfo[0], strinfo[1]);
                output += str;
            }


            rtxtOutput.Text = output;
        }
    }
}
