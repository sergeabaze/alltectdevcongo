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
using AllTech.FrameWork.Utils;
using AllTech.FacturationModule.ViewModel;
using System.Windows.Threading;

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for AdminModalBackUp.xaml
    /// </summary>
    public partial class AdminModalBackUp : Window
    {
        public AdminModalBackUp()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            {
                this.DialogResult = true;

                // chargement liste
            }
        }

        private void chkName_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chkBox = e.OriginalSource as CheckBox;

            ArchivePeview item = LviewGrid.SelectedItem as ArchivePeview;
            var checkBox = sender as CheckBox;
            var idFacture = checkBox.Content;
        }

        private void chkName_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void chkName_Click(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            var item = cb.DataContext;
            LviewGrid.SelectedItem = item;
        }

       

        public String LBInfos
        {
            get
            {
                return lbInfos.Content.ToString();
            }
            set
            {
                lbInfos.Content = value;
                lbInfos.Refresh();
            }
        }

        public Double ValueProgressBar
        {
            get
            {
                return progressBar.Value;
            }
            set
            {
                progressBar.Value = value;
                progressBar.Refresh();
            }
        }
    }


    public static class ExtensionMethods
    {
        private static Action EmptyDelegates = delegate() { };

        public static void RefreshLogin(this DispatcherObject uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegates);
        }

        public static void Refresh(this DispatcherObject uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegates);
        }
    }
}
