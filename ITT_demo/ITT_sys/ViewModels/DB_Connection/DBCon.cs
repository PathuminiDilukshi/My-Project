using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.ComponentModel;
using ITT_sys.ViewModels;
using ITT_sys.Models;
using System.Collections.ObjectModel;
namespace ITT_sys.ViewModels.DB_Connection
{
    public class DBCon
    {
     

        public string ConString;
        public SqlConnection con = null;
        public SqlCommand com = null;
        public SqlDataAdapter adapter;
        public DataSet ds;
        public DataTable dt;
        private static string connectionstring = @"Data Source=(local);Initial Catalog=ITT_demo;User ID=sa;Password=abc123;";
       

        public DBCon()
        {
                connection_Sql(); 
        }

        public void connection_Sql()
        {
            ConString = @"Data Source=(local);Initial Catalog=ITT_demo;User ID=sa;Password=abc123;";
            con = new SqlConnection(ConString);
            
        }

        public void OpenCon()
        {
            con.Open();
        }

        public void CloseCon()
        {
            con.Close();
        }

        public int insert_del_update(string query)
        {

            OpenCon();
            int line = 0;
            com = new SqlCommand(query, con);
            line = com.ExecuteNonQuery();
            com.Dispose();
            CloseCon();
            return line;

        }

        public int insert(string query)
        {
            using (SqlConnection myCon = new SqlConnection(connectionstring))
            {

                myCon.Open();
                int line = 0;
                com = new SqlCommand(query, myCon);
                line = com.ExecuteNonQuery();
                com.Dispose();
                myCon.Close();
                return line;

            }
        }

        public int RecordExists(string query)
        {
            using (SqlConnection myCon = new SqlConnection(connectionstring))
            {
                myCon.Open();
                int recordCount = 0;
                com = new SqlCommand(query, myCon);
                recordCount = Convert.ToInt32(com.ExecuteScalar());
                com.Dispose();
                myCon.Close();
                return recordCount;
            }
        }

        public int getSupID(string query)
        {
            using (SqlConnection myCon = new SqlConnection(connectionstring))
            {

                myCon.Open();
                int supID = 0 ;
                com = new SqlCommand(query, myCon);
                SqlDataReader reader = com.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        supID = reader.GetInt32(0);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                com.Dispose();
                myCon.Close();
                reader.Close();
                return supID;
            }
        }
    }
}
