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
    /// Interaction logic for CompteAnalytiqueClient.xaml
    /// </summary>
    public partial class CompteAnalytiqueClient : Window
    {
        CompteAnalClientViewModel localViewModel;
        public CompteAnalytiqueClient(int clientid,string clientName)
        {
            InitializeComponent();

            CompteAnalClientViewModel viewModel = new CompteAnalClientViewModel(this, clientid, clientName);
            this.DataContext = viewModel;
            localViewModel = viewModel;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //if (UserInterfaceUtilities.ValidateVisualTree(this) == false)
           // {
                this.DialogResult = true;
                // chargement liste
           // }
        }

        private void DetailView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
             this.localViewModel.CompteSelected = this.DetailView.SelectedItem as CompteAnalClientModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            {
                this.DialogResult = true;
            }
        }
    }
}
