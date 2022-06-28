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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AllTech.FrameWork.Model;

namespace AllTech.FacturationModule.Views.UCFacture
{
    /// <summary>
    /// Interaction logic for uc_departement.xaml
    /// </summary>
    public partial class uc_departement : UserControl
    {
        DepartementViewModel localviewModel;
        public uc_departement()
        {
            DepartementViewModel viewModel = new DepartementViewModel();
            InitializeComponent();
            localviewModel = viewModel;
            this.DataContext = viewModel;
        }

        private void DetailViewDep_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.localviewModel.DepSelected = DetailViewDep.SelectedItem as DepartementModel;
        }
    }
}
