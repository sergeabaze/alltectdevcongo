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
using System.Data;
using AllTech.FacturationModule.Report;
using System.Windows.Controls.Primitives;
using AllTech.FrameWork.Global;
using AllTech.FrameWork.Utils;
using AllTech.FrameWork.Views;
using Infragistics.Controls.Grids;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.DataPresenter.ExcelExporter;

namespace AllTech.FacturationModule.Views
{
    public delegate void RefreshDataViewDelegate(int idSite);
    /// <summary>
    /// Interaction logic for Facturation_List.xaml
    /// </summary>
    
    public partial class Facturation_List : UserControl
    {
        FacturationListeViewModel localViewmodel;
        static FacturationListeViewModel localViewmodels;
        int idsocieteCourant = 0;
        private CollectionViewSource viewSource = new CollectionViewSource();
        FactureModel newFacture = null;
        DataRowView newFactureRow = null;
        DataRowView factureRowSelected = null;
        Window localWindow;
        bool isloading;

        public Facturation_List(Window windowParent)
        {
            InitializeComponent();

            FacturationListeViewModel viewModel = new FacturationListeViewModel(this );
            localViewmodel = viewModel;
            localViewmodels = viewModel;
            this.DataContext = viewModel;
            localWindow = windowParent;
            double with = SystemParameters.WorkArea.Width;

            historicGrid.Height = GlobalDatas.mainHeight - 400; //((GlobalDatas.mainHeight * 0.45));
            navBar.Height = historicGrid.Height+17;
            navBar.Width = GlobalDatas.mainWidth * 0.17;
           
            
            EventArgs e = new EventArgs();
           
            EventHSTList.EventRefreshList += new EventHSTList.MyEventHandler(Refresh);
           SizeChanged+=new SizeChangedEventHandler(Facturation_List_SizeChanged);
           isloading = false;
        }

        private void historicGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // FactureModel newFacture = this.historicGrid.ActiveItem  as FactureModel;
            //DataRowView rowFacture = this.historicGrid.ActiveItem as DataRowView;
           // Int64 id =Convert.ToInt32 ( rowFacture.Row ["ID"]);// newFacture.IdFacture;
            //string numfaf = rowFacture.Row ["Numero_Facture"].ToString(); //newFacture.NumeroFacture;
        }

      

        private void Facturation_List_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (isloading)
            {
                double h = GlobalDatas.mainHeight;

                if (e.HeightChanged)
                {
                    if (e.PreviousSize.Height < e.NewSize.Height)
                        historicGrid.Height = e.NewSize.Height - 270;
                    else
                        historicGrid.Height = e.NewSize.Height - 250;
                }

                navBar.Height = historicGrid.Height+17;
                navBar.Width = GlobalDatas.mainWidth * 0.20;
             
            }
            else
            {
                //historicGrid.Height = e.NewSize.Height - 270;
                isloading = true;
            }
        }

        private void historicGrid_ActiveCellChanging(object sender, ActiveCellChangingEventArgs e)
        {

        }

     

        private void historicGrid_CellClicked(object sender, CellClickedEventArgs e)
        {
            //factureRowSelected = e.Cell.Row.Data as DataRowView ;
            //var dop = e.Cell.Column.Key;
            //e.Cell.Row.IsActive = true; ;


        }

     

        private void historicGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           
          
            e.Handled = true;
        }

        private void facturePrint_click(object sender, RoutedEventArgs e)
        {
            Cursor curser=Cursors.Wait  ;
           

            FactureModel factureSelect = ((Button)sender).CommandParameter as FactureModel;

            DataTable tclient = ReportModel.GetReportClient(factureSelect.IdClient);
            DataTable tableSociete = ReportModel.GetReportSociete(factureSelect.IdSite);
            DataTable tablePiedPage = ReportModel.GetReporPiedPage(factureSelect.IdSite);
            DataTable tableLibelle = ReportModel.GetLibelle(factureSelect.CurrentClient.IdLangue);
            DataTable tablefacture = ReportModel.GetFacture(factureSelect.IdFacture);
            DataTable tableLignefacture = ReportModel.GetLigneFacture(factureSelect.IdFacture);


                //impression du brouillon

            if (factureSelect.CurrentClient.IdExonere == 170001)
                {
                    DataTable tableLignefactures = ReportModel.GetLigneFacture_nonExo(factureSelect.IdFacture);
                    formeExonereNonExoner vf = new formeExonereNonExoner(tclient, tableSociete, tablePiedPage, tablefacture, tableLignefactures, tableLibelle, 3);
                    vf.ShowDialog();
                }
                else
                {
                    int newMode;
                    if (factureSelect.CurrentClient.IdExonere == 170002)
                        newMode = 1;
                    else newMode = 2;

                    formeExonereNonExoner vf = new formeExonereNonExoner(tclient, tableSociete, tablePiedPage, tablefacture, tableLignefacture, tableLibelle, newMode);
                    vf.ShowDialog();
                }
                 curser = Cursors.None ;
         
        
        }

        private void ctxnewinvoice_Click(object sender, RoutedEventArgs e)
        {
           // newFacture = this.historicGrid.ActiveItem as FactureModel;
            if (localViewmodel.CurrentDroit.Ecriture || localViewmodel.CurrentDroit.Developpeur)
            {
                FactureModel newFacture = new FactureModel();
                newFacture.IdFacture = 0;
                NewFactureEdition view = new NewFactureEdition(null, 0, null);
                view.Owner = Application.Current.MainWindow;
                //GlobalDatas.currentFacture = null;
                view.ShowDialog();


                EventRefreshGridHistoric hst = new EventRefreshGridHistoric();

                if (view.isOperation)
                    hst.typeOperation = "new";
                else hst.facture = null;
                EventArgs e1 = new EventArgs();
                hst.OnChangeList(e1);
            }
            else
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "PROBLEME DE PRIVILEGES";
                view.ViewModel.Message = "Pas assez de privilèges pour la création des factures";
                view.ShowDialog();
            }
        }


        private void historicGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            newFacture = this.historicGrid.ActiveItem as FactureModel;
        }

        /// <summary>
        /// Affichet la facture en modale pou edition
        /// on  récurère la ligne double clickée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void historicGrid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {


            newFacture = this.historicGrid.ActiveItem as FactureModel;
            //if (newFacture == null)
            //{
            try
            {


                if (localViewmodel.CurrentDroit.Edition || localViewmodel.CurrentDroit.Developpeur)
                {
                    // if (newFacture == null)
                    newFacture = this.historicGrid.ActiveItem as FactureModel;
                    if (newFacture != null)
                    {
                        if (newFacture.IdStatut <= 14002)
                        {
                            idsocieteCourant = newFacture.IdSite;
                            NewFactureEdition edition = new NewFactureEdition(newFacture.IdFacture, newFacture.IdStatut, null);
                            edition.Owner = Application.Current.MainWindow;


                            edition.ShowDialog();

                            if (edition.isOperation)
                            {

                                if (NewFactureEdition.currentFacture != null)
                                {
                                    if (NewFactureEdition.currentFacture.IdFacture > 0)
                                    {

                                        EventRefreshGridHistoric hst = new EventRefreshGridHistoric();
                                        hst.facture = newFactureRow;
                                        hst.typeOperation = "update";
                                        EventArgs e1 = new EventArgs();
                                        hst.OnChangeList(e1);
                                        //}

                                    }
                                    else
                                    {


                                        EventRefreshGridHistoric hst = new EventRefreshGridHistoric();

                                        if (edition.facturesListes != null && edition.facturesListes.Count > 0)
                                            hst.facture = edition.facturesListes;
                                        else hst.facture = null;

                                        hst.typeOperation = "new";
                                        EventArgs e1 = new EventArgs();
                                        hst.OnChangeList(e1);
                                    }
                                }
                            }
                            newFacture = null;
                        }
                        else
                        {
                            if (localViewmodel.CurrentDroit.Developpeur || localViewmodel.CurrentDroit.Edition)
                            {


                                try
                                {
                                    if (GlobalDatas.IsArchiveSelected)
                                    {
                                        
                                            DataSet tablefacture = ReportModel.GetFacture_archive(newFacture.IdFacture, newFacture.IdSite);
                                            DataTable tclient = ReportModel.GetReportClientArchive(newFacture.IdClient, newFacture.IdFacture);
                                            DataTable tableSociete = ReportModel.GetReportSocieteArchive(newFacture.IdSite);
                                            DataTable tablePiedPage = ReportModel.GetReporPiedPage(newFacture.IdSite);
                                            DataTable tableLibelle = ReportModel.GetLibelleArchive(newFacture.IdClientLangue);

                                            //DataTable tableLignefacture = ReportModel.GetLigneFacture(FactureSelected.IdFacture);
                                            ModalReport view = new ModalReport(tablefacture.Tables[0], tablefacture.Tables[1], tclient, tableSociete, tablePiedPage, tableLibelle);
                                            view.ShowDialog();
                                      
                                    }
                                    else
                                    {
                                        string mode = string.Empty;
                                        if (newFacture.IdStatut >= 14003)
                                        {

                                            DataSet tablefacture = ReportModel.GetFactureNew(newFacture.IdFacture, newFacture.IdSite, (newFacture.IdStatut == 14007 ? 1 : 0));

                                            DataTable tclient = ReportModel.GetReportClientArchive(newFacture.IdClient, newFacture.IdFacture);
                                            DataTable tableSociete = ReportModel.GetReportSocieteArchive(newFacture.IdSite);
                                            DataTable tablePiedPage = ReportModel.GetReporPiedPage(newFacture.IdSite);
                                            DataTable tableLibelle = ReportModel.GetLibelleArchive(newFacture.IdClientLangue);


                                            ModalReport view = new ModalReport(tablefacture.Tables[0], tablefacture.Tables[1], tclient, tableSociete, tablePiedPage, tableLibelle);
                                            view.ShowDialog();



                                        }
                                        else
                                        {
                                            DataTable tclient = ReportModel.GetReportClient(newFacture.IdClient);
                                            DataTable tableSociete = ReportModel.GetReportSociete(newFacture.IdSite);
                                            DataTable tablePiedPage = ReportModel.GetReporPiedPage(newFacture.IdSite);
                                            DataTable tableLibelle = ReportModel.GetLibelle(newFacture.CurrentClient.IdLangue);
                                            DataTable tablefacture = ReportModel.GetFacture(newFacture.IdFacture);

                                            if (newFacture.CurrentClient.Exonerere == null)
                                            {
                                                ExonerationModel exo = new ExonerationModel();
                                                mode = exo.EXONERATION_SELECTById(newFacture.CurrentClient.IdExonere).CourtDesc;
                                            }
                                            else mode = newFacture.CurrentClient.Exonerere.CourtDesc;


                                            if (mode == "part")
                                            {
                                                DataTable tableLignefactures = ReportModel.GetLigneFacture_nonExo(newFacture.IdFacture);
                                                formeExonereNonExoner vf = new formeExonereNonExoner(tclient, tableSociete, tablePiedPage, tablefacture, tableLignefactures, tableLibelle, 3);
                                                vf.ShowDialog();
                                            }
                                            else
                                            {
                                                DataTable tableLignefacture = ReportModel.GetLigneFacture(newFacture.IdFacture);
                                                ModalReport view = new ModalReport(tablefacture, tableLignefacture, tclient, tableSociete, tablePiedPage, tableLibelle);
                                                // formeExonereNonExoner vf = new formeExonereNonExoner(tclient, tableSociete, tablePiedPage, tablefacture, tableLignefacture, tableLibelle, 1);
                                                view.ShowDialog();
                                            }
                                        }
                                    }


                                }
                                catch (Exception ex)
                                {
                                    CustomExceptionView view = new CustomExceptionView();
                                    view.Owner = Application.Current.MainWindow;
                                    view.ViewModel.Message = ex.Message;
                                    view.ShowDialog();

                                    Utils.logUserActions(string.Format("<-- interface historique factures; Erreur Impression  -- {0}  ", ex.Message), "");

                                }
                            }
                            else
                            {
                                CustomExceptionView view = new CustomExceptionView();
                                view.Owner = Application.Current.MainWindow;
                                view.Title = "PROBLEME DE PRIVILEGES";
                                view.ViewModel.Message = "Pas assez de privilèges pour Visualiser cette factures valider";
                                view.ShowDialog();
                            }
                        }


                    }


                }
                else
                {


                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = "PROBLEME DE PRIVILEGES";
                    view.ViewModel.Message = "Pas assez de privilèges pour editer cette factures";
                    view.ShowDialog();
                }
                newFacture = null;
                this.historicGrid.ActiveItem = null;



            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "PROBLEME CHARGEMENT DONNEES REFERENCES";
                view.ViewModel.Message = "Problème Inconnu Survenu lors du Chargement de Cette Facture \r vérifier ses donnéees de refences associées(objet/exploitation) \r si le problème persite, veuillez contacter l'administrateur \r((" + ex.Message+")";
                view.ShowDialog();

                if (newFacture != null)
                    Utils.logUserActions("Erreur lors du chargement facture : " + newFacture.NumeroFacture + "" + ex.Message, "");
                newFacture = null;
            }
          
           
           
        }

     

        private void lstClintNonfact_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (localViewmodel.CurrentDroit.Edition || localViewmodel.CurrentDroit.Super || localViewmodel.CurrentDroit.Developpeur || localViewmodel.CurrentDroit.Proprietaire)
            {
                ClientModel clientSelect = lstClintNonfact.SelectedItem as ClientModel;
                if (clientSelect != null)
                {
                    NewFactureEdition edition = new NewFactureEdition(null, 0, clientSelect);
                    edition.Owner = Application.Current.MainWindow;
                    edition.ShowDialog();


                    EventRefreshGridHistoric hst = new EventRefreshGridHistoric();
                  
                    if (edition.isOperation)
                        hst.typeOperation = "new";
                    else hst.facture = null;
                    EventArgs e1 = new EventArgs();
                    hst.OnChangeList(e1);

                }
            }
        }


        public static void Refresh(object sender, EventArgs e)
        {
            FactureModel newFactures = new FactureModel();

            if (NewFactureEdition.currentFacture != null && NewFactureEdition.currentFacture.IdFacture >=0)
            {
                localViewmodels.RefreshcacheFacturesListeRecherche = newFactures.FACTURE_GETLISTE(localViewmodels.societeCourante.IdSociete);
            }
            
           
            
        }

        private void chkitems_Checked(object sender, RoutedEventArgs e)
        {
           // var facture = (((ListViewItem)sender).Content as FactureModel);
            var checkBox = sender as CheckBox;
            var  idFacture = checkBox.Content;
            this.localViewmodel.FactureSelected = this.localViewmodel.FacturesListe.FirstOrDefault(f => f.IdFacture == (idFacture!=null  ?long .Parse(idFacture.ToString ()):0));
        }


        private void SortableListViewColumnHeaderClicked(object sender, RoutedEventArgs e)
        {
            //((System.Windows.Controls.SortableListView)sender).GridViewColumnHeaderClicked(e.OriginalSource as GridViewColumnHeader);
        }

        private void mnFacture_Click(object sender, RoutedEventArgs e)
        {
          // popUpFacture.IsOpen = true;
        }

        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUnselectAll_Click(object sender, RoutedEventArgs e)
        {

        }



        private void mnClient_Click(object sender, RoutedEventArgs e)
        {
          // popUpClient.IsOpen = true;
        }



        private void mnDate_Click(object sender, RoutedEventArgs e)
        {
          //popUpDate.IsOpen = true;
        }

        private void popUpDate_Opened(object sender, EventArgs e)
        {

        }

        private void Show_PopupToolTip(object sender, MouseEventArgs e)
        {
            ListViewItem listViewItem = e.Source as ListViewItem;
            FactureModel items = listViewItem.Content as FactureModel;
            //MyFirstPopupTextBlock.Text = items.NumeroFacture;
            //MyToolTip.PlacementTarget = listViewItem;
            //MyToolTip.Placement = PlacementMode.MousePoint;
            //MyToolTip.IsOpen = true;
        }

        private void Hide_PopupToolTip(object sender, MouseEventArgs e)
        {
            //MyToolTip.IsOpen = false;
        }


        private void btnSelectAllClient_Click(object sender, RoutedEventArgs e)
        {
            // liste tous les clients
            this.localViewmodel.RefreshListe();
        }

        private void btnUnselectAllClient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ApplyclientFilters(object sender, RoutedEventArgs e)
        {
            var cli = (CheckBox)sender as CheckBox;
            string  decli = cli.Content.ToString ();
            localViewmodel.checkFilterClient(decli);
           
        }

        private void UnApplyclientFilters(object sender, RoutedEventArgs e)
        {
            var cli = (CheckBox)sender as CheckBox;
            string decli = cli.Content.ToString();
            localViewmodel.uncheckFilterClient(decli);

           

        }

     

        private void cmbClieListe_SelectionChanged(object sender, EventArgs e)
        {
            this.localViewmodel.ClientFactireselect = cmbClieListe.SelectedItem as ClientModel;
        }

        private void cmbClieListeArch_SelectionChanged(object sender, EventArgs e)
        {
            this.localViewmodel.ClientFactireselect = cmbClieListeArch.SelectedItem as ClientModel;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newh = SystemParameters.WorkArea.Height ;
            double actuahei = this.ActualHeight;
           // double hh = Application.Current.MainWindow.SizeToContent;
            this.SizeChanged+=new SizeChangedEventHandler(Facturation_List_SizeChanged);

        }
        protected void Facturation_List_SizeChanged(object sende, EventArgs e)
        {
           
        }

        private void chkitems_Checked_1(object sender, RoutedEventArgs e)
        {
            var facture = this.historicGrid.ActiveItem as FactureModel;


            if (facture != null)
            {
                this.localViewmodel.FactureSelected = facture;

                this.localViewmodel.FacturesListe.FirstOrDefault(f => f.IdFacture == facture.IdFacture).IsCheck = true;
            }

        }

      

        private void chkitems_Unchecked(object sender, RoutedEventArgs e)
        {

            var facture = this.historicGrid.ActiveItem as FactureModel;

            if (facture != null)
            {
                if (this.localViewmodel.FacturesListe != null && this.localViewmodel.FacturesListe.Count > 0)
                    if (this.localViewmodel.FacturesListe.Any(r => r.IdFacture == facture.IdFacture))
                      this.localViewmodel.FacturesListe.FirstOrDefault(r => r.IdFacture == facture.IdFacture).IsCheck = false;
                this.localViewmodel.FactureSelected = null;
            }
        }

     

       
        public void StartStopWait(bool values)
        {
          
            LoadingAdorner.IsAdornerVisible = values;
            
        }

        private void btnvalidation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExportToexcelSelect_Click(object sender, RoutedEventArgs e)
        {
            DataPresenterExcelExporter excelExporter = new DataPresenterExcelExporter();
           // excelExporter.
           // XamGridExcelExporter excelExporter = new XamGridExcelExporter();
            var facts = historicGrid.Visibility = Visibility.Visible;
           // historicGrid.FieldLayouts[0].Fields[fieldName].Visibility = Visibility.Visible;
           // int d = facts.Count;

        }

        private void historicGrid_Filtered(object sender, FilteredEventArgs e)
        {

        }

        private void btnInprimparam_Click(object sender, RoutedEventArgs e)
        {
            System.Drawing.Printing.PrintDocument printDocument1 = new System.Drawing.Printing.PrintDocument();
            System.Windows.Forms.PrintDialog printDialog1 = new System.Windows.Forms.PrintDialog();
            printDialog1.Document = printDocument1;
            System.Windows.Forms.DialogResult dr =printDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                //savegarde
                int nCopy = printDocument1.PrinterSettings.Copies;
                //Get the number of Start Page
                int sPage = printDocument1.PrinterSettings.FromPage;
                //Get the number of End Page
                int ePage = printDocument1.PrinterSettings.ToPage;
                //Get the printer name
                GlobalDatas.printerName = printDocument1.PrinterSettings.PrinterName;
                //string PrinterName = printDocument1.PrinterSettings.PrinterName;
               // crReportDocument.PrintToPrinter(nCopy, false, sPage, ePage);
            }
                
                
    


        }


      
    }
}
