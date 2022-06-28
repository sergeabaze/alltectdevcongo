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
    /// Interaction logic for Compte.xaml
    /// </summary>
    public partial class Compte : Window
    {
        CompteViewModel localViewModel;

        public Compte()
        {
            InitializeComponent();
            CompteViewModel viewModel = new CompteViewModel(this);
            localViewModel = viewModel;
            this.DataContext = viewModel;
           // toolBarMain.Width = SystemParameters.WorkArea.Width;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            {
                localViewModel. CompteList = new CompteModel().COMPTE_SELECT();

               
                   
                this.DialogResult = true;
            }
        }

        private void compte_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.localViewModel.CompteSelected = ((ListViewItem)sender).Content as CompteModel;
            e.Handled = true;
        }

        private void DetailView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.localViewModel.CompteSelected = this.DetailView.SelectedItem as CompteModel;
            //this.localViewModel.CompteSelected = ((ListViewItem)sender).Content as CompteModel;
            // e.Handled = true;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
