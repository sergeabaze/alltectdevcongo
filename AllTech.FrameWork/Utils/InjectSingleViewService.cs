using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity  ;
using System.Windows.Controls;
using AllTech.FrameWork.Utils;


namespace AllTech.FrameWork.Services
{
    public class InjectSingleViewService :ObservableBase, IInjectSingleViewService
    {
        IRegionManager _regionManager;
        IUnityContainer _container;

        public InjectSingleViewService(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }


        public void RegisterViewForRegion(string viewName, string regionName, Type viewType)
        {
            object view = _container.Resolve(viewType);
            IRegion region = _regionManager.Regions[regionName];
            region.Add(view, viewName);
            region.Activate(view);
        }

        public void ClearViewFromRegion(string viewName, string regionName)
        {
            IRegion region = _regionManager.Regions.FirstOrDefault(r => r.Name == regionName);
            if (region == null) return;
            object view = region.GetView(viewName);
            if (view == null) return;
            region.Remove(view);

        }

        public void ClearViewsFromRegion(string regionName)
        {
            IRegion region = _regionManager.Regions.FirstOrDefault(r => r.Name == regionName);
            if (region == null)
                return;

            IViewsCollection views = region.Views;
            if (views == null) return;
            List<object> viewList = views.ToList();
            for (int i = viewList.Count() - 1; i >= 0; i--)
            {
                
                UserControl userControl = (UserControl)viewList[i];
                List<string> regions = RegionControlHelper.GetRegions(userControl.GetType());
               
                if (regions != null)
                {
                    foreach (var innerRegion in regions)
                    {
                        ClearViewsFromRegion(innerRegion);
                    }
                }
                region.Remove(viewList[i]);
            }
        }



    }
}
