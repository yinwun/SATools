using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace ASSAREG
{
    public partial class Form1 : Form
    {
        private String[] dctAccount = 
            {
            "A","B","C","D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "a","b","c","d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
            "1","2","3","4","5","6","7","8","9","0"
        };

        private String RunningTmpFolder = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "tmp");
        private String RunningBakFolder = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "bak");
        private String DIPIPath = System.Configuration.ConfigurationManager.AppSettings["DIPIPath"];
        private String DestinationPath = System.Configuration.ConfigurationManager.AppSettings["DestinationPath"];
        private List<String> ListUserNamePwd = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindASSAGroupList();
            BindASSAList();
            //rtxtOutput.Text = "First Line\nSecond Line\nThird Line";
        }

        private void BindASSAGroupList()
        {
            lstbASSAGroupList.Items.Add("ASSA_1_04_05");
            lstbASSAGroupList.Items.Add("ASSA_1_06_07");
            lstbASSAGroupList.Items.Add("ASSA_1_08_09");
            lstbASSAGroupList.Items.Add("ASSA_1_10_11");
            lstbASSAGroupList.Items.Add("ASSA_2_04_05");
            lstbASSAGroupList.Items.Add("ASSA_2_06_07");
            lstbASSAGroupList.Items.Add("ASSA_2_08_09");
            lstbASSAGroupList.Items.Add("ASSA_2_10_11");
            lstbASSAGroupList.Items.Add("ASSA_3_04_05");
            lstbASSAGroupList.Items.Add("ASSA_4_06_07");
            lstbASSAGroupList.Items.Add("ASSA_4_08_09");
            lstbASSAGroupList.Items.Add("ASSA_4_10_11");
            lstbASSAGroupList.Items.Add("ASSA_5_04_05");
            lstbASSAGroupList.Items.Add("ASSA_5_06_07");
            lstbASSAGroupList.Items.Add("ASSA_5_08_09");
            lstbASSAGroupList.Items.Add("ASSA_5_10_11");
            lstbASSAGroupList.Items.Add("ASSA_6_04_05");
            lstbASSAGroupList.Items.Add("ASSA_6_06_07");
            lstbASSAGroupList.Items.Add("ASSA_6_08_09");
            lstbASSAGroupList.Items.Add("ASSA_6_10_11");
            lstbASSAGroupList.Items.Add("ASSA_7_04_05");
            lstbASSAGroupList.Items.Add("ASSA_7_06_07");
            lstbASSAGroupList.Items.Add("ASSA_7_08_09");
            lstbASSAGroupList.Items.Add("ASSA_7_10_11");
        }

        private void BindASSAList()
        {
            lstbASSAList.Items.Add("ASSA_02");
            lstbASSAList.Items.Add("ASSA_03");
            lstbASSAList.Items.Add("ASSA_04");
            lstbASSAList.Items.Add("ASSA_05");
            lstbASSAList.Items.Add("ASSA_07");
            lstbASSAList.Items.Add("ASSA_08");
            lstbASSAList.Items.Add("ASSA_09");
            lstbASSAList.Items.Add("ASSA_10");
        }

        private void GenerateAccout()
        {
            if (lstbASSAGroupList.SelectedItems.Count == 0)
            {
                MessageBox.Show("没有选择ASSA分组列表！ ");
                return;
            }

            if (lstbASSAList.SelectedItems.Count == 0)
            {
                MessageBox.Show("没有选择ASSA列表！ ");
                return;
            }

            string strASSAMO = String.Empty;
            string strDIPI = String.Empty;

            var items = lstbASSAList.SelectedItems;
            foreach (var item in items)
            {
                String actName = GenerateRandomStr(4, 12); //Account Name
                String actPwd = GenerateRandomStr(6, 12); //Account pwd
                String actSafeCode = GenerateRandomStr(6, 12); //safe code
                int qqNum = GenerateRandomInt(10000, 99999999);

                String strCreateAct = String.Format("{0}|{1}|{2}|{3}", actName, actPwd, actSafeCode, qqNum);
                ListUserNamePwd.Add(strCreateAct);

                int seq = 2;
                if (item.ToString().Contains("02") || item.ToString().Contains("03") || item.ToString().Contains("04") || item.ToString().Contains("05"))
                {
                    seq = 1;
                }
                
                String[] its = item.ToString().Split('_');
                //Save as ASSAMO for ASSAMO
                //yinwun703|xiaohuilili|{0}|HD_{1}_T
                strASSAMO += String.Format("{0}|{1}|{2}|HD_{3}_T\n", actName, actPwd, its[1], seq);


                //Save as data.txt for DIPI.
                //Save in running folder tmp sub folder.
                //njuqJkPMWLW|3uYW2GD66|FwvA6PkcKo|X|116.10.184.172|7021|摆摊电信4线shiqi.so
                strDIPI += String.Format("{0}|{1}|{2}|X|116.10.184.172|7021|摆摊电信4线shiqi.so\r\n", actName, actPwd, actSafeCode);

                //生成解锁文件
                String lockTempaltePath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"Template\Lock.txt");
                String lockFile = File.ReadAllText(lockTempaltePath);
                lockFile = lockFile.Replace("$$$SAFECODE$$$", actSafeCode);
                String lockActSafeCode = Path.Combine(RunningTmpFolder, "解锁.asc");
                if(File.Exists(lockActSafeCode))
                {
                    File.Delete(lockActSafeCode);
                }

                File.WriteAllText(lockActSafeCode, lockFile, Encoding.Default);
                String actDestFolder = String.Format("{0}/{1}/{2}", DestinationPath, lstbASSAGroupList.SelectedItem.ToString(), item.ToString());
                if (!Directory.Exists(actDestFolder))
                {
                    Directory.CreateDirectory(actDestFolder);
                }

                String actDestFile = Path.Combine(actDestFolder, "解锁.asc");
                if (File.Exists(actDestFile))
                {
                    File.Delete(actDestFile);
                }

                File.Move(lockActSafeCode, actDestFile);

            }

            //save into Data.txt
            String dataFile = String.Format("{0}/{1}", RunningTmpFolder, "data.txt");
            if (!Directory.Exists(RunningTmpFolder))
            {
                Directory.CreateDirectory(RunningTmpFolder);
            }
            if (!Directory.Exists(RunningBakFolder))
            {
                Directory.CreateDirectory(RunningBakFolder);
            }
            if (File.Exists(dataFile))
            {
                File.Delete(dataFile);
            }
            File.WriteAllText(dataFile, strDIPI);

            //output 
            rtxtOutput.Text = strASSAMO;
            MessageBox.Show("Generate Done!");
        }

        private void CopyFile()
        {
            //data.txt backup folder
            if (!Directory.Exists(RunningBakFolder))
            {
                Directory.CreateDirectory(RunningBakFolder);
            }
            String dataFileDipi = Path.Combine(DIPIPath, "data.txt");
            String dataFiletmp = String.Format("{0}/{1}", RunningTmpFolder, "data.txt");
            String dataFileabk = String.Format("{0}/{1}_{2}", RunningBakFolder, DateTime.Now.ToString("yyyyMMddHHmmssffffff"), "data.txt");
            //Copy to DIPI
            File.Copy(dataFiletmp, dataFileDipi, true);
            File.Move(dataFiletmp, dataFileabk);

            //Copy specail folder 解锁
            var items = lstbASSAList.SelectedItems;
            foreach (var item in items)
            {
                String subFolderPath = item.ToString();
                String masterFolderName = lstbASSAGroupList.SelectedItem.ToString();
            }

                MessageBox.Show("Copy Done!");
        }

        private String GenerateRandomStr(int min, int max)
        {
            int stringLength = GenerateRandomInt(min, max);
            String str = String.Empty;
            for (int i = 0; i < stringLength; i++)
            {
                int tmpNum = GenerateRandomInt(0, 61);
                str += dctAccount[tmpNum];
            }
            
            return str;
        }

        private int GenerateRandomInt(int min, int max)
        {
            int randNum = 8;
            long tick = DateTime.Now.Ticks;
            Random ran = new Random(Guid.NewGuid().GetHashCode());
            randNum = ran.Next(min, max);
            return randNum;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateAccout();
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            CopyFile();
        }

        private void btnCreateAcct_Click(object sender, EventArgs e)
        {
            WebIE frm = new WebIE();
            frm.ListUserNamePwd = ListUserNamePwd;
            frm.ShowDialog();
        }

        private void btnCreateChar_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", DIPIPath);
        }
    }
}
