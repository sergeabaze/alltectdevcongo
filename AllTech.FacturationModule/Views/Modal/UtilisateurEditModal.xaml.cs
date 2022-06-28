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
using AllTech.FrameWork.Global;
using AllTech.FrameWork.Model;
using AllTech.FacturationModule.ViewModel;
using AllTech.FrameWork.Utils;
using AllTech.FrameWork.Views;
using Multilingue.Resources;

namespace AllTech.FacturationModule.Views.Modal
{
    /// <summary>
    /// Interaction logic for UtilisateurEditModal.xaml
    /// </summary>
    public partial class UtilisateurEditModal : Window
    {
        public UtilisateurEditModal()
        {
            InitializeComponent();

            this.Height = GlobalDatas.mainHeight * 0.55;
            this.Width = GlobalDatas.mainWidth * 0.50;
            groupHeaderMain.Height = GlobalDatas.mainHeight * 0.55;
           // UservuewctGrid.Width = this.Width * 0.80;
           // UservuewctGrid.Height = this.Width * 0.30;
            Loaded+=new RoutedEventHandler(UtilisateurEditModal_Loaded);

           
        }

        private void UtilisateurEditModal_Loaded(object sender, EventArgs e)
        {
            int objcr = -1;
            DataRefUtilisateurViewModel _viewModel = this.DataContext as DataRefUtilisateurViewModel;
            DroitModel droitservice = new DroitModel();
            //if (_viewModel.UserSelected.Id > 0)
            //{
            //    List<DroitModel> newliste = droitservice.GetListdroit(_viewModel.UserSelected.Profile.IdProfile, _viewModel.UserSelected.Id);
            //    _viewModel.VuewListeDroitUser = newliste;
            //}
            //else _viewModel.VuewListeDroitUser = null;
            int obj = 0;
           
            if (_viewModel.UserSelected != null)
            {
                if (_viewModel.UserSelected.IdProfile > 0)
                {
                    // cmbprofile.SelectedIndex = -1;

                    if (_viewModel.ProfileList != null)
                    {
                        foreach (var val in _viewModel.ProfileList)
                        {
                            if (_viewModel.UserSelected.Profile.IdProfile == val.IdProfile)
                            {
                                //  cmbprofile.SelectedIndex = obj;

                                break;
                            }
                            obj++;

                        }
                        objcr = obj;
                    }
                }
            }
        }

        private void cmbprofile_SelectionChanged(object sender, EventArgs e)
        {
            DataRefUtilisateurViewModel _viewModel = this.DataContext as DataRefUtilisateurViewModel;
            ProfileModel profile = cmbprofile.SelectedItem as ProfileModel;
            if (profile != null)
                _viewModel.ProfileSelected = profile;
        }

        private void btnadPhoto_Click(object sender, RoutedEventArgs e)
        {
            DataRefUtilisateurViewModel _viewModel = this.DataContext as DataRefUtilisateurViewModel;
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
                    //FileStream fs = new FileStream(@imageName, FileMode.Open, FileAccess.Read);

                    //FileStream fsImg = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                    // Bitmap bitmap = new Bitmap((Bitmap)System.Drawing.Image.FromStream(fs));
                    // bitmap.Save(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                    //Bitmap myBitmap2 = new Bitmap(mamm, new Size(150, 175));
                    //fsImg.Close();

                    //pictureBox2.Image = myBitmap2;
                    //pictureBox2.Image.Save(Globale.Chemeincompletimages + @"\" + nouveau);

                    // string CheminImages = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal );

                    //string valsx = System.Environment.CurrentDirectory;




                    //byte[] imgByteArr = new byte[fs.Length];
                    //fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                    //fs.Close();
                    _viewModel.UserSelected.Photo = imageName;

                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataRefUtilisateurViewModel _viewModel = this.DataContext as DataRefUtilisateurViewModel;
            bool val = UserInterfaceUtilities.ValidateVisualTree(this);
            if (_viewModel.UserSelected != null)
            {
                if (!string.IsNullOrEmpty(_viewModel.UserSelected.Nom))
                {
                    StyledMessageBoxView messageBox = new StyledMessageBoxView();
                    messageBox.Owner = Application.Current.MainWindow;
                    messageBox.Title = ConstStrings.Get("messageCloseTitre"); ;
                    messageBox.ViewModel.Message = ConstStrings.Get("messageCloseMsg"); ;
                    if (messageBox.ShowDialog().Value == true)
                    {
                        if (UserInterfaceUtilities.ValidateVisualTree(this) == true)
                        {
                        this.DialogResult = true;
                        }
                    }
                } else  this.DialogResult = true;
            }
            else this.DialogResult = true;
             
        }
    }
}
