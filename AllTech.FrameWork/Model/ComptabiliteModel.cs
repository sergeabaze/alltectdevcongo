using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FACTURATION_DAL;
using System.Data;

namespace AllTech.FrameWork.Model
{
   public  class ComptabiliteModel
    {
       static IFacturation DAL = null;

       public ComptabiliteModel()
       {

       }

       /// <summary>
       /// function qui liste les donnes de parametres du ficfier à generer
       /// </summary>
       /// <returns></returns>
       public static DataTable GetComptaGene_Param_Fichier()
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DataTable table = DAL.GetComptaGene_Liste_ChampFichier();
               return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       /// <summary>
       /// champ selectionner
       /// </summary>
       /// <returns></returns>
       public static DataTable GetComptaGene_Param_Liste()
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DataTable table = DAL.GetComptaGene_Param_Liste();
               return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       public static bool GetComptaGene_Param_Add(int id, int idChamp, int taille, int position)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
                DAL.GetComptaGene_Param_Add( id, idChamp, taille, position);
               return true;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       public static bool GetComptaGene_Param_Delete(int id)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
                DAL.GetComptaGene_Param_Delete( id);
               return true;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }



       public static DataTable GetComptaGene_Liste()
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DataTable table = DAL.GetComptaGene_Liste();
               return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       /// <summary>
       /// creation du libelle de parametrage et son code
       /// </summary>
       /// <param name="id"></param>
       /// <param name="libelle"></param>
       /// <param name="code"></param>
       /// <returns></returns>
       public static bool GetComptaGene_Add(int id, string libelle,int code)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DAL.GetComptaGene_Add(id, libelle,code);
               return true;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }
       //suppression du libelle de parametrage et son code
       public static bool GetComptaGene_Delete(int id)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DAL.GetComptaGene_Delete(id);
               return true;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       //log journal vente

       public static DataTable GetLogCompta(int idJv, string typeMessage)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               return DAL.GetLogComptaByJv(idJv, typeMessage);
              
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       public static bool LogComptaAdd(int idJv, string numFacture, string messagererror, string typeMessage, bool valMessage)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               return DAL.LogComptaAdd(idJv, numFacture, messagererror, typeMessage, valMessage);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       public static bool LogComptaDelete(int idJv)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               return DAL.LogComptaDeLETE(idJv);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

    }
}
