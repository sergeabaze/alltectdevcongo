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
    /// Interaction logic for CompteGeneral.xaml
    /// </summary>
    public partial class CompteGeneral : Window
    {
        CompteGeneViewModel localViewModel;
        public bool IsOperationAction = false;

        public CompteGeneral()
        {
            InitializeComponent();
            CompteGeneViewModel viewModel = new CompteGeneViewModel(this);
            this.DataContext = viewModel;
            localViewModel = viewModel;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            {
                this.DialogResult = true;

                // chargement liste
            }
        }

        private void DetailView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.localViewModel.CompteGeneSelected = this.DetailView.SelectedItem as CompteGenralModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsOperationAction = localViewModel.IsOperation;
        }
    }
}
