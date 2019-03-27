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
using ITT_sys.Models;
using System.Text.RegularExpressions;



namespace ITT_sys.ViewModels
{
    public class RegisterBankViewModel : ValidationsCommand, IDataErrorInfo, INotifyPropertyChanged
	{
		#region properties_intializing

		private string _bankCode;
		private string _bankName;
		private string _addressLine1;
		private string _addressLine2;
		private string _addressLine3;
		private string _contactNo;
		private string _email;
        private string _calculatorOutput;



		public string BankCode
		{
			get { return _bankCode; }
			set
			{
                OnPropertyChanged(ref _bankCode, value);
			}
		}
		public string BankName
		{
			get { return _bankName; }
			set
			{
                OnPropertyChanged(ref _bankName, value);
			}
		}
		public string AddressLine1
		{
			get { return _addressLine1; }
			set
			{
                OnPropertyChanged(ref _addressLine1, value);
			}
		}
		public string AddressLine2
		{
			get { return _addressLine2; }
			set
			{

                OnPropertyChanged(ref _addressLine2, value);
			}
		}
		public string AddressLine3
		{
			get { return _addressLine3; }
			set
			{
                OnPropertyChanged(ref _addressLine3, value);
			}
		}
		public string ContactNo
		{
			get { return _contactNo; }
			set
			{
                OnPropertyChanged(ref _contactNo, value);
			}
		}
		public string Email
		{
			get { return _email; }
			set
			{
                OnPropertyChanged(ref _email, value);
			}
		}
        public String CalculatorOutput
        {
            get { return _calculatorOutput; }
            set
            {
                _calculatorOutput = value;
                NotifyPropertyChanged("CalculatorOutput");
            }
        }

        private void NotifyPropertyChanged(string p)
        {
           
        }
		
		#endregion 




        #region btton_click

        DBCon DB_Con = new DBCon();


        public RegisterBankViewModel()
        {
            SaveCommand = new DelegateCommand(Save, () => CanSave);
        }

        public ICommand SaveCommand { get; private set; }

        public bool CanSave
        {
            get { return true; }
            
        }

        public void Save()
        {
            
                 try
                 {

                     string query = "INSERT INTO Bank_details(Bank_Code,Bank_Name,Address_line1,Address_line2,Address_line3,Email,Tel_no) values('" + this.BankCode + "','" + this.BankName + "','" + this.AddressLine1 + "','" + this.AddressLine2 + "','" + this.AddressLine3 + "','" + this.Email + "','" + this.ContactNo + "');";


                     int noline = DB_Con.insert_del_update(query);

                     if (noline > 0)
                     {
                         //MessageBox.Show("Data inserted Successfully");
                       
                        
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
        public void Clear()
        {
            this.BankCode ="";
            this.BankName ="";
            this.AddressLine1 ="";
            this.AddressLine2 ="";
            this.AddressLine3="";
            this.Email="";
            this.ContactNo = "";

        }


       

       
    } 
}
