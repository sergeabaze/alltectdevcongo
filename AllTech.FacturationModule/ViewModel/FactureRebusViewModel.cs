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
using AllTech.FrameWork.Global;
using System.Collections.ObjectModel;
using AllTech.FacturationModule.Views.Modal;
using System.Data;
using AllTech.FacturationModule.Views;

namespace AllTech.FacturationModule.ViewModel
{
   

    public class FactureRebusViewModel : ViewModelBase
    {
        #region FIELDS

        private bool isBusy;
        bool _progressBarVisibility;


        private Cursor mouseCursor;

       
        string filtertexte;
        private RelayCommand deleteCommand;

        FactureModel _fatureCurrent;
        DelFacture factureSelect;
        List<DelFacture> listeFactures;

     


        FactureModel factureservice;
        FactureModel FactureCache;

        SocieteModel societeCourante;
        UtilisateurModel userConnected;
        #endregion

        #region CONSTRUCTEURS

        public FactureRebusViewModel()
        {
            factureservice = new FactureModel();
            societeCourante = GlobalDatas.DefaultCompany;

            userConnected = GlobalDatas.currentUser;

            loadDatas();
        }
        #endregion

        #region PROPERTIES
        public DelFacture FactureSelect
        {
            get { return factureSelect; }
            set { factureSelect = value;
            OnPropertyChanged("FactureSelect");
            }
        }


        public List<DelFacture> ListeFactures
        {
            get { return listeFactures; }
            set { listeFactures = value;
            OnPropertyChanged("ListeFactures");
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value;
            OnPropertyChanged("IsBusy");
            }
        }

        public bool ProgressBarVisibility
        {
            get { return _progressBarVisibility; }
            set { _progressBarVisibility = value;
            OnPropertyChanged("ProgressBarVisibility");
            }
        }

        public Cursor MouseCursor
        {
            get { return mouseCursor; }
            set { mouseCursor = value;
            OnPropertyChanged("MouseCursor");
            }
        }
        #endregion

        #region ICOMMAND
        public ICommand DeleteCommand
        {
            get
            {
                if (this.deleteCommand == null)
                {
                    this.deleteCommand = new RelayCommand(param => this.canDelete(), param => this.canExecuteDeletefacture());
                }
                return this.deleteCommand;
            }


        }
        #endregion

        #region METHODS


        void loadDatas()
        {
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;

            worker.DoWork += (o, args) =>
            {
                try
                {
                    long oldID = 0;

                    List<DelFacture> factures = new List<DelFacture>();

                    DataTable tabresult = factureservice.LISTE_FACTUTE_TO_DEL(societeCourante.IdSociete);
                    if (tabresult != null && tabresult.Rows.Count > 0)
                    {
                        foreach (DataRow row in tabresult.Rows)
                        {
                            if (Convert.ToInt64(row["ID"]) != oldID)
                            {
                                factures.Add(new DelFacture { ID = Convert.ToInt64(row["ID"]),
                                                              NumeroFacture = Convert.ToString(row["Numero_Facture"]),
                                                              Client = Convert.ToString(row["Nom_Client"]),
                                                              CreerPar = Convert.ToString(row["Cree_Par"]),
                                                              Exploitation = Convert.ToString(row["exploitation"]),
                                                              Objet = Convert.ToString(row["objet_facture"]),
                                                              MontantTTc = Convert.ToDecimal(row["totalTTC"]),
                                                              DateCreation = Convert.ToDateTime(row["Date_Creation"]),
                                                              DateSuppression = row["Date_Modification"] !=DBNull .Value ? Convert.ToDateTime(row["Date_Modification"]):DateTime.MinValue  ,
                                                              Items = GetListeFacture(Convert.ToInt64(row["ID"]), tabresult)
                                });
                            }
                            oldID = Convert.ToInt64(row["ID"]);

                        }

                        ListeFactures = factures;
                    }

                }
                catch (Exception ex)
                {
                    args.Result = ex.Message + " ;" + ex.InnerException;
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
                    this.MouseCursor = null;
                    this.IsBusy = false;
                }
                else
                {
                    this.MouseCursor = null;
                    this.IsBusy = false;

                }
                //this.OnPropertyChanged("ListEmployees");
            };

            worker.RunWorkerAsync();
        }

        List <DelLigneFactures> GetListeFacture(long IDFACTURE, DataTable table)
        {
            List<DelLigneFactures> items = new List<DelLigneFactures>();

            DataRow[] rows = table.Select(string.Format(" ID ='{0}'", IDFACTURE));
            DataTable newTable = table.Clone();
            foreach (DataRow newRow in rows)
                newTable.ImportRow(newRow);
            if (newTable != null && newTable.Rows.Count > 0)
            {
                foreach (DataRow ligne in newTable.Rows )
                {
                    items.Add(new DelLigneFactures { ID = Convert.ToInt64(ligne["ID_item"]),
                                                     IDFacture = Convert.ToInt64(ligne["ID"]),
                                                     NombreLignes = newTable.Rows.Count,
                                                     PrixUnit = Convert.ToDouble(ligne["Prixunit"]),
                                                     Qte = Convert.ToDouble(ligne["Quantite"]),
                                                     Produit = Convert.ToString(ligne["produit"]),
                                                     MontantHTTC = Convert.ToDouble(ligne["MontantHt"])
                    });
                }
            }
            
            
        
            
            return items;

        }

        void canDelete()
        {

        }

        bool canExecuteDeletefacture()
        {
            return true;
        }
        #endregion
    }


    public class DelFacture
    {
        public long ID { get; set; }
        public string NumeroFacture { get; set; }
        public string Client { get; set; }
        public  DateTime?  DateCreation { get; set; }
        public DateTime? DateSuppression { get; set; }
        public string Objet { get; set; }
        public string Exploitation { get; set; }
        public string CreerPar { get; set; }
        public decimal MontantTTc { get; set; }

        private List<DelLigneFactures> items = new List<DelLigneFactures>();

        public List<DelLigneFactures> Items
        {
            get { return items; }
            set { items = value; }
        }
    }

    public class DelLigneFactures
    {
        public long ID { get; set; }
        public long IDFacture { get; set; }
        public int NombreLignes { get; set; }
        public string Produit { get; set; }
        public double  Qte { get; set; }
        public double PrixUnit { get; set; }
        public double MontantHTTC { get; set; }
    }
 
}
