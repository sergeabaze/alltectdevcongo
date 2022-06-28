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
using AllTech.FrameWork.Utils;
using System.Collections.ObjectModel;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for New_Dataref_Produits.xaml
    /// </summary>
    public partial class New_Dataref_Produits : Window
    {
        DataRefPrestationViewModel _viewmodel;
        private ObservableCollection<CheckedListItem<string>> productFilters = new ObservableCollection<CheckedListItem<string>>();
        private CollectionViewSource viewSource = new CollectionViewSource();
       private   Window formparent;

        public New_Dataref_Produits( Window parent)
        {
            InitializeComponent();
            formparent = parent;

            //Double workHeight = formparent.Height;
            //Double workWidth = formparent.Width;
            //this.WindowState = WindowState.Maximized;
            this.Top = 100;//(workHeight - this.Height) / 2;
            this.Left = 100;// (workWidth - this.Width) / 2;
            DataRefPrestationViewModel viewModel = new DataRefPrestationViewModel();
            _viewmodel = viewModel;

            this.DataContext = viewModel;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            //{
            this.DialogResult = true;
            // }
        }

        private void preductGrid_SelectChange(object sender, MouseButtonEventArgs e)
        {


            this._viewmodel.ProduitSelected = productGrid.ActiveItem as ProduitModel;
            if (this._viewmodel.ProduitSelected != null)
            {
                if (_viewmodel.LanguageList != null)
                {
                    int i = 0;
                    foreach (var val in _viewmodel.LanguageList)
                    {
                        if (val.Id == _viewmodel.ProduitSelected.IdLangue)
                        {
                            cmbLangue.SelectedIndex = i;
                            break;
                        }

                        i++;
                    }
                }
            }
        }

        private void detail_click(object sender, RoutedEventArgs e)
        {
            ProduitModel produitSelect = ((Button)sender).CommandParameter as ProduitModel;
            DeataiProduit vf = new DeataiProduit(produitSelect );
           // vf.Owner = Application.Current.MainWindow;
            vf.ShowDialog();
        }

        private void btnDesignationFilter_Click(object sender, RoutedEventArgs e)
        {
            productFilters = this._viewmodel.ProductFilters;
            viewSource.Filter += viewSource_Filter;
            viewSource.Source = _viewmodel.ProduitList;
            lstProduits.ItemsSource = productFilters;


            popDesignation.IsOpen = true;
        }

        private void viewSource_Filter(object sender, FilterEventArgs e)
        {
            ProduitModel prod = (ProduitModel)e.Item;

            int count = productFilters.Where(w => w.IsChecked).Count(w => w.Item == prod.Libelle);

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
    }
}
