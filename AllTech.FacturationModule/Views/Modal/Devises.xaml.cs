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
    /// Interaction logic for Devises.xaml
    /// </summary>
    public partial class Devises : Window
    {
        DeviseViewModel localViewModel;

        public Devises()
        {
            InitializeComponent();
            DeviseViewModel viewModel = new DeviseViewModel(this);
            localViewModel = viewModel;
            this.DataContext = viewModel;
        }

        private void devise_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //this.localViewModel.DeviseSelected = ((ListViewItem)sender).Content as DeviseModel;
            //e.Handled = true;
            this.localViewModel.DeviseSelected = Deviseslist.SelectedItem  as DeviseModel;
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
