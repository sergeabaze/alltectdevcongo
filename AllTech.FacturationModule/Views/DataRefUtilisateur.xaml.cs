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
using AllTech.FrameWork.Model;
using System.IO;
using System.Drawing;
using AllTech.FrameWork.Global;
using AllTech.FacturationModule.Views.Modal;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for DataRefUtilisateur.xaml
    /// </summary>
    public partial class DataRefUtilisateur : UserControl
    {
        DataRefUtilisateurViewModel _viewModel;
        int objcr = -1;
        bool isloading;
        Window localWindow;
        public DataRefUtilisateur(Window window)
        {
            DataRefUtilisateurViewModel viewModel = new DataRefUtilisateurViewModel(window);
            this.DataContext = viewModel;
            GlobalDatas.ViewModeluser = viewModel;
            InitializeComponent();
           
            _viewModel = viewModel;
            localWindow = window;
            toolbarMain.Width = SystemParameters.WorkArea.Width;

            double d = GlobalDatas.mainMaxHeight;
            userGrid.Height = GlobalDatas.mainHeight - 460;

            //if (GlobalDatas.mainHeight >= GlobalDatas.mainMaxHeight)
            //    userGrid.Height = GlobalDatas.mainHeight -500;
            //else
            //    userGrid.Height = GlobalDatas.mainHeight -300;
            
         
            double vWidleft = GlobalDatas.mainWidth * 0.15;
            optionrechechName.Width = vWidleft;
            txtNomRecherche.Width = optionrechechName.Width * 0.70;
           // userGrid.Height = GlobalDatas.mainHeight * 0.3;
           // optionProfilUsers.Height = userGrid.Height*0.85;



           // controlprofile.Height = GlobalDatas.mainHeight - 400;
           // controlVuesdroits.Height = GlobalDatas.mainHeight - 400;


            //optionProfilUsersDroit.Height = userGrid.Height*0.80;
        

          
            //groupuservues.Height = GlobalDatas.mainHeight - 400;
           
            //profileVuesGrid.Height = groupuservues.Height * 0.60;
            //profileSousVuesGrid.Height = groupuservues.Height * 0.40;

            ////
            //optionProfilUsersDroit.Height = GlobalDatas.mainHeight - 400;
            //optionProfilUsersDroit.Width = GlobalDatas.mainWidth * 0.90;

          

            //lstbOxProfiledroits.Height = GlobalDatas.mainHeight * 0.52;
          
            isloading = true;
        }

        private void userGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._viewModel.UserSelected = this.userGrid.ActiveItem  as UtilisateurModel;
            UtilisateurEditModal view = new UtilisateurEditModal();
            view.DataContext = _viewModel;
            view.Owner = localWindow;
            view.ShowDialog();
            if (this._viewModel.IsAction)
                this._viewModel.refreshdata();
            //int obj = 0;
            
           
            //}
        }

        private void search_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //this.ViewModel.IsSearchButtonEnabled = (this.search.Text.Length > 0) ? true : false;
        }

        private void btnadPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.UserSelected != null)
            {
           string   imageName;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".csv";
            dlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
            Nullable<bool> result = dlg.ShowDialog();


            if (result == true)
            {
               
                    imageName = dlg.FileName;
                    string nomImage = imageName.Substring(imageName.LastIndexOf("\\") + 1);
                    string nouveau = _viewModel.UserSelected .Nom +DateTime.Now.Year+ nomImage;
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

        private void cmbprofile_SelectionChanged(object sender, EventArgs e)
        {
            //ProfileModel profile = cmbprofile.SelectedItem as ProfileModel;
            //if (profile != null)
            //    _viewModel.ProfileSelected = profile;

        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!isloading)
            {
                if (e.HeightChanged)
                {
                    if (e.PreviousSize.Height < e.NewSize.Height)
                        userGrid.Height = GlobalDatas.mainHeight - 460;
                    else
                        userGrid.Height = GlobalDatas.mainHeight - 360;
                }

                //groupuservues.Height = GlobalDatas.mainHeight - 400;
             
                //optionProfilUsersDroit.Height = userGrid.Height * 0.80;
                //optionProfilUsersDroit.Width = GlobalDatas.mainWidth * 0.90;

                //profileVuesGrid.Height = userGrid.Height * 0.60;
                //profileSousVuesGrid.Height = userGrid.Height * 0.40;

                //profileVuesGrid.Width = GlobalDatas.mainWidth * 0.50;
                //profileSousVuesGrid.Width = GlobalDatas.mainWidth * 0.45;

              

                double vWidleft = GlobalDatas.mainWidth * 0.15;
                optionrechechName.Width = vWidleft;
                txtNomRecherche.Width = optionrechechName.Width * 0.70;
                //profileVuesGrid.Height = GlobalDatas.mainHeight -400;
                //profileSousVuesGrid.Height = GlobalDatas.mainHeight * 0.1;
               // lstbOxProfiledroits.Height = GlobalDatas.mainHeight * 0.5;
            }
            isloading = false;
        }

        private void btnCancelnom_Click(object sender, RoutedEventArgs e)
        {
            txtNomRecherche.Text = string.Empty;
        }

        private void profileVuesGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
          //  tabitemUserdroits.Focus();
        }

       
    }
}
