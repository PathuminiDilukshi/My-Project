using System;
using System.Windows;  
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITT_sys.ViewModels;
using ITT_sys.ViewModels.Commands;

namespace ITT_sys.Models
{
    public class RegisterBankModel : ValidationsCommand
    {
        private Visibility _MessageVisibilty; 
 


        public Visibility MessageVisibilty 
        {
            get { return _MessageVisibilty; } 
            set
            { 
                OnPropertyChanged(ref _MessageVisibilty, value);
            } 
        }  
        
        public string BankCode
        {
            get;
            set;
            
        }
        public string BankName
        {
            get;
            set;
            
        }
        public string AddressLine1
        {
            get;
            set;
            
        }
        public string AddressLine2
        {
            get;
            set;

        }
        public string AddressLine3
        {
            get;
            set;

        }
        public string ContactNo
        {
            get;
            set;

        }
        public string Email
        {
            get;
            set;

        }

    }
}
