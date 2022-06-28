using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using AllTech.FrameWork.Services;
using System.Windows.Input;
using AllTech.FrameWork.Command;
using AllTech.FrameWork.Region;
using System.ComponentModel;
using AllTech.FrameWork.Views;
using System.Windows;
using AllTech.FrameWork.Model;
using System.Collections.ObjectModel;
using System.Data;
using AllTech.FacturationModule.Views.Modal;
using System.Threading;
using AllTech.FrameWork.Global;

namespace AllTech.FacturationModule.Views.Modal
{

    
   public  class DetailProduitClientViewModel:ViewModelBase 
    {
        private readonly IRegionManager _regionManager;
        //private readonly IEventAggregator eventAggregator;
        private readonly IUnityContainer _container;
        private IInjectSingleViewService _injectSingleViewService;

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;
        private RelayCommand closeCommand;


        private bool isBusy;
        bool _progressBarVisibility;
        private Cursor mouseCursor;
        string filtertexte;

        ObservableCollection<DetailProductModel> _detailProduitlist = new ObservableCollection<DetailProductModel>();
        ObservableCollection<DetailProductModel> _cacheDetailProduitlist;


        DetailProductModel _detailProduitSelect;
        DetailProductModel detailService;
        ObservableCollection<ProduitModel> _produitList;

       
        ProduitModel produitselected;
        ProduitModel produitService;

        ObservableCollection<ClientModel> _clientList;
        ClientModel clientService;
        ClientModel _clientSelected;
        SocieteModel societeCourante;


        public DetailProduitClientViewModel(ClientModel client)
       {
           Clientselected = client;
         
           clientService = new ClientModel();
           detailService = new DetailProductModel();
           societeCourante = GlobalDatas.DefaultCompany;
           produitService = new ProduitModel();
           _injectSingleViewService = new InjectSingleViewService(_regionManager, _container);
           loadDatas();
       }

        #region Propriete

        public ObservableCollection<ProduitModel> ProduitList
        {
            get { return _produitList; }
            set { _produitList = value;
            this.OnPropertyChanged("ProduitList");
            }
        }

        public ObservableCollection<DetailProductModel> DetailProduitlist
        {
            get { return _detailProduitlist; }
            set
            {
                _detailProduitlist = value;
                this.OnPropertyChanged("DetailProduitlist");
            }
        }

        public DetailProductModel DetailProduitSelect
        {
            get { return _detailProduitSelect; }
            set
            {
                _detailProduitSelect = value  ;
               // TxtEnable_a = true;
                this.OnPropertyChanged("DetailProduitSelect");
            }
        }


        public ProduitModel Produitselected
        {
            get { return produitselected; }
            set
            {
                produitselected = value;
                this.OnPropertyChanged("Produitselected");
            }
        }

        public ClientModel Clientselected
        {
            get { return _clientSelected; }
            set
            {
                _clientSelected = value;
                if (DetailProduitSelect != null)
                    DetailProduitSelect.IdClient = value.IdClient;

                this.OnPropertyChanged("Clientselected");
            }
        }



        public ObservableCollection<ClientModel> ClientList
        {
            get { return _clientList; }
            set
            {
                _clientList = value;
                this.OnPropertyChanged("ClientList");
            }
        }

        public ObservableCollection<DetailProductModel> CacheDetailProduitlist
        {
            get { return _cacheDetailProduitlist; }
            set
            {
                _cacheDetailProduitlist = value;
                this.OnPropertyChanged("CacheDetailProduitlist");
            }
        }
        #endregion


        #region ICOMMAND


        public ICommand SaveCommand
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.saveCommand = new RelayCommand(param => this.canSaveProduit(), param => this.canExecuteSaveProduit());
                }
                return this.saveCommand;
            }
        }

        public ICommand NewCommand
        {
            get
            {
                if (this.newCommand == null)
                {
                    this.newCommand = new RelayCommand(param => this.canNewProduit());
                }
                return this.newCommand;
            }


        }

        public ICommand DeleteCommand
        {
            get
            {
                if (this.deleteCommand == null)
                {
                    this.deleteCommand = new RelayCommand(param => this.canDeleteProduit(), param => this.canExecuteDeleteProduit());
                }
                return this.deleteCommand;
            }


        }
        //public RelayCommand CloseCommand
        //{
        //    get
        //    {
        //        if (this.closeCommand == null)
        //        {
        //            this.closeCommand = new RelayCommand(param => this.canClose());
        //        }
        //        return this.closeCommand;
        //    }

        //}

        #endregion

        #region METHODS

        void loadDatas()
        {
           
            BackgroundWorker worker = new BackgroundWorker();
            
            worker.DoWork += (o, args) =>
            {
                try
                {
                    if (societeCourante != null)
                    {
                        if (Clientselected != null)
                        {
                            List <DetailProductModel> detailProduit = detailService.DETAIL_PRODUIT_BYCLIENT(Clientselected.IdClient, societeCourante.IdSociete);
                            if (detailProduit !=null )
                            {
                                HashSet<int> IdProduits = new HashSet<int>();
                                foreach (DetailProductModel det in detailProduit)
                                    IdProduits.Add(det.IdProduit);
                                detailProduit.OrderBy (p=>p .IdProduit);

                                ObservableCollection<DetailProductModel> newListeProduit = new ObservableCollection<DetailProductModel>();
                                foreach (DetailProductModel prod in detailProduit)
                                    newListeProduit.Add(prod);

                                DetailProduitlist = newListeProduit;
                                DetailProduitlist.OrderBy(p => p.IdProduit);
                                    //detailService.DETAIL_PRODUIT_GETLISTE(0, Clientselected.IdClient);
                               // ProduitList = newListeProduit; // produitService.Produit_SELECT(societeCourante.IdSociete);
                            //CacheDetailProduitlist = DetailProduitlist;
                            }
                        }
                    }
                   

                }
                catch (Exception ex)
                {
                    args.Result =string .Format ("{0} ; {1}", ex.Message , ex.InnerException);
                }

            };
            worker.RunWorkerCompleted += (o, args) =>
            {
                if (args.Result != null)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.ViewModel.Message = args.Result.ToString();
                    view.ShowDialog();
                 
                }
              
                //this.OnPropertyChanged("ListEmployees");
            };

            worker.RunWorkerAsync();
        }

        private void canNewProduit()
        {
            _detailProduitSelect = new DetailProductModel();
            DetailProduitSelect = _detailProduitSelect;
          

        }



        private void canSaveProduit()
        {
            try
            {
               
                if ((DetailProduitSelect.IdDetail == 0))
                {
                    if (Clientselected != null)
                    {
                        DetailProduitSelect.IdProduit = Produitselected.IdProduit;
                        detailService.DETAIL_PRODUIT_ADD(DetailProduitSelect);
                        for (int i = 0; i < 50; i += 5)
                            Thread.Sleep(100);
                        DetailProduitlist = detailService.DETAIL_PRODUIT_GETLISTE(Produitselected.IdProduit, 0);
                        DetailProduitSelect = null;
                        ClientList = null;
                        ClientList = clientService.CLIENT_GETLISTE(societeCourante.IdSociete, true);
                     
                    }
                    else
                        MessageBox.Show("Préciser Le client Pour ce produit !");

                }
                else
                {
                    detailService.DETAIL_PRODUIT_ADD(DetailProduitSelect);
                    for (int i = 0; i < 50; i += 5)
                        Thread.Sleep(100);
                    DetailProduitlist = detailService.DETAIL_PRODUIT_GETLISTE(Produitselected.IdProduit, 0);
                    DetailProduitSelect = null;
                   
                }

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "INFORMATION SAUVEGARDE";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
             
            }
        }

        bool canExecuteSaveProduit()
        {
            return DetailProduitSelect != null ? true : false;
        }


        private void canDeleteProduit()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = Application.Current.MainWindow;
            messageBox.Title = "INFORMATION SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez vous Supprimer ce Détail ?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    detailService.DETAIL_PRODUIT_DELETE(DetailProduitSelect.IdDetail);
                    DetailProduitSelect = null;
                    DetailProduitlist = detailService.DETAIL_PRODUIT_GETLISTE(Produitselected.IdProduit, 0);
                    DetailProduitSelect = null;

                   

                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = "INFORMATION SUPPRESSION";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                  
                }
            }
        }

        bool canExecuteDeleteProduit()
        {
            return DetailProduitSelect != null ? true : false;
        }


        void filter(string values)
        {
            if (DetailProduitlist != null || DetailProduitlist.Count > 0)
            {
                DataTable newTable = new DataTable();
                DataColumn col1 = new DataColumn("id", typeof(Int32));
                DataColumn col2 = new DataColumn("idclient", typeof(Int32));
                DataColumn col4 = new DataColumn("idproduit", typeof(Int32));
                DataColumn col5 = new DataColumn("quantite", typeof(int));
                DataColumn col6 = new DataColumn("nomproduit", typeof(string));
                DataColumn col7 = new DataColumn("prixUnit", typeof(double));
                DataColumn col8 = new DataColumn("exonere", typeof(bool));
                DataColumn col9 = new DataColumn("nomclient", typeof(string));

                newTable.Columns.Add(col1);
                newTable.Columns.Add(col2);
                newTable.Columns.Add(col4);
                newTable.Columns.Add(col5);
                newTable.Columns.Add(col6);
                newTable.Columns.Add(col7);
                newTable.Columns.Add(col8);
                newTable.Columns.Add(col9);


                DataRow row = null;

                foreach (DetailProductModel sm in CacheDetailProduitlist)
                {
                    row = newTable.NewRow();
                    row[0] = sm.IdDetail;
                    row[1] = sm.IdClient;
                    row[2] = sm.IdProduit;
                    row[3] = sm.Quantite;
                    row[4] = sm.NomProduit;
                    row[5] = sm.Prixunitaire;
                    row[6] = sm.Exonerer;
                    row[7] = sm.Customer.NomClient;
                    newTable.Rows.Add(row);

                }

                DataRow[] nlignes = newTable.Select(string.Format("nomclient like '{0}%'", values.Trim()));
                DataTable filterDatatable = newTable.Clone();
                foreach (DataRow rows in nlignes)
                    filterDatatable.ImportRow(rows);

                DetailProductModel fm;
                ObservableCollection<DetailProductModel> newliste = new ObservableCollection<DetailProductModel>();

                foreach (DataRow r in filterDatatable.Rows)
                {
                    fm = new DetailProductModel()
                    {
                        IdDetail = Int32.Parse(r[0].ToString()),
                        IdClient = Int32.Parse(r[1].ToString()),
                        IdProduit = Int32.Parse(r[2].ToString()),
                        Quantite = Int32.Parse(r[3].ToString()),
                        NomProduit = r[4].ToString(),
                        Prixunitaire = decimal.Parse(r[5].ToString()),
                        Exonerer = bool.Parse(r[6].ToString()),
                        Customer = CacheDetailProduitlist.First(p => p.IdDetail == Int32.Parse(r[0].ToString())).Customer
                    };
                    newliste.Add(fm);
                }
                DetailProduitlist = newliste;

            }
            else
            {
                DetailProduitlist = CacheDetailProduitlist;
                //loadDatas();

            }
        }
        #endregion
    }
}
