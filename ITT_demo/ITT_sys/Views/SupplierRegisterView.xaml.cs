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
using System.Data;

namespace ITT_sys.Views
{
    /// <summary>
    /// Interaction logic for SupplierRegisterView.xaml
    /// </summary>
    public partial class SupplierRegisterView : UserControl
    {
        List<TruckerModel> t1;
        List<int> lstSelectedEmpNo;
        static public string Title;
        TruckerModel objEmpToAdd = new TruckerModel();
        TruckerModel objSupToAdd = new TruckerModel();
        SupplierRegisterViewModel objDs = new SupplierRegisterViewModel();
        static int index;
        SupplierRegisterViewModel objContext;
       

        public SupplierRegisterView()
        {
            InitializeComponent();
            DataContext = objDs.GetAlltruckers();
          //  var dataprovider = (System.Windows.Data.XmlDataProvider)(((UserControl1)(elementHost1.Child)).Resources["rssData"]);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            dgEmp.ItemsSource = objDs.GetAlltruckers();
            lstSelectedEmpNo = new List<int>();

            ComboBox.ItemsSource = objDs.FillList();
        }

        private void DataGrid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void DatePicker_SelectedDateChanged(object sender,SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                Title = "No date";
                Console.WriteLine("Title =" + Title);
            }
            else
            {
                // ... No need to display the time.
                Title = date.Value.ToShortDateString();
                Console.WriteLine("Title =" + Title);
                objEmpToAdd.registerDate = Title;
            }
        }

       

        private void dgEmp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            objEmpToAdd = dgEmp.SelectedItem as TruckerModel;

        }
       

        private void dgEmp_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
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
            FrameworkElement element = dgEmp.Columns[5].GetCellContent(e.Row);
            if (element.GetType() == typeof(CheckBox))
            {
                if (((CheckBox)element).IsChecked == true)
                {
                    FrameworkElement element_TruckId = dgEmp.Columns[0].GetCellContent(e.Row);
                    int truckId = Convert.ToInt32(((TextBlock)element_TruckId).Text);
                    lstSelectedEmpNo.Add(truckId);
                }
            }
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Res = MessageBox.Show("Do you want to Create this new entry", "Confirm", MessageBoxButton.YesNo);
                if (Res == MessageBoxResult.Yes)
                {
                    objDs.InsertEmployee(objEmpToAdd);
                    dgEmp.Columns[0].IsReadOnly = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Res = MessageBox.Show("Do you want to Update this entry", "Confirm", MessageBoxButton.YesNo);
                if (Res == MessageBoxResult.Yes)
                {
                    objDs.updateSupplier(objSupToAdd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgSup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            objSupToAdd = dgSup.SelectedItem as TruckerModel;

            var dg = sender as DataGrid;
            if (dg == null) return;
            index = dg.SelectedIndex;
            Console.WriteLine("row index =" + index);
            //here we get the actual row at selected index
            DataGridRow row = dg.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;

            //here we get the actual data item behind the selected row
            var item = dg.ItemContainerGenerator.ItemFromContainer(row);
            Console.WriteLine("row item =" + item);
        }

        private void dgSup_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                FrameworkElement element_TruckId = dgSup.Columns[0].GetCellContent(e.Row);
                if (element_TruckId.GetType() == typeof(TextBox))
                {
                    var truckId = ((TextBox)element_TruckId).Text;
                    objSupToAdd.TruckId = Convert.ToString(truckId);
                }
                FrameworkElement element_TruckType = dgSup.Columns[1].GetCellContent(e.Row);
                if (element_TruckType.GetType() == typeof(TextBox))
                {
                    var truckType = ((TextBox)element_TruckType).Text;
                    objSupToAdd.TruckType = Convert.ToString(truckType);
                }

                FrameworkElement element_TruckSize = dgSup.Columns[2].GetCellContent(e.Row);
                if (element_TruckSize.GetType() == typeof(TextBox))
                {
                    var truckSize = ((TextBox)element_TruckSize).Text;
                    objSupToAdd.TruckSize = Convert.ToString(truckSize);
                }   

                FrameworkElement element_Status = dgSup.Columns[3].GetCellContent(e.Row);
                if (element_Status.GetType() == typeof(TextBox))
                {
                    var status = ((TextBox)element_Status).Text;
                    objSupToAdd.Status = Convert.ToString(status);
                }

                FrameworkElement element_regDate = dgSup.Columns[4].GetCellContent(e.Row);
                if (element_Status.GetType() == typeof(TextBox))
                {
                    var JoinDate = ((TextBox)element_regDate).Text;
                    objSupToAdd.JoinDate = Convert.ToDateTime(JoinDate);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgSup_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
           
        }
        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Res = MessageBox.Show("Do you want to Delete this new entry", "Confirm", MessageBoxButton.YesNo);
                if (Res == MessageBoxResult.Yes)
                {
                    objDs.DeleteTruck(objEmpToAdd);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void sup_delete_btn_Click(object sender, RoutedEventArgs e)
        {

            //if (dgSup.SelectedItem != null)
            //{
            //    Console.WriteLine("selected item=" + dgSup.SelectedItem);
            //    this.dgSup.Items.Remove(dgSup.SelectedItem);
            //}
            objDs.DeleteTruck(objEmpToAdd);
            dgSup.Items.RemoveAt(index);
        }

      

        

       
    }
}
