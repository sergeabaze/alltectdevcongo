using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace FACTURATION_DAL.Model
{
  public   class CompteTiers
    {

        public int IdCompteT { get; set; }
        public string NumeroCompte { get; set; }
        public int IdClient { get; set; }
        public int IdSite { get; set; }

        private string ConnectionString = string.Empty;

        public CompteTiers(string _connectionString)
        {
            this.ConnectionString = _connectionString;
        }

        public CompteTiers()
          {
          }

        #region Const

        internal const string PS_DELETE = "PS_CompteTiersDelete";
        internal const string PS_INSERT = "PS_CompteTiersInsert";
        internal const string PS_SELECT = "PS_CompteTiersSelect";
        internal const string PS_SELECT_BY = "PS_CompteTiersSelectByClient";
        internal const string PS_UPDATE = "PS_CompteTiersUpdate";
        #endregion

       

        #region Methods
        public CompteTiers SelectByid(int id)
        {
            CompteTiers compte = null;
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_SELECT;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                comand.Parameters["inId"].Value = id;
                comand.Parameters["inIdSite"].Value = 0;
           
                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    compte = new CompteTiers();
                    compte.IdCompteT = reader.GetInt32(0);
                    compte.IdSite = reader.GetInt32(1);
                    compte.IdClient = reader.GetInt32(2);
                    compte.NumeroCompte = reader.GetString(3);
               
                    break;
                }
            }
            return compte;
        }

        public List<CompteTiers> SelectAll(int idSite)
        {
            CompteTiers compte = null;
            List<CompteTiers> comptes = new List<CompteTiers>();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_SELECT;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                comand.Parameters["inId"].Value = 0;
                comand.Parameters["inIdSite"].Value = idSite;

                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    compte = new CompteTiers();
                    compte.IdCompteT = reader.GetInt32(0);
                    compte.IdSite = reader.GetInt32(1);
                    compte.IdClient = reader.GetInt32(2);
                    compte.NumeroCompte = reader.GetString(3);

                    comptes.Add(compte);
                }
            }
            return comptes;
        }


        public List<CompteTiers> SelectByClient(int idSite,int idClient)
        {
            CompteTiers compte = null;
            List<CompteTiers> comptes = new List<CompteTiers>();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_SELECT_BY;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                comand.Parameters.Add("inMode", MySqlDbType.Int32);
                comand.Parameters["inIdSite"].Value = idSite;
                comand.Parameters["inIdClient"].Value = idClient;
                comand.Parameters["inMode"].Value = 0;
                
                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    compte = new CompteTiers();
                    compte.IdCompteT = reader.GetInt32(0);
                    compte.IdSite = reader.GetInt32(1);
                    compte.IdClient = reader.GetInt32(2);
                    compte.NumeroCompte = reader.GetString(3);

                    comptes.Add(compte);
                }
            }
            return comptes;
        }

        public List<CompteTiers> SelectByClient_Archive(int idSite, int idClient)
        {
            CompteTiers compte = null;
            List<CompteTiers> comptes = new List<CompteTiers>();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_SELECT_BY;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                comand.Parameters.Add("inMode", MySqlDbType.Int32);
                comand.Parameters["inIdSite"].Value = idSite;
                comand.Parameters["inIdClient"].Value = idClient;
                comand.Parameters["inMode"].Value = 1;

                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    compte = new CompteTiers();
                    compte.IdCompteT = reader.GetInt32(0);
                    compte.IdSite = reader.GetInt32(1);
                    compte.IdClient = reader.GetInt32(2);
                    compte.NumeroCompte = reader.GetString(3);

                    comptes.Add(compte);
                }
            }
            return comptes;
        }


       // liste les comptestiers du clint non utilisr ds  linterface exploitation
        public List<CompteTiers> SelectByExploitation(int idSite, int idClient)
        {
            CompteTiers compte = null;
            List<CompteTiers> comptes = new List<CompteTiers>();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_CompteTiersSelectByExploitation";
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                comand.Parameters["inIdSite"].Value = idSite;
                comand.Parameters["inIdClient"].Value = idClient;

                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    compte = new CompteTiers();
                    compte.IdCompteT = reader.GetInt32(0);
                    compte.IdSite = reader.GetInt32(1);
                    compte.IdClient = reader.GetInt32(2);
                    compte.NumeroCompte = reader.GetString(3);

                    comptes.Add(compte);
                }
            }
            return comptes;
        }


        public bool Insert(CompteTiers compte, int idSite)
        {
            string valString = FDataBase.GetConnection(ConnectionString);

            using (MySqlConnection con = new MySqlConnection(valString))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_INSERT;
                comand.CommandTimeout = Constants.TimeOut;

                comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                comand.Parameters.Add("inNumero", MySqlDbType.VarChar, 255);
                comand.Parameters["inIdClient"].Value = compte.IdClient;
                comand.Parameters["inIdsite"].Value = idSite;
                comand.Parameters["inNumero"].Value = compte.NumeroCompte;
               

                comand.Connection = con;
                con.Open();
                comand.ExecuteNonQuery();
            }
            return true;
        }


        public bool Update(CompteTiers compte)
        {
            string valString = FDataBase.GetConnection(ConnectionString);

            using (MySqlConnection con = new MySqlConnection(valString))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_UPDATE;
                comand.CommandTimeout = Constants.TimeOut;

                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters.Add("inNumero", MySqlDbType.VarChar, 255);
                comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                comand.Parameters.Add("inMode", MySqlDbType.Int32);
                comand.Parameters["inId"].Value = compte.IdCompteT;
                comand.Parameters["inNumero"].Value = compte.NumeroCompte;
                comand.Parameters["inIdClient"].Value = compte.IdClient;
                comand.Parameters["inIdsite"].Value = compte.IdSite;
                comand.Parameters["inMode"].Value = 0;


                comand.Connection = con;
                con.Open();
                comand.ExecuteNonQuery();
            }
            return true;
        }

        //inId int,inNumero varchar(100),inIdClient int, inIdsite int,inMode int
        public bool Update_Archive(CompteTiers compte)
        {
            string valString = FDataBase.GetConnection(ConnectionString);

            using (MySqlConnection con = new MySqlConnection(valString))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_UPDATE;
                comand.CommandTimeout = Constants.TimeOut;

                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters.Add("inNumero", MySqlDbType.VarChar, 255);
                comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                comand.Parameters.Add("inMode", MySqlDbType.Int32);
                comand.Parameters["inId"].Value = compte.IdCompteT;
                comand.Parameters["inNumero"].Value = compte.NumeroCompte;
                comand.Parameters["inIdClient"].Value = compte.IdClient;
                comand.Parameters["inIdsite"].Value = compte.IdSite;
                comand.Parameters["inMode"].Value =1;


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
