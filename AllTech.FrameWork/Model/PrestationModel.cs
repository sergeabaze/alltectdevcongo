using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using System.Collections.ObjectModel;

namespace AllTech.FrameWork.Model
{
    public class PrestationModel : ViewModelBase
    {
        public int Id { get; set; }
        private string libelle;

        public PrestationModel()
        {

        }

        #region PROPERTIES

        public string Libelle
        {
            get { return libelle; }
            set
            {
                libelle = value;
                this.OnPropertyChanged("Libelle");
            }
        }
        #endregion

        #region METHODS

        public ObservableCollection<PrestationModel> Prestation_SELECT()
        {
            ObservableCollection<PrestationModel> Prestations = new ObservableCollection<PrestationModel>();
            try
            {
                return Prestations;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }


        public PrestationModel Prestation_SELECTBY_ID(int id)
        {
            PrestationModel Prestation = new PrestationModel();
            try
            {
                return Prestation;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool Prestation_INSERT(PrestationModel prestation)
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

        public bool Prestation_UPDATE(PrestationModel prestation)
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

        public bool Prestation_DELETE(int id)
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
