using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vcode
{
    public partial class Form1 : Form
    {

        [DllImport("WmCode.dll")]
        public static extern bool LoadWmFromFile(string FilePath, string Password);

        [DllImport("WmCode.dll")]
        public static extern bool LoadWmFromBuffer(byte[] FileBuffer, int FileBufLen, string Password);

        [DllImport("WmCode.dll")]
        public static extern bool GetImageFromFile(string FilePath, StringBuilder Vcode);

        [DllImport("WmCode.dll")]
        public static extern bool GetImageFromBuffer(byte[] FileBuffer, int ImgBufLen, StringBuilder Vcode);

        [DllImport("WmCode.dll")]
        public static extern bool SetWmOption(int OptionIndex, int OptionValue);


        [DllImport("urlmon.dll", EntryPoint = "URLDownloadToFileA")]
        public static extern int URLDownloadToFile(int pCaller, string szURL, string szFileName, int dwReserved, int lpfnCB);

        private int i = 0;
        private String[] files;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load Lib
            String libFile = System.Environment.CurrentDirectory + "\\SoCode.dat";
            if (LoadWmFromFile(libFile, "1234"))
            {
                SetWmOption(6, 90);
                files = Directory.GetFiles(@"E:\AI\code");
            }
            else
            {
                MessageBox.Show("error");
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            StringBuilder Result = new StringBuilder('\0', 256);

            picBox.Image = Image.FromFile(files[i]);

            //以下使用GetImageFromBuffer接口
            FileStream fsMyfile = File.OpenRead(files[i]);
            int FileLen = (int)fsMyfile.Length;
            byte[] Buffer = new byte[FileLen];
            fsMyfile.Read(Buffer, 0, FileLen);
            fsMyfile.Close();

            if (GetImageFromBuffer(Buffer, FileLen, Result))
                txtResult.Text = Result.ToString();
            else
                MessageBox.Show("识别失败");

            i++;
        }

        private void btnHTTP_Click(object sender, EventArgs e)
        {
            //创建
            String url = "http://reg.shiqi.me/register2.htm";
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            //设置请求方法
            httpWebRequest.Method = "GET";
            //请求超时时间
            httpWebRequest.Timeout = 20000;
            //发送请求
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //利用Stream流读取返回数据
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
            //获得最终数据，一般是json
            string responseContent = streamReader.ReadToEnd();
            streamReader.Close();
            httpWebResponse.Close();
        }
    }
}
