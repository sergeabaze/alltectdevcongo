using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.Model;
using System.Windows;
using System.Xml.Linq;
using System.Windows.Controls;
using System.Collections;

namespace AllTech.FrameWork.Global
{
   public  class GlobalDatas:Control
    {

       public static UtilisateurModel currentUser;
       public static string defaultParameterFile = "parameter.xml";
       public static string defaultDirectory = "parameterFile";
       public static string defaultPassword="";
       public static string defaultLoggin="";
       public static string urlDossierimages = "";
       public static string ConnectionString = "";
       public static Window MainWindow;
       public static object ViewModeluser;

       public static XElement CurrentParameter;
       public static string  CurrentConnectionString;
       public static FactureModel currentFacture=null ;
       public static SocieteModel DefaultCompany;
       public static List<SocieteModel> listeCompany;
       public static string defaultLanguage = "";
       public static string cultureCode = string.Empty;
       public static bool IsArchiveDatas =false;
       public static bool IsArchiveSelected = false;
       public static int?  IdSiteArchiveDatas = null;
       public static bool IdDataRefArchiveDatas = false;

       public static ParametresModel dataBasparameter;
       public static bool  isConnectionOk =true ;
       public static string SIGLE = "SF";
       public static string EXTRACT_VALIDE = "EXTV";
       public static string EXTRACT = "EXT";
       public static double mainWidth;
       public static double mainHeight;
       public static double mainMaxHeight;

       public static Hashtable DisplayLanguage;
       public static DateTime? licenseDate;
       public static DateTime? licenseLastDate;
       public static string licenseNumero = string.Empty;
       public static string licenseCPU = string.Empty;
       public static string licenseHDD = string.Empty;
       public static Guid licenseGUID = new Guid();
       public static bool isCPUorGUID = false;
       public static int licenseJoursrestant;

       public static string printerName = null;
      


       //public static readonly DependencyProperty CurrentParameter = DependencyProperty.Register("PrameterFiles",
       //  typeof(object),
       //  typeof(GlobalDatas),
       //  new FrameworkPropertyMetadata(string.Empty));

       //public string PrameterFiles
       //{
       //    get { return (string )GetValue(CurrentParameter); }
       //    set { SetValue(CurrentParameter, value); }
       //}

       //public static XElement parameter = null;
    }
}
