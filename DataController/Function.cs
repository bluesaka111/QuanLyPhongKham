using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Windows.Forms;
using System.ComponentModel;
using DTObject;

namespace DataController
{
    public class Function
    {
        public static int maxRecords(SqlConnection sqlcon, string table, Errors err)
        {
            int maxRes = 0;
            if(sqlcon.State != ConnectionState.Closed)
            {
                sqlcon.Close();
            }
            try
            {
                sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = sqlcon;
                sqlcom.CommandText = "SELECT COUNT(*) FROM " + table;
                maxRes = (int)sqlcom.ExecuteScalar();
            }
            catch(Exception ex)
            {
                err.errors = ex.Message;
            }
            return maxRes;
        }
        public static Errors LoadPatientTableStruct(UserInfo userdata, DataTable dt, int start, int amount, Errors err)
        {
            dt.Reset();
            if (Connection.sqlcon.State != ConnectionState.Closed)
            {
                Connection.sqlcon.Close();
            }
            Connection.sqlcon.ConnectionString = Login.SetConnection();
            try
            {
                Connection.sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = Connection.sqlcon;
                sqlcom.CommandText = "usp_LoadPatient";
                sqlcom.CommandType = CommandType.StoredProcedure;
                if (!sqlcom.Parameters.Contains("@employeeID")) { sqlcom.Parameters.Add("@employeeID", SqlDbType.NChar); }
                sqlcom.Parameters["@employeeID"].Direction = ParameterDirection.Input;
                sqlcom.Parameters["@employeeID"].Value = userdata.employeeID;
                sqlcom.Parameters["@employeeID"].Size = 10;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlcom;
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                if (start != -1)
                {
                    adapter.Fill(start, amount, dt);
                }
                else
                {
                    adapter.Fill(dt);
                }
                Connection.sqlcon.Close();
            }
            catch(Exception ex)
            {
                err.errors = ex.Message;
                Connection.sqlcon.Close();
            }
            return err;
        }


        public static Errors LoadEmployeeTableStruct(UserInfo userdata, DataTable dt, int start, int amount, Errors err)
        {
            dt.Reset();
            if (Connection.sqlcon.State != ConnectionState.Closed)
            {
                Connection.sqlcon.Close();
            }
            Connection.sqlcon.ConnectionString = Login.SetConnection();
            try
            {
                Connection.sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = Connection.sqlcon;
                sqlcom.CommandText = "usp_LoadEmployee";
                sqlcom.CommandType = CommandType.StoredProcedure;
                if (!sqlcom.Parameters.Contains("@employeeID")) { sqlcom.Parameters.Add("@employeeID", SqlDbType.NChar); }
                sqlcom.Parameters["@employeeID"].Direction = ParameterDirection.Input;
                sqlcom.Parameters["@employeeID"].Value = userdata.employeeID;
                sqlcom.Parameters["@employeeID"].Size = 10;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlcom;
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                if (start != -1)
                {
                    adapter.Fill(start, amount, dt);
                }
                else
                {
                    adapter.Fill(dt);
                }
                Connection.sqlcon.Close();
            }
            catch (Exception ex)
            {
                err.errors = ex.Message;
                Connection.sqlcon.Close();
            }
            return err;
        }


    }
}
