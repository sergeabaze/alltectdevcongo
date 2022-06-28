using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FACTURATION_DAL;
using AllTech.FrameWork.Global;

namespace AllTech.FrameWork.Model
{
   internal  class DataProviderObject
    {
        private static IFacturation _facturationDal = null;
        static ParametresModel OldParametersDatabase = null;
        static  ParametresModel  ParametersDatabase = GlobalDatas.dataBasparameter;
         
        static  StringBuilder chaine = new StringBuilder();
        static StringBuilder chaines = new StringBuilder();
        //string.Format("SERVER={0};UID='{1}'; PASSWORD='{2}';database='{3}'", tablist[0], tablist[2], tablist[4], tablist[3]);
        public static string GetStringConnection
        {
            get
            {
                //string returnString = string.Empty;
                //if (OldParametersDatabase == null)
                //{
                //    chaines = new StringBuilder();

                //    chaines.Append(ParametersDatabase.Nomserveur);
                //    chaines.Append(";");
                //    chaines.Append(ParametersDatabase.Port);
                //    chaines.Append(";");
                //    chaines.Append(ParametersDatabase.Utilisateur);
                //    chaines.Append(";");
                //    chaines.Append(ParametersDatabase.NomBd);
                //    chaines.Append(";");
                //    chaines.Append(ParametersDatabase.Motpasse);
                //    OldParametersDatabase = ParametersDatabase;

                //    returnString = chaines.ToString();
                //}
                //else
                //{


                //    if (!ParametersDatabase.Nomserveur.Equals(OldParametersDatabase.Nomserveur) ||
                //        !ParametersDatabase.Port.Equals(OldParametersDatabase.Port) ||
                //        !ParametersDatabase.NomBd.Equals(OldParametersDatabase.NomBd) ||
                //         !ParametersDatabase.Motpasse.Equals(OldParametersDatabase.Motpasse))
                //    {
                //        chaine = new StringBuilder();

                //        chaine.Append(ParametersDatabase.Nomserveur);
                //        chaine.Append(";");
                //        chaine.Append(ParametersDatabase.Port);
                //        chaine.Append(";");
                //        chaine.Append(ParametersDatabase.Utilisateur);
                //        chaine.Append(";");
                //        chaine.Append(ParametersDatabase.NomBd);
                //        chaine.Append(";");
                //        chaine.Append(ParametersDatabase.Motpasse);
                //        returnString = chaine.ToString();
                //        // _facturationDal = new Facturation(chaine.ToString());
                //    }
                //    else returnString = chaine.ToString();
                //}
                return GlobalDatas.ConnectionString;
            }
        }
        public static IFacturation FacturationDal
        {


            get
            {
                try
                {
                    //if (_facturationDal == null)
                    //{
                    //if (OldParametersDatabase == null) OldParametersDatabase = ParametersDatabase;
                    //if (OldParametersDatabase == null)
                    //{
                    //    chaine = new StringBuilder();

                    //    chaine.Append(ParametersDatabase.Nomserveur);
                    //    chaine.Append(";");
                    //    chaine.Append(ParametersDatabase.Port);
                    //    chaine.Append(";");
                    //    chaine.Append(ParametersDatabase.Utilisateur);
                    //    chaine.Append(";");
                    //    chaine.Append(ParametersDatabase.NomBd);
                    //    chaine.Append(";");
                    //    chaine.Append(ParametersDatabase.Motpasse);
                    //    OldParametersDatabase = ParametersDatabase;

                    //    string machaine = GlobalDatas.ConnectionString;
                    //    _facturationDal = new Facturation(chaine.ToString());
                    //}
                    //else
                    //{


                    //if ( ! ParametersDatabase.Nomserveur.Equals(OldParametersDatabase.Nomserveur) ||
                    //    ! ParametersDatabase.Port .Equals (OldParametersDatabase .Port ) ||
                    //    !ParametersDatabase.NomBd.Equals(OldParametersDatabase.NomBd) ||
                    //     ! ParametersDatabase.Motpasse .Equals (OldParametersDatabase .Motpasse  ) )
                    //{
                    //    chaine = new StringBuilder();

                    //    chaine.Append(ParametersDatabase.Nomserveur);
                    //    chaine.Append(";");
                    //    chaine.Append(ParametersDatabase.Port);
                    //    chaine.Append(";");
                    //    chaine.Append(ParametersDatabase.Utilisateur);
                    //    chaine.Append(";");
                    //    chaine.Append(ParametersDatabase.NomBd);
                    //    chaine.Append(";");
                    //    chaine.Append(ParametersDatabase.Motpasse);

                    //    _facturationDal = new Facturation(chaine.ToString());
                    //}
                    //else
                    //{
                    //    chaine = new StringBuilder();

                    //    chaine.Append(ParametersDatabase.Nomserveur);
                    //    chaine.Append(";");
                    //    chaine.Append(ParametersDatabase.Port);
                    //    chaine.Append(";");
                    //    chaine.Append(ParametersDatabase.Utilisateur);
                    //    chaine.Append(";");
                    //    chaine.Append(ParametersDatabase.NomBd);
                    //    chaine.Append(";");
                    //    chaine.Append(ParametersDatabase.Motpasse);

                    //    _facturationDal = new Facturation(chaine.ToString());
                    //}
                    //}
                    _facturationDal = new Facturation(GlobalDatas.ConnectionString);  
                    return DataProviderObject._facturationDal;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message );
                }
                }
            
            
        }

        public static IFacturation GetStringArchive
        {
            get
            {
                //StringBuilder chaine = new StringBuilder();
                //chaine.Append(GlobalDatas.dataBasparameter.NomserveurArchive);
                //chaine.Append(";");
                //chaine.Append(GlobalDatas.dataBasparameter.PortArchive);
                //chaine.Append(";");
                //chaine.Append(GlobalDatas.dataBasparameter.UtilisateurArchive);
                //chaine.Append(";");
                //chaine.Append(GlobalDatas.dataBasparameter.NomBdArchive);
                //chaine.Append(";");
                //chaine.Append(GlobalDatas.dataBasparameter.MotpasseArchive);

                _facturationDal = new Facturation(GlobalDatas.ConnectionString);
                return _facturationDal;
            }
        }
      
      
    }
}
