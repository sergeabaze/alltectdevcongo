using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using AllTech.FrameWork.Region;
using AllTech.FacturationModule.Views;

namespace AllTech.FacturationModule
{
    public class ModuleFactureInit : IModule
    {

        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public ModuleFactureInit(IUnityContainer container, IRegionManager regionManager)
        {
            this.container     = container;
            this.regionManager = regionManager;
        
        }

        public void Initialize()
        {

            this.regionManager.RegisterViewWithRegion(RegionNames.ViewRegion,
                                                       () => this.container.Resolve<DataRefUtilisateur>());
        }


    }
}
