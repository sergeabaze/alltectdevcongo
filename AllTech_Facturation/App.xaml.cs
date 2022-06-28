using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using AllTech.FrameWork.Views;
using AllTech.FrameWork.Global;
using AllTech_Facturation.Views;
using System.Threading;
using System.Globalization;
using System.Windows.Markup;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace AllTech_Facturation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
           

            base.OnStartup(e);
            bool bautreInstance = VerifSiAutreInstance();
            if (bautreInstance == false)
            {
               
                try
                {
                    Utils.NewParameterFile();
                    GlobalDatas.dataBasparameter = Utils.GetParametersApplication();

                    Utils.logConnection("***********************  Nouvelle connection **************  " + DateTime.Now, "");
                    Utils.logConnection("Chargement fichier de parametres " + DateTime.Now, "");
                }
                catch (Exception ex)
                {
                    //Utils.logConnection(" Erreure de chargement fichier de Parametres  : " + ex.Message + " " + DateTime.Now, "");
                  
                    HandleException(ex);
                    
                }
                CultureInfo culture = new CultureInfo(GlobalDatas.defaultLanguage);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;


                // Ensure the current culture passed into bindings is the OS culture.
                // By default, WPF uses en-US as the culture, regardless of the system settings.
                //FrameworkElement.LanguageProperty.OverrideMetadata(
                //  typeof(FrameworkElement),
                //  new FrameworkPropertyMetadata(
                //      XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

                //getDefaultLanguage();

                Boostrapper bootstrap = new Boostrapper();
                bootstrap.Run();
            }
            else
            {
                MessageBox.Show("Un processus est Déja En cours");
                
            }

        }

        void getDefaultLanguage()
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "en-US":
                    dict.Source = new Uri("..\\Resources\\ResourceEn.xaml",
                                  UriKind.Relative);
                    break;
                case "fr-FR":
                    dict.Source = new Uri("..\\Resources\\ResourceFr.xaml",
                                       UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("..\\Resources\\ResourceEn.xaml",
                                      UriKind.Relative);
                    break;
            }
            //ResourceFr

            //ResourceDictionary dict = Application.Current.Resources.MergedDictionaries[0];
            //int nbre = Resources.MergedDictionaries.Count;
            this.Resources.MergedDictionaries.Add(dict);

        }


        static bool VerifSiAutreInstance()
        {
            Process curProc = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcesses();
          
            foreach (Process p in procs)
                if (curProc.Id != p.Id)
                    if (curProc.ProcessName == p.ProcessName) return true;
            return false;
        }
        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void HandleException(Exception ex)
        {
            if (ex == null)
                return;

              //ExceptionPolicy.HandleException(ex, "Default Policy");
            MessageBox.Show(ex.Message);
            
            Environment.Exit(1);
        }
    }
}
