using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FACTURATION_DAL.Model;
using AllTech.FrameWork.PropertyChange;

namespace AllTech.FrameWork.Model
{
    public class CompteOhadaModel : ViewModelBase
    {

        private int id;
        private string libelle;
        private string code;
        private int idlibelleType;

      

          CompteOhada dale = null;

        public CompteOhadaModel()
        {
            dale = new CompteOhada(DataProviderObject.GetStringConnection);
           this.Id = 0;
           this.Libelle = string.Empty;
           this.Code = string.Empty;
           this.IdlibelleType = 0;
        }

        #region Region properties

        public int Id
        {
            get { return id; }
            set { id = value;
            this.OnPropertyChanged("Id");
            }
        }

        public int IdlibelleType
        {
            get { return idlibelleType; }
            set { idlibelleType = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value;
            this.OnPropertyChanged("Code");
            }
        }

        public string Libelle
        {
            get { return libelle; }
            set { libelle = value;
            this.OnPropertyChanged("Libelle");
            }
        }
        #endregion

        #region Region Methods

        public List<CompteOhadaModel> selectAll_Free(int idsite)
        {
            List<CompteOhadaModel> liste = new List<CompteOhadaModel>();
            List<CompteOhada> comptes = dale.SelectCmpt_Free(idsite);
            if (comptes != null)
            {
                foreach (CompteOhada cmp in comptes)
                {
                    CompteOhadaModel newCmpte = new CompteOhadaModel();
                    newCmpte.Id = cmp.Id;
                    newCmpte.Libelle = cmp.Libelle;
                    newCmpte.Code = cmp.Code;
                    newCmpte.IdlibelleType = cmp.IdLibelleType;
                    liste.Add(newCmpte);
                }
            }
            return liste;
        }

        public List<CompteLibelleOhadaModel> SelectCompteOhadaByElelmentId()
        {
            List<CompteLibelleOhadaModel> newListe = new List<CompteLibelleOhadaModel>();
            List<CompteLibelleOhadaModel> listeElement = selectAllLibelleTypeByCompteOhada();
            if (listeElement != null)
            {
                foreach (CompteLibelleOhadaModel cmpt in listeElement)
                {
                   
                    cmpt.CompteOhada = selectAllByLibelleTypeId(cmpt.ID);
                    newListe.Add(cmpt);
                }
            }

            return newListe;
        }

        public List<CompteLibelleOhadaModel> selectAllLibelleType()
        {
            List<CompteLibelleOhadaModel> comptes = new List<CompteLibelleOhadaModel>();
            List<ComptaOhadaLibelle> dalComptes = dale.SelectLibelleAll();
            if (dalComptes != null)
            {
                foreach (ComptaOhadaLibelle cmp in dalComptes)
                {
                    CompteLibelleOhadaModel newCmpte = new CompteLibelleOhadaModel();
                    newCmpte.ID = cmp.ID;
                    newCmpte.libelle = cmp.libelle;
                    comptes.Add(newCmpte);

                }
            }
            return comptes;
        }


        public List<CompteLibelleOhadaModel> selectAllLibelleTypeByCompteOhada()
        {
            List<CompteLibelleOhadaModel> comptes = new List<CompteLibelleOhadaModel>();
            List<ComptaOhadaLibelle> dalComptes = dale.SelectLibelleAllByCompteOhada();
            if (dalComptes != null)
            {
                foreach (ComptaOhadaLibelle cmp in dalComptes)
                {
                    CompteLibelleOhadaModel newCmpte = new CompteLibelleOhadaModel();
                    newCmpte.ID = cmp.ID;
                    newCmpte.libelle = cmp.libelle;
                    comptes.Add(newCmpte);

                }
            }
            return comptes;
        }



        public List<CompteOhadaModel> selectAllByLibelleTypeId(int idLibelleId)
        {
            List<CompteOhadaModel> comptes = new List<CompteOhadaModel>();
            List<CompteOhada> dalComptes = dale.SelectAllByLibelleId(idLibelleId);
            if (dalComptes != null)
            {
                foreach (CompteOhada cmp in dalComptes)
                {
                    CompteOhadaModel newCmpte = new CompteOhadaModel();
                    newCmpte.Id = cmp.Id;
                    newCmpte.Libelle = cmp.Libelle;
                    newCmpte.Code = cmp.Code;
                    newCmpte.IdlibelleType = cmp.IdLibelleType;
                    comptes.Add(newCmpte);

                }
            }
            return comptes;
        }


       public  List<CompteOhadaModel> selectAll(int idSite)
        {
            List<CompteOhadaModel> comptes = new List<CompteOhadaModel>();
            List<CompteOhada> dalComptes = dale.SelectAll(idSite);
            if (dalComptes != null)
            {
                foreach (CompteOhada cmp in dalComptes)
                {
                    CompteOhadaModel newCmpte = new CompteOhadaModel();
                    newCmpte.Id = cmp.Id;
                    newCmpte.Libelle = cmp.Libelle;
                    newCmpte.Code = cmp.Code;
                    newCmpte.IdlibelleType = cmp.IdLibelleType;
                    comptes.Add(newCmpte);

                }
            }
            return comptes;
        }

       public List<CompteOhadaModel> selectLibelleAllByElelementId(int idcompteElementId)
       {
           List<CompteOhadaModel> comptes = new List<CompteOhadaModel>();
           List<CompteOhada> dalComptes = dale.SelectAllByLibelleId(idcompteElementId);
           if (dalComptes != null)
           {
               foreach (CompteOhada cmp in dalComptes)
               {
                   CompteOhadaModel newCmpte = new CompteOhadaModel();
                   newCmpte.Id = cmp.Id;
                   newCmpte.Libelle = cmp.Libelle;
                   newCmpte.Code = cmp.Code;
                   newCmpte.IdlibelleType = cmp.IdLibelleType;
                   comptes.Add(newCmpte);

               }
           }
           return comptes;
       }


        public CompteOhadaModel selectById(int id)
        {
            CompteOhadaModel newCmpte = null;
            CompteOhada daleCompte = dale.SelectByid(id);
            if (daleCompte != null)
            {
                newCmpte = new CompteOhadaModel();
                newCmpte.Id = daleCompte.Id;
                newCmpte.Libelle = daleCompte.Libelle;
                newCmpte.Code = daleCompte.Code;
                newCmpte.IdlibelleType = daleCompte.IdLibelleType;
            }
            return newCmpte;
        }

        public bool Insert(CompteOhadaModel compte, int idsite)
        {
            CompteOhada newCmpte = new CompteOhada();

            newCmpte.Id = compte.Id;
            newCmpte.Libelle = compte.Libelle;
            newCmpte.Code = compte.Code;
            newCmpte.IdLibelleType = compte.IdlibelleType;
            dale.Insert(newCmpte, idsite);

            return true;
        }

        public bool CompteElement_Insert(CompteLibelleOhadaModel compte,int idsite)
        {
            ComptaOhadaLibelle newCmpte = new ComptaOhadaLibelle();

            newCmpte.ID = compte.ID;
            newCmpte.libelle = compte.libelle;
            dale.LibelleType_Add(newCmpte);

            return true;
        }

        public bool Update(CompteOhadaModel compte)
        {
            CompteOhada newCmpte = new CompteOhada();

            newCmpte.Id = compte.Id;
            newCmpte.Libelle = compte.Libelle;
            newCmpte.Code = compte.Code;
            newCmpte.IdLibelleType = compte.IdlibelleType;
            dale.Update(newCmpte);
            return true;
        }

        public bool Delete(int id)
        {
            dale.Delete(id);
            return true;
        }

        public bool CompteElement_Delete(int id)
        {
            dale.LibelleType_Delete(id);
            return true;
        }

        #endregion

    }


    public class CompteLibelleOhadaModel
    {
        public int ID { get; set; }
        public string libelle { get; set; }
        private List<CompteOhadaModel> compteOhada;

        public List<CompteOhadaModel> CompteOhada
        {
            get { return compteOhada; }
            set { compteOhada = value; }
        }
    }
}
