using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BusinessLayer
{
    public class DAO
    {
        string ConnectionString;
        SqlConnection Conn;
        SqlCommand comm;
        public void ConnectMssql()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["PrimaryConnectionMSSQL"].ConnectionString;
            Conn = new SqlConnection(ConnectionString);
            Conn.Open();
        }

        public SqlCommand getSqlCommandForSP()
        {
            comm = new SqlCommand();
            comm.Connection = Conn;
            comm.CommandType = CommandType.StoredProcedure;
            return comm;
        }

        public SqlCommand getSqlCommandForDirect()
        {
            comm = new SqlCommand();
            comm.Connection = Conn;
            comm.CommandType = CommandType.TableDirect;
            return comm;
        }

        public void AddIntParameter(SqlCommand com, string param, int? value)
        {
            com.Parameters.Add("@" + param, SqlDbType.Int).Value = value;
        }

        public void AddStringParameter(SqlCommand com, string param, string value)
        {
            com.Parameters.Add("@" + param, SqlDbType.NVarChar).Value = value;
        }

        public SqlParameter AddIntOutputParameter(SqlCommand com, string param)
        {
            SqlParameter outPutParameter = new SqlParameter();
            outPutParameter.ParameterName = "@" + param;
            outPutParameter.SqlDbType = SqlDbType.Int;
            outPutParameter.Direction = ParameterDirection.Output;
            com.Parameters.Add(outPutParameter);
            return outPutParameter;
        }

        public DataSet ExecuteReader()
        {
            DataSet ds = new DataSet();
            var adapter = new SqlDataAdapter(comm);
            adapter.Fill(ds);
            return ds;
        }

    }
}