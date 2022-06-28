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
    /// Interaction logic for ObjetClientList.xaml
    /// </summary>
    public partial class ObjetClientList : Window
    {
        ObjetClientListViewModel localViewModel;
        public ClientModel currentClient;

        public ObjetClientList()
        {
            InitializeComponent();
            ObjetClientListViewModel viewModel = new ObjetClientListViewModel(this);
            localViewModel = viewModel;
            this.DataContext = viewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            localViewModel.CurrentClient = currentClient;
        }

        private void lstObjet_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //this.localViewModel.ExploitSelected = ((ListViewItem)sender).Content as ExploitationFactureModel;
            //e.Handled = true;

            this.localViewModel.ObjeSelected  = DetailView.SelectedItem as ObjetFactureModel  ;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            {
                this.DialogResult = true;
            }
        }

        private void cmbObjets_SelectionChanged(object sender, EventArgs e)
        {
            ObjetGenericModel objt = cmbObjets.SelectedItem as ObjetGenericModel;
            if (objt != null)
                localViewModel.ObjetGenSelected = objt;
        }
    }
}
