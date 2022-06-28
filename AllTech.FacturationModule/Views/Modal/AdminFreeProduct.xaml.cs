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
using System.Windows.Shapes;
using AllTech.FacturationModule.ViewModel;

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for AdminFreeProduct.xaml
    /// </summary>
    public partial class AdminFreeProduct : Window
    {
        public AdminFreeProduct()
        {
            InitializeComponent();
        }

        public String LBInfos
        {
            get
            {

                return lablinfoProduct.Content.ToString();
            }
            set
            {
                lablinfoProduct.Content = value;
                lablinfoProduct.Refresh();
            }
        }

        public Double ValueProgressBar
        {
            get
            {
                return progreBarProduct.Value;
            }
            set
            {
                progreBarProduct.Value = value;
                progreBarProduct.Refresh();
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void chkName_Click(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;

          
            var item = cb.DataContext;
            FreeProduct nitem = (FreeProduct)item;
            if (cb.IsChecked.Value)
                nitem.IsChecked=true;
            else nitem.IsChecked = false;
            LviewGrid.SelectedItem = nitem;
        }

        private void chList_Checked(object sender, RoutedEventArgs e)
        {
            List<FreeProduct> list=LviewGrid.ItemsSource as List<FreeProduct>;
            for (int i = 0; i < list.Count - 1; i++)
                list[i].IsChecked = true;
            LviewGrid.ItemsSource = list;
            LviewGrid.UpdateLayout();
           
        }

        private void chList_Unchecked(object sender, RoutedEventArgs e)
        {
            List<FreeProduct> list = LviewGrid.ItemsSource as List<FreeProduct>;
            for (int i = 0; i < list.Count - 1; i++)
                list[i].IsChecked = false;
            LviewGrid.ItemsSource = list;
            LviewGrid.UpdateLayout();
        }
    }
}
