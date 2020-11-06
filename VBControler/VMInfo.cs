using System;
using System.Collections.Generic;
using System.Text;

namespace XAPP.VMInfo
{
    public class VMInfo
    {
        private string _name;
        private string _guid;
        private string _group;
        private string _status;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Guid
        {
            get
            {
                return _guid;
            }
            set
            {
                _guid = value;
            }
        }

        public string Group
        {
            get
            {
                return _group;
            }
            set
            {
                _group = value;
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }
    }
}
