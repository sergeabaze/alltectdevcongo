using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;
using System.Data;

namespace AllTech.FrameWork.Model
{
   public  class JournalVentesModel: ViewModelBase
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public int IdProduit { get; set; }
        public string NumCompteGeneral { get; set; }
        public string NumeroCompteTiers { get; set; }
        public string NumeroCompteAnalytique { get; set; }
        public string LibelleMontant { get; set; }
        public string LibelleSectionAnal { get; set; }
        public string LibelleOpertion { get; set; }
        public string StatutOperation { get; set; }
        public string CodeJournal { get; set; }
        public string NumeroJournal { get; set; }
        public string NumeroFacture { get; set; }
        public DateTime Datefacture { get; set; }
        public double? MontantDebit { get; set; }
        public double? MontantCredit { get; set; }
        public string LibelleClient { get; set; }
        public string Ordre { get; set; }
        public int? IdStatut { get; set; }
        public int? IdDate { get; set; }
        private string backGround;

        public string BackGround
        {
            get { return backGround; }
            set { backGround = value;
            this.OnPropertyChanged("BackGround");
            }
        }
      
        List<JournalVenteCmptAnalityqueViewModel> compteAnalytique;

        public List<JournalVenteCmptAnalityqueViewModel> CompteAnalytique
        {
            get { return compteAnalytique; }
            set { compteAnalytique = value; }
        }
        Facturation DAL = null;

        public JournalVentesModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
        }

        #region Methods

        public List<JournalVentesModel> JournalventeHistoriqueTOexport(int idDate)
        {
            List<JournalVentesModel> jvhsts = new List<JournalVentesModel>();
            List<JournalVentes> journals = DAL.GetJournalVente_Historique(idDate);
            if (journals != null && journals.Count > 0)
            {
                foreach (JournalVentes jv in journals)
                {
                    JournalVentesModel jvm = new JournalVentesModel();
                    jvm.Id = jv.Id;
                   
                    jvm.NumCompteGeneral = jv.NumCompteGeneral;
                    jvm.NumeroCompteTiers = jv.NumeroCompteTiers;
                    jvm.NumeroCompteAnalytique = jv.NumeroCompteAnalytique;
                    jvm.LibelleMontant = jv.LibelleMontant;
                    jvm.LibelleSectionAnal = jv.LibelleSectionAnal;
                    jvm.LibelleOpertion = jv.LibelleOpertion;
                   
                    jvm.StatutOperation = jv.StatutOperation;
                    jvm.MontantDebit = jv.MontantDebit;
                    jvm.MontantCredit = jv.MontantCredit;
                    jvm.Ordre = jv.Ordre;
                    jvm.IdStatut = jv.IdStatut;
                    jvhsts.Add(jvm);
                }
            }
            return jvhsts;
        }

       //List<JournalVentes> GetJournalVente_Historiquegenerate(int idDate)
        public DataSet GetListToExport(int idDate,string mode)
        {
           return  DAL.GetJournalListToExport(idDate,mode);
        }

        public bool JournalVenteToExportUpdate(int idDate)
        {
            return DAL.JournalVenteToxexportupdate(idDate);
        }

        List<JournalVenteCmptAnalityqueViewModel> GetListe(List<JournalVenteCompteAnalytique> jvs)
        {
            List<JournalVenteCmptAnalityqueViewModel> liste = new List<JournalVenteCmptAnalityqueViewModel>();
            foreach (JournalVenteCompteAnalytique jvm in jvs)
            {
                JournalVenteCmptAnalityqueViewModel jv = new JournalVenteCmptAnalityqueViewModel();
                jv.ID_Client = jvm.ID_Client;
                jv.ID_Datejournal = jvm.ID_Datejournal;
                jv.IDCompteAnal = jvm.IDCompteAnal;
                jv.LibelleMontant = jvm.LibelleMontant;
                jv.MontantFacture = jvm.MontantFacture;
                jv.NumeroCmptAnal = jvm.NumeroCmptAnal;
                jv.Numerofacture = jvm.Numerofacture;
                jv.Datefacture = jvm.Datefacture;
                liste.Add(jv);
            }
            return liste;
        }


        public List<JournalVentesModel> GetListHistorique_jv(ref DataTable tablisteFact, int idDate,string mode)
        {
            //DataTable tablisteFact = null;
            List<JournalVentesModel> jvhsts = new List<JournalVentesModel>();
            List<JournalVentes> journals = DAL.GetJournalVente_Historiquegenerate(ref tablisteFact, idDate, mode);
            if (journals != null && journals.Count > 0)
            {
                foreach (JournalVentes jv in journals)
                {
                    JournalVentesModel jvm = new JournalVentesModel();
                    jvm.Id = jv.Id;
                    jvm.IdProduit = jv.IdProduit;
                    jvm.IdClient = jv.IdClient;
                    jvm.NumCompteGeneral = jv.NumCompteGeneral;
                    jvm.NumeroCompteTiers = jv.NumeroCompteTiers;
                    jvm.NumeroCompteAnalytique = jv.NumeroCompteAnalytique;
                    jvm.LibelleMontant = jv.LibelleMontant;
                    jvm.LibelleSectionAnal = jv.LibelleSectionAnal;
                    jvm.LibelleOpertion = jv.LibelleOpertion;
                    jvm.StatutOperation = jv.StatutOperation;
                    jvm.MontantDebit = jv.MontantDebit;
                    jvm.MontantCredit = jv.MontantCredit;
                    jvm.Ordre = jv.Ordre;
                    jvm.IdStatut = jv.IdStatut;
                    jvm.Datefacture = jv.Datefacture;
                    jvm.CodeJournal = jv.CodeJournal;
                    jvm.NumeroJournal = jv.NumeroJournal;
                    jvm.NumeroFacture = jv.NumeroFacture;
                    jvm.LibelleClient = jv.LibelleClient;
                    jvm.IdDate = jv.IdDate;
                    if (jvm.IdStatut == 14003) jvm.BackGround = "White";
                    else if (jv.IdStatut == 14006) jvm.BackGround = "Red";
                    jvhsts.Add(jvm);
                }
            }
            return jvhsts;
        }

        public List<JournalVentesModel> GetListHistorique_Search(ref DataTable tablisteFact,string NumeroFacture)
        {
            //DataTable tablisteFact = null;
            List<JournalVentesModel> jvhsts = new List<JournalVentesModel>();
            List<JournalVentes> journals = DAL.GetJournalVente_Search(ref tablisteFact, NumeroFacture);
            if (journals != null && journals.Count > 0)
            {
                foreach (JournalVentes jv in journals)
                {
                    JournalVentesModel jvm = new JournalVentesModel();
                    jvm.Id = jv.Id;
                    jvm.IdProduit = jv.IdProduit;
                    jvm.IdClient = jv.IdClient;
                    jvm.NumCompteGeneral = jv.NumCompteGeneral;
                    jvm.NumeroCompteTiers = jv.NumeroCompteTiers;
                    jvm.NumeroCompteAnalytique = jv.NumeroCompteAnalytique;
                    jvm.LibelleMontant = jv.LibelleMontant;
                    jvm.LibelleSectionAnal = jv.LibelleSectionAnal;
                    jvm.LibelleOpertion = jv.LibelleOpertion;
                    jvm.StatutOperation = jv.StatutOperation;
                    jvm.MontantDebit = jv.MontantDebit;
                    jvm.MontantCredit = jv.MontantCredit;
                    jvm.Ordre = jv.Ordre;
                    jvm.IdStatut = jv.IdStatut;
                    jvm.Datefacture = jv.Datefacture;
                    jvm.CodeJournal = jv.CodeJournal;
                    jvm.NumeroJournal = jv.NumeroJournal;
                    jvm.NumeroFacture = jv.NumeroFacture;
                    jvm.LibelleClient = jv.LibelleClient;
                    jvm.IdDate = jv.IdDate;
                    if (jvm.IdStatut == 14003) jvm.BackGround = "White";
                    else if (jv.IdStatut == 14006) jvm.BackGround = "Red";
                    jvhsts.Add(jvm);
                }
            }
            return jvhsts;
        }

        public bool JournalVente_testeDelete(int idDate)
        {
            return DAL.JournalVenteTesteDelete(idDate);
        }

        public DataTable JournalVente_Periode_mois(string annes)
        {
            return DAL.JournalVente_periodeMois(annes);
        }

        public bool JournalVenteHistoriqueUpdateNote(int idDate, int idLigne, string numero)
        {
            return DAL.JournalVenteHistoriqueUpdateNote(idDate, idLigne, numero);
        }

        #endregion
    }
}
