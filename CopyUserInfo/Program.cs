using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CopyUserInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CopyUserInfo();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
           
        }

        private static void CopyUserInfo()
        {
            String xzDataPath = @"C:\NW\data.txt";
            String destinationPath = @"E:\5Z\userinfo.txt";
            String[] lines = File.ReadAllLines(xzDataPath);
            String[] str = lines[1].Split('|');
            String content = str[0] + ";" + str[1];

            Console.WriteLine(content);
            File.AppendAllText(destinationPath, content + "\r\n");


            Console.WriteLine("DONE");

            System.Threading.Thread.Sleep(1000);

        }
    }
}
