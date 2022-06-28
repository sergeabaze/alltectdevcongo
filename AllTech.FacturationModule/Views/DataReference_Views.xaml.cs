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
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using AllTech.FrameWork.Model;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for DataReference_Views.xaml
    /// </summary>
    public partial class DataReference_Views : UserControl
    {
        DataReferenceViewModel localViewModel;
        bool isloading = false;
        public DataReference_Views(Window control)
        {
          
            InitializeComponent();
            DataReferenceViewModel viewModel = new DataReferenceViewModel(control);
            this.DataContext = viewModel;
            //if (_client.IdClient ==0)
           viewModel.BtnCloseVisible = true  ;
            //else viewModel.BtnCloseVisible = false ;
           //Double workHeight = SystemParameters.WorkArea.Height;
           //Double worTop = SystemParameters.WorkArea.Top;
           //Double workWidth = SystemParameters.WorkArea.Width;
          
           //this.Width = (workWidth / 2) + 400;
           //this.Height = (workHeight / 2) + 350;
           localViewModel = viewModel;
           //double nh = control.Height;
           //double nW = control.Width;
           //double top = control.Top;
          // this.Height = AllTech.FrameWork.Global.GlobalDatas.mainMaxHeight;
           // tabcontrol.Height =AllTech.FrameWork.Global.GlobalDatas.mainMaxHeight;
           // tabcontrol.Height
          // this.Height = workHeight-350;
         
         //  isloading = false;
        }

        private void mnItemsociete_GotFocus(object sender, RoutedEventArgs e)
        {
           // MessageBox.Show("focus societe");
        }

        private void mnUsers_GotFocus(object sender, RoutedEventArgs e)
        {
            localViewModel.IsLoaderUsers();
        }

        private void product_GotFocus(object sender, RoutedEventArgs e)
        {
            localViewModel.IsLoaderProducts();
        }

        private void mnClient_GotFocus(object sender, RoutedEventArgs e)
        {
            localViewModel.IsLoaderClients();
        }

        private void mnDatarefBill_GotFocus(object sender, RoutedEventArgs e)
        {
            localViewModel.IsLoaderDatarefBill();
        }

        private void mnCompta_GotFocus(object sender, RoutedEventArgs e)
        {
            localViewModel.IsLoaderAccount();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //double h = e.NewSize.Height;
            //tabcontrol.Height = h;
           // tabcontrol.Height = e.NewSize.Height - 200;
           // if (e.Handled)
            //{
                //if (e.HeightChanged)
                //{
                //    tabcontrol.Height = e.NewSize.Height - 300;
                //}
              
            //}
            //double w = this.Width;
            //double h = this.Height;
            //Double workHeight = SystemParameters.WorkArea.Height;
            //tabcontrol.Height =  workHeight - 300;

            //if (isloading)
            //{
            //    if (e.NewSize.Width != e.PreviousSize.Width)
            //        tabcontrol.Height = e.NewSize.Height - 230;
            //    //this.Height = e.NewSize.Height-300;
            //    // this.Width = e.NewSize.Width;
            //    isloading = false;
            //}
            //isloading = true;
        }

        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //if (isloading)
            //{
            //    if (e.NewSize.Width != e.PreviousSize.Width )
            //        tabcontrol.Height = e.NewSize.Height - 200;
            //       //this.Height = e.NewSize.Height-300;
            //   // this.Width = e.NewSize.Width;
            //    isloading = false;
            //}
            //isloading = true;
        }

      

       
    }
}
