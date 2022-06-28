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
using AllTech.FrameWork.Global;
using System.ComponentModel;
using MySql.Data.MySqlClient;
using System.Timers;
using AllTech.FrameWork.Helpers;
using System.Windows.Threading;
using System.Data;





namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for AdministrationDatas.xaml
    /// </summary>
    public partial class AdministrationDatas : UserControl
    {
        AdministrationDataViewModel localViewModel;
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlBackup mb;
        DispatcherTimer timer1;
        BackgroundWorker bwImport;
        string Databasetarget;
        int curBytes;
        int totalBytes;
        bool isloading;
        bool cancel = false;

        public AdministrationDatas()
        {
            InitializeComponent();
            AdministrationDataViewModel viewModel = new AdministrationDataViewModel(this);
            this.DataContext = viewModel;
            localViewModel = viewModel;
            toolbAdmin.Width = SystemParameters.WorkArea.Width;
            gridhisto.Height = GlobalDatas.mainHeight - 360;
            datagridListeTables.Height = GlobalDatas.mainHeight - 500;
            //if (GlobalDatas.mainHeight > 530)
            //    gridHst.Height = (GlobalDatas.mainHeight - 530);
            //else
            //    gridHst.Height = 80;

            //mb = new MySqlBackup();
            //mb.ImportProgressChanged += mb_ImportProgressChanged;

            //timer1 = new DispatcherTimer();
            //timer1.Interval = TimeSpan.FromSeconds(10);
            //timer1.Tick += timer1_Tick;

            //bwImport = new BackgroundWorker();
            //bwImport.DoWork += bwImport_DoWork;
            //bwImport.RunWorkerCompleted += bwImport_RunWorkerCompleted;
            //Loaded+=new RoutedEventHandler(AdministrationDatas_Loaded);
           expandHisto.IsExpanded = true;
            isloading = true;
            this.SizeChanged+=new SizeChangedEventHandler(AdministrationDatas_SizeChanged);
        }

        private void AdministrationDatas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!isloading)
            {
                //if (e.PreviousSize.Height < e.NewSize.Height)
                //    GridHistorique.Height = GlobalDatas.mainHeight - 460;
                //else
                gridhisto.Height = GlobalDatas.mainHeight - 360;
                datagridListeTables.Height = GlobalDatas.mainHeight - 500;
            }
            isloading = false;
        }

        void AdministrationDatas_Loaded(object sender, RoutedEventArgs e)
        {
           // CmbDtatabaseListe.ItemsSource = localViewModel.ListeDatabases;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSavaAS_Click(object sender, RoutedEventArgs e)
        {
            string chemin = GlobalDatas.dataBasparameter.CheminFichierPath;
            string nomFichier = string.IsNullOrEmpty(GlobalDatas.dataBasparameter.NomFichierExport) ?"sysfact_Dump":GlobalDatas.dataBasparameter.NomFichierExport;
            

            System.Windows.Forms.SaveFileDialog f = new System.Windows.Forms.SaveFileDialog();
            if (chemin != "")
                f.InitialDirectory = chemin;
            f.Filter = "*.sql|*.sql|*.*|*.*";
            f.FileName =string.Format("{0} {1}{2}", nomFichier, DateTime.Now.ToString("yyyy-MM-dd HHmmss") ,".sql");
            if (System.Windows.Forms.DialogResult.OK == f.ShowDialog())
            {
                localViewModel.PathSaveAs = f.FileName;
                GlobalDatas.dataBasparameter.PathLog = System.IO.Path.GetDirectoryName(localViewModel.PathSaveAs);
                
            }
        }

        public String LBInfos
        {
            get
            {
                return lanbelInfo.Content.ToString();
            }
            set
            {
                lanbelInfo.Content = value;
                lanbelInfo.Refresh();
            }
        }

        public Double ValueProgressBar
        {
            get
            {
                return progressbar.Value;
            }
            set
            {
                progressbar.Value = value;
                progressbar.Refresh();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            cancel = true;
        }

      

        private void CmbDtatabaseListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Databaseinfo dbSelect = CmbDtatabaseListe.SelectedItem as Databaseinfo;
            if (dbSelect!=null )
            Databasetarget = dbSelect.DatabaseNAme;
        }

        private void btnFichier_Click(object sender, RoutedEventArgs e)
        {
            string chemin = GlobalDatas.dataBasparameter.CheminFichierPath;

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            if (chemin != "")
                dlg.InitialDirectory = chemin;

            dlg.FileName = "Databases"; // Default file name
            dlg.DefaultExt = ".sql"; // Default file extension
            dlg.Filter = "Dump database (.sql)|*.sql"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                localViewModel.PathImportDB = filename;
               // txtpathBD.Text = filename;
               // IsfileSelect = false;
            }
        }

        private void btnopen_Click(object sender, RoutedEventArgs e)
        {
            string chemin =GlobalDatas.dataBasparameter.CheminFichierPath;

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            if (chemin != "")
                dlg.InitialDirectory = chemin;

            dlg.FileName = "Databases"; // Default file name
            dlg.DefaultExt = ".sql"; // Default file extension
            dlg.Filter = "Dump database (.sql)|*.sql"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
               // localViewModel.PathImportDB = filename;
                // txtpathBD.Text = filename;
                // IsfileSelect = false;
            }

        }

        private void btnSelecAll_Click(object sender, RoutedEventArgs e)
        {
           // btnSelecAll.IsEnabled = false;

            foreach (Tables r in datagridListeTables.ItemsSource)
            {
                r.Ischeckd=true;
            }
            
            //var liste= localViewModel.ListeTableDB;
            //for (int i = 0; i < liste.Count - 1; i++)
            //{
            //    if (!liste[i].Ischeckd)
            //    liste[i].Ischeckd = true;
            //}

            //localViewModel.ListeTableDB = null;
            //localViewModel.ListeTableDB = liste;
            //btnSelecAll.IsEnabled = true;
            
            
            //foreach (DataGridRow r in datagridListeTables.Items)
            //{
            //   r
            //    //r.Item[0].Value = true;
            //}
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void IscheckedCk_Checked(object sender, RoutedEventArgs e)
        {
            Tables ligne = datagridListeTables.SelectedItem as Tables;
            if (ligne != null)
            {
              localViewModel.ListeTableDB.Find(tbl => tbl.tablename == ligne.tablename).Ischeckd=true;
                
            }
        }

        private void IscheckedCk_Unchecked(object sender, RoutedEventArgs e)
        {
            Tables ligne = datagridListeTables.SelectedItem as Tables;
            if (ligne != null)
            {
                localViewModel.ListeTableDB.Find(tbl => tbl.tablename == ligne.tablename).Ischeckd = false;

            }
        }

        private void expandexport_Expanded(object sender, RoutedEventArgs e)
        {
            expandImport.IsExpanded = false;
            expandHisto.IsExpanded = false;
        }

        private void expandImport_Expanded(object sender, RoutedEventArgs e)
        {
            expandexport.IsExpanded = false;
            expandHisto.IsExpanded = false;
        }

        private void expandHisto_Expanded(object sender, RoutedEventArgs e)
        {
            expandImport.IsExpanded = false;
            expandexport.IsExpanded = false;
        }

        private void ckbselectall_Checked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < localViewModel.ListeTableDB.Count - 1; i++)
            {
                ((List<Tables>)datagridListeTables.ItemsSource)[i].Ischeckd = true;
            }
          
              // datagridListeTables.ItemsSource
        }

        private void ckbselectall_Unchecked(object sender, RoutedEventArgs e)
        {

            foreach (Tables r in datagridListeTables.ItemsSource)
            {
                r.Ischeckd = false;
            }
        }
    }


}
