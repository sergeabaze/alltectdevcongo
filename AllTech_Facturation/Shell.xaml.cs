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
using System.Windows.Threading;
using AllTech.FrameWork.Global;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;
using AllTech.FrameWork.Utils;

namespace AllTech_Facturation
{
    public delegate void MyEventHandler( EventArgs e);
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : Window
    {
        private DispatcherTimer timer;
       
        private DispatcherTimer timerClient;
        public static event MyEventHandler eventTemporaryAction;
       
        bool isloading;
        ShellViewModel localViemodel;
        public Shell()
        {
            InitializeComponent();
          
           
            Communicator.eventClientNonFacturees+=new Communicator.MyEventHandler(Communicator_eventClientNonFacturees);
            Communicator.eventCloseMainView+=new Communicator.MyEventHandler(Communicator_eventCloseMainView);
          this.WindowState = WindowState.Maximized;
            Double workHeight = SystemParameters.WorkArea.Height;
            Double workWidth = SystemParameters.WorkArea.Width;
            double hh = this.ActualHeight; 
           ShellViewModel viewModel = new ShellViewModel(this );
            //this.Top = (workHeight - this.Height) / 2;
            //this.Left = (workWidth - this.Width) / 2;
            contentheight.Height = workHeight - 110;
            this.DataContext = viewModel;
            localViemodel = viewModel;
          
            GlobalDatas.mainWidth = workWidth;
            Loaded += new RoutedEventHandler(Window_Loaded);
            eventTemporaryAction += new MyEventHandler(TesteTemporaryAccount);

            GlobalDatas.MainWindow = this;
            btnLangue.Content = GlobalDatas.defaultLanguage;
            switch (GlobalDatas.defaultLanguage)
            {
                case "fr-FR": { menuFrancais.IsChecked = true; menuAglais.IsChecked = false; break; }
                case "en-US": { menuFrancais.IsChecked = false; menuAglais.IsChecked = true; break; }
            }

             Closing+=new System.ComponentModel.CancelEventHandler(Shell_Closing);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (localViemodel.IsnewwLoggin)
            {

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(10);
                timer.Tick += timer1_Tick;
                timer.Start();
                MyFirstPopupTextBlock.Text = "";// GlobalDatas.DisplayLanguage["toolbarPopUpLicenseMsgDefaulloggin"].ToString();
                showPopUp();
            }
            

                // pour un utilisateur temporataire on verifie son temps restant
            
        }

        private void Shell_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                Utils.logConnection(" fermeture de l' application en date du  " + DateTime.Now, "");

                Process curProc = Process.GetCurrentProcess();
                Process[] procs = Process.GetProcesses();

                foreach (Process p in procs)
                    if (curProc.Id == p.Id)
                        if (curProc.ProcessName == p.ProcessName)
                            curProc.Kill();

            }
            catch (Exception ex)
            {
                Utils.logConnection(" fermeture de l' application en date du  " + DateTime.Now + "" + ex.Message, "");
            }
        }

        public static  void OnChange(EventArgs e)
        {
            eventTemporaryAction(e);
        }
        //
        protected void Communicator_eventCloseMainView(object sender, EventArgs e)
        {
            this.Hide();
        }
        protected void Communicator_eventClientNonFacturees(object sender, EventArgs e)
        {
            Communicator com = sender as Communicator;
            lstClintNonfact.ItemsSource = com.Clients;

            timerClient = new DispatcherTimer();
            timerClient.Interval = TimeSpan.FromSeconds(30);
            timerClient.Tick+=new EventHandler(timerClient_Tick);
            timerClient.Start();

            for (int i = 0; i < 100; i += 5)
                Thread.Sleep(100);
            popPopListeclientNonFActurer.IsOpen = true;
        }

        private void timerClient_Tick(object sender, EventArgs e)
        {
            timerClient.Stop();
            //this.Close();
            popPopListeclientNonFActurer.IsOpen = false;
        }



        private void btnClientClose_Click(object sender, RoutedEventArgs e)
        {
            popPopListeclientNonFActurer.IsOpen = false;
        }

      

        protected  void TesteTemporaryAccount(EventArgs e)
        {

            MyToolTipTemporaire.IsOpen = false;

                if (localViemodel.ProfileDateSelected != null)
                {
                    TimeSpan differenceDate = (DateTime)localViemodel.ProfileDateSelected.Datefin - DateTime.Parse(DateTime.Now.ToShortDateString());
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(20);
                    timer.Tick += timerTempo_Tick;
                    timer.Start();

                    if (differenceDate.TotalDays == 0)
                    {
                        MyFirstPopupTex.Text = GlobalDatas.DisplayLanguage["toolbarPopUpLicenseMsgTemporarycmpt"].ToString();
                        //LabelCompte = "Dernier jour de Validitier";
                        showPopUpTemp();
                    } else if (differenceDate.TotalDays < 0)
                    {
                        MyFirstPopupTex.Text = GlobalDatas.DisplayLanguage["toolbarPopUpLicenseMsgExpirationtemp"].ToString();

                        showPopUpTemp();
                       
                    }
                     else
                    {
                        GlobalDatas.DisplayLanguage["toolbarPopUpLicenseMsgJourRestant"].ToString();

                        if (differenceDate.TotalDays<=5)
                            MyFirstPopupTex.Text = string.Format(GlobalDatas.DisplayLanguage["toolbarPopUpLicenseMsgrestevalidite"].ToString(), differenceDate.TotalDays);
                        else MyFirstPopupTex.Text = string.Format(GlobalDatas.DisplayLanguage["toolbarPopUpLicenseMsgJourRestant"].ToString(), differenceDate.TotalDays);
                        showPopUpTemp();
                    }
                }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            //this.Close();
            MyToolTip.IsOpen = false ;
        }

        private void timerTempo_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            //this.Close();
            MyToolTipTemporaire.IsOpen = false;
        }

        void showPopUp()
        {
            
            MyToolTip.IsOpen = true;
        }

        void showPopUpTemp()
        {
            
            MyToolTipTemporaire.IsOpen = true;
        }

       

        public   void ModifieContentregion(object obj)
        {
            localViemodel.ContenRegion = obj; 
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            Application.Current.MainWindow = this;
            GlobalDatas.mainWidth = this.ActualWidth;
            GlobalDatas.mainHeight = this.ActualHeight;

        }

        private void Window_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {
         
                //Application.Current.MainWindow = this;
            if (e.WidthChanged)
                GlobalDatas.mainWidth = e.NewSize.Width;
            if (e.HeightChanged)
            {
                GlobalDatas.mainHeight = e.NewSize.Height;
                GlobalDatas.mainMaxHeight = e.NewSize.Height;
            }
                if (isloading)
                {
                contentheight.Height = this.ActualHeight - 130;
            }
            isloading = true;
        }



        #region EVENT DISPLAY LANGUAGE



        private void menuFrancais_Click(object sender, RoutedEventArgs e)
        {
            //ResourceDictionary dict = new ResourceDictionary();
            //dict.Source = new Uri("..\\Resources\\ResourceFr.xaml",
            //                          UriKind.Relative);
            //Application.Current.Resources.MergedDictionaries.Add(dict);
            btnLangue.Content = "fr-FR";
            menuAglais.IsChecked = false;
            menuFrancais.IsChecked = true;
            //GlobalDatas.DisplayLanguage = (Hashtable)Application.Current.TryFindResource("contentFile");
            CultureInfo culture = new CultureInfo("fr-FR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            Multilingue.Resources.LanguageHelper.inittread();
        }

        private void menuAglais_Click(object sender, RoutedEventArgs e)
        {
            //ResourceDictionary dict = new ResourceDictionary();
            //dict.Source = new Uri("..\\Resources\\ResourceEn.xaml",
            //                           UriKind.Relative);
            //Application.Current.Resources.MergedDictionaries.Add(dict);
            btnLangue.Content = "en-Us";
            menuAglais.IsChecked = true;
            menuFrancais.IsChecked = false;
            //GlobalDatas.DisplayLanguage = (Hashtable)Application.Current.TryFindResource("contentFile");
            CultureInfo culture = new CultureInfo("en-Us");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            Multilingue.Resources.LanguageHelper.inittread();
        }

        #endregion

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Process curProc = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcesses();

            foreach (Process p in procs)
                if (curProc.Id == p.Id)
                    if (curProc.ProcessName == p.ProcessName)
                        curProc.Kill();
        }

       
    }
}
