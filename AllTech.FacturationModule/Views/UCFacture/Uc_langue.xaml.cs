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
using AllTech.FrameWork.Model;

namespace AllTech.FacturationModule.Views.UCFacture
{
    /// <summary>
    /// Interaction logic for Uc_langue.xaml
    /// </summary>
    public partial class Uc_langue : UserControl
    {
        LangueVieModel localviewModel;
        public Uc_langue()
        {
            InitializeComponent();
            LangueVieModel vieModel = new LangueVieModel();
            localviewModel = vieModel;
            this.DataContext = vieModel;
            
        }

        private void LanguageView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.localviewModel.LanguageSelected = LanguageView.SelectedItem as LangueModel;
        }
    }
}
