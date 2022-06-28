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
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using AllTech.FrameWork.Model;

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for WmodalDatarefs.xaml
    /// </summary>
    public partial class WmodalDatarefs : Window
    {
        DataReferenceViewModel viewModel;
        public static ClientModel client;
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public WmodalDatarefs(IRegionManager regionManager, IUnityContainer container,ClientModel _client)
        {
            InitializeComponent();
            _regionManager = regionManager;
            _container = container;
            client =_client ;
           // viewModel=new DataReferenceViewModel (
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            {
                this.DialogResult = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

          //  DataReference_Views view = new DataReference_Views(_regionManager, _container, client);
           // modalview.Content = view;
        }
    }
}
