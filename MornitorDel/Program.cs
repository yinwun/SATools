using System;
using System.Collections.Generic;
using System.Text;
using XAPP.COMLIB;
using System.Configuration;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace MornitorDel
{
    class Program
    {
        private COMLIB cm = new COMLIB();
        private Dictionary<IntPtr, String> dctASSA = new Dictionary<IntPtr, String>();
        private Dictionary<IntPtr, String> dctMainWindow = new Dictionary<IntPtr, String>();
        private string RunningPath = System.AppDomain.CurrentDomain.BaseDirectory;
        private int waitTime = 1000;//mls
       

        #region 获取ASSA所有子控件
        const int WM_CLOSE = 0x0010;

        public delegate bool EnumChildWindowsProc(IntPtr hwnd, long lParam);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern long EnumChildWindows(IntPtr hWndParent, EnumChildWindowsProc lpEnumFunc, long lParam);
        [DllImport("user32.dll")]
        public static extern long GetClassName(IntPtr hwnd, StringBuilder lpClassName, long nMaxCount);
        private static bool EumWinChiPro(IntPtr hWnd)
        {
            StringBuilder s = new StringBuilder(256);
            GetClassName(hWnd, s, 257);
            string ss = s.ToString();
            ss = ss.Trim();
           Console.WriteLine(ss);
            return true;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowText(HandleRef hWnd, StringBuilder lpString, int nMaxCount);[DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(HandleRef hWnd);[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool EnumWindows(EnumThreadWindowsCallback callback, IntPtr extraData);[DllImport("user32.dll", ExactSpelling = true)]
        private static extern bool EnumChildWindows(HandleRef hwndParent, EnumChildrenCallback lpEnumFunc, HandleRef lParam); private delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);

        private delegate bool EnumChildrenCallback(IntPtr hwnd, IntPtr lParam);
        private bool EnumWindowsCallback(IntPtr handle)
        {
            int num1 = GetWindowTextLength(new HandleRef(this, handle)) * 2;
            StringBuilder builder1 = new StringBuilder(num1);
            GetWindowText(new HandleRef(this, handle), builder1, builder1.Capacity);
            System.Console.WriteLine(string.Format("Wnd:{0} Title: {1}", handle, builder1.ToString()));
            //Application.DoEvents();
            //listBox1.Items.Add(string.Format("Wnd:{0} Title: {1}", handle, builder1.ToString()));
            EnumChildWindows(new HandleRef(this, handle), new EnumChildrenCallback(EnumChildWindowsCallback), new HandleRef(null, IntPtr.Zero));
            return true;
        }

        private bool EnumChildWindowsCallback(IntPtr handle, IntPtr lparam)
        {
            int num1 = GetWindowTextLength(new HandleRef(this, handle)) * 2;
            StringBuilder builder1 = new StringBuilder(num1);
            GetWindowText(new HandleRef(this, handle), builder1, builder1.Capacity);
            if (!dctASSA.ContainsKey(handle) && !String.IsNullOrEmpty(builder1.ToString()))
            {
                dctASSA.Add(handle, builder1.ToString());
            }
            //System.Console.WriteLine(string.Format("/tSubWnd:{0} Title: {1}", handle, builder1.ToString()));
            // listBox1.Items.Add(string.Format("SubWnd:{0} Title: {1}", handle, builder1.ToString()));
            return true;
        }


        private bool EnumMainWindowsCallback(IntPtr handle)
        {
            int num1 = GetWindowTextLength(new HandleRef(this, handle)) * 2;
            StringBuilder builder1 = new StringBuilder(num1);
            GetWindowText(new HandleRef(this, handle), builder1, builder1.Capacity);
            System.Console.WriteLine(string.Format("Wnd:{0} Title: {1}", handle, builder1.ToString()));
            //Application.DoEvents();
            //listBox1.Items.Add(string.Format("Wnd:{0} Title: {1}", handle, builder1.ToString()));
            EnumChildWindows(new HandleRef(this, handle), new EnumChildrenCallback(EnumMainChildWindowsCallback), new HandleRef(null, IntPtr.Zero));
            return true;
        }

        private bool EnumMainChildWindowsCallback(IntPtr handle, IntPtr lparam)
        {
            int num1 = GetWindowTextLength(new HandleRef(this, handle)) * 2;
            StringBuilder builder1 = new StringBuilder(num1);
            GetWindowText(new HandleRef(this, handle), builder1, builder1.Capacity);
            if (!dctMainWindow.ContainsKey(handle) && !String.IsNullOrEmpty(builder1.ToString()))
            {
                dctMainWindow.Add(handle, builder1.ToString());
            }
            //System.Console.WriteLine(string.Format("/tSubWnd:{0} Title: {1}", handle, builder1.ToString()));
            // listBox1.Items.Add(string.Format("SubWnd:{0} Title: {1}", handle, builder1.ToString()));
            return true;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        #endregion

        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("等待1分钟.");
                System.Threading.Thread.Sleep(60000);
                (new Program()).run();
            }
        }

        public void run()
        {
            MornitorFolder();
        }

        private void StartStoneage(String uaerName, String passWord)
        {
            //Start ASSA
            IntPtr mainHanlder = IntPtr.Zero;
            bool isDone = true;
            isDone = StartASSA(ref mainHanlder);
            if (!isDone)
            {
                Console.WriteLine("Initail ASSA Fail!");
                return;
            }
            System.Threading.Thread.Sleep(waitTime);

            //激活石器
            IntPtr iBShiqi = cm.FindWindowEx(mainHanlder, "启动石器", true);
            if (iBShiqi == IntPtr.Zero)
            {
                Console.WriteLine("Start Shiqi Fail!");
                return;
            }
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseRightClick(iBShiqi);
            System.Threading.Thread.Sleep(waitTime);


            //获取石器句柄
            IntPtr saHanlder = cm.GetMainWindowhWnd("StoneAge", "StoneAge");
            if (saHanlder == IntPtr.Zero)
            {
                Console.WriteLine("Get SA hWnd Fail!");
                return;
            }

            //Input username and Password
            InputUserNameNPWD(uaerName, passWord);

            System.Threading.Thread.Sleep(waitTime);

            //点击自动登陆
            CLickAutoLogin();

            //点解脚本按钮
            IntPtr ibtScript = cm.GetHwndByValue(dctASSA, "脚本");
            cm.MouseRightClick(ibtScript);

            //选中脚本
            RunScript();

            //启动脚本
            ClickRunScript(mainHanlder);

            //等待时间
            processCla();

            //关闭石器
            SendMessage(saHanlder, WM_CLOSE, 0, 0);


        }

        private void InputUserNameNPWD(String userName, String password)
        {
            int loginX = -1;
            int loginY = -1;
            //User Name
            cm.GetXY("LoginUserNameXY", ref loginX, ref loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            SendKeys.SendWait(userName);
            System.Threading.Thread.Sleep(waitTime);
            //Password
            loginX = -1;
            loginY = -1;
            cm.GetXY("LoginPasswordXY", ref loginX, ref loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            SendKeys.SendWait(password);
            System.Threading.Thread.Sleep(waitTime);
        }


        private bool StartASSA(ref IntPtr mainHanlder)
        {
            try
            {
                string assaExe = cm.GetConfigurationData("ASSAEXE");
                string assaPath = String.Format("{0}", cm.GetConfigurationData("ASSAPATH_base"));
                int processID = cm.StartAPP(Path.Combine(assaPath, assaExe), assaPath);
                System.Threading.Thread.Sleep(waitTime);
                mainHanlder = cm.GetWnd(processID, "ThunderRT6FormDC", "柚子研究");
                if (mainHanlder == IntPtr.Zero)
                {
                    Console.WriteLine("Start Fail!");
                    return false;
                }
                //Load all the hwnd
                dctASSA.Clear();
                EnumWindowsCallback(mainHanlder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }

        private void MornitorFolder()
        {
            String path = cm.GetConfigurationData("MornitorFolder");
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] fileInfo = directoryInfo.GetFiles();
            String logPath = cm.GetConfigurationData("LogPath");
            if (fileInfo.Length > 0)
            {
                foreach(FileInfo finfo in fileInfo)
                {
                    String[] strsTmp = File.ReadAllLines(finfo.FullName);
                    foreach(String s in strsTmp)
                    {
                        if (String.IsNullOrEmpty(s)) continue;
                        Console.WriteLine(s);
                        String[] strsTnp = s.Split(']');
                        if (strsTnp.Length == 2)
                        {
                            String[] strsNamePWD = strsTnp[1].Trim().Split('|');
                            if (strsNamePWD.Length == 3)
                            {
                                //生成人物名字文件
                                CreateASC(strsNamePWD[2]);
                                //Input username and Password
                                StartStoneage(strsNamePWD[0], strsNamePWD[1]);

                                File.AppendAllText(logPath, String.Format("[{0}] {1},{2}\r\n", DateTime.Now, strsNamePWD[0], strsNamePWD[1]));
                            }
                            else
                            {
                                Console.WriteLine("Cannot get the pwd!");
                                File.AppendAllText(logPath, String.Format("[{0}] Cannot get the pwd! {1} s\r\n", DateTime.Now, s));
                            }
                        }
                        else
                        {
                            Console.WriteLine("Cannot get the acctount info!");
                            File.AppendAllText(logPath, String.Format("[{0}] Cannot get the acctount info! {1}\r\n", DateTime.Now, s));
                        }


                    }

                    //删除文件
                    try
                    {
                        File.Delete(finfo.FullName);
                    }
                    catch { }
                }
            }

           // Console.ReadLine();
        }

        private void KillDieProcess(int pID)
        {
            Process process = System.Diagnostics.Process.GetProcessById(pID);
            Process.Start("taskkill", "/PID " + pID + " /F");
        }

        private void CLickAutoLogin()
        {
            //点击自动登录
            IntPtr ibtAutoLogin = cm.GetHwndByValue(dctASSA, "自动登陆");
            cm.MouseRightClick(ibtAutoLogin);
            System.Threading.Thread.Sleep(waitTime);
        }

        private void ClickRunScript(IntPtr mainHanlde)
        {
            IntPtr ibtScript = cm.GetHwndByValue(dctASSA, "启动");
            cm.MouseRightClick(ibtScript);
            System.Threading.Thread.Sleep(waitTime);
        }

        private void RunScript()
        {
            int loginX = -1;
            int loginY = -1;
            String key = "HD";
            //Click First Level
            cm.GetXY(key, ref loginX, ref loginY);
            Console.WriteLine(loginX + " " + loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
         
        }

        private void processCla()
        {
            int wtProcess = 0;
            int.TryParse(cm.GetConfigurationData("waitTime"), out wtProcess);
            for (int i = 1; i <= wtProcess; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop);//将光标至于当前行的开始位置
                Console.Write(i);
                System.Threading.Thread.Sleep(1000);
            }

        }

        private void CreateASC(String name)
        {
            string assaPath = String.Format("{0}\\脚本\\人物名字.asc", cm.GetConfigurationData("ASSAPATH_base"));
            String content = String.Format("print /fb;73;iciiiiiiiiiii;0;{0};100180;30900;10;0;0;10;5;5;0;0;1", name);
            File.WriteAllText(assaPath, content);
        }
    }
}
