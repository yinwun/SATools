using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using XAPP.COMLIB;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Configuration;
using System.Threading;

namespace ASSAMON
{
    public partial class Form1 : Form
    {

        private COMLIB cm = new COMLIB();
        private int waitTime = 500;//mls
        private Dictionary<IntPtr, String> dctASSA = new Dictionary<IntPtr, String>();
        private Dictionary<IntPtr, String> dctMainWindow = new Dictionary<IntPtr, String>();
        private string RunningPath = System.AppDomain.CurrentDomain.BaseDirectory;
        List<ActiveInfo> lstAcitveInfo = new List<ActiveInfo>();
        DoubleBufferListView doubleBufferlvActiveInfo = new DoubleBufferListView();
        List<Dictionary<String, String>> lstActList = new List<Dictionary<string, string>>();
        private List<String> lstStopTime = new List<string>();
        Dictionary<String, IntPtr> dctProc = new Dictionary<String, IntPtr>();
        Dictionary<String, String> dctProcAct = new Dictionary<string, string>();
        private double killMem = 0;
        Thread LogThread;
        private bool isRun = false;
        private static object LockObject = new Object();

        public Form1()
        {
            InitializeComponent();

            //Right Concern
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Top = 0;

            string ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            lblver.Text = ver;
        }


        #region 遍历ASSA某控件的

        #endregion

        #region 获取ASSA所有子控件

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
        private bool EnumWindowsCallback(IntPtr handle)
        {
            int num1 = GetWindowTextLength(new HandleRef(this, handle)) * 2;
            StringBuilder builder1 = new StringBuilder(num1);
            GetWindowText(new HandleRef(this, handle), builder1, builder1.Capacity);
            //System.Console.WriteLine(string.Format("Wnd:{0} Title: {1}", handle, builder1.ToString()));
            Application.DoEvents();
            //listBox1.Items.Add(string.Format("Wnd:{0} Title: {1}", handle, builder1.ToString()));
            EnumChildWindows(new HandleRef(this, handle), new EnumChildrenCallback(EnumChildWindowsCallback), new HandleRef(null, IntPtr.Zero));
            return true;
        }

        private bool EnumChildWindowsCallback(IntPtr handle, IntPtr lparam)
        {
            int num1 = GetWindowTextLength(new HandleRef(this, handle)) * 2;
            StringBuilder builder1 = new StringBuilder(num1);
            GetWindowText(new HandleRef(this, handle), builder1, builder1.Capacity);
            dctASSA.Add(handle, builder1.ToString());
            //if (!dctASSA.ContainsKey(handle) && !String.IsNullOrEmpty(builder1.ToString()))
            //{
            //    dctASSA.Add(handle, builder1.ToString());
            //}
            //System.Console.WriteLine(string.Format("/tSubWnd:{0} Title: {1}", handle, builder1.ToString()));
            // listBox1.Items.Add(string.Format("SubWnd:{0} Title: {1}", handle, builder1.ToString()));
            return true;
        }


        private bool EnumMainWindowsCallback(IntPtr handle)
        {
            int num1 = GetWindowTextLength(new HandleRef(this, handle)) * 2;
            StringBuilder builder1 = new StringBuilder(num1);
            GetWindowText(new HandleRef(this, handle), builder1, builder1.Capacity);
            //System.Console.WriteLine(string.Format("Wnd:{0} Title: {1}", handle, builder1.ToString()));
            Application.DoEvents();
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

        #region 登陆操作

        private bool StartASSA(ref IntPtr mainHanlder, String sequence)
        {
            try
            {
                //String assaPathKey = String.Format("ASSAPATH{0}", sequence);
                string assaExe = cm.GetConfigurationData("ASSAEXE");
                string assaPath = String.Format("{0}{1}", cm.GetConfigurationData("ASSAPATH"), sequence);
                int processID = cm.StartAPP(Path.Combine(assaPath, assaExe), assaPath);
                System.Threading.Thread.Sleep(waitTime);
                mainHanlder = cm.GetWnd(processID, "ThunderRT6FormDC", "石器时代");
                if (mainHanlder == IntPtr.Zero)
                {
                    MessageBox.Show("Start Fail!");
                    return false;
                }
                //Load all the hwnd
                dctASSA.Clear();
                EnumWindowsCallback(mainHanlder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }

        private bool StartSelftASSA(ref IntPtr mainHanlder, String sequence)
        {
            try
            {
                //String assaPathKey = String.Format("ASSAPATH{0}", sequence);
                string assaExe = cm.GetConfigurationData("ASSAEXE");
                string assaPath = String.Format("{0}", cm.GetConfigurationData("ASSAPATH_base"));
                int processID = cm.StartAPP(Path.Combine(assaPath, assaExe), assaPath);
                System.Threading.Thread.Sleep(waitTime);
                mainHanlder = cm.GetWnd(processID, "ThunderRT6FormDC", "石器时代");
                if (mainHanlder == IntPtr.Zero)
                {
                    MessageBox.Show("Start Fail!");
                    return false;
                }
                //Load all the hwnd
                dctASSA.Clear();
                EnumWindowsCallback(mainHanlder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }
        private void StartStoneage(Dictionary<String, String> dctLoginInfo)
        {
            //Start ASSA
            IntPtr mainHanlder = IntPtr.Zero;
            bool isDone = true;
            isDone = StartASSA(ref mainHanlder, dctLoginInfo["squence"]);
            if (!isDone)
            {
                MessageBox.Show("Initail ASSA Fail!");
                return;
            }
            System.Threading.Thread.Sleep(waitTime);

            //激活石器
            IntPtr iBShiqi = cm.FindWindowEx(mainHanlder, "激活石器", true);
            if (iBShiqi == IntPtr.Zero)
            {
                MessageBox.Show("Start Shiqi Fail!");
                return;
            }
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseRightClick(iBShiqi);
            System.Threading.Thread.Sleep(waitTime);

            //获取石器句柄
            IntPtr saHanlder = cm.GetMainWindowhWnd("StoneAge", "StoneAge");
            if (saHanlder == IntPtr.Zero)
            {
                MessageBox.Show("Get SA hWnd Fail!");
                return;
            }
            //保存句柄
            IntPtr sauPid = IntPtr.Zero;
            GetWindowThreadProcessId(saHanlder, ref sauPid);
            if (!dctProc.ContainsKey(sauPid.ToString()))
            {
                dctProc.Add(sauPid.ToString(), mainHanlder);
            }
            //绑定数据
            //LoadActiveData(saHanlder, dctLoginInfo["userName"]);

            //Input username and Password
            InputUserNameNPWD(dctLoginInfo["userName"], dctLoginInfo["password"]);
            if (!dctProcAct.ContainsKey(sauPid.ToString()))
            {
                dctProcAct.Add(sauPid.ToString(), dctLoginInfo["userName"]);
            }


            //前置ASSA
            PutASSATOTOP(mainHanlder);

            //点击资料显示
            if (dctLoginInfo["type"].Contains("C"))
            {

                IntPtr ubtInfo = cm.GetHwndByValue(dctASSA, "资料显示");
                cm.MouseRightClick(ubtInfo);
                System.Threading.Thread.Sleep(waitTime);

                if (dctLoginInfo["type"].Contains("1_C"))
                {
                    IntPtr infoshowHanlder = cm.GetMainWindowhWnd("ThunderRT6FormDC", "资料显示");
                    RECT rct;
                    GetWindowRect(infoshowHanlder, out rct);
                    Rectangle screen = Screen.FromHandle(infoshowHanlder).Bounds;
                    Point pt = new Point(rct.Left, rct.Bottom);// + rct.Top);
                    cm.MoveWindow(infoshowHanlder, pt.X, pt.Y);

                    System.Threading.Thread.Sleep(waitTime);
                    int fligtX = 150;
                    int filgtY = 770;
                    cm.MouseMove(fligtX, filgtY);
                    System.Threading.Thread.Sleep(waitTime);
                    cm.MouseSingleClick();

                }
                else
                {
                    System.Threading.Thread.Sleep(waitTime);
                    int fligtX = 150;
                    int filgtY = 540;
                    cm.MouseMove(fligtX, filgtY);
                    System.Threading.Thread.Sleep(waitTime);
                    cm.MouseSingleClick();
                }
                System.Threading.Thread.Sleep(waitTime);
            }

            //点击自动登陆
            CLickAutoLogin();

            //点击脚本
            IntPtr ibtScript = cm.GetHwndByValue(dctASSA, "脚本");
            cm.MouseRightClick(ibtScript);
            System.Threading.Thread.Sleep(waitTime);

            //运行脚本
            RunScript(dctLoginInfo["type"]);

            //启动脚本
            ClickRunScript(mainHanlder);

            //SendKeys.SendWait("{F5}");
            //System.Threading.Thread.Sleep(waitTime);

            //隐藏石器
            CliclHideASSA(mainHanlder);

            //绑定数据
            //LoadActiveData(saHanlder, dctLoginInfo["userName"]);

            SendKeys.SendWait("{F9}");

        }

        private void StartStoneageForFlight(Dictionary<String, String> dctLoginInfo, int seq)
        {
            //Start ASSA
            IntPtr mainHanlder = IntPtr.Zero;
            bool isDone = true;
            isDone = StartSelftASSA(ref mainHanlder, dctLoginInfo["squence"]);
            if (!isDone)
            {
                MessageBox.Show("Initail ASSA Fail!");
                return;
            }
            System.Threading.Thread.Sleep(waitTime);

            //Move
            MoveAssaWindows(mainHanlder, seq);


            //激活石器
            IntPtr iBShiqi = cm.FindWindowEx(mainHanlder, "激活石器", true);
            if (iBShiqi == IntPtr.Zero)
            {
                MessageBox.Show("Start Shiqi Fail!");
                return;
            }
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseRightClick(iBShiqi);
            System.Threading.Thread.Sleep(waitTime);

            //获取石器句柄
            IntPtr saHanlder = cm.GetMainWindowhWnd("StoneAge", "StoneAge");
            if (saHanlder == IntPtr.Zero)
            {
                MessageBox.Show("Get SA hWnd Fail!");
                return;
            }
            //绑定数据
            LoadActiveData(saHanlder, dctLoginInfo["userName"]);

            //Input username and Password
            InputUserNameNPWD(dctLoginInfo["userName"], dctLoginInfo["password"]);

            System.Threading.Thread.Sleep(waitTime);

            //前置ASSA
            //PutASSATOTOP(mainHanlder);

            //点击自动登陆
            CLickAutoLogin();

            bool isF9 = false;
            while (!isF9)
            {
                cm.BringToFront(saHanlder);

                RECT rct;
                GetWindowRect(saHanlder, out rct);
                Rectangle screen = Screen.FromHandle(saHanlder).Bounds;
                Point pt = new Point(rct.Left, rct.Bottom);// + rct.Top);

                if (pt.Y == 640)
                {
                    isF9 = true;
                    break;
                }

                SendKeys.Send("{F9}");
                System.Threading.Thread.Sleep(2000);
            }

            //移动石器窗口
            MoveStoneageWindows(saHanlder, seq);
        }

        private void StartStoneageForClean(Dictionary<String, String> dctLoginInfo, int seq)
        {
            //Start ASSA
            IntPtr mainHanlder = IntPtr.Zero;
            bool isDone = true;
            isDone = StartSelftASSA(ref mainHanlder, dctLoginInfo["squence"]);
            if (!isDone)
            {
                MessageBox.Show("Initail ASSA Fail!");
                return;
            }
            System.Threading.Thread.Sleep(waitTime);

            //Move
            MoveAssaCleanWindows(mainHanlder, seq);


            //激活石器
            IntPtr iBShiqi = cm.FindWindowEx(mainHanlder, "激活石器", true);
            if (iBShiqi == IntPtr.Zero)
            {
                MessageBox.Show("Start Shiqi Fail!");
                return;
            }
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseRightClick(iBShiqi);
            System.Threading.Thread.Sleep(waitTime);

            //获取石器句柄
            IntPtr saHanlder = cm.GetMainWindowhWnd("StoneAge", "StoneAge");
            if (saHanlder == IntPtr.Zero)
            {
                MessageBox.Show("Get SA hWnd Fail!");
                return;
            }
            //绑定数据
            LoadActiveData(saHanlder, dctLoginInfo["userName"]);

            //Input username and Password
            InputUserNameNPWD(dctLoginInfo["userName"], dctLoginInfo["password"]);

            System.Threading.Thread.Sleep(waitTime);

            //前置ASSA
            //PutASSATOTOP(mainHanlder);

            //点击自动登陆
            CLickAutoLogin();

            //移动石器窗口
            MoveStoneageCLeanWindows(saHanlder, seq);

            //点击脚本
            IntPtr ibtScript = cm.GetHwndByValue(dctASSA, "脚本");
            cm.MouseRightClick(ibtScript);
            System.Threading.Thread.Sleep(waitTime);
        }

        private void MoveStoneageCLeanWindows(IntPtr hwnd, int seq)
        {
            RECT rct;
            GetWindowRect(hwnd, out rct);
            Rectangle screen = Screen.FromHandle(hwnd).Bounds;
            Point pt = new Point(rct.Left, rct.Bottom);// + rct.Top);
            int assaLength = rct.Right - rct.Left - 10;
            int asLen = 301;

            if (seq == 1)
            {
                cm.MoveWindow(hwnd, asLen + assaLength, 0);
            }
            else if (seq == 2)
            {
                cm.MoveWindow(hwnd, asLen * 2 + assaLength * 2, 0);
            }
            else if (seq == 3)
            {
                cm.MoveWindow(hwnd, asLen + assaLength, 512);
            }
            else if (seq == 4)
            {
                cm.MoveWindow(hwnd, asLen * 2 + assaLength * 2, 512);
            }
            else if (seq == 5)
            {
                cm.MoveWindow(hwnd, assaLength * 3, 0);
            }
        }

        private void MoveAssaCleanWindows(IntPtr hwndSA, int seq)
        {
            RECT rct;
            GetWindowRect(hwndSA, out rct);
            Rectangle screen = Screen.FromHandle(hwndSA).Bounds;
            Point pt = new Point(rct.Left, rct.Bottom);// + rct.Top);
            int y = pt.Y * 3 + 75;
            int assaLength = rct.Right - rct.Left;
            int stoneAgeLen = 650;
            if (seq == 2)
            {
                cm.MoveWindow(hwndSA, assaLength + stoneAgeLen * 2, 0);
            }
            else if (seq == 3)
            {
                cm.MoveWindow(hwndSA, rct.Left, rct.Bottom);
            }
            else if (seq == 4)
            {
                cm.MoveWindow(hwndSA, assaLength + stoneAgeLen * 2, 512);
            }
            else if (seq == 5)
            {
                cm.MoveWindow(hwndSA, assaLength * 4, y);
            }

        }


        private void MoveStoneageWindows(IntPtr hwnd, int seq)
        {
            RECT rct;
            GetWindowRect(hwnd, out rct);
            Rectangle screen = Screen.FromHandle(hwnd).Bounds;
            Point pt = new Point(rct.Left, rct.Bottom);// + rct.Top);
            int assaLength = rct.Right - rct.Left - 10;

            if (seq == 1)
            {
                cm.MoveWindow(hwnd, assaLength, pt.Y - 5);
            }
            else if (seq == 2)
            {
                cm.MoveWindow(hwnd, assaLength * 2, pt.Y - 5);
            }
            else if (seq == 3)
            {
                cm.MoveWindow(hwnd, assaLength, 0);
            }
            else if (seq == 4)
            {
                cm.MoveWindow(hwnd, assaLength * 2, 0);
            }
            else if (seq == 5)
            {
                cm.MoveWindow(hwnd, assaLength * 3, 0);
            }
        }



        private void MoveAssaWindows(IntPtr hwnd, int seq)
        {
            RECT rct;
            GetWindowRect(hwnd, out rct);
            Rectangle screen = Screen.FromHandle(hwnd).Bounds;
            Point pt = new Point(rct.Left, rct.Bottom);// + rct.Top);
            int y = pt.Y * 3 + 75;
            int assaLength = rct.Right - rct.Left;
            if (seq == 1)
            {
                cm.MoveWindow(hwnd, 0, y);
            }
            else if (seq == 2)
            {
                cm.MoveWindow(hwnd, assaLength, y);
            }
            else if (seq == 3)
            {
                cm.MoveWindow(hwnd, assaLength * 2, y);
            }
            else if (seq == 4)
            {
                cm.MoveWindow(hwnd, assaLength * 3, y);
            }
            else if (seq == 5)
            {
                cm.MoveWindow(hwnd, assaLength * 4, y);
            }

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
        private void PutASSATOTOP(IntPtr mainHanlder)
        {
            //前置ASSA
            System.Threading.Thread.Sleep(waitTime);
            cm.BringToFront(mainHanlder);
            System.Threading.Thread.Sleep(waitTime);
        }
        private void CLickAutoLogin()
        {
            //点击自动登录
            IntPtr ibtAutoLogin = cm.GetHwndByValue(dctASSA, "自动登陆");
            cm.MouseRightClick(ibtAutoLogin);
            System.Threading.Thread.Sleep(waitTime);
        }
        private void LoadAccounts()
        {
            String accountFile = Path.Combine(RunningPath, "Accounts.ini");
            using (StreamReader sr = new StreamReader(accountFile, Encoding.Default))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    lstccount.Items.Add(line.ToString());
                }
            }

            var items = lstccount.Items;
            foreach (var item in items)
            {
                Dictionary<String, String> dctActList = new Dictionary<string, string>();
                cm.GetUserNamePWD(item.ToString(), ref dctActList);
                lstActList.Add(dctActList);
            }
        }
        private void RunScript(String type)
        {

            int loginX = -1;
            int loginY = -1;
            String key = String.Empty;
            //Click Type HD or MO
            string tmpStr = type;
            key = tmpStr.Substring(0, 2);
            cm.GetXY(key, ref loginX, ref loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);

            //Node type
            tmpStr = type;
            key = tmpStr.Substring(0, 4);
            cm.GetXY(key, ref loginX, ref loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);

            //Click Script
            tmpStr = type;
            key = tmpStr;
            cm.GetXY(key, ref loginX, ref loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
        }
        private void ClickRunScript(IntPtr mainHanlde)
        {
            IntPtr ibtScript = cm.GetHwndByValue(dctASSA, "启动");
            cm.MouseRightClick(ibtScript);
            System.Threading.Thread.Sleep(waitTime);
        }
        private void CliclHideASSA(IntPtr mainHanlde)
        {
            IntPtr ibtScript = cm.GetHwndByValue(dctASSA, "隐藏石器");
            cm.MouseRightClick(ibtScript);
            System.Threading.Thread.Sleep(waitTime);
        }
        private void StartAccounts(bool isALL)
        {
            Dictionary<String, String> dctLoginInfo = new Dictionary<string, string>();
            if (isALL)
            {
                foreach (var item in lstccount.Items)
                {
                    dctLoginInfo = new Dictionary<string, string>();
                    cm.GetUserNamePWD(item.ToString(), ref dctLoginInfo);
                    StartStoneage(dctLoginInfo);
                    System.Threading.Thread.Sleep(3000);
                }

            }
            else
            {
                var items = lstccount.SelectedItems;
                foreach (var item in items)
                {
                    dctLoginInfo = new Dictionary<string, string>();
                    cm.GetUserNamePWD(item.ToString(), ref dctLoginInfo);
                    StartStoneage(dctLoginInfo);
                    System.Threading.Thread.Sleep(3000);
                }
            }


        }

        private void StartFilghtAccounts()
        {
            Dictionary<String, String> dctLoginInfo = new Dictionary<string, string>();
            var items = lstccount.SelectedItems;
            int i = 1;
            foreach (var item in items)
            {
                dctLoginInfo = new Dictionary<string, string>();
                cm.GetUserNamePWD(item.ToString(), ref dctLoginInfo);
                StartStoneageForFlight(dctLoginInfo, i);
                System.Threading.Thread.Sleep(waitTime);
                i++;
            }
        }

        private void StartCleanAccounts()
        {
            Dictionary<String, String> dctLoginInfo = new Dictionary<string, string>();
            var items = lstccount.SelectedItems;
            int i = 1;
            foreach (var item in items)
            {
                dctLoginInfo = new Dictionary<string, string>();
                cm.GetUserNamePWD(item.ToString(), ref dctLoginInfo);
                StartStoneageForClean(dctLoginInfo, i);
                System.Threading.Thread.Sleep(waitTime);
                i++;
            }
        }

        #endregion

        #region 绑定数据

        private void LoadActiveData(IntPtr sahWnd, string account)
        {
            ClickErrorWindow();

            System.Threading.Thread.Sleep(1);

            TaskBarUtil.RefreshNotification();
            //SA PROID
            List<int> lstProID = new List<int>();
            String saName = System.Configuration.ConfigurationManager.AppSettings["SAName"];
            foreach (Process p in Process.GetProcessesByName(saName))
            {
                lstProID.Add(p.Id);
            }

            //获取进程
            List<ActiveInfo> lstAI = new List<ActiveInfo>();
            List<String> lstSYSName = new List<string>();
          
            //CLear the dataview
            doubleBufferlvActiveInfo.Items.Clear();
            //dctProc
            foreach (int i in lstProID)
            {
                ActiveInfo ai = new ActiveInfo();
                //PID
                ai.ProID = i;
                Process process = System.Diagnostics.Process.GetProcessById(i);
                //Memory
                ai.Memory = process.WorkingSet64 / (1024.0 * 1024.0);

                if (ai.Memory < killMem)
                {
                    dctProcAct.Remove(i.ToString()); //删除账号
                    KillDieProcess(ai.ProID);
                    //删除list 删除lv
                    System.Threading.Thread.Sleep(1000);
                    continue;
                }

                ListViewItem lvi = new ListViewItem();
                lvi.Text = String.Format("{0}", doubleBufferlvActiveInfo.Items.Count + 1);
                lvi.SubItems.Add(i.ToString());//PID

                lvi.SubItems.Add(String.Format("{0}", ai.Memory));//Memory

                //名称
                ai.Name = ReadSADataFromRAMString(i, cm.GetConfigurationData("CharName"));
                lvi.SubItems.Add(ai.Name);//名称

                //帐号
                //**********************************************************************************

                ai.Account = ReadSADataFromRAMString(i, cm.GetConfigurationData("CharAct"));
                String actMem = GetCharAct(ai.Account);
                String actStart = String.Empty;
                dctProcAct.TryGetValue(i.ToString(), out actStart);
                //如果内存名字为空登录没成功，取启动名字
                if (String.IsNullOrEmpty(actMem))
                {
                    actMem = actStart;
                }

                //加入账号
                if (!String.IsNullOrEmpty(actMem))
                {
                    lstSYSName.Add(actMem);
                    lvi.SubItems.Add(actMem);//帐号
                }
                //*************************************************************************************


                //声望
                ai.ShengWang = ReadSADataFromRAMInt(i, cm.GetConfigurationData("addrSW"));
                lvi.SubItems.Add(String.Format("{0}", ai.ShengWang));//声望
                //XY, 坐标
                int x = ReadSADataFromRAMInt(i, cm.GetConfigurationData("addrX"));
                int y = ReadSADataFromRAMInt(i, cm.GetConfigurationData("addrY"));
                ai.XY = String.Format("{0},{1}", x, y);
                lvi.SubItems.Add(ai.XY);//坐标
                //Map
                int map = ReadSADataFromRAMInt(i, cm.GetConfigurationData("addrMap"));
                lvi.SubItems.Add(String.Format("{0}", map));//地图
                ai.MAP = String.Format("{0}", map);
                //hWnd
                ai.Hwnd = sahWnd;
                lvi.SubItems.Add(sahWnd.ToString());
                //Stone
                int stone = ReadSADataFromRAMInt(i, cm.GetConfigurationData("CharStone"));
                ai.Stone = String.Format("{0}", stone);
                lvi.SubItems.Add(String.Format("{0}", stone));//石头
                //HP
                int hp = ReadSADataFromRAMInt(i, cm.GetConfigurationData("CharHP"));
                ai.HP = String.Format("{0}", hp);
                lvi.SubItems.Add(String.Format("{0}", hp));//HP

                lstAI.Add(ai);
                this.doubleBufferlvActiveInfo.Items.Add(lvi);
            }

            int processNum = 0;
            int.TryParse(cm.GetConfigurationData("ProcessNum"), out processNum);

            if (lstSYSName.Count < processNum)
            {
                //判断没有的账号
                String newAct = String.Empty;
                foreach (Dictionary<String, String> dctH in lstActList)
                {
                    String tmpUserName = String.Empty;
                    dctH.TryGetValue("userName", out tmpUserName);

                    if (!lstSYSName.Contains(tmpUserName))
                    {
                        newAct = tmpUserName;
                        break;
                    }
                }


                if (!String.IsNullOrEmpty(newAct))
                {
                    System.Threading.Thread.Sleep(1);
                    if (!CheckActAlreadyRunning(newAct))
                    {
                        Dictionary<String, String> dct = GetActDct(newAct);
                        StartStoneage(dct);
                    }
                }
            }

            ClickErrorWindow();

        }

        private bool CheckActAlreadyRunning(String act)
        {
            bool blExisted = false;
            String saName = System.Configuration.ConfigurationManager.AppSettings["SAName"];
            foreach (Process p in Process.GetProcessesByName(saName))
            {
                String actTmp = GetCharAct(ReadSADataFromRAMString(p.Id, cm.GetConfigurationData("CharAct")));
                if (actTmp.Equals(act))
                {
                    blExisted = true;
                    break;
                }
                
            }

            return blExisted;
        }
    
        //获取账号
        private String GetCharAct(string addressAct)
        {
            String str = String.Empty;
            foreach (Dictionary<String, String> dct in lstActList)
            {
                dct.TryGetValue("userName", out str);
                if (addressAct.Contains(str))
                {
                    return str;
                }
            }
            return String.Empty;
        }

        private void DeleteActiveList(int pID)
        {
            //for (ActiveInfo i in lstAcitveInfo)
            for (int i = 0; i < lstAcitveInfo.Count; i++)
            {
                if (lstAcitveInfo[i].ProID == pID)
                {
                    lstAcitveInfo.RemoveAt(i);
                    break;
                }
            }
        }

        private void DeleteLvAcitive(int pID)
        {
            foreach (ListViewItem item in this.doubleBufferlvActiveInfo.Items)
            {
                //second column PID
                int proID = -1;
                int.TryParse(item.SubItems[1].Text, out proID);
                if (proID == pID)
                {
                    doubleBufferlvActiveInfo.Items.RemoveAt(item.Index);
                    break;
                }
            }
        }

        private void InitialDoubleBufferListView()
        {
            doubleBufferlvActiveInfo.GridLines = true;
            doubleBufferlvActiveInfo.FullRowSelect = true;
            doubleBufferlvActiveInfo.HideSelection = false;
            doubleBufferlvActiveInfo.Location = new System.Drawing.Point(17, 185);
            doubleBufferlvActiveInfo.Name = "doubleBufferlvActiveInfo";
            doubleBufferlvActiveInfo.Size = new System.Drawing.Size(750, 170);
            doubleBufferlvActiveInfo.TabIndex = 2;
            doubleBufferlvActiveInfo.UseCompatibleStateImageBehavior = false;
            doubleBufferlvActiveInfo.View = System.Windows.Forms.View.Details;
            doubleBufferlvActiveInfo.BringToFront();
            this.Controls.Add(doubleBufferlvActiveInfo);

            doubleBufferlvActiveInfo.Clear();
            //Initial Columns
            doubleBufferlvActiveInfo.Columns.Add("No.", 30);
            doubleBufferlvActiveInfo.Columns.Add("PID", 50);
            doubleBufferlvActiveInfo.Columns.Add("Memory", 100);
            doubleBufferlvActiveInfo.Columns.Add("名称", 95);
            doubleBufferlvActiveInfo.Columns.Add("帐号", 100);
            doubleBufferlvActiveInfo.Columns.Add("声望", 70);
            doubleBufferlvActiveInfo.Columns.Add("坐标", 70);
            doubleBufferlvActiveInfo.Columns.Add("地图", 50);
            doubleBufferlvActiveInfo.Columns.Add("句柄", 50);
            doubleBufferlvActiveInfo.Columns.Add("石头", 100);
            doubleBufferlvActiveInfo.Columns.Add("HP", 100);

            this.doubleBufferlvActiveInfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.doubleBufferlvActiveInfo_MouseDoubleClick);
        }

        private void doubleBufferlvActiveInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //dctProc

            var items = doubleBufferlvActiveInfo.SelectedItems;
            foreach (ListViewItem item in items)
            {
                String pids = item.SubItems[1].Text;
                if (!String.IsNullOrEmpty(pids))
                {
                    IntPtr assaHwd = IntPtr.Zero;

                    dctProc.TryGetValue(pids, out assaHwd);
                    cm.BringTopASSA(assaHwd);
                }
                else
                {
                    MessageBox.Show("SPID is null!");
                }
                break;
            }
        }

        /*
         * 删除进程 
         */
        [DllImport("kernel32.dll")]
        static extern void ExitProcess(uint uExitCode);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);
        private void KillDieProcess(int pID)
        {

            Process process = System.Diagnostics.Process.GetProcessById(pID);
            //process.Kill();
            //process.Close();
            Process.Start("taskkill", "/PID " + pID + " /F");
            //process.CloseMainWindow();

        }

        private void CloseAllASSA()
        {
            bStop = true;
            System.Threading.Thread.Sleep(5000);
            foreach (ListViewItem item in this.doubleBufferlvActiveInfo.Items)
            {
                int proID = -1;
                int.TryParse(item.SubItems[1].Text, out proID);
                KillDieProcess(proID);
                System.Threading.Thread.Sleep(waitTime);
            }
            lstAcitveInfo.Clear();
            doubleBufferlvActiveInfo.Items.Clear();

        }

        #endregion

        #region 委托刷新数据
        private bool bStop;
        private void DoService()
        {
            int rTime = 0;
            int.TryParse(cm.GetConfigurationData("RefreshTime"), out rTime);
            while (!bStop)
            {
                try
                {
                    refreshDataInv(refreshData);

                    System.Threading.Thread.Sleep(rTime);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //搞个最简单滴取值滴方法~
        private void refreshData()
        {
            LoadActiveData(IntPtr.Zero, String.Empty);

           // MonitorActs();

        }

        private delegate void RefreshDelegate();
        //判断一下是不是该用Invoke滴~，不是就直接返回~
        private void refreshDataInv(RefreshDelegate myDelegate)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(myDelegate);
            }
            else
            {
                myDelegate();
            }
        }
        private void StartDelegateRefresh()
        {
            LogThread = new Thread(new ThreadStart(DoService));
            if (bStop == true)
            {
                //设置线程为后台线程,那样进程里就不会有未关闭的程序了
                LogThread.IsBackground = true;
                if (bStop == true)
                {
                    LogThread.Start();//起线程
                }

                bStop = false;
                btnRefresh.Text = "停止刷新";
            }
            else
            {
                bStop = true;
                LogThread.Abort();
                btnRefresh.Text = "开始刷新";
            }
        }

        private Dictionary<String, String> GetActDct(String act)
        {
            Dictionary<String, String> dctRet = new Dictionary<string, string>();
            foreach (Dictionary<String, String> dct in lstActList)
            {
                if (dct.ContainsValue(act))
                {
                    dctRet = dct;
                    break;
                }
            }
            return dctRet;
        }


        private void MonitorActs()
        {
            if (doubleBufferlvActiveInfo.Items.Count < 10)
            {
                foreach (Dictionary<String, String> dct in lstActList)
                {
                    bool isExit = false;
                    foreach (ListViewItem item in doubleBufferlvActiveInfo.Items)
                    {
                        String act = String.Format("{0}", item.SubItems[4].Text);
                        if (dct.ContainsValue(act))
                        {
                            isExit = true;
                        }
                    }

                    if (!isExit)
                    {
                        StartStoneage(dct);
                    }
                }
            }
        }




        private void ClickErrorWindow()
        {
            string strx = String.Empty;
            IntPtr errHwnd = IntPtr.Zero;

            EnumMainWindowsCallback(IntPtr.Zero);

            foreach (KeyValuePair<IntPtr, string> kv in dctMainWindow)
            {
                if (kv.Value.Contains("应用程序错误"))
                //if (kv.Value.Contains("脚本"))
                {
                    errHwnd = (IntPtr)kv.Key;
                    //MessageBox.Show(kv.Key + " | " + kv.Value);
                    break;
                }
            }


            if (errHwnd != IntPtr.Zero)
            {
                IntPtr errYESHwnd = IntPtr.Zero;
                dctMainWindow.Clear();
                EnumMainWindowsCallback(errHwnd);
                foreach (KeyValuePair<IntPtr, string> kv in dctMainWindow)
                {
                    if (kv.Value.Contains("确定"))
                    {
                        errYESHwnd = (IntPtr)kv.Key;
                        //MessageBox.Show("2: " + kv.Key + " | " + kv.Value);
                        //System.Threading.Thread.Sleep(5000);
                        cm.ClickButton(errYESHwnd);
                        System.Threading.Thread.Sleep(waitTime);
                        break;
                    }
                }
            }

        }

       
        private void btnClickMe_Click(object sender, EventArgs e)
        {
            //ClickErrorWindow();


            //Test();
            //GetPixelColor();

            //ClickErrorWindow();
            //TaskBarUtil.RefreshNotificationArea();

            //TaskBarUtil.RefreshNotification();
            //1245780
            IntPtr p = new IntPtr(1245780);
            cm.BringTopASSA(p);
        }
        #endregion

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

        private int ReadSADataFromRAMInt(int PPID, String address)
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

        private string ReadSADataFromRAMString(int PPID, String address)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;
            lstccount.SelectionMode = SelectionMode.MultiExtended;
            LoadAccounts();
            InitialDoubleBufferListView();
            double.TryParse(cm.GetConfigurationData("KillMem") ,out killMem) ;

            bStop = true;
            if (bStop == true)
            {
                btnRefresh.Text = "开始刷新";
            }
            else
            {
                btnRefresh.Text = "停止刷新";
            }
        }

        private void BtnStartALL_Click(object sender, EventArgs e)
        {
            StartAccounts(true);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartAccounts(false);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            StartDelegateRefresh();
        }

        private void btnCloseALL_Click(object sender, EventArgs e)
        {
            CloseAllASSA();
        }

        private void btnLoadInfo_Click(object sender, EventArgs e)
        {
            LoadActiveData(IntPtr.Zero, String.Empty);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (!isRun)
            {
                if (!lstStopTime.Contains(DateTime.Now.Hour.ToString()))
                {
                    isRun = true;
                    refreshData();
                    isRun = false;
                }
            }
        }

        private bool start = false;
        private void btnTimer_Click(object sender, EventArgs e)
        {
           
           
            String stopTime = cm.GetConfigurationData("StopTime");
            
            String[] strs = stopTime.Split(',');
            lstStopTime.Clear();
            lstStopTime = new List<string>(strs);
            if (start == false)
            {
                timer1.Enabled = true;
                start = true;
                btnTimer.Text = "停止Timer";
            }
            else
            {
                timer1.Enabled = false;
                start = false;
                btnTimer.Text = "开始Timer";
            }

        }

        private void btnFlight_Click(object sender, EventArgs e)
        {
            StartFilghtAccounts();
        }

        private void btnClearn_Click(object sender, EventArgs e)
        {
            StartCleanAccounts();
        }

        #region STW
        //
        private void StartFlightSTW()
        {

        }

        private void StartFilghtAccountsSTW()
        {
            Dictionary<String, String> dctLoginInfo = new Dictionary<string, string>();
            var items = lstccount.SelectedItems;
            int i = 1;
            foreach (var item in items)
            {
                dctLoginInfo = new Dictionary<string, string>();
                cm.GetUserNamePWD(item.ToString(), ref dctLoginInfo);
                //StartStoneageForFlight(dctLoginInfo, i);
                System.Threading.Thread.Sleep(waitTime);
                i++;
            }
        }

        private bool StartSelftSTW(ref IntPtr mainHanlder, String sequence)
        {
            try
            {

                string assaExe = @"D:\GAME\STW\外挂STW\STWb2.exe";
                string assaPath = String.Format("{0}", cm.GetConfigurationData("ASSAPATH_base"));
                int processID = cm.StartAPP(Path.Combine(assaPath, assaExe), assaPath);
                System.Threading.Thread.Sleep(waitTime);
                mainHanlder = cm.GetWnd(processID, "ThunderRT6FormDC", "石器时代");
                if (mainHanlder == IntPtr.Zero)
                {
                    MessageBox.Show("Start Fail!");
                    return false;
                }
                //Load all the hwnd
                dctASSA.Clear();
                EnumWindowsCallback(mainHanlder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }

        private void StartStoneageForFlightSTW(Dictionary<String, String> dctLoginInfo, int seq)
        {
            //Start ASSA
            IntPtr mainHanlder = IntPtr.Zero;
            bool isDone = true;
            isDone = StartSelftASSA(ref mainHanlder, dctLoginInfo["squence"]);
            if (!isDone)
            {
                MessageBox.Show("Initail ASSA Fail!");
                return;
            }
            System.Threading.Thread.Sleep(waitTime);

            //Move
            MoveAssaWindows(mainHanlder, seq);


            //激活石器
            IntPtr iBShiqi = cm.FindWindowEx(mainHanlder, "激活石器", true);
            if (iBShiqi == IntPtr.Zero)
            {
                MessageBox.Show("Start Shiqi Fail!");
                return;
            }
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseRightClick(iBShiqi);
            System.Threading.Thread.Sleep(waitTime);

            //获取石器句柄
            IntPtr saHanlder = cm.GetMainWindowhWnd("StoneAge", "StoneAge");
            if (saHanlder == IntPtr.Zero)
            {
                MessageBox.Show("Get SA hWnd Fail!");
                return;
            }
            //绑定数据
            LoadActiveData(saHanlder, dctLoginInfo["userName"]);

            //Input username and Password
            InputUserNameNPWD(dctLoginInfo["userName"], dctLoginInfo["password"]);

            System.Threading.Thread.Sleep(waitTime);

            //前置ASSA
            //PutASSATOTOP(mainHanlder);

            //点击自动登陆
            CLickAutoLogin();

            bool isF9 = false;
            while (!isF9)
            {
                cm.BringToFront(saHanlder);

                RECT rct;
                GetWindowRect(saHanlder, out rct);
                Rectangle screen = Screen.FromHandle(saHanlder).Bounds;
                Point pt = new Point(rct.Left, rct.Bottom);// + rct.Top);

                if (pt.Y == 640)
                {
                    isF9 = true;
                    break;
                }

                SendKeys.Send("{F9}");
                System.Threading.Thread.Sleep(2000);
            }

            //移动石器窗口
            MoveStoneageWindows(saHanlder, seq);
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            DeleteSelected();
        }

        private void DeleteSelected()
        {
            foreach (ListViewItem item in this.doubleBufferlvActiveInfo.SelectedItems)
            {
                //second column PID
                int proID = -1;
                int.TryParse(item.SubItems[1].Text, out proID);
                if (proID > 0)
                {
                    KillDieProcess(proID);
                    doubleBufferlvActiveInfo.Items.RemoveAt(item.Index);
                }
            }
        }

        //应用程序发送此消息来设置一个窗口的文本   
        const int WM_SETTEXT = 0x0C;
        //应用程序发送此消息来复制对应窗口的文本到缓冲区   
        const int WM_GETTEXT = 0x0D;
        //得到与一个窗口有关的文本的长度（不包含空字符）   
        const int WM_GETTEXTLENGTH = 0x0E;
        const int CB_SETCURSEL = 0x014E;
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
       // private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, StringBuilder lParam);

        private void Test()
        {
            /*
            IntPtr mainHanlder = IntPtr.Zero;
            string assaExe = cm.GetConfigurationData("ASSAEXE");
            string assaPath = String.Format("{0}{1}", cm.GetConfigurationData("ASSAPATH"), "01");
            int processID = cm.StartAPP(Path.Combine(assaPath, assaExe), assaPath);
            System.Threading.Thread.Sleep(waitTime);
            mainHanlder = cm.GetWnd(processID, "ThunderRT6FormDC", "石器时代");
            if (mainHanlder == IntPtr.Zero)
            {
                MessageBox.Show("Start Fail!");
            }
             */
            //Load all the hwnd
            IntPtr pss = new IntPtr(135804);
            EnumWindowsCallback(pss);
            String s = "";
            foreach (var item in dctASSA)
            {
                s += item.Key + ":" + item.Value + "\r\n";
            }
            MessageBox.Show(s);

           

            IntPtr p = new IntPtr(135770);
            const int buffer_size = 1024;
            StringBuilder buffer = new StringBuilder(buffer_size);
            SendMessage(p, CB_SETCURSEL, 1, buffer);










            IntPtr ps = new IntPtr(135804);
            SendMessage(ps, WM_GETTEXT, buffer_size, buffer);
            MessageBox.Show(buffer.ToString());
   
            //cm.MouseRightClick(p);
            String a = "";

        }
       
    }

}



