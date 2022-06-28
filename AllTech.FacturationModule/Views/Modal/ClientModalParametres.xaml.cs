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
using AllTech.FrameWork.Global;
using AllTech.FacturationModule.ViewModel;

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for ClientModalParametres.xaml
    /// </summary>
    public partial class ClientModalParametres : Window
    {
        public ClientModalParametres()
        {
            InitializeComponent();
            if ( GlobalDatas.mainWidth<650)
                this.Width = GlobalDatas.mainWidth*0.60;
            else
                this.Width = 650;

            this.Height = 400;
            //this.Width = GlobalDatas.mainWidth -200;
            //this.Height = GlobalDatas.mainHeight -100;
            gridproduits.Height = this.Height * 0.60;

            this.Loaded+=new RoutedEventHandler(ClientModalParametres_Loaded);
        }

        void ClientModalParametres_Loaded(object sender, EventArgs e)
        {
            DatarefViewModel viewModel = this.DataContext as DatarefViewModel;
            viewModel.LoadproduitClients();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void chkitems_Checked_1(object sender, RoutedEventArgs e)
        {
            var produit = this.gridproduits.ActiveItem as produisuivi;
          

            if (produit != null)
            {
                // this.localViewModel.ProduiSuiviSelect = produit;
                DatarefViewModel viewModel = this.DataContext as DatarefViewModel;

                if (!viewModel.isloadingParam)
                {
                    produit.IsParameter = true;
                    viewModel.ListeProduitsuivis.FirstOrDefault(p => p.IDproduit == produit.IDproduit).IsParameter = true;
                    if (!viewModel.ListeProduitsuiviUpdate.Exists(p => p.IDproduit == produit.IDproduit))
                        viewModel.ListeProduitsuiviUpdate.Add(produit);
                    else viewModel.ListeProduitsuiviUpdate.FirstOrDefault(p => p.IDproduit == produit.IDproduit).IsParameter = true;
                    // this.localViewModel.FacturesListe.FirstOrDefault(f => f.IdFacture == facture.IdFacture).IsCheck = true;
                }
            }

        }

        private void chkitems_Unchecked(object sender, RoutedEventArgs e)
        {
            DatarefViewModel viewModel = this.DataContext as DatarefViewModel;
            if (!viewModel.isloadingParam)
            {
                var produit = this.gridproduits.ActiveItem as produisuivi;
                if (produit != null)
                {
                    produit.IsParameter = false;
                    viewModel.ListeProduitsuivis.FirstOrDefault(p => p.IDproduit == produit.IDproduit).IsParameter = false;
                    if (viewModel.ListeProduitsuiviUpdate.Exists(p => p.IDproduit == produit.IDproduit))
                        viewModel.ListeProduitsuiviUpdate.FirstOrDefault(p => p.IDproduit == produit.IDproduit).IsParameter = false;
                    else viewModel.ListeProduitsuiviUpdate.Add(produit);

                }
            }
        }
    }
}
