using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using AllTech.FacturationModule.Views;
using AllTech.FrameWork.Command;
using System.Windows.Input;
using System.Windows;
using AllTech.FrameWork.Model;

namespace AllTech.FacturationModule.ViewModel
{
    public class WFacturationModal_viewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;
       // private IInjectSingleViewService _injectSingleViewService;
       // Facturation_view _SelectedFactureView;
        FactureModel _factureSelected;

        private RelayCommand newCommand;
        private RelayCommand saveCommand;


        public WFacturationModal_viewModel()
       {
           //_regionManager = regionManager;
           //_container = container;
          // Facturation_view View = new Facturation_view (null ) ;//_container.Resolve<Facturation_view>();
           //Facturation_view view = new Facturation_view();
            //SelectedFactureView = View;
       }

        //public Facturation_view SelectedFactureView
        //{
        //    get { return _SelectedFactureView; }
        //    set { _SelectedFactureView = value;
        //    this.OnPropertyChanged("SelectedFactureView");
        //    }
        //}

        public FactureModel FactureSelected
        {
            get { return _factureSelected; }
            set { _factureSelected = value;
            this.OnPropertyChanged("FactureSelected");
            }
        }

      
      

    }
}
