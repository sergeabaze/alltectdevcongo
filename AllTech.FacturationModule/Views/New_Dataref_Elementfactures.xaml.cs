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
    /// Interaction logic for New_Dataref_Elementfactures.xaml
    /// </summary>
    public partial class New_Dataref_Elementfactures : Window
    {
        public New_Dataref_Elementfactures()
        {
            InitializeComponent();

            DataRefElementFactureViewModel viewModel = new DataRefElementFactureViewModel();
            this.DataContext = viewModel;
            this.Top = 100;// (workHeight - this.Height) / 2;
            this.Left = 100;// (workWidth - this.Width) / 2;

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
