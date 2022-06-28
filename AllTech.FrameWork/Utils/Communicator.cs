using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using AllTech.FrameWork.Model;

namespace AllTech.FrameWork.Utils
{
   public  class Communicator
    {
          public delegate void MyEventHandler(object sender, EventArgs e);
          public static event MyEventHandler userControlName;
          public static event MyEventHandler eventjourLimite;
          public static event MyEventHandler eventClientNonFacturees;
          public static event MyEventHandler eventCleartxtqty;
          public static event MyEventHandler eventCloseWindow;
          public static event MyEventHandler eventCloseMainView;
        
          public string contentVue ;
          public string Message;
          public ObservableCollection<ClientModel> Clients;

         public void OnChangeText(EventArgs e)
          {
              userControlName(this, e);
          }

         public void OnChangePopUp(EventArgs e)
         {
             eventjourLimite(this, e);
         }

         public void OnChangeShowList(EventArgs e)
         {
             eventClientNonFacturees(this, e);
         }

         public void OnChangeClearQuantity(EventArgs e)
         {
             eventCleartxtqty(this, e);
         }

         public void OnChangeCloseWindow(EventArgs e)
         {
             eventCloseWindow(this, e);
         }

         public void OnChangeCloseView(EventArgs e)
         {
             eventCloseMainView(this, e);
         }

    }
}
