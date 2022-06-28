using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace AllTech.FrameWork.Services
{
    public interface IInjectSingleViewService : INotifyPropertyChanged 
    {
        void RegisterViewForRegion( string viewName, string regionName, Type viewType);
        void ClearViewFromRegion(string viewName, string regionName);
        void ClearViewsFromRegion(string regionName);
        

    }
}
