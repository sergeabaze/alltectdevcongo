using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using AllTech.FrameWork.Model;

namespace AllTech.FrameWork.Utils
{
  public   class EventClientNonFacturees
    {

      public delegate void MyEventHandler(object sender, EventArgs e);
      public static event MyEventHandler eventClientNonFacturees;
      public ObservableCollection<ClientModel> Clients;

      public void OnChangeShowList(EventArgs e)
      {
          eventClientNonFacturees(this, e);
      }

    }
}
