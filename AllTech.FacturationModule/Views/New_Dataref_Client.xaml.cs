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
using AllTech.FacturationModule.ViewModel;
using AllTech.FrameWork.Model;
using AllTech.FacturationModule.Views.Modal;
using System.Collections.ObjectModel;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for New_Dataref_Client.xaml
    /// </summary>
    public partial class New_Dataref_Client : Window
    {
        DatarefViewModel localViewModel;

        public New_Dataref_Client(Window wpfParent)
        {
            InitializeComponent();
            //Double workHeight = wpfParent.Height;
           // Double workWidth = wpfParent.Width;
            //this.WindowState = WindowState.Maximized;
            this.Top = 100;// (workHeight - this.Height) / 2;
            this.Left = 100;// (workWidth - this.Width) / 2;

            DatarefViewModel viewModel = new DatarefViewModel();
            this.DataContext = viewModel;
            localViewModel = viewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void produitGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.localViewModel.ClientSelected = this.produitGrid.ActiveItem as ClientModel;
            int i = 0;
            int j = 0;
            int ee = 0;
            int d = 0;
            int dd = 0;

            if (this.localViewModel.LanguageList != null)
            {
                foreach (var langue in this.localViewModel.LanguageList)
                {
                    if (langue.Id == this.localViewModel.ClientSelected.IdLangue)
                    {
                        cmbLangue.SelectedIndex = i;
                        break;
                    }

                    i++;
                }
            }

            if (this.localViewModel.ExonerateList != null)
            {
                foreach (var val in this.localViewModel.ExonerateList)
                {
                    if (val.ID == this.localViewModel.ClientSelected.IdExonere)
                    {
                        cmbexonere.SelectedIndex = j;
                        break;
                    }

                    j++;
                }
            }
            //

            if (this.localViewModel.DeviseList != null)
            {
                foreach (var val in this.localViewModel.DeviseList)
                {
                    if (val.ID_Devise == this.localViewModel.ClientSelected.IdDeviseFact)
                    {
                        cmbDevise.SelectedIndex = ee;
                        break;
                    }

                    ee++;
                }
            }

            if (this.localViewModel.TaxePorataList != null)
            {
                foreach (var val in this.localViewModel.TaxePorataList)
                {
                    if (val.ID_Taxe == this.localViewModel.ClientSelected.Idporata)
                    {
                        cmbPorata.SelectedIndex = d;
                        break;
                    }
                    if (this.localViewModel.ClientSelected.Idporata == 0)
                    {
                        cmbPorata.SelectedIndex = -1;
                        break;
                    }

                    d++;
                }
            }

            if (this.localViewModel.CompteList != null)
            {
                foreach (var val in this.localViewModel.CompteList)
                {
                    if (val.ID == this.localViewModel.ClientSelected.IdCompte)
                    {
                        cmbCompte.SelectedIndex = dd;
                        break;
                    }

                    dd++;
                }
            }

            int ter = 0;
            if (this.localViewModel.LibelleList != null)
            {
                foreach (var val in this.localViewModel.LibelleList)
                {
                    if (val.ID == this.localViewModel.ClientSelected.IdTerme)
                    {
                        cmbTerme.SelectedIndex = ter;
                        break;
                    }

                    ter++;
                }
            }



        }

        private void detail_click(object sender, RoutedEventArgs e)
        {
            ClientModel client = ((Button)sender).CommandParameter as ClientModel;
            DetailProduitClient vf = new DetailProduitClient(client);
          //  vf.Owner = Application.Current.MainWindow;
            vf.ShowDialog();
        }

        private void cmbLangue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int val = cmbLangue.SelectedIndex;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            //{
            this.DialogResult = true;
            // }
        }
    }
}
