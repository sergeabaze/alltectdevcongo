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
using AllTech.FrameWork.Model;
using AllTech.FrameWork.Utils;

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for DetailProduitClient.xaml
    /// </summary>
    public partial class DetailProduitClient : Window
    {

        DetailProduitClientViewModel localViewModel;

        public DetailProduitClient(ClientModel  client)
        {
            InitializeComponent();
            DetailProduitClientViewModel viewModel = new DetailProduitClientViewModel(client);
            this.DataContext = viewModel;
            localViewModel = viewModel;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            {
                this.DialogResult = true;
            }
        }


        private void DetailView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.localViewModel.DetailProduitSelect = ((ListViewItem)sender).Content as DetailProductModel;
            e.Handled = true;
        }

        private void DetailView_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
           // this.localViewModel.DetailProduitSelect = ((ListViewItem)sender).Content as DetailProductModel;
           this.localViewModel.DetailProduitSelect = this.DetailView.SelectedItem as DetailProductModel;
        }
    }
}
