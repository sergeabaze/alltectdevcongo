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
    /// Interaction logic for FactureInformation.xaml
    /// </summary>
    public partial class FactureInformation : Window
    {
        FactureInformationViewModel _viewModel;

        public FactureInformation( FactureInformationViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            {
                this.DialogResult = true;
            }
        }


        private void Objetfacture_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //this._viewModel.Objetselected = ((ListViewItem)sender).Content as ObjetFactureModel;
            this._viewModel.Objetselected = Objetfacture.SelectedItem  as ObjetFactureModel;

            int i = 0;
            foreach (var ob in this._viewModel.LanguageList)
            {
                if (this._viewModel.Objetselected.IdObjet == ob.Id)
                {
                    Objetfacture.SelectedIndex = i;
                    break;
                }
            }
            e.Handled = true;
        }

      

     
    }
}
