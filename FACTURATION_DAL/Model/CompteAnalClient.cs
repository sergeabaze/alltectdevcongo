using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace FACTURATION_DAL.Model
{
    public class CompteAnalClient
    {


        private int id;
        private int idClient;
        private int idCompteAnal;
        private List<CompteAnalClient> compteAnalyTiques;
        private string ConnectionString = string.Empty;
        private CompteAnalytique compteAnalytique;

        #region Const
        internal const string PS_DELETE = "PS_CompteAnalytiqueClientDelete";
        internal const string PS_INSERT = "PS_CompteAnalytiqueClientInsert";
        internal const string PS_SELECT = "PS_CompteAnalytiqueClientSelect";
        internal const string PS_UPDATE = "PS_CompteAnalytiqueClientUpdate";

        #endregion

        public CompteAnalClient(string _connectionString)
        {
            compteAnalyTiques = new List<CompteAnalClient>();
            this.ConnectionString = _connectionString;
        }

        public CompteAnalClient(int idClient, string _connectionString)
        {
            this.ConnectionString = _connectionString;
            if (!string.IsNullOrEmpty(ConnectionString))
                compteAnalyTiques = SelectByClientid(idClient);
            else return;
           
        }

        public CompteAnalClient()
        {
            compteAnalyTiques = new List<CompteAnalClient>();
            //this.ConnectionString = _connectionString;
        }

        #region Properties

        public CompteAnalytique CompteAnalytique
        {
            get { return compteAnalytique; }
            set { compteAnalytique = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int IdClient
        {
            get { return idClient; }
            set { idClient = value; }
        }

        public int IdCompteAnal
        {
            get { return idCompteAnal; }
            set { idCompteAnal = value; }
        }
        public List<CompteAnalClient> CompteAnalyTiques
        {
            get { return compteAnalyTiques; }
            set { compteAnalyTiques = value; }
        }

      
        #endregion

        #region Methodes

        public List<CompteAnalClient> SelectByClientid(int idClient)
        {
            CompteAnalClient compte = null;
            List<CompteAnalClient> lists = new List<CompteAnalClient>();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_SELECT;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                comand.Parameters["inIdClient"].Value = idClient;
              
                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    compte = new CompteAnalClient();
                    compte.Id = reader.GetInt32(0);
                    compte.IdClient = reader.GetInt32(1);
                    compte.IdCompteAnal = reader.GetInt32(2);
                    compte.CompteAnalytique=new CompteAnalytique(FDataBase.GetConnection(ConnectionString)).SelectByid(compte.IdCompteAnal);
                    lists.Add(compte);
                }
            }
            return lists;
        }

        public List<CompteAnalClient> SelectAll(int idSite)
        {
            return null;
        }

        public bool Insert(CompteAnalClient compte)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_INSERT;
                comand.CommandTimeout = Constants.TimeOut;

                comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                comand.Parameters.Add("inIdcompteAnal", MySqlDbType.Int32);
                comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                comand.Parameters["inIdClient"].Value = compte.IdClient;
                comand.Parameters["inIdcompteAnal"].Value = compte.IdCompteAnal;
                comand.Parameters["inIdSite"].Value =0;

                comand.Connection = con;
                con.Open();
                comand.ExecuteNonQuery();
            }
            return true;
        }

        public bool Update(CompteAnalClient compte)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_UPDATE;
                comand.CommandTimeout = Constants.TimeOut;

                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                comand.Parameters.Add("inIdcompteAnal", MySqlDbType.Int32);
                comand.Parameters.Add("inIdSite", MySqlDbType.Int32);

                comand.Parameters["inId"].Value = compte.Id;
                comand.Parameters["inIdClient"].Value = compte.IdClient;
                comand.Parameters["inIdcompteAnal"].Value = compte.IdCompteAnal;
                comand.Parameters["inIdSite"].Value = 0;

                comand.Connection = con;
                con.Open();
                comand.ExecuteNonQuery();
            }
            return true;
        }

        public bool Delete(int id)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = PS_DELETE;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters["inId"].Value = id;
                comand.Connection = con;
                con.Open();
                comand.ExecuteNonQuery();
            }

            return true;
        }

        #endregion
    }
}
