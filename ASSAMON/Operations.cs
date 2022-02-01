using System;
using System.Collections.Generic;
using System.Text;
using XAPP.COMLIB;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ASSAMON
{
    public class Operations
    {
        private COMLIB cm = new COMLIB();
        AccessControl accessControl = new AccessControl();
        Constant constant = new Constant();

        private Dictionary<IntPtr, String> dctMainWindow = new Dictionary<IntPtr, String>();

        public void InputUserNameNPWD(String userName, String password, int waitTime)
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

        public void PutASSATOTOP(IntPtr mainHanlder,int waitTime)
        {
            //前置ASSA
            System.Threading.Thread.Sleep(waitTime);
            cm.BringToFront(mainHanlder);
            System.Threading.Thread.Sleep(waitTime);
        }

        public void DisplayCaptionInformation(Dictionary<String, String> dctLoginInfo, Dictionary<IntPtr, String> dctASSA, int waitTime)
        {
            if (dctLoginInfo["type"].Contains("C"))
            {

                IntPtr ubtInfo = cm.GetHwndByValue(dctASSA, "资料显示");
                cm.MouseRightClick(ubtInfo);
                System.Threading.Thread.Sleep(waitTime);

                if (dctLoginInfo["type"].Contains("1_C"))
                {
                    IntPtr infoshowHanlder = cm.GetMainWindowhWnd("ThunderRT6FormDC", "资料显示");
                    Constant.RECT rct;
                    AccessControl.GetWindowRect(infoshowHanlder, out rct);
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
        }

        public void CLickAutoLogin(Dictionary<IntPtr, String> dctASSA, int waitTime)
        {
            //点击自动登录
            IntPtr ibtAutoLogin = cm.GetHwndByValue(dctASSA, "自动登陆");
            cm.MouseRightClick(ibtAutoLogin);
            System.Threading.Thread.Sleep(waitTime);
        }

        private void ClickErrorWindow(int waitTime)
        {
            string strx = String.Empty;
            IntPtr errHwnd = IntPtr.Zero;

            dctMainWindow = accessControl.EnumMainWindowsCallback(IntPtr.Zero);

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
                dctMainWindow = accessControl.EnumMainWindowsCallback(errHwnd);
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


        public void MoveStoneageWindows(IntPtr hwnd, int seq)
        {
            Constant.RECT rct;
            AccessControl.GetWindowRect(hwnd, out rct);
            Rectangle screen = Screen.FromHandle(hwnd).Bounds;
            Point pt = new Point(rct.Left, rct.Bottom);// + rct.Top);
            int assaLength = rct.Right - rct.Left - 10;

            switch (seq)
            {
                case 1:
                    break;
                case 2:
                    cm.MoveWindow(hwnd, pt.X + Convert.ToInt32(cm.GetConfigurationData(constant.ASSAWITH)), 0 );
                    break;
                case 3:
                    cm.MoveWindow(hwnd, pt.X + Convert.ToInt32(cm.GetConfigurationData(constant.ASSAWITH))*2, 0);
                    break;
                case 4:
                    cm.MoveWindow(hwnd, pt.X + Convert.ToInt32(cm.GetConfigurationData(constant.ASSAWITH)) * 3, 0);
                    break;
                case 5:
                    cm.MoveWindow(hwnd, pt.X + Convert.ToInt32(cm.GetConfigurationData(constant.ASSAWITH)) * 4, 0);
                    break;
            }
        }

        public void MoveInformation(Dictionary<String, String> dctLoginInfo, Dictionary<IntPtr, String> dctASSA, int waitTime, int seq, StartUPType startUPType)
        {
            IntPtr ubtInfo = cm.GetHwndByValue(dctASSA, "资料显示");
            cm.MouseRightClick(ubtInfo);
            System.Threading.Thread.Sleep(waitTime);

            IntPtr infoshowHanlder = cm.GetMainWindowhWnd("ThunderRT6FormDC", "资料显示");
            Constant.RECT rct;
            AccessControl.GetWindowRect(infoshowHanlder, out rct);
            Rectangle screen = Screen.FromHandle(infoshowHanlder).Bounds;
            Point pt = new Point(rct.Left, rct.Bottom);// + rct.Top);

            int ubInfoHigth = rct.Bottom - rct.Top - 10;
            int ubInfoLength = rct.Right - rct.Left - 10;
            int fligtX = 0;
            int filgtY = 0;

            if (startUPType.Equals(StartUPType.FIVE) || startUPType.Equals(StartUPType.LIANCHONG))
            {
                switch (seq)
                {
                    case 1:
                        fligtX = pt.X + 280;
                        filgtY = rct.Top + 35;
                        break;
                    case 2:
                        cm.MoveWindow(infoshowHanlder, pt.X, pt.Y);
                        fligtX = pt.X + 280;
                        filgtY = 35 + pt.Y;
                        break;
                    case 3:
                        cm.MoveWindow(infoshowHanlder, pt.X + ubInfoLength, rct.Top);
                        fligtX = pt.X + 280 + ubInfoLength;
                        filgtY = rct.Top + 35;
                        break;
                    case 4:
                        cm.MoveWindow(infoshowHanlder, pt.X + ubInfoLength, rct.Top + ubInfoHigth);
                        fligtX = pt.X + 280 + ubInfoLength;
                        filgtY = rct.Top + ubInfoHigth + 35;
                        break;
                    case 5:
                        cm.MoveWindow(infoshowHanlder, pt.X + ubInfoLength * 2, rct.Top);
                        fligtX = pt.X + 280 + ubInfoLength * 2;
                        filgtY = rct.Top + 35;
                        break;
                }
                cm.MouseMove(fligtX, filgtY);
                System.Threading.Thread.Sleep(waitTime);
                cm.MouseSingleClick();
                System.Threading.Thread.Sleep(waitTime);
            }
            else if (startUPType.Equals(StartUPType.SHENGWANG))
            {
                fligtX = pt.X + 135;
                filgtY = rct.Top + 35;

                cm.MouseMove(fligtX, filgtY);
                System.Threading.Thread.Sleep(waitTime);
                cm.MouseSingleClick();
                System.Threading.Thread.Sleep(waitTime);
            }

        }

        public void RunScript(int waitTime)
        {

            int loginX = -1;
            int loginY = -1;
            cm.GetXY(constant.ScriptXYRoot, ref loginX, ref loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            
            //Node type
            cm.GetXY(constant.ScriptXYNode, ref loginX, ref loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
            
            //Click Script
            cm.GetXY(constant.ScriptXY, ref loginX, ref loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseMove(loginX, loginY);
            System.Threading.Thread.Sleep(waitTime);
            cm.MouseSingleClick();
            System.Threading.Thread.Sleep(waitTime);
        }

        public void RunScript(String type, int waitTime)
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


        public void ClickRunScript(IntPtr mainHanlde, Dictionary<IntPtr, String> dctASSA, int waitTime)
        {
            IntPtr ibtScript = cm.GetHwndByValue(dctASSA, "启动");
            cm.MouseRightClick(ibtScript);
            System.Threading.Thread.Sleep(waitTime);
        }

        public void CliclHideASSA(IntPtr mainHanlde, Dictionary<IntPtr, String> dctASSA, int waitTime)
        {
            IntPtr ibtScript = cm.GetHwndByValue(dctASSA, "隐藏石器");
            cm.MouseRightClick(ibtScript);
            System.Threading.Thread.Sleep(waitTime);
        }
    }
}
