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
	public class RegisterBankViewModel : Conductor<object>,IDataErrorInfo,INotifyPropertyChanged
	{

	  

		#region properties_intializing

		private string _bankCode;
		private string _bankName;
		private string _addressLine1;
		private string _addressLine2;
		private string _addressLine3;
		private string _contactNo;
		private string _email;
		private RegisterBankModel testModel; 
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
		public string BankName
		{
			get { return _bankName; }
			set
			{
				_bankName = value;
				NotifyOfPropertyChange(() => BankName);
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
		
		public RegisterBankModel RegisterBankModel
		{
			get { return testModel; }
			set 
			{
				testModel = value;
				NotifyOfPropertyChange(() => RegisterBankModel);
			}
		}

		public RegisterBankViewModel()
		{
			FillList();
			FillList_deletePage();
		}
		#endregion 

		#region btton_click

		public ICommand SaveCommand { get; private set; }

		public bool CanSave
		{
			get
			{
				return true;
				
			}

			
		}
		#endregion

		# region regionstation page_button_click
		public void Save()
		{
			SaveCommand = new DelegateCommand(Save, () => CanSave);
			string Query = "select COUNT(*) from Bank_details where Bank_Code='" + this.BankCode + "'";

			int lines = DB_Con.RecordExists(Query);

			if (lines > 0)
			{
				MessageBox.Show("Bank Code is in the dataBase ");
			}
			else
			{
				try
				{
				 

					if (!string.IsNullOrEmpty(BankCode) && !string.IsNullOrEmpty(BankCode))
					{
						string query = "INSERT INTO Bank_details(Bank_Code,Bank_Name,Address_line1,Address_line2,Address_line3,Email,Tel_no) values('" + this.BankCode + "','" + this.BankName + "','" + this.AddressLine1 + "','" + this.AddressLine2 + "','" + this.AddressLine3 + "','" + this.Email + "','" + this.ContactNo + "');";


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
					else
					{
						MessageBox.Show("Please Enter Bank Code and Name ");
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
			this.BankCode = "";
			this.BankName = "";
			this.AddressLine1 = "";
			this.AddressLine2 = "";
			this.AddressLine3 = "";
			this.Email = "";
			this.ContactNo = "";
		}

		#endregion

		#region validations
		public string Error { get { return null; } }

		public string this[string PropertyName]
		{
			get
			{
				string error = null;

				switch (PropertyName)
				{
					case "BankCode":
						error = ValidateBankCode();
						break;
					case "BankName":
						error = ValidateBankName();
						break;
					case "AddressLine1":
						error = ValidateAddr1();
						break;
					case "AddressLine2":
						error = ValidateAddr2();
						break;
					case "AddressLine3":
						error = ValidateAddr3();
						break;
					case "ContactNo":
						error = ValidateContact();
						break;
					case "Email":
						error = ValidateEmali();
						break;

				}

				return error;
			}
		}

		private string ValidateBankCode()
		{

			if (string.IsNullOrEmpty(BankCode))
				return "Please Enter Bank Code";
		   
			int value;

			bool success = (int.TryParse(BankCode, out value));

			if (!success)
			{
				return "Please enter an interger value";
			}
			
			return null;
		}

		private string ValidateBankName()
		{
		  
			if (string.IsNullOrEmpty(BankName))
				return "Please Fill the required Felids";
		   
			return null;
		}

		private string ValidateAddr1()
		{

			if (string.IsNullOrEmpty(AddressLine1))
				return "Please Fill the required Felids";

			return null;
		}

		private string ValidateAddr2()
		{

			if (string.IsNullOrEmpty(AddressLine2))
				return "Please Fill the required Felids";

			return null;
		}

		private string ValidateAddr3()
		{

			if (string.IsNullOrEmpty(AddressLine3))
				return "Please Fill the required Felids";

			return null;
		}

		private string ValidateContact()
		{

			if (string.IsNullOrEmpty(ContactNo))
				return "Please Fill the required Felids";

			int value;

			bool success = (int.TryParse(ContactNo, out value));

			if (!success)
			{
				return "Please enter an interger value";
			}
			return null;
		}

		private string ValidateEmali()
		{

			if (string.IsNullOrEmpty(Email))
				return "Please Fill the required Felids";
			else if (!Regex.IsMatch(Email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
				return "Enter a Valid Email";
			return null;
		}

		
		#endregion

		#region Bankupdate_page
		public ObservableCollection<RegisterBankModel> updateBankModel { get; set; }


		private void FillList()
		{
			try
			{
				DB_Con.connection_Sql();
				DB_Con.con.Open();
				DB_Con.com = new SqlCommand("select * from Branch_details", DB_Con.con);
				DB_Con.adapter = new SqlDataAdapter(DB_Con.com);
				DB_Con.ds = new DataSet();
				DB_Con.adapter.Fill(DB_Con.ds, "tblBankdetails");

				if (updateBankModel == null)
					updateBankModel = new ObservableCollection<RegisterBankModel>();

				foreach (DataRow dr in DB_Con.ds.Tables[0].Rows)
				{
					updateBankModel.Add(new RegisterBankModel
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

		#region Load_bankdetails

		private void FillBankName(string BankCode)
		{
			try
			{

				DB_Con.con = new SqlConnection(DB_Con.ConString);
				DB_Con.con.Open();
				string query = "select Bank_Name,Address_line1,Address_line2,Address_line3,Email,Tel_no from Bank_details where Bank_Code='" + BankCode + "'";
				DB_Con.com = new SqlCommand(query, DB_Con.con);
				SqlDataAdapter da = new SqlDataAdapter(DB_Con.com);
				DataSet ds = new DataSet();
				da.Fill(ds, "tblBankdetails");

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					string Bank_Name = dr["Bank_Name"].ToString();
					_itemInEditMode.BankName = Bank_Name;
					string Address_line1 = dr["Address_line1"].ToString();
					_itemInEditMode.AddressLine1 = Address_line1;
					string Address_line2 = dr["Address_line2"].ToString();
					_itemInEditMode.AddressLine2 = Address_line2;
					string Address_line3 = dr["Address_line3"].ToString();
					_itemInEditMode.AddressLine3 = Address_line3;
					string Email = dr["Email"].ToString();
					_itemInEditMode.Email = Email;
					string Contact = dr["Tel_no"].ToString();
					_itemInEditMode.ContactNo = Contact;
				}

				DB_Con.com.Dispose();
				DB_Con.con.Close();
			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}

		}

		private RegisterBankModel _selectedValue;

		public RegisterBankModel SelectedValue
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




		private RegisterBankModel _itemInEditMode = new RegisterBankModel();


		public RegisterBankModel ItemInEditMode
		{
			get { return _itemInEditMode; }
		}

		#endregion
		public void update()
		{

			try
			{
				SaveCommand = new DelegateCommand(update, () => CanSave);

				
				string query = "UPDATE Bank_details SET Bank_Name ='" + this._itemInEditMode.BankName.Trim() + "'  ,Address_line1 ='" + this._itemInEditMode.AddressLine1.Trim() + "',Address_line2 ='" + this._itemInEditMode.AddressLine2.Trim() + "',Address_line3 = '" + this._itemInEditMode.AddressLine3.Trim() + "',Email='" + this._itemInEditMode.Email.Trim() + "',Tel_no='" + this._itemInEditMode.ContactNo.Trim() + "'   where Bank_Code='" + _selectedValue2.BankCode.Trim() + "'";


				int noline = DB_Con.insert_del_update(query);

				if (noline > 0)
				{
					MessageBox.Show("Data Updated Successfully");
					ActivateItem(new BankRecordsViewModel());
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
		#endregion


		#region BankDelete_Page


		public ObservableCollection<RegisterBankModel> deleteBankModel { get; set; }

		private void FillList_deletePage()
		{
			

			try
			{
				DB_Con.connection_Sql();
				DB_Con.con.Open();
				DB_Con.com = new SqlCommand("select * from Bank_details", DB_Con.con);
				DB_Con.adapter = new SqlDataAdapter(DB_Con.com);
				DB_Con.ds = new DataSet();
				DB_Con.adapter.Fill(DB_Con.ds, "tblBankdetails");

				if (deleteBankModel == null)
					deleteBankModel = new ObservableCollection<RegisterBankModel>();

				foreach (DataRow dr in DB_Con.ds.Tables[0].Rows)
				{
					deleteBankModel.Add(new RegisterBankModel
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
		
	   
		 private RegisterBankModel _selectedValue2;

		 public RegisterBankModel SelectedValue2
		 {
			 get { return _selectedValue2; }

			 set
			 {

				 _selectedValue2 = value;
				 NotifyOfPropertyChange(() => SelectedValue2);

				 FillBankDetails(_selectedValue2.BankCode);
				 NotifyOfPropertyChange(() => ItemInEditMode2);
			 }
		 }


		 private RegisterBankModel _itemInEditMode2 = new RegisterBankModel();


		 public RegisterBankModel ItemInEditMode2
		 {
			 get { return _itemInEditMode2; }
		 }

		 private void FillBankDetails(string BankCode)
		 {
			 try
			 {

				 DB_Con.con = new SqlConnection(DB_Con.ConString);
				 DB_Con.con.Open();
				 string query = "select Bank_Name,Address_line1,Address_line2,Address_line3,Email,Tel_no from Bank_details where Bank_Code='" + BankCode + "'";
				 DB_Con.com = new SqlCommand(query, DB_Con.con);
				 SqlDataAdapter da = new SqlDataAdapter(DB_Con.com);
				 DataSet ds = new DataSet();
				 da.Fill(ds, "tblBankdetails");

				 foreach (DataRow dr in ds.Tables[0].Rows)
				 {
					 string Bank_Name = dr["Bank_Name"].ToString();
					 _itemInEditMode2.BankName = Bank_Name;
					 string Address_line1 = dr["Address_line1"].ToString();
					 _itemInEditMode2.AddressLine1 = Address_line1;
					 string Address_line2 = dr["Address_line2"].ToString();
					 _itemInEditMode2.AddressLine2 = Address_line2;
					 string Address_line3 = dr["Address_line3"].ToString();
					 _itemInEditMode2.AddressLine3 = Address_line3;
					 string Email = dr["Email"].ToString();
					 _itemInEditMode2.Email = Email;
					 string Contact = dr["Tel_no"].ToString();
					 _itemInEditMode2.ContactNo = Contact;


				 }

				 DB_Con.com.Dispose();
				 DB_Con.con.Close();
			 }
			 catch (Exception ex)
			 {
				 MessageBox.Show(ex.ToString());
			 }

		 }

		 public void Delete()
		 {
			 try
			 {
				 SaveCommand = new DelegateCommand(Delete, () => CanSave);
				 string query = "delete from Bank_details where Bank_Code='" + _selectedValue2.BankCode.Trim() + "'";

				 int noline = DB_Con.insert_del_update(query);
				 if (noline > 0)
				 {
					 MessageBox.Show("Row Deleted!");
					 ActivateItem(new BankRecordsViewModel());
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
		#endregion
	} 
}
