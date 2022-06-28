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
using System.Xml.Linq;
using AllTech.FrameWork.Global;
using AllTech.FrameWork.Views;




namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for DataRef_JournalVente.xaml
    /// </summary>
    public partial class DataRef_JournalVente : UserControl
    {

        JournalventeViewModel localViewModel;
        double currentH = 0;
        double currentW = 0;
        bool isloading = false;
        public DataRef_JournalVente(Window window)
        {
            InitializeComponent();

            JournalventeViewModel viewModel = new JournalventeViewModel(this, window);
            this.DataContext = viewModel;
            localViewModel = viewModel;

           // double widht = SystemParameters.WorkArea.Width;
           // double height = SystemParameters.WorkArea.Height;

            GridHistorique.Height = GlobalDatas.mainHeight - 450;
            lstClintNonfact.Height = GlobalDatas.mainHeight - 450;
           // GridHistorique.Width = widht - 275;
            isloading = false;
            optinalLeft.Width = GlobalDatas.mainWidth*0.15;
            optinalLeft.Height = GlobalDatas.mainHeight - 420;

            lixtHistoric.Height = optinalLeft.Height - 180;
           // 
            //lixtHistoric.Width = optinalLeft.Width -10;
           
            //currentH = height - 300;
            //currentW = widht - 400;
        }

        private void cmbPeriode_SelectionChanged(object sender, EventArgs e)
        {
            PeriodeDateJv periode = cmbPeriode.SelectedItem as PeriodeDateJv;
            if (periode != null)
                localViewModel.PeriodeSelected = periode;
        }

        private void cmbPeriodeMois_SelectionChanged(object sender, EventArgs e)
        {
            PeriodeMoisJv periode = cmbPeriodeMois.SelectedItem as PeriodeMoisJv;
            if (periode != null)
                localViewModel.PeriodeMoisSelected = periode;
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        public void StartStopWait(bool values)
        {
            //LoadingAdorner.IsAdornerVisible = !LoadingAdorner.IsAdornerVisible;
            LoadingAdorner.IsAdornerVisible = values;
            //GridFacture.IsEnabled = !GridFacture.IsEnabled;
        }

        private void btnChemin_Click(object sender, RoutedEventArgs e)
        {
            string folderPath=string .Empty ;
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
              
            }

            if (!string.IsNullOrEmpty(folderPath))
            {
                try
                {

                    if (string.IsNullOrEmpty(localViewModel.ParametersDatabase.PathJournalVente))
                    {
                        XElement parameter = XElement.Parse(Utils.GetParameterFiles().ToString());
                        foreach (var ele in parameter.Elements())
                        {
                            if (ele.Attribute("id").Value == "bases")
                            {
                                ele.Element("jvreperory").SetValue(folderPath);
                            }
                        }
                        parameter.Save(Utils.getfileName());
                        GlobalDatas.dataBasparameter.PathJournalVente = folderPath;
                        localViewModel.ParametersDatabase.PathJournalVente = folderPath;
                        localViewModel.NomDossier = folderPath;
                    }
                    else
                    {

                        System.Windows.Forms.DialogResult messageBox = System.Windows.Forms.MessageBox.Show("Il existe deja un dossier de sauvegarde des fichiers \n Voulez vous continuer ?", "Message", System.Windows.Forms.MessageBoxButtons.YesNo);
                           // messageBox.Owner =this;
                           
                            if (messageBox==System.Windows.Forms.DialogResult.Yes)
                            {
                                // vérifier si le dossier contient des fichiers

                                
                                    //if (!localViewModel.ParametersDatabase.PathJournalVente.EndsWith("Documents"))
                                    //{
                                    //    if (Directory.Exists(localViewModel.ParametersDatabase.PathJournalVente))
                                    //    {
                                    //        string[] fichies= Directory.GetFiles(localViewModel.ParametersDatabase.PathJournalVente);
                                    //        if (fichies.Length > 0)
                                    //        {
                                    //            System.Windows.Forms.DialogResult messageBoxNew = System.Windows.Forms.MessageBox.Show("Ce dossier contient des fichiés \n Souhaiter vous les déplacer également ?", "Message", System.Windows.Forms.MessageBoxButtons.YesNo);
                                    //            if (messageBoxNew == System.Windows.Forms.DialogResult.Yes)
                                    //            {
                                    //                foreach (string f in fichies)
                                    //                {
                                    //                    FileInfo ff = new FileInfo(f);
                                    //                    ff.CopyTo(ff.Name);
                                    //                   // File.Copy(f, folderPath);
                                    //                }
                                    //            }
                                    //        }

                                    //    }
                                       // Directory.Delete(localViewModel.ParametersDatabase.PathJournalVente);
                                    //}
                             
                                   XElement parameter = XElement.Parse(Utils.GetParameterFiles().ToString());
                                   foreach (var ele in parameter.Elements())
                                   {
                                       if (ele.Attribute("id").Value == "bases")
                                       {
                                           ele.Element("jvreperory").SetValue(folderPath);
                                       }
                                   }
                                   parameter.Save(Utils.getfileName());
                                   GlobalDatas.dataBasparameter.PathJournalVente = folderPath;
                                   localViewModel.ParametersDatabase.PathJournalVente = folderPath;
                                   localViewModel.NomDossier = folderPath;

                            }

                    }
                
                   
                }
                catch (IOException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "MESSAGE ERREURR CREATION DOSSIER", System.Windows.Forms.MessageBoxButtons.OK);
                }
            }
        }

        private void btnVoirChemin_Click(object sender, RoutedEventArgs e)
        {
          
            string path = localViewModel.ParametersDatabase.PathJournalVente;

            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            if (Directory.Exists(path))
            {
                openFileDialog1.InitialDirectory = path;
            }
            else
            {
               // openFileDialog1.InitialDirectory ="Document";
                openFileDialog1.FileName = "Document";
            }

            // Default file name
            openFileDialog1.DefaultExt = ".txt"; // Default file extension
            openFileDialog1.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = openFileDialog1.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = openFileDialog1.FileName;
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (isloading)
            {
                //double nheight = SystemParameters.WorkArea.Height;
                //double nWidth = SystemParameters.WorkArea.Width;

                //if (e.NewSize.Width != e.PreviousSize.Width)
                //{
                //    if (e.NewSize.Width < e.PreviousSize.Width)
                //    {
                //        GridHistorique.Height = e.NewSize.Height - 450;
                //        GridHistorique.Width = e.NewSize.Width - 250; //e.NewSize.Width;
                //        currentH = e.NewSize.Height + 100;
                //        currentW = e.NewSize.Width - 100;
                //    }
                //    else
                //    {
                //        GridHistorique.Height = nheight - 450;
                //        GridHistorique.Width = nWidth - 275;
                //        currentH = nheight - 200;
                //        currentW = nWidth - 200;
                //    }


                //}
                optinalLeft.Width = GlobalDatas.mainWidth * 0.15;
                optinalLeft.Height = GlobalDatas.mainHeight - 420;

                lixtHistoric.Height = optinalLeft.Height - 180;

              //  GridHistorique.Height = GlobalDatas.mainHeight - 500;
               // isloading = false;
            }
            isloading = true;
        }

        private void btnCancelnom_Click(object sender, RoutedEventArgs e)
        {
            //txtSearch.Text = string.Empty;
        }

        private void rdobjet_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                XElement parameter = XElement.Parse(Utils.GetParameterFiles().ToString());
                //var queryBase = from p in parameter.Elements()
                //                where (string)p.Attribute("id").Value == "jvmodeSelect"
                //                select p;

                foreach (var ele in parameter.Elements())
                {
                    if (ele.Attribute("id").Value == "bases")
                    {
                        ele.Element("jvmodeSelect").SetValue("1");
                    }
                }
                GlobalDatas.dataBasparameter.JvModeSelect = true;
                parameter.Save(Utils.getfileName());
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title ="Message";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
               
            }

        }

        private void rdProduits_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                XElement parameter = XElement.Parse(Utils.GetParameterFiles().ToString());
                //var queryBase = from p in parameter.Elements()
                //                where (string)p.Attribute("id").Value == "jvmodeSelect"
                //                select p;

                foreach (var ele in parameter.Elements())
                {
                    if (ele.Attribute("id").Value == "bases")
                    {
                        ele.Element("jvmodeSelect").SetValue("0");
                    }
                }

                GlobalDatas.dataBasparameter.JvModeSelect = false;
                parameter.Save(Utils.getfileName());
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "Message";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

            }
        }

        private void lixtHistoric_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            JournalVentesModel jv = lixtHistoric.SelectedItem as JournalVentesModel;
        }

       

       

        //private void lixtHistoric_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //JournalventesDatesModel hst = lixtHistoric.SelectedItem as JournalventesDatesModel;
        //}
    }
}
