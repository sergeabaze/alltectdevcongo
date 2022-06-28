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
using System.IO;
using AllTech.FrameWork.Model;
using AllTech.FrameWork.Global;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for DataRef_Company.xaml
    /// </summary>
    public partial class DataRef_Company : UserControl
    {
        DatarefCompanyViewModel localViewModel;
        string imageName;
        bool isloading;
        public DataRef_Company()
        {
            InitializeComponent();
            DatarefCompanyViewModel viewModel = new DatarefCompanyViewModel( );
            localViewModel = viewModel;
            this.DataContext = viewModel ;
           // toolbarMain.Width = SystemParameters.WorkArea.Width;
            maniBodyOptional.Height = GlobalDatas.mainHeight-370;
            isloading = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Image File (*.jpg;*.bmp;*.gif;*.png)|*.jpg;*.bmp;*.gif;*.png";
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
                localViewModel.CurrentSociete.Logo  = imgByteArr;

                }
            }
        }

        private void btnimagePiedpage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".csv";
            dlg.Filter = "Image File (*.jpg;*.bmp;*.gif;*.png)|*.jpg;*.bmp;**.gif.png";
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
                    localViewModel.CurrentSociete.LogoPiedPage= imgByteArr;

                }
            }
        }

        private void cmbCompany_SelectionChanged(object sender, EventArgs e)
        {
            SocieteModel societe = cmbCompany.SelectedItem as SocieteModel;
            if (societe != null)
                localViewModel.CurrentSociete = societe;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!isloading)
                maniBodyOptional.Height = GlobalDatas.mainHeight-370;
                isloading = false;
        }
    }
}
