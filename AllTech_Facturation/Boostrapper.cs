using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using System.Windows;
using AllTech_Facturation.Views;
using AllTech.FrameWork.Logger;
using Microsoft.Practices.Prism.Logging;
using System.Threading;
using AllTech_Facturation.Report;
using System.Data;

namespace AllTech_Facturation
{
    public class Boostrapper : UnityBootstrapper
    {
        private readonly LibraryLoggerAdapter _logger = new LibraryLoggerAdapter();
        private NewSplaschScreen newsplash;
        protected override DependencyObject CreateShell()
        {
            newsplash = new NewSplaschScreen();
            newsplash.Show();

            Shell shell = this.Container.Resolve<Shell>();
            return shell;
        }

        protected override void InitializeShell()
        {
            App.Current.MainWindow = (Window)this.Shell;
           newsplash.Close();
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            
        }


        protected override ILoggerFacade CreateLogger()
        {
            return _logger;
        }
        


    }
}
