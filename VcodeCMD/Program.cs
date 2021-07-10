using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VcodeCMD
{
    class Program
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


        static void Main(string[] args)
        {
            GenerateImage();
            //MoveItems();
            Console.ReadLine();
        }

        private static void MoveItems()
        {
            String[] files = Directory.GetFiles(@"F:\captcha\image");

            for (int i = 0; i < files.Length; i++)
            {
                String path = files[i];

                String file = @"F:\captcha\\image1\\" + Guid.NewGuid().ToString() + ".jpg";
                File.Move(path, file);
                Console.WriteLine("Move from " + path + " to " + file);

                if (i == 20000) break;
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void GenerateImage()
        {
            String[] files;
            String libFile = System.Environment.CurrentDirectory + "\\SoCode.dat";
            if (LoadWmFromFile(libFile, "1234"))
            {
                SetWmOption(6, 90);
                files = Directory.GetFiles(@"F:\captcha\image1");
            }
            else
            {
                Console.WriteLine("error");
                return;
            }

            for (int i = 0; i < files.Length; i++)
            {
                StringBuilder Result = new StringBuilder('\0', 256);

                //以下使用GetImageFromBuffer接口
                FileStream fsMyfile = File.OpenRead(files[i]);
                int FileLen = (int)fsMyfile.Length;
                byte[] Buffer = new byte[FileLen];
                fsMyfile.Read(Buffer, 0, FileLen);
                fsMyfile.Close();

                if (GetImageFromBuffer(Buffer, FileLen, Result))
                {

                    String rst = Result.ToString();
                    String path = files[i];

                    String file = @"F:\captcha\iamge_train\\" + rst + "_" + Guid.NewGuid().ToString() + ".jpg";
                    File.Copy(path, file, true);

                    Console.WriteLine("Copy from " + path + " to " + file);
                }
                else
                    Console.WriteLine("识别失败");

            }
        }
    }
}
