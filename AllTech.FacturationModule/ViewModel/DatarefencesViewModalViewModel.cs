using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using AllTech.FrameWork.Model;
using AllTech.FrameWork.Global;
using AllTech.FacturationModule.Views;
using System.Windows;


namespace AllTech.FacturationModule.ViewModel
{
    public class DatarefencesViewModalViewModel : ViewModelBase
    {

        object donneesRegion;
        object clientRegion;
        object produitRegion;
        object userRegion;
        object companyRegion;
        SocieteModel societeCourante;
        UtilisateurModel userConnected;
        ParametresModel _parametersDatabase;
        DroitModel _currentDroit;

        bool isMenuCompanyVisible;
        bool isMenuproductVisible;
        bool isMenuUsersVisible;
        bool isMenuClientVisible;
        bool isMenufacturesVisible;
        Window _window;

       
       public DatarefencesViewModalViewModel( Window window)
       {
           societeCourante = GlobalDatas.DefaultCompany;
           ParametersDatabase = GlobalDatas.dataBasparameter;
           UserConnected = GlobalDatas.currentUser;
           CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference"));
           _window = window;
           LoadView();
       }

        #region PRPRIETES

       public bool IsMenufacturesVisible
       {
           get { return isMenufacturesVisible; }
           set
           {
               isMenufacturesVisible = value;
               this.OnPropertyChanged("IsMenufacturesVisible");
           }
       }

       public bool IsMenuClientVisible
       {
           get { return isMenuClientVisible; }
           set
           {
               isMenuClientVisible = value;
               this.OnPropertyChanged("IsMenuClientVisible");
           }
       }

       public bool IsMenuUsersVisible
       {
           get { return isMenuUsersVisible; }
           set
           {
               isMenuUsersVisible = value;
               this.OnPropertyChanged("IsMenuUsersVisible");
           }
       }

       public bool IsMenuproductVisible
       {
           get { return isMenuproductVisible; }
           set
           {
               isMenuproductVisible = value;
               this.OnPropertyChanged("IsMenuproductVisible");
           }
       }

       public bool IsMenuCompanyVisible
       {
           get { return isMenuCompanyVisible; }
           set
           {
               isMenuCompanyVisible = value;
               this.OnPropertyChanged("IsMenuCompanyVisible");
           }
       }

       public ParametresModel ParametersDatabase
       {
           get { return _parametersDatabase; }
           set
           {
               _parametersDatabase = value;
               this.OnPropertyChanged("ParametersDatabase");
           }
       }


       public object DonneesRegion
       {
           get { return donneesRegion; }
           set { donneesRegion = value;

           OnPropertyChanged("DonneesRegion");
           }
       }

       public object ClientRegion
       {
           get { return clientRegion; }
           set { clientRegion = value;
           OnPropertyChanged("ClientRegion");
           }
       }

       public object ProduitRegion
       {
           get { return produitRegion; }
           set { produitRegion = value;
           OnPropertyChanged("ProduitRegion");
           }
       }

       public object UserRegion
       {
           get { return userRegion; }
           set { userRegion = value;
           OnPropertyChanged("UserRegion");
           }
       }

       public object CompanyRegion
       {
           get { return companyRegion; }
           set { companyRegion = value;
           OnPropertyChanged("CompanyRegion");
           }
       }

      


       public UtilisateurModel UserConnected
       {
           get { return userConnected; }
           set
           {
               userConnected = value;
               this.OnPropertyChanged("UserConnected");
           }
       }

       public DroitModel CurrentDroit
       {
           get { return _currentDroit; }
           set
           {
               _currentDroit = value;
               this.OnPropertyChanged("CurrentDroit");
           }
       }
        #endregion

        #region ICOMMAND

        #endregion

        #region METHODS

       void LoadView()
       {
           try
           {
               if (CurrentDroit.SousDroits.Exists(idv => idv.LibelleSouVue.Contains("societe")))
               {

                   //DataRef_Company Views = new DataRef_Company(_window);
                   //CompanyRegion = Views;
                   IsMenuCompanyVisible = true;
               }

               if (CurrentDroit.SousDroits.Exists(idv => idv.LibelleSouVue.Contains("utilisateurs")))
               {

                  // DataRefUtilisateur Views = new DataRefUtilisateur();
                  // UserRegion = Views;
                   IsMenuUsersVisible = true;
               }

               if (CurrentDroit.SousDroits.Exists(idv => idv.LibelleSouVue.Contains("produits")))
               {
                   DatarefClient view = new DatarefClient(_window);
                   ProduitRegion = view;
                   IsMenuproductVisible = true;
               }

               if (CurrentDroit.SousDroits.Exists(idv => idv.LibelleSouVue.Contains("client")))
               {

                   //DataRef_Produit views = new DataRef_Produit(_window);
                   //ClientRegion = views;
                   IsMenuClientVisible = true;

               }

               if (CurrentDroit.SousDroits.Exists(idv => idv.LibelleSouVue.Contains("factures")))
               {
                   DatarefInvoice view = new DatarefInvoice(_window);
                   DonneesRegion = view;

                   IsMenufacturesVisible = true;
               }
           }
           catch (Exception ex)
           {
               System.Windows.Forms.MessageBox.Show("Erreur " + ex.Message, "");
           }
       }
        #endregion

    }
}
