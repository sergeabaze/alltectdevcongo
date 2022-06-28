using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllTech.FrameWork.Utils
{
   public  class EventRefreshGridHistoric
    {
        public delegate void MyEventHandler(object sender, EventArgs e);
        public static event MyEventHandler EventRefreshList;
        public object  facture=null ;
        public string typeOperation = string.Empty;

        public void OnChangeList(EventArgs e)
        {
            EventRefreshList(this, e);
        }
    }
}
