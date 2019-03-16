using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ITT_sys.Views;
using ITT_sys.ViewModels.Commands;
using ITT_sys.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ITT_sys.ViewModels
{
    public class DashboardViewModel : Conductor<object>,INotifyPropertyChanged
    {
     
        public void registerPage_Bank()
        {

            ActivateItem(new RegisterBankViewModel());
        }

        public void ButtonClose()
        {
            Application.Current.Shutdown();
        }


        public void registerPage_Branch()
        {

            ActivateItem(new RegisterBranchViewModel());
        }
        public void registerPage_Supplier()
        {

            ActivateItem(new SupplierRegisterViewModel());
        }
         
    }
    
}
