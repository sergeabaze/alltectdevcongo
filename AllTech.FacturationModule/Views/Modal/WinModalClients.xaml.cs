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
using AllTech.FrameWork.Model;
using AllTech.FacturationModule.ViewModel;
using AllTech.FrameWork.Utils;
using AllTech.FrameWork.Global;
using AllTech.FrameWork.Views;

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for WinModalClients.xaml
    /// </summary>
    public partial class WinModalClients : Window
    {
        CompteModalClientViewModel localViewModel = null;
        ClientModel localClient;
        public WinModalClients(ClientModel client)
        {
            InitializeComponent();
          
           CompteModalClientViewModel viewModel = new CompteModalClientViewModel(this, client);
            localClient=client;
            this.DataContext = viewModel;
            localViewModel = viewModel;
             viewModel.ClientSelected = client;
             this.Height = GlobalDatas.mainHeight - 250;// *0.83; //- 200;
             this.Width = GlobalDatas.mainWidth - 400;// * 0.90; // - 600;

             tabControlHedader.Height = this.Height-150;
             tabControlObject.Height = this.Height - 150;
             tabControlExploitation.Height = this.Height - 150;

             gridobjet.Height = tabControlObject.Height - 20;
             gridExploitation.Height = tabControlExploitation.Height-20;
         
           

           // LoadingDatas();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            tabControlHedader.Height = this.Height - 150;
            tabControlObject.Height = this.Height - 150;
            tabControlExploitation.Height = this.Height - 150;

            gridobjet.Height = tabControlObject.Height - 20;
            gridExploitation.Height = tabControlExploitation.Height - 20;
        }


        void LoadingDatas()
        {
            int i = 0;
            int j = 0;
            int ee = 0;
            int d = 0;
            int dd = 0;
            int idgene = 0;

            if (this.localViewModel.LibelleList.Count > 0)
                        {
                            int ter = 0;
                            if (this.localViewModel.LibelleList != null)
                            {
                                foreach (var val in this.localViewModel.LibelleList)
                                {
                                    if (val.ID ==localClient.IdTerme)
                                    {
                                       cmblibelleTerme.SelectedIndex = ter;
                                        //cmbTerme.SelectedIndex = 1;
                                        break;
                                    }

                                    ter++;
                           }
                      }
               }
                    


            //if (this.localViewModel.LanguageList != null)
            //{
            //    foreach (var langue in this.localViewModel.LanguageList)
            //    {
            //        if (langue.Id == this.localViewModel.ClientSelected.IdLangue)
            //        {
            //             cmbLangue.SelectedIndex = i;
            //            break;
            //        }

            //        i++;
            //    }
            //}

            //if (this.localViewModel.ExonerateList != null)
            //{
            //    foreach (var val in this.localViewModel.ExonerateList)
            //    {
            //        if (val.ID == this.localViewModel.ClientSelected.IdExonere)
            //        {
            //             cmbexonere.SelectedIndex = j;
            //            break;
            //        }

            //        j++;
            //    }
            //}
            ////

            //if (this.localViewModel.DeviseList != null)
            //{
            //    foreach (var val in this.localViewModel.DeviseList)
            //    {
            //        if (val.ID_Devise == this.localViewModel.ClientSelected.IdDeviseFact)
            //        {
            //             cmbDevise.SelectedIndex = ee;
            //            break;
            //        }

            //        ee++;
            //    }
            //}

            //if (this.localViewModel.TaxePorataList != null)
            //{
            //    foreach (var val in this.localViewModel.TaxePorataList)
            //    {
            //        if (val.ID_Taxe == this.localViewModel.ClientSelected.Idporata)
            //        {
            //             cmbPorata.SelectedIndex = d;
            //            break;
            //        }
            //        if (this.localViewModel.ClientSelected.Idporata == 0)
            //        {
            //             cmbPorata.SelectedIndex = -1;
            //            break;
            //        }

            //        d++;
            //    }
            //}

            //if (this.localViewModel.CompteList != null)
            //{
            //    foreach (var val in this.localViewModel.CompteList)
            //    {
            //        if (val.ID == this.localViewModel.ClientSelected.IdCompte)
            //        {
            //             cmbCompte.SelectedIndex = dd;
            //            break;
            //        }

            //        dd++;
            //    }
            //}

            //int ter = 0;
            //if (this.localViewModel.LibelleList != null)
            //{
            //    foreach (var val in this.localViewModel.LibelleList)
            //    {
            //        if (val.ID == this.localViewModel.ClientSelected.IdTerme)
            //        {
            //              cmbTerme.SelectedIndex = ter;
            //            break;
            //        }

            //        ter++;
            //    }
            //}
        }

        //private void cmblibelleTerme_SelectionChanged(object sender, EventArgs e)
        //{
        //    LibelleTermeModel terme = cmblibelleTerme.SelectedItem as LibelleTermeModel;
        //    if (terme != null)
        //        localViewModel.LibelleSelected = terme;
        //}

        private void cmbLangue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             int val=  cmbLangue.SelectedIndex;

        }



        private void cmbLangue_SelectionChanged(object sender, EventArgs e)
        {
            LangueModel langue = cmbLangue.SelectedItem as LangueModel;
            if (langue != null)
                localViewModel.LanguageSelected = langue;
        }

        private void cmbexonere_SelectionChanged(object sender, EventArgs e)
        {
            ExonerationModel exonere = cmbexonere.SelectedItem as ExonerationModel;
            if (exonere != null)
                localViewModel.ExonerateSelected = exonere;

        }

        private void cmbDevise_SelectionChanged(object sender, EventArgs e)
        {
            DeviseModel devise = cmbDevise.SelectedItem as DeviseModel;
            if (devise != null)
                localViewModel.DeviseSelected = devise;
        }

        private void cmbDeviseFact_SelectionChanged(object sender, EventArgs e)
        {
            DeviseModel devise = cmbDeviseFact.SelectedItem as DeviseModel;
            if (devise != null)
                localViewModel.DeviseFactureSelected = devise;
        }


        private void cmbTerme_SelectionChanged(object sender, EventArgs e)
        {
            //LibelleTermeModel terme = cmbTerme.SelectedItem as LibelleTermeModel;
            //if (terme != null)
            //    localViewModel.LibelleSelected = terme;
        }

        private void cmbPorata_SelectionChanged(object sender, EventArgs e)
        {
            TaxeModel porata = cmbPorata.SelectedItem as TaxeModel;
            if (porata != null)
                localViewModel.TaxePorataSelected = porata;
        }

        private void cmbCompte_SelectionChanged(object sender, EventArgs e)
        {
            CompteModel compte = cmbCompte.SelectedItem as CompteModel;
            if (compte != null)
                localViewModel.CompteSelected = compte;
        }

      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            //{
                this.DialogResult = true;

           
              

              // rechargement
           // }
        }

        private void cmbCmTiers_SelectionChanged(object sender, EventArgs e)
        {
            CompteTiersModel compte = cmbCmTiers.SelectedItem as CompteTiersModel;
            if (compte != null)
                localViewModel.CompteTierSelected = compte;
        }

        private void chkClientActive_Checked(object sender, RoutedEventArgs e)
        {

        }

        //private void tabItem_Suivi_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    localViewModel.LoadproduitClients();
        //}

        //private void chkitems_Checked_1(object sender, RoutedEventArgs e)
        //{
        //    var produit = this.gridproduits.ActiveItem as produisuivi;


        //    if (produit != null)
        //    {
        //       // this.localViewModel.ProduiSuiviSelect = produit;
        //        produit.IsParameter = true;
        //        this.localViewModel.ListeProduitsuivis.FirstOrDefault(p => p.IDproduit == produit.IDproduit).IsParameter = true;
        //        if (!localViewModel.ListeProduitsuiviUpdate.Exists(p => p.IDproduit == produit.IDproduit))
        //        this.localViewModel.ListeProduitsuiviUpdate.Add(produit);
        //        else this.localViewModel.ListeProduitsuiviUpdate.FirstOrDefault(p => p.IDproduit == produit.IDproduit).IsParameter = true;
        //       // this.localViewModel.FacturesListe.FirstOrDefault(f => f.IdFacture == facture.IdFacture).IsCheck = true;
        //    }
        //}

        //private void chkitems_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    var produit = this.gridproduits.ActiveItem as produisuivi;
        //    if (produit != null)
        //    {
        //        produit.IsParameter = false;
        //        this.localViewModel.ListeProduitsuivis.FirstOrDefault(p => p.IDproduit == produit.IDproduit).IsParameter = false;
        //        if (this.localViewModel.ListeProduitsuiviUpdate.Exists(p=>p.IDproduit==produit.IDproduit))
        //            this.localViewModel.ListeProduitsuiviUpdate.FirstOrDefault(p => p.IDproduit == produit.IDproduit).IsParameter = false;
        //        else this.localViewModel.ListeProduitsuiviUpdate.Add(produit);

        //    }
        //}

        private void btnSaveClose_Click(object sender, RoutedEventArgs e)
        {
           
          
            this.Close();
        }

        private void btnCancelobjet_Click(object sender, RoutedEventArgs e)
        {
            object row = ((Button)sender).CommandParameter as object;
            int testc = int.Parse(row.ToString());
            try
            {
                if (row != null)
                {
                    ObjetFactureModel objetservice = new ObjetFactureModel();

                    int codeMessage = objetservice.OBJECT_FACTURE_DELETE(testc);
                    if (codeMessage == 110)
                    {

                        localViewModel.loadeObjet();
                    }
                    else
                        throw new Exception("Echec Supression, il est déja utilisé dans une facture");

                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE INFORMATION SUPPRESSION OBJET";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        private void btnCancelnom_Click(object sender, RoutedEventArgs e)
        {
            txtLibelleExploite.Text = string.Empty;
        }

        private void btnCancelExploitation_Click(object sender, RoutedEventArgs e)
        {
            object row = ((Button)sender).CommandParameter as object;
            int testc = int.Parse(row.ToString());
            ExploitationFactureModel _exploitService = new ExploitationFactureModel();
            try
            {
                if (row != null)
                {
                    int codeMessage = _exploitService.EXPLOITATION_FACTURE_DELETE(testc);
                    if (codeMessage == 110)
                        localViewModel.LoadExploitation();
                    else if (codeMessage == 111)
                        throw new Exception("Echec suppression, elle est déja utilisée dans une facture");
                     else if (codeMessage == 113)
                        throw new Exception("Echec Suppression, Un compte Analytique est rattaché ");
                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE INFORMATION SUPPRESSION EXPLOITATION";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        private void btnNewClient_Click(object sender, RoutedEventArgs e)
        {
            HeadeInfoGene.Focus();
           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }

      

      

       
    }
}
