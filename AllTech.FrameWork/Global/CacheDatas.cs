using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.Model;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Data;

namespace AllTech.FrameWork.Global
{
   public  class CacheDatas
    {
      

       public static DroitModel ui_currentdroitCompanyInterface;
       public static DroitModel ui_currentdroitClientInterface;
       public static DroitModel ui_currentdroitUserInterface;
       public static DroitModel ui_currentdroitProduitInterface;
       public static DroitModel ui_currentdroitFistoricFacturesInterface;
       public static DroitModel ui_currentdroitFactureElementInterface;

       public static ObservableCollection<UtilisateurModel> ui_UsersCache;
       public static List<ProfileModel> ui_profilCache;
       public static List<CompteModel> ui_ClientCompte;
       public static List<ExonerationModel> ui_ClientExonerations;
       public static List<DeviseModel > ui_ClientDevises;
       public static List<TaxeModel> ui_ClientTaxes;
       public static ObservableCollection<ClientModel > ui_ClientClients;
       public static ObservableCollection<StatutModel> ui_Statut;
       public static ObservableCollection<ObjetGenericModel> ui_ClientObjetS;
       public static ObservableCollection<ExploitationFactureModel > ui_ClientExploitations;

       public static ObservableCollection<LangueModel> ui_ProduitLangue;
       public static ObservableCollection<ProduitModel> ui_ProduitProduits;

       public static ObservableCollection<FactureModel> Listefactures;
       public static DataTable dtlListefactures = null;
       public static DateTime? lastUpdatefacture;

       public static ObservableCollection<ClientModel> ui_Hst_factureClient;
       public static DeviseModel deviseDefault;
       public static TaxeModel  taxeDefault;
       //public static readonly DependencyProperty CurrentParameter = DependencyProperty.Register("PrameterFiles",
       //typeof(ProfileModel),
       //typeof(GlobalDatas),
       //new FrameworkPropertyMetadata(string.Empty));

       //public static readonly DependencyProperty ui_currentdroitClientInterface = DependencyProperty.Register("ui_currentDroitClientI",
       //  typeof(DroitModel),
       //  typeof(GlobalDatas),
       //  new FrameworkPropertyMetadata(string.Empty));

       //public  DroitModel ui_currentDroitClientI
       //{
       //    get { return (DroitModel)GetValue(ui_currentdroitClientInterface); }
       //    set { SetValue(ui_currentdroitClientInterface, value); }
       //}




       //public ProfileModel PrameterFiles
       //{
       //    get { return (ProfileModel)GetValue(CurrentParameter); }
       //    set { SetValue(CurrentParameter, value); }
       //}

       //public static readonly DependencyProperty SortPropertyNameProperty =
       //     DependencyProperty.RegisterAttached("SortPropertyName", typeof(string), typeof(DroitModel));

       //public static string GetSortPropertyName(GridViewColumn obj)
       //{
       //    return (string)obj.GetValue(SortPropertyNameProperty);
       //}

       //public static void SetSortPropertyName(GridViewColumn obj, string value)
       //{
       //    obj.SetValue(SortPropertyNameProperty, value);
       //}
    }
}
