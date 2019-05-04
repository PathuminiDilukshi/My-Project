using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using ITT_sys.ViewModels.Commands;
using ITT_sys.ViewModels.DB_Connection;
using ITT_sys.Models;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;


namespace ITT_sys.ViewModels
{
	public class RegisterBranchViewModel : Conductor<object>,INotifyPropertyChanged
	{
		#region properties_intializing

		private string _branchCode;
		private string _branchName;
		private string _addressLine1;
		private string _addressLine2;
		private string _addressLine3;
		private string _contactNo;
		private string _email;
		DBCon DB_Con = new DBCon();

		public string BranchCode
		{
			get { return _branchCode; }
			set
			{
				_branchCode = value;
				NotifyOfPropertyChange(() => BranchCode);
			}
		}

		
		private string _branchCode_update;

		public string BranchCode_update
	{
		get { return _branchCode_update;}

		set 
		{
			_branchCode_update = value;
			NotifyOfPropertyChange(() => BranchCode_update);
		
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


		public RegisterBranchViewModel()
		{
			FillList();
			FillBankCodeList2();
		}

		private RegisterBranchModel _itemInEditMode = new RegisterBranchModel();

		public RegisterBranchModel ItemInEditMode
		{
			get { return _itemInEditMode; }
		}


		private RegisterBranchModel _selectedValue;

		public RegisterBranchModel SelectedValue
		{
			get { return _selectedValue; }

			set
			{

				_selectedValue = value;
				NotifyOfPropertyChange(() => SelectedValue);

				FillBankName(_selectedValue.BankCode);
				NotifyOfPropertyChange(() => ItemInEditMode);
			}
		}

		public ObservableCollection<RegisterBranchModel> addBranchModel { get; set; }
		#endregion

		#region btton_click

		public ICommand SaveCommand { get; private set; }

		public bool CanSave
		{
			get { return true; }
		}
		#endregion

		# region regionstation_branch

		public void save()
		{
            SaveCommand = new DelegateCommand(save, () => CanSave);
      
            string Query = "select COUNT(*) from Branch_details where  BankCode ='" + this._selectedValue.BankCode.Trim() + "' and Branch_Code='" + this.BranchCode + "'";

			int lines = DB_Con.RecordExists(Query);

            if (lines > 0)
            {
                MessageBox.Show("This BankCode and BranchCode exist in DataBase ");
            }
            else
            {
                try
                {
                    string query = "INSERT INTO Branch_details (BankCode,Branch_Code,Branch_Name,Address_line1,Address_line2,Address_line3,Email,Tel_no)VALUES('"
                        + _selectedValue.BankCode.Trim() + "','"
                        + this.BranchCode + "','"
                        + this.BranchName + "','"
                        + this.AddressLine1 + "','"
                        + this.AddressLine2 + "','"
                        + this.AddressLine3 + "','"
                        + this.Email + "','"
                        + this.ContactNo + "');";
                    int noline = DB_Con.insert(query);
                    if (noline > 0)
                    {
                        MessageBox.Show("Data inserted Successfully");
                    }
                    else
                    {
                        MessageBox.Show("Try again!");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
		}

		public void Clearbtn()
		{
			Clear();
		}

		public void Clear()
		{
			this.BranchCode = "";
			this.BranchName = "";
			this.AddressLine1 = "";
			this.AddressLine2 = "";
			this.AddressLine3 = "";
			this.Email = "";
			this.ContactNo = "";
		}

		private void FillList()
		{
			try
			{
				DB_Con.connection_Sql();
				DB_Con.con.Open();
                DB_Con.com = new SqlCommand("select BankCode from Bank_details", DB_Con.con);
				DB_Con.adapter = new SqlDataAdapter(DB_Con.com);
				DB_Con.ds = new DataSet();
				DB_Con.adapter.Fill(DB_Con.ds, "tblBankdetails");

				if (addBranchModel == null)
					addBranchModel = new ObservableCollection<RegisterBranchModel>();

				foreach (DataRow dr in DB_Con.ds.Tables[0].Rows)
				{
					addBranchModel.Add(new RegisterBranchModel
					{
						BankCode = dr[0].ToString()
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



		private void FillBankName(string BankCode)
		{
			try
			{
				DB_Con.con = new SqlConnection(DB_Con.ConString);
				DB_Con.con.Open();
                string query = "select BankName from Bank_details where BankCode='" + BankCode + "'";
				DB_Con.com = new SqlCommand(query, DB_Con.con);
				SqlDataAdapter da = new SqlDataAdapter(DB_Con.com);
				DataSet ds = new DataSet();
				da.Fill(ds, "tblBankdetails");

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
                    string Bank_Name = dr["BankName"].ToString();
					_itemInEditMode.BankName = Bank_Name;
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

		#endregion

		#region UpdateBranch
		private RegisterBranchModel _itemInEditMode2 = new RegisterBranchModel();

		public RegisterBranchModel ItemInEditMode2
		{
			get { return _itemInEditMode2; }
		}



		private RegisterBranchModel _selectedValue2;

		public RegisterBranchModel SelectedValue2
		{
			get { return _selectedValue2; }

			set
			{
				_selectedValue2 = value;
				NotifyOfPropertyChange(() => SelectedValue2);

				FillbankNameupdation(_selectedValue2.BankCode);
				NotifyOfPropertyChange(() => ItemInEditMode2);    
			}
		}


		public ObservableCollection<RegisterBranchModel> updateBranchModel { get; set; }

		private void FillBankCodeList2()
		{
			try
			{
				DB_Con.connection_Sql();
				DB_Con.con.Open();
				DB_Con.com = new SqlCommand("select BankCode from Bank_details", DB_Con.con);
				DB_Con.adapter = new SqlDataAdapter(DB_Con.com);
				DB_Con.ds = new DataSet();
				DB_Con.adapter.Fill(DB_Con.ds, "tb2Bankdetails");

				if (updateBranchModel == null)
					updateBranchModel = new ObservableCollection<RegisterBranchModel>();


				foreach (DataRow dr in DB_Con.ds.Tables[0].Rows)
				{
					updateBranchModel.Add(new RegisterBranchModel
					{
						BankCode = dr["BankCode"].ToString()
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

		private void FillbankNameupdation(string BankCode)
		{
			try
			{
				string query = "select BankName from Bank_details where BankCode ='" + BankCode + "'";
				DB_Con.connection_Sql();
				DB_Con.con.Open();
				DB_Con.com = new SqlCommand(query, DB_Con.con);
				SqlDataAdapter da = new SqlDataAdapter(DB_Con.com);
				DataSet ds = new DataSet();
				da.Fill(ds, "tb2Bankdetails");

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					string Bank_Name = dr["BankName"].ToString();
					_itemInEditMode2.BankName = Bank_Name;
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

		public void update()
		{
            SaveCommand = new DelegateCommand(update, () => CanSave);

            string Query = "select COUNT(*) from Branch_details where  BankCode ='" + this._selectedValue2.BankCode.Trim() + "' and BranchCode='" + this._itemInEditMode2.BranchCode.Trim() + "'";

			int lines = DB_Con.RecordExists(Query);

            if (lines == 1)
            {
                try
                {

                    string query = " UPDATE Branch_details SET BankCode = '" + _selectedValue2.BankCode.Trim() + "',BranchCode ='" + this._itemInEditMode2.BranchCode.Trim() + "',BranchName = '" + this._itemInEditMode2.BranchName.Trim() + "',Address_line1 = '" + this._itemInEditMode2.AddressLine1.Trim() + "',Address_line2 = '" + this._itemInEditMode2.AddressLine1.Trim() + "',Address_line3 = '" + this._itemInEditMode2.AddressLine1.Trim() + "',Email ='" + this._itemInEditMode2.Email.Trim() + "' ,Tel_no ='" + this._itemInEditMode2.ContactNo.Trim() + "' WHERE BankCode = '" + _selectedValue2.BankCode.Trim() + "' AND BranchCode ='" + this._itemInEditMode2.BranchCode.Trim() + "' ";

                    int noline = DB_Con.insert(query);

                    if (noline > 0)
                    {
                        MessageBox.Show("Data Updated Successfully");
                        ActivateItem(new BranchRecordsViewModel());
                    }
                    else
                    {
                        MessageBox.Show("Try again!");
                    }

                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Does not exist a record where Bank Code is '" + this._selectedValue2.BankCode.Trim() + "' and BranchCode is '" + this._itemInEditMode2.BranchCode.Trim() + "'" );
            }

		}

		#endregion

	}

}
