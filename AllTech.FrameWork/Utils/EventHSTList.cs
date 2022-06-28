using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllTech.FrameWork.Utils
{
   public  class EventHSTList
    {
        public delegate void MyEventHandler(object sender, EventArgs e);
        public static event MyEventHandler EventRefreshList;
        public string id;

        public void OnChangeList(EventArgs e)
        {
            EventRefreshList(this, e);
        }
    }
}
