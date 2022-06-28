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
using AllTech.FacturationModule.ViewModel;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for Facture_rebus.xaml
    /// </summary>
    public partial class Facture_rebus : Window
    {
        FactureRebusViewModel localViewModel;

        public Facture_rebus()
        {
            FactureRebusViewModel viewModel = new FactureRebusViewModel();
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
    }
}
