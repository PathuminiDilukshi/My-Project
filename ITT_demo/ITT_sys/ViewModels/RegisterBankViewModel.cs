using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using ITT_sys.ViewModels.Commands;
using ITT_sys.ViewModels.DB_Connection;



namespace ITT_sys.ViewModels
{
	public class RegisterBankViewModel : Screen,INotifyPropertyChanged
	{
		#region properties_intializing

		private string _bankCode;
		private string _bankName;
		private string _addressLine1;
		private string _addressLine2;
		private string _addressLine3;
		private string _contactNo;
		private string _email;



		public string BankCode
		{
			get { return _bankCode; }
			set
			{
				_bankCode = value;
				NotifyOfPropertyChange("BankCode");
			}
		}
		public string BankName
		{
			get { return _bankName; }
			set
			{
				_bankName = value;
				NotifyOfPropertyChange("BankName");
			}
		}
		public string AddressLine1
		{
			get { return _addressLine1; }
			set
			{
				_addressLine1 = value;
				NotifyOfPropertyChange("AddressLine1");
			}
		}
		public string AddressLine2
		{
			get { return _addressLine2; }
			set 
			{
				_addressLine2 = value;
				NotifyOfPropertyChange("AddresSLine2"); 
			}
		}
		public string AddressLine3
		{
			get { return _addressLine3; }
			set
			{
				_addressLine3 = value;
				NotifyOfPropertyChange("AddresSLine3");
			}
		}
		public string ContactNo
		{
			get { return _contactNo; }
			set 
			{    _contactNo = value;
			NotifyOfPropertyChange("ContactNo");
			}
		}
		public string Email
		{
			get { return _email; }
			set 
			{ 
				_email = value;
				NotifyOfPropertyChange("Email");
			}
		}
		
		#endregion 

		 DBCon DB_Con = new DBCon();


		public RegisterBankViewModel ()
		{
				 SaveCommand = new DelegateCommand(Save, () => CanSave);
		}

		public  ICommand SaveCommand { get; private set; }

		public bool CanSave
		{
			get { return !string.IsNullOrEmpty(BankCode) && !string.IsNullOrEmpty(BankName); }
			//get { return !string.IsNullOrEmpty(BankCode) && !string.IsNullOrEmpty(BankName) && !string.IsNullOrEmpty(AddressLine1) && !string.IsNullOrEmpty(AddressLine2) && !string.IsNullOrEmpty(AddressLine3) && !string.IsNullOrEmpty(ContactNo) && !string.IsNullOrEmpty(Email); }
		}

		public void Save()
		{
			try
		{

			string query = "INSERT INTO User_infomation(ID,First_name,Address) values('" + this.BankCode + "','" + this.BankName + "','" + this.AddressLine1 + "','" + this.AddressLine2 + "','" + this.AddressLine3 + "','" + this.ContactNo + "','" + this.Email + "');";


			int noline = DB_Con.insert_del_update(query);

			if (noline > 0)
			{
				MessageBox.Show("Data inserted Successfully");
			}
			else
			{
				MessageBox.Show("Try again!");
			}


		}
		catch( Exception ex)
		{   
			MessageBox.Show(ex.ToString());
		}
	  }

	} 
}
