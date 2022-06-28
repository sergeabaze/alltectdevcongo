using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;

namespace AllTech.FrameWork.Model
{

    public class LibelleModel : ViewModelBase
    {
        public long IdLibelle { get; set; }
        public string lib_numFact { get; set; }
        public string lib_objtFact { get; set; }
        public string lib_datefact { get; set; }
        public string lib_client { get; set; }
        public string lib_prepare { get; set; }
        
        public int  Idlangue;


        public LibelleModel()
        {

        }

        #region PROPERTIES
        
        #endregion

       

        #region METHODS

        public LibelleModel GetLIBELLEBy_IDLangue(int idlangue)
        {
            LibelleModel liste = new LibelleModel();
            List<LibelleModel> listes = new List<LibelleModel>();
            listes.Add(new LibelleModel { IdLibelle = 1, lib_numFact="Numemro Fact" , lib_client ="Client", lib_datefact ="Date Facture", lib_objtFact ="Objet Facture", lib_prepare ="Preparé Par" ,Idlangue = 1 });
            listes.Add(new LibelleModel { IdLibelle = 2, lib_numFact = "Invoice Num", lib_client = "Customer", lib_datefact = "Date invoice", lib_objtFact = "Object ", lib_prepare = "Prepared by", Idlangue = 2 });
            return listes.First (l=>l .Idlangue ==idlangue);
        }

        #endregion


    }
}
