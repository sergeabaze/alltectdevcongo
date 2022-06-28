using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;

namespace AllTech.FrameWork.Model
{
    public class CompteModel : ViewModelBase
    {

        public int ID { get; set; }
        public int IDSite { get; set; }
        private string numeroCompte;
        private string nomBanque;
        private string ville;
        private string agence;
        private string rue;
        private string boitePostal;
        private string telephone;
        private string pays;
        private string quartier;

       

        Facturation DAL = null;

        public CompteModel() 
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
            ID = 0;
            numeroCompte = string.Empty;
        }

        #region PROPERTIES

        public string NumeroCompte
        {
            get { return numeroCompte; }
            set { numeroCompte = value;
            this.OnPropertyChanged("NumeroCompte");
            }
        }

        public string Quartier
        {
            get { return quartier; }
            set { quartier = value;
            this.OnPropertyChanged("Quartier");
            }
        }

        public string NomBanque
        {
            get { return nomBanque; }
            set
            {
                nomBanque = value;
                this.OnPropertyChanged("NomBanque");
            }
        }

        public string Ville
        {
            get { return ville; }
            set
            {
                ville = value;
                this.OnPropertyChanged("Ville");
            }
        }
        public string Agence
        {
            get { return agence; }
            set
            {
                agence = value;
                this.OnPropertyChanged("Agence");
            }
        }
        public string Rue
        {
            get { return rue; }
            set
            {
                rue = value;
                this.OnPropertyChanged("Rue");
            }
        }
        public string BoitePostal
        {
            get { return boitePostal; }
            set
            {
                boitePostal = value;
                this.OnPropertyChanged("BoitePostal");
            }
        }
        public string Telephone
        {
            get { return telephone; }
            set
            {
                telephone = value;
                this.OnPropertyChanged("Telephone");
            }
        }

        public string Pays
          {
              get { return pays; }
            set
            {
                pays = value;
                this.OnPropertyChanged("Pays");
            }
        }

      
        #endregion


        #region METHOD


        public List<CompteModel > COMPTE_SELECT()
        {
            List<CompteModel> comptes = new List<CompteModel>();

            try
            {

                List<Compte > devisefrom = DAL.GetAll_COMPTE ();
                if (devisefrom != null)
                {
                    foreach (var dev in devisefrom)
                        comptes.Add(Convertfrom(dev));
                }
                return comptes;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }

        public List<CompteModel> COMPTE_SELECT_Archive()
        {
            List<CompteModel> comptes = new List<CompteModel>();

            try
            {

                List<Compte> devisefrom = DAL.GetAll_COMPTEARchive();
                if (devisefrom != null)
                {
                    foreach (var dev in devisefrom)
                        comptes.Add(Convertfrom(dev));
                }
                return comptes;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }

        public List<CompteModel> COMPTE_SELECTBYSITE(Int32 idsite)
        {
            List<CompteModel> comptes = new List<CompteModel>();

            try
            {

                List<Compte> devisefrom = DAL.GetAll_COMPTESite (idsite );
                if (devisefrom != null)
                {
                    foreach (var dev in devisefrom)
                        comptes.Add(Convertfrom(dev));
                }
                return comptes;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }

        public List<CompteModel> COMPTE_SELECTBYSITE_Archive(Int32 idsite)
        {
            List<CompteModel> comptes = new List<CompteModel>();

            try
            {

                List<Compte> devisefrom = DAL.GetAll_COMPTESiteArchive(idsite);
                if (devisefrom != null)
                {
                    foreach (var dev in devisefrom)
                        comptes.Add(Convertfrom(dev));
                }
                return comptes;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }



        public CompteModel  Compte_SELECTById(int id)
        {
            CompteModel currentCompte = null;
            try
            {

                Compte  cpt = DAL.Get_COMPTEBYID (id);
                if (cpt != null)
                    currentCompte = Convertfrom(cpt);

                return currentCompte;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }


        public bool COMPTE_ADD(CompteModel  compte)
        {

            try
            {

                DAL.COMPTE_ADD(ConvertTo(compte));
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool COMPTE_DELETE(int id)
        {

            try
            {
                DAL.COMPTE_DELETE(id);
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        #endregion


        #region BUISNESS METHOD

        CompteModel Convertfrom(Compte  compte)
        {
            CompteModel comptes = null;
            if (compte != null)
                comptes = new CompteModel
                { 
                    ID = compte.ID, IDSite = compte.IDSite,
                    NumeroCompte = compte.NumeroCompte,
                    NomBanque = compte.NomBanque,
                    Ville = compte.Ville,
                    Agence = compte.Agence,
                    BoitePostal = compte.BoitePostal,
                    Pays = compte.Pays,
                    Rue = compte.Rue,
                    Telephone = compte.Telephone ,
                    Quartier =compte .Quartier 
                };
            return comptes;

        }

        Compte ConvertTo(CompteModel compte)
        {
            Compte mcompte = null;
            if (compte != null)
                mcompte = new Compte { 
                    ID = compte.ID, 
                    IDSite = compte.IDSite,
                    NumeroCompte = compte.NumeroCompte,
                    NomBanque = compte.NomBanque,
                    Ville = compte.Ville,
                    Agence = compte.Agence,
                    BoitePostal = compte.BoitePostal,
                    Pays = compte.Pays,
                    Rue = compte.Rue,
                    Telephone = compte.Telephone ,
                    Quartier =compte .Quartier 
                };
            return mcompte;
        }
        #endregion

    }
}
