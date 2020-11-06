using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CopyFiles
{
    class Program
    {
        private static String HOSTPathFile = System.Configuration.ConfigurationManager.AppSettings["HOSTPath"];
        private static String SharePath = System.Configuration.ConfigurationManager.AppSettings["SharePath"];
        private static String HOSTASSA = System.Configuration.ConfigurationManager.AppSettings["HOSTASSA"];
        static void Main(string[] args)
        {
            try
            {
                // CopyFile();
                CopyFile_xp_xz();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                Console.Read();

            }

        }

        private static void CopyFile_xp_xz()
        {
            String src = @"F:\SRC\NW_2";
            String dest = @"Y:\xp_xz\PC{0}\脚本\NW_2";
            String root = @"Y:\xp_xz";
            if (!Directory.Exists(src))
            {
                Console.WriteLine("Source folder does not exist!");
                return;
            }
            String[] files = Directory.GetFiles(src);

            String[] dirs = Directory.GetDirectories(root);

            foreach(String s in dirs)
            {
                String destPath = Path.Combine(s, @"脚本\NW_2");
                foreach(String f in files)
                {
                    string fileName = Path.GetFileName(f);
                    File.Copy(f, Path.Combine(destPath, fileName), true);
                    Console.WriteLine(destPath);
                }
            }

        }


        private static void CopyFile()
        {
            String hostFile = File.ReadAllText(HOSTPathFile);
            String shareFilePath = String.Format("{0}/{1}", SharePath, hostFile);//Z:\ASSA_AUTO\ASSA_1_04_05
            String[] dirs = Directory.GetDirectories(shareFilePath);
            for (int i = 1; i <= dirs.Length; i++)
            {
                String deshostFolder = String.Empty;
                String srchostFolder = String.Empty;
                if (i == 6 || i == 1) continue;
                if (i == 10)
                {
                    srchostFolder = String.Format("{0}/ASSA_{1}", shareFilePath, i);
                    deshostFolder = String.Format("{0}{1}/脚本/HD/1", HOSTASSA, i);
                }
                else
                {
                    srchostFolder = String.Format("{0}/ASSA_0{1}", shareFilePath, i);
                    deshostFolder = String.Format(@"{0}0{1}\脚本\HD\1", HOSTASSA, i);
                }

                CopyFile(srchostFolder, deshostFolder);
            }

        }

        /// <summary>
                /// 拷贝目录下的所有文件到目的目录。
                /// </summary>
                /// <param >源路径</param>
                /// <param >目的路径</param>
        private static void CopyFile(String srcpath, String desPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(srcpath);
            System.IO.FileInfo[] files = dirInfo.GetFiles();
            foreach (System.IO.FileInfo file in files)
            {
                string sourceFileFullName = file.FullName;
                String destFileFullName = Path.Combine(desPath, file.Name);
                file.CopyTo(destFileFullName, true);
            }
        }


    }
}
