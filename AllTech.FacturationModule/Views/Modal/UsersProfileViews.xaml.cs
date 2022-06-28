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
using AllTech.FrameWork.Global;
using AllTech.FacturationModule.ViewModel;

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for UsersProfileViews.xaml
    /// </summary>
    public partial class UsersProfileViews : UserControl
    {
        public UsersProfileViews()
        {
            InitializeComponent();
            this.DataContext = GlobalDatas.ViewModeluser as DataRefUtilisateurViewModel;
            // viewModel = _viewModel;
            double localHeight = (GlobalDatas.mainHeight - 460);
        }

        private void profileVuesGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

      
    }
}
