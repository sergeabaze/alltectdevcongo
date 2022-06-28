using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;

namespace AllTech.FrameWork.Model
{
    public class SocieteModel : ViewModelBase
    {

        public int IdSociete { get; set; }
        private string libelle;
        private string raisonSocial;
        private string titreManager;
        private string nomManager;
        private string numContribualbe;
        private string numImmatriculation;
        private string pays;
        private string ville;
        private string adresse_2;
        private string adesse_1;
        private string boitePostal;
        private string telephone;
        private string faxe;
        private string siteIntenet;
        private string sigleSite;
        private string capitalSte;
        private string rc;
        private string numeroNif;
        private string regime;
        private string taxeNu;

      
        private  byte[] logo;
        private byte[] logoPiedPage;

     

        


       

        Facturation DAL = null;

        public SocieteModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
        }

        #region PROPERTIES

        public string NumeroNif
        {
            get { return numeroNif; }
            set { numeroNif = value;
            this.OnPropertyChanged("NumeroNif");
            }
        }

        public string TaxeNu
        {
            get { return taxeNu; }
            set { taxeNu = value;
            this.OnPropertyChanged("TaxeNu");
            }
        }

        public string Regime
        {
            get { return regime; }
            set { regime = value;
            this.OnPropertyChanged("Regime");
            }
        }

        public string Rc
        {
            get { return rc; }
            set { rc = value;
            this.OnPropertyChanged("Rc");
            }
        }

        public string Libelle
        {
            get { return libelle; }
            set { libelle = value;
            this.OnPropertyChanged("Libelle");
            }
        }

        public string CapitalSte
        {
            get { return capitalSte; }
            set { capitalSte = value;
            this.OnPropertyChanged("CapitalSte");
            }
        }

        public byte[] Logo
        {
            get { return logo ; }
            set { logo  = value;
            this.OnPropertyChanged("Logo");
            }
        }

        public byte[] LogoPiedPage
        {
            get { return logoPiedPage; }
            set { logoPiedPage = value;
            this.OnPropertyChanged("LogoPiedPage");
            }
        }


        public string SigleSite
        {
            get { return sigleSite; }
            set { sigleSite = value;
            this.OnPropertyChanged("SigleSite");
            }
        }


        public string RaisonSocial
        {
            get { return raisonSocial; }
            set { raisonSocial = value;
            this.OnPropertyChanged("RaisonSocial");
            }
        }


        public string TitreManager
        {
            get { return titreManager; }
            set { titreManager = value;
            this.OnPropertyChanged("TitreManager");
            }
        }


        public string NomManager
        {
            get { return nomManager; }
            set { nomManager = value;
            this.OnPropertyChanged("NomManager");
            }
        }


        public string NumContribualbe
        {
            get { return numContribualbe; }
            set { numContribualbe = value;
            this.OnPropertyChanged("NumContribualbe");
            }
        }


        public string NumImmatriculation
        {
            get { return numImmatriculation; }
            set { numImmatriculation = value;
            this.OnPropertyChanged("NumImmatriculation");
            }
        }




        public string Pays
        {
            get { return pays; }
            set { pays = value;
            this.OnPropertyChanged("Pays");
            }
        }


        public string Ville
        {
            get { return ville; }
            set { ville = value;
            this.OnPropertyChanged("Ville");
            }
        }


        public string Adesse_1
        {
            get { return adesse_1; }
            set { adesse_1 = value;
            this.OnPropertyChanged("Adesse_1");
            }
        }


        public string Adresse_2
        {
            get { return adresse_2; }
            set { adresse_2 = value;
            this.OnPropertyChanged("Adresse_2");
            }
        }


        public string BoitePostal
        {
            get { return boitePostal; }
            set { boitePostal = value;
            this.OnPropertyChanged("BoitePostal");
            }
        }


        public string Telephone
        {
            get { return telephone; }
            set { telephone = value;
            this.OnPropertyChanged("Telephone");
            }
        }


        public string Faxe
        {
            get { return faxe; }
            set { faxe = value;
            this.OnPropertyChanged("Faxe");
            }
        }


        public string SiteIntenet
        {
            get { return siteIntenet; }
            set { siteIntenet = value;
            this.OnPropertyChanged("SiteIntenet");
            }
        }
        #endregion

        #region METHODS

        public byte[] Get_SOCIETE_SIGNATURE()
        {
            
            try
            {
                byte[] signature = DAL.Get_SOCIETE_SIGNATURE ();


                return signature;
            }
            catch (Exception ex)
            {
                throw new DALException(ex.Message);
            }
        }


        public bool SOCIETE_SIGNATURE_ADD(int id,byte[] signature)
        {
            try
            {

                DAL.SOCIETE_ADD_SIGNATURE(id, signature);
                return true;
            }
            catch (Exception ex)
            {
                throw new DALException(ex.Message);
            }
        }

        public bool SOCIETE_SIGNATURE_DELETE(int id)
        {
            try
            {

                DAL.SOCIETE_DELETE_SIGNATURE(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new DALException(ex.Message);
            }
        }



        public SocieteModel Get_SOCIETE_DEFAULT()
        {
            SocieteModel currentSociete=null ;
            try
            {
                Societe societe = DAL.Get_SOCIETE_FEFAULT();
                if (societe != null)
                    currentSociete = ConvertFrom(societe);
                return currentSociete;
            }
            catch (Exception ex)
            {
                throw new DALException(ex.Message);
            }
        }


        public SocieteModel Get_SOCIETE_BYID(int id)
        {
            SocieteModel currentSociete = null;
            try
            {
                Societe societe = DAL.Get_SOCIETEById (id );
                if (societe != null)
                    currentSociete = ConvertFrom(societe);
                return currentSociete;
            }
            catch (Exception ex)
            {
                throw new DALException(ex.Message);
            }
        }


        public List<SocieteModel> Get_SOCIETE_LIST()
        {
            List<SocieteModel> currentSociete = new List<SocieteModel>(); ;
            try
            {
               List <Societe> societe = DAL.GetAll_SOCIETE_LIST ();
                if (societe != null)
                    foreach (Societe st in societe )
                    currentSociete.Add (ConvertFrom(st));
                return currentSociete;
            }
            catch (Exception ex)
            {
                throw new DALException(ex.Message);
            }
        }



        public bool SOCIETE_ADD(SocieteModel societe)
        {
            try
            {
                if (societe != null)
                    DAL.SOCIETE_ADD(ConverTo(societe));
                return true;
            }
            catch (Exception ex) 
            {
                throw new DALException(ex.Message);
            }
        }

        public bool SOCIETE_DELETE_LOGO(int type, int idSte)
        {
            try
            {

                DAL.SOCIETE_DELETE_LOGO (type,idSte );
                return true;
            }
            catch (Exception ex)
            {
                throw new DALException(ex.Message);
            }
        }


        public bool SOCIETE_DELETE()
        {
            try
            {
                
                    DAL.SOCIETE_DELETE ();
                return true;
            }
            catch (Exception ex)
            {
                throw new DALException(ex.Message);
            }
        }

        #endregion

        #region BUISNESS METHOD

        SocieteModel ConvertFrom(Societe societe)
        {
            SocieteModel so = null;
            if (societe != null)
                so = new SocieteModel { 
                    IdSociete = societe.IdSociete, 
                    Adesse_1 = societe.Adesse_1,
                    Adresse_2 = societe.Adresse_2,
                    BoitePostal = societe.BoitePostal,
                    Faxe = societe.Faxe,
                    Libelle = societe.Libelle,
                    NomManager = societe.NomManager,
                    NumContribualbe = societe.NumContribualbe,
                    NumImmatriculation = societe.NumImmatriculation,
                    Pays = societe.Pays,
                    RaisonSocial = societe.RaisonSocial,
                    SiteIntenet = societe.SiteIntenet,
                    Telephone = societe.Telephone,
                    TitreManager = societe.TitreManager,
                    Ville = societe.Ville, SigleSite =societe .SigleSite ,
                    Logo = societe.LogoSociete , 
                    LogoPiedPage =societe .LogoPiedPage , 
                    CapitalSte =societe .Capital ,
                    Rc = societe.Rc,
                    NumeroNif = societe.NumeroNif,
                    Regime = societe.Regime,
                    TaxeNu = societe.TaxeNu
                   
                };
            return so;
        }

        Societe ConverTo(SocieteModel societe)
        {
            Societe so = null;
            if (societe != null)
                so = new Societe { 
                    IdSociete = societe.IdSociete,
                    Ville = societe.Ville,
                    TitreManager = societe.TitreManager,
                    Telephone = societe.Telephone,
                    SiteIntenet = societe.SiteIntenet,
                    RaisonSocial = societe.RaisonSocial,
                    Pays = societe.Pays,
                    NumImmatriculation = societe.NumImmatriculation,
                    NumContribualbe = societe.NumContribualbe,
                    NomManager = societe.NomManager,
                    Libelle = societe.Libelle,
                    Faxe = societe.Faxe,
                    Adesse_1 = societe.Adesse_1,
                    Adresse_2 = societe.Adresse_2,
                    BoitePostal = societe.BoitePostal , 
                    SigleSite =societe .SigleSite ,
                     LogoSociete =societe .Logo  ,
                     LogoPiedPage =societe .LogoPiedPage , 
                     Capital =societe .CapitalSte ,
                     Rc =societe.Rc ,
                    NumeroNif = societe.NumeroNif,
                    Regime = societe.Regime,
                    TaxeNu = societe.TaxeNu

                };
            return so;

        }

        #endregion
    }
}
