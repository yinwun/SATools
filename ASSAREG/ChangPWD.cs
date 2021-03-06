﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ASSAREG
{
    public partial class ChangPWD : Form
    {
        private List<String> _lstUserNamePwd = new List<string>();
        private bool isLoad = false;
        private int index = 0;

        public List<String> ListUserNamePwd
        {
            get
            {
                return _lstUserNamePwd;
            }
            set
            {
                _lstUserNamePwd = value;
            }

        }

        public ChangPWD()
        {
            SetWebBrowserFeatures(11);
            InitializeComponent();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            isLoad = true;
            SetValue();
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            LoadFiles();
        }

        private void SetValue()
        {
            if (isLoad)
            {
                String[] str = ListUserNamePwd[index].Split('|');
                Random rd = new Random();
                int i = rd.Next(1, 99999999);
                //1. username 2. pwd 3. safecode 4. qq
                webBrowser1.Document.GetElementById("id").SetAttribute("value", str[0]);
                webBrowser1.Document.GetElementById("oldpass").SetAttribute("value", str[1]);
                webBrowser1.Document.GetElementById("oldpass2").SetAttribute("value", str[2]);
                webBrowser1.Document.GetElementById("pass").SetAttribute("value", str[3]);
                webBrowser1.Document.GetElementById("pass2").SetAttribute("value", str[3]);
                webBrowser1.Document.GetElementById("pass3").SetAttribute("value", str[4]);
                webBrowser1.Document.GetElementById("pass4").SetAttribute("value", str[4]);
                index++;
            }

        }

        /// <summary>  
        /// 修改注册表信息来兼容当前程序  
        ///   
        /// </summary>  
        static void SetWebBrowserFeatures(int ieVersion)
        {
            // don't change the registry if running in-proc inside Visual Studio  
            if (LicenseManager.UsageMode != LicenseUsageMode.Runtime)
                return;
            //获取程序及名称  
            var appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            //得到浏览器的模式的值  
            UInt32 ieMode = GeoEmulationModee(ieVersion);
            var featureControlRegKey = @"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\";
            //设置浏览器对应用程序（appName）以什么模式（ieMode）运行  
            Microsoft.Win32.Registry.SetValue(featureControlRegKey + "FEATURE_BROWSER_EMULATION",
                appName, ieMode, Microsoft.Win32.RegistryValueKind.DWord);
            // enable the features which are "On" for the full Internet Explorer browser  
            //不晓得设置有什么用  
            Microsoft.Win32.Registry.SetValue(featureControlRegKey + "FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION",
                appName, 1, Microsoft.Win32.RegistryValueKind.DWord);


            //Registry.SetValue(featureControlRegKey + "FEATURE_AJAX_CONNECTIONEVENTS",  
            //    appName, 1, RegistryValueKind.DWord);  


            //Registry.SetValue(featureControlRegKey + "FEATURE_GPU_RENDERING",  
            //    appName, 1, RegistryValueKind.DWord);  


            //Registry.SetValue(featureControlRegKey + "FEATURE_WEBOC_DOCUMENT_ZOOM",  
            //    appName, 1, RegistryValueKind.DWord);  


            //Registry.SetValue(featureControlRegKey + "FEATURE_NINPUT_LEGACYMODE",  
            //    appName, 0, RegistryValueKind.DWord);  
        }

        /// <summary>  
        /// 获取浏览器的版本  
        /// </summary>  
        /// <returns></returns>  
        static int GetBrowserVersion()
        {
            int browserVersion = 0;
            using (var ieKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                System.Security.AccessControl.RegistryRights.QueryValues))
            {
                var version = ieKey.GetValue("svcVersion");
                if (null == version)
                {
                    version = ieKey.GetValue("Version");
                    if (null == version)
                        throw new ApplicationException("Microsoft Internet Explorer is required!");
                }
                int.TryParse(version.ToString().Split('.')[0], out browserVersion);
            }
            //如果小于7  
            if (browserVersion < 7)
            {
                throw new ApplicationException("不支持的浏览器版本!");
            }
            return browserVersion;
        }

        static UInt32 GeoEmulationModee(int browserVersion)
        {
            UInt32 mode = 11000; // Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode.   
            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode.   
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode.   
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.                      
                    break;
                case 10:
                    mode = 10000; // Internet Explorer 10.  
                    break;
                case 11:
                    mode = 11000; // Internet Explorer 11  
                    break;
            }
            return mode;
        }


        void LoadFiles()
        {
            String pth = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "actinfo.txt");
            String[] lines = File.ReadAllLines(pth);
            ListUserNamePwd.Clear();
            foreach (string s in lines)
            {
                ListUserNamePwd.Add(s);
            }
        }

        private void ChangPWD_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(@"http://shiqi.so/changepass.htm");
        }
    }
}
