using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using XAPP.COMLIB;
using System.IO;
using System.Diagnostics;
using System.Management;

namespace ASSAMON
{
    public partial class frmMain : Form
    {
        #region 变量

        private COMLIB cm = new COMLIB();
        private AccessControl accessControl = new AccessControl();
        private Operations operations = new Operations();
        private Constant constant = new Constant();
        private Dictionary<IntPtr, String> dctMainWindow = new Dictionary<IntPtr, String>();
        private Dictionary<IntPtr, String> dctASSA = new Dictionary<IntPtr, String>();
        private Dictionary<IntPtr, IntPtr> dcthwdProId = new Dictionary<IntPtr, IntPtr>();
        Dictionary<String, String> dctProcAct = new Dictionary<string, string>();
        private StartUPType startUPType;

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        private string RunningPath = System.AppDomain.CurrentDomain.BaseDirectory;
        List<ActiveInfo> lstAcitveInfo = new List<ActiveInfo>();
        DoubleBufferListView doubleBufferlvActiveInfo = new DoubleBufferListView();
        List<Dictionary<String, String>> lstActList = new List<Dictionary<string, string>>();
        private List<String> lstStopTime = new List<string>();
        Dictionary<String, IntPtr> dctProc = new Dictionary<String, IntPtr>();

        private double killMem = 0;
        //Thread LogThread;
        private bool isRun = false;
        private static object LockObject = new Object();
        private bool start = false;

        #endregion

        public frmMain()
        {
            InitializeComponent();
        }

        #region Event
        private void btnTimer_Click(object sender, EventArgs e)
        {
            int interval = 10000;
            Int32.TryParse(cm.GetConfigurationData(constant.TimerInterval), out interval);
            timer1.Interval = interval;
            if (start == false)
            {
                timer1.Enabled = true;
                start = true;
                btnTimer.Text = "停止Timer";
                btnTimer.BackColor = Color.LightGreen;
                tSSlblTimer.Text = "| Timer 运行中....";
            }
            else
            {
                timer1.Enabled = false;
                start = false;
                btnTimer.Text = "开始Timer";
                btnTimer.BackColor = Color.LightYellow;
                tSSlblTimer.Text = "| Timer 休息中....";

            }
        }

        private void btnCloseSelected_Click(object sender, EventArgs e)
        {
            DeleteSelected();
        }

        private void btnStartSelected_Click(object sender, EventArgs e)
        {
            //Auto Run Script: True
            startUPType = StartUPType.FIVE;
            tlsddbStartUpType.Text = StartUPType.FIVE.ToString();
            startUPType = StartUPType.FIVE;
            StartAccounts(true);
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            LoadActiveData();
        }

        private void btnCloseAll_Click(object sender, EventArgs e)
        {
            String saName = System.Configuration.ConfigurationManager.AppSettings["SAName"];
            foreach (Process p in Process.GetProcessesByName(saName))
            {
                AccessControl.KillDieProcess(p.Id);
                System.Threading.Thread.Sleep(100);
            }

            //记录句柄
            String hwdFileSA = Path.Combine(RunningPath, "hwdFileSA.txt");
            String hwdFileASSA = Path.Combine(RunningPath, "hwdFileASSA.txt");
            String hwdFileAct = Path.Combine(RunningPath, "hwdFileAct.txt");

            if (File.Exists(hwdFileSA))
            {
                File.Delete(hwdFileSA);
            }

            if (File.Exists(hwdFileASSA))
            {
                File.Delete(hwdFileASSA);
            }

            if (File.Exists(hwdFileAct))
            {
                File.Delete(hwdFileAct);
            }

            System.Threading.Thread.Sleep(100);
            LoadActiveData();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //LoadActiveData();
            //CheckRunningActs();
            /*
            String ss = String.Empty;
            String saName = System.Configuration.ConfigurationManager.AppSettings["SAName"];
            foreach (Process p in Process.GetProcessesByName(saName))
            {
                ss += GetCharAct(AccessControl.ReadSADataFromRAMString(p.Id, cm.GetConfigurationData("CharAct"))) + "\n";

            }
            MessageBox.Show(ss);
            */
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Process");
            foreach (ManagementObject mo in searcher.Get())
            {
                String a = mo["Name"].ToString().Trim() + "," + mo["ProcessId"].ToString().Trim() + "," + String.Format("{0}", mo["Status"]);
                if (a.Contains("sa_8001sf"))
                { 
                MessageBox.Show(a);
                }
            }



        }

        private void btnSW_Click(object sender, EventArgs e)
        {
            //Auto Run Script: True
            startUPType = StartUPType.SHENGWANG;
            tlsddbStartUpType.Text = StartUPType.SHENGWANG.ToString();
            startUPType = StartUPType.SHENGWANG;
            StartAccounts(true);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Set APP Version
            string ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            tsslblVersion.Text = "VER: " + ver;

            //Timer
            timer1.Enabled = false;
            tSSlblTimer.Text = "| Timer 休息中....";

            LoadAccounts();
            InitialDoubleBufferListView();
            Loadhwd();
            foreach (StartUPType sup in Enum.GetValues(typeof(StartUPType)))
            {
                tlsddbStartUpType.DropDownItems.Add(sup.ToString());
            }
            tlsddbStartUpType.Text = StartUPType.FIVE.ToString();
            startUPType = StartUPType.FIVE;

            //右下角
            int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
            int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - this.Height;
            Point p = new Point(x, y);
            this.PointToScreen(p);
            this.Location = p;

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!isRun)
            {
                if (!lstStopTime.Contains(DateTime.Now.Hour.ToString()))
                {
                    isRun = true;
                    accessControl.ClickErrorWindow(constant.waitTime);
                    refreshData();
                    CheckRunningActs();
                    isRun = false;
                }
            }
        }

        #endregion

        /*
         * Load Login Account
         */
        private void LoadAccounts()
        {
            String accountFile = Path.Combine(RunningPath, constant.accountINI);
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


        #region Custom View
        /*
         * Load Grid View
         */
        private void InitialDoubleBufferListView()
        {
            doubleBufferlvActiveInfo.GridLines = true;
            doubleBufferlvActiveInfo.FullRowSelect = true;
            doubleBufferlvActiveInfo.HideSelection = false;
            doubleBufferlvActiveInfo.Location = new System.Drawing.Point(17, 110);
            doubleBufferlvActiveInfo.Name = "doubleBufferlvActiveInfo";
            doubleBufferlvActiveInfo.Size = new System.Drawing.Size(750, 120);
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
            doubleBufferlvActiveInfo.Columns.Add("名称", 70);
            doubleBufferlvActiveInfo.Columns.Add("帐号", 100);
            doubleBufferlvActiveInfo.Columns.Add("声望", 70);
            doubleBufferlvActiveInfo.Columns.Add("坐标", 70);
            doubleBufferlvActiveInfo.Columns.Add("地图", 50);
            doubleBufferlvActiveInfo.Columns.Add("句柄", 70);
            doubleBufferlvActiveInfo.Columns.Add("石头", 80);
            doubleBufferlvActiveInfo.Columns.Add("HP", 50);

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

        #endregion

        /*
         * 启动账号
         */
        private void StartAccounts(bool isStartScript)
        {
            //登录
            Dictionary<String, String> dctLoginInfo = new Dictionary<string, string>();
            foreach (int i in lstccount.SelectedIndices)
            {
                String item = lstccount.Items[i].ToString();
                dctLoginInfo = new Dictionary<string, string>();
                cm.GetUserNamePWD(item.ToString(), ref dctLoginInfo);
                StartStoneage(dctLoginInfo, isStartScript, i + 1);
                System.Threading.Thread.Sleep(3000);
            }


            //记录句柄
            String hwdFileSA = Path.Combine(RunningPath, "hwdFileSA.txt");
            String hwdFileASSA = Path.Combine(RunningPath, "hwdFileASSA.txt");
            String hwdFileAct = Path.Combine(RunningPath, "hwdFileAct.txt");

            //SA
            String content = String.Empty;
            if (File.Exists(hwdFileSA))
            {
                File.Delete(hwdFileSA);
            }
            content = String.Empty;
            foreach (var item in dcthwdProId)
            {
                content += item.Key + ":" + item.Value + "\r\n";
            }

            File.WriteAllText(hwdFileSA, content);

            //ASSA
            content = String.Empty;
            if (File.Exists(hwdFileASSA))
            {
                File.Delete(hwdFileASSA);
            }
            content = String.Empty;
            foreach (var item in dctProc)
            {
                content += item.Key + ":" + item.Value + "\r\n";
            }

            File.WriteAllText(hwdFileASSA, content);

            //Account
            content = String.Empty;
            if (File.Exists(hwdFileAct))
            {
                File.Delete(hwdFileAct);
            }
            content = String.Empty;
            foreach (var item in dctProcAct)
            {
                content += item.Key + ":" + item.Value + "\r\n";
            }

            File.WriteAllText(hwdFileAct, content);
        }

        private bool StartASSA(ref IntPtr mainHanlder, String sequence)
        {
            try
            {
                string assaExe = cm.GetConfigurationData(constant.assaEXE);
                string assaPath = cm.GetConfigurationData(constant.assaPath);
                if (!Directory.Exists(assaPath))
                {
                    assaPath = String.Format("{0}_{1}", assaPath, sequence);
                }

                int processID = cm.StartAPP(Path.Combine(assaPath, assaExe), assaPath);
                System.Threading.Thread.Sleep(constant.waitTime);
                mainHanlder = cm.GetWnd(processID, "ThunderRT6FormDC", cm.GetConfigurationData(constant.saClassName));
                if (mainHanlder == IntPtr.Zero)
                {
                    MessageBox.Show("Start Fail!");
                    return false;
                }
                //Load all the hwnd
                dctASSA.Clear();
                dctASSA = accessControl.EnumWindowsCallback(mainHanlder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        /*
         * 启动石器
         */
        private void StartStoneage(Dictionary<String, String> dctLoginInfo, bool isStartScript, int seq)
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
            System.Threading.Thread.Sleep(constant.waitTime);


            //激活石器
            IntPtr iBShiqi = cm.FindWindowEx(mainHanlder, cm.GetConfigurationData(constant.saStartButtonClassName), true);
            if (iBShiqi == IntPtr.Zero)
            {
                MessageBox.Show("Start Shiqi Fail!");
                return;
            }
            System.Threading.Thread.Sleep(constant.waitTime);
            cm.MouseRightClick(iBShiqi);
            System.Threading.Thread.Sleep(constant.waitTime);


            //获取石器句柄
            IntPtr saHanlder = cm.GetMainWindowhWnd("StoneAge", "StoneAge");
            if (saHanlder == IntPtr.Zero)
            {
                MessageBox.Show("Get SA hWnd Fail!");
                return;
            }
            //保存句柄
            IntPtr sauPid = IntPtr.Zero;
            AccessControl.GetWindowThreadProcessId(saHanlder, ref sauPid);
            if (!dctProc.ContainsKey(sauPid.ToString()))
            {
                dctProc.Add(sauPid.ToString(), mainHanlder);
            }

            dcthwdProId.Add(sauPid, saHanlder);

            //Input username and Password
            operations.InputUserNameNPWD(dctLoginInfo["userName"], dctLoginInfo["password"], constant.waitTime);
            if (!dctProcAct.ContainsKey(sauPid.ToString()))
            {
                dctProcAct.Add(sauPid.ToString(), dctLoginInfo["userName"]);
            }

            //前置ASSA
            operations.PutASSATOTOP(mainHanlder, constant.waitTime);

            //点击资料显示
            operations.MoveInformation(dctLoginInfo, dctASSA, constant.waitTime, seq, startUPType);

            //点击自动登陆
            operations.CLickAutoLogin(dctASSA, constant.waitTime);
          

            if (isStartScript)
            {
                //点击脚本
                IntPtr ibtScript = cm.GetHwndByValue(dctASSA, "脚本");
                cm.MouseRightClick(ibtScript);
                System.Threading.Thread.Sleep(constant.waitTime);

                //运行脚本
                if (startUPType == StartUPType.SHENGWANG)
                {
                    operations.RunScript(dctLoginInfo["type"], constant.waitTime);
                }
                else
                {
                    operations.RunScript(constant.waitTime);
                }
                

                //启动脚本
                operations.ClickRunScript(mainHanlder, dctASSA, constant.waitTime);
            }

            //隐藏石器
            operations.CliclHideASSA(mainHanlder, dctASSA, constant.waitTime);

            //Move main ASSA window
            if (startUPType.Equals(StartUPType.FIVE) || startUPType.Equals(StartUPType.ZHENGLI) || startUPType.Equals(StartUPType.LIANCHONG))
            {
                operations.MoveStoneageWindows(mainHanlder, seq);
            }

            if (startUPType.Equals(StartUPType.SHENGWANG))
            {
                SendKeys.SendWait("{F9}");
            }

            LoadActiveData();
        }

        private void LoadActiveData()
        {
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
                //hwd
                IntPtr hwd = IntPtr.Zero;
                dcthwdProId.TryGetValue(new IntPtr(ai.ProID), out hwd);

                //Memory
                ai.Memory = process.WorkingSet64 / (1024.0 * 1024.0);

                if (ai.Memory < killMem)
                {
                    dctProcAct.Remove(i.ToString()); //删除账号
                                                     //Get Hwd
                                                     //Coding
                                                     //////////////////////////////////////////
                                                     //AccessControl.SendMessage(hwd, Constant.WM_CLOSE, 0, 0);//get Handler 
                    AccessControl.KillDieProcess(i);
                    //删除list 删除lv
                    System.Threading.Thread.Sleep(1000);
                    continue;
                }

                ListViewItem lvi = new ListViewItem();
                lvi.Text = String.Format("{0}", doubleBufferlvActiveInfo.Items.Count + 1);
                lvi.SubItems.Add(i.ToString());//PID

                lvi.SubItems.Add(String.Format("{0}", ai.Memory));//Memory

                //名称
                ai.Name = AccessControl.ReadSADataFromRAMString(i, cm.GetConfigurationData("CharName"));
                lvi.SubItems.Add(ai.Name);//名称

                //帐号
                //**********************************************************************************

                ai.Account = AccessControl.ReadSADataFromRAMString(i, cm.GetConfigurationData("CharAct"));
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
                }
                lvi.SubItems.Add(actMem);//帐号
                //*************************************************************************************


                //声望
                ai.ShengWang = AccessControl.ReadSADataFromRAMInt(i, cm.GetConfigurationData("addrSW"));
                lvi.SubItems.Add(String.Format("{0}", ai.ShengWang));//声望
                //XY, 坐标
                int x = AccessControl.ReadSADataFromRAMInt(i, cm.GetConfigurationData("addrX"));
                int y = AccessControl.ReadSADataFromRAMInt(i, cm.GetConfigurationData("addrY"));
                ai.XY = String.Format("{0},{1}", x, y);
                lvi.SubItems.Add(ai.XY);//坐标
                //Map
                int map = AccessControl.ReadSADataFromRAMInt(i, cm.GetConfigurationData("addrMap"));
                lvi.SubItems.Add(String.Format("{0}", map));//地图
                ai.MAP = String.Format("{0}", map);
                //hWnd
                ai.Hwnd = hwd;
                lvi.SubItems.Add(hwd.ToString());
                //Stone
                int stone = AccessControl.ReadSADataFromRAMInt(i, cm.GetConfigurationData("CharStone"));
                ai.Stone = String.Format("{0}", stone);
                lvi.SubItems.Add(String.Format("{0}", stone));//石头
                //HP
                int hp = AccessControl.ReadSADataFromRAMInt(i, cm.GetConfigurationData("CharHP"));
                ai.HP = String.Format("{0}", hp);
                lvi.SubItems.Add(String.Format("{0}", hp));//HP

                lstAI.Add(ai);
                this.doubleBufferlvActiveInfo.Items.Add(lvi);
            }
        }

        private bool CheckActAlreadyRunning(String act)
        {
            bool blExisted = false;
            String saName = System.Configuration.ConfigurationManager.AppSettings["SAName"];
            foreach (Process p in Process.GetProcessesByName(saName))
            {
                String actTmp = GetCharAct(AccessControl.ReadSADataFromRAMString(p.Id, cm.GetConfigurationData("CharAct")));
                if (actTmp.Equals(act))
                {
                    blExisted = true;
                    break;
                }

            }

            return blExisted;
        }

        private String GetCharAct(string addressAct)
        {
            foreach (Dictionary<String, String> dct in lstActList)
            {
                String str = String.Empty;
                dct.TryGetValue("userName", out str);
                int actLen = str.Length;
                if (addressAct.Length < actLen) continue;
                String tmpaddressAct = addressAct.Substring(0, actLen);
                if (tmpaddressAct.Equals(str))
                {
                    return str;
                }
            }
            return String.Empty;
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

        private void DeleteSelected()
        {
            foreach (ListViewItem item in this.doubleBufferlvActiveInfo.SelectedItems)
            {
                //second column PID
                /*
                int hwd = -1;
                int.TryParse(item.SubItems[8].Text, out hwd);
                if (hwd > 0)
                {
                    AccessControl.SendMessage(new IntPtr(hwd), Constant.WM_CLOSE, 0, 0);//get Handler 
                    doubleBufferlvActiveInfo.Items.RemoveAt(item.Index);
                }*/

                int proID = -1;
                int.TryParse(item.SubItems[1].Text, out proID);
                if (proID > 0)
                {
                    AccessControl.KillDieProcess(proID);
                    doubleBufferlvActiveInfo.Items.RemoveAt(item.Index);
                }
            }
        }


        /// <summary>
        /// 处理 SA， ASSA，账号
        /// </summary>
        private void Loadhwd()
        {
            String hwdFileSA = Path.Combine(RunningPath, "hwdFileSA.txt");
            String hwdFileASSA = Path.Combine(RunningPath, "hwdFileASSA.txt");
            String hwdFileAct = Path.Combine(RunningPath, "hwdFileAct.txt");
            String content = String.Empty;
            dcthwdProId.Clear();
            dctProc.Clear();
            dctProcAct.Clear();
            //Handle Hwd
            String saName = System.Configuration.ConfigurationManager.AppSettings["SAName"];
            List<int> lstProID = new List<int>();
            foreach (Process p in Process.GetProcessesByName(saName))
            {
                lstProID.Add(p.Id);
            }

            if (lstProID.Count > 0)
            {
                //SA hwd
                if (File.Exists(hwdFileSA))
                {
                    //Read File
                    String[] lines = File.ReadAllLines(hwdFileSA);
                    foreach (String s in lines)
                    {
                        if (String.IsNullOrEmpty(s)) continue;
                        IntPtr prodID = IntPtr.Zero;
                        IntPtr hwd = IntPtr.Zero;
                        int tmpPID;
                        String[] strs = s.Split(':');
                        Int32.TryParse(strs[0], out tmpPID);
                        if (lstProID.Contains(tmpPID))
                        {
                            prodID = new IntPtr(Convert.ToInt32(strs[0]));
                            hwd = new IntPtr(Convert.ToInt32(strs[1]));
                            dcthwdProId.Add(prodID, hwd);
                        }
                    }
                }

                //ASSA hwd
                if (File.Exists(hwdFileASSA))
                {
                    //Read File
                    String[] lines = File.ReadAllLines(hwdFileASSA);
                    foreach (String s in lines)
                    {
                        if (String.IsNullOrEmpty(s)) continue;
                        IntPtr prodID = IntPtr.Zero;
                        IntPtr hwd = IntPtr.Zero;
                        int tmpPID;
                        String[] strs = s.Split(':');
                        Int32.TryParse(strs[0], out tmpPID);
                        if (lstProID.Contains(tmpPID))
                        {
                            prodID = new IntPtr(Convert.ToInt32(strs[0]));
                            hwd = new IntPtr(Convert.ToInt32(strs[1]));
                            dctProc.Add(prodID.ToString(), hwd);
                        }
                    }
                }

                //ACT 
                if (File.Exists(hwdFileAct))
                {
                    //Read File
                    String[] lines = File.ReadAllLines(hwdFileAct);
                    foreach (String s in lines)
                    {
                        if (String.IsNullOrEmpty(s)) continue;
                        IntPtr prodID = IntPtr.Zero;
                        String act = String.Empty;
                        int tmpPID;
                        String[] strs = s.Split(':');
                        Int32.TryParse(strs[0], out tmpPID);
                        if (lstProID.Contains(tmpPID))
                        {
                            prodID = new IntPtr(Convert.ToInt32(strs[0]));
                            act = strs[1];
                            dctProcAct.Add(prodID.ToString(), act);
                        }
                    }
                }
            }
            else
            {
                if (File.Exists(hwdFileSA))
                {
                    File.Delete(hwdFileSA);
                }

                if (File.Exists(hwdFileASSA))
                {
                    File.Delete(hwdFileASSA);
                }

                if (File.Exists(hwdFileAct))
                {
                    File.Delete(hwdFileAct);
                }
            }
        }

        private void refreshData()
        {
            LoadActiveData();

        }

        private void CheckRunningActs()
        {
            int processNum = 0;
            int.TryParse(cm.GetConfigurationData("ProcessNum"), out processNum);
            List<int> lstProID = new List<int>();
            String saName = System.Configuration.ConfigurationManager.AppSettings["SAName"];
            foreach (Process p in Process.GetProcessesByName(saName))
            {
                lstProID.Add(p.Id);
            }

            if (lstProID.Count < processNum)
            {
                if (startUPType.Equals(StartUPType.SHENGWANG) || startUPType.Equals(StartUPType.FIVE))
                {
                    foreach (int i in lstccount.SelectedIndices)
                    {
                        String item = lstccount.Items[i].ToString();
                        String[] strs = item.ToString().Split('|');
                        String userAct = strs[0];
                        if (String.IsNullOrEmpty(userAct)) continue;
                        if (!CheckActAlreadyRunning(userAct))
                        {
                            Dictionary<String, String> dct = GetActDct(userAct);
                            StartStoneage(dct, true, i + 1);
                            System.Threading.Thread.Sleep(100);
                            LoadActiveData();
                        }

                           
                        System.Threading.Thread.Sleep(3000);
                    }

                }

            }

          
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

      

        private void tlsddbStartUpType_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string msg = String.Format("{0}", e.ClickedItem.Text);
            startUPType = (StartUPType)Enum.Parse(typeof(StartUPType), msg);
            this.tlsddbStartUpType.Text = msg;
        }
    }
}
