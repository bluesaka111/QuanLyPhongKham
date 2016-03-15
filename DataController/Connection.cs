using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DataController
{
    class Connection
    {
        public static SqlConnection sqlcon = new SqlConnection();
        public static SqlCommand sqlcom = new SqlCommand();
    }
}
