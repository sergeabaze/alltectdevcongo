using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;

namespace AllTech.FrameWork.Model
{
    public class JournalVentesGroupeModel : ViewModelBase
    {
        public int Id { get; set; }
        public string CodeJournal { get; set; }
        public DateTime Datefacture { get; set; }
        public string Numerofacture { get; set; }
        public double? MontantDebit { get; set; }
        public double? MontantCredit { get; set; }
        public string Libellefacture { get; set; }
        public string LibelleStatut { get; set; }
        public string LibelleStatutOperation { get; set; }
        public Int64 IdFactures { get; set; }
        public int IdStatut { get; set; }
        private string backGround;

        public string BackGround
        {
            get { return backGround; }
            set { backGround = value;
            this.OnPropertyChanged("BackGround");
            }
        }

        private List<JournalVentesModel> journalVentesCmpList = new List<JournalVentesModel>();

        public List<JournalVentesModel> JournalVentesCmpList
        {
            get { return journalVentesCmpList; }
            set { journalVentesCmpList = value; }
        }


        Facturation DAL = null;
        public JournalVentesGroupeModel()
        {
             DAL = (Facturation)DataProviderObject.FacturationDal;
        
        }

        #region Methods


        public List<JournalVentesGroupeModel> HistoricComptes(int idDateHistoric)
        {
            List<JournalVentesGroupeModel> liste = new List<JournalVentesGroupeModel>();
            List<JournalVenteGroupe> listerFromDale = DAL.GetJournalVente_HistoriqueGroupBy(idDateHistoric);
            if (listerFromDale != null && listerFromDale.Count > 0)
            {
                foreach (JournalVenteGroupe jvd in listerFromDale)
                {
                    JournalVentesGroupeModel jv = new JournalVentesGroupeModel();
                    jv.Id = jvd.Id;
                    jv.CodeJournal = jvd.CodeJournal;
                    jv.Numerofacture = jvd.Numerofacture;
                    jv.MontantDebit = jvd.MontantDebit;
                    jv.MontantCredit = jvd.MontantCredit;
                    jv.Libellefacture = jvd.Libellefacture;
                    jv.IdFactures = jvd.IdFactures;
                    jv.LibelleStatut = jvd.LibelleStatut;
                    jv.LibelleStatutOperation = jvd.LibelleStatutOperation;
                    jv.IdFactures = jvd.IdFactures;
                    jv.IdStatut = jvd.IdStatut;
                    jv.JournalVentesCmpList = GetListHistorique(jvd.JournalVentes);
                    if (jv.IdStatut == 14003) jv.BackGround = "White";
                    else if (jv.IdStatut == 14006) jv.BackGround = "Red";
                    liste.Add(jv);
                }
            }
            return liste;
        }


        public List<JournalVentesModel> GetListHistorique(List<JournalVentes> journals)
        {
            List<JournalVentesModel> jvhsts = new List<JournalVentesModel>();
            //List<JournalVentes> journals = DAL.GetJournalVente_Historique(idDate);
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
                    if (jvm.IdStatut == 14003) jvm.BackGround = "White";
                    else if (jv.IdStatut == 14006) jvm.BackGround = "Red";
                    jvhsts.Add(jvm);
                }
            }
            return jvhsts;
        }

     


        //List<JournalVenteCmptAnalityqueViewModel> GetListeAnalytique(List<JournalVenteCompteAnalytique> jvs)
        //{
        //    List<JournalVenteCmptAnalityqueViewModel> liste = new List<JournalVenteCmptAnalityqueViewModel>();
        //    foreach (JournalVenteCompteAnalytique jvm in jvs)
        //    {
        //        JournalVenteCmptAnalityqueViewModel jv = new JournalVenteCmptAnalityqueViewModel();
        //        jv.ID_Client = jvm.ID_Client;
        //        jv.ID_Datejournal = jvm.ID_Datejournal;
        //        jv.IDCompteAnal = jvm.IDCompteAnal;
        //        jv.LibelleMontant = jvm.LibelleMontant;
        //        jv.MontantFacture = jvm.MontantFacture;
        //        jv.NumeroCmptAnal = jvm.NumeroCmptAnal;
        //        jv.Numerofacture = jvm.Numerofacture;
        //        jv.Datefacture = jvm.Datefacture;
        //        liste.Add(jv);
        //    }
        //    return liste;
        //}

        #endregion
    }
}
