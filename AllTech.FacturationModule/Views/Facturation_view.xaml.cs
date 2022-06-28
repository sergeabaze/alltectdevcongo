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
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Windows.Controls.Primitives;
using AllTech.FrameWork.Utils;
using AllTech.FrameWork.Views;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for Facturation_view.xaml
    /// </summary>
    public partial class Facturation_view : UserControl
    {
        Facturation_viewModel _viewModel;
        LigneFactureModel lignefacture = null;
        public static  FactureModel currentFacture;
        static bool isvaluesAddAutocomple = false;
        public static FactureModel FatureCurrent;

        public Facturation_view(FactureModel _fatureCurrent, IRegionManager _regionManage, IUnityContainer _containe)
        {
           
            InitializeComponent();
           
            FatureCurrent = _fatureCurrent;
            currentFacture = _fatureCurrent;
            Facturation_viewModel viewModel = new Facturation_viewModel(_regionManage,_containe, FatureCurrent);
            if (_fatureCurrent.IdFacture == 0)
                viewModel.BtnCloseVisible = true;
            else
                viewModel.BtnCloseVisible = false ;
            _viewModel = viewModel;
            this.DataContext = viewModel;
            if (FatureCurrent != null && FatureCurrent.IdFacture > 0)
                txtObjet.Text = FatureCurrent.Label_objet;
            //ComboPoUp(cmbClient);
            //ComboPoUp(cmbexploit );
            //ComboPoUp(cmbObjet );

           // VeridDataContext();
        }

        void VeridDataContext()
        {
            //FromDate.BlackoutDates.Add(new CalendarDateRange(new DateTime(), DateTime.Now.AddDays(-1)));
            //datevalidate.BlackoutDates.Add(new CalendarDateRange(new DateTime(), DateTime.Now.AddDays(-1)));
            //if (_viewModel.FatureCurrent.DateCreation != null)
            //{
            //    if (_viewModel.FatureCurrent.DateCreation < datevalidate.SelectedDate)
            //        _viewModel.FatureCurrent.DateCreation = null;
            //    this.datevalidate.BlackoutDates.Add(new CalendarDateRange(new DateTime(), ((DateTime)_viewModel.FatureCurrent.DateCreation).AddDays(-1)));
            //}
            //else
            //{
            //    this.datevalidate.BlackoutDates.Clear();
            //    datevalidate.BlackoutDates.Add(new CalendarDateRange(new DateTime(), DateTime.Now.AddDays(-1)));
            //}
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
            LoadDatas(FatureCurrent);
           
        }

        #region INIT DATAS
        
       
        void LoadDatas(FactureModel newFacture)
        {
            int i=0;
            int exp = 0;
            int obj = 0; int ii = 0;

          
            if (newFacture.IdFacture > 0)
            {
                if (_viewModel.ClientList != null)
                {
                    foreach (var val in _viewModel.ClientList)
                    {
                        if (val.IdClient == newFacture.IdClient)
                        {
                            cmbClient.SelectedIndex = i;
                            break;
                        }
                        i++;
                    }
                }
            }


            if (newFacture.IdFacture > 0)
            {
                if (_viewModel.ObjetList != null)
                {
                    foreach (var val in _viewModel.ObjetList)
                    {
                        if (val.IdObjet == newFacture.IdObjetFacture)
                        {
                            cmbObjet.SelectedIndex = obj;
                            break;
                        }
                        obj++;
                    }
                }
            }

            if (newFacture.IdFacture > 0)
            {
                if (_viewModel.ExploitationList != null)
                {
                    foreach (var val in _viewModel.ExploitationList)
                    {
                        if (val.IdExploitation == newFacture.IdExploitation)
                        {
                            cmbexploit.SelectedIndex = exp;
                            break;
                        }
                        exp++;
                    }
                }
            }


            if (newFacture.IdFacture > 0)
            {
                if (_viewModel.DepartementList != null)
                {

                    foreach (var val in _viewModel.DepartementList)
                    {
                        if (val.IdDep == newFacture.IdDepartement)
                        {
                            cmbDep.SelectedIndex = ii;
                            break;
                        }
                        ii++;
                    }
                }
            }


           

        }

       
        #endregion

    


        #region Protected Methods



        private void cmbClient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                e.Handled = true;
                cmbClient.Items.MoveCurrentToNext();
            }
        }

        private void cmbObjet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                e.Handled = true;
                cmbObjet.Items.MoveCurrentToNext();
            }
        }

      

        private void cmbexploit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                e.Handled = true;
                cmbexploit.Items.MoveCurrentToNext();
            }
        }

        void ComboPoUp(ComboBox  combo)
        {
            combo.Loaded += delegate
            {
                TextBox textbox = combo.Template.FindName("PART_EditableTextBox", combo) as TextBox;
                Popup popup = combo.Template.FindName("PART_Popup", combo) as Popup;
                if (textbox != null)
                {
                    textbox.TextChanged += delegate
                    {

                        combo.Items.Filter += a =>
                        {
                            if (a.ToString().StartsWith(textbox.Text))
                            {
                                popup.IsOpen = true;

                                string val = textbox.Text;
                                return true;
                            }
                            return false;
                        };
                    };
                }
            };

            if (combo.Name == "cmbClient")
              combo.KeyDown += new System.Windows.Input.KeyEventHandler(cmbClient_KeyDown);
           else  if (combo.Name == "cmbObjet")
                combo.KeyDown += new System.Windows.Input.KeyEventHandler(cmbObjet_KeyDown);
           else  if (combo.Name == "cmbexploit")
                combo.KeyDown += new System.Windows.Input.KeyEventHandler(cmbexploit_KeyDown);
        }


        //protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        //{
        //    //base.OnClosing(e);
        //    this._viewModel.Dispose();
        //}

        #endregion

        #region Event Handlers

        private void btnAddgrid_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LigneFacture.Description = txtOther.Text;
            txtOther.Text = string.Empty;
            //txtQty.Value = 0;
        }

        private void items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           // this._viewModel.LigneFacture= this.Items.SelectedItem as LigneFactureModel;
        }

        private void search_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //this.ViewModel.IsSearchButtonEnabled = (this.search.Text.Length > 0) ? true : false;
        }

        private void workOrders_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //this.ViewModel.WorkOrder = ((ListViewItem)sender).Content as WorkOrderViewModel;
            e.Handled = true;
        }

        private void workOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //this.ViewModel.SelectedWorkOrder = this.workOrders.SelectedItem as WorkOrderViewModel;
        }


        private void cmdnewfacture_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LigneFatureListe = null;
            currentFacture = null;
         
        }

        private void txtQty_LostFocus(object sender, RoutedEventArgs e)
        {
            float result = 0;

            string currentqty = string.Empty;
            if (txtQty.Text != string.Empty)
            {
                if (float.TryParse(txtQty.Text, out result))
                    _viewModel.Qtyselect = result;
               
            }

        }

        private void txtMontant_MouseDown(object sender, MouseButtonEventArgs e)
        {
           decimal  newval=0;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                 var value = ((TextBlock)sender).Text;
                foreach (var ligne in _viewModel.LigneCommandList)
                {
                    newval += ligne.montantHt;
                    value = value.ToString().Replace(".00", " ").Replace (","," ").Trim ();
                    if (ligne.montantHt == (decimal )Common.GetDoule(value))
                        break;
                }

                _viewModel.AfficheResume = string.Format("{0:#,##}", newval);
                
            }
        

        }

        private void txtMontant_MouseLeave(object sender, MouseEventArgs e)
        {
            _viewModel.AfficheResume = "";
        }


        private void cmbexploit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != cmbexploit.SelectedItem)
            {

                ComboBoxItem cbItem = (ComboBoxItem)cmbexploit.SelectedItem;
                string val = cbItem.Content.ToString();
            }

        }

        private void cmbObjet_LostFocus(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cbItem = (ComboBoxItem)cmbexploit.SelectedValue ;
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
                    _viewModel.FatureCurrent.Label_objet = string.Empty;
                }
            }
        }

        private void cmbClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this._viewModel.ClientSelected != null)
            {
                if (!isvaluesAddAutocomple)
                {
                    lignefacture = new LigneFactureModel();
                    List<AutoCompleteEntry> liste = lignefacture.LIGNE_FACTURE_DESCRIPTION("ligne");
                    foreach (var entry in liste)
                        txtOther.AddItem(entry);


                    liste = lignefacture.LIGNE_FACTURE_DESCRIPTION("objet");
                    foreach (var entry in liste)
                        txtObjet.AddItem(entry);

                    isvaluesAddAutocomple = true;
                }
                txtOther.Text = string.Empty;
                txtOther.Text = null;
            }

        }

        private void txtObjet_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_viewModel.FatureCurrent != null)
                _viewModel.FatureCurrent.Label_objet = txtObjet.Text;

        }

        private void btnValide_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.FatureCurrent != null)
                _viewModel.FatureCurrent.Label_objet = txtObjet.Text;
            currentFacture = _viewModel.FatureCurrent;
        }


        private void Items_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var items = this.Items.SelectedItem as LigneCommand;
            this._viewModel.IsdblClickoperation = true;
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

            this._viewModel.CurOldQty = (double)items.quantite;
            this._viewModel.CuroldPu = (double)items.PrixUnit;

            if (this._viewModel.ClientSelected.Exonerere  != null)
            {
                if (this._viewModel.ClientSelected.Exonerere .CourtDesc  == "part")
                {
                    if (items.Idetail > 0)
                    {
                        DetailProductModel detailservice = new DetailProductModel();
                        DetailProductModel det = detailservice.DETAIL_PRODUIT_GETBYID(items.Idetail);
                        this._viewModel.CurIsExonere = det.Exonerer;
                        this._viewModel.CurIsProrata  = det.Isprorata;
                    }
                   
                }
            }

            int i = 0;
            foreach (var val in _viewModel.CacheProduiList)
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
            this._viewModel.OldProduitSelected = prod;
            this._viewModel.OldLigneFacture = newLine;

            this._viewModel.IsdblClickoperation = false ;
            //

        }

        #endregion

        private void suppression_click(object sender, RoutedEventArgs e)
        {

            var items = this.Items.SelectedItem as LigneCommand;

            if (_viewModel.droitFormulaire.Super || _viewModel.droitFormulaire.Suppression)
            {
                if (FatureCurrent != null && FatureCurrent.DateCreation != null)
                {
                    StyledMessageBoxView messageBox = new StyledMessageBoxView();
                    messageBox.Owner = Application.Current.MainWindow;
                    messageBox.Title = " INFORMATION SUPPRESSIONLIGNE FACTURE";
                    messageBox.ViewModel.Message = "Voulez vous Supprimer Cette Ligne de Facture";
                    if (messageBox.ShowDialog().Value == true)
                    {
                        try
                        {
                            if (items.ID > 0)
                            {
                                _viewModel.ligneFactureService.LIGNE_FACTURE_DELETE(items.ID);
                                List<LigneFactureModel> newliste = _viewModel.ligneFactureService.LIGNE_FACTURE_BYIDFActure(_viewModel.FatureCurrent .IdFacture );
                                _viewModel.LigneFatureListe = newliste;
                            }
                            else
                            {
                                _viewModel.LigneCommandList.Remove(items);
                                _viewModel.LigneCommandList = _viewModel.LigneCommandList;
                                _viewModel.CacheLigneCommandList = _viewModel.LigneCommandList;
                            }
                           

                            //this.IsBusy = false;
                        }
                        catch (Exception ex)
                        {
                            CustomExceptionView view = new CustomExceptionView();
                            view.Owner = Application.Current.MainWindow;
                            view.Title = "Warning Message Delete Product";
                            view.ViewModel.Message = ex.Message;
                            view.ShowDialog();
                            //IsBusy = false;
                            //this.MouseCursor = null;
                        }
                    }
                }
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        

       

      

       

       

       
      

       

      

       

       

      

      

      
    }
}
