using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using XAPP.COMLIB;
using XAPP.VMInfo;
using System.Collections;

namespace VBControler
{
    public partial class Form1 : Form
    {
        private string runningPath = System.AppDomain.CurrentDomain.BaseDirectory;
        private string vbEXEPath = ConfigurationManager.AppSettings["VBRunningPath"].ToString();
        private string sleepTime = ConfigurationManager.AppSettings["startSleepTime"].ToString();
        private string screenShotFolder = ConfigurationManager.AppSettings["ScreenShotFolder"].ToString();
        private string vbExe = "VBoxManage.exe";
        private Dictionary<string, string> dctVms = new Dictionary<string, string>();
        private Dictionary<string, string> dctRunningVms = new Dictionary<string, string>();
        private List<VMInfo> lstVMInfo = new List<VMInfo>();
        List<String> lstGroup = new List<string>();

        // Declare a Hashtable array in which to store the groups.
        private Hashtable[] groupTables;
        private bool isRunningXPOrLater = OSFeature.Feature.IsPresent(OSFeature.Themes);
        // Declare a variable to store the current grouping column.
        int groupColumn = 0;

        public Form1()
        {
            InitializeComponent();
            LoadVMS();
            LoadRunningVMS();

        }
        private void LoadVMS()
        {
            COMLIB cb = new COMLIB();
            string str = cb.RunCmd2(Path.Combine(vbEXEPath, vbExe), "list vms");

            if (String.IsNullOrEmpty(str))
            {
                MessageBox.Show("Error");
                return;
            }

            string[] strResults = str.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 3; i < strResults.Length; i++)
            {
                string doubleQuote = "\"";
                string tmp = strResults[i].Replace(@"\", String.Empty);
                tmp = tmp.Replace(doubleQuote, String.Empty);
                tmp = tmp.Replace("{", String.Empty);
                tmp = tmp.Replace("}", String.Empty);
                string[] strvms = tmp.Split(' ');
                if (strvms.Length == 2)
                {
                    if (dctVms.ContainsKey(strvms[1])) continue;
                    dctVms.Add(strvms[1], strvms[0]);
                }
            }
        }
        private void LoadRunningVMS()
        {
            COMLIB cb = new COMLIB();
            string str = cb.RunCmd2(Path.Combine(vbEXEPath, vbExe), "list runningvms");

            if (String.IsNullOrEmpty(str))
            {
                MessageBox.Show("Error");
                return;
            }

            string[] strResults = str.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 3; i < strResults.Length; i++)
            {
                string doubleQuote = "\"";
                string tmp = strResults[i].Replace(@"\", String.Empty);
                tmp = tmp.Replace(doubleQuote, String.Empty);
                tmp = tmp.Replace("{", String.Empty);
                tmp = tmp.Replace("}", String.Empty);
                string[] strvms = tmp.Split(' ');
                if (strvms.Length == 2)
                {
                    if (dctRunningVms.ContainsKey(strvms[1])) continue;
                    dctRunningVms.Add(strvms[1], strvms[0]);
                }
            }

        }
        private void StartVM(string vmGUID)
        {
            COMLIB cb = new COMLIB();
            string str = cb.RunCmd2(Path.Combine(vbEXEPath, vbExe), String.Format("startvm {0}", vmGUID));

            if (String.IsNullOrEmpty(str))
            {
                MessageBox.Show("Error");
                return;
            }

           if (str.Contains("started"))
            {
                int st = 0;
                int.TryParse(sleepTime, out st);
                System.Threading.Thread.Sleep(st);
            }

        }
        private void ShutDownVM(string vmGUID)
        {
            COMLIB cb = new COMLIB();
            string str = cb.RunCmd2(Path.Combine(vbEXEPath, vbExe), String.Format("controlvm {0} acpipowerbutton", vmGUID));

            if (String.IsNullOrEmpty(str))
            {
                MessageBox.Show("Error");
                return;
            }

            if (str.Contains("100%"))
            {
                
            }

        }
        private VMInfo GetVMInfo(ref VMInfo info)
        {
            COMLIB cb = new COMLIB();
            string str = cb.RunCmd2(Path.Combine(vbEXEPath, vbExe), String.Format("showvminfo {0}", info.Guid));

            if (String.IsNullOrEmpty(str))
            {
                MessageBox.Show("Error");
                return null;
            }

            string[] strResults = str.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 3; i < strResults.Length; i++)
            {
                if (strResults[i].Contains("Groups"))
                {
                    string[] strvms = strResults[i].Split(':');
                    if (strvms.Length > 0)
                    {
                        string tmpGroups = strvms[1].Replace(" ", String.Empty);
                        tmpGroups = tmpGroups.Replace("/", String.Empty);
                        info.Group = tmpGroups;
                        if (!lstGroup.Contains(tmpGroups)) lstGroup.Add(tmpGroups);
                        continue;
                    }
                }
               
            }

            return info;

        }
        private void GetscreenShoto(string vmGUID)
        {
            COMLIB cb = new COMLIB();
            string str = cb.RunCmd2(Path.Combine(vbEXEPath, vbExe), String.Format("controlvm {0} {1}.png", vmGUID, Path.Combine(runningPath, String.Format("{0}\\{1}", screenShotFolder, vmGUID))));

            if (String.IsNullOrEmpty(str))
            {
                MessageBox.Show("Error");
                return;
            }

        }
        private bool LoadVMSFromFile(bool refresh)
        {
            bool isDone = true;
            try
            {
                string filePath = Path.Combine(runningPath, "data.txt");
                if (refresh)
                {
                    VMDataFileHandling();
                }
                else
                {

                    if (!File.Exists(filePath))
                    {
                        VMDataFileHandling();
                        isDone = true;
                    }
                    else
                    {
                        string[] vmData = File.ReadAllLines(filePath);
                        foreach (string s in vmData)
                        {
                            string[] tmp = s.Split('|');
                            VMInfo info = new VMInfo();
                            info.Guid = tmp[0];
                            info.Name = tmp[1];
                            info.Group = tmp[2];
                            lstVMInfo.Add(info);
                        }
                        isDone = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                isDone = false;
            }


            return isDone;
        }

        private void InitailData()
        {
            lstVMInfo.Clear();
            dctRunningVms.Clear();
            dctVms.Clear();
            lstGroup.Clear();
            lvVMS.Clear();
        }

        private void VMDataFileHandling()
        {
            string filePath = Path.Combine(runningPath, "data.txt");

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            
            //Load ALL VMS
            LoadVMS();
            //Load ALL Running VMS
            LoadRunningVMS();
            //Load VM Info, Group

            foreach (KeyValuePair<string, string> item in dctVms)
            {
                VMInfo info = new VMInfo();
                info.Guid = item.Key;
                info.Name = item.Value;
                GetVMInfo(ref info);
                lstVMInfo.Add(info);
                string vmData = String.Format("{0}|{1}|{2} \r\n", info.Guid, info.Name, info.Group);

                File.AppendAllText(filePath, vmData);
            }
        }

        private void InitailVMS(bool refresh)
        {
            lvVMS.Dock = DockStyle.Fill;
            lvVMS.View = View.Details;

            LoadVMSFromFile(refresh);

            this.lvVMS.Columns.Clear();  //好习惯，先清除再添加保证数据的一致性
            this.lvVMS.Columns.Add("GUID", 250, HorizontalAlignment.Left);
            this.lvVMS.Columns.Add("NAME", 60, HorizontalAlignment.Left);
            this.lvVMS.Columns.Add("GROUP", 60, HorizontalAlignment.Left);

            //var glist = from v in lstVMInfo
            //            orderby v.Group
            //            select new { Guid = v.Guid, Name = v.Name, Group = v.Group };

            //foreach (var it in glist)
            //{
            //    ListViewItem item = new ListViewItem(it.Guid);
            //    item.SubItems.Add(it.Name);
            //    item.SubItems.Add(it.Group);
            //    lvVMS.Items.Add(item);
            //}

            foreach (VMInfo info in lstVMInfo)
            {
                ListViewItem item = new ListViewItem(info.Guid);
                item.SubItems.Add(info.Name);
                item.SubItems.Add(info.Group);
                lvVMS.Items.Add(item);
            }

            groupTables = new Hashtable[lvVMS.Columns.Count];
            for (int column = 0; column < lvVMS.Columns.Count; column++)
            {
                // Create a hash table containing all the groups 
                // needed for a single column.
                groupTables[column] = CreateGroupsTable(column);
            }

           

            SetGroups(2);
        }

        private void CloseVMs(bool isALL)
        {
            if(isALL)
            {

            }
            else
            {
                //this.lvVMS.SelectedItems
                foreach(ListViewItem item in this.lvVMS.SelectedItems)
                {
                    string guid = item.Text.Trim();
                    ShutDownVM(guid);
                    System.Threading.Thread.Sleep(500);
                }
            }
        }

        // Sorts ListViewGroup objects by header value.
        private class ListViewGroupSorter : IComparer
        {
            private SortOrder order;

            // Stores the sort order.
            public ListViewGroupSorter(SortOrder theOrder)
            {
                order = theOrder;
            }

            // Compares the groups by header value, using the saved sort
            // order to return the correct value.
            public int Compare(object x, object y)
            {
                int result = String.Compare(
                    ((ListViewGroup)x).Header,
                    ((ListViewGroup)y).Header
                );
                if (order == SortOrder.Ascending)
                {
                    return result;
                }
                else
                {
                    return -result;
                }
            }
        }

        // Sets myListView to the groups created for the specified column.
        private void SetGroups(int column)
        {
            // Remove the current groups.
            lvVMS.Groups.Clear();

            // Retrieve the hash table corresponding to the column.
            Hashtable groups = (Hashtable)groupTables[column];

            // Copy the groups for the column to an array.
            ListViewGroup[] groupsArray = new ListViewGroup[groups.Count];
            groups.Values.CopyTo(groupsArray, 0);

            // Sort the groups and add them to myListView.
            Array.Sort(groupsArray, new ListViewGroupSorter(lvVMS.Sorting));
            lvVMS.Groups.AddRange(groupsArray);

            // Iterate through the items in myListView, assigning each 
            // one to the appropriate group.
            foreach (ListViewItem item in lvVMS.Items)
            {
                // Retrieve the subitem text corresponding to the column.
                string subItemText = item.SubItems[column].Text;

                // Assign the item to the matching group.
                item.Group = (ListViewGroup)groups[subItemText];
            }
         
        }

        // Creates a Hashtable object with one entry for each unique
        // subitem value (or initial letter for the parent item)
        // in the specified column.
        private Hashtable CreateGroupsTable(int column)
        {
            // Create a Hashtable object.
            Hashtable groups = new Hashtable();

            // Iterate through the items in myListView.
            foreach (ListViewItem item in lvVMS.Items)
            {
                // Retrieve the text value for the column.
                string subItemText = item.SubItems[column].Text;

                // If the groups table does not already contain a group
                // for the subItemText value, add a new group using the 
                // subItemText value for the group header and Hashtable key.
                if (!groups.Contains(subItemText))
                {
                    groups.Add(subItemText, new ListViewGroup(subItemText,
                        HorizontalAlignment.Left));
                }
            }

            // Return the Hashtable object.
            return groups;
        }

        private void btnRefreh_Click(object sender, EventArgs e)
        {
            //VMInfo info = new VMInfo();
            //info.Guid = "bb72653f-a80b-4526-a824-77607cbe06a6";
            //GetVMInfo(ref info);
            InitailData();
            InitailVMS(true);
           
        }

        private void btnCloseSelected_Click(object sender, EventArgs e)
        {
            CloseVMs(false);
        }
    }
}
