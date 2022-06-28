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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AllTech.FacturationModule.ViewModel;
using AllTech.FrameWork.Global;

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : UserControl
    {
      
        public UserProfile()
        {
            InitializeComponent();
           // DataRefUtilisateurViewModel _viewModel = new DataRefUtilisateurViewModel(GlobalDatas.MainWindow);
            this.DataContext = GlobalDatas.ViewModeluser as DataRefUtilisateurViewModel;
           // viewModel = _viewModel;
            double localHeight = (GlobalDatas.mainHeight - 460);
           // optionProfilUsers.Height = (localHeight * 0.70)-5;
          // rightEdgeDock.Height = localHeight * 0.70;

           // rightEdgePropertiesDock.Height = localHeight * 0.40;
           // gridSousProfile.Height = (localHeight * 0.40) - 5;
            //
            
        }
    }
}
