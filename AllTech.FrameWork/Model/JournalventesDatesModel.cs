using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using System.Data;
using FACTURATION_DAL.Model;

namespace AllTech.FrameWork.Model
{
    public class JournalventesDatesModel : ViewModelBase 
    {

        public int ID { get; set; }
        public int IdSite { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string NumeroJournal { get; set; }
        public int? NbreFactures { get; set; }

        Facturation DAL = null;

        public JournalventesDatesModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
        }

        #region properties
        
        #endregion

        #region Methods



        public DataTable JournalVenteGetListe(int idSite, DateTime dateDebut, DateTime dateFin)
        {

            return DAL.JournalVenteGetListe(idSite, dateDebut, dateFin);
        }

        public bool DateJournalInsert(ref int idDate, int nbreFacture, int idSite, DateTime dateDebut, DateTime dateFin)
        {
            return DAL.DateJournalInsert(ref idDate, nbreFacture, idSite, dateDebut, dateFin);
        }

        public bool DateJournalUpdate(int idDate, int nombrefActure)
        {
            return DAL.DateJournalUpdate(idDate, nombrefActure);
        }


        public bool JournalGlobaleGenerate(int idDate, DataRow row,bool isnextDay)
        {
            try
            {
                return DAL.GenerationJournalVentesGlobal(idDate, row, isnextDay);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        
        

        public bool JournalVentesListeInsert(int id, Int64 idFacture,int idclient)
        {
            if (DAL.InsertJournalVentes(id, idFacture, idclient))
                return true;
            else return false;
        }
        public DataTable GetJournalVentesListeByPeriode(int idSite, DateTime periodedebut, DateTime periodeFin)
        {
            DataTable table = DAL.GetJournalVentesListeByPeriode(idSite, periodedebut, periodeFin);
            return table;
        }


        public DataTable GetFactureByIdjV(Int64 IdFacture)
        {
            DataTable table = DAL.GetFactyreByIdjv(IdFacture);
            return table;
        }


        public DataTable GetJournalVentesDates_Periodes(int idSite)
        {
            DataTable table = DAL.GetJournalVentesDates_Periodes(idSite);
            return table;
        }

        public List<JournalventesDatesModel> GetJournalVentesDates_List(int idsite, DateTime periodeSelected)
        {
            List<JournalventesDatesModel> jurnals = new List<JournalventesDatesModel>();

            try
            {

                var  depFrom = DAL.GetJournalVentesDates_List(idsite, periodeSelected);
                JournalventesDatesModel jv = null;
                if (depFrom != null && depFrom.Count >0)
                {
                    foreach (var jvv in depFrom)
                    {
                        jv = new JournalventesDatesModel();
                        jv.ID = jvv.ID;
                        jv.DateDebut = jvv.DateDebut;
                        jv.DateFin = jvv.DateFin;
                        jv.IdSite = jvv.IdSite;
                        jv.NumeroJournal = jvv.NumeroJournal;
                        jv.NbreFactures = jvv.NbreFactures;
                        jurnals.Add(jv);
                    }
                        
                }
                return jurnals;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }

        public bool JournalVentesDatesAdd(ref int id, JournalventesDatesModel jvv)
        {

            JournalVentesDates jv = new JournalVentesDates();
            jv.ID = jvv.ID;
            jv.DateDebut = jvv.DateDebut;
            jv.DateFin = jvv.DateFin;
            jv.IdSite = jvv.IdSite;
            if (DAL.JournalVenteDatesADD(ref id, jv))
                return true;
            else return false;
        }


        public bool JournalVentesDELETE(int id)
        {
            if (DAL.JournalVenteDatesDELETE(id))
                return true;
            else return false;
        }
        #endregion
    }
}
