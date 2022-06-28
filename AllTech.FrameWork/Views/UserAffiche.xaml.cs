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
using AllTech.FrameWork.Model;
using System.Collections.ObjectModel;
using AllTech.FrameWork.Global;

namespace AllTech.FrameWork.Views
{
    /// <summary>
    /// Interaction logic for UserAffiche.xaml
    /// </summary>
    public partial class UserAffiche : UserControl
    {
        SocieteModel societeCourante;
        FactureModel factureservice;
        ObservableCollection<FactureModel> listeFacture = null;
        int nbref_nbrCreate=0;
        int nbref_nbrencours = 0;
        int nbref_nbValide = 0;
        int nbref_nbrSortie = 0;
        int nbref_nbrSuspend = 0;
        int nbref_nbrAvoir = 0;

        public UserAffiche()
        {
            InitializeComponent();
            societeCourante = GlobalDatas.DefaultCompany;
            StatutModel statut=new StatutModel ();
            funnel.ItemsSource = GetDatas;
            funnel.Height=((GlobalDatas.mainHeight *0.35));
            
        }

        ObservableCollection<FinancialDataPoint> GetDatas
        {

            get
            {
                ObservableCollection<FinancialDataPoint> liste = new ObservableCollection<FinancialDataPoint>();
                try
                {
                    if (societeCourante != null)
                    {
                        factureservice = new FactureModel();
                        if (CacheDatas.Listefactures == null)
                        {
                            
                                listeFacture = factureservice.FACTURE_GETLISTE(societeCourante.IdSociete);
                                CacheDatas.Listefactures = listeFacture;
                                CacheDatas.lastUpdatefacture = DateTime.Now;
                           
                        }
                        else
                        {
                            if (CacheDatas.lastUpdatefacture.HasValue)
                                listeFacture = CacheDatas.Listefactures;
                            else
                            {
                                listeFacture = factureservice.FACTURE_GETLISTE(societeCourante.IdSociete);
                                CacheDatas.Listefactures = listeFacture;
                                CacheDatas.lastUpdatefacture = DateTime.Now;
                            }
                        }
                       
                        if (listeFacture != null)
                        {
                            //nbref_nbrCreate = listeFacture.Count(f => f.CurrentStatut.CourtDesc == "1");
                            //nbref_nbrencours = listeFacture.Count(f => f.CurrentStatut.CourtDesc == "2");
                            //nbref_nbValide = listeFacture.Count(f => f.CurrentStatut.CourtDesc == "3");
                            //nbref_nbrSortie = listeFacture.Count(f => f.CurrentStatut.CourtDesc == "4");
                            //nbref_nbrSuspend = listeFacture.Count(f => f.CurrentStatut.CourtDesc == "5");
                            //nbref_nbrAvoir = listeFacture.Count(f => f.CurrentStatut.CourtDesc == "7");

                            nbref_nbrCreate = listeFacture.Count(f => f.IdStatut==14001);
                            nbref_nbrencours = listeFacture.Count(f => f.IdStatut == 14002);
                            nbref_nbValide = listeFacture.Count(f => f.IdStatut == 14003);
                            nbref_nbrSortie = listeFacture.Count(f => f.IdStatut == 14004);
                            nbref_nbrSuspend = listeFacture.Count(f => f.IdStatut == 14005);
                            nbref_nbrAvoir = listeFacture.Count(f => f.IdStatut == 14007);
                        }
                    }

                   // Global.Utils.logConnection(" Fin  chargement Ecran accueille et Historique des factures ", "");

                    liste.Add(new FinancialDataPoint { Spending = nbref_nbrCreate, Budget = nbref_nbrCreate, Label = Multilingue.Resources.LanguageHelper.LblUserAfficheCreated });//  GlobalDatas.DisplayLanguage["lblUserAfficheCreated"].ToString() });
                    liste.Add(new FinancialDataPoint { Spending = nbref_nbrencours, Budget = nbref_nbrencours, Label = Multilingue.Resources.LanguageHelper.LblUserAfficheCours });
                    liste.Add(new FinancialDataPoint { Spending = nbref_nbValide, Budget = nbref_nbValide, Label = Multilingue.Resources.LanguageHelper.LblUserAfficheValidee });
                    liste.Add(new FinancialDataPoint { Spending = nbref_nbrSortie, Budget = nbref_nbrSortie, Label = Multilingue.Resources.LanguageHelper.LblUserAfficheSorties });
                    liste.Add(new FinancialDataPoint { Spending = nbref_nbrSuspend, Budget = nbref_nbrSuspend, Label = Multilingue.Resources.LanguageHelper.LblUserAfficheSuspendu });
                    liste.Add(new FinancialDataPoint { Spending = nbref_nbrAvoir, Budget = nbref_nbrAvoir, Label = "Facture Avoir" });


                  

                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = Multilingue.Resources.LanguageHelper.LblUserAfficheErrorTitre;  //GlobalDatas.DisplayLanguage["lblUserAfficheErrorTitre"].ToString();
                    view.ViewModel.Message = Multilingue.Resources.LanguageHelper.LblUserAfficheError; // GlobalDatas.DisplayLanguage["lblUserAfficheError"].ToString() + ex.Message;
                    view.ShowDialog();

                    Global.Utils.logConnection("<- Dbrequette -- Fatal error lors du chargement de l'hisroique des factures","");
                }
                return liste;
                
            }
        }
    }

  

    public class FinancialDataPoint
    {
        public string Label { get; set; }
        public double Spending { get; set; }
        public double Budget { get; set; }


        public string ToolTip { get { return String.Format("{0}, Spending {1}, Budget {2}", Label, Spending, Budget); } }

    }
}
