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
    /// Interaction logic for DeataiProduit.xaml
    /// </summary>
    public partial class DeataiProduit : Window
    {
        DetailProduitViewModel localViewModel;
        public DeataiProduit(ProduitModel produit)
        {
            InitializeComponent();
            DetailProduitViewModel viewModel = new DetailProduitViewModel(produit,this );
            this .DataContext=viewModel ;
            localViewModel = viewModel;

            //Double workHeight = parentform.Height ;
            //Double workWidth = parentform.Width ;
            //this.WindowState = WindowState.Maximized;
            //this.Top = (workHeight - this.Height) / 2;
            //this.Left = (workWidth - this.Width) / 2;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            {
                this.DialogResult = true;
            }
        }

        private void DetailView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.localViewModel.isteste = true;
            this.localViewModel.DetailProduitSelect  = ((ListViewItem)sender).Content as DetailProductModel ;

            this.localViewModel.isteste = false;
            if (this.localViewModel.DetailProduitSelect != null)
            {
                if (localViewModel.ClientList != null)
                {
                    int i = 0;
                    foreach (var val in localViewModel.ClientList)
                    {
                        if (val.IdClient == this.localViewModel.DetailProduitSelect.IdClient)
                        {
                            cmbClient.SelectedIndex = i;
                            break;
                        }

                        i++;
                    }
                }
                e.Handled = true;
            }
        }

        private void DetailView_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            this.localViewModel.isteste = true;
            this.localViewModel.isDoubleclick = true;
            this.localViewModel.DetailProduitSelect = this.DetailView.SelectedItem as DetailProductModel;
            this.localViewModel.isteste =false ;
            if (this.localViewModel.DetailProduitSelect != null) {
                if (localViewModel.ClientList != null)
                {
                    int i = 0;
                    if (this.localViewModel.DetailProduitSelect != null)
                    {
                        if (localViewModel.ClientList != null)
                        {
                            foreach (var val in localViewModel.ClientList)
                            {

                                if (val.IdClient == this.localViewModel.DetailProduitSelect.IdClient)
                                {
                                    cmbClient.SelectedIndex = i;
                                    break;
                                }

                                i++;
                            }
                        }
                    }
                }
            

           
                if (this.localViewModel.DetailProduitSelect.IdExploitation > 0)
                {
                    int j = 0;
                    if (localViewModel.ExploitationList != null)
                    {
                        foreach (var val in localViewModel.ExploitationList)
                        {

                            if (val.IdExploitation == this.localViewModel.DetailProduitSelect.IdExploitation)
                            {
                                cmbExploitation.SelectedIndex = j;
                                break;
                            }

                            j++;
                        }
                    }

                }
                else
                    cmbExploitation.SelectedIndex = -1;
                this.localViewModel.isDoubleclick = false;
        }

          
           
           
            e.Handled = true;
        }

        private void cmbClient_SelectionChanged(object sender, EventArgs e)
        {
            ClientModel client = cmbClient.SelectedItem as ClientModel;
            if (client != null)
                localViewModel.Clientselected = client;
        }

        private void cmbExploitation_SelectionChanged(object sender, EventArgs e)
        {
            ExploitationFactureModel exploit = cmbExploitation.SelectedItem as ExploitationFactureModel;
            if (exploit != null)
                localViewModel.ExploitationSelected = exploit;
        }
    }
}
