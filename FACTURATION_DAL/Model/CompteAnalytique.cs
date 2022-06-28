using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace FACTURATION_DAL.Model
{
  public   class CompteAnalytique
    {

        private int idCompteAnalytique;
        private string numerocompte;
        public string Code { get; set; }
        public string Libelle { get; set; }

        private string ConnectionString = string.Empty;
        public CompteAnalytique(string _connectionString)
      {
          idCompteAnalytique = 0;
          numerocompte = string.Empty;
          this.ConnectionString = _connectionString;
      }

        public CompteAnalytique()
        {
            idCompteAnalytique = 0;
            numerocompte = string.Empty;
        }

        #region Properties

      public int IdCompteAnalytique
      {
          get { return idCompteAnalytique; }
          set { idCompteAnalytique = value; }
      }


      public string Numerocompte
      {
          get { return numerocompte; }
          set { numerocompte = value; }
      }
      #endregion

        #region Const

      internal const string PS_DELETE = "PS_CompteAnalytiqueDelete";
      internal const string PS_INSERT = "PS_CompteAnalytiqueInsert";
      internal const string PS_SELECT = "PS_CompteAnalytiqueSelect";
      internal const string PS_UPDATE = "PS_CompteAnalytiqueUpdate";
      #endregion

        #region Methodes

      public CompteAnalytique SelectByid(int id)
      {
          CompteAnalytique compte = null;
          using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
          {
              con.Open();
              MySqlCommand comand = new MySqlCommand();
              comand.Connection = con;
              comand.CommandType = CommandType.StoredProcedure;
              comand.CommandText = PS_SELECT;
              comand.CommandTimeout = Constants.TimeOut;
              comand.Parameters.Add("inId", MySqlDbType.Int32);
              comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
              comand.Parameters["inId"].Value = id;
              comand.Parameters["inIdsite"].Value = 0;
              MySqlDataReader reader = comand.ExecuteReader();
              while (reader.Read())
              {
                  compte = new CompteAnalytique();
                  compte.IdCompteAnalytique = reader.GetInt32(0);
                  compte.Numerocompte = reader.GetString(1);
                  if (!reader.IsDBNull(3))
                  compte.Code = reader.GetString(3);
                  if (!reader.IsDBNull(4))
                      compte.Libelle = reader.GetString(4);
                  break;
              }
          }
          return compte;
      }

      public List <CompteAnalytique> SelectAll(int idSite)
      {
          List<CompteAnalytique> comptes = new List<CompteAnalytique>();
          CompteAnalytique compte = null;
          using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
          {
              con.Open();
              MySqlCommand comand = new MySqlCommand();
              comand.Connection = con;
              comand.CommandType = CommandType.StoredProcedure;
              comand.CommandText = PS_SELECT;
              comand.CommandTimeout = Constants.TimeOut;
              comand.Parameters.Add("inId", MySqlDbType.Int32);
              comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
              comand.Parameters["inId"].Value = 0;
              comand.Parameters["inIdsite"].Value = idSite;
              MySqlDataReader reader = comand.ExecuteReader();
              while (reader.Read())
              {
                  compte = new CompteAnalytique();
                  compte.IdCompteAnalytique = reader.GetInt32(0);
                  compte.Numerocompte = reader.GetString(1);
                  if (!reader.IsDBNull(3))
                  compte.Code = reader.GetString(3);
                  if (!reader.IsDBNull(4))
                      compte.Libelle = reader.GetString(4);
                  comptes.Add(compte);
              }
          }

          return comptes;
      }

     


      public bool Insert(CompteAnalytique compte, int idSite)
      {
          using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
          {
              MySqlCommand comand = new MySqlCommand();
              comand.CommandType = CommandType.StoredProcedure;
              comand.CommandText = PS_INSERT;
              comand.CommandTimeout = Constants.TimeOut;
              comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
              comand.Parameters.Add("inNumeroCompte", MySqlDbType.VarChar, 255);
              comand.Parameters.Add("inCode", MySqlDbType.VarChar, 50);
              comand.Parameters.Add("inLibelleSection", MySqlDbType.VarChar, 255);
              comand.Parameters["inIdSite"].Value = idSite;
              comand.Parameters["inNumeroCompte"].Value = compte.Numerocompte;
              comand.Parameters["inCode"].Value = compte.Code;
              comand.Parameters["inLibelleSection"].Value = compte.Libelle;
              
              comand.Connection = con;
              con.Open();
              comand.ExecuteNonQuery();
          }
          return true;
      }

      public bool Update(CompteAnalytique compte)
      {
          using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
          {
              MySqlCommand comand = new MySqlCommand();
              comand.CommandType = CommandType.StoredProcedure;
              comand.CommandText = PS_UPDATE;
              comand.CommandTimeout = Constants.TimeOut;
              comand.Parameters.Add("inId", MySqlDbType.Int32);
              comand.Parameters.Add("inNumeroCompte", MySqlDbType.VarChar, 255);
              comand.Parameters.Add("inCode", MySqlDbType.VarChar, 50);
              comand.Parameters.Add("inLibelleSection", MySqlDbType.VarChar, 255);
              comand.Parameters["inId"].Value = compte.IdCompteAnalytique;
              comand.Parameters["inNumeroCompte"].Value = compte.Numerocompte;
              comand.Parameters["inCode"].Value = compte.Code;
              comand.Parameters["inLibelleSection"].Value = compte.Libelle;

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
