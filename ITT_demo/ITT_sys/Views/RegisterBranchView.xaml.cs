using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ITT_sys.Views
{
    /// <summary>
    /// Interaction logic for RegisterBranchView.xaml
    /// </summary>
    public partial class RegisterBranchView : UserControl
    {
       
        public SqlConnection con = null;
        public SqlCommand com = null;

        public RegisterBranchView()
        {
            InitializeComponent();
            fill_combo();
        }



       string  ConString = @"Data Source=(local);Initial Catalog=ITT_demo;User ID=sa;Password=abc123;";

        void fill_combo()
        {
            
            con = new SqlConnection(ConString);

            try
            {
                con.Open();
                string query = "select * from Bank_details";
                com = new SqlCommand(query, con);
                com.CommandType = CommandType.Text;
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string BankCode = dr.GetString(0);
                        ComboBox.Items.Add(BankCode);
                    }
                }

                com.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
