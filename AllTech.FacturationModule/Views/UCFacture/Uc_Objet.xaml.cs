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
    /// Interaction logic for Uc_Objet.xaml
    /// </summary>
    public partial class Uc_Objet : UserControl
    {
        ObjetViewModel localviewModel;
        public Uc_Objet()
        {
            InitializeComponent();
            ObjetViewModel viemodel = new ObjetViewModel();
            localviewModel = viemodel;
            this.DataContext = viemodel;
        }

        private void lstObjet_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.localviewModel.Objetselected = lstObjet.SelectedItem as ObjetGenericModel;
            if (this.localviewModel.Objetselected != null)
            {
                if (localviewModel.LanguageList != null)
                {
                    int i = 0;
                    foreach (var obj in localviewModel.LanguageList)
                    {
                        if (obj.Id == this.localviewModel.Objetselected.IdLangue)
                        {
                            cmblangue.SelectedIndex = i;
                            break;
                        }
                        i++;
                    }
                }
            }
        }
    }
}
