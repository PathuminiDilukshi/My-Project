using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ITT_sys.Attachedproperties
{
    public class PasswordBoxProperties
    {
        //listner to attached attached property
        public static readonly DependencyProperty MonitorPasswordProperty = DependencyProperty.RegisterAttached("MonitorPassword", typeof(bool), typeof(PasswordBoxProperties), new PropertyMetadata(false ,OnMonitorPasswordChanged));


        
        private static void OnMonitorPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = (d as PasswordBox);

            if (passwordBox == null)
                return;



            if ((bool)e.NewValue)
            {
                SetHasText(passwordBox);
                passwordBox.PasswordChanged += passwordBox_PasswordChanged;
            }
        }


        //everytime password propery changes this method fired
        static void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            SetHasText((PasswordBox)(sender));
           
            //string pwd =(((PasswordBox)(sender)).Password);
            //Console.WriteLine("password is " + pwd);
        }


        public static void SetMonitorPassword(PasswordBox element,bool value)
        {
            element.SetValue(MonitorPasswordProperty, value);
        }


        public static bool GetMonitorPassword(PasswordBox element)
        {
            return (bool)element.GetValue(MonitorPasswordProperty);
        }


        //public bool hasText { get; set; } = false

        public static readonly DependencyProperty HasTextProperty = DependencyProperty.RegisterAttached("HasText", typeof(bool), typeof(PasswordBoxProperties), new PropertyMetadata(false));

        private static void SetHasText(PasswordBox element)
        {
            element.SetValue(HasTextProperty,element.SecurePassword.Length > 0);
        }


        public static bool GetHasText(PasswordBox element)
        {
            return (bool)element.GetValue(HasTextProperty);
        }
    }
}
