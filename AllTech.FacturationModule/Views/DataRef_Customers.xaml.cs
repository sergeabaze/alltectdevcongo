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
using System.Collections.ObjectModel;
using AllTech.FrameWork.Global;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for DataRef_Produit.xaml
    /// </summary>
    public partial class DataRef_Customers : UserControl
    {
        DatarefViewModel localViewModel;
        Window localwindow;
        bool isloading = false;
      

        public DataRef_Customers(Window window)
        {
            InitializeComponent();
            DatarefViewModel viewModel = new DatarefViewModel(window,this);
            this.DataContext = viewModel;
            localViewModel = viewModel;
            localwindow = window;
           
             isloading = false;
             optionrechechName.Width = (GlobalDatas.mainWidth * 0.15);
             groupeVisible.Width = optionrechechName.Width;

             txtNomClient.Width = optionrechechName.Width *0.70;
             txtville.Width = optionrechechName.Width*0.70;
          // produitGrid.Width = GlobalDatas.mainWidth*0.75;//(GlobalDatas.mainWidth * 0.70);
             produitGrid.Height = GlobalDatas.mainHeight-370;
             optionrechechName.Height = produitGrid.Height;
        }

      

        private void produitGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (localViewModel.CurrentDroit.Edition || localViewModel.CurrentDroit.Developpeur)
            {
                ClientModel clientSelect = this.produitGrid.ActiveItem as ClientModel;
                if (clientSelect != null)
                {
                    WinModalClients detClient = new WinModalClients(clientSelect);
                    detClient.Owner = Application.Current.MainWindow;
                    detClient.ShowDialog();
                    if (GlobalDatas.IdDataRefArchiveDatas)
                        localViewModel.loadDatasArchivesValidate();
                    else
                        localViewModel.loadDatas();
                    // this.localViewModel.loadDatas();
                }
            }
            else MessageBox.Show("Pas Assez de Privileges en edition pour cette opération","DROITS",MessageBoxButton.OK,MessageBoxImage.Hand);

        }

        private void btnNewClient_Click(object sender, RoutedEventArgs e)
        {
            if (localViewModel.CurrentDroit.Ecriture || localViewModel.CurrentDroit.Developpeur)
            {
                ClientModel client = new ClientModel();
                WinModalClients vf = new WinModalClients(client);
                vf.Owner = Application.Current.MainWindow;
                vf.ShowDialog();
            }
            else MessageBox.Show("Pas Assez de Privileges en écriture pour cette opération", "DROITS", MessageBoxButton.OK, MessageBoxImage.Hand);

        }


        private void detail_click(object sender, RoutedEventArgs e)
        {
            if (localViewModel.CurrentDroit.Edition || localViewModel.CurrentDroit.Developpeur)
            {
                ClientModel client = ((Button)sender).CommandParameter as ClientModel;
                DetailProduitClient vf = new DetailProduitClient(client);
                vf.Owner = localwindow;
                vf.ShowDialog();
            }
            else MessageBox.Show("Pas Assez de Privileges en Edition pour cette opération", "DROITS", MessageBoxButton.OK, MessageBoxImage.Hand);

        }

       

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }


     

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (isloading)
            {
                if (e.HeightChanged)
                {
                    if (e.PreviousSize.Height < e.NewSize.Height)
                        produitGrid.Height = GlobalDatas.mainHeight - 370;
                    else
                        produitGrid.Height = GlobalDatas.mainHeight - 270;
                }


                optionrechechName.Width = (GlobalDatas.mainWidth * 0.15);
                groupeVisible.Width = optionrechechName.Width;

                txtNomClient.Width = optionrechechName.Width * 0.70;
                txtville.Width = optionrechechName.Width * 0.70;
                // produitGrid.Width = GlobalDatas.mainWidth*0.75;//(GlobalDatas.mainWidth * 0.70);
              
                optionrechechName.Height = produitGrid.Height;

            // produitGrid.Height = GlobalDatas.mainHeight - 440;
            // produitGrid.Width = GlobalDatas.mainWidth - 275;
              //  optionrechechName.Width = (GlobalDatas.mainWidth * 0.15);
               // produitGrid.Width = GlobalDatas.mainWidth*0.70;//(GlobalDatas.mainWidth * 0.70);
               // produitGrid.Height = GlobalDatas.mainHeight * 0.56;
            }
            isloading=true;
                  
           
                 
        }

        public void StartStopWait(bool values)
        {
            //LoadingAdorner.IsAdornerVisible = !LoadingAdorner.IsAdornerVisible;
            LoadingAdorner.IsAdornerVisible = values;
            //GridFacture.IsEnabled = !GridFacture.IsEnabled;
        }

        private void btnCancelnom_Click(object sender, RoutedEventArgs e)
        {
            txtNomClient.Text = string.Empty;
        }

        private void btncancelVille_Click(object sender, RoutedEventArgs e)
        {
            txtville.Text = string.Empty;
        }

        private void produitGrid_RowSelectorClicked(object sender, Infragistics.Controls.Grids.RowSelectorClickedEventArgs e)
        {
            ClientModel cliet = produitGrid.ActiveItem as ClientModel;
        }

      

      
    }
}
