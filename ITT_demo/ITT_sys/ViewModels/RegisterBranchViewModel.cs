using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITT_sys.Models;
using System.Windows;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using ITT_sys.ViewModels.DB_Connection;


namespace ITT_sys.ViewModels
{
    public class RegisterBranchViewModel : ValidationsCommand
    {


        #region set_Connection_string
        public string ConString = @"Data Source=(local);Initial Catalog=ITT_demo;User ID=sa;Password=abc123;";
        public SqlConnection con = null;
        public SqlCommand com = null;
        public SqlDataAdapter adapter;
        public DataSet ds;

        #endregion


        public ObservableCollection<RegisterBranchModel> registerBranchModel { get; set; }


        #region load_bank_codes
        public RegisterBranchViewModel()
        {
            FillList();
        }



        private void FillList()
        {
            try
            {
                con = new SqlConnection(ConString);
                con.Open();
                com = new SqlCommand("select * from Bank_details", con);
                adapter = new SqlDataAdapter(com);
                ds = new DataSet();
                adapter.Fill(ds, "tblBankdetails");

                if (registerBranchModel == null)
                    registerBranchModel = new ObservableCollection<RegisterBranchModel>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    registerBranchModel.Add(new RegisterBranchModel
                    {

                        id = dr[0].ToString()


                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                ds = null;
                adapter.Dispose();
                con.Close();
                con.Dispose();
            }
        }
        #endregion

        #region Load_bankName

        private void FillBankName(string BankCode)
        {
            try
            {

                con = new SqlConnection(ConString);
                con.Open();
                string query = "select Bank_Name from Bank_details where Bank_Code='" + BankCode + "'";
                com = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds, "tblBankdetails");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string Bank_Name = dr["Bank_Name"].ToString();
                    _itemInEditMode.id = Bank_Name;

                }

                com.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private RegisterBranchModel _selectedValue;

        public RegisterBranchModel SelectedValue
        {
            get { return _selectedValue; }

            set
            {

                _selectedValue = value;
                OnPropertyChanged("SelectedValue");

                FillBankName(_selectedValue.id);
                OnPropertyChanged("ItemInEditMode");
            }
        }


        private RegisterBranchModel _itemInEditMode = new RegisterBranchModel();
        public RegisterBranchModel ItemInEditMode
        {
            get { return _itemInEditMode; }
        }

        #endregion
       
    }

}
