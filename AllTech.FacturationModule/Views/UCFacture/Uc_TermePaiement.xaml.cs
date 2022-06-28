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
    /// Interaction logic for Uc_TermePaiement.xaml
    /// </summary>
    public partial class Uc_TermePaiement : UserControl
    {
        TermeViewModel localviewModel;
        public Uc_TermePaiement()
        {
            TermeViewModel viewModel = new TermeViewModel();
            InitializeComponent();
            localviewModel = viewModel;
            this.DataContext = viewModel;
        }

        private void DetailViewTerme_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.localviewModel.TermeSelected = DetailViewTerme.SelectedItem as LibelleTermeModel;
        }
    }
}
