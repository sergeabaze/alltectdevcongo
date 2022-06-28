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
using AllTech.FrameWork.Utils;
using System.Windows.Threading;
using AllTech.FacturationModule.ViewModel;

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for AdminFreeCustomer.xaml
    /// </summary>
    public partial class AdminFreeCustomer : Window
    {
        public AdminFreeCustomer()
        {
            InitializeComponent();
        }

        public String LBInfos
        {
            get
            {

                return lablinfocli.Content.ToString();
            }
            set
            {
                lablinfocli.Content = value;
                lablinfocli.Refresh();
            }
        }

        public Double ValueProgressBar
        {
            get
            {

                return progreBarClient.Value;
            }
            set
            {
                progreBarClient.Value = value;
                progreBarClient.Refresh();
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            {
                this.DialogResult = true;

                // chargement liste
            }
        }

        private void chkName_Click(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            var item = cb.DataContext;
            ListeFreeClient nitem = item as ListeFreeClient;

            if (cb.IsChecked.Value)
                nitem.Checked = true;
            else nitem.Checked = false;

            LviewGrid.SelectedItem = item;
        }
    }

   
}
