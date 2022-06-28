using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace FACTURATION_DAL.Model
{
  public   class CompteGeneral
    {

      public int IdCompteGen { get; set; }
      public string Libelle { get; set; }
      public string Code { get; set; }
    

      private string ConnectionString = string.Empty;

      public CompteGeneral(string _connectionString)
      {
          IdCompteGen = 0;
          Libelle = string.Empty;
          Code = string.Empty;
          this.ConnectionString = _connectionString;
      }

      public CompteGeneral()
      {
      }

      #region Const

      internal const string PS_DELETE = "PS_CompteGenDelete";
      internal const string PS_INSERT = "PS_CompteGenInsert";
      internal const string PS_SELECT = "PS_CompteGenSelect";
      internal const string PS_UPDATE = "PS_CompteGenUpdate";
      #endregion

      #region Properties

   


      #endregion

      #region Methodes

      public CompteGeneral SelectByid(int id)
      {
          CompteGeneral compte = null;
          using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
          {
               con.Open();
                   MySqlCommand comand = new MySqlCommand();
                   comand.Connection = con;
                   comand.CommandType = CommandType.StoredProcedure;
                   comand.CommandText = PS_SELECT;
                   comand.CommandTimeout = Constants.TimeOut;
                   comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                   comand.Parameters.Add("inId", MySqlDbType.Int32);
                   comand.Parameters["inIdsite"].Value = 0;
                   comand.Parameters["inId"].Value = id;
                   MySqlDataReader reader = comand.ExecuteReader();
                   while (reader.Read())
                   {
                       compte = new CompteGeneral();
                       compte.IdCompteGen = reader.GetInt32(0);
                       compte.Libelle = reader.GetString(1);
                       compte.Code = reader.GetString(2);
                      
                       break;
                   }
          }
          return compte;
      }

      public List <CompteGeneral> SelectAll(int idSite)
      {
          List<CompteGeneral> comptes = new List<CompteGeneral>();
          CompteGeneral compte = null;
          using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
          {
              con.Open();
              MySqlCommand comand = new MySqlCommand();
              comand.Connection = con;
              comand.CommandType = CommandType.StoredProcedure;
              comand.CommandText = PS_SELECT;
              comand.CommandTimeout = Constants.TimeOut;
              comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
              comand.Parameters.Add("inId", MySqlDbType.Int32);
              comand.Parameters["inIdsite"].Value = idSite;
              comand.Parameters["inId"].Value = 0;
              MySqlDataReader reader = comand.ExecuteReader();
              while (reader.Read())
              {
                  compte = new CompteGeneral();
                  compte.IdCompteGen = reader.GetInt32(0);
                  compte.Libelle = reader.GetString(1);
                  compte.Code = reader.GetString(2);
               
                  comptes.Add(compte);
              }
          }

          return comptes;
      }

      public bool Insert(int idSite,int idcompteOhada)
      {
          string valString = FDataBase.GetConnection(ConnectionString);

          using (MySqlConnection con = new MySqlConnection(valString))
          {
              MySqlCommand comand = new MySqlCommand();
              comand.CommandType = CommandType.StoredProcedure;
              comand.CommandText = PS_INSERT;
              comand.CommandTimeout = Constants.TimeOut;

              comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
              comand.Parameters.Add("inIdCodeoahda", MySqlDbType.Int32);

              comand.Parameters["inIdSite"].Value = idSite;
              comand.Parameters["inIdCodeoahda"].Value = idcompteOhada;
              
              comand.Connection = con;
              con.Open();
              comand.ExecuteNonQuery();
          }
          return true;
      }

      public bool Update(int id, int idcompteOhada)
      {
          using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
          {
              MySqlCommand comand = new MySqlCommand();
              comand.CommandType = CommandType.StoredProcedure;
              comand.CommandText = PS_UPDATE;
              comand.CommandTimeout = Constants.TimeOut;
              comand.Parameters.Add("inId", MySqlDbType.Int32);
              comand.Parameters.Add("inIdCodeoahda", MySqlDbType.Int32);

              comand.Parameters["inId"].Value = id;
              comand.Parameters["inIdCodeoahda"].Value = idcompteOhada;
             

              comand.Connection = con;
              con.Open();
              comand.ExecuteNonQuery();
          }
          return true;
      }


     
      public bool Delete(int idCompte)
      {
          using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
          {
              MySqlCommand comand = new MySqlCommand();
              comand.CommandType = CommandType.StoredProcedure;
              comand.CommandText = PS_DELETE;
              comand.CommandTimeout = Constants.TimeOut;
              comand.Parameters.Add("inId", MySqlDbType.Int32);
              comand.Parameters["inId"].Value = idCompte;
              comand.Connection = con;
              con.Open();
              comand.ExecuteNonQuery();
          }

          return true;
      }
        
        #endregion
    }
}
