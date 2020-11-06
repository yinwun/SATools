using System;
using System.Collections.Generic;
using System.Text;

namespace ASSAMON
{
    public class ActiveInfo
    {
        int _proID = -1;
        double _memory = 0;
        String _name = String.Empty;
        String _account = String.Empty;
        int _shengang = -1;
        String _xy = String.Empty;
        String _map = String.Empty;
        IntPtr _hWnd = IntPtr.Zero;
        String _stone = String.Empty;
        String _hp = String.Empty;

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            return Equals(obj as ActiveInfo);
        }

        private bool Equals(ActiveInfo info)
        {
            return (this._proID == info.ProID);
        }
        public override int GetHashCode()
        {
            return this.ProID;
        }

        public int ProID
        {
            get { return _proID; }
            set { _proID = value; }
        }

        public double Memory
        {
            get { return _memory; }
            set { _memory = value; }
        }

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public String Account
        {
            get { return _account; }
            set { _account = value; }
        }

        public int ShengWang
        {
            get { return _shengang; }
            set { _shengang = value; }
        }

        public String XY
        {
            get { return _xy; }
            set { _xy = value; }
        }

        public String MAP
        {
            get { return _map; }
            set { _map = value; }
        }

        public IntPtr Hwnd
        {
            get { return _hWnd; }
            set { _hWnd = value; }
        }

        public String Stone
        {
            get { return _stone; }
            set { _stone = value; }
        }

        public String HP
        {
            get { return _hp; }
            set { _hp = value; }
        }
    }
}
