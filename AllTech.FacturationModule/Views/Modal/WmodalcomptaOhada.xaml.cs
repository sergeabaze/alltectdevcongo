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
    /// Interaction logic for WmodalcomptaOhada.xaml
    /// </summary>
    public partial class WmodalcomptaOhada : Window
    {
        ComptaOhadaViewModel localViewModel=null ;
        public WmodalcomptaOhada(Window windiw)
        {
            InitializeComponent();

            ComptaOhadaViewModel viewModel = new ComptaOhadaViewModel(this);
            this.DataContext = viewModel;
            localViewModel = viewModel;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            //{
                this.DialogResult = true;
            //}
        }

        private void cmbCmptLibelle_SelectionChanged(object sender, EventArgs e)
        {
            localViewModel.CmbCompteLibelleSelect = cmbCmptLibelle.SelectedItem as CompteLibelleOhadaModel;
        }

        private void GridOhada_SelectedRowsCollectionChanged(object sender, Infragistics.Controls.Grids.SelectionCollectionChangedEventArgs<Infragistics.Controls.Grids.SelectedRowsCollection> e)
        {
            if (GridOhada.ActiveCell != null)
            {
                var row = GridOhada.ActiveCell.Row.Data as CompteOhadaModel;
                if (row != null)
                {
                    localViewModel.CompteOhadaSelected = row;
                    for (int i = 0; i < localViewModel.CmbCompteLibelles.Count; i++)
                    {
                        if (row.IdlibelleType == localViewModel.CmbCompteLibelles[i].ID)
                        {
                            cmbCmptLibelle.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }
    }
}
