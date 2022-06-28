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
    /// Interaction logic for Uc_statut.xaml
    /// </summary>
    public partial class Uc_statut : UserControl
    {
        StatutViewModel localviewModel;
        public Uc_statut()
        {
            InitializeComponent();
            StatutViewModel viewModel = new StatutViewModel();
            localviewModel = viewModel;
            this.DataContext = viewModel;
        }

        private void DetailView_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            
            this.localviewModel.StatutSelected  = DetailView.SelectedItem  as StatutModel ;
            if (this.localviewModel.StatutSelected != null)
            {
                if (localviewModel.StatutList != null)
                {
                    int i = 0;
                    foreach (var obj in localviewModel.StatutList)
                    {
                        if (obj.IdLangue == this.localviewModel.StatutSelected.IdLangue)
                        {
                           // cmblanguestat.SelectedIndex = i;
                            break;
                        }
                        i++;
                    }
                }
            
        }

        }
    }
}
