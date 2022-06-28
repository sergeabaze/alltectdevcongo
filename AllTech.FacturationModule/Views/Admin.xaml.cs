using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AllTech.FacturationModule.ViewModel;
using AllTech.FrameWork.Global;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : UserControl
    {
        AdminViewModel localViewModel;
        bool isloading;
        public Admin()
        {
            AdminViewModel viewModel = new AdminViewModel();
            InitializeComponent();
            this.DataContext = viewModel;
            //if (GlobalDatas.mainHeight > 120)
            //    LayoutRoot.Height = GlobalDatas.mainHeight - 120;
            //else LayoutRoot.Height = 110;

            GridHistorique.Height = GlobalDatas.mainHeight - 400;
            isloading = true;
           this.SizeChanged+=new SizeChangedEventHandler(Admin_SizeChanged);

        }

        private void Admin_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!isloading)
            {
                //if (e.PreviousSize.Height < e.NewSize.Height)
                //    GridHistorique.Height = GlobalDatas.mainHeight - 460;
                //else
                   GridHistorique.Height = GlobalDatas.mainHeight - 400;
            }
            isloading = false;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                //var task = button.DataContext as Task;

                //((ObservableCollection<Task>)lstBxTask.ItemsSource).Remove(task);
            }
            else
            {
                return;
            }
        }
    }
}
