using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using System.Collections.ObjectModel;

namespace AllTech.FrameWork.Model
{
    public class ParametresModel : ViewModelBase
    {

        public int Id { get; set; }
        private string libelle;
        private string typeBd;
        private string nomserveur;
        private string nomBd;
        private string port;
        private string utilisateur;
        private string motpasse;

        private string deaultUser;
        private int idtva;
        private string deaultPassword;
        private int idDevise;
        private string codeCourant;
        private string dejautiliser;
        private string dureeMode;
        private string defaulLanguage;
        string dureeDev;
        string dureeProd;
        string dureeTeste;
        string dureeLocaton;

        private string dossierImages;
        private string paginationHtrc;
        private string pathLog;
        private string pathBackUpLog;
        private string mailFrom;
        private string mailTo;
        private string smtp;
        private string portSmtp;
        private string logginSmtp;
        private string passwordSmtp;

        private string nomserveurArchive;
        private string motpasseArchive;
        private string nomBdArchive;
        private string portArchive;
        private string utilisateurArchive;
       
        private string cheminFichierPath;
        private string codeJournalVente;
        private string pathJournalVente;
        private int jourLimiteFacturation;
        private string nomFichierExport;

      
     

      

        private bool jvModeSelect;
       

       
    

        public ParametresModel()
        {
            port = "3306";
            utilisateur = "root";
        }


        #region PROPERTIES

        public string NomFichierExport
        {
            get { return nomFichierExport; }
            set { nomFichierExport = value;
            this.OnPropertyChanged("NomFichierExport");
            }
        }

        public int JourLimiteFacturation
        {
            get { return jourLimiteFacturation; }
            set { jourLimiteFacturation = value;
            this.OnPropertyChanged("JourLimiteFacturation");
            }
        }

        public bool JvModeSelect
        {
            get { return jvModeSelect; }
            set { jvModeSelect = value;
            this.OnPropertyChanged("JvModeSelect");
            }
        }

        public string NomserveurArchive
        {
            get { return nomserveurArchive; }
            set
            {
                nomserveurArchive = value;
                this.OnPropertyChanged("NomserveurArchive");
            }
        }


        public string NomBdArchive
        {
            get { return nomBdArchive; }
            set
            {
                nomBdArchive = value;
                this.OnPropertyChanged("NomBdArchive");
            }
        }


        public string PortArchive
        {
            get { return portArchive; }
            set
            {
                portArchive = value;
                this.OnPropertyChanged("PortArchive");
            }
        }


        public string UtilisateurArchive
        {
            get { return utilisateurArchive; }
            set
            {
                utilisateurArchive = value;
                this.OnPropertyChanged("PortArchive");
            }
        }


        public string MotpasseArchive
        {
            get { return motpasseArchive; }
            set
            {
                motpasseArchive = value;
                this.OnPropertyChanged("MotpasseArchive");
            }
        }

        public string PathJournalVente
        {
            get { return pathJournalVente; }
            set { pathJournalVente = value;
            this.OnPropertyChanged("PathJournalVente");
            }
        }

        public string CodeJournalVente
        {
            get { return codeJournalVente; }
            set
            {
                codeJournalVente = value;
                this.OnPropertyChanged("CodeJournalVente");
            }
        }
        public string DefaulLanguage
        {
            get { return defaulLanguage; }
            set
            {
                defaulLanguage = value;
                this.OnPropertyChanged("DefaulLanguage");
            }
        }

        public string PasswordSmtp
        {
            get { return passwordSmtp; }
            set { passwordSmtp = value;
            this.OnPropertyChanged("PasswordSmtp");
            }
        }

        public string LogginSmtp
        {
            get { return logginSmtp; }
            set { logginSmtp = value;
            this.OnPropertyChanged("LogginSmtp");
            }
        }

        public string CheminFichierPath
        {
            get { return cheminFichierPath; }
            set { cheminFichierPath = value;
            this.OnPropertyChanged("CheminFichierPath");
            }
        }

        public string PortSmtp
        {
            get { return portSmtp; }
            set { portSmtp = value;
            this.OnPropertyChanged("PortSmtp");
            }
        }

        public string Smtp
        {
            get { return smtp; }
            set { smtp = value;
            this.OnPropertyChanged("Smtp");
            }
        }

        public string MailFrom
        {
            get { return mailFrom; }
            set { mailFrom = value;
            this.OnPropertyChanged("MailFrom");
            }
        }

        public string MailTo
        {
            get { return mailTo; }
            set { mailTo = value;
            this.OnPropertyChanged("MailTo");
            }
        }


        public string PathBackUpLog
        {
            get { return pathBackUpLog; }
            set { pathBackUpLog = value;
            this.OnPropertyChanged("PathBackUpLog");
            }

        }
        public string PathLog
        {
            get { return pathLog; }
            set { pathLog = value;
            this.OnPropertyChanged("PathLog");
            }
        }

        public string DossierImages
        {
            get { return dossierImages; }
            set { dossierImages = value;
            this.OnPropertyChanged("DossierImages");
            }
        }


        public string PaginationHtrc
        {
            get { return paginationHtrc; }
            set { paginationHtrc = value;
            this.OnPropertyChanged("PaginationHtrc");
            }
        }
     

        public string DureeDev
        {
            get { return dureeDev; }
            set { dureeDev = value;
            this.OnPropertyChanged("DureeDev");
            }
        }
    

        public string DureeProd
        {
            get { return dureeProd; }
            set { dureeProd = value;
            this.OnPropertyChanged("DureeProd");
            }
        }
    
        public string DureeTeste
        {
            get { return dureeTeste; }
            set { dureeTeste = value;
            this.OnPropertyChanged("DureeTeste");
            }
        }
      

        public string DureeLocaton
        {
            get { return dureeLocaton; }
            set { dureeLocaton = value;
            this.OnPropertyChanged("DureeLocaton");
            }
        }



        public string DureeMode
        {
            get { return dureeMode; }
            set { dureeMode = value;
            this.OnPropertyChanged("DureeMode");
            }
        }


        public string CodeCourant
        {
            get { return codeCourant; }
            set { codeCourant = value;
            this.OnPropertyChanged("CodeCourant");
            }
        }


        public string Dejautiliser
        {
            get { return dejautiliser; }
            set { dejautiliser = value;
            this.OnPropertyChanged("Dejautiliser");
            }
        }
      

        public string Motpasse
        {
            get { return motpasse; }
            set
            {
                motpasse = value;
                this.OnPropertyChanged("Motpasse");
            }
        }

        public string Utilisateur
        {
            get { return utilisateur; }
            set
            {
                utilisateur = value;
                this.OnPropertyChanged("Utilisateur");
            }
        }


        public string Libelle
        {
            get { return libelle; }
            set
            {
                libelle = value;
                this.OnPropertyChanged("Libelle");
            }
        }

        public string TypeBd
        {
            get { return typeBd; }
            set
            {
                typeBd = value;
                this.OnPropertyChanged("TypeBd");
            }
        }

        public string Nomserveur
        {
            get { return nomserveur; }
            set
            {
                nomserveur = value;
                this.OnPropertyChanged("Nomserveur");
            }
        }

        public string NomBd
        {
            get { return nomBd; }
            set
            {
                nomBd = value;
                this.OnPropertyChanged("NomBd");
            }
        }

        public string Port
        {
            get { return port; }
            set
            {
                port = value;
                this.OnPropertyChanged("Port");
            }
        }

        public string DeaultUser
        {
            get { return deaultUser; }
            set { deaultUser = value;
            this.OnPropertyChanged("DeaultUser");
            }
        }


        public string DeaultPassword
        {
            get { return deaultPassword; }
            set { deaultPassword = value;
            this.OnPropertyChanged("DeaultPassword");
            }
        }



        public int IdDevise
        {
            get { return idDevise; }
            set { idDevise = value;
            this.OnPropertyChanged("IdDevise");
            }
        }

        public int Idtva
        {
            get { return idtva; }
            set { idtva = value;
            this.OnPropertyChanged("Idtva");
            }
        }
        #endregion

        #region METHODS

        public ObservableCollection<ParametresModel> Parametres_SELECT()
        {
            ObservableCollection<ParametresModel> parametress = new ObservableCollection<ParametresModel>();
            try
            {
                return parametress;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }


        public ParametresModel Parametres_SELECTBY_ID(int id)
        {
            ParametresModel parametres = new ParametresModel();
            try
            {
                return parametres;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool Parametres_INSERT(ParametresModel parametres)
        {

            try
            {
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool Parametres_UPDATE(ParametresModel parametres)
        {

            try
            {
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool Parametres_DELETE(int id)
        {

            try
            {
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }



        #region Protected Methods

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion
        #endregion

       
    }

    
}
