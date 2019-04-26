﻿using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ITT_sys.Views
{
    /// <summary>
    /// Interaction logic for BranchRecordsView.xaml
    /// </summary>
    public partial class BranchRecordsView : UserControl
    {
        public BranchRecordsView()
        {
            InitializeComponent();
        }

        private void DataGrid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;


        }
    }
}
