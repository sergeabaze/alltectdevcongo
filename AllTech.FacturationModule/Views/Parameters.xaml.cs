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
using AllTech.FacturationModule.ViewModel;
using AllTech.FacturationModule.Views.Modal;
using AllTech.FrameWork.Model;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for Parameters.xaml
    /// </summary>
    public partial class Parameters : UserControl
    {
        ParametersViewModel localViewModel;
       
        public Parameters()
        {
            InitializeComponent();
            ParametersViewModel viewModel = new ParametersViewModel();
            localViewModel = viewModel;
            this.DataContext = viewModel;
            toolbMain.Width = SystemParameters.WorkArea.Width;
        }

        private void statut_clik(object sender, RoutedEventArgs e)
        {
            StatusFacture vf = new StatusFacture();
            vf.Owner = Application.Current.MainWindow;
            vf.ShowDialog();
        }

        private void btnpathLog_Click(object sender, RoutedEventArgs e)
        {
            //Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //dlg.FileName = "Document"; // Default file name
            //dlg.DefaultExt = ".txt"; // Default file extension
            //dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            //// Show open file dialog box
            //Nullable<bool> result = dlg.ShowDialog();

            //// Process open file dialog box results
            //if (result == true)
            //{
            //    // Open document
            //    string filename = dlg.FileName;
            //}

            string folderPath = string.Empty;
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
                if (localViewModel.CurrentParametres != null)
                {
                    localViewModel.CurrentParametres.CheminFichierPath = null ;
                    localViewModel.CurrentParametres.CheminFichierPath = folderPath;
                }
            }
        }
        private void btnPathBackUp_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = string.Empty;
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
                if (localViewModel.CurrentParametres != null)
                    localViewModel.CurrentParametres.PathBackUpLog = folderPath;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // ModifieContentregion();
           // parentForm.
        }

        private void btn_logFile_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = string.Empty;
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
                if (localViewModel.CurrentParametres != null)
                {
                    localViewModel.CurrentParametres.PathLog = null;
                    localViewModel.CurrentParametres.PathLog = folderPath;
                }
            }
           

        }

        private void cmbLangues_SelectionChanged(object sender, EventArgs e)
        {
            DisplayLangues langue = cmbLangues.SelectedItem as DisplayLangues;
            if (langue != null)
                localViewModel.DisplaylangueSelect = langue;
        }

       


       
    }

   

   
}
