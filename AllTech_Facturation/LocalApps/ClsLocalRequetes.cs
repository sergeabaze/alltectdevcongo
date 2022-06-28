using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AllTech_Facturation.LocalApps
{
  public   class ClsLocalRequetes
    {

      public static SqlConnection getconnection()
      {
        
          try
          {
              string chaine = ConfigurationManager.ConnectionStrings["myLocalSqlServer"].ConnectionString;
              SqlConnection con = new SqlConnection(chaine);

              con.Open();
              return con;
          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message );
          }

      }


      public static License GetLicense()
      {
          SqlCommand comand = new SqlCommand();
          License license = null;
          try
          {
              comand.Connection = getconnection();
              comand.CommandType = CommandType.StoredProcedure ;
              comand.CommandText = "getLicense";
             
              SqlDataReader reader = comand.ExecuteReader();
                  while (reader.Read())
                  {
                      license = new License { id = reader.GetInt32(0), numLicense = reader.GetString(1), dateConnected = reader.GetDateTime (2) };
                      break;
                  }
              
          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }
          return  license;

      }

      public static bool  IsExistLicense()
      {
          SqlCommand comand = new SqlCommand();
          object nbreLignes;
          try
          {
              comand.Connection = getconnection();
              comand.CommandType = CommandType.Text ;
              comand.CommandText = "select count (*) from License";

                nbreLignes = comand.ExecuteScalar();
             
          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }
          return   int.Parse(nbreLignes.ToString ()) ==0 ?false:true ;
      }

      public static bool  AddLicense(int idste, string numeroLicense)
      {
          SqlCommand comand = new SqlCommand();
         
          try
          {
              comand.Connection = getconnection();
              comand.CommandType = CommandType.StoredProcedure;
              comand.CommandText = "AddLicense";

              comand.Parameters.Add (new SqlParameter("@InIdSte", SqlDbType.Int));
              comand.Parameters.Add(new SqlParameter("@InnumLicense", SqlDbType.VarChar,255));
              comand.Parameters["@InIdSte"].Value  = idste;
              comand.Parameters["@InnumLicense"].Value = numeroLicense;
               comand.ExecuteNonQuery ();
               return true;

          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }
         
      }
    }

  public class License
  {
      public int id { get; set; }
      public string numLicense { get; set; }
      public DateTime? dateConnected { get; set; }
  }
    
}
