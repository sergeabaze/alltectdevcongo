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
using System.Windows.Threading;

namespace AllTech_Facturation.Views
{
    /// <summary>
    /// Interaction logic for UserLoggin.xaml
    /// </summary>
    public partial class UserLoggin : UserControl
    {
        ShellViewModel _viemodel;

        public UserLoggin()
        {
            InitializeComponent();
        }

        public ShellViewModel ViewModel
        {
            get { return _viemodel; }
            set { _viemodel = value; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _viemodel;
        }

        public string LblwarningInfo
        {
            get
            {
                return lblWarning.Content.ToString();
            }
            set
            {
                lblWarning.Content = value;
                lblWarning.Refresh();
            }
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

        private void cmbDatabes_SelectionChanged(object sender, EventArgs e)
        {

        }


        //public bool setnewpasswordvisibility
        //{

        //    set
        //    {
        //        if (value)
        //            newPasword.Visibility = Visibility.Visible;
        //        else newPasword.Visibility = Visibility.Hidden;

        //    }

        //}
    
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
