using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Runtime.InteropServices;

namespace ASSAMON
{
    public class Constant
    {
        public String accountINI = "Accounts.ini";
        public String assaEXE = "ASSAEXE";
        public String assaPath = "ASSAPATH";
        public int waitTime = 500;//mls
        public String saClassName = "SAClassName";
        public String saStartButtonClassName = "SAStartButtonClassName";
        public const int WM_CLOSE = 0x0010;
        public String ASSAWITH = "ASSAWITH";
        public String ScriptXYRoot = "ScriptXYRoot";
        public String ScriptXYNode = "ScriptXYNode";
        public String ScriptXY = "ScriptXY";
        public String TimerInterval = "TimerInterval";
        

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }
    }

    public enum StartUPType
    {
        SHENGWANG,
        FIVE,
        ZHENGLI,
        LIANCHONG
    }
}
