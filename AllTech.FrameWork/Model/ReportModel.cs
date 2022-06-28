using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FACTURATION_DAL;
using System.Globalization;
using AllTech.FrameWork.Utils;

namespace AllTech.FrameWork.Model
{
   public  class ReportModel
    {

      static  IFacturation  DAL = null;

       public static DataTable GetReportClient(int id)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DataTable table = DAL.ReportClient(id);
               return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message );
           }

       }

       public static DataTable GetReportClientArchive(int id, Int64 idfacture)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DataTable table = DAL.ReportClientArchive(id, idfacture);
               return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       public static DataTable GetReportSociete(int id)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DataTable table = DAL.ReportSociete(id);
               return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       public static DataTable GetReportSocieteArchive(int id)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DataTable table = DAL.ReportSocieteArchive(id);
               return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }


       public static DataTable GetReporPiedPage(int idSociete)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DataTable table = DAL.ReportPiedPAge(idSociete);
               return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       public static DataTable GetReporPiedPageArchive(int idSociete)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DataTable table = DAL.ReportPiedPAgeArchive(idSociete);
               return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }


       public static DataTable GetLibelle(int idLangue)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DataTable table = DAL.ReportLIBELLE (idLangue);
               return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }


       public static DataTable GetLibelleArchive(int idLangue)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DataTable table = DAL.ReportLIBELLEArchive(idLangue);
               return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       public static DataSet GetFactureNew(long id,int idSite, int mode)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           DataSet ds = DAL.ReportFACTURENEW(id, idSite);

           if (ds != null)
           {
               if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
               {
                   if (Convert.ToInt32(ds.Tables[0].Rows[0]["IDLAngue"]) == 1)
                   {
                       ds.Tables[0].Rows[0]["DateImpression"] = string.Format("{0} {1} ", ds.Tables[0].Rows[0]["DateImpression"], DateTime.Parse(ds.Tables[0].Rows[0]["Date_Creation"].ToString()).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR")));
                       ds.Tables[0].Columns["newdateEcheance"].DataType = typeof(string);
                       ds.Tables[0].Rows[0]["newdateEcheance"] = DateTime.Parse(ds.Tables[0].Rows[0]["DateEcheance"].ToString()).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));
                      

                       if (mode == 0)
                       {
                           #region Translate lettre


                           var montaconver = ds.Tables[0].Rows[0]["montantConvert"] != DBNull.Value ? Convert.ToDouble(ds.Tables[0].Rows[0]["montantConvert"]) : 0;
                           var montantFacture = ds.Tables[0].Rows[0]["montant_Facture"] != DBNull.Value ? Convert.ToDouble(ds.Tables[0].Rows[0]["montant_Facture"]) : 0;

                           string[] tabFacture = montantFacture.ToString().Replace('.', ',').Split(new char[] { ',' });
                           string translatconvrt1 = string.Empty;
                           string translatconvrt2 = string.Empty;

                           if (tabFacture.Length > 1)
                           {
                               if (!tabFacture[0].Equals("0"))
                                   translatconvrt1 = NombreEnLettre.changerNombrelettre(tabFacture[0]);
                               else translatconvrt1 = "Zero";
                               ds.Tables[0].Rows[0]["montant_lettre_fact"] = translatconvrt1;


                               if (!string.IsNullOrEmpty(tabFacture[1].TrimEnd(new char[] { '0' })))
                               {
                                   if (!tabFacture[1].Equals("0"))
                                       translatconvrt2 = NombreEnLettre.changerNombrelettre(tabFacture[1]);
                                   else translatconvrt2 = "";

                                   ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = translatconvrt2;
                               }
                               else ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = "";
                           }
                           else
                           {
                               if (!tabFacture[0].Equals("0"))
                                   translatconvrt1 = NombreEnLettre.changerNombrelettre(tabFacture[0]);
                               else translatconvrt1 = "";

                               ds.Tables[0].Rows[0]["montant_lettre_fact"] = translatconvrt1;
                               ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = "";
                           }

                           //montant de conversion
                           string[] tabConvert = montaconver.ToString().Replace('.', ',').Split(new char[] { ',' });
                           string translatconvrt11 = string.Empty;
                           string translatconvrt21 = string.Empty;
                           if (tabConvert.Length > 1)
                           {
                               if (!tabConvert[0].Equals("0"))
                                   translatconvrt11 = NombreEnLettre.changerNombrelettre(tabConvert[0]);
                               else translatconvrt11 = "Zero";
                               ds.Tables[0].Rows[0]["mnt_convert_lettre"] = translatconvrt11;


                               if (!string.IsNullOrEmpty(tabConvert[1].TrimEnd(new char[] { '0' })))
                               {
                                   if (!tabConvert[1].Equals("0"))
                                       translatconvrt21 = NombreEnLettre.changerNombrelettre(tabConvert[1]);
                                   else translatconvrt21 = "";

                                   ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = translatconvrt21;
                               }
                               else ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = "";
                           }
                           else
                           {
                               if (!tabFacture[0].Equals("0"))
                                   translatconvrt11 = NombreEnLettre.changerNombrelettre(tabConvert[0]);
                               else translatconvrt11 = "";

                               ds.Tables[0].Rows[0]["mnt_convert_lettre"] = translatconvrt11;
                               ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = "";
                           }
                           #endregion
                         
                          // ds.Tables[0].Rows[0]["montant_lettre"] = ds.Tables[0].Rows[0]["montantConvert"] != DBNull.Value ? NombreEnLettre.changerNombrelettre(Convert.ToString(ds.Tables[0].Rows[0]["montantConvert"])) : "0";
                         //  ds.Tables[0].Rows[0]["montantXaf_lettre"] = ds.Tables[0].Rows[0]["montantXaf"] != DBNull.Value ?(Convert.ToString(ds.Tables[0].Rows[0]["montantXaf"]).Equals("0")?"": NombreEnLettre.changerNombrelettre(Convert.ToString(ds.Tables[0].Rows[0]["montantXaf"])) ): "0";
                       }
                       else
                       {

                           #region Translate lettre


                           var montaconver = ds.Tables[0].Rows[0]["montantConvert"] != DBNull.Value ? Convert.ToDouble(ds.Tables[0].Rows[0]["montantConvert"]) : 0;
                           var montantFacture = ds.Tables[0].Rows[0]["montant_Facture"] != DBNull.Value ? Convert.ToDouble(ds.Tables[0].Rows[0]["montant_Facture"]) : 0;

                           string[] tabFacture = montantFacture.ToString().Replace('.', ',').Split(new char[] { ',' });
                           string translatconvrt1 = string.Empty;
                           string translatconvrt2 = string.Empty;

                           if (tabFacture.Length > 1)
                           {
                               if (!tabFacture[0].Equals("0"))
                                   translatconvrt1 = NombreEnLettre.changerNombrelettre(tabFacture[0]);
                               else translatconvrt1 = "Zero";
                               ds.Tables[0].Rows[0]["montant_lettre_fact"] ="Moins "+ translatconvrt1;


                               if (!string.IsNullOrEmpty(tabFacture[1].TrimEnd(new char[] { '0' })))
                               {
                                   if (!tabFacture[1].Equals("0"))
                                       translatconvrt2 = NombreEnLettre.changerNombrelettre(tabFacture[1]);
                                   else translatconvrt2 = "";

                                   ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] =translatconvrt2;
                               }
                               else ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = "";
                           }
                           else
                           {
                               if (!tabFacture[0].Equals("0"))
                                   translatconvrt1 = NombreEnLettre.changerNombrelettre(tabFacture[0]);
                               else translatconvrt1 = "";

                               ds.Tables[0].Rows[0]["montant_lettre_fact"] = "Moins " + translatconvrt1;
                               ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = "";
                           }

                           //montant de conversion
                           string[] tabConvert = montaconver.ToString().Replace('.', ',').Split(new char[] { ',' });
                           string translatconvrt11 = string.Empty;
                           string translatconvrt21 = string.Empty;
                           if (tabConvert.Length > 1)
                           {
                               if (!tabConvert[0].Equals("0"))
                                   translatconvrt11 = NombreEnLettre.changerNombrelettre(tabConvert[0]);
                               else translatconvrt11 = "Zero";
                               ds.Tables[0].Rows[0]["mnt_convert_lettre"] = "Moins " + translatconvrt11;


                               if (!string.IsNullOrEmpty(tabConvert[1].TrimEnd(new char[] { '0' })))
                               {
                                   if (!tabConvert[1].Equals("0"))
                                       translatconvrt21 = NombreEnLettre.changerNombrelettre(tabConvert[1]);
                                   else translatconvrt21 = "";

                                   ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = translatconvrt21;
                               }
                               else ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = "";
                           }
                           else
                           {
                               if (!tabFacture[0].Equals("0"))
                                   translatconvrt11 = NombreEnLettre.changerNombrelettre(tabConvert[0]);
                               else translatconvrt11 = "";

                               ds.Tables[0].Rows[0]["mnt_convert_lettre"] = "Moins " + translatconvrt11;
                               ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = "";
                           }
                           #endregion

                           //ds.Tables[0].Rows[0]["montant_lettre"] = ds.Tables[0].Rows[0]["montantConvert"] != DBNull.Value ? "Moins " + NombreEnLettre.changerNombrelettre(Convert.ToString(ds.Tables[0].Rows[0]["montantConvert"])) : "0";
                          // ds.Tables[0].Rows[0]["montantXaf_lettre"] = ds.Tables[0].Rows[0]["montantXaf"] != DBNull.Value ? (Convert.ToString(ds.Tables[0].Rows[0]["montantXaf"]).Equals("0")?"":  "Moins " + NombreEnLettre.changerNombrelettre(Convert.ToString(ds.Tables[0].Rows[0]["montantXaf"])) ): "0";

                       }


                   }
                   else
                   {
                       if (Convert.ToInt32(ds.Tables[0].Rows[0]["IDLAngue"]) == 2)
                       {
                           var debut = ds.Tables[0].Rows[0]["DateEcheance"];
                           int newDateDay = DateTime.Parse(ds.Tables[0].Rows[0]["Date_Creation"].ToString()).Day;

                           DateTime dateImp = DateTime.Parse(ds.Tables[0].Rows[0]["Date_Creation"].ToString());
                           string dateImpression = dateImp.ToString("MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                           string NdateImp = getDayWithSuffix(dateImp.Day);

                           ds.Tables[0].Rows[0]["DateImpression"] = string.Format("{0} {1} {2}", ds.Tables[0].Rows[0]["DateImpression"], NdateImp, dateImpression);

                           ds.Tables[0].Columns["newdateEcheance"].DataType = typeof(string);

                          

                           ds.Tables[0].Rows[0]["newdateEcheance"] = DateTime.Parse(ds.Tables[0].Rows[0]["DateEcheance"].ToString()).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                           
                            if (mode == 0)
                            {
                                #region Translate lettre


                                var montaconver = ds.Tables[0].Rows[0]["montantConvert"] != DBNull.Value ? Convert.ToDouble(ds.Tables[0].Rows[0]["montantConvert"]) : 0;
                                var montantFacture = ds.Tables[0].Rows[0]["montant_Facture"] != DBNull.Value ? Convert.ToDouble(ds.Tables[0].Rows[0]["montant_Facture"]) : 0;

                                string[] tabFacture = montantFacture.ToString().Replace('.', ',').Split(new char[] { ',' });
                                string translatconvrt1 = string.Empty;
                                string translatconvrt2 = string.Empty;

                                if (tabFacture.Length > 1)
                                {
                                    if (!tabFacture[0].Equals("0"))
                                        translatconvrt1 = NumberToEnglish.changeNumericToWords(tabFacture[0]);
                                    else translatconvrt1 = "Zero";
                                    ds.Tables[0].Rows[0]["montant_lettre_fact"] = translatconvrt1;


                                    if (!string.IsNullOrEmpty(tabFacture[1].TrimEnd(new char[] { '0' })))
                                    {
                                        if (!tabFacture[1].Equals("0"))
                                            translatconvrt2 = NumberToEnglish.changeNumericToWords(tabFacture[1]);
                                        else translatconvrt2 = "";

                                        ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = translatconvrt2;
                                    }
                                    else ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = "";
                                }
                                else
                                {
                                    if (!tabFacture[0].Equals("0"))
                                        translatconvrt1 = NumberToEnglish.changeNumericToWords(tabFacture[0]);
                                    else translatconvrt1 = "";

                                    ds.Tables[0].Rows[0]["montant_lettre_fact"] = translatconvrt1;
                                    ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = "";
                                }

                                //montant de conversion
                                string[] tabConvert = montaconver.ToString().Replace('.', ',').Split(new char[] { ',' });
                                string translatconvrt11 = string.Empty;
                                string translatconvrt21 = string.Empty;
                                if (tabConvert.Length > 1)
                                {
                                    if (!tabConvert[0].Equals("0"))
                                        translatconvrt11 = NumberToEnglish.changeNumericToWords(tabConvert[0]);
                                    else translatconvrt11 = "Zero";
                                    ds.Tables[0].Rows[0]["mnt_convert_lettre"] = translatconvrt11;


                                    if (!string.IsNullOrEmpty(tabConvert[1].TrimEnd(new char[] { '0' })))
                                    {
                                        if (!tabConvert[1].Equals("0"))
                                            translatconvrt21 = NumberToEnglish.changeNumericToWords(tabConvert[1]);
                                        else translatconvrt21 = "";

                                        ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = translatconvrt21;
                                    }
                                    else ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = "";
                                }
                                else
                                {
                                    if (!tabFacture[0].Equals("0"))
                                        translatconvrt11 = NumberToEnglish.changeNumericToWords(tabConvert[0]);
                                    else translatconvrt11 = "";

                                    ds.Tables[0].Rows[0]["mnt_convert_lettre"] = translatconvrt11;
                                    ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = "";
                                }
                                #endregion

                               //ds.Tables[0].Rows[0]["montant_lettre"] = ds.Tables[0].Rows[0]["montantConvert"] != DBNull.Value ? NumberToEnglish.changeNumericToWords(Convert.ToString(ds.Tables[0].Rows[0]["montantConvert"])) : "0";
                              // ds.Tables[0].Rows[0]["montantXaf_lettre"] = ds.Tables[0].Rows[0]["montantXaf"] != DBNull.Value ?( Convert.ToString(ds.Tables[0].Rows[0]["montantXaf"]).Equals("0")?"": NumberToEnglish.changeNumericToWords(Convert.ToString(ds.Tables[0].Rows[0]["montantXaf"]))) : "0";
                           }
                           else
                           {
                               #region Translate lettre


                               var montaconver = ds.Tables[0].Rows[0]["montantConvert"] != DBNull.Value ? Convert.ToDouble(ds.Tables[0].Rows[0]["montantConvert"]) : 0;
                               var montantFacture = ds.Tables[0].Rows[0]["montant_Facture"] != DBNull.Value ? Convert.ToDouble(ds.Tables[0].Rows[0]["montant_Facture"]) : 0;

                               string[] tabFacture = montantFacture.ToString().Replace('.', ',').Split(new char[] { ',' });
                               string translatconvrt1 = string.Empty;
                               string translatconvrt2 = string.Empty;

                               if (tabFacture.Length > 1)
                               {
                                   if (!tabFacture[0].Equals("0"))
                                       translatconvrt1 = NumberToEnglish.changeNumericToWords(tabFacture[0]);
                                   else translatconvrt1 = "Zero";
                                   ds.Tables[0].Rows[0]["montant_lettre_fact"] = "Minus " + translatconvrt1;


                                   if (!string.IsNullOrEmpty(tabFacture[1].TrimEnd(new char[] { '0' })))
                                   {
                                       if (!tabFacture[1].Equals("0"))
                                           translatconvrt2 = NumberToEnglish.changeNumericToWords(tabFacture[1]);
                                       else translatconvrt2 = "";

                                       ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = "Minus " + translatconvrt2;
                                   }
                                   else ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = "";
                               }
                               else
                               {
                                   if (!tabFacture[0].Equals("0"))
                                       translatconvrt1 = NumberToEnglish.changeNumericToWords(tabFacture[0]);
                                   else translatconvrt1 = "";

                                   ds.Tables[0].Rows[0]["montant_lettre_fact"] = translatconvrt1;
                                   ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = "";
                               }

                               //montant de conversion
                               string[] tabConvert = montaconver.ToString().Replace('.', ',').Split(new char[] { ',' });
                               string translatconvrt11 = string.Empty;
                               string translatconvrt21 = string.Empty;
                               if (tabConvert.Length > 1)
                               {
                                   if (!tabConvert[0].Equals("0"))
                                       translatconvrt11 = NumberToEnglish.changeNumericToWords(tabConvert[0]);
                                   else translatconvrt11 = "Zero";
                                   ds.Tables[0].Rows[0]["mnt_convert_lettre"] = "Minus " + translatconvrt11;


                                   if (!string.IsNullOrEmpty(tabConvert[1].TrimEnd(new char[] { '0' })))
                                   {
                                       if (!tabConvert[1].Equals("0"))
                                           translatconvrt21 = NumberToEnglish.changeNumericToWords(tabConvert[1]);
                                       else translatconvrt21 = "";

                                       ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = translatconvrt21;
                                   }
                                   else ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = "";
                               }
                               else
                               {
                                   if (!tabFacture[0].Equals("0"))
                                       translatconvrt11 = NumberToEnglish.changeNumericToWords(tabConvert[0]);
                                   else translatconvrt11 = "";

                                   ds.Tables[0].Rows[0]["mnt_convert_lettre"] = "Minus " + translatconvrt11;
                                   ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = "";
                               }
                               #endregion
                              
                              // ds.Tables[0].Rows[0]["montant_lettre"] = ds.Tables[0].Rows[0]["montantConvert"] != DBNull.Value ? "Minus " + NumberToEnglish.changeNumericToWords(Convert.ToString(ds.Tables[0].Rows[0]["montantConvert"])) : "0";
                              // ds.Tables[0].Rows[0]["montantXaf_lettre"] = ds.Tables[0].Rows[0]["montantXaf"] != DBNull.Value ? (Convert.ToString(ds.Tables[0].Rows[0]["montantXaf"]).Equals("0")?"": "Minus " + NumberToEnglish.changeNumericToWords(Convert.ToString(ds.Tables[0].Rows[0]["montantXaf"]))) : "0";
                           }
                       }
                   }


               }
           }
           return ds;
       }

       public static DataSet GetFacture_Avoir(long id, int idSite)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           DataSet ds = DAL.ReportFACTURE_AVOIR(id, idSite);

           if (ds != null)
           {
               if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
               {
                   if (Convert.ToInt32(ds.Tables[0].Rows[0]["IDLAngue"]) == 1)
                   {
                       ds.Tables[0].Rows[0]["DateImpression"] = string.Format("{0} {1} ", ds.Tables[0].Rows[0]["DateImpression"], DateTime.Parse(ds.Tables[0].Rows[0]["Date_Creation"].ToString()).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR")));
                       ds.Tables[0].Columns["newdateEcheance"].DataType = typeof(string);
                       ds.Tables[0].Rows[0]["newdateEcheance"] = DateTime.Parse(ds.Tables[0].Rows[0]["DateEcheance"].ToString()).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));


                       //  var valmontant = ds.Tables[0].Rows[0]["montantConvert"] != DBNull.Value ? ds.Tables[0].Rows[0]["montantConvert"] : 0;
                       //  string conveString = double.Parse( valmontant.ToString()) !=0 ? NombreEnLettre.changerNombrelettre(valmontant.ToString()):"0";
                       //  ds.Tables[0].Rows[0]["montant_lettre"] = conve;
                       // ds.Tables[0].Rows[0]["montant_lettre"] = conveString;
                       ds.Tables[0].Rows[0]["montant_lettre"] = ds.Tables[0].Rows[0]["montantConvert"] != DBNull.Value ? "Moins "+ NombreEnLettre.changerNombrelettre(Convert.ToString(ds.Tables[0].Rows[0]["montantConvert"])) : "0";
                       ds.Tables[0].Rows[0]["montantXaf_lettre"] = ds.Tables[0].Rows[0]["montantXaf"] != DBNull.Value ? "Moins " + NombreEnLettre.changerNombrelettre(Convert.ToString(ds.Tables[0].Rows[0]["montantXaf"])) : "0";

                   }
                   else
                   {
                       if (Convert.ToInt32(ds.Tables[0].Rows[0]["IDLAngue"]) == 2)
                       {
                           var debut = ds.Tables[0].Rows[0]["DateEcheance"];
                           int newDateDay = DateTime.Parse(ds.Tables[0].Rows[0]["Date_Creation"].ToString()).Day;

                           DateTime dateImp = DateTime.Parse(ds.Tables[0].Rows[0]["Date_Creation"].ToString());
                           string dateImpression = dateImp.ToString("MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                           string NdateImp = getDayWithSuffix(dateImp.Day);

                           ds.Tables[0].Rows[0]["DateImpression"] = string.Format("{0} {1} {2}", ds.Tables[0].Rows[0]["DateImpression"], NdateImp, dateImpression);

                           ds.Tables[0].Columns["newdateEcheance"].DataType = typeof(string);
                           ds.Tables[0].Rows[0]["newdateEcheance"] = DateTime.Parse(ds.Tables[0].Rows[0]["DateEcheance"].ToString()).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));

                           ds.Tables[0].Rows[0]["montant_lettre"] = ds.Tables[0].Rows[0]["montantConvert"] != DBNull.Value ?"Minus "+ NumberToEnglish.changeNumericToWords(Convert.ToString(ds.Tables[0].Rows[0]["montantConvert"])) : "0";
                           ds.Tables[0].Rows[0]["montantXaf_lettre"] = ds.Tables[0].Rows[0]["montantXaf"] != DBNull.Value ? "Minus " + NumberToEnglish.changeNumericToWords(Convert.ToString(ds.Tables[0].Rows[0]["montantXaf"])) : "0";
                       }
                   }


               }
           }
           return ds;
       }

       public static DataSet GetFacture_archive(long id,int idSite)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           DataSet ds = DAL.ReportFACTURE_Archive(id, idSite);

           if (ds != null)
           {
               if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
               {
                   if (Convert.ToInt32(ds.Tables[0].Rows[0]["IDLAngue"]) == 1)
                   {
                       ds.Tables[0].Rows[0]["DateImpression"] = string.Format("{0} {1} ", ds.Tables[0].Rows[0]["DateImpression"], DateTime.Parse(ds.Tables[0].Rows[0]["Date_Creation"].ToString()).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR")));
                       ds.Tables[0].Columns["newdateEcheance"].DataType = typeof(string);
                       ds.Tables[0].Rows[0]["newdateEcheance"] = DateTime.Parse(ds.Tables[0].Rows[0]["DateEcheance"].ToString()).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));

                       #region Translate lettre


                       var montaconver = ds.Tables[0].Rows[0]["montantConvert"] != DBNull.Value ? Convert.ToDouble(ds.Tables[0].Rows[0]["montantConvert"]) : 0;
                       var montantFacture = ds.Tables[0].Rows[0]["montant_Facture"] != DBNull.Value ? Convert.ToDouble(ds.Tables[0].Rows[0]["montant_Facture"]) : 0;

                       string[] tabFacture = montantFacture.ToString().Replace('.', ',').Split(new char[] { ',' });
                       string translatconvrt1 = string.Empty;
                       string translatconvrt2 = string.Empty;

                       if (tabFacture.Length > 1)
                       {
                           if (!tabFacture[0].Equals("0"))
                               translatconvrt1 = NombreEnLettre.changerNombrelettre(tabFacture[0]);
                           else translatconvrt1 = "Zero";
                           ds.Tables[0].Rows[0]["montant_lettre_fact"] = translatconvrt1;


                           if (!string.IsNullOrEmpty(tabFacture[1].TrimEnd(new char[] { '0' })))
                           {
                               if (!tabFacture[1].Equals("0"))
                                   translatconvrt2 = NombreEnLettre.changerNombrelettre(tabFacture[1]);
                               else translatconvrt2 = "";

                               ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = translatconvrt2;
                           }
                           else ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = "";
                       }
                       else
                       {
                           if (!tabFacture[0].Equals("0"))
                               translatconvrt1 = NombreEnLettre.changerNombrelettre(tabFacture[0]);
                           else translatconvrt1 = "";

                           ds.Tables[0].Rows[0]["montant_lettre_fact"] = translatconvrt1;
                           ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = "";
                       }

                       //montant de conversion
                       string[] tabConvert = montaconver.ToString().Replace('.', ',').Split(new char[] { ',' });
                       string translatconvrt11 = string.Empty;
                       string translatconvrt21 = string.Empty;
                       if (tabConvert.Length > 1)
                       {
                           if (!tabConvert[0].Equals("0"))
                               translatconvrt11 = NombreEnLettre.changerNombrelettre(tabConvert[0]);
                           else translatconvrt11 = "Zero";
                           ds.Tables[0].Rows[0]["mnt_convert_lettre"] = translatconvrt11;


                           if (!string.IsNullOrEmpty(tabConvert[1].TrimEnd(new char[] { '0' })))
                           {
                               if (!tabConvert[1].Equals("0"))
                                   translatconvrt21 = NombreEnLettre.changerNombrelettre(tabConvert[1]);
                               else translatconvrt21 = "";

                               ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = translatconvrt21;
                           }
                           else ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = "";
                       }
                       else
                       {
                           if (!tabFacture[0].Equals("0"))
                               translatconvrt11 = NombreEnLettre.changerNombrelettre(tabConvert[0]);
                           else translatconvrt11 = "";

                           ds.Tables[0].Rows[0]["mnt_convert_lettre"] = translatconvrt11;
                           ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = "";
                       }
                       #endregion


                     //  ds.Tables[0].Rows[0]["montant_lettre"] = ds.Tables[0].Rows[0]["montantConvert"] != DBNull.Value ? NombreEnLettre.changerNombrelettre(Convert.ToString(ds.Tables[0].Rows[0]["montantConvert"])) : "0";
                       //ds.Tables[0].Rows[0]["montantXaf_lettre"] = ds.Tables[0].Rows[0]["montantXaf"] != DBNull.Value ?( Convert.ToString(ds.Tables[0].Rows[0]["montantXaf"]).Equals("0") ? "" : NombreEnLettre.changerNombrelettre(Convert.ToString(ds.Tables[0].Rows[0]["montantXaf"]))) : "0";

                   }
                   else
                   {
                       if (Convert.ToInt32(ds.Tables[0].Rows[0]["IDLAngue"]) == 2)
                       {
                           var debut = ds.Tables[0].Rows[0]["DateEcheance"];
                           int newDateDay = DateTime.Parse(ds.Tables[0].Rows[0]["Date_Creation"].ToString()).Day;

                           DateTime dateImp = DateTime.Parse(ds.Tables[0].Rows[0]["Date_Creation"].ToString());
                           string dateImpression = dateImp.ToString("MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                           string NdateImp = getDayWithSuffix(dateImp.Day);

                           ds.Tables[0].Rows[0]["DateImpression"] = string.Format("{0} {1} {2}", ds.Tables[0].Rows[0]["DateImpression"], NdateImp, dateImpression);

                           ds.Tables[0].Columns["newdateEcheance"].DataType = typeof(string);
                           ds.Tables[0].Rows[0]["newdateEcheance"] = DateTime.Parse(ds.Tables[0].Rows[0]["DateEcheance"].ToString()).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));

                           #region Translate lettre


                           var montaconver = ds.Tables[0].Rows[0]["montantConvert"] != DBNull.Value ? Convert.ToDouble(ds.Tables[0].Rows[0]["montantConvert"]) : 0;
                           var montantFacture = ds.Tables[0].Rows[0]["montant_Facture"] != DBNull.Value ? Convert.ToDouble(ds.Tables[0].Rows[0]["montant_Facture"]) : 0;

                           string[] tabFacture = montantFacture.ToString().Replace('.', ',').Split(new char[] { ',' });
                           string translatconvrt1 = string.Empty;
                           string translatconvrt2 = string.Empty;

                           if (tabFacture.Length > 1)
                           {
                               if (!tabFacture[0].Equals("0"))
                                   translatconvrt1 = NumberToEnglish.changeNumericToWords(tabFacture[0]);
                               else translatconvrt1 = "Zero";
                               ds.Tables[0].Rows[0]["montant_lettre_fact"] = translatconvrt1;


                               if (!string.IsNullOrEmpty(tabFacture[1].TrimEnd(new char[] { '0' })))
                               {
                                   if (!tabFacture[1].Equals("0"))
                                       translatconvrt2 = NumberToEnglish.changeNumericToWords(tabFacture[1]);
                                   else translatconvrt2 = "";

                                   ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = translatconvrt2;
                               }
                               else ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = "";
                           }
                           else
                           {
                               if (!tabFacture[0].Equals("0"))
                                   translatconvrt1 = NumberToEnglish.changeNumericToWords(tabFacture[0]);
                               else translatconvrt1 = "";

                               ds.Tables[0].Rows[0]["montant_lettre_fact"] = translatconvrt1;
                               ds.Tables[0].Rows[0]["mnt_lettreCentime_fact"] = "";
                           }

                           //montant de conversion
                           string[] tabConvert = montaconver.ToString().Replace('.', ',').Split(new char[] { ',' });
                           string translatconvrt11 = string.Empty;
                           string translatconvrt21 = string.Empty;
                           if (tabConvert.Length > 1)
                           {
                               if (!tabConvert[0].Equals("0"))
                                   translatconvrt11 = NumberToEnglish.changeNumericToWords(tabConvert[0]);
                               else translatconvrt11 = "Zero";
                               ds.Tables[0].Rows[0]["mnt_convert_lettre"] = translatconvrt11;


                               if (!string.IsNullOrEmpty(tabConvert[1].TrimEnd(new char[] { '0' })))
                               {
                                   if (!tabConvert[1].Equals("0"))
                                       translatconvrt21 = NumberToEnglish.changeNumericToWords(tabConvert[1]);
                                   else translatconvrt21 = "";

                                   ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = translatconvrt21;
                               }
                               else ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = "";
                           }
                           else
                           {
                               if (!tabFacture[0].Equals("0"))
                                   translatconvrt11 = NumberToEnglish.changeNumericToWords(tabConvert[0]);
                               else translatconvrt11 = "";

                               ds.Tables[0].Rows[0]["mnt_convert_lettre"] = translatconvrt11;
                               ds.Tables[0].Rows[0]["mnt_convert_Lettrecentime"] = "";
                           }
                           #endregion

                          // ds.Tables[0].Rows[0]["montant_lettre"] = ds.Tables[0].Rows[0]["montantConvert"] != DBNull.Value ? NumberToEnglish.changeNumericToWords(Convert.ToString(ds.Tables[0].Rows[0]["montantConvert"])) : "0";
                         //  ds.Tables[0].Rows[0]["montantXaf_lettre"] = ds.Tables[0].Rows[0]["montantXaf"] != DBNull.Value ? (Convert.ToString(ds.Tables[0].Rows[0]["montantXaf"]).Equals("0") ? "" : NumberToEnglish.changeNumericToWords(Convert.ToString(ds.Tables[0].Rows[0]["montantXaf"]))) : "0";
                       }
                   }


               }
           }
          
           return ds;
       }

     
       public static DataTable GetFacture(long id)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
            
           try
           {
               DataTable table = DAL.ReportFACTURE(id);

               if (table != null)
               {
                   //
                   if (Convert.ToInt32(table.Rows[0]["IDLAngue"]) == 1)
                   {
                       //table.Rows[0]["DateImpression"] =string.Format("{0} {1}", table.Rows[0]["DateImpression"] , DateTime.Parse(table.Rows[0]["Date_Creation"].ToString()).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR")));
                       //table.Columns["newdateEcheance"].DataType = typeof(string);
                       //table.Rows[0]["newdateEcheance"] = DateTime.Parse(table.Rows[0]["DateEcheance"].ToString()).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));

                       table.Rows[0]["DateImpression"] = string.Format("{0} {1} ", table.Rows[0]["DateImpression"], DateTime.Parse(table.Rows[0]["Date_Creation"].ToString()).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR")));
                       table.Columns["newdateEcheance"].DataType = typeof(string);
                       table.Rows[0]["newdateEcheance"] = DateTime.Parse(table.Rows[0]["DateEcheance"].ToString()).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));
                      
                       #region Translate lettre
                       
                     
                       var montaconver = table.Rows[0]["montantConvert"] != DBNull.Value ? Convert.ToDouble( table.Rows[0]["montantConvert"]) : 0;
                       var montantFacture = table.Rows[0]["montant_Facture"] != DBNull.Value ? Convert.ToDouble(table.Rows[0]["montant_Facture"]) : 0;

                         string[] tabFacture = montantFacture.ToString().Replace('.', ',').Split(new char[] { ',' });
                         string translatconvrt1=string.Empty ;
                         string translatconvrt2=string.Empty ;

                       if (tabFacture.Length > 1)
                       {
                           if (!tabFacture[0].Equals("0"))
                               translatconvrt1 = NombreEnLettre.changerNombrelettre(tabFacture[0]);
                           else translatconvrt1 = "Zero";
                           table.Rows[0]["montant_lettre_fact"] = translatconvrt1;


                           if (!string.IsNullOrEmpty(tabFacture[1].TrimEnd(new char[] { '0' })))
                           {
                               if (!tabFacture[1].Equals("0"))
                                translatconvrt2 = NombreEnLettre.changerNombrelettre(tabFacture[1]);
                               else translatconvrt2 = "";

                               table.Rows[0]["mnt_lettreCentime_fact"] = translatconvrt2;
                           }
                           else table.Rows[0]["mnt_lettreCentime_fact"] = "";
                       }
                       else
                       {
                           if (!tabFacture[0].Equals("0"))
                            translatconvrt1 = NombreEnLettre.changerNombrelettre(tabFacture[0]);
                           else translatconvrt1 = "";

                           table.Rows[0]["montant_lettre_fact"] = translatconvrt1;
                           table.Rows[0]["mnt_lettreCentime_fact"] = "";
                       }

                       //montant de conversion
                       string[] tabConvert = montaconver.ToString().Replace('.', ',').Split(new char[] { ',' });
                       string translatconvrt11 = string.Empty;
                       string translatconvrt21 = string.Empty;
                       if (tabConvert.Length > 1)
                       {
                           if (!tabConvert[0].Equals("0"))
                               translatconvrt11 = NombreEnLettre.changerNombrelettre(tabConvert[0]);
                           else translatconvrt11 = "Zero";
                           table.Rows[0]["mnt_convert_lettre"] = translatconvrt11;


                           if (!string.IsNullOrEmpty(tabConvert[1].TrimEnd(new char[] { '0' })))
                           {
                               if (!tabConvert[1].Equals("0"))
                                   translatconvrt21 = NombreEnLettre.changerNombrelettre(tabConvert[1]);
                               else translatconvrt21 = "";

                               table.Rows[0]["mnt_convert_Lettrecentime"] = translatconvrt21;
                           }
                           else table.Rows[0]["mnt_convert_Lettrecentime"] = "";
                       }
                       else
                       {
                           if (!tabFacture[0].Equals("0"))
                               translatconvrt11 = NombreEnLettre.changerNombrelettre(tabConvert[0]);
                           else translatconvrt11 = "";

                           table.Rows[0]["mnt_convert_lettre"] = translatconvrt11;
                           table.Rows[0]["mnt_convert_Lettrecentime"] = "";
                       }
                       #endregion
                       // table.Rows[0]["montantXaf_lettre"] = table.Rows[0]["montantXaf"] != DBNull.Value ?(Convert.ToString(table.Rows[0]["montantXaf"]).Equals("0")?"": NombreEnLettre.changerNombrelettre(Convert.ToString(table.Rows[0]["montantXaf"]))) : "0";
                     
                   }
                   if (Convert.ToInt32(table.Rows[0]["IDLAngue"]) == 2)
                   {
                       // Englais language
                       var debut = table.Rows[0]["DateEcheance"];
                       int newDateDay = DateTime.Parse(table.Rows[0]["Date_Creation"].ToString()).Day;

                       DateTime dateImp = DateTime.Parse(table.Rows[0]["Date_Creation"].ToString());
                       string dateImpression = dateImp.ToString("MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                       string NdateImp = getDayWithSuffix(dateImp.Day);

                       table.Rows[0]["DateImpression"] = string.Format("{0} {1} {2}", table.Rows[0]["DateImpression"], NdateImp, dateImpression);

                       table.Columns["newdateEcheance"].DataType = typeof(string);
                       table.Rows[0]["newdateEcheance"] = DateTime.Parse(table.Rows[0]["DateEcheance"].ToString()).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));

                       #region Translate lettre


                       var montaconver = table.Rows[0]["montantConvert"] != DBNull.Value ? Convert.ToDouble(table.Rows[0]["montantConvert"]) : 0;
                       var montantFacture = table.Rows[0]["montant_Facture"] != DBNull.Value ? Convert.ToDouble(table.Rows[0]["montant_Facture"]) : 0;

                       string[] tabFacture = montantFacture.ToString().Replace('.', ',').Split(new char[] { ',' });
                       string translatconvrt1 = string.Empty;
                       string translatconvrt2 = string.Empty;

                       if (tabFacture.Length > 1)
                       {
                           if (!tabFacture[0].Equals("0"))
                               translatconvrt1 = NumberToEnglish.changeNumericToWords(tabFacture[0]);
                           else translatconvrt1 = "Zero";
                           table.Rows[0]["montant_lettre_fact"] = translatconvrt1;


                           if (!string.IsNullOrEmpty(tabFacture[1].TrimEnd(new char[] { '0' })))
                           {
                               if (!tabFacture[1].Equals("0"))
                                   translatconvrt2 = NumberToEnglish.changeNumericToWords(tabFacture[1]);
                               else translatconvrt2 = "";

                               table.Rows[0]["mnt_lettreCentime_fact"] = translatconvrt2;
                           }
                           else table.Rows[0]["mnt_lettreCentime_fact"] = "";
                       }
                       else
                       {
                           if (!tabFacture[0].Equals("0"))
                               translatconvrt1 = NumberToEnglish.changeNumericToWords(tabFacture[0]);
                           else translatconvrt1 = "";

                           table.Rows[0]["montant_lettre_fact"] = translatconvrt1;
                           table.Rows[0]["mnt_lettreCentime_fact"] = "";
                       }

                       //montant de conversion
                       string[] tabConvert = montaconver.ToString().Replace('.', ',').Split(new char[] { ',' });
                       string translatconvrt11 = string.Empty;
                       string translatconvrt21 = string.Empty;
                       if (tabConvert.Length > 1)
                       {
                           if (!tabConvert[0].Equals("0"))
                               translatconvrt11 = NumberToEnglish.changeNumericToWords(tabConvert[0]);
                           else translatconvrt11 = "Zero";
                           table.Rows[0]["mnt_convert_lettre"] = translatconvrt11;


                           if (!string.IsNullOrEmpty(tabConvert[1].TrimEnd(new char[] { '0' })))
                           {
                               if (!tabConvert[1].Equals("0"))
                                   translatconvrt21 = NumberToEnglish.changeNumericToWords(tabConvert[1]);
                               else translatconvrt21 = "";

                               table.Rows[0]["mnt_convert_Lettrecentime"] = translatconvrt21;
                           }
                           else table.Rows[0]["mnt_convert_Lettrecentime"] = "";
                       }
                       else
                       {
                           if (!tabFacture[0].Equals("0"))
                               translatconvrt11 = NumberToEnglish.changeNumericToWords(tabConvert[0]);
                           else translatconvrt11 = "";

                           table.Rows[0]["mnt_convert_lettre"] = translatconvrt11;
                           table.Rows[0]["mnt_convert_Lettrecentime"] = "";
                       }
                       #endregion

                     // table.Rows[0]["montant_lettre"] = table.Rows[0]["montantConvert"] != DBNull.Value ? NumberToEnglish.changeNumericToWords(Convert.ToString(table.Rows[0]["montantConvert"])) : "0";
                      // table.Rows[0]["montantXaf_lettre"] = table.Rows[0]["montantXaf"] != DBNull.Value ? (Convert.ToString(table.Rows[0]["montantXaf"]).Equals("0") ? "" : NumberToEnglish.changeNumericToWords(Convert.ToString(table.Rows[0]["montantXaf"]))) : "0";
                     
                       
                   }
               }
               return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       public static DataTable GetLigneFacture(long id)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           string montant=string .Empty ;
           try
           {
               return  DAL.ReportLIGNEFACTURE(id);

               //if (table != null)
               //{
                

               //    DataRow currentRow = table.Rows[0];
               //    double montantbase = currentRow["montant_conversion"] != DBNull.Value ? double.Parse(currentRow["montant_conversion"].ToString()) : 0;

               //    if (currentRow["libelle_langue"].ToString() == "en")
               //        montant =NumberToEnglish .changeNumericToWords (montantbase.ToString());
               //    if (currentRow["libelle_langue"].ToString() == "fr")
               //        montant = NombreEnLettre.changerNombrelettre(montantbase.ToString());
               //    for (int i=0;i <table.Rows .Count ;i ++)
               //    {
               //        table.Rows[i]["montant_lettre"] = montant;

               //    }


               //}
               //return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       public static double  MontantFacture()
       {
           try
           {
            return  DAL.ReportMontantFactureLettre();
           } 
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       public static DataTable GetLigneFacture_nonExo(long id)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           string montant = string.Empty;
           try
           {
               DataTable table = DAL.ReportLIGNEFACTURE_NONEXO_PARTIEL(id);

               DataRow currentRow = table.Rows[0];
               double montantbase = currentRow["total_facture_TTC"] != DBNull.Value ? double.Parse(currentRow["total_facture_TTC"].ToString()) : 0;
               if (currentRow["libelle_langue"].ToString() == "en")
                   montant = NumberToEnglish.changeNumericToWords(montantbase.ToString());
               if (currentRow["libelle_langue"].ToString() == "fr")
                   montant = NombreEnLettre.changerNombrelettre(montantbase.ToString());
               for (int i = 0; i < table.Rows.Count; i++)
               {
                   table.Rows[i]["montant_lettre"] = montant;

               }

               return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }


       public static string getDayWithSuffix(int day)
       {
           string d = day.ToString();
           if (day < 11 || day > 13)
           {
               if (d.EndsWith("1"))
               {
                   d += "st";
               }
               else if (d.EndsWith("2"))
               {
                   d += "nd";
               }
               else if (d.EndsWith("3"))
               {
                   d += "rd";
               }
               else
               {
                   d += "th";
               }

           }
           else
           {
               d += "th";
           }
           return d;
       }

    }
}
