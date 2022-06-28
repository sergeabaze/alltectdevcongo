using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
   public  class ObjetFacture
    {

        public int IdObjet { get; set; }
        public Int32 IdobjetGen { get; set; }
        public string Libelle { get; set; }
        public Int32  IdClient { get; set; }
        

        private Client _client;
        public Client Client
        {
            get { return _client; }
            set { _client = value; }
        }
        private ObjetGenerique objetGeneric;

        public ObjetGenerique ObjetGeneric
        {
            get { return objetGeneric; }
            set { objetGeneric = value; }
        }
    }
}
