using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataController
{
    public class StoredData
    {
        private string employeeIDcode;
        private string username;
        private string password;
        public StoredData()
        {
            employeeIDcode = null;
            username = String.Empty;
            password = String.Empty;
            employeeIDcode = String.Empty;
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
    }
}
