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

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for ComptaOhadaLibellePlage.xaml
    /// </summary>
    public partial class ComptaOhadaLibellePlage : Window
    {
        ComptaOhadaLibellePlageViewModel localViewModel;
        public ComptaOhadaLibellePlage(Window localwindow)
        {
            InitializeComponent();

            ComptaOhadaLibellePlageViewModel viewModel = new ComptaOhadaLibellePlageViewModel(localwindow);
            this.DataContext = viewModel;
            localViewModel = viewModel;
        }

        private void gridPlage_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CompteLibelleOhadaModel compte = gridPlage.ActiveItem as CompteLibelleOhadaModel;
            if (compte != null)
                localViewModel.CompteSelect = compte;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            //{
            this.DialogResult = true;
            //}
        }
    }
}
