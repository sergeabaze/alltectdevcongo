using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllTech.FrameWork.Model
{
   public  class JournalVenteCmptAnalityqueViewModel
    {

        public int ID_Client { get; set; }
        public int IDCompteAnal { get; set; }
        public string NumeroCmptAnal { get; set; }
        public DateTime Datefacture { get; set; }
        public string Numerofacture { get; set; }
        public double MontantFacture { get; set; }
        public string LibelleMontant { get; set; }
        public int ID_Datejournal { get; set; }
    }
}
