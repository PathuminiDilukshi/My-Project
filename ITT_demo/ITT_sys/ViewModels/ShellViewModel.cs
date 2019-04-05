using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Controls;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using ITT_sys.ViewModels.Commands;
using ITT_sys.Models;
using ITT_sys.ViewModels.DB_Connection;



namespace ITT_sys.ViewModels
{
    public class ShellViewModel : ValidationsCommand, IDataErrorInfo
    {

        private ShellModel _shellModel;

        #region properties

        private string _username;
        private string _email;
        private string _passwordtxt;
        private string _confirmPassword;

       

      
        public string Username
        {
            get { return _username; }
            set
            {
                OnPropertyChanged(ref _username, value);
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



        public string Passwordtxt
        {
            get { return _passwordtxt; }
            set { _passwordtxt = value; }
        }


     

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; }
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
                    case "Username":
                        error = ValidateUserName();
                        break;
                    case "Email" :
                   
                        error = ValidateEmail();
                        break;
                }

                return error;
            }
        }

        

        public string ValidatePassword(string Password)
        {


            if (string.IsNullOrWhiteSpace(Password))
                return "Password is empty ";
            if (Password.Length < 4)
                return "VeryWeak";
            if (Password.Length >= 8)
                return "weak";
            if (Password.Length >= 12)
                return "Medium";
            if (Regex.Match(Password, @"/\d+/", RegexOptions.ECMAScript).Success)
                return "strong";
            if (Regex.Match(Password, @"/[a-z]/", RegexOptions.ECMAScript).Success &&
              Regex.Match(Password, @"/[A-Z]/", RegexOptions.ECMAScript).Success)
                return "very strong";
            if (Regex.Match(Password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success)
                return "very strong";

            return null;
        }

       

        private string ValidateEmail()
        {
            if (string.IsNullOrEmpty(Email))
                return "email cannot be empty";

            else if (!Regex.IsMatch(Email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                return "Enter a Valid Email";

            return null;
        }

        private  string ValidateUserName()
        {
            if (string.IsNullOrEmpty(Username))
               return "Username cannot be empty";
            else if (Username.Length < 5)
               return "Username must be a minimum of 5 characters.";
            return null;
        }
        #endregion



        #region buttin_click

        DBCon DB_Con = new DBCon();

        public ShellModel ShellModel
        {
            get { return _shellModel; }
            set
            {
                OnPropertyChanged(ref _shellModel, value);
            }
        }

        public ICommand SaveCommand { get; private set; }

        public bool CanSave
        {
            get
            {
                return true;

            }


        }

        public void Save()
        {
          
            try
            {
                SaveCommand = new DelegateCommand(Save, () => CanSave);

                string query = "INSERT INTO User_details(User_Name,Email,Password) values('" + this.Username + "','" + this.Email + "','" + this.Passwordtxt + "');";


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


            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        #endregion
    }

}
