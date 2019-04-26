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
    public class BranchRecordsViewModel :Screen
    {
        #region properties_intializing

        private string _bankCode;
		private string _branchCode;
		private string _branchName;
		private string _addressLine1;
		private string _addressLine2;
		private string _addressLine3;
		private string _contactNo;
		private string _email;
		DBCon DB_Con = new DBCon();


        public string BankCode
        {
            get { return _bankCode; }
            set
            {
                _bankCode = value;
                NotifyOfPropertyChange(() => BankCode);
            }
        }
		public string BranchCode
		{
			get { return _branchCode; }
			set
			{
				_branchCode = value;
				NotifyOfPropertyChange(() => BranchCode);
			}
		}


		public string BranchName
		{
			get { return _branchName; }
			set
			{
				_branchName = value;
				NotifyOfPropertyChange(() => BranchName);
			}
		}
		public string AddressLine1
		{
			get { return _addressLine1; }
			set
			{
				_addressLine1 = value;
				NotifyOfPropertyChange(() => AddressLine1);
			}
		}
		public string AddressLine2
		{
			get { return _addressLine2; }
			set
			{

				_addressLine2 = value;
				NotifyOfPropertyChange(() => AddressLine2);
			}
		}
		public string AddressLine3
		{
			get { return _addressLine3; }
			set
			{
				_addressLine3 = value;
				NotifyOfPropertyChange(() => AddressLine3);
			}
		}
		public string ContactNo
		{
			get { return _contactNo; }
			set
			{
				_contactNo = value;
				NotifyOfPropertyChange(() => ContactNo);
			}
		}
		public string Email
		{
			get { return _email; }
			set
			{
				_email = value;
				NotifyOfPropertyChange(() => Email);
			}
		}

        private ObservableCollection<RegisterBranchModel> _branchDetailsGrid = new ObservableCollection<RegisterBranchModel>();

        public ObservableCollection<RegisterBranchModel> BranchDetailsGrid
        {
            get { return _branchDetailsGrid; }
            set { _branchDetailsGrid = value; }
        }
		#endregion

        public BranchRecordsViewModel()
        {
            DataGrid();
        }

      

        private void DataGrid()
        {
              try
              {
                string Query = "select TOP 1 * from Branch_details order by updatedAt desc";
                DB_Con.connection_Sql();
                DB_Con.con.Open();
                DB_Con.com = new SqlCommand(Query, DB_Con.con);
                DB_Con.adapter = new SqlDataAdapter(DB_Con.com);
                DB_Con.ds = new DataSet();
                DB_Con.adapter.Fill(DB_Con.ds, "tblBranchdetails");

                if (BranchDetailsGrid == null)
                    BranchDetailsGrid = new ObservableCollection<RegisterBranchModel>();

                foreach (DataRow dr in DB_Con.ds.Tables[0].Rows)
                {
                    BranchDetailsGrid.Add(new RegisterBranchModel
                    {

                        BankCode = dr["BankCode"].ToString(),
                        BranchCode=dr["BranchCode"].ToString(),
                        BranchName = dr["BranchName"].ToString(),
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
