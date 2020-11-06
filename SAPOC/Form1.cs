using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;

namespace SAPOC
{
    public partial class Form1 : Form
    {
        #region API

        //从指定内存中读取字节集数据
        [DllImportAttribute("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, IntPtr lpNumberOfBytesRead);

        //从指定内存中写入字节集数据
        [DllImport("Kernel32.dll")]
        public static extern int WriteProcessMemory(IntPtr hProcess, IntPtr addrMem, StringBuilder strBuffer, int intSize, int filewriten);

        //打开一个已存在的进程对象，并返回进程的句柄
        [DllImportAttribute("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        //关闭一个内核对象。其中包括文件、文件映射、进程、线程、安全和同步对象等。
        [DllImport("kernel32.dll")]
        private static extern void CloseHandle(IntPtr hObject);

        #endregion


        private static byte[] result = new byte[1024];
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClickMe_Click(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < 2; i++)
            {
                TestSock();
            }*/
            //GenerateImage();
            //LoadIMG();
           MessageBox.Show(encoding("503316480"));
        }

        private void GenerateImage()
        {
            string txtFileName = "52440100640000001e00000093030000906080019f90602a019f906020019f906042019f8f60019f906028029f9f906010019f906018019f8460029f9f906015019f90601e029f9f906026019f8360029f9f906023019f8960019f8a60839f8a60869f906013049f609f9f90603208a79fa09f9fa0a1a5836010109f9f6060a7a2a09f9fa0a1a3a39f9fa28f9f04a76060a7849f03a0a1a590602e01a6889f05a0a7609f608d9f04a26060a2849f84608f9f02a0a790602c01a1849f01a0849f02a0a7836001a08b9f04a76060a3849f01a68360869f01a18a9f01a790602b849f04a36060a1839f04a26060a38c9f04a66060a5849f01a58360849f02a760849f04a36060a1839f01a290602b01a0839f04a66060a6839f04a06060a1839f03a0a6a7869f04a06060a7849f03a26060859f03a760a0839f04a66060a6839f01a08960019f90602801a5849f026060849f03a560a2879f03a560608c9f04a760609f8560859f90603002a6a0849f03a16060849f07a66060a39fa1a3849f036060a0899f02a0a68360019f836002a6a0849f01a190603001a7849f05a0a66060a0839f01a5836003a760a7909f1104a1a7609f846001a7849f02a0a6906031859f04a56060a2839f01a186608b9f04a66060a3839f03a0a79f8560859f01a5859f906028019f846002a7a1839f03a660a6849f01a6846001a7849f03a760a6849f01a5836001a0839f02a39f866001a7849f01a690602b01a5839f04a36060a6839f04a16060a0849f05a3a79fa6a1849f036060a7869f036060a0839f03a060a5839f04a36060a6839f01a190602c07a09f9fa0a760a5849f036060a68c9f01a18360849f02a260879f096060a09f9fa0a760a5849f90602c01a5849f01a0849f01a1836001a58b9f01a7836001a1849f02a0a0859f04a06060a5849f01a0849f01a190602d01a3889f01a6846001a6899f01a5846001a28d9f036060a3889f01a68360019f90602a08a7a3a09f9fa0a2a784600d9f9f60a6a2a09f9fa0a3a79f9f836001a3889f1010a0a2a660609f9f60a7a3a09f9fa0a2a790601e019f90601a029f9f8d60029f9f906011839f90603d029f9f8460909f1d06609f9f609f9f906025909f1c8460019f8a60029f9f8b60029f9f8560029f9f906034029f9f90601a909f108460839f90602a899f8c608f9f03609f9f8460019f8e60029f9f906020889f8360029f9f90601d019f8460849f906011029f9f906027029f9f906024029f9f90603a029f9f906025019f90608a";
            Base64StringToImage(txtFileName);
        }

        private void Base64StringToImage(string txtFileName)
        {
            try
            {
                FileStream ifs = new FileStream(txtFileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(ifs);

                String inputStr = sr.ReadToEnd();
                byte[] arr = Convert.FromBase64String(inputStr);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                //bmp.Save(txtFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //bmp.Save(txtFileName + ".bmp", ImageFormat.Bmp);
                //bmp.Save(txtFileName + ".gif", ImageFormat.Gif);
                //bmp.Save(txtFileName + ".png", ImageFormat.Png);
                ms.Close();
                sr.Close();
                ifs.Close();
                this.pictureBox1.Image = bmp;
                if (File.Exists(txtFileName))
                {
                    File.Delete(txtFileName);
                }
                //MessageBox.Show("转换成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Base64StringToImage 转换失败\nException：" + ex.Message);
            }
        }

        private void TestSock()
        {
            //设定服务器IP地址
            IPAddress ip = IPAddress.Parse("116.10.184.160");
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(ip, 9068)); //配置服务器IP与端口
                Console.WriteLine("连接服务器成功");
            }
            catch
            {
                Console.WriteLine("连接服务器失败，请按回车键退出！");
                return;
            }
            //通过clientSocket接收数据
            int receiveLength = clientSocket.Receive(result);
            richTextBox1.Text += Encoding.ASCII.GetString(result, 0, receiveLength) + "\n";

            Console.WriteLine("发送完毕，按回车键退出");
            Console.ReadLine();

        }


        public delegate int LoadBMP(String img);//编译
        private void LoadIMG()
        {
            //IntPtr p = LoadBMP(@"E:\ASSA\2_Mo.bmp");

            DllInvoke dll = new DllInvoke(@"E:\Sandbox\SATools\SAPOC\FindPicture5_04.dll");
            LoadBMP obj = (LoadBMP)dll.Invoke("LoadBMP", typeof(LoadBMP));
        }
          

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("1");
        }

        //将值写入指定内存地址中
        public void WriteMemoryValue()
        {

            try
            {
                //WriteProcessMemory(IntPtr hProcess, IntPtr addrMem, StringBuilder strBuffer, int intSize, int filewriten);
                int userName = 0x0454F278;
                int pwd = 0x0455AA58;
                String value = "yinwun110";
                String value1 = "123456a";
                StringBuilder sb = new StringBuilder(value);
                StringBuilder sb1 = new StringBuilder(value1);
                //打开一个已存在的进程对象  0x1F0FFF 最高权限
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, 26032);
                //从指定内存中写入字节集数据
                WriteProcessMemory(hProcess, (IntPtr)userName, sb, sb.ToString().Length, 1);
                WriteProcessMemory(hProcess, (IntPtr)pwd, sb1, sb1.ToString().Length, 1);
                //关闭操作
                CloseHandle(hProcess);
            }
            catch { }


        }

        public String encoding(String str)
        {
            if (str == null)
                return null;
            if (str.Length <= 1)
                return str;

            StringBuilder sb = new StringBuilder("");

            char pre_c = str[0];

            int cnt = 1;
            for (int i = 1; i < str.Length; i++)
            {
                char c = str[i];
                if (c == pre_c)
                {
                    cnt++;
                    continue;
                }
                else
                {
                    sb.Append(cnt.ToString());
                    sb.Append(c);
                    cnt = 1;
                    pre_c = c;
                }
            }
            sb.Append(cnt.ToString());


            return sb.ToString();
        }

    }

}





