using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ITT_sys.ViewModels;
using ITT_sys.Models;

namespace ITT_sys.Views
{
    /// <summary>
    /// Interaction logic for SupplierRegisterView.xaml
    /// </summary>
    public partial class SupplierRegisterView : UserControl
    {
        TruckerModel objEmpToAdd = new TruckerModel();
        SupplierRegisterViewModel objDs = new SupplierRegisterViewModel();

        public SupplierRegisterView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            
           dgEmp.ItemsSource = objDs.GetAlltruckers();
        }

        private void dgEmp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            objEmpToAdd = dgEmp.SelectedItem as TruckerModel;

        }


        private void dgEmp_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                FrameworkElement element_TruckId = dgEmp.Columns[0].GetCellContent(e.Row);
                if (element_TruckId.GetType() == typeof(TextBox))
                {
                    var truckId = ((TextBox)element_TruckId).Text;
                    objEmpToAdd.TruckId = Convert.ToString(truckId);
                }

                FrameworkElement element_TruckType = dgEmp.Columns[1].GetCellContent(e.Row);
                if (element_TruckType.GetType() == typeof(TextBox))
                {
                    var truckType = ((TextBox)element_TruckType).Text;
                    objEmpToAdd.TruckType = Convert.ToString(truckType);
                }

                FrameworkElement element_TruckSize = dgEmp.Columns[2].GetCellContent(e.Row);
                if (element_TruckSize.GetType() == typeof(TextBox))
                {
                    var truckSize = ((TextBox)element_TruckSize).Text;
                    objEmpToAdd.TruckSize = Convert.ToString(truckSize);
                }
                FrameworkElement element_JoinDate = dgEmp.Columns[3].GetCellContent(e.Row);
                if (element_JoinDate.GetType() == typeof(TextBox))
                {
                    var joinDate = ((TextBox)element_JoinDate).Text;
                    objEmpToAdd.JoinDate = Convert.ToString(joinDate);
                }
                FrameworkElement element_Status = dgEmp.Columns[3].GetCellContent(e.Row);
                if (element_Status.GetType() == typeof(TextBox))
                {
                    var status = ((TextBox)element_Status).Text;
                    objEmpToAdd.Status = Convert.ToString(status);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgEmp_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                var Res = MessageBox.Show("Do you want to Create this new entry", "Confirm", MessageBoxButton.YesNo);
                if (Res == MessageBoxResult.Yes)
                {
                    objDs.InsertEmployee(objEmpToAdd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
    }
}
