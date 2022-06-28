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
using System.IO;

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for ModalSignature.xaml
    /// </summary>
    public partial class ModalSignature : Window
    {
        ModalSignatureViewModel localviemodel;
        public ModalSignature( )
        {
            InitializeComponent();
            ModalSignatureViewModel viewModel = new ModalSignatureViewModel();
            localviemodel = viewModel;
            DataContext = viewModel;
        }

      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            {
                this.DialogResult = true;
            }
        }

        private void btnDossier_Click(object sender, RoutedEventArgs e)
        {
             Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Image File (*.jpg;*.bmp;*.gif;*.png)|*.jpg;*.bmp;*.gif;*.png";
            Nullable<bool> result = dlg.ShowDialog();
           string  imageName = dlg.FileName;

            if (result == true)
            {
                FileStream fs = new FileStream(@imageName, FileMode.Open, FileAccess.Read);
               
                byte[] imgByteArr = new byte[fs.Length];
              
                fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                localviemodel .Signature = imgByteArr;
                fs.Close();
            }
        }
    }
}
