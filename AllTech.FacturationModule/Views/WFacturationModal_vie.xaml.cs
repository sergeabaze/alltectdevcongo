using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AllTech.FacturationModule.ViewModel;
using AllTech.FrameWork.Utils;
using AllTech.FrameWork.Model;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for WFacturationModal_vie.xaml
    /// </summary>
    public partial class WFacturationModal_vie : Window
    {
        FactureModel _factureSelected;
        //Facturation_view view = null;
        public FactureModel actualFacture=null ;
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        bool btnCloseVisible;

     

        public WFacturationModal_vie(FactureModel currentFacture, IRegionManager regionManager, IUnityContainer container)
        {
           // WFacturationModal_viewModel viewModel = new WFacturationModal_viewModel();
            InitializeComponent();
            _factureSelected = currentFacture;
            _regionManager = regionManager;
            _container = container;
            
           // this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            {
                this.DialogResult = true;
            }
        }

        public FactureModel FactureSelected
        {
            get { return _factureSelected; }
            set { _factureSelected = value; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // view = new Facturation_view(_factureSelected, _regionManager, _container);
           // modalview.Content = view;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            //actualFacture = view.currentFacture;
          
        }

     
    }
}
