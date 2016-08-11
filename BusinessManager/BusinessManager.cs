using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
    public class BusinessManager:DAO
    {
        public DataSet getStaffInfo(int? staffid = null)
        {
            DataSet result = new DataSet();
            ConnectMssql();
            SqlCommand cmd = getSqlCommandForSP();
            cmd.CommandText = "getStaffDetails";
            AddIntParameter(cmd, "StaffID", staffid);
            result = ExecuteReader();
            return result;
        }

        public int ValidateUser(string Username, string Password)
        {            
            ConnectMssql();
            SqlParameter IsAuthenticated = new SqlParameter();
            SqlParameter StaffID = new SqlParameter();
            SqlCommand cmd = getSqlCommandForSP();
            cmd.CommandText = "validateStaffLogin";
            AddStringParameter(cmd, "Username", Username);
            AddStringParameter(cmd, "Password", Password);
            IsAuthenticated = AddIntOutputParameter(cmd, "IsAuthenticated");
            StaffID = AddIntOutputParameter(cmd, "StaffID");
            cmd.ExecuteNonQuery();
            return Convert.ToInt32(StaffID.Value);
        }

        public DataSet AddStaffInfo(string StaffName,string Gender,string DOB, string Phone, string Email, string Languages, string Country, string Username, string Password)
        {
            DataSet result = new DataSet();
            ConnectMssql();
            SqlCommand cmd = getSqlCommandForSP();
            cmd.CommandText = "AddStaffDetails";
            AddStringParameter(cmd, "Name", StaffName);
            AddStringParameter(cmd, "Gender", Gender);
            AddStringParameter(cmd, "DOB", DOB);
            AddStringParameter(cmd, "Phone", Phone);
            AddStringParameter(cmd, "Email", Email);
            AddStringParameter(cmd, "Languages", Languages);
            AddStringParameter(cmd, "Country", Country);
            AddStringParameter(cmd, "Username", Username);
            AddStringParameter(cmd, "Password", Password);
            result = ExecuteReader();
            return result;
        }
    }
}