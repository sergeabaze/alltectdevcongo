using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
  public   class ProfileDate
    {
        public int Id { get; set; }
        public DateTime Datedebut { get; set; }
        public DateTime DateFin { get; set; }
        public int IdProfile { get; set; }
        public int idUser { get; set; }
    }
}
