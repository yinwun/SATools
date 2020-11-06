using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Configuration;
using XAPP.COMLIB;

namespace DeleteAct
{
    public partial class Form1 : Form
    {
        private COMLIB cm = new COMLIB();
        private Dictionary<IntPtr, String> dctASSA = new Dictionary<IntPtr, String>();
        private Dictionary<IntPtr, String> dctMainWindow = new Dictionary<IntPtr, String>();
        private string RunningPath = System.AppDomain.CurrentDomain.BaseDirectory;
        private int waitTime = 1000;//mls
        public Form1()
        {
            InitializeComponent();
        }

        #region 获取ASSA所有子控件

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

        private void StartStoneage()
        {
            //Start ASSA
            IntPtr mainHanlder = IntPtr.Zero;
            bool isDone = true;
            isDone = StartASSA(ref mainHanlder);
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

            String accountFile = Path.Combine(RunningPath, "Accounts.ini");
            String[] acts = File.ReadAllLines(accountFile);

            for (int i=0; i<acts.Length;i++)
            {
                if (acts[i].Length <= 0) continue;
                String[] strs = acts[i].Split('|');
                string userName = strs[0];
                string pwd = strs[1];
                //Input username and Password
                InputUserNameNPWD(userName, pwd);

                DeleteReturn();

                System.Threading.Thread.Sleep(waitTime);
            }

            MessageBox.Show("OK");


        }

        private bool StartASSA(ref IntPtr mainHanlder)
        {
            try
            {
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
            //Click Logon
            cm.GetXY("LoginButton", ref loginX, ref loginY);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            //Click Line1
            cm.GetXY("Line1", ref loginX, ref loginY);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            //Line DX
            cm.GetXY("Line1DX", ref loginX, ref loginY);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            //click del
            cm.GetXY("Delbutton", ref loginX, ref loginY);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            //input pwd
            SendKeys.SendWait(password);
            //click del button
            cm.GetXY("DelConfrimBtn", ref loginX, ref loginY);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();

        }

        private void DeleteReturn()
        {
            int loginX = -1;
            int loginY = -1;
            //Del return
            cm.GetXY("BtnDelReturn", ref loginX, ref loginY);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            //Click return
            cm.GetXY("BtnReturn", ref loginX, ref loginY);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            //Del
            cm.GetXY("LoginUserNameXY", ref loginX, ref loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            for (int i =0;i<20;i++)
            { 
                SendKeys.SendWait("{BACKSPACE}");
            }
            //PWD
            cm.GetXY("LoginPasswordXY", ref loginX, ref loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            for (int i = 0; i < 20; i++)
            {
                SendKeys.SendWait("{BACKSPACE}");
            }

        }
        private void CreateNewUserPwd(String userName, String password)
        {
            int loginX = -1;
            int loginY = -1;
            //Click create
            cm.GetXY("BtnCreateChar", ref loginX, ref loginY);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            // Select char
            cm.GetXY("BtnSelectChar", ref loginX, ref loginY);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            //Confirm Char
            cm.GetXY("BtnCfChar", ref loginX, ref loginY);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            //Char Name
            cm.GetXY("BtnCharName", ref loginX, ref loginY);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            SendKeys.SendWait("AAAA");
            //Char Pro
            cm.GetXY("BtnCharPro", ref loginX, ref loginY);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            for(int i = 0; i < 20; i++)
            {
                cm.MouseSingleClick();
                System.Threading.Thread.Sleep(waitTime);
            }
            System.Threading.Thread.Sleep(waitTime);
            //Create
            cm.GetXY("BtnCharCreateConfrom", ref loginX, ref loginY);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            //Char var
            cm.GetXY("BtnCharVar", ref loginX, ref loginY);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            //Char var Conf
            cm.GetXY("BtnCharVarConf", ref loginX, ref loginY);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            
        }



            private void PutASSATOTOP(IntPtr mainHanlder)
        {
            //前置ASSA
            System.Threading.Thread.Sleep(waitTime);
            cm.BringToFront(mainHanlder);
            System.Threading.Thread.Sleep(waitTime);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartStoneage();
           
        }
    }
}
