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
using AllTech.FrameWork.Model;

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for WTaxeDevises.xaml
    /// </summary>
    public partial class WTaxeDevises : Window
    {
        TaxesDeviesViewModel _viewModel;
        public bool IsAction = false;
        public WTaxeDevises()
        {
            InitializeComponent();
            TaxesDeviesViewModel viewModel = new TaxesDeviesViewModel(this);
            _viewModel = viewModel;
            this.DataContext = viewModel;
            this.Closing+=new System.ComponentModel.CancelEventHandler(WTaxeDevises_Closing);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            {
                this.DialogResult = true;
            }
        }

        private void WTaxeDevises_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsAction = _viewModel.isAction;
        }

        private void taxes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //this._viewModel.TaxeSelected = ((ListViewItem)sender).Content as TaxeModel;
            //e.Handled = true;
            this._viewModel.TaxeSelected = Taxes .SelectedItem  as TaxeModel;
        }

        //private void devise_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    this._viewModel.DeviseSelected = Taxes.SelectedItem  as DeviseModel;
        //    e.Handled = true;
        //}

        //private void language_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    this._viewModel.LanguageSelected  = ((ListViewItem)sender).Content as LangueModel ;
        //    e.Handled = true;
        //}

       

    }
}
