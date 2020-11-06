using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Collections;

using System.Windows.Forms;

namespace XAPP.COMLIB
{
    public class COMLIB
    {
        /// <summary>
        /// 运行cmd命令
        /// 不显示命令窗口
        /// </summary>
        /// <param name="cmdExe">指定应用程序的完整路径</param>
        /// <param name="cmdStr">执行命令行参数</param>
        public string RunCmd2(string cmdExe, string cmdStr)
        {
            string str = String.Empty;
            try
            {
                using (Process p = new Process())
                {
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    //如果调用程序路径中有空格时，cmd命令执行失败，可以用双引号括起来 ，在这里两个引号表示一个引号（转义）
                    string strCmd = string.Format(@"""{0}"" {1} {2}", cmdExe, cmdStr, "&exit");

                    p.StandardInput.WriteLine(strCmd);
                    p.StandardInput.AutoFlush = true;
                    str = p.StandardOutput.ReadToEnd();

                    while (!p.StandardOutput.EndOfStream)
                    {
                        str += p.StandardOutput.ReadLine() + Environment.NewLine;
                    }

                    p.WaitForExit();
                    p.Close();
                }
            }
            catch (Exception ex)
            {
                throw (ex);

            }
            return str;
        }

        public int StartAPP(string fileName, string workingFolder)
        {
            string str = String.Empty;
            Process procASSA;
            try
            {
                ProcessStartInfo pInfo = new ProcessStartInfo();
                pInfo.FileName = fileName;
                pInfo.UseShellExecute = false;
                pInfo.RedirectStandardInput = true;
                pInfo.RedirectStandardOutput = true;
                pInfo.RedirectStandardError = true;
                pInfo.CreateNoWindow = true;
                pInfo.WorkingDirectory = workingFolder;
                procASSA = Process.Start(pInfo);
                procASSA.WaitForInputIdle();//执行的进程必须有UI，如果没有UI，则忽略这个  

                return procASSA.Id;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string GetConfigurationData(String key)
        {
            string str = String.Empty;
            str = String.Format("{0}", ConfigurationManager.AppSettings[key]);
            return str;
        }


        [DllImport("user32.dll")]
        static extern IntPtr GetTopWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        static extern Int32 GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindow(IntPtr hWnd, UInt32 uCmd);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, uint hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow", SetLastError = true)]
        public static extern void SetForegroundWindow(IntPtr hwnd);

        public delegate bool CallBack(IntPtr hwnd, int lParam);

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        private static extern int SetCursorPos(int x, int y);

        private static readonly UInt32 GW_HWNDNEXT = 2;
        const int BM_CLICK = 0xF5;
        const int MOUSEEVENTF_MOVE = 0x0001; //移动鼠标
        const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下
        const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;// 模拟鼠标右键按下
        const int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下
        const int MOUSEEVENTF_MIDDLEUP = 0x0040; //模拟鼠标中键抬起
        const int MOUSEEVENTF_ABSOLUTE = 0x8000; //标示是否采用绝对坐标 
        const int WM_LBUTTONDOWN = 0x0201;
        const int WM_LBUTTONUP = 0x0202;


        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_SHOWWINDOW = 0x0040;

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        public void MoveWindow(IntPtr hWnd, int x, int y)
        {
            SetWindowPos(hWnd, IntPtr.Zero, x, y, 0, 0, SWP_NOZORDER | SWP_NOSIZE | SWP_SHOWWINDOW);

        }

        public IntPtr GetWnd(Int32 pID, String className, String text)
        {
            IntPtr h = GetTopWindow(IntPtr.Zero);
            while (h != IntPtr.Zero)
            {
                UInt32 newID;
                GetWindowThreadProcessId(h, out newID);
                if (newID == pID)
                {
                    StringBuilder sbClassName = new StringBuilder(200);
                    StringBuilder sbText = new StringBuilder(200);

                    GetClassName(h, sbClassName, 200);
                    GetWindowText(h, sbText, 200);
                    if (sbClassName.ToString().IndexOf(className, StringComparison.CurrentCultureIgnoreCase) >= 0 &&
                        sbText.ToString().IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    {
                        break;
                    }
                }

                h = GetWindow(h, GW_HWNDNEXT);
            }

            return h;
        }

        public UInt32 GetProcessIDByhWnd(IntPtr hWnd)
        {
            UInt32 newID;
            GetWindowThreadProcessId(hWnd, out newID);
            return newID;
        }

        /// <summary>
        /// 查找窗体上控件句柄
        /// </summary>
        /// <param name="hwnd">父窗体句柄</param>
        /// <param name="lpszWindow">控件标题(Text)</param>
        /// <param name="bChild">设定是否在子窗体中查找</param>
        /// <returns>控件句柄，没找到返回IntPtr.Zero</returns>
        public IntPtr FindWindowEx(IntPtr hwnd, string lpszWindow, bool bChild)
        {
            IntPtr iResult = IntPtr.Zero;
            // 首先在父窗体上查找控件
            iResult = FindWindowEx(hwnd, 0, null, lpszWindow);
            // 如果找到直接返回控件句柄
            if (iResult != IntPtr.Zero) return iResult;

            // 如果设定了不在子窗体中查找
            if (!bChild) return iResult;

            // 枚举子窗体，查找控件句柄
            int i = EnumChildWindows(
             hwnd,
             (h, l) =>
             {
                 IntPtr f1 = FindWindowEx(h, 0, null, lpszWindow);
                 if (f1 == IntPtr.Zero)
                     return true;
                 else
                 {
                     iResult = f1;
                     return false;
                 }
             },
             0);
            // 返回查找结果
            return iResult;
        }

        public IntPtr GetMainWindowhWnd(string lpClassName, string lpWindowName)
        {
            IntPtr hWnd = IntPtr.Zero;
            hWnd = FindWindow(lpClassName, lpWindowName);
            return hWnd;
        }


        public void MouseRightClick(IntPtr hWnd)
        {
            SendMessage(hWnd, BM_CLICK, 0, 0);     //发送点击按钮的消息

        }

        public void MouseMove(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public void MouseSingleClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public void ClickButton(IntPtr hWnd)
        {
            SendMessage(hWnd, WM_LBUTTONDOWN, 0, 0);
            SendMessage(hWnd, WM_LBUTTONUP, 0, 0);
            SendMessage(hWnd, WM_LBUTTONDOWN, 0, 0);
            SendMessage(hWnd, WM_LBUTTONUP, 0, 0);
        }

        public void MouseDoubleClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 1, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 1, 0, 0, 0);
        }

        public void BringToFront(IntPtr hWnd)
        {
            if (IsIconic(hWnd))
            {
                ShowWindowAsync(hWnd, 9);
            }

            SetForegroundWindow(hWnd);
        }

        public IntPtr GetHwndByValue(Dictionary<IntPtr, String> dctAssa, String name)
        {
            IntPtr hwnd = IntPtr.Zero;
            foreach (var item in dctAssa)
            {
                if (item.Value == name)
                {
                    hwnd = item.Key;
                    break;
                }
            }

            return hwnd;
        }

        public void GetXY(String key, ref int x, ref int y)
        {
            String XY = String.Format("{0}", System.Configuration.ConfigurationManager.AppSettings[key]);
            if (String.IsNullOrEmpty(XY)) return;
            String[] XYs = XY.Split(',');
            if (XYs.Length != 2) return;
            int.TryParse(XYs[0], out x);
            int.TryParse(XYs[1], out y);
        }

        public void GetUserNamePWD(String inputStr, ref Dictionary<String, String> dctLoginInfo)
        {
            String[] strs = inputStr.Split('|');
            dctLoginInfo.Add("userName", strs[0]);
            dctLoginInfo.Add("password", strs[1]);
            dctLoginInfo.Add("squence", strs[2]);
            dctLoginInfo.Add("type", strs[3]);
        }


        public void BringTopASSA(IntPtr hWnd)
        {
            ShowWindowAsync(hWnd, 9);
            SetForegroundWindow(hWnd);


        }
    }

}
