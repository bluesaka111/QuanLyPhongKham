using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Windows.Forms;
using DTObject;

namespace DataController
{
    public class Errors
    {
        public string errors { get; set; }
        public Errors()
        {
            errors = String.Empty;
        }
    }

    public class CryptoExtras
    {
        public static string GetMD5Hash(MD5 md5hash, string input)
        {
            byte[] md5HashData = md5hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sbuilder = new StringBuilder();
            for(int i = 0; i < md5HashData.Length; i++)
            {
                sbuilder.Append(md5HashData[i].ToString("x2"));
            }
            return sbuilder.ToString();
        }
    }
    public class Login
    {
        protected internal static string SetConnection()
        {
            SqlConnectionStringBuilder sqlstring = new SqlConnectionStringBuilder();
            sqlstring.DataSource = "(local)";
            sqlstring.InitialCatalog = "QuanLyPhongKham";
            sqlstring.IntegratedSecurity = false;
            sqlstring.UserID = "sa";
            sqlstring.Password = "Windows100";
            return sqlstring.ConnectionString;
        }

        public static void userLogin(UserInfo userdata, DataTable dt, Errors err)
        {
            MD5 cryptoMD5 = new MD5CryptoServiceProvider();
            cryptoMD5 = MD5CryptoServiceProvider.Create();
            string passhash = CryptoExtras.GetMD5Hash(cryptoMD5, userdata.passcode);
            if(Connection.sqlcon.State != ConnectionState.Closed)
            {
                Connection.sqlcon.Close();
            }
            Connection.sqlcon.ConnectionString = SetConnection();
            try
            {
                Connection.sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = Connection.sqlcon;
                sqlcom.CommandText = "QuanLyPhongKham.dbo.usp_UserLogin";
                sqlcom.CommandType = CommandType.StoredProcedure;
                if (!sqlcom.Parameters.Contains("@username")) { sqlcom.Parameters.Add("@username", SqlDbType.NVarChar); }
                if (!sqlcom.Parameters.Contains("@passhash")) { sqlcom.Parameters.Add("@passhash", SqlDbType.NVarChar); }
                sqlcom.Parameters["@username"].Value = userdata.userid;
                sqlcom.Parameters["@username"].Size = 50;
                sqlcom.Parameters["@passhash"].Value = passhash;
                sqlcom.Parameters["@passhash"].Size = 255;
                SqlDataAdapter sqlda = new SqlDataAdapter();
                sqlda.SelectCommand = sqlcom;
                sqlda.Fill(dt);
                Connection.sqlcon.Close();
            }
            catch(Exception ex)
            {
                err.errors = ex.Message;
                Connection.sqlcon.Close();
            }
        }
    }
}
