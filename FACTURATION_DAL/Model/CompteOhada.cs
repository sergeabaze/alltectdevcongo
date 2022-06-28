using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace FACTURATION_DAL.Model
{
   public  class CompteOhada
    {

        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Code { get; set; }
        public int IdLibelleType { get; set; }

        private string ConnectionString = string.Empty;

        public CompteOhada(string _connectionString)
        {
            Id = 0;
            Libelle = string.Empty;
            Code = string.Empty;
            IdLibelleType = 0;
            this.ConnectionString = _connectionString;

        }

        public CompteOhada()
        {
            Id = 0;
            Libelle = string.Empty;
            Code = string.Empty;
            IdLibelleType = 0;
        }

        #region Const

        internal const string PS_DELETE = "PS_ComptaOhadaDelete";
        internal const string PS_INSERT = "PS_ComptaOhadaInsert";
        internal const string PS_SELECT = "PS_ComptaOhadaSelect";
        internal const string PS_UPDATE = "PS_ComptaOhadaUpdate";

        internal const string PS_LIBELLE_DELETE = "PS_CompteOhadaLibelleTypeDelete";
        internal const string PS_LIBELLE_ADD = "PS_CompteOhadaLibelleTypeAdd";
        internal const string PS_LIBELLE_SELECT = "PS_CompteOhadaLibelleTypeSelect";
        internal const string PS_LIBELLE_SELECTBY = "PS_CompteOhadaSelectbyLibelleType";
        internal const string PS_LIBELLE_SELECTBYId = "PS_CompteOhadaSelectByLibelleTypeId";

        internal const string PS_CMP_OAHADA_FREE = "PS_CompteOhada_SelecteFree";

        #endregion

        #region properties

        #endregion

        #region methods

        public List<CompteOhada> SelectCmpt_Free(int idSite)
        {
            List<CompteOhada> comptes = new List<CompteOhada>();
            CompteOhada compte = null;
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_CMP_OAHADA_FREE;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                //comand.Parameters["inIdsite"].Value = 0;
                comand.Parameters["inIdsite"].Value = idSite;
               
                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    compte = new CompteOhada();
                    compte.Id = reader.GetInt32(0);
                    compte.Code = reader.GetString(2);
                    compte.Libelle = reader.GetString(1);
                    compte.IdLibelleType = 0;
                    comptes.Add(compte);
                }
            }

            return comptes;
        }

        public CompteOhada SelectByid(int id)
        {
            CompteOhada compte = null;
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_SELECT;
                comand.CommandTimeout = Constants.TimeOut;
               // comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                comand.Parameters.Add("inId", MySqlDbType.Int32);
                //comand.Parameters["inIdsite"].Value = 0;
                comand.Parameters["inId"].Value = id;
                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    compte = new CompteOhada();
                    compte.Id = reader.GetInt32(0);
                    compte.Code = reader.GetString(1);
                    compte.Libelle = reader.GetString(2);
                    compte.IdLibelleType = reader.GetInt32(3);

                
                    break;
                }
            }
            return compte;
        }

       /// <summary>
       /// retourne la liste des comptes ohada par site
       /// </summary>
       /// <param name="idSite"></param>
       /// <returns></returns>
        public List<CompteOhada> SelectAll(int idSite)
        {
            List<CompteOhada> comptes = new List<CompteOhada>();
            CompteOhada compte = null;
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_SELECT;
                comand.CommandTimeout = Constants.TimeOut;
                //comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                comand.Parameters.Add("inId", MySqlDbType.Int32);
               // comand.Parameters["inIdsite"].Value = idSite;
                comand.Parameters["inId"].Value = 0;
                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    compte = new CompteOhada();
                    compte.Id = reader.GetInt32(0);
                    compte.Code = reader.GetString(1);
                    compte.Libelle = reader.GetString(2);
                   compte.IdLibelleType = reader.GetInt32(3);
                    comptes.Add(compte);
                }
            }

            return comptes;
        }

        public List<CompteOhada> SelectAllByLibelleId(int idLibelle)
        {
            List<CompteOhada> comptes = new List<CompteOhada>();
            CompteOhada compte = null;
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_LIBELLE_SELECTBYId;
                comand.CommandTimeout = Constants.TimeOut;
                //comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                comand.Parameters.Add("inIdLibelle", MySqlDbType.Int32);
                // comand.Parameters["inIdsite"].Value = idSite;
                comand.Parameters["inIdLibelle"].Value = idLibelle;
                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    compte = new CompteOhada();
                    compte.Id = reader.GetInt32(0);
                    compte.Code = reader.GetString(1);
                    compte.Libelle = reader.GetString(2);
                    compte.IdLibelleType = reader.GetInt32(3);
                    comptes.Add(compte);
                }
            }

            return comptes;
        }


       /// <summary>
        /// retournre tous les libelle type de la table t_compteOhadaLibelleType
       /// </summary>
       /// <returns></returns>
        public List<ComptaOhadaLibelle> SelectLibelleAll()
        {
            List<ComptaOhadaLibelle> comptes = new List<ComptaOhadaLibelle>();
            ComptaOhadaLibelle compte = null;
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_LIBELLE_SELECT;
                comand.CommandTimeout = Constants.TimeOut;
                //comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
               // comand.Parameters.Add("inId", MySqlDbType.Int32);
                // comand.Parameters["inIdsite"].Value = idSite;
               // comand.Parameters["inId"].Value = 0;
                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    compte = new ComptaOhadaLibelle();
                    compte.ID = reader.GetInt32(0);
                    compte.libelle = reader.GetString(1);
                    comptes.Add(compte);
                }
            }

            return comptes;
        }


       /// <summary>
       /// retourne uniquement les libelle comptes deja associes à la table compteOhada
       /// </summary>
       /// <returns></returns>
        public List<ComptaOhadaLibelle> SelectLibelleAllByCompteOhada()
        {
            List<ComptaOhadaLibelle> comptes = new List<ComptaOhadaLibelle>();
            ComptaOhadaLibelle compte = null;
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_LIBELLE_SELECTBY;
                comand.CommandTimeout = Constants.TimeOut;
                //comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                // comand.Parameters.Add("inId", MySqlDbType.Int32);
                // comand.Parameters["inIdsite"].Value = idSite;
                // comand.Parameters["inId"].Value = 0;
                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    compte = new ComptaOhadaLibelle();
                    compte.ID = reader.GetInt32(0);
                    compte.libelle = reader.GetString(1);
                    comptes.Add(compte);
                }
            }

            return comptes;
        }



        public bool Insert(CompteOhada compte, int idSite)
        {
            string valString = FDataBase.GetConnection(ConnectionString);

            using (MySqlConnection con = new MySqlConnection(valString))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_INSERT;
                comand.CommandTimeout = Constants.TimeOut;

                comand.Parameters.Add("inLibelle", MySqlDbType.VarChar, 255);
                comand.Parameters.Add("inCode", MySqlDbType.VarChar, 255);
                comand.Parameters.Add("inType", MySqlDbType.Int32);

                comand.Parameters["inLibelle"].Value = compte.Libelle;
                comand.Parameters["inCode"].Value = compte.Code;
                comand.Parameters["inType"].Value = compte.IdLibelleType;

                comand.Connection = con;
                con.Open();
                comand.ExecuteNonQuery();
            }
            return true;
        }



        public bool LibelleType_Add(ComptaOhadaLibelle compte)
        {
            string valString = FDataBase.GetConnection(ConnectionString);

            using (MySqlConnection con = new MySqlConnection(valString))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_LIBELLE_ADD;
                comand.CommandTimeout = Constants.TimeOut;
             
                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters.Add("inLibelle", MySqlDbType.VarChar, 255);
                comand.Parameters["inId"].Value = compte.ID;
                comand.Parameters["inLibelle"].Value = compte.libelle;
             
                comand.Connection = con;
                con.Open();
                comand.ExecuteNonQuery();
            }
            return true;
        }

        public bool Update(CompteOhada compte)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_UPDATE;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters.Add("inLibelle", MySqlDbType.VarChar, 255);
                comand.Parameters.Add("inCode", MySqlDbType.VarChar, 255);
                comand.Parameters.Add("inType", MySqlDbType.Int32);
                comand.Parameters["inId"].Value = compte.Id;
                comand.Parameters["inLibelle"].Value = compte.Libelle;
                comand.Parameters["inCode"].Value = compte.Code;
                comand.Parameters["inType"].Value = compte.IdLibelleType;

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
                comand.Parameters.Add("inID", MySqlDbType.Int32);
                comand.Parameters["inID"].Value = idCompte;
                comand.Connection = con;
                con.Open();
                comand.ExecuteNonQuery();
            }

            return true;
        }

        public bool LibelleType_Delete(int idCompte)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_LIBELLE_DELETE;
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


   public class ComptaOhadaLibelle
   {
       public int ID { get; set; }
       public string  libelle { get; set; }
   }
}
