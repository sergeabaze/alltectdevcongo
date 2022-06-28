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
using System.Windows.Threading;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for ProgressWindow.xaml
    /// </summary>
    public partial class ProgressWindow : Window
    {
        public ProgressWindow()
        {
            InitializeComponent();
        }

        //public string LblwarningInfo
        //{
        //    get
        //    {
        //        return lblWarning.Content.ToString();
        //    }
        //    set
        //    {
        //        lblWarning.Content = value;
        //        lblWarning.Refresh();
        //    }
        //}

        public String LBInfos
        {
            get
            {
                return DisplayText.Text.ToString();
            }
            set
            {
                DisplayText.Text  = value;
                DisplayText.Refresh();
            }
        }

        public Double ValueProgressBar
        {
            get
            {
                return ProgressBarControl.Value;
            }
            set
            {
                ProgressBarControl.Value = value;
                ProgressBarControl.Refresh();
            }
        }

        public double ProgressBarMaximum
        {
            get { return ProgressBarControl.Maximum; }
            set
            {
                ProgressBarControl.Maximum = value;
            }
        }

        public double ProgressBarMinimum
        {
            get { return ProgressBarControl.Minimum ; }
            set
            {
                ProgressBarControl.Minimum = value;
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
