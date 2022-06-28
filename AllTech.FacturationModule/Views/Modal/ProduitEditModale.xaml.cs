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
using AllTech.FrameWork.Global;

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for ProduitEditModale.xaml
    /// </summary>
    public partial class ProduitEditModale : Window
    {
        Window parentWindow;
       
        public ProduitEditModale(Window window)
        {
            InitializeComponent();
          
            parentWindow = window;
            this.Height = GlobalDatas.mainHeight * 0.75;
            this.Width = GlobalDatas.mainWidth * 0.75;
            productGrid.Width = this.Width * 0.87;
            productGrid.Height = this.Height * 0.46;

          
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            //EvenRefreshGridDataRef action = new EvenRefreshGridDataRef();
            //EventArgs e1 = new EventArgs();
            //action.typeOperation = "event";
            //action.OnChangeList(e1);
        }

        private void btnAddNewdetai_Click(object sender, RoutedEventArgs e)
        {
           // ProduitModel produitSelect = ((Button)sender).CommandParameter as ProduitModel;
            DataRefPrestationViewModel vieModel = this.DataContext as DataRefPrestationViewModel;
            if (vieModel.ProduitSelected != null)
            {
                DeataiProduit vf = new DeataiProduit(vieModel.ProduitSelected);
                vf.Owner = parentWindow;
                vf.ShowDialog();

                DetailProductModel detService = new DetailProductModel();
                vieModel.DetailProduits = detService.DETAIL_PRODUIT_GETLISTE(vieModel.ProduitSelected.IdProduit, 0);
            }
        }

        private void cmbLangueAff_SelectionChanged(object sender, EventArgs e)
        {
            DataRefPrestationViewModel vieModel = this.DataContext as DataRefPrestationViewModel;
            LangueModel langue = cmbLangueAff.SelectedItem as LangueModel;
            if (langue != null)
                vieModel.LangueSelected = langue;
            else vieModel.LangueSelected = null;
        }
    }
}
