using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace ITT_sys.ViewModels.Commands
{
    public class RelayCommand: ICommand

    {
        public ShellViewModel ViewModel { get; set; }


        public RelayCommand(ShellViewModel _viewModel)
        {
            this.ViewModel = _viewModel;
        }

        public bool CanExecute(object parameter)
        {
            
            return false;
        }

        public event EventHandler CanExecuteChanged;


        //Execute method action is going to happen 
        public void Execute(object parameter)
        {
            this.ViewModel.SignIn_btn_click();
        }
    }
}
