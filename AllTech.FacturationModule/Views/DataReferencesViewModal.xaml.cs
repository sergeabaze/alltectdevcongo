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

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for DataReferencesViewModal.xaml
    /// </summary>
    public partial class DataReferencesViewModal : Window
    {
        public DataReferencesViewModal(Window parent)
        {
            InitializeComponent();
            DatarefencesViewModalViewModel viewModel = new DatarefencesViewModalViewModel(this);
            this.DataContext = viewModel;

            //parent.Top = 100;//(workHeight - this.ActualHeight) / 2;
            //parent.Left = 100; //(workWidth - this.ActualWidth) / 2;
            Double workHeight = SystemParameters.WorkArea.Height;
            Double workWidth = SystemParameters.WorkArea.Width;

            this.Width = (workWidth / 2)+400 ;
            this.Height = (workHeight / 2)+350;
            //this.Top = (workHeight - this.Height) / 2;
            //this.Left = (workWidth - this.Width) / 2;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            //{
            this.DialogResult = true;
            // }
        }
    }
}
