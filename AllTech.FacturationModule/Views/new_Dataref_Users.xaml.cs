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
using AllTech.FrameWork.Model;
using System.IO;
using System.Drawing;
using AllTech.FrameWork.Utils;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for new_Dataref_Users.xaml
    /// </summary>
    public partial class new_Dataref_Users : Window
    {
        DataRefUtilisateurViewModel _viewModel;
        int objcr = -1;

        public new_Dataref_Users(Window wpfParent)
        {
            InitializeComponent();
           // Double workHeight = wpfParent.Height;
           // Double workWidth = wpfParent.Width;
            //this.WindowState = WindowState.Maximized;
            this.Top = 100;//(workHeight - this.Height) / 2;
            this.Left = 100;//(workWidth - this.Width) / 2;
            DataRefUtilisateurViewModel viewModel = new DataRefUtilisateurViewModel();
            this.DataContext = viewModel;
            _viewModel = viewModel;
        }

        private void userGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._viewModel.UserSelected = this.userGrid.ActiveItem as UtilisateurModel;

            int obj = 0;

            if (this._viewModel.UserSelected != null)
            {
                if (this._viewModel.UserSelected.IdProfile > 0)
                {
                    cmbprofile.SelectedIndex = -1;

                    if (_viewModel.ProfileList != null)
                    {
                        foreach (var val in _viewModel.ProfileList)
                        {
                            if (this._viewModel.UserSelected.IdProfile == val.IdProfile)
                            {
                                cmbprofile.SelectedIndex = obj;

                                break;
                            }
                            obj++;

                        }
                        objcr = obj;
                    }
                }
            }
        }

        private void search_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //this.ViewModel.IsSearchButtonEnabled = (this.search.Text.Length > 0) ? true : false;
        }

        private void btnadPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.UserSelected != null)
            {
                string imageName;
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.FileName = "Document";
                dlg.DefaultExt = ".csv";
                dlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                Nullable<bool> result = dlg.ShowDialog();


                if (result == true)
                {

                    imageName = dlg.FileName;
                    string nomImage = imageName.Substring(imageName.LastIndexOf("\\") + 1);
                    string nouveau = _viewModel.UserSelected.Nom + DateTime.Now.Year + nomImage;
                    _viewModel.NouveauNomImages = imageName;

                    _viewModel.UserSelected.Photo = imageName;

                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
            //{
            this.DialogResult = true;
            // }
        }
    }
}
