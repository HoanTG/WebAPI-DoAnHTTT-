using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using WebAPI_DoAnHTTT_.Models;

namespace WebAPI_DoAnHTTT_.Controllers
{
    public class Database
    {

        public static DataTable Read_Table(string StoredProcedureName, Dictionary<string, object> dic_param = null)
        {
            string SQLconnectionString = ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString;
            DataTable result = new DataTable("table1");
            using (SqlConnection conn = new SqlConnection(SQLconnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = StoredProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                if (dic_param != null)
                {
                    foreach (KeyValuePair<string, object> data in dic_param)
                    {
                        if (data.Value == null)
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, data.Value);
                        }
                    }
                }
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(result);

                }
                catch (Exception ex)
                {

                }
            }
            return result;
        }

        public static object Exec_Command(string StoredProcedureName, Dictionary<string, object> dic_param = null)
        {
            string SQLconnectionString = ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString;
            object result = null;
            using (SqlConnection conn = new SqlConnection(SQLconnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();

                // Start a local transaction.

                cmd.Connection = conn;
                cmd.CommandText = StoredProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;

                if (dic_param != null)
                {
                    foreach (KeyValuePair<string, object> data in dic_param)
                    {
                        if (data.Value == null)
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, data.Value);
                        }
                    }
                }
                cmd.Parameters.Add("@CurrentID", SqlDbType.Int).Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                    result = cmd.Parameters["@CurrentID"].Value;
                    // Attempt to commit the transaction.

                }
                catch (Exception ex)
                {

                    result = null;
                }

            }
            return result;
        }
        public static DataTable LayDsQuanAn()
        {   
            return Read_Table("ChiTietQuanAn");
        }

        public static DataTable ThemQuanYeuThich(string MSQA)
        {
            Dictionary<string,object> param = new Dictionary<string,object>();
            param.Add("MSQA", MSQA);
            return Read_Table("ThemQuanYeuThich",param);
        }

        public static DataTable DSQuanYT()
        {
            return Read_Table("DSQuanYeuThich");
        }
        public static DataTable XoaQuanYeuThich(string MSQA)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MSQA", MSQA);
            return Read_Table("XoaQuanAn", param);
        }
        public static int ThemTaiKhoan(AccountUser acc)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("Username", acc.Username);
            param.Add("PasswordUser", acc.PasswordUser);
            int kq = int.Parse(Exec_Command("ThemTaiKhoan", param).ToString());
            return kq;
        }
    }
}