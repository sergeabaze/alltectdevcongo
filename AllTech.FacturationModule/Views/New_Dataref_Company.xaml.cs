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
using System.IO;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for New_Dataref_Company.xaml
    /// </summary>
    public partial class New_Dataref_Company : Window
    {
        DatarefCompanyViewModel localViewModel;
        string imageName;

        public New_Dataref_Company(Window wpfParent)
        {
            InitializeComponent();
           double h= this.ActualHeight;

            //Double workHeight = SystemParameters.WorkArea.Height;
            //Double workWidth = SystemParameters.WorkArea.Width;
           this.Top = 100;//(workHeight - this.ActualHeight) / 2;
           this.Left =100; //(workWidth - this.ActualWidth) / 2;
            DatarefCompanyViewModel viewModel = new DatarefCompanyViewModel();
            localViewModel = viewModel;
            this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".csv";
            dlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
            Nullable<bool> result = dlg.ShowDialog();


            if (result == true)
            {
                // Open document 
                imageName = dlg.FileName;

                //FileInfo info = new FileInfo(filename);
                if (localViewModel.CurrentSociete != null)
                {
                    //Initialize a file stream to read the image file

                    FileStream fs = new FileStream(@imageName, FileMode.Open, FileAccess.Read);
                    //Initialize a byte array with size of stream
                    byte[] imgByteArr = new byte[fs.Length];
                    //Read data from the file stream and put into the byte array
                    fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    localViewModel.CurrentSociete.Image = imgByteArr;

                }
            }
        }

        private void btnimagePiedpage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".csv";
            dlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
            Nullable<bool> result = dlg.ShowDialog();


            if (result == true)
            {
                // Open document 
                imageName = dlg.FileName;

                //FileInfo info = new FileInfo(filename);
                if (localViewModel.CurrentSociete != null)
                {

                    FileStream fs = new FileStream(@imageName, FileMode.Open, FileAccess.Read);
                    //Initialize a byte array with size of stream
                    byte[] imgByteArr = new byte[fs.Length];
                    //Read data from the file stream and put into the byte array
                    fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    localViewModel.CurrentSociete.LogoPiedPage = imgByteArr;

                }
            }
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
