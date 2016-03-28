using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTObject
{
    public class UserInfo
    {
        private string employeeIDcode;
        private string username;
        private string password;
        private string strname;
        private string employeeRank;
        public UserInfo()
        {
            employeeIDcode = null;
            username = String.Empty;
            password = String.Empty;
            employeeIDcode = String.Empty;
            strname = String.Empty;
            employeeRank = String.Empty;
        }

        public string userid
        {
            get { return username; }
            set { username = value; }
        }

        public string passcode
        {
            get { return password; }
            set { password = value; }
        }

        public string employeeID
        {
            get { return employeeIDcode; }
            set { employeeIDcode = value; }
        }

        public string StrName
        {
            get { return this.strname; }
            set { this.strname = value; }
        }

        public string employeeRnk
        {
            get { return employeeRank; }
            set { employeeRank = value; }
        }
    }
}
