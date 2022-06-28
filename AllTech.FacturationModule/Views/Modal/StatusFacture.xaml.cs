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
using AllTech.FrameWork.Model;

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for StatusFacture.xaml
    /// </summary>
    public partial class StatusFacture : Window
    {
        StatusFactureViewModel localViewModel;
        public StatusFacture()
        {
            StatusFactureViewModel viewModel = new StatusFactureViewModel();
            InitializeComponent();
            localViewModel = viewModel;
            this.DataContext = viewModel;
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


            this.localViewModel.StatutSelected = DetailView.SelectedItem  as StatutModel;
        }
    }
}
