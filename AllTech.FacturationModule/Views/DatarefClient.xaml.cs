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
using AllTech.FacturationModule.ViewModel;
using AllTech.FrameWork.Model;
using AllTech.FacturationModule.Views.Modal;
using AllTech.FrameWork.Utils;
using System.Collections.ObjectModel;
using AllTech.FrameWork.Global;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for DatarefPrestation.xaml
    /// </summary>
    public partial class DatarefClient : UserControl
    {
        DataRefPrestationViewModel _viewmodel;
        private ObservableCollection<CheckedListItem<string>> productFilters = new ObservableCollection<CheckedListItem<string>>();
        private CollectionViewSource viewSource = new CollectionViewSource();
        Window local;
        bool isloading;

        public DatarefClient(Window window)
        {
            InitializeComponent();
            local = window;
            DataRefPrestationViewModel viewModel = new DataRefPrestationViewModel( window);
            this.DataContext = viewModel; 
            _viewmodel = viewModel;
            toolbarMain.Width = SystemParameters.WorkArea.Width;

            productGrid.Height = GlobalDatas.mainHeight-400;
            double vWidleft = GlobalDatas.mainWidth * 0.15;
            optionrechechName.Width =  vWidleft;
            txtTRecherche.Width = optionrechechName.Width * 0.70;
            isloading = true;
            //GlobalDatas.mainWidth ;
            //GlobalDatas.mainHeight = this.ActualHeight;
        }

      

        private void preductGrid_SelectChange(object sender, MouseButtonEventArgs e)
        {


            this._viewmodel.ProduitSelected = productGrid.ActiveItem as ProduitModel;
            ProduitEditModale view = new ProduitEditModale(local);
            view.Owner = local;
            view.DataContext = _viewmodel;
            view.ShowDialog();

            //if (this._viewmodel.ProduitSelected != null)
            //{
            //    if (_viewmodel.LanguageList != null)
            //    {
            //        int i = 0;
            //        foreach (var val in _viewmodel.LanguageList)
            //        {
            //            if (val.Id == _viewmodel.ProduitSelected.IdLangue)
            //            {
            //                cmbLangueAff.SelectedIndex = i;
            //                break;
            //            }

            //            i++;
            //        }
            //    }
            //}
        }

        private void detail_click(object sender, RoutedEventArgs e)
        {
            ProduitModel  produitSelect = ((Button)sender).CommandParameter as ProduitModel ; 
            DeataiProduit vf = new DeataiProduit(produitSelect);
            vf.Owner = local;
            vf.ShowDialog();
        }

        private void btnDesignationFilter_Click(object sender, RoutedEventArgs e)
        {
            productFilters = this._viewmodel.ProductFilters;
            viewSource.Filter += viewSource_Filter;
            viewSource.Source = _viewmodel.ProduitList ;
          //  lstProduits.ItemsSource = productFilters;


          // popDesignation.IsOpen = true;
        }

        private void viewSource_Filter(object sender, FilterEventArgs e)
        {
            ProduitModel prod = (ProduitModel)e.Item;

            int count = productFilters.Where(w => w.IsChecked).Count(w => w.Item == prod.Libelle );

            if (count == 0)
            {
                e.Accepted = false;
                return;
            }

            e.Accepted = true;
        }

        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (CheckedListItem<string> item in productFilters)
            {
                item.IsChecked = true;
            }

        }

        private void btnUnselectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (CheckedListItem<string> item in productFilters)
            {
                item.IsChecked = false;
            }

        }

        private void ApplyFilters(object sender, RoutedEventArgs e)
        {
            viewSource.View.Refresh();
            _viewmodel.ViewSource.View.Refresh();
        }

        private void cmbLangueAff_SelectionChanged(object sender, EventArgs e)
        {
            LangueModel langue = cmbLangueAff.SelectedItem as LangueModel;
            if (langue != null)
                _viewmodel.LangueSelected = langue;
            else _viewmodel.LangueSelected = null;
        }

        //private void cmbLangue_SelectionChanged(object sender, EventArgs e)
        //{
        //    LangueModel langue = cmbLangue.SelectedItem as LangueModel;
        //    if (langue != null)
        //        _viewmodel.LanguageSelected = langue;
        //}

        private void cmbLangue_Click(object sender, RoutedEventArgs e)
        {
            cmbLangueAff.SelectedIndex = -1;
        }

        private void txtRecherche_Click(object sender, RoutedEventArgs e)
        {
            txtTRecherche.Text = string.Empty;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!isloading)
            {
                productGrid.Height = GlobalDatas.mainHeight - 400;
                double vWidleft = GlobalDatas.mainWidth * 0.15;
                optionrechechName.Width = vWidleft;
                txtTRecherche.Width = optionrechechName.Width * 0.70;
            }
                isloading = false;
        }

      
    }
}
