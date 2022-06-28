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
using AllTech.FrameWork.Utils;

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for Exoneration.xaml
    /// </summary>
    public partial class Exoneration : Window
    {
        ExonerationViewModel localViewModel;
        public bool IsACtion;

        public Exoneration()
        {
            InitializeComponent();
            ExonerationViewModel viewModel = new ExonerationViewModel(this );
            localViewModel = viewModel;
            this.DataContext = viewModel;
             this.Closing+=new System.ComponentModel.CancelEventHandler(Exoneration_Closing);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            {
                this.DialogResult = true;
            }
        }

        private void lstexonere_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //this.localViewModel.ExonereCourant = ((ListViewItem)sender).Content as ExonerationModel;
            //e.Handled = true;  

            this.localViewModel.ExonereCourant = lstexonere.SelectedItem  as ExonerationModel;
        }

        private void Exoneration_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsACtion = localViewModel.IsAction;
        }
    }
}
