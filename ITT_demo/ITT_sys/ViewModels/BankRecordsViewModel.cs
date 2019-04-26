using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using ITT_sys.ViewModels.DB_Connection;
using ITT_sys.Models;
using System.Windows;
using System.ComponentModel;
using ITT_sys.ViewModels;

namespace ITT_sys.ViewModels
{
    public class BankRecordsViewModel : Screen
    {
        #region  properies
        private string _bankCode;

        public string BankCode
        {
            get { return _bankCode; }
            set {
                    _bankCode = value;
                    NotifyOfPropertyChange(() => BankCode);
                }      
         }

        private string _bankName;

        public string BankName
        {
            get { return _bankName; }
            set
            {
                _bankName = value;
                NotifyOfPropertyChange(() => BankName);
            }
        }


        private string _adressLine1;

        public string AddressLine1
        {
            get { return _adressLine1; }
            set
            {
                _adressLine1 = value;
                NotifyOfPropertyChange(() => AddressLine1);
            }
        }

        private string _adressLine2;

        public string AddressLine2
        {
            get { return _adressLine2; }
            set
            {
                _adressLine2 = value;
                NotifyOfPropertyChange(() => AddressLine2);
            }
        }

        private string _adressLine3;

        public string AddressLine3
        {
            get { return _adressLine3; }
            set
            {
                _adressLine3 = value;
                NotifyOfPropertyChange(() => AddressLine3);

            }
        }

        private string _contactNo;

        public string ContactNo
        {
            get { return _contactNo; }
            set
            {
                _contactNo = value;
                NotifyOfPropertyChange(() => ContactNo);
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }

        DBCon DB_Con = new DBCon();
       
        private ObservableCollection<RegisterBankModel> _mysampleGrid = new ObservableCollection<RegisterBankModel>();

        public ObservableCollection<RegisterBankModel> MysampleGrid
        {
            get { return _mysampleGrid; }
            set { _mysampleGrid = value; }
        }
        #endregion 


   
        public BankRecordsViewModel()
        {
            DataGrid();
        }

      

        private void DataGrid()
        {
              try
              {
                string Query = "select TOP 1 * from Bank_details order by updatedAt desc";
                DB_Con.connection_Sql();
                DB_Con.con.Open();
                DB_Con.com = new SqlCommand(Query, DB_Con.con);
                DB_Con.adapter = new SqlDataAdapter(DB_Con.com);
                DB_Con.ds = new DataSet();
                DB_Con.adapter.Fill(DB_Con.ds, "tblBankdetails");

                if (MysampleGrid == null)
                    MysampleGrid = new ObservableCollection<RegisterBankModel>();

                foreach (DataRow dr in DB_Con.ds.Tables[0].Rows)
                {
                    MysampleGrid.Add(new RegisterBankModel
                    {
                     BankCode=dr[0].ToString(),
                     BankName = dr["BankName"].ToString(),
                     AddressLine1 = dr["Address_line1"].ToString(),
                     AddressLine2 = dr["Address_line2"].ToString(),
                     AddressLine3 = dr["Address_line3"].ToString(),
                     Email = dr["Email"].ToString(),
                     ContactNo = dr["Tel_no"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                DB_Con.ds = null;
                DB_Con.adapter.Dispose();
                DB_Con.con.Close();
                DB_Con.con.Dispose();
            }
        }

    }
}
