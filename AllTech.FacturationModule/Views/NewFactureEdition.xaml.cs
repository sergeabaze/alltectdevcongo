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
using AllTech.FrameWork.Views;
using AllTech.FrameWork.Global;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Threading;
using System.Threading;

namespace AllTech.FacturationModule.Views
{
    public delegate void MyEventHandler(EventArgs e);
    /// <summary>
    /// Interaction logic for NewFactureEdition.xaml
    /// </summary>
    public partial class NewFactureEdition : Window
    {
        Facture_editionViewModel localViewmodel;
        public static  FactureModel currentFacture;
        public FactureModel newCurrentFacture;
        public FactureModel newCurrentFactureFromShell;
        public List<FactureModel> facturesListes = new List<FactureModel>();
        public bool isOperation;
        LigneFactureModel lignefacture = null;
        static bool isvaluesAddAutocomple=false ;
        double mainHeight = 0;
        double mainwidth = 0;
        public static event MyEventHandler eventJourLimitAction;
        private DispatcherTimer timerLimiteJourF;



        public NewFactureEdition( Int64?  IdFacture,int idStatut,ClientModel client)
        {
            InitializeComponent();

            if (IdFacture != null)
                currentFacture = GetFactureByID(IdFacture, idStatut);
            else
            {
                if (client != null)
                {
                    currentFacture = new FactureModel();
                    currentFacture.IdClient = client.IdClient;
                    // met li du prduit
                    currentFacture.IdDepartement = client.IdTerme;
                    currentFacture.IdSite = client.IdSite;
                }
                else
                    currentFacture = new FactureModel();
            }
            
           // currentFacture = IdFacture != null ? GetFactureByID(IdFacture, idStatut) : new FactureModel();

            Facture_editionViewModel viewModel = new Facture_editionViewModel(currentFacture);
            localViewmodel = viewModel;
            this.DataContext = viewModel;
            double modalWidth = GlobalDatas.mainWidth * 0.90;
            double modalHeight = GlobalDatas.mainHeight * 0.85;
            Communicator.eventjourLimite += new Communicator.MyEventHandler(Communicator_eventjourLimite);
            Communicator.eventCleartxtqty += new Communicator.MyEventHandler(Communicator_eventCleartxtqty);
            Communicator.eventCloseWindow+=new Communicator.MyEventHandler(Communicator_eventCloseWindow);

            if (GlobalDatas.mainHeight >= GlobalDatas.mainMaxHeight)
            {
                borderCustomer.Width = modalWidth * 0.32;
                borderStatus.Width = modalWidth * 0.30;
                borderInvoice.Width = modalWidth * 0.33;


            }
            else
            {
                borderCustomer.Width = modalWidth * 0.35;
                borderStatus.Width = modalWidth * 0.29;
                borderInvoice.Width = modalWidth * 0.34;
                //Items.Height = modalHeight * 0.35;
            }

            Items.Height = modalHeight - 509;

            cmbClient.Width = (modalWidth * 0.32) / 2;
            txtInvoice.Width = (modalWidth * 0.33) - 20;


            this.Width = modalWidth;
            this.Height = modalHeight;
         

            Closing += new System.ComponentModel.CancelEventHandler(NewFactureEdition_Closing);
          
         
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void NewFactureEdition_Closing(object sender, CancelEventArgs e)
        {

        }

        protected void Communicator_eventCleartxtqty(object sender, EventArgs e)
        {
            txtQty.Text = string.Empty;
            txtOther.Text = string.Empty;
        }

        protected void Communicator_eventCloseWindow(object sender, EventArgs e)
        {
            this.Close();
        }

        //

        protected void Communicator_eventjourLimite(object sender, EventArgs e)
        {
            Communicator com = sender as Communicator;


            txtJourLimiteMsg.Text = com.Message;
            timerLimiteJourF = new DispatcherTimer();
            timerLimiteJourF.Interval = TimeSpan.FromSeconds(20);
            timerLimiteJourF.Tick += new EventHandler(timerLimiteJourF_Tick); ;
            timerLimiteJourF.Start();


            //for (int i = 0; i < 50; i += 5)
            //    Thread.Sleep(100);

            popPouPmesageJourlimite.IsOpen = true;

        }

        private void timerLimiteJourF_Tick(object sender, EventArgs e)
        {
            timerLimiteJourF.Stop();
            //this.Close();
            popPouPmesageJourlimite.IsOpen = false;
        }

        FactureModel GetFactureByID(Int64? IDFacture, int idstatut)
        {
            FactureModel facture = null;
            try
            {
                if (GlobalDatas.IsArchiveSelected)
                {
                    facture = new FactureModel().GET_FACTURE_BYID_Archive((Int64)IDFacture, GlobalDatas.DefaultCompany.IdSociete);
                }
                else
                {
                    if (idstatut >= 14003 )
                       facture = new FactureModel().GET_FACTUREVALIDE_BYID((Int64)IDFacture, GlobalDatas.DefaultCompany.IdSociete, 0);
                    else
                        facture = new FactureModel().GET_FACTURE_BYID((Int64)IDFacture);
                }
                return facture;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "PROBLEME DE CHARGEMENT FACTURE";
                view.ViewModel.Message = "Problème survenu lors du chargement de la facture courante";
                view.ShowDialog();
                Utils.logUserActions("<-- interface edition facture --Erreure chargement Facture    " + ex.Message, "");
                return null;
            }
        }


        #region Comboboxs Events

   

        private void cmbClient_SelectionChanged(object sender, EventArgs e)
        {
            ClientModel client = cmbClient.SelectedItem as ClientModel;
            if (client!=null )
            this.localViewmodel.ClientSelected = cmbClient.SelectedItem as ClientModel ;
            this.localViewmodel.IsVisibleButtonCancel = true ;
        }


        private void cmbClient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                e.Handled = true;
               // cmbClient.Items.MoveCurrentToNext();
            }
        }

        private void cmbObjet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != cmbObjet.SelectedItem)
            {
                ObjetFactureModel cbItem = cmbObjet.SelectedItem as ObjetFactureModel;
                // string val = cbItem.Content.ToString();
                if (cbItem.IdObjet > 0)
                {
                    txtObjet.Text = string.Empty;
                    localViewmodel.FatureCurrent.Label_objet = string.Empty;
                }
                else
                {
                    txtObjet.Text = localViewmodel.FatureCurrent.Label_objet;
                }
            }
        }

        private void cmbObjet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                e.Handled = true;
               // cmbObjet.Items.MoveCurrentToNext();
            }
        }

        private void cmbexploit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                e.Handled = true;
                //cmbexploit.Items.MoveCurrentToNext();
            }
        }

        private void cmbexploit_KeyDown_1(object sender, KeyEventArgs e)
        {

        }


        private void cmbexploit_SelectionChanged(object sender, EventArgs e)
        {
            ExploitationFactureModel exploitation = cmbexploit.SelectedItem as ExploitationFactureModel;
            if (exploitation != null)
                this.localViewmodel.ExploitationSelected = exploitation;
        }

        private void cmbObjet_SelectionChanged(object sender, EventArgs e)
        {
            if (null != cmbObjet.SelectedItem)
            {
                ObjetFactureModel cbItem = cmbObjet.SelectedItem as ObjetFactureModel;
                // string val = cbItem.Content.ToString();
                this.localViewmodel.ObjetSelected = cbItem;
                if (cbItem.IdObjet > 0)
                {
                    txtObjet.Text = string.Empty;
                    localViewmodel.FatureCurrent.Label_objet = string.Empty;
                }
                else
                {
                    txtObjet.Text = localViewmodel.FatureCurrent.Label_objet;
                }
            }
        }

        private void cmbDep_SelectionChanged(object sender, EventArgs e)
        {
            DepartementModel dep = cmbDep.SelectedItem as DepartementModel;
            if (dep != null)
                this.localViewmodel.Depselected = dep;
        }

        private void cmbCosTaxe_SelectionChanged(object sender, EventArgs e)
        {
            TaxeModel taxe = cmbCosTaxe.SelectedItem as TaxeModel;
            if (taxe != null)
            {
                this.localViewmodel.CostTaxeSelected = taxe;

            }
        }

        private void cmbTaxedefaut_SelectionChanged(object sender, EventArgs e)
        {
            TaxeModel taxe = cmbTaxedefaut.SelectedItem as TaxeModel;
            if (taxe!=null )
            this.localViewmodel.TaxeSelected = taxe;
        }


        private void cmbChoice_SelectionChanged(object sender, EventArgs e)
        {
            ProduitModel prod = cmbChoice.SelectedItem as ProduitModel;
            if (prod != null)
                this.localViewmodel.ProduitSelected = prod;
        }


        #endregion


        private void txtObjet_LostFocus(object sender, RoutedEventArgs e)
        {
            if (localViewmodel.FatureCurrent != null)
                localViewmodel.FatureCurrent.Label_objet = txtObjet.Text;
        }

        private void txtQty_LostFocus(object sender, RoutedEventArgs e)
        {
            float result = 0;

            string currentqty = string.Empty;
            if (txtQty.Text != string.Empty)
            {
                //if (float.TryParse(txtQty.Text, out result))
                localViewmodel.NewQtySelect = txtQty.Text;
            }
        }

   

        #region DATA GRID EVENTS
        
      
     
       
        /// <summary>
        /// Mise jour dune ligne de facture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Items_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var items = this.Items.ActiveItem  as LigneCommand;
            if (items != null)
            {
                this.localViewmodel.IsdblClickoperation = true;
                LigneFactureModel newLine = new LigneFactureModel();
                newLine.Description = items.Description;
                txtOther.Text = items.Description;
                newLine.Quantite = items.quantite;
                newLine.IdProduit = items.IdProduit;
                newLine.IdDetailProduit = items.Idetail;
                newLine.IdLigneFacture = items.ID;
                newLine.PrixUnitaire = items.PrixUnit;
                newLine.MontanHT = items.montantHt;
                newLine.Exonere = items.estExonere;
                newLine.IdSite = items.IdSite;
                newLine.SpecialFacture = items.SpecialMode;

                this.localViewmodel.CurOldQty = (double)items.quantite;
                this.localViewmodel.CuroldPu = (double)items.PrixUnit;
                DetailProductModel det = null;
                if (localViewmodel.NewDetailListeProduit == null)
                {
                    DetailProductModel detailservice = new DetailProductModel();
                    det = detailservice.DETAIL_PRODUIT_GETBYID(items.Idetail);
                }
                else
                {
                    det = localViewmodel.NewDetailListeProduit.Find(p => p.IdDetail == items.Idetail);
                }
                if (det != null)
                {
                    this.localViewmodel.CurIsExonere = det.Exonerer;
                    this.localViewmodel.CurIsProrata = det.Isprorata;
                }
                else
                {
                    if (localViewmodel.NewDetailListeProduit.Count == 1)
                    {
                        this.localViewmodel.CurIsExonere = localViewmodel.NewDetailListeProduit[0].Exonerer;
                        this.localViewmodel.CurIsProrata = localViewmodel.NewDetailListeProduit[0].Isprorata;
                    }

                }

                int i = 0;
                foreach (var val in localViewmodel.CacheProduiList)
                {
                    if (val.IdProduit == items.IdProduit)
                    {
                        cmbChoice.SelectedIndex = i;
                        break;
                    }
                    i++;
                }

                ProduitModel prod = new ProduitModel();

                prod.IdProduit = items.IdProduit;
                prod.PrixUnitaire = (decimal)items.PrixUnit;
                prod.Libelle = items.Produit;
                this.localViewmodel.OldProduitSelected = prod;
                this.localViewmodel.OldLigneFacture = newLine;

                this.localViewmodel.IsdblClickoperation = false;
            }

        }

        private void txtMontant_MouseLeave(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                localViewmodel.AfficheResume = "";
            }
        }

      

        /// <summary>
        ///  affiche le total net en en bas d ligne de facture 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMontant_MouseDown(object sender, MouseButtonEventArgs e)
        {
            decimal  newval = 0;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var value = ((TextBlock)sender).Text;
                foreach (var ligne in localViewmodel.LigneCommandList)
                {
                    newval += ligne.montantHt;
                    value = value.ToString().Replace(".00", " ").Replace(",", " ").Trim();
                    if (ligne.montantHt == (decimal)Common.GetDoule(value))
                        break;
                }

                localViewmodel.AfficheResume = string.Format("{0:#,##}", newval);

            }
        
        }


      

        #endregion


        #region CLICK EVENTS

        /// <summary>
        ///  ajout nouvelle ligne ou mise jour ligne commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddgrid_Click(object sender, RoutedEventArgs e)
        {
            localViewmodel.LigneFacture.Description = txtOther.Text;
            txtOther.Text = string.Empty;
            cmbChoice.SelectedIndex = -1;
        }

        private void btnValide_Click(object sender, RoutedEventArgs e)
        {
            if (localViewmodel.FatureCurrent != null)
                localViewmodel.FatureCurrent.Label_objet = txtObjet.Text;
            currentFacture = localViewmodel.FatureCurrent;
        }
   
        private void cmdnewfacture_Click(object sender, RoutedEventArgs e)
        {
            localViewmodel.LigneFatureListe = null;
            currentFacture = null;
            cmbChoice.SelectedIndex = -1;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
           // bool valeur = UserInterfaceUtilities.ValidateVisualTree(this);

            if (localViewmodel.IsOperationClosing  == true)
            {
                StyledMessageBoxView messageBox = new StyledMessageBoxView();
                messageBox.Owner = Application.Current.MainWindow;
                messageBox.Title = " INFORMATION FERMETURE FORMULAIRE";
                messageBox.ViewModel.Message = "Ce formulaire contient des donnés voulez vous fermer ?";
                if (messageBox.ShowDialog().Value == true)
                {
                    //if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
                    //{
                   // this.DialogResult = true;
                    this.Close();
                    //}
                }
            }
            else
            {
                //if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
                //{
               // this.DialogResult = true;
                this.Close();
            }
        }

        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isOperation = localViewmodel.IsOperation;
            currentFacture = null;
            newCurrentFactureFromShell = localViewmodel.FatureCurrent;
            facturesListes = localViewmodel.TotalFacturesCreer;
            if (localViewmodel.IsOperation)
            {
                if (localViewmodel.IsnewFactureEdite || localViewmodel.isDeleteFacture)
                {
                    // if new invoice
                    currentFacture = new FactureModel { IdFacture = 0 };
                    newCurrentFacture = localViewmodel.FatureCurrent;
                }
                else // update invoice
                {
                    currentFacture = localViewmodel.FatureCurrent;

                  
                }

            }
           // else
                //currentFacture = new FactureModel { IdFacture=-1 };
            if (localViewmodel.FatureCurrent!=null)
            if (localViewmodel.FatureCurrent.CurrentStatut != null)
            {
                if (int.Parse(localViewmodel.FatureCurrent.CurrentStatut.CourtDesc) == 1 || int.Parse(localViewmodel.FatureCurrent.CurrentStatut.CourtDesc) == 2)
                    localViewmodel.FatureCurrent.DateCloture = null;
            }

            Utils.logUserActions("*******  Fin de traitement de la facture ****************", "");
           
        }

        private void btnCancelCustomel_Click(object sender, RoutedEventArgs e)
        {
            if (this.localViewmodel.ModeFactureAvoirEnable)
            {
                this.localViewmodel.ModeFactureAvoir = true;
                this.localViewmodel.ModeFacturenormale = false;
            }

          
        }

        private void toggleSwitch3_Checked(object sender, RoutedEventArgs e)
        {
            this.localViewmodel.ModeFacturenormale = true;
            this.localViewmodel.ModeFactureAvoir = false;
        }

        private void toggleSwitch3_Checked_1(object sender, RoutedEventArgs e)
        {
            this.localViewmodel.ModeFacturenormale = true;
            this.localViewmodel.ModeFactureAvoir = false;
        }

        private void toggleSwitch3_Unchecked(object sender, RoutedEventArgs e)
        {
            this.localViewmodel.ClientIndex = -1;
            this.localViewmodel.IsVisibleButtonCancel = false;
        }

        private void txtordername_SelectionChanged(object sender, RoutedEventArgs e)
        {
            localViewmodel.isFactureOperation = true;
        }

       

      

    }


    
}
