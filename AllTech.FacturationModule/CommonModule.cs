using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AllTech.FrameWork.Model;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using AllTech.FrameWork.Global;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using AllTech.FrameWork.Views;
using System.Xml;
using System.IO.Compression;

namespace AllTech.FacturationModule
{
  public   class CommonModule
    {

     
        #region INDEXE COMBOBOXE


      public static int GetindexeComboBoxClient(ObservableCollection<ClientModel> clients, int id)
      {
          int position = -1;

          try
          {
              if (clients != null)
              {
                  var client = clients.FirstOrDefault(cl=>cl.IdClient ==id);
                  if (client != null)
                  {
                      position = 0;
                      foreach (ClientModel cli in clients)
                      {

                          if (cli.IdClient == id)
                              break;

                          position++;

                      }
                  }
                  //if (id > 0)
                  //{
                  //    position = 0;
                  //    foreach (ClientModel cli in clients)
                  //    {

                  //        if (cli.IdClient == id)
                  //            break;

                  //        position++;

                  //    }
                  //}
              }

          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }

          return position;
      }

      public static int GetindexeComboBoxOjetFacture(ObservableCollection<ObjetFactureModel> listes, int id)
      {
          int position = 0;
          try { 
          if (listes != null)
          {
              var objet = listes.FirstOrDefault(ob=>ob.IdObjet ==id );
              if (objet != null)
              {
                  position = 0;
                  foreach (ObjetFactureModel cli in listes)
                  {

                      if (cli.IdObjet == id)
                          break;
                      position++;

                  }
              }
              //if (id > 0)
              //{
              //    position = 0;
              //    foreach (ObjetFactureModel cli in listes)
              //    {
                    
              //        if (cli.IdObjet  == id)
              //            break;
              //        position++;

              //    }
              //}
          }
          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }

          return position;
      }

      public static int GetindexeComboBoxExploitationFacture(ObservableCollection<ExploitationFactureModel > listes, int id)
      {
          int position =-1;
          try { 
          if (listes != null)
          {
              var exploit = listes.FirstOrDefault(exp => exp.IdExploitation == id);
              if (exploit != null)
              {
                  position = 0;
                  foreach (ExploitationFactureModel cli in listes)
                  {

                      if (cli.IdExploitation == id)
                          break;
                      position++;

                  }

              }
              //if (id > 0)
              //{
              //    position = 0;
              //    foreach (ExploitationFactureModel cli in listes)
              //    {
                     
              //        if (cli.IdExploitation   == id)
              //            break;
              //        position++;

              //    }
              //}
          }
          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }

        
          return position;
      }

      public static int GetindexeComboBoxDepartement(List<DepartementModel> listes, int id)
      {
          int position = -1;
          try { 
          if (listes != null)
          {
              if (id > 0)
              {
                  position = 0;
                  foreach (DepartementModel cli in listes)
                  {
                  
                      if (cli.IdDep  == id)
                          break;
                      position++;

                  }
              }
          }
          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }

          return position;
      }

      public static int GetindexeComboBoxProduit(ObservableCollection<ProduitModel> listes, int id)
      {
          int position = 0;
          try { 
          if (listes != null)
          {
              if (id > 0)
              {
                  foreach (ProduitModel cli in listes)
                  {

                      if (cli.IdProduit  == id)
                          break;
                      position++;

                  }
              }
          }
          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }

          return position;
      }


      public static int GetindexeComboBoxExoneration(List<ExonerationModel> exonerations,int id)
      {
          int position = 0;
          try { 
          if (exonerations != null)
          {
              if (id > 0)
              {
                  foreach (ExonerationModel exo in exonerations)
                  {
                      if (exo.ID == id)
                          break;
                      position++;
                  }
              }
          }
          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }

          return position ;
      }


      public static int GetindexeComboBoxDeviseFacturation(List<DeviseModel> liste, int id)
      {
          int position = 0;
          try { 
          if (liste != null)
          {
              if (id > 0)
              {
                  foreach (DeviseModel obj in liste)
                  {
                      if (obj.ID_Devise  == id)
                          break;
                      position++;
                  }
              }
          }
          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }

          return position;
      }

      public static int GetindexeComboBoxProrata(List<TaxeModel> liste, int id)
      {
          int position = 0;
          if (liste != null)
          {
              if (id > 0)
              {
                  foreach (TaxeModel obj in liste)
                  {
                      if (obj.ID_Taxe  == id)
                          break;
                      position++;
                  }
              }
          }

          return position;
      }


      public static int GetindexeComboBoxCompte(List<CompteModel> liste, int id)
      {
          int position = 0;
          if (liste != null)
          {
              if (id > 0)
              {
                  foreach (CompteModel obj in liste)
                  {
                      if (obj.ID  == id)
                          break;
                      position++;
                  }
              }
          }

          return position;
      }

      public static int GetindexeComboBoxLibelleTerme(List<LibelleTermeModel> liste, int id)
      {
          int position = 0;
          if (liste != null)
          {
              if (id > 0)
              {
                  foreach (LibelleTermeModel obj in liste)
                  {
                      if (obj.ID  == id)
                          break;
                      position++;
                  }
              }
          }

          return position;
      }


        #endregion

        #region FILTRE LISTE FACTURE


      public static DataTable SetDataTableFacture()
      {

          DataTable newTable = new DataTable();
          DataColumn col1 = new DataColumn("idfacture", typeof(Int64));
          DataColumn col2 = new DataColumn("numfact", typeof(string));
          DataColumn col3 = new DataColumn("moisPrest", typeof(DateTime));

          DataColumn col4 = new DataColumn("idobjet", typeof(Int32));
          DataColumn col5 = new DataColumn("idclient", typeof(Int32));
          DataColumn col6 = new DataColumn("idtaxe", typeof(Int32));
          DataColumn col7 = new DataColumn("idevise", typeof(Int32));
          DataColumn col8 = new DataColumn("idstatut", typeof(Int32));

          DataColumn col9 = new DataColumn("idmode", typeof(Int32));
          DataColumn col10 = new DataColumn("idcree", typeof(Int32));
          DataColumn col11 = new DataColumn("idmodif", typeof(Int32));
          DataColumn col12 = new DataColumn("idmodep", typeof(Int32));

          DataColumn col13 = new DataColumn("datecloture", typeof(string));
          DataColumn col14 = new DataColumn("datecheance", typeof(string ));
          DataColumn col15 = new DataColumn("datecreation", typeof(string));

          DataColumn col16 = new DataColumn("objetfact", typeof(string));
          DataColumn col17 = new DataColumn("explotfact", typeof(string));//16
          DataColumn col18 = new DataColumn("client", typeof(string));
          DataColumn col19= new DataColumn("utilisateur", typeof(string));
          DataColumn col20 = new DataColumn("taxe", typeof(string));
          DataColumn col21 = new DataColumn("devise", typeof(string));
          DataColumn col22 = new DataColumn("statut", typeof(string));
          DataColumn col23 = new DataColumn("modep", typeof(string));
          DataColumn col24 = new DataColumn("color", typeof(string));
          DataColumn col25 = new DataColumn("clienOk", typeof(bool));
          DataColumn col26 = new DataColumn("clientBg", typeof(string));
          DataColumn col27 = new DataColumn("rowNum", typeof(long));
          DataColumn col28 = new DataColumn("ttc", typeof(double));
          DataColumn col29 = new DataColumn("st", typeof(int));
          DataColumn col30 = new DataColumn("idsite", typeof(int));
       
        


          newTable.Columns.Add(col1);
          newTable.Columns.Add(col2);
          newTable.Columns.Add(col3);
          newTable.Columns.Add(col4);

          newTable.Columns.Add(col5);
          newTable.Columns.Add(col6);
          newTable.Columns.Add(col7);
          newTable.Columns.Add(col8);
          newTable.Columns.Add(col9);
          newTable.Columns.Add(col10);
          newTable.Columns.Add(col11);
          newTable.Columns.Add(col12);
          newTable.Columns.Add(col13);
          newTable.Columns.Add(col14);
          newTable.Columns.Add(col15);

          newTable.Columns.Add(col16);
          newTable.Columns.Add(col17);
          newTable.Columns.Add(col18);
          newTable.Columns.Add(col19);
          newTable.Columns.Add(col20);
          newTable.Columns.Add(col21);
          newTable.Columns.Add(col22);
          newTable.Columns.Add(col23);
          newTable.Columns.Add(col24);
          newTable.Columns.Add(col25);
          newTable.Columns.Add(col26);
          newTable.Columns.Add(col27);
          newTable.Columns.Add(col28);
          newTable.Columns.Add(col29);
          newTable.Columns.Add(col30);
        

          return newTable;
      }


         public static void AdDatasInDatatable(ObservableCollection<FactureModel> CacheFacturesListe, DataTable newTable)
      {
            DataRow row = null;
            if (CacheFacturesListe != null)
            {
                foreach (FactureModel sm in CacheFacturesListe)
                {
                    row = newTable.NewRow();
                    row[0] = sm.IdFacture;
                    row[1] = sm.NumeroFacture;
                    row[2] = sm.MoisPrestation != null ? sm.MoisPrestation : DateTime.MinValue;
                    row[3] = sm.IdObjetFacture;
                    row[4] = sm.IdClient;
                    row[5] = sm.IdTaxe;
                    row[6] = sm.IdDevise;
                    row[7] = sm.IdStatut;
                    row[8] = sm.IdModePaiement;
                    row[9] = sm.IdCreerpar;
                    row[10] = sm.IdModifierPar;
                    row[11] = sm.IdModePaiement;
                    row[12] = sm.DateCloture != null ? ((DateTime)sm.DateCloture).ToShortDateString() : null ;
                    row[13] = sm.DateEcheance != null ? ((DateTime)sm.DateEcheance).ToShortDateString() : null ;
                    row[14] = sm.DateCreation != null ? ((DateTime)sm.DateCreation).ToShortDateString() : null;


                    row[15] = CacheFacturesListe.First(ob => ob.IdObjetFacture == sm.IdObjetFacture).CurrentObjetFacture ?? CacheFacturesListe.First(ob => ob.IdObjetFacture == sm.IdObjetFacture).CurrentObjetFacture;
                    if (sm.CurrentExploitation != null)
                        row[16] = CacheFacturesListe.First(ob => ob.IdExploitation == sm.IdExploitation).CurrentExploitation.Libelle;
                    row[17] = CacheFacturesListe.FirstOrDefault(cli => cli.IdClient == sm.IdClient).CurrentClient.NomClient;
                    row[18] = CacheFacturesListe.FirstOrDefault(us => us.IdCreerpar == sm.IdCreerpar).UserCreate.Nom;
                    row[19] = CacheFacturesListe.FirstOrDefault(t => t.IdTaxe == sm.IdTaxe).CurrentTaxe.Libelle;
                    row[20] = CacheFacturesListe.FirstOrDefault(d => d.IdDevise == sm.IdDevise).CurrentDevise.Libelle;
                    row[21] = CacheFacturesListe.FirstOrDefault(st => st.IdStatut == sm.IdStatut).CurrentStatut.Libelle;
                    row[22] = CacheFacturesListe.FirstOrDefault(mp => mp.IdModePaiement == sm.IdModePaiement).CurrentModePaiement.Libelle;
                    row[23] = sm.BackGround;
                    row[24] = sm.ClienOk;
                    row[25] = sm.ClientbackGround;
                    row[26] = sm.NumeroLigne ;
                    row[27] = sm.TotalTTC ;
                    row[28] = sm.icon ;
                    row[29] = sm.IdSite ;
                  




                    newTable.Rows.Add(row);
                }
            }
      }

        #endregion

        #region FILTRE CLIENT

         public static DataTable SetDataTableClient()
         {
             DataTable newTable = new DataTable();
             DataColumn col1 = new DataColumn("id", typeof(Int32));
             DataColumn col2 = new DataColumn("nom", typeof(string));
             DataColumn col4 = new DataColumn("contribuable", typeof(string));
             DataColumn col5 = new DataColumn("ville", typeof(string));
             DataColumn col6 = new DataColumn("rue1", typeof(string));
             DataColumn col7 = new DataColumn("rue2", typeof(string));
             DataColumn col8 = new DataColumn("idlangue", typeof(Int32));
             DataColumn col9 = new DataColumn("termenbre", typeof(Int32));
             DataColumn col10 = new DataColumn("termedesc", typeof(string));
             DataColumn col11 = new DataColumn("idporata", typeof(int));
             DataColumn col12 = new DataColumn("dateecheance ", typeof(string));
             DataColumn col13 = new DataColumn("boitepostal", typeof(string));
             DataColumn col14 = new DataColumn("idexonere", typeof(Int32));
             DataColumn col15 = new DataColumn("idsite", typeof(Int32));
             DataColumn col16 = new DataColumn("idcompte", typeof(Int32));
             DataColumn col17 = new DataColumn("numeroimat", typeof(string));
             DataColumn col18 = new DataColumn("idDevise", typeof(int));
             DataColumn col19 = new DataColumn("idTerme", typeof(int));

             newTable.Columns.Add(col1);
             newTable.Columns.Add(col2);
             newTable.Columns.Add(col4);
             newTable.Columns.Add(col5);
             newTable.Columns.Add(col6);
             newTable.Columns.Add(col7);
             newTable.Columns.Add(col8);
             newTable.Columns.Add(col9);
             newTable.Columns.Add(col10);
             newTable.Columns.Add(col11);
             newTable.Columns.Add(col12);
             newTable.Columns.Add(col13);
             newTable.Columns.Add(col14);
             newTable.Columns.Add(col15);
             newTable.Columns.Add(col16);
             newTable.Columns.Add(col17);
             newTable.Columns.Add(col18);
             newTable.Columns.Add(col19);

             return newTable;
         }
        #endregion

        #region DROITS UTILISATEUR LOGGIN
          

         #endregion

        #region AUTRES

         public static FileInfo[] GetListeFiles(string path)
         {
           
             FileInfo[] fileList = null;
             string cheminPhysique = string.Empty;
             string defaultPath = string.Empty;
            // string[] localDrives = System.Environment.GetLogicalDrives();
             //DriveInfo[] drives = DriveInfo.GetDrives();
             try
             {
                 if (string.IsNullOrEmpty(path))
                 {
                     cheminPhysique = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                     defaultPath = cheminPhysique + "\\" + path.Substring(path.IndexOf('\\') + 1);
                     defaultPath = defaultPath + "SYSFACT_Log";
                 }
                 else
                 {
                     defaultPath = path.Substring(0, path.LastIndexOf('\\')); 
                 }
                
                 //if ((path.IndexOf("D") == 0) || (path.IndexOf("C") == 0) || (path.IndexOf("E") == 0))
                 //{
                 //    string lecteur = path.Substring(0, 1);
                 //    foreach (DriveInfo drive in drives)
                 //    {
                 //        if (drive.Name.ToLower().Contains(lecteur.ToLower()))
                 //        {
                 //            if (drive.DriveType == DriveType.Fixed)
                 //            {
                 //                // un disk
                 //                defaultPath = path.Substring(0, path.LastIndexOf('\\'));
                 //            }
                 //            else
                 //            {
                 //                // le lecteur nest pas un diske  
                 //                // donc par default on utilise mes documents
                 //                cheminPhysique = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                 //                defaultPath = cheminPhysique + "\\" + path.Substring(path.IndexOf('\\') + 1);
                 //            }
                 //            break;
                 //        }
                 //    }
                 //}
                 //else
                 //{
                 //    // chemin du serveur
                 //    cheminPhysique = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                 //    defaultPath = cheminPhysique + "\\" + path.Substring(path.IndexOf('\\') + 1);
                 //}


                  
                 if (Directory.Exists(defaultPath))
                 {
                     DirectoryInfo folderRoot = new DirectoryInfo(defaultPath);
                      fileList = folderRoot.GetFiles("*.xml",  SearchOption.TopDirectoryOnly );
                     //files = Directory.GetFiles(defaultPath, "*.*", SearchOption.TopDirectoryOnly);
                     
                 }
             }
             catch (Exception ex)
             {
                 throw new Exception(ex .Message );
             }
             return fileList;
         }


         public static bool GetDirectoryLog(string pathdefault)
         {
             bool isvaluestrue = false;
             string cheminPhysique=string .Empty ;
             string defaultPath = string.Empty;
             try
             {
                 if (string.IsNullOrEmpty(pathdefault))
                 {
                     cheminPhysique = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                     defaultPath = cheminPhysique + "\\" + pathdefault.Substring(pathdefault.IndexOf('\\') + 1);
                 }
                 else
                 {
                     defaultPath = pathdefault.Substring(0, pathdefault.LastIndexOf('\\')); 
                 }



                 //int val= pathdefault.IndexOf ("D");
                 //string newstr=pathdefault.Substring (pathdefault.IndexOf ('\\')+1);
                 //DriveInfo[] drives = DriveInfo.GetDrives();
                 //string[] localDrives = System.Environment.GetLogicalDrives();

                 //if ((pathdefault.IndexOf("D") == 0) || (pathdefault.IndexOf("C") == 0) || (pathdefault.IndexOf("E") == 0))
                 //{
                 //    string  lecteur = pathdefault.Substring(0,1);
                 //    foreach (DriveInfo drive in drives)
                 //    {
                 //        if (drive.Name.ToLower().Contains(lecteur.ToLower()))
                 //        {
                 //            if (drive.DriveType == DriveType.Fixed)
                 //            {
                 //                // un disk
                 //                defaultPath = pathdefault.Substring(0, pathdefault.LastIndexOf('\\'));
                 //            }
                 //            else
                 //            {
                 //                // le lecteur nest pas un diske  
                 //                // donc par default on utilise mes documents
                 //                cheminPhysique = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                 //                defaultPath = cheminPhysique + "\\" + pathdefault.Substring(pathdefault.IndexOf('\\') + 1);
                 //            }
                 //            break;
                 //        }
                 //    }
                    
                 //}
                 //else
                 //{
                 //    // acces au serveur
                 //    cheminPhysique = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                 //    defaultPath = cheminPhysique + "\\" + pathdefault.Substring(pathdefault.IndexOf('\\') + 1);
                 //}

             

                 if (!Directory.Exists(defaultPath))
                 {
                     Directory.CreateDirectory(defaultPath);
                     isvaluestrue = true;
                 }
                 else isvaluestrue = true;

             }
             catch (IOException ex)
             {
                 throw new IOException(ex .Message );
             }
             return isvaluestrue;
         }

         public static string GetLogBAckUpPath(string backUpathdefault)
         {
             string cheminPhysique = string.Empty;
             string defaultPath = string.Empty;
             try
             {
                 if (string.IsNullOrEmpty(backUpathdefault))
                 {
                     cheminPhysique = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                     defaultPath = cheminPhysique + "\\" + backUpathdefault.Substring(backUpathdefault.IndexOf('\\') + 1);
                     defaultPath = defaultPath + "SYSFACT_BackUpLog";
                 }
                 else
                 {
                     defaultPath = backUpathdefault.Substring(0, backUpathdefault.LastIndexOf('\\'));
                    
                 }
                 if (!Directory.Exists(defaultPath))
                 {
                     Directory.CreateDirectory(defaultPath);
                    
                 }

                 return defaultPath;
             }
             catch (IOException ex)
             {
                 throw new IOException(ex.Message);
             }
         }
         public static string GetLogPath(string pathdefault)
         {
             string cheminPhysique = string.Empty;
             string defaultPath = string.Empty;

             if (string.IsNullOrEmpty(pathdefault))
             {
                 cheminPhysique = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                   defaultPath = cheminPhysique + "\\" + pathdefault.Substring(pathdefault.IndexOf('\\') + 1);
                   defaultPath = defaultPath + "SYSFACT_Log";
             }
             else
             {
                 defaultPath = pathdefault.Substring(0, pathdefault.LastIndexOf('\\'));
             }
            
             //if ((pathdefault.IndexOf("D") == 0) || (pathdefault.IndexOf("C") == 0) || (pathdefault.IndexOf("E") == 0))
             //{
             //     string  lecteur = pathdefault.Substring(0,1);
             //     foreach (DriveInfo drive in drives)
             //     {
             //         if (drive.Name.ToLower().Contains(lecteur.ToLower()))
             //         {
             //             if (drive.DriveType == DriveType.Fixed)
             //             {
             //                 // un disk
             //                 defaultPath = pathdefault.Substring(0, pathdefault.LastIndexOf('\\'));
             //             }
             //             else
             //             {
             //                 // le lecteur nest pas un diske  
             //                 // donc par default on utilise mes documents
             //                 cheminPhysique = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
             //                 defaultPath = cheminPhysique + "\\" + pathdefault.Substring(pathdefault.IndexOf('\\') + 1);
             //             }
             //             break;
             //         }
             //     }
             //    cheminPhysique = "D:";
             //}
             //else
             //{
             //    // chemin du serveur
             //    cheminPhysique = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
             //    defaultPath = cheminPhysique + "\\" + pathdefault.Substring(pathdefault.IndexOf('\\') + 1);
             //}
            

            // string defaultPath = cheminPhysique + "\\" + pathdefault.Substring(pathdefault.IndexOf('\\') + 1);
             if (!Directory.Exists(defaultPath))
                     Directory.CreateDirectory(defaultPath);

             return defaultPath;
         }

        
   

         public static void SaveImages(string image,string nomUtilisateur)
         {
             try
             {
                 string chemin = Utils.getUrlDossierimages();
                 string path = chemin + "\\" + GlobalDatas.dataBasparameter.DossierImages ;
                 if (Utils.CreateDirectory(path))
                 {
                     string nomImage = image.Substring(image.LastIndexOf("\\") + 1);
                     string nouveau = nomUtilisateur.Trim () + DateTime.Now.Year + nomImage;
                     if (!File.Exists(path + "\\" + nouveau))
                     {
                         FileStream fs = new FileStream(@image, FileMode.Open, FileAccess.Read);
                         Bitmap bitmap = new Bitmap((Bitmap)System.Drawing.Image.FromStream(fs));
                         fs.Close();
                         bitmap.Save(path + "\\" + nouveau);
                     }

                     
                 }
             }
             catch (Exception ex)
             {
                 throw new Exception("Erreure Copy de l'image dans le dossier spécifique"+ex.Message );
             }
         }


         public static string GetImage(string imagebd)
         {
             try
             {
                 string chemin = Utils.getUrlDossierimages();
                 string path = string.Empty;
                 if (File.Exists(chemin + "\\" + GlobalDatas.dataBasparameter.DossierImages + "\\" + imagebd))
                 {
                      path = chemin + "\\" + GlobalDatas.dataBasparameter.DossierImages + "\\" + imagebd;
                     FileStream fs = new FileStream(@path, FileMode.Open, FileAccess.Read);
                 }
                
                 return path;
                 
             }
             catch (Exception ex)
             {
                 throw new Exception("Erreure récuperation de l'mage");
             }
         }
        #endregion

        #region EXPORT DATA

        static LigneFactureModel  factureservice = null;
         public static void ExportToExcel(IEnumerable <FactureModel> listeview,SocieteModel company)
         {
             CreateDoc(listeview, company);
          
           
         }
         public static void ExportToExcel(DataTable  listeview)
         {
             CreateDoc(listeview);


         }

        

         public static void ExportfactureSortiesToExcel(List<FactureModel> listeview, string debut, string fin)
         {
             CreateDocSorties(listeview);


         }

         static void CreateDoc(DataTable  listeview)
         {
             try
             {
                 Excel.Application xlApp = new Excel.Application();
                 xlApp.Visible = false;
                 object misValue = System.Reflection.Missing.Value;
                 Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(1);


                 Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];
                 xlWorkSheet.Name = "Data Sheet";
                 // Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.Add("Data Sheet");
                 Excel.Range chartRangeTitre;


                 chartRangeTitre = xlWorkSheet.get_Range("C2", "G2");
                 chartRangeTitre.Merge(5);
                 chartRangeTitre.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Chocolate);
                 chartRangeTitre.Font.Size = 30;
                 chartRangeTitre.Font.Italic = true;
                 chartRangeTitre.FormulaR1C1 = string.Format("Rapport Historique Factures Sodexo /{0}", listeview.Rows[0]["Pays"].ToString());

                 //

                 Excel.Range chartRange;
                 chartRange = xlWorkSheet.get_Range("B4", "L4");
                 chartRange.Font.Bold = true;
                 chartRange.Font.Italic = true;
                 chartRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                 chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                 chartRange.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;

                 Excel.Range chartRangeA;
                 chartRangeA = xlWorkSheet.get_Range("B4", "B4");
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 chartRangeA.Font.Bold = true;
                 chartRangeA.ColumnWidth = 30;

                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 2] = "Facture #";

                 chartRangeA = xlWorkSheet.get_Range("C4", "C4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 3] = "Client";

                 chartRangeA = xlWorkSheet.get_Range("D4", "D4");
                 chartRangeA.ColumnWidth = 30;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 4] = "Date Création";

                 chartRangeA = xlWorkSheet.get_Range("E4", "E4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 5] = "Statut";

                 chartRangeA = xlWorkSheet.get_Range("F4", "F4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 6] = "Mois Prestation";


                 chartRangeA = xlWorkSheet.get_Range("G4", "G4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 7] = "Objet Facture";


                 chartRangeA = xlWorkSheet.get_Range("H4", "H4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 8] = "Département";

                 chartRangeA = xlWorkSheet.get_Range("I4", "I4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 9] = "Total HT";

                 chartRangeA = xlWorkSheet.get_Range("J4", "J4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 10] = "Prorata";

                 chartRangeA = xlWorkSheet.get_Range("K4", "K4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 11] = "Tva";

                 chartRangeA = xlWorkSheet.get_Range("L4", "L4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 12] = "Total TTC";

                 int k = 5;

                 foreach (DataRow fact in listeview.Rows)
                 {
                     object[] result = GetCovertFactureItem(fact);

                     Excel.Range chartRangeCell;
                     chartRangeCell = xlWorkSheet.get_Range("B" + k.ToString(), "L" + k.ToString());
                     chartRangeCell.Borders.Color = System.Drawing.Color.Black.ToArgb();
                     chartRangeCell.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;

                     xlWorkSheet.Cells[k, 2] = fact["Numero_Facture"].ToString();

                        Excel.Range chartRangeClient = xlWorkSheet.get_Range("C" + k.ToString(), "C" + k.ToString());
                     chartRangeClient.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft ;
                     xlWorkSheet.Cells[k, 3] = fact["ClientFacture"].ToString();
                    // if (!Convert.ToBoolean(fact["IsCheck"]))
                         chartRangeClient.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);

                     Excel.Range chartRangeDateformat = xlWorkSheet.get_Range("D" + k.ToString(), "D" + k.ToString());

                     xlWorkSheet.Cells[k, 4] =Convert.ToDateTime ( fact["Date_Creation"]).ToShortDateString();

                     Excel.Range chartRangeStatut = xlWorkSheet.get_Range("E" + k.ToString(), "E" + k.ToString());
                     chartRangeStatut.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                     xlWorkSheet.Cells[k, 5] = fact["StatutFacture"].ToString();
                     xlWorkSheet.Cells[k, 6] =Convert .ToDateTime ( fact["MoisPrestation"]).ToString("MMM-yyyy");

                     Excel.Range chartRangeObjet = xlWorkSheet.get_Range("G" + k.ToString(), "G" + k.ToString());
                     chartRangeObjet.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                     xlWorkSheet.Cells[k, 7] = fact["ObjetFacture"] !=DBNull.Value ? fact["ObjetFacture"].ToString():string .Empty ;

                     Excel.Range chartRangedep = xlWorkSheet.get_Range("H" + k.ToString(), "H" + k.ToString());
                     chartRangedep.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                     xlWorkSheet.Cells[k, 8] = Convert.ToInt32(fact["ID_Departement"]) != 1500 ? (fact["factureDepartment"].ToString().Contains("...") == true ? string.Empty : fact["factureDepartment"].ToString()) : string.Empty;

                     Excel.Range chartRangeFormate = xlWorkSheet.get_Range("I" + k.ToString(), "I" + k.ToString());
                     chartRangeFormate.NumberFormat = "# ### ### ##0";
                     chartRangeFormate.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                     xlWorkSheet.Cells[k, 9] = result[4]; // total HT

                     Excel.Range chartRangeFormateProrata = xlWorkSheet.get_Range("J" + k.ToString(), "J" + k.ToString());
                     chartRangeFormateProrata.NumberFormat = "# ### ### ##0";
                     chartRangeFormateProrata.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                     xlWorkSheet.Cells[k, 10] = result[3]; //prorata

                     Excel.Range chartRangeFormateTva = xlWorkSheet.get_Range("K" + k.ToString(), "K" + k.ToString());
                     chartRangeFormateTva.NumberFormat = "# ### ### ##0";
                     chartRangeFormateTva.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                     xlWorkSheet.Cells[k, 11] = result[1]; // tva

                     Excel.Range chartRangeFormateTtc = xlWorkSheet.get_Range("L" + k.ToString(), "L" + k.ToString());
                     chartRangeFormateTtc.NumberFormat = "# ### ### ##0";
                     chartRangeFormateTtc.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                     xlWorkSheet.Cells[k, 12] = Convert.ToDouble(fact["totalTTC"]); // total TTC
                     k++;
                 }

                 xlApp.Visible = true;

             }
             catch (Exception ex)
             {
                 throw new Exception(" Erreure Creation Fichier Excel" + ex.Message);
             }
         }


         static void CreateDoc(IEnumerable<FactureModel> listeview, SocieteModel company)
         {
             try
             {
                 factureservice = new LigneFactureModel();

                 Excel.Application xlApp = new Excel.Application();
                 xlApp.Visible = false;
                 object misValue = System.Reflection.Missing.Value;
                 Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(1);
                

                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];
                xlWorkSheet.Name = "Data Sheet";
                // Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.Add("Data Sheet");
                 Excel.Range chartRangeTitre;

                
                 chartRangeTitre = xlWorkSheet.get_Range("C2", "G2");
                 chartRangeTitre.Merge(5);
                 chartRangeTitre.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Chocolate);
                 chartRangeTitre.Font.Size = 30;
                 chartRangeTitre.Font.Italic = true;
                 chartRangeTitre.FormulaR1C1 = string.Format("Rapport Historique Factures Sodexo /{0}", company.Pays );



                 Excel.Range chartRange;
                 chartRange = xlWorkSheet.get_Range("B4", "O4");
                 chartRange.Font.Bold = true;
                 chartRange.Font.Italic = true;
                 chartRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                 chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                 chartRange.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;

                 Excel.Range chartRangeA;
                 chartRangeA = xlWorkSheet.get_Range("B4", "B4");
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 chartRangeA.Font.Bold = true ;
                 chartRangeA.ColumnWidth = 30;
             
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 2] = "Facture #";

                 chartRangeA = xlWorkSheet.get_Range("C4", "C4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 3] = "Client";

                 chartRangeA = xlWorkSheet.get_Range("D4", "D4");
                 chartRangeA.ColumnWidth = 30;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 4] = "Date Création";

                 chartRangeA = xlWorkSheet.get_Range("E4", "E4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 5] = "Statut";

                 chartRangeA = xlWorkSheet.get_Range("F4", "F4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 6] = "Mois Prestation";


                 chartRangeA = xlWorkSheet.get_Range("G4", "G4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 7] = "Objet Facture";


                 chartRangeA = xlWorkSheet.get_Range("H4", "H4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 8] = "Département";

                 chartRangeA = xlWorkSheet.get_Range("I4", "I4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 9] = "Compte Tiers";


                 chartRangeA = xlWorkSheet.get_Range("J4", "J4");
                 chartRangeA.ColumnWidth = 30;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 10] = "Compte Analytique/Exploitation";

                 chartRangeA = xlWorkSheet.get_Range("K4", "K4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 11] = "Total HT";

                 chartRangeA = xlWorkSheet.get_Range("L4", "L4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 12] = "Tva";

                 chartRangeA = xlWorkSheet.get_Range("M4", "M4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 13] = "Centimes";

                

                 chartRangeA = xlWorkSheet.get_Range("N4", "N4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 14] = "Marge Bénéficiaire";

                 chartRangeA = xlWorkSheet.get_Range("O4", "O4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 15] = "Total TTC";


       
              

                 int k = 5;
                 foreach (FactureModel fact in listeview)
                 {
                   // object[] result= GetCovertFactureItem(fact);

                     Excel.Range chartRangeCell;
                     chartRangeCell = xlWorkSheet.get_Range("B" + k.ToString(), "O" + k.ToString());
                     chartRangeCell.Borders.Color = System.Drawing.Color.Black.ToArgb();
                     chartRangeCell.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    

                     xlWorkSheet.Cells[k, 2] = fact.NumeroFacture;

                     Excel.Range chartRangeClient = xlWorkSheet.get_Range("C" + k.ToString(), "C" + k.ToString());
                     chartRangeClient.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft ;
                     xlWorkSheet.Cells[k, 3] = fact.LibelleClient;
                    // if (!fact.ClienOk)
                         chartRangeClient.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);

                   
                     Excel.Range chartRangeDateformat = xlWorkSheet.get_Range("D" + k.ToString(), "D" + k.ToString());
                     chartRangeDateformat.NumberFormat = "MM/DD/YYYY";
                     xlWorkSheet.Cells[k, 4] = fact.DateCreation.Value.ToShortDateString();

                     Excel.Range chartRangeStatut = xlWorkSheet.get_Range("E" + k.ToString(), "E" + k.ToString());
                     chartRangeStatut.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                     xlWorkSheet.Cells[k, 5] = fact.LibelleStatut;
                     xlWorkSheet.Cells[k, 6] = fact.MoisPrestation.Value.ToString ("MMM-yyyy");

                     Excel.Range chartRangeObjet = xlWorkSheet.get_Range("G" + k.ToString(), "G" + k.ToString());
                     chartRangeObjet.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                     xlWorkSheet.Cells[k, 7] = fact.LibelleClientObjet !=null ? fact.LibelleClientObjet:string .Empty  ;

                     Excel.Range chartRangedep = xlWorkSheet.get_Range("H" + k.ToString(), "H" + k.ToString());
                     chartRangedep.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                     xlWorkSheet.Cells[k, 8] = fact.IdDepartement != 150001 ? (fact.LibelleDepartement.Contains("...") == true ? string.Empty : fact.LibelleDepartement) : string.Empty;

                     Excel.Range chartRangecmpTier = xlWorkSheet.get_Range("I" + k.ToString(), "I" + k.ToString());
                     chartRangecmpTier.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                     xlWorkSheet.Cells[k, 9] = fact.CompteTier != null ? fact.CompteTier : string.Empty;

                     Excel.Range chartRangeCmptenanal = xlWorkSheet.get_Range("J" + k.ToString(), "J" + k.ToString());
                     chartRangeCmptenanal.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                     xlWorkSheet.Cells[k, 10] = fact.CompteAnalytique != null ? fact.CompteAnalytique : string.Empty;


                     Excel.Range chartRangeFormate = xlWorkSheet.get_Range("K" + k.ToString(), "K" + k.ToString());
                     chartRangeFormate.NumberFormat = "# ### ### ##0";
                     chartRangeFormate.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight  ;
                     xlWorkSheet.Cells[k, 11] = fact.TotalHT; // total HT

                     Excel.Range chartRangeFormateTva = xlWorkSheet.get_Range("L" + k.ToString(), "L" + k.ToString());
                     chartRangeFormateTva.NumberFormat = "# ### ### ##0";
                     chartRangeFormateTva.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                     xlWorkSheet.Cells[k, 12] = fact.TotalTVA; // tva


                     Excel.Range chartRangeFormateProrata = xlWorkSheet.get_Range("M" + k.ToString(), "M" + k.ToString());
                     chartRangeFormateProrata.NumberFormat = "# ### ### ##0";
                     chartRangeFormateProrata.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                     xlWorkSheet.Cells[k, 13] = fact.TotalPRORATA; //prorata

                 
                     Excel.Range chartRangeFormateMarge = xlWorkSheet.get_Range("N" + k.ToString(), "N" + k.ToString());
                     chartRangeFormateMarge.NumberFormat = "# ### ### ##0";
                     chartRangeFormateMarge.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                     xlWorkSheet.Cells[k, 14] = fact.TotalMarge.HasValue?fact.TotalMarge.Value:0 ; // Marge

                     Excel.Range chartRangeFormateTtc = xlWorkSheet.get_Range("O" + k.ToString(), "O" + k.ToString());
                     chartRangeFormateTtc.NumberFormat = "# ### ### ##0";
                     chartRangeFormateTtc.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                     xlWorkSheet.Cells[k, 15] = fact.TotalTTC; // total TTC

                
                     k++;
                 }

                 xlApp.Visible = true ;
                

             }
             catch (Exception ex)
             {
                 throw new Exception(" Erreure Creation Fichier Excel"+ex .Message );
             }
         }

         static void CreateDocSorties(List<FactureModel> listeview)
         {
             try
             {
                 Excel.Application xlApp = new Excel.Application();
                 xlApp.Visible = false ;
                 object misValue = System.Reflection.Missing.Value;
                 Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(1);
                 Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];
                 Excel.Range chartRangeTitre;


                 chartRangeTitre = xlWorkSheet.get_Range("C2", "E2");
                 chartRangeTitre.Merge(5);
                 chartRangeTitre.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Chocolate);
                 chartRangeTitre.Font.Size = 30;
                 chartRangeTitre.Font.Italic = true;
                 chartRangeTitre.FormulaR1C1 = "Liste Factures Sorties/ Payées ";



                 Excel.Range chartRange;
                 chartRange = xlWorkSheet.get_Range("B4", "I4");
                 chartRange.Font.Bold = true;
                 chartRange.Font.Italic = true;
                 chartRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                 chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                 chartRange.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;

                 Excel.Range chartRangeA;
                 chartRangeA = xlWorkSheet.get_Range("B4", "B4");
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 chartRangeA.Font.Bold = true;
                 chartRangeA.ColumnWidth = 30;

                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 2] = "Numéro facture";

                 chartRangeA = xlWorkSheet.get_Range("C4", "C4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 3] = "Client";

                 chartRangeA = xlWorkSheet.get_Range("D4", "D4");
                 chartRangeA.ColumnWidth = 30;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 4] = "Date Création";

                 chartRangeA = xlWorkSheet.get_Range("E4", "E4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 5] = "Statut";

                 chartRangeA = xlWorkSheet.get_Range("F4", "F4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 6] = "Date Validation";

                 chartRangeA = xlWorkSheet.get_Range("G4", "G4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 7] = "Date Sortie";

                 chartRangeA = xlWorkSheet.get_Range("H4", "H4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 8] = "Date Paiement";


                 chartRangeA = xlWorkSheet.get_Range("I4", "I4");
                 chartRangeA.ColumnWidth = 25;
                 chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
                 xlWorkSheet.Cells[4, 9] = "Total TTC";

                



                 int k = 5;
                 foreach (FactureModel fact in listeview)
                 {
                     Excel.Range chartRangeCell;
                     chartRangeCell = xlWorkSheet.get_Range("B" + k.ToString(), "I" + k.ToString());
                     chartRangeCell.Borders.Color = System.Drawing.Color.Black.ToArgb();
                     chartRangeCell.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                     xlWorkSheet.Cells[k, 2] = fact.NumeroFacture;


                     Excel.Range chartRangeClient = xlWorkSheet.get_Range("C" + k.ToString(), "C" + k.ToString());
                     chartRangeClient.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                     xlWorkSheet.Cells[k, 3] = fact.CurrentClient.NomClient;
                     if (!fact.ClienOk)
                         chartRangeClient.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

                     Excel.Range chartRangeDateformat = xlWorkSheet.get_Range("D" + k.ToString(), "D" + k.ToString());

                     xlWorkSheet.Cells[k, 4] = fact.DateCreation.Value.ToShortDateString();
                     xlWorkSheet.Cells[k, 5] = fact.CurrentStatut.Libelle;
                     Excel.Range chartRangeDateformat1 = xlWorkSheet.get_Range("F" + k.ToString(), "F" + k.ToString());
                     xlWorkSheet.Cells[k, 6] = fact.DateCloture !=null ? fact.DateCloture .Value.ToShortDateString() :string .Empty ;
                     xlWorkSheet.Cells[k, 7] =fact.DateSortie !=null ? fact.DateSortie.Value .ToShortDateString ():string .Empty ;
                     xlWorkSheet.Cells[k, 8] = fact.DatePaiement != null ? fact.DatePaiement.Value .ToShortDateString (): string.Empty;

                     Excel.Range chartRangeFormate = xlWorkSheet.get_Range("I" + k.ToString(), "I" + k.ToString());
                     chartRangeFormate.NumberFormat = "# ### ### ##0";
                     chartRangeFormate.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                     xlWorkSheet.Cells[k, 9] = fact.TotalTTC;
                   
                   
                     k++;
                 }
                 // format  "0.00";"#,##0.00";"$#,##0.00";"#,##0;[Red]-#,##0";
                 xlApp.Visible = true ;


             }
             catch (Exception ex)
             {
                 throw new Exception(" Erreure Creation Fichier Excel"+ex .Message );
             }
         }


         public static void ExportJournalVenteToExcel(List<JournalVentesModel> listeview, string debut, string fiin,string journalNum)
         {
             Excel.Application xlApp = new Excel.Application();
             xlApp.Visible = false;
             object misValue = System.Reflection.Missing.Value;
             Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(1);
             Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[1];
             Excel.Range chartRangeTitre;


             chartRangeTitre = xlWorkSheet.get_Range("C2", "E2");
             chartRangeTitre.Merge(5);
             chartRangeTitre.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Chocolate);
             chartRangeTitre.Font.Size = 20;
             chartRangeTitre.Font.Italic = true;
             chartRangeTitre.FormulaR1C1 = "Export du Journal de ventes ";

             Excel.Range chartRangeJvNum;
             chartRangeJvNum = xlWorkSheet.get_Range("B4", "B4");
             chartRangeJvNum.Borders.Color = System.Drawing.Color.Black.ToArgb();
             chartRangeJvNum.Font.Bold = true;
             chartRangeJvNum.FormulaR1C1 = "Numéro Journal ";
             xlWorkSheet.Cells[4, 3] = journalNum;

             Excel.Range chartRangeperiode;
             chartRangeperiode = xlWorkSheet.get_Range("B5", "B5");
             chartRangeperiode.Borders.Color = System.Drawing.Color.Black.ToArgb();
             chartRangeperiode.Font.Bold = true;
             chartRangeperiode.FormulaR1C1 = "Période : ";
             xlWorkSheet.Cells[5, 3] = string.Format("{0} - {1}", debut, fiin); 

             Excel.Range chartRangeA;
             chartRangeA = xlWorkSheet.get_Range("B7", "B7");
             chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
             chartRangeA.Font.Bold = true;
             chartRangeA.ColumnWidth =20;

             chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
             xlWorkSheet.Cells[7, 2] = "Numéro facture";

             chartRangeA = xlWorkSheet.get_Range("C7", "C7");
             chartRangeA.ColumnWidth = 35;
             chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
             xlWorkSheet.Cells[7, 3] = "Client";

             chartRangeA = xlWorkSheet.get_Range("D7", "D7");
             chartRangeA.ColumnWidth = 20;
             chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
             xlWorkSheet.Cells[7, 4] = "Compte Genérale";

             chartRangeA = xlWorkSheet.get_Range("E7", "E7");
             chartRangeA.ColumnWidth = 25;
             chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
             xlWorkSheet.Cells[7, 5] = "Compte Tiers";

             chartRangeA = xlWorkSheet.get_Range("F7", "F7");
             chartRangeA.ColumnWidth = 30;
             chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
             xlWorkSheet.Cells[7, 6] = "Compte Analytique";

             chartRangeA = xlWorkSheet.get_Range("G7", "G7");
             chartRangeA.ColumnWidth = 40;
             chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
             xlWorkSheet.Cells[7, 7] = "Libelle ";

             chartRangeA = xlWorkSheet.get_Range("H7", "H7");
             chartRangeA.ColumnWidth = 25;
             chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
             xlWorkSheet.Cells[7, 8] = "Montant Débit";

             chartRangeA = xlWorkSheet.get_Range("I7", "I7");
             chartRangeA.ColumnWidth = 25;
             chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
             xlWorkSheet.Cells[7, 9] = "Montant Crédit";

             chartRangeA = xlWorkSheet.get_Range("J7", "J7");
             chartRangeA.ColumnWidth = 25;
             chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
             xlWorkSheet.Cells[7, 10] = "Statut";

             chartRangeA = xlWorkSheet.get_Range("K7", "K7");
             chartRangeA.ColumnWidth = 25;
             chartRangeA.Borders.Color = System.Drawing.Color.Black.ToArgb();
             xlWorkSheet.Cells[7, 11] = "Observation";

                int k = 8;
                foreach (JournalVentesModel fact in listeview)
                {
                    Excel.Range chartRangeCell;
                    chartRangeCell = xlWorkSheet.get_Range("B" + k.ToString(), "K" + k.ToString());
                    chartRangeCell.Borders.Color = System.Drawing.Color.Black.ToArgb();
                    chartRangeCell.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    xlWorkSheet.Cells[k, 2] = fact.NumeroFacture;
                    xlWorkSheet.Cells[k, 3] = fact.LibelleClient;
                    xlWorkSheet.Cells[k, 4] = fact.NumCompteGeneral;

                    Excel.Range chartRangecmpTier = xlWorkSheet.get_Range("E" + k.ToString(), "E" + k.ToString());
                    chartRangecmpTier.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    xlWorkSheet.Cells[k, 5] = fact.NumeroCompteTiers;

                    Excel.Range chartRangeCmptenanal = xlWorkSheet.get_Range("F" + k.ToString(), "F" + k.ToString());
                    chartRangeCmptenanal.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    xlWorkSheet.Cells[k, 6] = fact.NumeroCompteAnalytique;

                    Excel.Range chartRangeLibelle = xlWorkSheet.get_Range("G" + k.ToString(), "G" + k.ToString());
                    chartRangeLibelle.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    xlWorkSheet.Cells[k, 7] = fact.LibelleSectionAnal;

                    Excel.Range chartRangeFormateTtc = xlWorkSheet.get_Range("H" + k.ToString(), "H" + k.ToString());
                    chartRangeFormateTtc.NumberFormat = "# ### ### ##0";
                    chartRangeFormateTtc.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlWorkSheet.Cells[k, 8] = fact.MontantDebit;

                    Excel.Range chartRangeFormateCredit = xlWorkSheet.get_Range("I" + k.ToString(), "I" + k.ToString());
                    chartRangeFormateCredit.NumberFormat = "# ### ### ##0";
                    chartRangeFormateCredit.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlWorkSheet.Cells[k, 9] = fact.MontantCredit;

                    xlWorkSheet.Cells[k, 10] = fact.StatutOperation;

                  
                    Excel.Range chartRangeObs = xlWorkSheet.get_Range("K" + k.ToString(), "K" + k.ToString());
                    chartRangeObs.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    xlWorkSheet.Cells[k,11] = fact.LibelleOpertion;
                     if (fact.LibelleOpertion.Contains("HT analytyque NA "))
                         chartRangeObs.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

                    k++;
                }
                xlApp.Visible = true;
         }

      
        #endregion

         #region ZIP UNZIP FILE
         public static void ZippFile(string pathSource, string pathDest, string  fichier)
         {
             FileStream destination = File.Create(pathDest + "\\" + fichier + ".zip");
             GZipStream compStream = new GZipStream(destination, CompressionMode.Compress);
             try
             {
                 FileStream source = File.OpenRead(pathSource);
                

               
                 int theByte = source.ReadByte();
                 while (theByte != -1)
                 {
                     compStream.WriteByte((byte)theByte);
                     theByte = source.ReadByte();
                 }

                 //using (FileStream inFile = fichier.OpenRead())
                 //{
                 //    if ((File.GetAttributes(fichier.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fichier.Extension != ".gz")
                 //    {
                 //        using (FileStream outFile = File.Create(fichier.FullName + ".gz"))
                 //        {
                 //            using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                 //            {
                 //                inFile.CopyTo(Compress);
                 //            }
                 //        }
                 //    }
                 //}
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }
             finally
             {
                 compStream.Dispose();
             }
         }
         #endregion

         static object[] GetCovertFactureItem(DataRow  FatureCurrent)
         {
             object[] tabretour = new object[5];
             DetailProductModel detailService = new DetailProductModel();
             factureservice = new LigneFactureModel();
             string modeExoneration = FatureCurrent["ShortName"].ToString();
             Int64 id = Convert.ToInt64(FatureCurrent["ID"]);

             List<LigneFactureModel> items = factureservice.LIGNE_FACTURE_BYIDFActure(id);
             List<DetailProductModel> listeDetail = detailService.DETAIL_PRODUIT_BYCLIENT(Convert.ToInt32(FatureCurrent["ID_Client"]), Convert.ToInt32(FatureCurrent["ID_Site"]));

             #region MONTAN FACTURE
             if (!string.IsNullOrEmpty(modeExoneration))
             {

                 if (modeExoneration == "exo")
                 {
                     tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(items, FatureCurrent["tauxPorata"].ToString(),
                       FatureCurrent["TaxeTaux"].ToString(), true, Convert.ToInt32(FatureCurrent["IdLangueClient"]) == 1 ? "fr" : "en", listeDetail);

                 }
                 else
                 {

                     if (modeExoneration == "non")
                     {
                         tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(items, FatureCurrent["tauxPorata"].ToString(),
                             FatureCurrent["TaxeTaux"].ToString(), false, Convert.ToInt32(FatureCurrent["IdLangueClient"]) == 1 ? "fr" : "en", listeDetail);

                     }
                     else
                     {
                         if (modeExoneration == "part")
                         {
                             tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_ExonerePartiel(items, FatureCurrent["tauxPorata"].ToString(),
                               FatureCurrent["TaxeTaux"].ToString(), Convert.ToInt32(FatureCurrent["IdLangueClient"]) == 1 ? "fr" : "en", listeDetail);




                         }

                     }
                 }
                 
             }
             #endregion
             return tabretour;
         }



        static  object[] GetCovertFactureItem(FactureModel FatureCurrent)
         {
             object[] tabretour = new object[5];

             string modeExoneration = string.Empty;
             DetailProductModel detailService = new DetailProductModel();

             try
             {

                 if (FatureCurrent.CurrentClient.Exonerere == null)
                 {
                     ExonerationModel exoService = new ExonerationModel();
                     modeExoneration = exoService.EXONERATION_SELECTById(FatureCurrent.CurrentClient.IdExonere).CourtDesc;
                 }
                 else modeExoneration = FatureCurrent.CurrentClient.Exonerere.CourtDesc;

                 List<LigneFactureModel> items = factureservice.LIGNE_FACTURE_BYIDFActure(FatureCurrent.IdFacture);
                 TaxeModel taxeService = new TaxeModel();


                 TaxeModel TaxePorataSelected = FatureCurrent.CurrentClient.Porata ;
                 ClientModel ClientSelected = FatureCurrent.CurrentClient;
                 List<DetailProductModel> listeDetail = detailService.DETAIL_PRODUIT_BYCLIENT(FatureCurrent.CurrentClient.IdClient, FatureCurrent.IdSite);

                 #region MONTAN FACTURE



                 if (!string.IsNullOrEmpty(modeExoneration))
                 {

                     if (modeExoneration == "exo")
                     {
                         tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(items, TaxePorataSelected.Taux,
                             FatureCurrent.CurrentTaxe.Taux, true, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"), listeDetail);

                     }
                     else
                         if (modeExoneration == "non")
                         {
                             tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(items, TaxePorataSelected.Taux,
                                 FatureCurrent.CurrentTaxe.Taux, false, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"), listeDetail);

                         }
                         else
                             if (modeExoneration == "part")
                             {
                                 tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_ExonerePartiel(items, TaxePorataSelected.Taux,
                                   FatureCurrent.CurrentTaxe.Taux, FatureCurrent.CurrentClient.IdLangue == 1 ? "fr" : "en", listeDetail);

                                


                             }





                 #endregion


                 }
                 return tabretour;

             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message );
             }

         }

    }

 



  public static  class ClasseExtension
  {

      public static bool IsIsAlphanumeric(this string source)
      {
          Regex pattern = new Regex("[^0-9a-zA-Z]");
          return !pattern.IsMatch(source);
      }

      public static bool Isnumeric(this string source)
      {
          Regex pattern = new Regex("[^0-9]");
          return !pattern.IsMatch(source);
      }

     

     
  }
}
