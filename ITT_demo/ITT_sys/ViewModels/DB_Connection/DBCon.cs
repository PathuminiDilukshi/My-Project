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
                com = new SqlCommand(query, con);
                line = com.ExecuteNonQuery();
                
                return line;

            }
        } 

           
    }
}
