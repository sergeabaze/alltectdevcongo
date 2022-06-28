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
    /// Interaction logic for CompteAnalityques.xaml
    /// </summary>
    public partial class CompteAnalityques : Window
    {

        CompteAnalytiqueViewModel localViewModel;
        public bool IsOperationAction=false ;

        public CompteAnalityques()
        {
            InitializeComponent();
            CompteAnalytiqueViewModel viewmodel = new CompteAnalytiqueViewModel(this);
            localViewModel = viewmodel;
            this.DataContext = viewmodel;
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
            this.localViewModel.CompteSelected = this.DetailView.SelectedItem as CompteAnalytiqueModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsOperationAction = localViewModel.Isoperation;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            {
                this.DialogResult = true;

                // chargement liste
            }

        }
    }
}
