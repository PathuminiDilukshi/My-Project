using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ITT_sys.ViewModels;
using Caliburn.Micro;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ITT_sys.Models
{
    public class ValidationsCommand : INotifyPropertyChanged 
    {


        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual bool OnPropertyChanged<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }

       
       
    }

    
}
