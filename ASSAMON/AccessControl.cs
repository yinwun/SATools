using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using XAPP.COMLIB;

namespace ASSAMON
{
    public class AccessControl
    {
        private COMLIB cm = new COMLIB();

        #region 变量

        public Dictionary<IntPtr, String> dctMainWindow = new Dictionary<IntPtr, String>();
        private Dictionary<IntPtr, String> dctASSA = new Dictionary<IntPtr, String>();

        #endregion

        #region 获取ASSA所有子控件

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref IntPtr lpdwProcessId);

        public delegate bool EnumChildWindowsProc(IntPtr hwnd, long lParam);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

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
            MessageBox.Show(ss);
            return true;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowText(HandleRef hWnd, StringBuilder lpString, int nMaxCount);[DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(HandleRef hWnd);[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool EnumWindows(EnumThreadWindowsCallback callback, IntPtr extraData);[DllImport("user32.dll", ExactSpelling = true)]
        private static extern bool EnumChildWindows(HandleRef hwndParent, EnumChildrenCallback lpEnumFunc, HandleRef lParam); private delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);

        private delegate bool EnumChildrenCallback(IntPtr hwnd, IntPtr lParam);


        public Dictionary<IntPtr, String> EnumWindowsCallback(IntPtr handle)
        {
            int num1 = GetWindowTextLength(new HandleRef(this, handle)) * 2;
            StringBuilder builder1 = new StringBuilder(num1);
            GetWindowText(new HandleRef(this, handle), builder1, builder1.Capacity);
            Application.DoEvents();
            EnumChildWindows(new HandleRef(this, handle), new EnumChildrenCallback(EnumChildWindowsCallback), new HandleRef(null, IntPtr.Zero));
            return dctASSA;
        }

        private bool EnumChildWindowsCallback(IntPtr handle, IntPtr lparam)
        {
            int num1 = GetWindowTextLength(new HandleRef(this, handle)) * 2;
            StringBuilder builder1 = new StringBuilder(num1);
            GetWindowText(new HandleRef(this, handle), builder1, builder1.Capacity);
            dctASSA.Add(handle, builder1.ToString());
            return true;
        }


        public Dictionary<IntPtr, String> EnumMainWindowsCallback(IntPtr handle)
        {
            int num1 = GetWindowTextLength(new HandleRef(this, handle)) * 2;
            StringBuilder builder1 = new StringBuilder(num1);
            GetWindowText(new HandleRef(this, handle), builder1, builder1.Capacity);
            //System.Console.WriteLine(string.Format("Wnd:{0} Title: {1}", handle, builder1.ToString()));
            Application.DoEvents();
            //listBox1.Items.Add(string.Format("Wnd:{0} Title: {1}", handle, builder1.ToString()));
            EnumChildWindows(new HandleRef(this, handle), new EnumChildrenCallback(EnumMainChildWindowsCallback), new HandleRef(null, IntPtr.Zero));
            return dctMainWindow;
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
            return true;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out Constant.RECT lpRect);


        #region Read Memory

        //读取进程内存的函数

        [DllImport("kernel32.dll ")]
        static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, out int lpBuffer, int nSize, out int lpNumberOfBytesRead);
        //得到目标进程句柄的函数

        [DllImport("kernel32.dll ")]
        static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern int OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        const int PROCESS_VM_READ = 0x0010;
        const int PROCESS_VM_WRITE = 0x0020;

        public static string ReadSADataFromRAMString(int PPID, String address)
        {
            string dataAddress = String.Empty;
            try
            {
                byte[] lpBuffer = new byte[256];
                int readByte;
                int hProcess = OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE, false, PPID);
                ReadProcessMemory(hProcess, Convert.ToInt32(address, 16), lpBuffer, lpBuffer.Length, out readByte);

                dataAddress = System.Text.Encoding.Default.GetString(lpBuffer); ;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return dataAddress;
        }

        public static int ReadSADataFromRAMInt(int PPID, String address)
        {
            int dataAddress = 0;
            try
            {
                int readByte;
                int hProcess = OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE, false, PPID);
                ReadProcessMemory(hProcess, Convert.ToInt32(address, 16), out dataAddress, 4, out readByte);
            }
            catch { }
            return dataAddress;
        }

        #endregion


        public static string KillDieProcess(int pID)
        {
            //實例一個Process類，啟動一個獨立進程
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            String command = "taskkill /PID " + pID + " /F";
            //Process類有一個StartInfo屬性，這個是ProcessStartInfo類，包括了一些屬性和方法，下面我們用到了他的幾個屬性：

            p.StartInfo.FileName = "cmd.exe";           //設定程序名
            p.StartInfo.Arguments = "/c " + command;    //設定程式執行參數
            p.StartInfo.UseShellExecute = false;        //關閉Shell的使用
            p.StartInfo.RedirectStandardInput = true;   //重定向標準輸入
            p.StartInfo.RedirectStandardOutput = true;  //重定向標準輸出
            p.StartInfo.RedirectStandardError = true;   //重定向錯誤輸出
            p.StartInfo.CreateNoWindow = true;          //設置不顯示窗口

            p.Start();   //啟動

            //p.StandardInput.WriteLine(command);       //也可以用這種方式輸入要執行的命令
            //p.StandardInput.WriteLine("exit");        //不過要記得加上Exit要不然下一行程式執行的時候會當機

            return p.StandardOutput.ReadToEnd();        //從輸出流取得命令執行結果

        }

        #endregion


        public void ClickErrorWindow(int waitTime)
        {
            string strx = String.Empty;
            IntPtr errHwnd = IntPtr.Zero;

            Dictionary<IntPtr, string> dctWindows = new Dictionary<IntPtr, string>();

            dctWindows = EnumMainWindowsCallback(IntPtr.Zero);

            foreach (KeyValuePair<IntPtr, string> kv in dctWindows)
            {
                //strx += kv.Key + " | " + kv.Value + "\n"; 
                if (kv.Value.Contains("应用程序错误") || kv.Value.Contains("SaDeb"))
                //if (kv.Value.Contains("脚本"))
                {
                    errHwnd = (IntPtr)kv.Key;
                    //MessageBox.Show(kv.Key + " | " + kv.Value);
                    break;
                }
            }
            //MessageBox.Show(strx);


            if (errHwnd != IntPtr.Zero)
            {
                IntPtr errYESHwnd = IntPtr.Zero;
                dctWindows.Clear();
                EnumMainWindowsCallback(errHwnd);
                foreach (KeyValuePair<IntPtr, string> kv in dctWindows)
                {
                    //strx += kv.Key + " | " + kv.Value + "\n";
                    if (kv.Value.Contains("确定"))
                    {
                        errYESHwnd = (IntPtr)kv.Key;
                        //MessageBox.Show("2: " + kv.Key + " | " + kv.Value);
                        //System.Threading.Thread.Sleep(5000);
                        cm.ClickButton(errYESHwnd);
                        System.Threading.Thread.Sleep(waitTime);
                    }
                    if (kv.Value.Contains("关闭程序"))
                    {
                        errYESHwnd = (IntPtr)kv.Key;
                        //MessageBox.Show("2: " + kv.Key + " | " + kv.Value);
                        //System.Threading.Thread.Sleep(5000);
                        cm.ClickButton(errYESHwnd);
                        System.Threading.Thread.Sleep(waitTime);
                    }
                }
            }
            //MessageBox.Show(strx);
        }
    }
}
