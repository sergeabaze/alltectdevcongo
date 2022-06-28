using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using AllTech.FrameWork.Model;
using System.Diagnostics;
using FACTURATION_DAL.Model;
using System.Globalization;
using  System.Security.Cryptography;
using System.Management;


namespace AllTech.FrameWork.Global
{

   public  class Utils
    {
       static  string  fileName = GlobalDatas.defaultParameterFile;
       static  string defaulDirectory = GlobalDatas.defaultDirectory;
     

       #region REGION GESTION TAXES FACTURES
       
    

       public static decimal  GetTaxeBy_langue(string langue,string inTaxe)
       {
           decimal  values = 0;
           decimal valuestry = 0;
           CultureInfo currentCulture = CultureInfo.CurrentCulture;

            string valuesWithout = inTaxe.Replace('%', ' ');
            if (valuesWithout.Contains(".") || valuesWithout.Contains(","))
            {
                values = (decimal.Parse(valuesWithout.Replace(",",
                                                  currentCulture.NumberFormat.NumberDecimalSeparator).Trim().Replace(".",
                                                  currentCulture.NumberFormat.NumberDecimalSeparator).Trim(),
                                                   currentCulture) / 100);
            }
            else
            {
                if (decimal.TryParse(valuesWithout, out valuestry))
                {
                    values = valuestry  / 100;
                }
                else
                    throw new Exception(string.Format("Impossible de formatter la valeur [{0}]  dans cette culture {1}",inTaxe, currentCulture.Name));
            }



           return values;

       }

       public static object[] GetTotal_TTC_Exonere(List<LigneFactureModel> listeLigne, string tauxProrata, string tauxTva, bool isExonere, string langue, List<DetailProductModel> details)
       {
           //exonere  donc pai prorata

           Int32 inIdDetail = 0;
           decimal inQte = 0;
           decimal    inPrixU = 0;
           decimal  totalTva = 0;
           string comentTva = "";
           string valtesteProrata = (tauxProrata != null && tauxProrata !="") ? tauxProrata.Replace('%', ' ') : "0%";
           decimal  total_ligne_ht = 0;
           object[] tabCalcul = new object[7];
           double prorata = 0;
           decimal  totalTTC = 0;

           ProduitModel prodService = new ProduitModel(); ;
           DetailProductModel detailservice = new DetailProductModel();
           if (listeLigne != null)
           {

               foreach (LigneFactureModel ligne in listeLigne)
               {
                   DetailProductModel detailProduit = details.Find(p => p.IdDetail == ligne.IdDetailProduit);

                   inIdDetail = ligne.IdDetailProduit;
                   if (ligne.Quantite.ToString().Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                       inQte = ligne.Quantite;
                   else
                       inQte = ligne.Quantite;

                   //inPrixU = ligne.PrixUnitaire ==1 ?ligne .PrixUnitaire  :ligne.PrixUnitaire;
                   if (ligne.PrixUnitaire == 1)
                       inPrixU = ligne.PrixUnitaire;
                   else if (ligne.Quantite.ToString().Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                       inPrixU = ligne.PrixUnitaire;
                   else inPrixU = ligne.PrixUnitaire;

                  // if (detailProduit.Specialfact )
                      // total_ligne_ht += inPrixU ;
                  // else 
                    total_ligne_ht+=(inPrixU * inQte);
               }

               if (isExonere)
               {
                   if (!valtesteProrata.Contains(".") || !valtesteProrata.Contains(","))
                   {
                       if (double.Parse(valtesteProrata.Replace ("%","")) == 0)
                       {
                           prorata = 0;
                           totalTTC = total_ligne_ht;
                           tauxProrata = "";
                       }
                       else
                       {
                           prorata = (double)GetTaxeBy_langue(langue, tauxProrata) * (double)total_ligne_ht;
                           totalTTC = total_ligne_ht + (decimal)Math.Round(prorata, 0);
                       }

                   }
                   else
                   {
                       prorata = (double)GetTaxeBy_langue(langue, tauxProrata) * (double)total_ligne_ht;
                       totalTTC = total_ligne_ht + (decimal)Math.Round(prorata, 0);
                   }

                   tabCalcul[0] = "(Exonéré)" + tauxTva;//tva
                   tabCalcul[1] = 0;//comment tva
                   tabCalcul[2] = tauxProrata;
                   tabCalcul[3] = prorata;
                  
               }
               else
               {

                   tabCalcul[0] = tauxTva;//tva

                   double calcul = (double)(GetTaxeBy_langue(langue, tauxTva) * total_ligne_ht);
                   calcul = Math.Round(calcul, 0);
                   tabCalcul[1] = calcul; ;//comment tva

                   tabCalcul[2] = "";
                   tabCalcul[3] =0;

                   totalTTC = total_ligne_ht + (decimal)calcul;
               }

              
               tabCalcul[4] = total_ligne_ht;
               tabCalcul[5] = totalTTC;
           }
           return tabCalcul;

       }

       public static object[] GetTotal_TTC_Exonere(List<LigneFactureModel> listeLigne, string tauxProrata, string tauxTva, bool isExonere, string langue)
       {
           //exonere  donc pai prorata

           Int32 inIdDetail = 0;
           decimal inQte = 0;
           decimal inPrixU = 0;
           decimal totalTva = 0;
           string comentTva = "";
           string valtesteProrata = tauxProrata != null ? tauxProrata.Replace('%', ' ') : "0%";
           decimal total_ligne_ht = 0;
           object[] tabCalcul = new object[7];
           double prorata = 0;
           decimal totalTTC = 0;

           ProduitModel prodService = new ProduitModel(); ;
           DetailProductModel detailservice = new DetailProductModel();
           if (listeLigne != null)
           {

               foreach (LigneFactureModel ligne in listeLigne)
               {
                  

                   inIdDetail = ligne.IdDetailProduit;
                   if (ligne.Quantite.ToString().Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                       inQte = ligne.Quantite;
                   else
                       inQte = ligne.Quantite;

                   //inPrixU = ligne.PrixUnitaire ==1 ?ligne .PrixUnitaire  :ligne.PrixUnitaire;
                   if (ligne.PrixUnitaire == 1)
                       inPrixU = ligne.PrixUnitaire;
                   else if (ligne.Quantite.ToString().Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                       inPrixU = ligne.PrixUnitaire;
                   else inPrixU = ligne.PrixUnitaire;

                   
                       total_ligne_ht += (inPrixU * inQte);
               }

               if (isExonere)
               {
                   if (!valtesteProrata.Contains(".") || !valtesteProrata.Contains(","))
                   {
                       if (double.Parse(valtesteProrata) == 0)
                       {
                           prorata = 0;
                           totalTTC = total_ligne_ht;
                           tauxProrata = "";
                       }
                       else
                       {
                           prorata = (double)GetTaxeBy_langue(langue, tauxProrata) * (double)total_ligne_ht;
                           totalTTC = total_ligne_ht + (decimal)Math.Round(prorata, 0);
                       }

                   }
                   else
                   {
                       prorata = (double)GetTaxeBy_langue(langue, tauxProrata) * (double)total_ligne_ht;
                       totalTTC = total_ligne_ht + (decimal)Math.Round(prorata, 0);
                   }

                   tabCalcul[0] = "(Exonéré)" + tauxTva;//tva
                   tabCalcul[1] = 0;//comment tva
                   tabCalcul[2] = tauxProrata;
                   tabCalcul[3] = prorata;

               }
               else
               {

                   tabCalcul[0] = tauxTva;//tva

                   double calcul = (double)(GetTaxeBy_langue(langue, tauxTva) * total_ligne_ht);
                   calcul = Math.Round(calcul, 0);
                   tabCalcul[1] = calcul; ;//comment tva

                   tabCalcul[2] = "";
                   tabCalcul[3] = 0;

                   totalTTC = total_ligne_ht + (decimal)calcul;
               }


               tabCalcul[4] = total_ligne_ht;
               tabCalcul[5] = totalTTC;
           }
           return tabCalcul;

       }




       public static decimal[] GetTaxe_From(LigneFactureModel ligne, string tauxProrata, string tauxTva, string langue)
       {
           Int32 inIdDetail = 0;
           decimal inQte = 0;
           decimal  inPrixU = 0;
           decimal  totalTva = 0;
           string comentTva = "";
           decimal[] tabCalcul = new decimal[2];
           decimal total_ligne_ht_tva = 0;
           decimal total_ligne_ht_prorata = 0;

           ProduitModel prodService = new ProduitModel(); ;
           DetailProductModel detailservice = new DetailProductModel();
           DetailProductModel detailProduit = null;
           try
           {

               inIdDetail = ligne.IdDetailProduit;

               if (ligne.Quantite.ToString().Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                   inQte = ligne.Quantite;
               else
               inQte = ligne.Quantite;

               if (ligne.CurrentDetailproduit==null )
                   detailservice.DETAIL_PRODUIT_GETBYID(inIdDetail);
               else 
               detailProduit = ligne.CurrentDetailproduit;

               if (detailProduit != null)
               {
                   if (detailProduit.Prixunitaire == 1)
                       inPrixU = ligne.PrixUnitaire;
                   else if (ligne.Quantite.ToString().Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                       inPrixU = ligne.PrixUnitaire;
                   else
                       inPrixU =detailProduit.Prixunitaire;

                   if (detailProduit.Exonerer)
                   {
                       //pas de taxe , mais prorara
                       if (detailProduit.Isprorata)
                       {
                           total_ligne_ht_prorata = (inPrixU * inQte);
                           total_ligne_ht_prorata = GetTaxeBy_langue(langue, tauxProrata) * total_ligne_ht_prorata;
                           tabCalcul[1] =Math.Round( total_ligne_ht_prorata,0);
                           tabCalcul[0] = 0;//current tva
                       }
                       else
                       {
                           tabCalcul[0] = 0;//current tva
                           tabCalcul[1] = 0;//current prorata;
                       }
                   }
                   else
                   {
                       total_ligne_ht_prorata = (inPrixU * inQte);
                       total_ligne_ht_prorata = GetTaxeBy_langue(langue, tauxTva) * total_ligne_ht_prorata;
                       tabCalcul[0] =Math.Round( total_ligne_ht_prorata,0);
                       tabCalcul[1] = 0;//current prorata
                   }
               }

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message );
           }
           return tabCalcul;

       }
       #region TTC EXONERATIONPARTIEL



       public static object[] GetTotal_TTC_ExonerePartiel(List<LigneFactureModel> listeLigne, string tauxProrata, string tauxTva, string langue, List<DetailProductModel> details)
       {

           Int32 inIdDetail = 0;
           decimal inQte = 0;
           decimal inPrixU = 0;
           decimal totalTva = 0;
           string comentTva = "";
           decimal total_ligne_ht = 0;
           decimal total_ligne_ht_tva = 0;
           decimal total_ligne_ht_prorata = 0;
           object[] tabCalcul = new object[9];
           double prorata = 0;
           double calculTva = 0;
           decimal totalTTC = 0;

           ProduitModel prodService = new ProduitModel(); ;
           DetailProductModel detailservice = new DetailProductModel();
           DetailProductModel detailProduit = null;
           try
           {
               if (listeLigne != null)
               {
                   tabCalcul[0] = string .Empty ;
                   tabCalcul[1] = 0;
                   tabCalcul[2] = "";
                   tabCalcul[3] = 0;
                   foreach (LigneFactureModel ligne in listeLigne)
                   {
                       inIdDetail = ligne.IdDetailProduit;
                       DetailProductModel ndetailProduit = details.Find(p => p.IdDetail == inIdDetail);

                       if (ligne.Quantite.ToString().Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                           inQte = ligne.Quantite;
                       else
                       inQte =ligne.Quantite;

                       if (ligne.CurrentDetailproduit == null)
                           detailProduit = detailservice.DETAIL_PRODUIT_GETBYID(inIdDetail);
                       else detailProduit = ligne.CurrentDetailproduit;

                       if (detailProduit.Quantite == 1 && detailProduit.Prixunitaire == 1)
                           inPrixU = ligne.PrixUnitaire;
                       else if (ligne.Quantite.ToString().Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                           inPrixU = ligne.PrixUnitaire;
                       else
                           inPrixU = detailProduit.Prixunitaire;

                      // if (ndetailProduit.Specialfact)
                          // total_ligne_ht += inPrixU;
                      // else
                           total_ligne_ht += (inPrixU * inQte);
                     

                       if (detailProduit != null && detailProduit.Exonerer)
                       {
                           //pas de taxe , mais prorara
                           total_ligne_ht_tva += 0;

                           if (detailProduit != null && detailProduit.Isprorata)
                           {
                               //if (ndetailProduit.Specialfact)
                                  // total_ligne_ht_prorata +=inPrixU ;
                               //else 
                               total_ligne_ht_prorata += (inPrixU * inQte);

                               tabCalcul[2] = tauxProrata;
                               tabCalcul[6] = 0;//current tva
                               tabCalcul[7] = (inPrixU * inQte);//current prorata;
                           }

                           else
                           {
                               total_ligne_ht_prorata += 0;
                               if (!tabCalcul[0].ToString().Contains("exonere") && tabCalcul[0]==string .Empty )
                               tabCalcul[0] = "(exonere)" + tauxTva;
                               tabCalcul[6] = 0;//current tva
                               tabCalcul[7] = 0;//current prorata;
                           }
                       }
                       else if (detailProduit != null && !detailProduit.Exonerer)
                       {
                           //taxe, pas de prorata

                           //calculTva+= (double.Parse(tauxTva.Replace('%', ' ').Trim(), CultureInfo.InvariantCulture) / 100) * (inPrixU * (float)inQte);
                          // if (ndetailProduit.Specialfact)
                             //  total_ligne_ht_tva +=inPrixU ;
                           //else 
                             total_ligne_ht_tva += (inPrixU * inQte);

                           tabCalcul[0] = tauxTva;
                           tabCalcul[6] = (inPrixU * inQte);//current tva
                           tabCalcul[7] = 0;//current prorata;
                       }
                   }

                   prorata =(double) (GetTaxeBy_langue(langue, tauxProrata) * total_ligne_ht_prorata);
                   prorata = Math.Round(prorata, 0);
                   calculTva =(double) (GetTaxeBy_langue(langue, tauxTva) * total_ligne_ht_tva);
                   calculTva = Math.Round(calculTva, 0);

                   totalTTC = total_ligne_ht + (decimal )(prorata + calculTva);


                   tabCalcul[1] = calculTva;
                   tabCalcul[3] = prorata;
                   tabCalcul[4] = total_ligne_ht;
                   tabCalcul[5] = totalTTC;
               }
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message );
           }
           return tabCalcul;

       }

       public static object[] GetTotal_TTC_ExonerePartiel(List<LigneFactureModel> listeLigne, string tauxProrata, string tauxTva, string langue)
       {

           Int32 inIdDetail = 0;
           decimal inQte = 0;
           decimal inPrixU = 0;
           decimal totalTva = 0;
           string comentTva = "";
           decimal total_ligne_ht = 0;
           decimal total_ligne_ht_tva = 0;
           decimal total_ligne_ht_prorata = 0;
           object[] tabCalcul = new object[9];
           double prorata = 0;
           double calculTva = 0;
           decimal totalTTC = 0;

           ProduitModel prodService = new ProduitModel(); ;
           DetailProductModel detailservice = new DetailProductModel();
           DetailProductModel detailProduit = null;
           try
           {
               if (listeLigne != null)
               {
                   tabCalcul[0] = string.Empty;
                   tabCalcul[1] = 0;
                   tabCalcul[2] = "";
                   tabCalcul[3] = 0;
                   foreach (LigneFactureModel ligne in listeLigne)
                   {
                       inIdDetail = ligne.IdDetailProduit;
                    

                       if (ligne.Quantite.ToString().Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                           inQte = ligne.Quantite;
                       else
                           inQte = ligne.Quantite;

                       if (ligne.CurrentDetailproduit == null)
                           detailProduit = detailservice.DETAIL_PRODUIT_GETBYID(inIdDetail);
                       else detailProduit = ligne.CurrentDetailproduit;

                       if (detailProduit.Quantite == 1 && detailProduit.Prixunitaire == 1)
                           inPrixU = ligne.PrixUnitaire;
                       else if (ligne.Quantite.ToString().Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                           inPrixU = ligne.PrixUnitaire;
                       else
                           inPrixU = detailProduit.Prixunitaire;

                     
                           total_ligne_ht += (inPrixU * inQte);


                       if (detailProduit != null && detailProduit.Exonerer)
                       {
                           //pas de taxe , mais prorara
                           total_ligne_ht_tva += 0;

                           if (detailProduit != null && detailProduit.Isprorata)
                           {
                            
                                   total_ligne_ht_prorata += (inPrixU * inQte);

                               tabCalcul[2] = tauxProrata;
                               tabCalcul[6] = 0;//current tva
                               tabCalcul[7] = (inPrixU * inQte);//current prorata;
                           }

                           else
                           {
                               total_ligne_ht_prorata += 0;
                               if (!tabCalcul[0].ToString().Contains("exonere") && tabCalcul[0] == string.Empty)
                                   tabCalcul[0] = "(exonere)" + tauxTva;
                               tabCalcul[6] = 0;//current tva
                               tabCalcul[7] = 0;//current prorata;
                           }
                       }
                       else if (detailProduit != null && !detailProduit.Exonerer)
                       {
                           //taxe, pas de prorata

                           //calculTva+= (double.Parse(tauxTva.Replace('%', ' ').Trim(), CultureInfo.InvariantCulture) / 100) * (inPrixU * (float)inQte);
                        
                               total_ligne_ht_tva += (inPrixU * inQte);

                           tabCalcul[0] = tauxTva;
                           tabCalcul[6] = (inPrixU * inQte);//current tva
                           tabCalcul[7] = 0;//current prorata;
                       }
                   }

                   prorata = (double)(GetTaxeBy_langue(langue, tauxProrata) * total_ligne_ht_prorata);
                   prorata = Math.Round(prorata, 0);
                   calculTva = (double)(GetTaxeBy_langue(langue, tauxTva) * total_ligne_ht_tva);
                   calculTva = Math.Round(calculTva, 0);

                   totalTTC = total_ligne_ht + (decimal)(prorata + calculTva);


                   tabCalcul[1] = calculTva;
                   tabCalcul[3] = prorata;
                   tabCalcul[4] = total_ligne_ht;
                   tabCalcul[5] = totalTTC;
               }
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return tabCalcul;

       }
       #endregion
       #endregion

       #region REGION VERIFICATION INFO CLIENT



       public static bool IsClientComplet(Client client)
       {
           bool isExist = false;

           if (client != null)
           {

               if (client != null)
               {
                           if (client.IdLangue > 0)
                               if (!string.IsNullOrEmpty(client.NomClient))
                                   if (!string.IsNullOrEmpty(client.NumeroImatriculation ))
                                       if (!string.IsNullOrEmpty(client.NumeroContribuable))
                                           if (!string.IsNullOrEmpty(client.Rue1))
                                               if ((client.IdCompte>0))
                                                   if (!string.IsNullOrEmpty(client.Ville))
                                                      
                                                           isExist = true;
                                                       else
                                                           isExist = false;


               }


           }
           return isExist;

       }

       public static bool IsClientComplet(ClientModel  client,int i)
       {
           bool isExist = false;

           if (client != null)
           {

               if (client != null)
               {
                   if (client.IdLangue > 0)
                       if (!string.IsNullOrEmpty(client.NomClient))
                           if (!string.IsNullOrEmpty(client.NumemroImat ))
                               if (!string.IsNullOrEmpty(client.NumeroContribuable))
                                   if (!string.IsNullOrEmpty(client.Rue1))
                                       if ((client.IdCompte > 0))
                                           if (!string.IsNullOrEmpty(client.Ville))

                                               isExist = true;
                                           else
                                               isExist = false;


               }


           }
           return isExist;

       }

       #endregion

       #region REGION GESTION FICHIERS



       public static string getUrlDossierimages()
       {
           try
           {
               
               return System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);

           }
           catch (Exception ex)
           {
               throw new Exception("Erreure Lecture Chelin dossier Image" + ex.ToString());
           }
       }

       public static void EcrireLog(string messageErreure, bool typeErreur)
       {
           string dossier = string.Empty;

           if (typeErreur)
           {
               //dossier = GetPathRootSave("Log\\Erreure");
               dossier = dossier + "Data.log";
           }
           else
           {
               // dossier = GetPathRootSave("Log\\Performance");
               dossier = dossier + "performance.log";
           }


           Stream myFile = File.Create(dossier);
           TextWriterTraceListener myTextListener = new TextWriterTraceListener(myFile);
           Trace.Listeners.Add(myTextListener);
           Trace.WriteLine(messageErreure);
           Trace.Flush();

       }

       public static bool CreateDirectory(string cheminDossier)
       {
           bool result = false;
           if (!Directory.Exists(cheminDossier))
           {
               try
               {
                   Directory.CreateDirectory(cheminDossier);
                   result = true;
               }
               catch (IOException en)
               {
                 throw new Exception(" File Failed" + en.ToString());
               }
               catch (Exception ex)
               {
                   throw new Exception("Create File Failed" + ex.ToString());
               }

           }
           else result = true;

           return result;
       }

       public static void CreateFile(string Chemin, string fichier)
       {
           if (!File.Exists(Chemin + @"\" + fichier))
           {
               try
               {
                   StreamWriter fs = File.CreateText(Chemin + @"\" + fichier);
                  
                   fs.Flush();
                   fs.Close();
               }
               catch (IOException en)
               {
                   FormatException ex = new FormatException("Failed", en);
                   throw new Exception(" File Failed" + en.ToString());
               }
               catch (Exception ex)
               {
                  
                   throw new Exception("" + ex.ToString());
               }
           }
       }

       public static void CreateFile(string Chemin, string fichier, string message)
       {
           bool verif = false;

           if (!File.Exists(Chemin + @"\" + fichier))
           {
               try
               {
                   StreamWriter fs = File.CreateText(Chemin + @"\" + fichier);
                   verif = true;
                   fs.Flush();
                   fs.Close();
               }
               catch (IOException en)
               {
                   FormatException ex = new FormatException("Failed", en);
                   throw new Exception(" File Failed" + en.ToString());
               }
               catch (Exception ex)
               {
                   verif = false;
                   throw new Exception("" + ex.ToString());
               }
           }
           else verif = true;

           if (verif)
           {
               FileStream fs = new FileStream(Chemin + @"\" + fichier, FileMode.Append, FileAccess.Write);
               StreamWriter swr = new StreamWriter(fs);
               swr.WriteLine(message);

               swr.Flush();
               fs.Close();
           }


       }

       public static void logConnection(string Message, string typeApplication)
       {
           
           //string paths = string.Empty;
           //if (GlobalDatas.dataBasparameter.CheminFichierPath.Equals("local"))
           //    paths = AppDomain.CurrentDomain.BaseDirectory;
           //else paths = GlobalDatas.dataBasparameter.CheminFichierPath;

           //string newPath = string.Empty;
           //if (GlobalDatas.dataBasparameter.CheminFichierPath.Equals("local"))
           //{
           //    int chemin = paths.IndexOf("bin");
           //    if (chemin == -1)
           //    {
           //        newPath = paths;
           //        newPath = newPath + "\\LOGS";
           //    }
           //    else
           //    {
           //        newPath = paths.Substring(0, chemin);
           //        newPath = newPath + "LOGS";
           //    }
           //}
           //else {

              
           //    newPath = paths + "LOGS";
           //} 

           //try
           //{
           //    if (CreateDirectory(newPath))
           //    {
           //        if (!File.Exists(newPath + "\\" + "Connection.data"))
           //        {
           //            StreamWriter sw = File.CreateText(newPath + "\\" + "Connection.data");
           //            sw.Write("\r\n{0}", typeApplication);
           //            sw.WriteLine();
           //            sw.WriteLine(" {0} {1}: ", DateTime.Now.ToShortTimeString(), DateTime.Now.ToLongDateString());
           //            sw.WriteLine("");
           //            sw.WriteLine("------------------------------------------------");

           //            sw.Close();
           //            sw.Dispose();
           //        }
           //        else
           //        {
           //            StreamWriter sw = File.AppendText(newPath + "\\" + "Connection.data");
           //            sw.Write("  {0}: ", DateTime.Now);
           //            sw.WriteLine("{0}", Message);
           //            sw.WriteLine("");
           //            sw.Close();
           //            sw.Dispose();
           //        }
           //    }
           //}
           //catch (IOException ex)
           //{
           //    throw new IOException(ex.Message );
           //}

           bool islocalPath = false;
           string paths = string.Empty;
           if (GlobalDatas.dataBasparameter.PathLog.Equals("local"))
               paths = AppDomain.CurrentDomain.BaseDirectory;
           else paths = GlobalDatas.dataBasparameter.PathLog;

           string newPath = string.Empty;
           if (GlobalDatas.dataBasparameter.PathLog.Equals("local"))
           {
               islocalPath = true;
               int chemin = paths.IndexOf("bin");
               if (chemin == -1)
               {
                   newPath = paths;
               }
               else
               {
                   newPath = paths.Substring(0, chemin);
               }
               newPath = newPath + "LOGS";
           }
           else
           {
               paths = AppDomain.CurrentDomain.BaseDirectory;
               newPath = paths + "\\LOGS";
           }

           if (islocalPath)
           {
               // local path

           }
           else
           {

               if (!Directory.Exists(newPath))
               {
                   try
                   {
                       Directory.CreateDirectory(newPath);
                   }
                   catch (IOException ex)
                   {
                       paths = AppDomain.CurrentDomain.BaseDirectory;
                       int chemin = paths.IndexOf("bin");
                       if (chemin == -1)
                       {
                           newPath = paths;
                       }
                       else
                       {
                           newPath = paths.Substring(0, chemin);
                       }
                       newPath = newPath + "LOGS";
                   }
               }
               else
               {
                   //
               }

           }




           try
           {
               if (!Directory.Exists(newPath))
               {
                   Directory.CreateDirectory(newPath);
               }


               if (!File.Exists(newPath + "\\" + "Connection.data"))
               {
                   StreamWriter sw = File.CreateText(newPath + "\\" + "Connection.data");
                   sw.Write("\r\n{0}", typeApplication);
                   sw.WriteLine();
                   sw.WriteLine(" {0} {1}: ", DateTime.Now.ToShortTimeString(), DateTime.Now.ToLongDateString());
                   sw.WriteLine("");
                   sw.WriteLine("------------------------------------------------");

                   sw.Close();
                   sw.Dispose();
               }
               else
               {
                   StreamWriter sw = File.AppendText(newPath + "\\" + "Connection.data");
                   sw.Write("  {0}: ", DateTime.Now);
                   sw.WriteLine("{0}", Message);
                   sw.WriteLine("");
                   sw.Close();
                   sw.Dispose();
               }

           }
           catch (IOException ex)
           {
               throw new IOException(ex.Message);
           }

       }

       public static void logUserActions(string Message, string initMessage)
       {
           // string paths=string .Empty ;
           // if (GlobalDatas.dataBasparameter.CheminFichierPath.Equals("local"))
           //     paths = AppDomain.CurrentDomain.BaseDirectory;
           // else paths=GlobalDatas.dataBasparameter.CheminFichierPath;

           //string newPath = string.Empty;
           //if (GlobalDatas.dataBasparameter.CheminFichierPath.Equals("local"))
           //{
           //    int chemin = paths.IndexOf("bin");
           //    if (chemin == -1)
           //    {
           //        newPath = paths;
           //        newPath = newPath + "\\LOGS";
           //    }
           //    else
           //    {
           //        newPath = paths.Substring(0, chemin);
           //        newPath = newPath + "LOGS";
           //    }
           //}
           //else
           //{
              
           //    newPath = paths + "LOGS";
           //}
           
           //try
           //{
           //    string pathFichier=string.Format("logUsers{0}.data", DateTime.Now.ToString("MMyyyy"));


           //    if (CreateDirectory(newPath ))
           //    {
           //        if (!File.Exists(newPath + "\\" + pathFichier))
           //        {
           //            StreamWriter sw = File.CreateText(newPath + "\\" + pathFichier);
           //            sw.Write("  {0}: ", DateTime.Now);
           //            sw.WriteLine("{0}", initMessage);
           //            sw.WriteLine("");

           //            sw.Close();
           //            sw.Dispose();
           //        }
           //        else
           //        {
                       
           //            StreamWriter sw = File.AppendText(newPath + "\\" + pathFichier);
           //            sw.Write("  {0}: ", DateTime.Now);
           //            sw.WriteLine("{0}", Message);
           //            sw.WriteLine("");
           //            sw.Close();
           //            sw.Dispose();
           //        }
           //    }
           //}
           //catch (IOException ex)
           //{
           //    throw new IOException(ex.Message);
           //}

           string paths = string.Empty;
           bool islocalPath = false;
           if (GlobalDatas.dataBasparameter.PathLog.Equals("local"))
               paths = AppDomain.CurrentDomain.BaseDirectory;
           else paths = GlobalDatas.dataBasparameter.PathLog;

           string newPath = string.Empty;
           if (GlobalDatas.dataBasparameter.PathLog.Equals("local"))
           {
               islocalPath = true;
               int chemin = paths.IndexOf("bin");
               if (chemin == -1)
               {
                   newPath = paths;
               }
               else
               {
                   newPath = paths.Substring(0, chemin);

               }
               newPath = newPath + "LOGS";
           }
           else
           {
               //paths = AppDomain.CurrentDomain.BaseDirectory;

               newPath = paths + "LOGS";
           }

           if (islocalPath)
           {
               // local path

           }
           else
           {

               if (!Directory.Exists(newPath))
               {
                   try
                   {
                       Directory.CreateDirectory(newPath);
                   }
                   catch (IOException ex)
                   {
                       paths = AppDomain.CurrentDomain.BaseDirectory;
                       int chemin = paths.IndexOf("bin");
                       if (chemin == -1)
                       {
                           newPath = paths;
                       }
                       else
                       {
                           newPath = paths.Substring(0, chemin);
                       }
                       newPath = newPath + "LOGS";
                   }
               }
               else
               {
                   //
               }
           }


           try
           {
               string pathNewFile = string.Format("logUsers{0}.data", DateTime.Now.ToString("MMyyyy"));
               if (CreateDirectory(newPath))
               {
                   if (!File.Exists(newPath + "\\" + pathNewFile))
                   {
                       StreamWriter sw = File.CreateText(newPath + "\\" + pathNewFile);
                       sw.Write("  {0}: ", DateTime.Now);
                       sw.WriteLine("{0}", initMessage);
                       sw.WriteLine("");

                       sw.Close();
                       sw.Dispose();
                   }
                   else
                   {
                       StreamWriter sw = File.AppendText(newPath + "\\" + pathNewFile);
                       sw.Write("  {0}: ", DateTime.Now);
                       sw.WriteLine("{0}", Message);
                       sw.WriteLine("");
                       sw.Close();
                       sw.Dispose();
                   }
               }
           }
           catch (IOException ex)
           {
               throw new IOException(ex.Message);
           }

       }


       public static string GetErrorImportFile()
       {
           string pathretun =null;

           string paths = string.Empty;
           bool islocalPath = false;
           if (GlobalDatas.dataBasparameter.PathLog.Equals("local"))
               paths = AppDomain.CurrentDomain.BaseDirectory;
           else paths = GlobalDatas.dataBasparameter.PathLog;

           string newPath = string.Empty;
           if (GlobalDatas.dataBasparameter.PathLog.Equals("local"))
           {
               islocalPath = true;
               int chemin = paths.IndexOf("bin");
               if (chemin == -1)
               {
                   newPath = paths;
               }
               else
               {
                   newPath = paths.Substring(0, chemin);

               }
               newPath = newPath + "LOGS";
           }
           else
           {
               //paths = AppDomain.CurrentDomain.BaseDirectory;

               newPath = paths + "LOGS";
           }

           if (!Directory.Exists(newPath))
           {
               try
               {
                   Directory.CreateDirectory(newPath);
               }
               catch (IOException ex)
               {
                   paths = AppDomain.CurrentDomain.BaseDirectory;
                   int chemin = paths.IndexOf("bin");
                   if (chemin == -1)
                   {
                       newPath = paths;
                   }
                   else
                   {
                       newPath = paths.Substring(0, chemin);
                   }
                   newPath = newPath + "LOGS";
               }
           }


           try
           {
               string pathNewFile = string.Format("ErrorImportDB{0}.txt", DateTime.Now.ToString("MMyyyy"));
               if (CreateDirectory(newPath))
               {
                   if (!File.Exists(newPath + "\\" + pathNewFile))
                   {
                       StreamWriter sw = File.CreateText(newPath + "\\" + pathNewFile);
                       pathretun = newPath + "\\" + pathNewFile;
                       sw.Close();
                       sw.Dispose();
                   }else
                       pathretun = newPath + "\\" + pathNewFile;

               }
               return pathretun;
           }
           catch (Exception ex)
           {
               throw new IOException(ex.Message);
           }

         

       }


       public static bool  NewParameterFile()
       {
         string newPath=string .Empty ;

           string paths=AppDomain.CurrentDomain.BaseDirectory ;

           int  chemin = paths.IndexOf("bin");
           if (chemin == -1)
           {
               newPath = paths;
           }
           else 
           { 
               newPath = paths.Substring(0, chemin);
           }

           newPath = newPath + defaulDirectory;
           try
           {
               if (!Directory.Exists(newPath))
               {
                   Directory.CreateDirectory(newPath);
                   if (!File.Exists(newPath + "\\" + fileName))
                   {
                       FileStream fs = File.Create(newPath + "\\" + fileName);
                       XDocument xdoc = new XDocument(
                         new XElement("parameters",
                              new XElement("parameter",
                                    new XAttribute("id", "connectionString"),
                                    new XElement("server", "localhost"),
                                    new XElement("nameBd", "sysfact"),
                                    new XElement("username", "root"),
                                    new XElement("password", "root")

                                  ),
                                   new XElement("parameter",
                                    new XAttribute("id", "connectionStringArchives"),
                                    new XElement("server", "localhost"),
                                    new XElement("nameBd", "sysfact_archives"),
                                    new XElement("username", "root"),
                                    new XElement("password", "root")

                                  ),
                                   new XElement("parameter",
                                    new XAttribute("id", "devises"),
                                    new XElement("dtva", "15001"),
                                    new XElement("ddevise", "160001"),
                                      new XElement("defaultlangue", "fr-FR")

                                  ),
                                   new XElement("parameter",
                                    new XAttribute("id", "bases"),
                                    new XElement("dev", "100"),
                                    new XElement("prod", "100"),
                                    new XElement("location", "100"),
                                    new XElement("teste", "100"),
                                    new XElement("nombrePagination", "10"),
                                    new XElement("dossierImages", "SodexoImages"),
                                    new XElement("mailfrom", "Sodexo@sodexo.com"),
                                    new XElement("mailTo", "SodexoDla@yahoo.com"),
                                    new XElement("smtp", "smtp.gmail.com"),
                                    new XElement("jvreperory", ""),
                                    new XElement("jvmodeSelect", "0")
                                  ),
                                  new XElement("parameter",
                                    new XAttribute("id", "user"),
                                    new XElement("dpassword", "@sodexo2012"),
                                    new XElement("duser", "sodexo")


                                  ),
                                    new XElement("parameter",
                                    new XAttribute("id", "config"),
                                    new XElement("dejaUtiliser", "1"),
                                    new XElement("mode", "prod")
                                    
                                  ),
                                  new XElement("parameter",
                                    new XAttribute("id", "log"),
                                    new XElement("import", ""),
                                     new XElement("backUpLog", ""),
                                       new XElement("pathlog", "local")
                                  )

                                  )); // switch 1
                       fs.Close();
                       xdoc.Save(newPath + "\\" + fileName);


                   }
               }
               else
               {
                   if (!File.Exists(newPath + "\\" + fileName))
                   {
                       FileStream fs = File.Create(newPath + "\\" + fileName);
                       XDocument xdoc = new XDocument(
                         new XElement("parameters",
                              new XElement("parameter",
                                    new XAttribute("id", "connectionString"),
                                    new XElement("server", "localhost"),
                                    new XElement("nameBd", "sysfact"),
                                    new XElement("username", "root"),
                                    new XElement("password", "root")

                                  ),
                                   new XElement("parameter",
                                    new XAttribute("id", "connectionStringArchives"),
                                    new XElement("server", "localhost"),
                                    new XElement("nameBd", "sysfact_archives"),
                                    new XElement("username", "root"),
                                    new XElement("password", "root")

                                  ),
                                   new XElement("parameter",
                                    new XAttribute("id", "devises"),
                                    new XElement("dtva", "15001"),
                                    new XElement("ddevise", "160001"),
                                      new XElement("defaultlangue", "fr-FR")

                                  ),
                                   new XElement("parameter",
                                    new XAttribute("id", "bases"),
                                    new XElement("dev", "100"),
                                    new XElement("prod", "100"),
                                    new XElement("location", "100"),
                                    new XElement("teste", "100"),
                                    new XElement("nombrePagination", "10"),
                                    new XElement("dossierImages", "SodexoImages"),
                                     new XElement("mailfrom", "Sodexo@sodexo.com"),
                                    new XElement("mailTo", "SodexoDla@yahoo.com"),
                                    new XElement("smtp", "smtp.gmail.com"),
                                   new XElement("jvreperory", ""),
                                    new XElement("jvmodeSelect", "0")
                                  ),
                                  new XElement("parameter",
                                    new XAttribute("id", "user"),
                                     new XElement("dpassword", "@sodexo2012"),
                                    new XElement("duser", "sodexo")


                                  ),
                                   new XElement("parameter",
                                    new XAttribute("id", "config"),
                                    new XElement("dejaUtiliser", "1"),
                                    new XElement("mode", "prod")
                                    
                                  ),
                                   new XElement("parameter",
                                    new XAttribute("id", "log"),
                                    new XElement("import", ""),
                                     new XElement("backUpLog", ""),
                                       new XElement("pathlog", "local")
                                  )

                                  )); // switch 1
                       fs.Close();
                       xdoc.Save(newPath + "\\" + fileName);


                   }
               }
               return true;
               
           }
           catch (IOException ex)
           {
               throw new IOException(ex.Message);
              
           }
         
       }

       public static string getfileName()
       {
           string newPath = string.Empty;
           string paths = AppDomain.CurrentDomain.BaseDirectory;
           int chemin = paths.IndexOf("bin");
           if (chemin == -1)
           {
               newPath = paths;
           }
           else
           {
               newPath = paths.Substring(0, chemin);
           }
         
           newPath = newPath + defaulDirectory + "\\" + fileName;
           return newPath;
       }

       public static XElement GetParameterFiles()
       {
           XElement parameter = null;
           string newPath = string.Empty;

           string paths = AppDomain.CurrentDomain.BaseDirectory;

           int chemin = paths.IndexOf("bin");
           if (chemin == -1)
           {
               newPath = paths;
           }
           else
           {
               newPath = paths.Substring(0, chemin);
           }
           newPath = newPath + defaulDirectory+ "\\" + fileName;
           try
           {
               if (File.Exists(newPath))
                    parameter = XElement.Load(newPath);

                   return parameter;
               
           }
           catch (IOException ex)
           {
               throw new IOException(ex.Message);
           }
       }

       private static void AddText(FileStream fs, string value)
       {
           byte[] info = new UTF8Encoding(true).GetBytes(value);
           fs.Write(info, 0, info.Length);
       }

       public static ParametresModel GetParametersApplication()
       {
           ParametresModel CurrentParametres = new ParametresModel();
          

           try
           {
               XElement parameter = XElement.Parse(GetParameterFiles().ToString());

               var queryDatabase = from p in parameter.Elements()
                                   where (string)p.Attribute("id").Value == "connectionString"
                                   select p;
               foreach (var de in queryDatabase)
               {
                   CurrentParametres.Nomserveur = de.Element("server").Value;
                   CurrentParametres.Port = "3306";
                   CurrentParametres.Utilisateur = de.Element("username").Value;
                   CurrentParametres.NomBd = de.Element("nameBd").Value;
                   CurrentParametres.Motpasse = de.Element("password").Value;


               }

               var queryDatabaseArchive = from p in parameter.Elements()
                                          where (string)p.Attribute("id").Value == "connectionStringArchives"
                                          select p;
               foreach (var de in queryDatabaseArchive)
               {
                   CurrentParametres.NomserveurArchive = de.Element("server").Value;
                   CurrentParametres.PortArchive = "3306";
                   CurrentParametres.UtilisateurArchive = de.Element("username").Value;
                   CurrentParametres.NomBdArchive = de.Element("nameBd").Value;
                   CurrentParametres.MotpasseArchive = de.Element("password").Value;


               }

               var queryDefaultPass = from p in parameter.Elements()
                                      where (string)p.Attribute("id").Value == "user"
                                      select p;
               foreach (var de in queryDefaultPass)
               {
                   CurrentParametres.DeaultPassword = de.Element("dpassword").Value;
                   CurrentParametres.DeaultUser = de.Element("duser").Value;
               }

               var queryDevises = from p in parameter.Elements()
                                  where (string)p.Attribute("id").Value == "devises"
                                  select p;
               foreach (var de in queryDevises)
               {
                   CurrentParametres.Idtva = int.Parse(de.Element("dtva").Value);
                   CurrentParametres.IdDevise = int.Parse(de.Element("ddevise").Value);
                   CurrentParametres.DefaulLanguage = de.Element("defaultlangue").Value;
                   GlobalDatas.defaultLanguage = CurrentParametres.DefaulLanguage;

               }


               var queryConfig = from p in parameter.Elements()
                                  where (string)p.Attribute("id").Value == "config"
                                  select p;
               foreach (var de in queryConfig)
               {
                   CurrentParametres.Dejautiliser  = de.Element("dejaUtiliser").Value;
                   CurrentParametres.CodeCourant = de.Element("mode").Value;
                  
               }

               var queryBase = from p in parameter.Elements()
                               where (string)p.Attribute("id").Value == "bases"
                                 select p;
               foreach (var de in queryBase)
               {
                   CurrentParametres.DureeDev  = de.Element("dev").Value;
                   CurrentParametres.DureeProd  = de.Element("prod").Value;
                   CurrentParametres.DureeLocaton = de.Element("location").Value;
                   CurrentParametres.DureeTeste = de.Element("teste").Value;
                   CurrentParametres.DossierImages = de.Element("dossierImages").Value;
                   CurrentParametres.PaginationHtrc = de.Element("nombrePagination").Value;
                   CurrentParametres.MailFrom = de.Element("mailfrom").Value;
                   CurrentParametres.MailTo = de.Element("mailTo").Value;
                   CurrentParametres.Smtp = de.Element("smtp").Value;
                   CurrentParametres.PathJournalVente = de.Element("jvreperory").Value;
                   CurrentParametres.JvModeSelect =int.Parse ( de.Element("jvmodeSelect").Value)==1?true:false;

               }

               var queryBasePath = from log in parameter.Elements()
                               where (string)log.Attribute("id").Value == "log"
                               select log;
               foreach (var de in queryBasePath)
               {
                   CurrentParametres.CheminFichierPath = de.Element("import").Value;
                   CurrentParametres.PathBackUpLog = de.Element("backUpLog").Value;
                   CurrentParametres.PathLog = de.Element("pathlog").Value;
               }
             

               return CurrentParametres;
           }
           catch (Exception ex)
           {
               throw new Exception("Erreur chargement des parametres du fichier" +"\n"+ ex.Message );
           }

         

       }

       public static void  GetParametersFromDatabase(int idSite)
       {
           ParametresModel CurrentParametresDatabase = new ParametresModel();
          // ParametresModel CurrentParametresData = new ParametresModel();

           try
           {
               SettingsModel setting = new SettingsModel();
               //var de = GlobalDatas.defaultParameterFile;
               List<SettingsModel> Listeconfigurations = setting.Configuration_List(idSite);
               if (Listeconfigurations != null && Listeconfigurations.Count > 0)
               {
                   if (GlobalDatas.dataBasparameter != null)
                   {

                       
                       SettingsModel defaultpassword = Listeconfigurations.Find(sr => sr.Code == "dpassword");
                       SettingsModel deviseId = Listeconfigurations.Find(sr => sr.Code == "ddevise");
                       SettingsModel tvaId = Listeconfigurations.Find(sr => sr.Code == "dtva");
                       SettingsModel dossierImage = Listeconfigurations.Find(sr => sr.Code == "dossierImages");
                       SettingsModel pagination = Listeconfigurations.Find(sr => sr.Code == "nombrePagination");
                       SettingsModel mailFrom = Listeconfigurations.Find(sr => sr.Code == "mailfrom");
                       SettingsModel mailTo = Listeconfigurations.Find(sr => sr.Code == "mailTo");
                       SettingsModel smtp = Listeconfigurations.Find(sr => sr.Code == "smtp");

                       SettingsModel pathImport = Listeconfigurations.Find(sr => sr.Code == "import");
                       SettingsModel pathBacklog = Listeconfigurations.Find(sr => sr.Code == "backUpLog");
                       SettingsModel pathlogFile = Listeconfigurations.Find(sr => sr.Code == "pathLogFile");

                       SettingsModel portSmtp = Listeconfigurations.Find(sr => sr.Code == "portsmtp");
                       SettingsModel logginSmtp = Listeconfigurations.Find(sr => sr.Code == "loggingsmtp");
                       SettingsModel passWSmtp = Listeconfigurations.Find(sr => sr.Code == "passwdsmtp");
                       SettingsModel codejournalV = Listeconfigurations.Find(sr => sr.Code == "codeJnlvente");
                       SettingsModel jourlimite = Listeconfigurations.Find(sr => sr.Code == "jourlimite");
                       SettingsModel expornameDb = Listeconfigurations.Find(sr => sr.Code == "dbNameExport");


                       if (defaultpassword != null)
                           GlobalDatas.dataBasparameter.DeaultPassword = defaultpassword.Libelle;

                       if (deviseId != null)
                           GlobalDatas.dataBasparameter.IdDevise = Int32.Parse(deviseId.Libelle);
                       if (tvaId != null)
                           GlobalDatas.dataBasparameter.Idtva = Int32.Parse(tvaId.Libelle);

                       if (dossierImage != null)
                           GlobalDatas.dataBasparameter.DossierImages = dossierImage.Libelle;
                       if (pagination != null)
                           GlobalDatas.dataBasparameter.PaginationHtrc = pagination.Libelle;
                       if (mailFrom != null)
                           GlobalDatas.dataBasparameter.MailFrom = mailFrom.Libelle;
                       if (mailTo != null)
                           GlobalDatas.dataBasparameter.MailTo = mailTo.Libelle;
                       if (smtp != null)
                           GlobalDatas.dataBasparameter.Smtp = smtp.Libelle;

                       if (pathImport != null)
                           GlobalDatas.dataBasparameter.CheminFichierPath = pathImport.Libelle;
                       if (pathBacklog != null)
                           GlobalDatas.dataBasparameter.PathBackUpLog  = pathBacklog.Libelle;

                       if (pathlogFile != null)
                           GlobalDatas.dataBasparameter.PathLog = pathlogFile.Libelle;
                       //
                       if (portSmtp != null)
                           GlobalDatas.dataBasparameter.PortSmtp= portSmtp.Libelle;

                       if (logginSmtp != null)
                           GlobalDatas.dataBasparameter.LogginSmtp=logginSmtp.Libelle;
                       if (passWSmtp != null)
                           GlobalDatas.dataBasparameter.PasswordSmtp= passWSmtp.Libelle;

                       if (codejournalV != null)
                           GlobalDatas.dataBasparameter.CodeJournalVente = codejournalV.Libelle;

                       if (jourlimite != null)
                           GlobalDatas.dataBasparameter.JourLimiteFacturation = Int32.Parse(jourlimite.Libelle);

                       if (expornameDb != null)
                           GlobalDatas.dataBasparameter.NomFichierExport = expornameDb.Libelle;
                          
                   }

                
               }
             
             
           }
           catch (Exception ex)
           {
               throw new Exception("Erreur chargement des parametres de la base de données" + "\n" + ex.Message);
           }
       }

       
       #endregion

       #region CRYPTOGRAPHIE

        #region cryptage

       private static string EncryptString(string clearText, string strKey, string strIv)
       {
          
           // Place le texte à chiffrer dans un tableau d'octets
           byte[] plainText = Encoding.UTF8.GetBytes(clearText);

           // Place la clé de chiffrement dans un tableau d'octets
           byte[] key = Encoding.UTF8.GetBytes(strKey);

           // Place le vecteur d'initialisation dans un tableau d'octets
           byte[] iv = Encoding.UTF8.GetBytes(strIv);

           RijndaelManaged rijndael = new RijndaelManaged();
           // Définit le mode utilisé
           rijndael.Mode = CipherMode.CBC;

           // Crée le chiffreur AES - Rijndael
           ICryptoTransform aesEncryptor = rijndael.CreateEncryptor(key, iv);

           MemoryStream ms = new MemoryStream();

           // Ecris les données chiffrées dans le MemoryStream
           CryptoStream cs = new CryptoStream(ms, aesEncryptor, CryptoStreamMode.Write);
           cs.Write(plainText, 0, plainText.Length);
           cs.FlushFinalBlock();


           // Place les données chiffrées dans un tableau d'octet
           byte[] CipherBytes = ms.ToArray();


           ms.Close();
           cs.Close();

           // Place les données chiffrées dans une chaine encodée en Base64
           return Convert.ToBase64String(CipherBytes);

          
       }

       public static void EncryptFile(string strKey, string strIv, string pathPlainTextFile, string pathCypheredTextFile)
       {
           //System.Security.Cryptography.Rijndael rijndael;

           // Place la clé de déchiffrement dans un tableau d'octets
           byte[] key = Encoding.UTF8.GetBytes(strKey);

           // Place le vecteur d'initialisation dans un tableau d'octets
           byte[] iv = Encoding.UTF8.GetBytes(strIv);

           RijndaelManaged rijndael = new RijndaelManaged();

           // Définit le mode utilisé
           rijndael.Mode = CipherMode.CBC;

           ICryptoTransform aesDecryptor = rijndael.CreateDecryptor();

          // CryptoStream cs = new CryptoStream(fsCrypt, aesDecryptor, CryptoStreamMode.Write);

           // FileStream of the file that is currently encrypted.    
           //FileStream fsIn = new FileStream(pathCypheredTextFile, FileMode.OpenOrCreate);

           //int data;

           //while ((data = fsIn.ReadByte()) != -1)
           //    cs.WriteByte((byte)data);
           //cs.Close();
           //fsIn.Close();
           //fsCrypt.Close();  




       }
       
        #endregion 

        #region Decryptage
       public static string DecryptString(string cipherText, string strKey, string strIv)
       {
           // Place le texte à déchiffrer dans un tableau d'octets
           byte[] cipheredData = Convert.FromBase64String(cipherText);

           // Place la clé de déchiffrement dans un tableau d'octets
           byte[] key = Encoding.UTF8.GetBytes(strKey);

           // Place le vecteur d'initialisation dans un tableau d'octets
           byte[] iv = Encoding.UTF8.GetBytes(strIv);

           RijndaelManaged rijndael = new RijndaelManaged();
           rijndael.Mode = CipherMode.CBC;


           // Ecris les données déchiffrées dans le MemoryStream
           ICryptoTransform decryptor = rijndael.CreateDecryptor(key, iv);
           MemoryStream ms = new MemoryStream(cipheredData);
           CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);

           // Place les données déchiffrées dans un tableau d'octet
           byte[] plainTextData = new byte[cipheredData.Length];

           int decryptedByteCount = cs.Read(plainTextData, 0, plainTextData.Length);

           ms.Close();
           cs.Close();

           return Encoding.UTF8.GetString(plainTextData, 0, decryptedByteCount);


       }

       public static void DecryptFile(string strKey, string strIv, string pathCypheredTextFile, string pathPlainTextFile)
       {
       }
        #endregion

      

       public static string GetHrdDiskReference()
       {
           string drive = "C";
           ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
           dsk.Get();
           string volumeSerial = dsk["VolumeSerialNumber"].ToString();
           return volumeSerial;
           
       }
        #endregion


       public static string GetProcessorReference()
       {
           object cpuInfo = null;
           ManagementClass mc = new ManagementClass("win32_processor");
           ManagementObjectCollection moc = mc.GetInstances();

           foreach (ManagementObject mo in moc)
           {
               //processorID
               cpuInfo = mo.Properties["processorID"].Value;
               break;
           }
           return cpuInfo == null ? string.Empty : cpuInfo.ToString();
       }
    }
}
