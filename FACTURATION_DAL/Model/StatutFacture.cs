﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
   public  class StatutFacture
    {

        public int IdStatut { get; set; }
        public string Libelle { get; set; }
        public int IdLangue { get; set; }
        public string ShortName { get; set; }
        private Langue _llangue;

        public Langue Llangue
        {
            get { return _llangue; }
            set { _llangue = value; }
        }
    }
}
