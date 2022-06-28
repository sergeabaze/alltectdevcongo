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
using AllTech.FrameWork.Model;
using AllTech.FacturationModule.ViewModel;

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for WModalmail.xaml
    /// </summary>
    public partial class WModalmail : Window
    {
        public bool isSendmail;
        MailViewModel localViewModel;
        public WModalmail(DroitModel _currentDroit,List<LignesFichiers> fattache)
        {
            InitializeComponent();
            MailViewModel viewModel = new MailViewModel(_currentDroit, fattache,this);
            this.DataContext = viewModel;
            localViewModel = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            //{
                this.DialogResult = true;
            //}
        }

        private void txtBody_LostFocus(object sender, RoutedEventArgs e)
        {
            localViewModel.Messagebody = txtBody.Text;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isSendmail = localViewModel.IsSendMAil;
        }
    }
}
