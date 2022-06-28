using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FACTURATION_DAL.Model;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;


namespace FACTURATION_DAL
{
    public class Facturation : IFacturation
    {
        private string ConnectionString = string.Empty;


        public Facturation(string _connectionString)
        {
            this.ConnectionString = _connectionString;
        }

        #region UTILISATEUR

        /// <summary>
        /// fonction retournant la liste des utilisateurs
        /// </summary>
        /// <returns></returns>
        public List<Utilisateur> GetAllUtilisateur(bool modeUser)
        {
            List<Utilisateur> listes = new List<Utilisateur>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.GetAllUtilisateur;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Bit);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inMode"].Value = modeUser;
                    comand.Parameters["isArchive"].Value = "NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Utilisateur user = new Utilisateur();
                        user.IdUtilisateur = reader.GetInt32(0);
                        user.Nom = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            user.Prenom = reader.GetString(2);
                        else user.Prenom = string.Empty;

                        if (!reader.IsDBNull(3))
                            user.Fonction = reader.GetString(3);
                        else user.Fonction = string.Empty;

                        if (!reader.IsDBNull(4))
                            user.Loggin = reader.GetString(4);
                        else user.Loggin = string.Empty;

                        if (!reader.IsDBNull(5))
                            user.Password = reader.GetString(5);
                        else user.Password = string.Empty;

                        if (!reader.IsDBNull(6))
                            user.DateConnection = reader.GetDateTime(6);
                        if (!reader.IsDBNull(7))
                            user.IdProfil = reader.GetInt32(7);
                        else user.IdProfil = 0;
                        if (!reader.IsDBNull(8))
                            user.Photo = reader.GetString(8);
                        else user.Photo = string.Empty;

                        if (!reader.IsDBNull(9))
                            user.EstVerouiller = reader.GetBoolean(9);
                        else user.EstVerouiller = false;

                        if (!reader.IsDBNull(10))
                            user.IdSite = reader.GetInt32(10);


                        user.Profile = GetUtilisateurProfileById(user.IdProfil);
                        listes.Add(user);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Utilisateur> GetAllUtilisateur()
        {
            List<Utilisateur> listes = new List<Utilisateur>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.GetAllUtilisateur;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Bit);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inMode"].Value = false;
                    comand.Parameters["isArchive"].Value = "NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Utilisateur user = new Utilisateur();
                        user.IdUtilisateur = reader.GetInt32(0);
                        user.Nom = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            user.Prenom = reader.GetString(2);
                        else user.Prenom = string.Empty;

                        if (!reader.IsDBNull(3))
                            user.Fonction = reader.GetString(3);
                        else user.Fonction = string.Empty;

                        if (!reader.IsDBNull(4))
                            user.Loggin = reader.GetString(4);
                        else user.Loggin = string.Empty;

                        if (!reader.IsDBNull(5))
                            user.Password = reader.GetString(5);
                        else user.Password = string.Empty;

                        if (!reader.IsDBNull(6))
                            user.DateConnection = reader.GetDateTime(6);
                        if (!reader.IsDBNull(7))
                            user.IdProfil = reader.GetInt32(7);
                        else user.IdProfil = 0;
                        if (!reader.IsDBNull(8))
                            user.Photo = reader.GetString(8);
                        else user.Photo = string.Empty;

                        if (!reader.IsDBNull(9))
                            user.EstVerouiller = reader.GetBoolean(9);
                        else user.EstVerouiller = false;

                        if (!reader.IsDBNull(10))
                            user.IdSite = reader.GetInt32(10);


                        user.Profile = GetUtilisateurProfileById_Admin(user.IdProfil, user.IdUtilisateur);
                        listes.Add(user);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Utilisateur> GetAllUtilisateur_Archive(bool modeUser)
        {
            List<Utilisateur> listes = new List<Utilisateur>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.GetAllUtilisateur;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Bit);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inMode"].Value = modeUser;
                    comand.Parameters["isArchive"].Value = "OUI";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Utilisateur user = new Utilisateur();
                        user.IdUtilisateur = reader.GetInt32(0);
                        user.Nom = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            user.Prenom = reader.GetString(2);
                        else user.Prenom = string.Empty;

                        if (!reader.IsDBNull(3))
                            user.Fonction = reader.GetString(3);
                        else user.Fonction = string.Empty;

                        if (!reader.IsDBNull(4))
                            user.Loggin = reader.GetString(4);
                        else user.Loggin = string.Empty;

                        if (!reader.IsDBNull(5))
                            user.Password = reader.GetString(5);
                        else user.Password = string.Empty;

                        if (!reader.IsDBNull(6))
                            user.DateConnection = reader.GetDateTime(6);
                        if (!reader.IsDBNull(7))
                            user.IdProfil = reader.GetInt32(7);
                        else user.IdProfil = 0;
                        if (!reader.IsDBNull(8))
                            user.Photo = reader.GetString(8);
                        else user.Photo = string.Empty;

                        if (!reader.IsDBNull(9))
                            user.EstVerouiller = reader.GetBoolean(9);
                        else user.EstVerouiller = false;

                        if (!reader.IsDBNull(10))
                            user.IdSite = reader.GetInt32(10);


                        user.Profile = GetUtilisateurProfileById(user.IdProfil);
                        listes.Add(user);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Utilisateur GetUtilisateur_ById(int id)
        {
            Utilisateur user = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.GetAllUtilisateur;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Bit);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar,5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inMode"].Value = false;
                    comand.Parameters["isArchive"].Value ="NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        user = new Utilisateur();
                        user.IdUtilisateur = reader.GetInt32(0);
                        user.Nom = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            user.Prenom = reader.GetString(2);
                        else user.Prenom = string.Empty;

                        if (!reader.IsDBNull(3))
                            user.Fonction = reader.GetString(3);
                        else user.Fonction = string.Empty;

                        if (!reader.IsDBNull(4))
                            user.Loggin = reader.GetString(4);
                        else user.Loggin = string.Empty;

                        if (!reader.IsDBNull(5))
                            user.Password = reader.GetString(5);
                        else user.Password = string.Empty;

                        if (!reader.IsDBNull(6))
                            user.DateConnection = reader.GetDateTime(6);
                        if (!reader.IsDBNull(7))
                            user.IdProfil = reader.GetInt32(7);
                        else user.IdProfil = 0;

                        if (!reader.IsDBNull(8))
                            user.Photo = reader.GetString(8);
                        else user.Photo = string.Empty;
                        if (!reader.IsDBNull(9))
                            user.EstVerouiller = reader.GetBoolean(9);
                        else user.EstVerouiller = false;

                        if (!reader.IsDBNull(10))
                            user.IdSite = reader.GetInt32(10);

                        user.Profile = GetUtilisateurProfileById(user.IdProfil);
                        break;
                    }


                }


                return user;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Utilisateur GetUtilisateur_Archive_ById(int id)
        {
            Utilisateur user = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.GetAllUtilisateur;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Bit);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inMode"].Value = false;
                    comand.Parameters["isArchive"].Value = "OUI";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        user = new Utilisateur();
                        user.IdUtilisateur = reader.GetInt32(0);
                        user.Nom = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            user.Prenom = reader.GetString(2);
                        else user.Prenom = string.Empty;

                        if (!reader.IsDBNull(3))
                            user.Fonction = reader.GetString(3);
                        else user.Fonction = string.Empty;

                        if (!reader.IsDBNull(4))
                            user.Loggin = reader.GetString(4);
                        else user.Loggin = string.Empty;

                        if (!reader.IsDBNull(5))
                            user.Password = reader.GetString(5);
                        else user.Password = string.Empty;

                        if (!reader.IsDBNull(6))
                            user.DateConnection = reader.GetDateTime(6);
                        if (!reader.IsDBNull(7))
                            user.IdProfil = reader.GetInt32(7);
                        else user.IdProfil = 0;

                        if (!reader.IsDBNull(8))
                            user.Photo = reader.GetString(8);
                        else user.Photo = string.Empty;
                        if (!reader.IsDBNull(9))
                            user.EstVerouiller = reader.GetBoolean(9);
                        else user.EstVerouiller = false;

                        if (!reader.IsDBNull(10))
                            user.IdSite = reader.GetInt32(10);

                        user.Profile = GetUtilisateurProfileById(user.IdProfil);
                        break;
                    }


                }


                return user;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// fonction de loggin
        /// </summary>
        /// <param name="loggin"></param>
        /// <param name="motPasse"></param>
        /// <returns></returns>
        public Utilisateur GetUtilisateurLoggin(string loggin, string motPasse)
        {
            Utilisateur user = null;
            try
            {


                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.GetUtilisateurLoggin;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inloggin", MySqlDbType.String);
                    comand.Parameters.Add("inPassword", MySqlDbType.String);

                    comand.Parameters["inloggin"].Value = loggin;
                    comand.Parameters["inPassword"].Value = motPasse;

                    MySqlDataReader reader = comand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        user = new Utilisateur();
                        user.IdUtilisateur = reader.GetInt32(0);
                        user.Nom = reader.GetString(1).ToUpper();
                        if (!reader.IsDBNull(2))
                            user.Prenom = reader.GetString(2).ToUpper();
                        else user.Prenom = string.Empty;

                        if (!reader.IsDBNull(3))
                            user.Fonction = reader.GetString(3).ToUpper();
                        else user.Fonction = string.Empty;

                        if (!reader.IsDBNull(4))
                            user.Loggin = reader.GetString(4);
                        else user.Loggin = string.Empty;

                        if (!reader.IsDBNull(5))
                            user.Password = reader.GetString(5);
                        else user.Password = string.Empty;

                        if (!reader.IsDBNull(6))
                            user.DateConnection = reader.GetDateTime(6);
                        if (!reader.IsDBNull(7))
                            user.IdProfil = reader.GetInt32(7);
                        else user.IdProfil = 0;

                        if (!reader.IsDBNull(8))
                            user.Photo = reader.GetString(8);
                        else user.Photo = string.Empty;

                        if (!reader.IsDBNull(9))
                            user.EstVerouiller = reader.GetBoolean(9);
                        else user.EstVerouiller = false;

                        if (!reader.IsDBNull(10))
                            user.IdSite = reader.GetInt32(10);

                        user.Profile = GetUtilisateurProfileById(user.IdProfil, user.IdUtilisateur);


                        //update Date Mise Jour

                        break;
                    }


                }
                // UtilisateurUpdateLogg(user.IdUtilisateur, FDataBase.GetConnection(ConnectionString));


                return user;
            }
            catch (MySqlException ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// fonction de loggin dun nouvel utilisateur
        /// </summary>
        /// <param name="loggin"></param>
        /// <param name="motPasse"></param>
        /// <param name="nouveauPass"></param>
        /// <returns></returns>
        public Utilisateur GetUtilisateurLoggin_New(string loggin, string motPasse, string nouveauPass)
        {
            Utilisateur user = null;
            try
            {

                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_UTILISATEUR_LOGGIN_NEW;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inloggin", MySqlDbType.String);
                    comand.Parameters.Add("inPassword", MySqlDbType.String);
                    comand.Parameters.Add("inNewPassword", MySqlDbType.String);

                    comand.Parameters["inloggin"].Value = loggin;
                    comand.Parameters["inPassword"].Value = motPasse;
                    comand.Parameters["inNewPassword"].Value = nouveauPass;

                    MySqlDataReader reader = comand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        user = new Utilisateur();
                        user.IdUtilisateur = reader.GetInt32(0);
                        user.Nom = reader.GetString(1).ToUpper();
                        if (!reader.IsDBNull(2))
                            user.Prenom = reader.GetString(2).ToUpper();
                        else user.Prenom = string.Empty;

                        if (!reader.IsDBNull(3))
                            user.Fonction = reader.GetString(3).ToUpper();
                        else user.Fonction = string.Empty;

                        if (!reader.IsDBNull(4))
                            user.Loggin = reader.GetString(4);
                        else user.Loggin = string.Empty;

                        if (!reader.IsDBNull(5))
                            user.Password = reader.GetString(5);
                        else user.Password = string.Empty;

                        if (!reader.IsDBNull(6))
                            user.DateConnection = reader.GetDateTime(6);
                        if (!reader.IsDBNull(7))
                            user.IdProfil = reader.GetInt32(7);
                        else user.IdProfil = 0;

                        if (!reader.IsDBNull(8))
                            user.Photo = reader.GetString(8);
                        else user.Photo = string.Empty;
                        if (!reader.IsDBNull(9))
                            user.EstVerouiller = reader.GetBoolean(9);
                        else user.EstVerouiller = false;

                        if (!reader.IsDBNull(10))
                            user.IdSite = reader.GetInt32(10);

                        user.Profile = GetUtilisateurProfileById(user.IdProfil, user.IdUtilisateur);


                        //update Date Mise Jour

                        break;
                    }


                }
               // if (user!=null )
               // UtilisateurUpdateLogg(user.IdUtilisateur, FDataBase.GetConnection(ConnectionString));


                return user;
            }
            catch (MySqlException ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public List<Profile> GetUtilisateurProfile()
        {
            Profile profile = null;
            List<Profile> profiles = new List<Profile>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PROFILE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = 0;


                    MySqlDataReader reader = comand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        profile = new Profile();
                        profile.idProfile = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            profile.Libelle = reader.GetString(1);
                        profile.ShortName = reader.GetString(2);

                        profile.ListeDroits = GetList_DROITS(profile.idProfile);
                        profiles.Add(profile);


                    }


                }



                return profiles;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public Profile GetUtilisateurProfileById(int idProfile, int iduser)
        {
            Profile profile = null;


            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PROFILE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = idProfile;


                    MySqlDataReader reader = comand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        profile = new Profile();
                        profile.idProfile = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            profile.Libelle = reader.GetString(1);
                        profile.ShortName = reader.GetString(2);
                        profile.ListeDroits = GetList_DROITS(profile.idProfile, iduser);

                        break;
                    }


                }



                return profile;
            }
            catch (MySqlException ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Profile GetUtilisateurProfileById_Admin(int idProfile, int iduser)
        {
            Profile profile = null;


            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PROFILE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = idProfile;


                    MySqlDataReader reader = comand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        profile = new Profile();
                        profile.idProfile = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            profile.Libelle = reader.GetString(1);
                        profile.ShortName = reader.GetString(2);
                        profile.ListeDroits = GetList_DROITS_Admin(profile.idProfile, iduser);

                        break;
                    }


                }



                return profile;
            }
            catch (MySqlException ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public Profile GetUtilisateurProfileById(int idProfile)
        {
            Profile profile = null;


            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PROFILE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = idProfile;


                    MySqlDataReader reader = comand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        profile = new Profile();
                        profile.idProfile = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            profile.Libelle = reader.GetString(1);
                        profile.ShortName = reader.GetString(2);
                        profile.ListeDroits = GetList_DROITS(profile.idProfile, 0);

                        break;
                    }


                }



                return profile;
            }
            catch (MySqlException ex)
            {

                throw new Exception(ex.Message);

            }
        }

        public ProfileDate GetProfileDate(int iduser, int idprofile)
        {
            ProfileDate profile = null;
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Constants.PS_PROFILE_DATE_SELECT;
                cmd.CommandTimeout = Constants.TimeOut;

                cmd.Parameters.Add("inIdUser", MySqlDbType.Int32);
                cmd.Parameters.Add("inIdProfile", MySqlDbType.Int32);
                cmd.Parameters["inIdUser"].Value = iduser;
                cmd.Parameters["inidProfile"].Value = idprofile;

                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        profile = new ProfileDate();
                        profile.Id = reader.GetInt32(0);
                        profile.idUser = reader.GetInt32(1);
                        profile.IdProfile = reader.GetInt32(2);
                        if (!reader.IsDBNull(3))
                            profile.Datedebut = reader.GetDateTime(3);
                        if (!reader.IsDBNull(4))
                            profile.DateFin = reader.GetDateTime(4);


                        break;
                    }


                }
                catch (MySqlException ex)
                {
                    throw new DALException(ex.Message);
                }

            }
            return profile;

        }


        public bool ProfileUpdatedate(int id, int idprofile, int idUser, DateTime? dateDebut, DateTime? dateFin)
        {
            bool isValues = false;

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Constants.PS_PROFILE_ADD_DATE;
                cmd.CommandTimeout = Constants.TimeOut;

                cmd.Parameters.Add("inId", MySqlDbType.Int32);
                cmd.Parameters.Add("inIdUser", MySqlDbType.Int32);
                cmd.Parameters.Add("inidProfile", MySqlDbType.Int32);
                cmd.Parameters.Add("indateDebut", MySqlDbType.DateTime);
                cmd.Parameters.Add("inDatefin", MySqlDbType.DateTime);
                cmd.Parameters["inId"].Value = id;
                cmd.Parameters["indateDebut"].Value = dateDebut;
                cmd.Parameters["inDatefin"].Value = dateFin;
                cmd.Parameters["inIdUser"].Value = idUser;
                cmd.Parameters["inidProfile"].Value = idprofile;
                try
                {
                    cmd.ExecuteNonQuery();
                    isValues = true;
                   
                }
                catch (MySqlException ex)
                {
                    throw new DALException(ex.Message);
                }
            }
            return isValues;

        }


        public List<Droit> GetList_DROITS(int idprofile)
        {
            List<Droit> droits = new List<Droit>();
            List<Droit> droitsSelected = new List<Droit>();
            HashSet<int> IdvuesListe = new HashSet<int>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DROIT_VUES_PROFILE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdprofile", MySqlDbType.Int32);
                    comand.Parameters["inIdprofile"].Value = idprofile;



                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Droit droit = new Droit();
                        droit.ID = reader.GetInt32(0);
                        droit.IProfile = reader.GetInt32(1);
                        droit.IdVues = reader.GetInt32(2);

                        if (!reader.IsDBNull(3))
                            droit.LibelleVue = reader.GetString(3);
                        else droit.LibelleVue = string.Empty;

                        if (!reader.IsDBNull(4))
                            droit.Lecture = reader.GetBoolean(4);
                        else droit.Lecture = false;
                        if (!reader.IsDBNull(5))
                            droit.Ecriture = reader.GetBoolean(5);
                        else droit.Ecriture = false;
                        if (!reader.IsDBNull(6))
                            droit.Suppression = reader.GetBoolean(6);
                        else droit.Suppression = false;
                        if (!reader.IsDBNull(7))
                            droit.Super = reader.GetBoolean(7);
                        else droit.Super = false;

                        if (!reader.IsDBNull(8))
                            droit.Impression = reader.GetBoolean(8);
                        else droit.Impression = false;

                        if (!reader.IsDBNull(9))
                            droit.Edition = reader.GetBoolean(9);
                        else droit.Edition = false;
                        if (!reader.IsDBNull(10))
                            droit.Validation = reader.GetBoolean(10);
                        else droit.Validation = false;

                        if (!reader.IsDBNull(11))
                            droit.Testeur = reader.GetBoolean(11);
                        else droit.Testeur = false;

                        if (!reader.IsDBNull(12))
                            droit.proprietaire = reader.GetBoolean(12);
                        else droit.proprietaire = false;

                        if (!reader.IsDBNull(13))
                            droit.execution = reader.GetBoolean(13);
                        else droit.execution = false;

                        droit.IDutilisateur = 0;

                        if (!reader.IsDBNull(14))
                            droit.developpeur = reader.GetBoolean(14);
                        else droit.developpeur = false;

                        if (!reader.IsDBNull(15))
                            droit.Marge = reader.GetBoolean(15);
                        else droit.Marge = false;

                        if (!reader.IsDBNull(16))
                            droit.ArchiveView = reader.GetBoolean(16);
                        else droit.ArchiveView = false;

                        if (!reader.IsDBNull(17))
                            droit.ArchiveExecute = reader.GetBoolean(17);
                        else droit.ArchiveExecute = false;

                        if (!reader.IsDBNull(18))
                            droit.ImportDb = reader.GetBoolean(18);
                        else droit.ImportDb = false;
                        if (!reader.IsDBNull(19))
                            droit.ExportDB = reader.GetBoolean(19);
                        else droit.ExportDB = false;

                        if (!reader.IsDBNull(20))
                            droit.Signature = reader.GetBoolean(20);
                        else droit.Signature = false;

                        if (!reader.IsDBNull(21))
                            droit.Extraction = reader.GetBoolean(21);
                        else droit.Extraction = false;

                        if (!reader.IsDBNull(22))
                            droit.MasterUser = reader.GetBoolean(22);
                        else droit.MasterUser = false;


                        if (!reader.IsDBNull(23))
                            droit.StatutSuspension = reader.GetBoolean(23);
                        else droit.StatutSuspension = false;

                        if (!reader.IsDBNull(24))
                            droit.StatutSortie = reader.GetBoolean(24);
                        else droit.StatutSortie = false;

                        if (!reader.IsDBNull(25))
                            droit.StatutSuppression = reader.GetBoolean(25);
                        else droit.StatutSuppression = false;

                        if (!reader.IsDBNull(26))
                            droit.Jvpreparation = reader.GetBoolean(26);
                        else droit.Jvpreparation = false;

                        if (!reader.IsDBNull(27))
                            droit.JvSuppression = reader.GetBoolean(27);
                        else droit.JvSuppression = false;

                        if (!reader.IsDBNull(28))
                            droit.JvLecture = reader.GetBoolean(28);
                        else droit.JvLecture = false;

                        if (!reader.IsDBNull(29))
                            droit.JvExport = reader.GetBoolean(29);
                        else droit.JvExport = false;


                     
                        if (!reader.IsDBNull(30))
                            droit.IDSousVue = reader.GetInt32(30);
                        else droit.IDSousVue = 0;

                        if (!reader.IsDBNull(31))
                            droit.LibelleSouVue = reader.GetString(31);
                        else droit.LibelleSouVue = string.Empty;

                        //if (!reader.IsDBNull(26))
                        //    droit.IdUserDroits = reader.GetInt32(26);
                        //else droit.IdUserDroits = 0;



                        //if (!reader.IsDBNull(20))
                        //    droit.IDSousVue = reader.GetInt32(20);
                        //else droit.IDSousVue = 0;

                        //if (!reader.IsDBNull(21))
                        //    droit.LibelleSouVue = reader.GetString(21);
                        //else droit.LibelleSouVue = string.Empty;


                        droits.Add(droit);

                        IdvuesListe.Add(droit.IdVues);
                    }

                    foreach (int id in IdvuesListe)
                    {
                        Droit newD = droits.First(d => d.IdVues == id);
                        List<Droit> listeSousVues = droits.FindAll(sv => sv.IdVues == id && sv.IDSousVue > 0);
                        newD.SousDroits = listeSousVues;
                        droitsSelected.Add(newD);
                    }
                }



                return droitsSelected;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Droit> GetList_DROITS(int idprofile, int idUser)
        {
            List<Droit> droits = new List<Droit>();
            List<Droit> droitsSelected = new List<Droit>();
            HashSet<int> IdvuesListe = new HashSet<int>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DROIT_VUES_USER;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdprofile", MySqlDbType.Int32);
                    comand.Parameters.Add("inIduser", MySqlDbType.Int32);
                    comand.Parameters["inIdprofile"].Value = idprofile;
                    comand.Parameters["inIduser"].Value = idUser;


                    MySqlDataReader reader = comand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        Droit droit = new Droit();
                        droit.ID = reader.GetInt32(0);
                        droit.IProfile = reader.GetInt32(1);
                        droit.IdVues = reader.GetInt32(2);

                        if (!reader.IsDBNull(3))
                            droit.LibelleVue = reader.GetString(3);
                        else droit.LibelleVue = string.Empty;

                        if (!reader.IsDBNull(4))
                            droit.Lecture = reader.GetBoolean(4);
                        else droit.Lecture = false;
                        if (!reader.IsDBNull(5))
                            droit.Ecriture = reader.GetBoolean(5);
                        else droit.Ecriture = false;
                        if (!reader.IsDBNull(6))
                            droit.Suppression = reader.GetBoolean(6);
                        else droit.Suppression = false;
                        if (!reader.IsDBNull(7))
                            droit.Super = reader.GetBoolean(7);
                        else droit.Super = false;

                        if (!reader.IsDBNull(8))
                            droit.Impression = reader.GetBoolean(8);
                        else droit.Impression = false;

                        if (!reader.IsDBNull(9))
                            droit.Edition = reader.GetBoolean(9);
                        else droit.Edition = false;
                        if (!reader.IsDBNull(10))
                            droit.Validation = reader.GetBoolean(10);
                        else droit.Validation = false;

                        if (!reader.IsDBNull(11))
                            droit.Testeur = reader.GetBoolean(11);
                        else droit.Testeur = false;

                        if (!reader.IsDBNull(12))
                            droit.proprietaire = reader.GetBoolean(12);
                        else droit.proprietaire = false;

                        if (!reader.IsDBNull(13))
                            droit.execution = reader.GetBoolean(13);
                        else droit.execution = false;

                        if (!reader.IsDBNull(14))
                            droit.developpeur = reader.GetBoolean(14);
                        else droit.developpeur = false;

                        if (!reader.IsDBNull(15))
                            droit.Marge = reader.GetBoolean(15);
                        else droit.Marge = false;

                        if (!reader.IsDBNull(16))
                            droit.ArchiveView = reader.GetBoolean(16);
                        else droit.ArchiveView = false;

                        if (!reader.IsDBNull(17))
                            droit.ArchiveExecute = reader.GetBoolean(17);
                        else droit.ArchiveExecute = false;

                        if (!reader.IsDBNull(18))
                            droit.ImportDb = reader.GetBoolean(18);
                        else droit.ImportDb = false;
                        if (!reader.IsDBNull(19))
                            droit.ExportDB = reader.GetBoolean(19);
                        else droit.ExportDB = false;

                        if (!reader.IsDBNull(20))
                            droit.Signature = reader.GetBoolean(20);
                        else droit.Signature = false;

                        if (!reader.IsDBNull(21))
                            droit.Extraction = reader.GetBoolean(21);
                        else droit.Extraction = false;

                        if (!reader.IsDBNull(22))
                            droit.MasterUser = reader.GetBoolean(22);
                        else droit.MasterUser = false;
                        //td.StatutSuspension ,td.StatutSortie ,td.StatutSuppression ,td.Jvpreparation ,td.JvSuppression ,td.JvLecture ,td.JvExport
                        if (!reader.IsDBNull(23))
                            droit.StatutSuspension = reader.GetBoolean(23);
                        else droit.StatutSuspension = false;

                        if (!reader.IsDBNull(24))
                            droit.StatutSortie = reader.GetBoolean(24);
                        else droit.StatutSortie = false;

                        if (!reader.IsDBNull(25))
                            droit.StatutSuppression = reader.GetBoolean(25);
                        else droit.StatutSuppression = false;

                        if (!reader.IsDBNull(26))
                            droit.Jvpreparation = reader.GetBoolean(26);
                        else droit.Jvpreparation = false;

                        if (!reader.IsDBNull(27))
                            droit.JvSuppression = reader.GetBoolean(27);
                        else droit.JvSuppression = false;

                        if (!reader.IsDBNull(28))
                            droit.JvLecture = reader.GetBoolean(28);
                        else droit.JvLecture = false;

                        if (!reader.IsDBNull(29))
                            droit.JvExport = reader.GetBoolean(29);
                        else droit.JvExport = false;





                        if (!reader.IsDBNull(30))
                            droit.IDutilisateur = reader.GetInt32(30);
                        else droit.IDutilisateur = 0;

                        if (!reader.IsDBNull(31))
                            droit.IDSousVue = reader.GetInt32(31);
                        else droit.IDSousVue = 0;

                        if (!reader.IsDBNull(32))
                            droit.LibelleSouVue = reader.GetString(32);
                        else droit.LibelleSouVue = string.Empty;

                        if (!reader.IsDBNull(33))
                            droit.IdUserDroits = reader.GetInt32(33);
                        else droit.IdUserDroits = 0;



                        //if (!reader.IsDBNull(20))
                        //    droit.IDutilisateur = reader.GetInt32(20);
                        //else droit.IDutilisateur = 0;

                        //if (!reader.IsDBNull(21))
                        //    droit.IDSousVue = reader.GetInt32(21);
                        //else droit.IDSousVue = 0;

                        //if (!reader.IsDBNull(22))
                        //    droit.LibelleSouVue = reader.GetString(22);
                        //else droit.LibelleSouVue = string.Empty;

                        //if (!reader.IsDBNull(23))
                        //    droit.IdUserDroits = reader.GetInt32(23);
                        //else droit.IdUserDroits = 0;

                        droits.Add(droit);

                        IdvuesListe.Add(droit.IdVues);

                    }
                    string dep = "";

                    foreach (int id in IdvuesListe)
                    {
                        Droit newD = droits.First(d => d.IdVues == id && d.IDutilisateur==0);

                       var testeval=  droits.FindAll(d => d.IdVues == id);

                        if (testeval.Count >1)
                        {
                            var liste=droits.FindAll(d => d.IdVues == id);
                            foreach (Droit dr in liste)
                            {
                                if (!newD.Ecriture) newD.Ecriture = dr.Ecriture;
                                if (!newD.Lecture) newD.Lecture = dr.Lecture;
                                if (!newD.Suppression) newD.Suppression = dr.Suppression;
                                if (!newD.Impression) newD.Impression = dr.Impression;
                                if (!newD.Edition) newD.Edition = dr.Edition;
                                if (!newD.Validation) newD.Validation = dr.Validation;
                                if (!newD.proprietaire) newD.proprietaire = dr.proprietaire;
                                if (!newD.execution) newD.execution = dr.execution;
                                if (!newD.Marge) newD.Marge = dr.Marge;
                                if (!newD.ArchiveView) newD.ArchiveView = dr.ArchiveView;
                                if (!newD.ArchiveExecute) newD.ArchiveExecute = dr.ArchiveExecute;
                                if (!newD.ImportDb) newD.ImportDb = dr.ImportDb;
                                if (!newD.Signature) newD.Signature = dr.Signature;
                                if (!newD.Extraction) newD.Extraction = dr.Extraction;
                                if (!newD.MasterUser) newD.MasterUser = dr.MasterUser;
                                if (!newD.StatutSuspension) newD.StatutSuspension = dr.StatutSuspension;
                                if (!newD.StatutSortie) newD.StatutSortie = dr.StatutSortie;
                                if (!newD.StatutSuppression) newD.StatutSuppression = dr.StatutSuppression;
                                if (!newD.Jvpreparation) newD.Jvpreparation = dr.Jvpreparation;
                                if (!newD.JvSuppression) newD.JvSuppression = dr.JvSuppression;
                                if (!newD.JvLecture) newD.JvLecture = dr.JvLecture;
                                if (!newD.JvExport) newD.JvExport = dr.JvExport;
                               
                            }

                        }
                        List<Droit> listeSousVues = droits.FindAll(sv => sv.IdVues == id && sv.IDSousVue > 0);
                        List<Droit> listeSousVuesSelecte = new List<Droit>();

                        if (listeSousVues != null)
                        {
                            foreach (Droit drs in listeSousVues)
                            {
                                Droit newSD = listeSousVues.First(d => d.IDSousVue == drs.IDSousVue && d.IDutilisateur == 0);
                                var testevalSub = listeSousVues.FindAll(d => d.IDSousVue == drs.IDSousVue);



                                if (testevalSub.Count > 1)
                                {
                                    var liste = listeSousVues.FindAll(d => d.IDSousVue == drs.IDSousVue);
                                    foreach (Droit dr in liste)
                                    {
                                        if (!newSD.Ecriture) newSD.Ecriture = dr.Ecriture;
                                        if (!newSD.Lecture) newSD.Lecture = dr.Lecture;
                                        if (!newSD.Suppression) newSD.Suppression = dr.Suppression;
                                        if (!newSD.Impression) newSD.Impression = dr.Impression;
                                        if (!newSD.Edition) newSD.Edition = dr.Edition;
                                        if (!newSD.Validation) newSD.Validation = dr.Validation;
                                        if (!newSD.proprietaire) newSD.proprietaire = dr.proprietaire;
                                        if (!newSD.execution) newSD.execution = dr.execution;
                                        if (!newSD.Marge) newSD.Marge = dr.Marge;
                                        if (!newSD.ArchiveView) newSD.ArchiveView = dr.ArchiveView;
                                        if (!newSD.ArchiveExecute) newSD.ArchiveExecute = dr.ArchiveExecute;
                                        if (!newSD.ImportDb) newSD.ImportDb = dr.ImportDb;
                                        if (!newSD.Signature) newSD.Signature = dr.Signature;
                                        if (!newSD.Extraction) newSD.Extraction = dr.Extraction;
                                        if (!newSD.MasterUser) newSD.MasterUser = dr.MasterUser;
                                        if (!newSD.StatutSuspension) newSD.StatutSuspension = dr.StatutSuspension;
                                        if (!newSD.StatutSortie) newSD.StatutSortie = dr.StatutSortie;
                                        if (!newSD.StatutSuppression) newSD.StatutSuppression = dr.StatutSuppression;
                                        if (!newSD.Jvpreparation) newSD.Jvpreparation = dr.Jvpreparation;
                                        if (!newSD.JvSuppression) newSD.JvSuppression = dr.JvSuppression;
                                        if (!newSD.JvLecture) newSD.JvLecture = dr.JvLecture;
                                        if (!newSD.JvExport) newSD.JvExport = dr.JvExport;

                                        listeSousVuesSelecte.Add(newSD);

                                    }

                                }
                                else
                                    listeSousVuesSelecte.Add(newSD);

                            }
                           
                       
                        }
                        newD.SousDroits = listeSousVuesSelecte;
                        droitsSelected.Add(newD);
                    }


                }





                return droitsSelected;
            }
            catch (MySqlException ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<Droit> GetList_DROITS_Admin(int idprofile, int idUser)
        {
            List<Droit> droits = new List<Droit>();
            List<Droit> droitsSelected = new List<Droit>();
            HashSet<int> IdvuesListe = new HashSet<int>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DROIT_VUES_USER;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdprofile", MySqlDbType.Int32);
                    comand.Parameters.Add("inIduser", MySqlDbType.Int32);
                    comand.Parameters["inIdprofile"].Value = idprofile;
                    comand.Parameters["inIduser"].Value = idUser;


                    MySqlDataReader reader = comand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        Droit droit = new Droit();
                        droit.ID = reader.GetInt32(0);
                        droit.IProfile = reader.GetInt32(1);
                        droit.IdVues = reader.GetInt32(2);

                        if (!reader.IsDBNull(3))
                            droit.LibelleVue = reader.GetString(3);
                        else droit.LibelleVue = string.Empty;

                        if (!reader.IsDBNull(4))
                            droit.Lecture = reader.GetBoolean(4);
                        else droit.Lecture = false;
                        if (!reader.IsDBNull(5))
                            droit.Ecriture = reader.GetBoolean(5);
                        else droit.Ecriture = false;
                        if (!reader.IsDBNull(6))
                            droit.Suppression = reader.GetBoolean(6);
                        else droit.Suppression = false;
                        if (!reader.IsDBNull(7))
                            droit.Super = reader.GetBoolean(7);
                        else droit.Super = false;

                        if (!reader.IsDBNull(8))
                            droit.Impression = reader.GetBoolean(8);
                        else droit.Impression = false;

                        if (!reader.IsDBNull(9))
                            droit.Edition = reader.GetBoolean(9);
                        else droit.Edition = false;
                        if (!reader.IsDBNull(10))
                            droit.Validation = reader.GetBoolean(10);
                        else droit.Validation = false;

                        if (!reader.IsDBNull(11))
                            droit.Testeur = reader.GetBoolean(11);
                        else droit.Testeur = false;

                        if (!reader.IsDBNull(12))
                            droit.proprietaire = reader.GetBoolean(12);
                        else droit.proprietaire = false;

                        if (!reader.IsDBNull(13))
                            droit.execution = reader.GetBoolean(13);
                        else droit.execution = false;

                        if (!reader.IsDBNull(14))
                            droit.developpeur = reader.GetBoolean(14);
                        else droit.developpeur = false;

                        if (!reader.IsDBNull(15))
                            droit.Marge = reader.GetBoolean(15);
                        else droit.Marge = false;

                        if (!reader.IsDBNull(16))
                            droit.ArchiveView = reader.GetBoolean(16);
                        else droit.ArchiveView = false;

                        if (!reader.IsDBNull(17))
                            droit.ArchiveExecute = reader.GetBoolean(17);
                        else droit.ArchiveExecute = false;

                        if (!reader.IsDBNull(18))
                            droit.ImportDb = reader.GetBoolean(18);
                        else droit.ImportDb = false;
                        if (!reader.IsDBNull(19))
                            droit.ExportDB = reader.GetBoolean(19);
                        else droit.ExportDB = false;

                        if (!reader.IsDBNull(20))
                            droit.Signature = reader.GetBoolean(20);
                        else droit.Signature = false;

                        if (!reader.IsDBNull(21))
                            droit.Extraction = reader.GetBoolean(21);
                        else droit.Extraction = false;

                        if (!reader.IsDBNull(22))
                            droit.MasterUser = reader.GetBoolean(22);
                        else droit.MasterUser = false;
                        //td.StatutSuspension ,td.StatutSortie ,td.StatutSuppression ,td.Jvpreparation ,td.JvSuppression ,td.JvLecture ,td.JvExport
                        if (!reader.IsDBNull(23))
                            droit.StatutSuspension = reader.GetBoolean(23);
                        else droit.StatutSuspension = false;

                        if (!reader.IsDBNull(24))
                            droit.StatutSortie = reader.GetBoolean(24);
                        else droit.StatutSortie = false;

                        if (!reader.IsDBNull(25))
                            droit.StatutSuppression = reader.GetBoolean(25);
                        else droit.StatutSuppression = false;

                        if (!reader.IsDBNull(26))
                            droit.Jvpreparation = reader.GetBoolean(26);
                        else droit.Jvpreparation = false;

                        if (!reader.IsDBNull(27))
                            droit.JvSuppression = reader.GetBoolean(27);
                        else droit.JvSuppression = false;

                        if (!reader.IsDBNull(28))
                            droit.JvLecture = reader.GetBoolean(28);
                        else droit.JvLecture = false;

                        if (!reader.IsDBNull(29))
                            droit.JvExport = reader.GetBoolean(29);
                        else droit.JvExport = false;





                        if (!reader.IsDBNull(30))
                            droit.IDutilisateur = reader.GetInt32(30);
                        else droit.IDutilisateur = 0;

                        if (!reader.IsDBNull(31))
                            droit.IDSousVue = reader.GetInt32(31);
                        else droit.IDSousVue = 0;

                        if (!reader.IsDBNull(32))
                            droit.LibelleSouVue = reader.GetString(32);
                        else droit.LibelleSouVue = string.Empty;

                        if (!reader.IsDBNull(33))
                            droit.IdUserDroits = reader.GetInt32(33);
                        else droit.IdUserDroits = 0;



                        //if (!reader.IsDBNull(20))
                        //    droit.IDutilisateur = reader.GetInt32(20);
                        //else droit.IDutilisateur = 0;

                        //if (!reader.IsDBNull(21))
                        //    droit.IDSousVue = reader.GetInt32(21);
                        //else droit.IDSousVue = 0;

                        //if (!reader.IsDBNull(22))
                        //    droit.LibelleSouVue = reader.GetString(22);
                        //else droit.LibelleSouVue = string.Empty;

                        //if (!reader.IsDBNull(23))
                        //    droit.IdUserDroits = reader.GetInt32(23);
                        //else droit.IdUserDroits = 0;

                        droits.Add(droit);

                       // IdvuesListe.Add(droit.IdVues);

                    }
                   


                }





                return droits;
            }
            catch (MySqlException ex)
            {

                throw new Exception(ex.Message);
            }
        }


        bool UtilisateurUpdateLogg(int idUser, string connection)
        {
            bool isValues = false;

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Constants.UtilisateurUpdateLogg;
                cmd.CommandTimeout = Constants.TimeOut;
                cmd.Parameters.Add("inId", MySqlDbType.Int32);
                cmd.Parameters["inId"].Value = idUser;
                try
                {
                    cmd.ExecuteNonQuery();
                    isValues = true;

                }
                catch (MySqlException ex)
                {
                    throw new DALException(ex.Message);
                }
            }
            return isValues;

        }

        /// <summary>
        /// fonction de reinitialisation du mot de passe par ladministrateur
        /// </summary>
        /// <param name="idUser"></param>

        /// <param name="defaultpass"></param>
        /// <param name="idprofileAdmin"></param>

        /// <returns></returns>
        public bool Utilisateur_InitPassword(int idUser, string defaultpass, int idprofileAdmin)
        {
            bool isValues = false;

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Constants.PS_UTILISATEUR_INITPASSWORD;
                cmd.CommandTimeout = Constants.TimeOut;
                cmd.Parameters.Add("inIdUser", MySqlDbType.Int32);
                cmd.Parameters.Add("indefPassword", MySqlDbType.VarChar, 255);
                cmd.Parameters.Add("inIdprofile", MySqlDbType.Int32);

                cmd.Parameters["inIdUser"].Value = idUser;
                cmd.Parameters["indefPassword"].Value = defaultpass;
                cmd.Parameters["inIdprofile"].Value = idprofileAdmin;

                try
                {
                    cmd.ExecuteNonQuery();
                    isValues = true;

                }
                catch (MySqlException ex)
                {
                    throw new DALException(ex.Message);
                }
            }
            return isValues;

        }

        /// <summary>
        /// fonction de teste du loggin utilisateur
        /// </summary>
        /// <param name="userloggin"></param>
        /// <returns></returns>
        public bool Utilisateur_LogName(string userloggin)
        {
            bool isValues = false;


            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Constants.PS_UTILISATEUR_LOG_NAME;
                cmd.CommandTimeout = Constants.TimeOut;
                cmd.Parameters.Add("inUserLogin", MySqlDbType.VarChar, 255);
                cmd.Parameters.Add("inresult", MySqlDbType.Bit);
                cmd.Parameters["inresult"].Direction = ParameterDirection.Output;

                cmd.Parameters["inUserLogin"].Value = userloggin;
                cmd.Parameters["inresult"].Value = false;

                try
                {
                    cmd.ExecuteNonQuery();
                    isValues = int.Parse(cmd.Parameters["inresult"].Value.ToString()) == 1 ? true : false;


                }
                catch (MySqlException ex)
                {

                    throw new DALException(ex.Message);

                }
            }
            return isValues;

        }

        /// <summary>
        /// vérifiction si le compte est vérouiller
        /// </summary>
        /// <param name="userloggin"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Utilisateur_IsAcountLock(string userloggin, string password)
        {
            bool isValues = false;

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Constants.PS_UTILISATEUR_COMPTISLOCK;
                cmd.CommandTimeout = Constants.TimeOut;
                cmd.Parameters.Add("inUserLogin", MySqlDbType.VarChar, 255);
                cmd.Parameters.Add("inUserPass", MySqlDbType.VarChar, 255);
                cmd.Parameters.Add("inresult", MySqlDbType.Bit);
                cmd.Parameters["inresult"].Direction = ParameterDirection.Output;

                cmd.Parameters["inUserLogin"].Value = userloggin;
                cmd.Parameters["inUserPass"].Value = password;
                cmd.Parameters["inresult"].Value = false;

                try
                {
                    cmd.ExecuteNonQuery();
                    isValues = int.Parse(cmd.Parameters["inresult"].Value.ToString()) == 1 ? true : false;

                }
                catch (MySqlException ex)
                {
                    throw new DALException(ex.Message);
                }
            }
            return isValues;

        }

        /// <summary>
        /// vérouillage ducompte
        /// </summary>
        /// <param name="idUser">id du compte</param>
        /// <returns></returns>
        public bool Utilisateur_LockAccount(int idUser)
        {
            bool isValues = false;

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Constants.PS_UTILISATEUR_LOCKCOMPTE;
                cmd.CommandTimeout = Constants.TimeOut;
                cmd.Parameters.Add("inIDUSER", MySqlDbType.Int32);
                cmd.Parameters["inIDUSER"].Value = idUser;


                try
                {
                    cmd.ExecuteNonQuery();
                    isValues = true;

                }
                catch (MySqlException ex)
                {
                    throw new DALException(ex.Message);
                }
            }
            return isValues;

        }


        public int UTILISATEURADD(Utilisateur user)
        {
            bool isValues = false;
            int valuesreturn = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.UtilisateurInsert;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inNom", MySqlDbType.String);
                    comand.Parameters.Add("inPrenom", MySqlDbType.String);
                    comand.Parameters.Add("inFonction", MySqlDbType.String);
                    comand.Parameters.Add("inloggin", MySqlDbType.String);
                    comand.Parameters.Add("inPassword", MySqlDbType.String);
                    comand.Parameters.Add("inProfile", MySqlDbType.Int32);
                    comand.Parameters.Add("inPhoto", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIduserOut", MySqlDbType.Int32);
                    comand.Parameters["inIduserOut"].Direction = ParameterDirection.Output;
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = user.IdUtilisateur;

                    comand.Parameters["inNom"].Value = user.Nom != null ? user.Nom.Trim() : null;
                    comand.Parameters["inPrenom"].Value = user.Prenom != null ? user.Prenom.Trim() : null;
                    comand.Parameters["inFonction"].Value = user.Nom != null ? user.Fonction.Trim() : null;
                    comand.Parameters["inloggin"].Value = user.Loggin;
                    comand.Parameters["inPassword"].Value = user.Password;
                    comand.Parameters["inProfile"].Value = user.IdProfil;
                    comand.Parameters["inPhoto"].Value = user.Photo;
                    comand.Parameters["inIduserOut"].Value = 0;
                    comand.Parameters["inIdste"].Value = user.IdSite;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                    valuesreturn = int.Parse(comand.Parameters["inIduserOut"].Value.ToString());

                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return valuesreturn;
        }


        public bool UTILISATEURDELETE(int Iduser)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.UtilisateurDelete;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = Iduser;
                    comand.Connection = con;
                    con.Open();

                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }





        #endregion

        #region ADMINISTRATION UTILISATEUR


        public Dictionary<ClasseVues, List<ClasseVues>> GetListeVues()
        {
            DataTable tblVuesParent = VUES_SELECT(0);
            DataTable tblVuesEnfant = null;
            Dictionary<ClasseVues, List<ClasseVues>> listevues = new Dictionary<ClasseVues, List<ClasseVues>>();

            HashSet<int> parents = new HashSet<int>();

            List<ClasseVues> vuesParents = new List<ClasseVues>();
            List<ClasseVues> vuesEnfants = null;

            ClasseVues parent = null;
            if (tblVuesParent.Rows.Count > 0)
            {
                foreach (DataRow row in tblVuesParent.Rows)
                {
                    parent = new ClasseVues { IdVue = Convert.ToInt32(row["idparent"]), Libelle = Convert.ToString(row["parent"]) };
                    vuesParents.Add(parent);
                    parents.Add(parent.IdVue);
                }

                foreach (int valid in parents)
                {
                    ClasseVues newparent = vuesParents.Find(p => p.IdVue == valid);
                    DataRow[] ListeRows = tblVuesParent.Select(string.Format("idparent='{0}' and idenfant>'{1}'", valid, 0));
                    DataTable newDataTable = tblVuesParent.Clone();
                    vuesEnfants = new List<ClasseVues>();
                    foreach (DataRow row in ListeRows)
                    {
                        ClasseVues enfant = new ClasseVues { IdVue = Convert.ToInt32(row["idenfant"]), Libelle = Convert.ToString(row["enfant"]) };
                        vuesEnfants.Add(enfant);
                    }
                    listevues.Add(newparent, vuesEnfants);



                }

            }

            return listevues;
        }



        DataTable VUES_SELECT(int id)
        {
            DataTable tblVues = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_VUES_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = id;

                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblVues);
                }



                return tblVues;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<Droit> Get_DROITS_USER(int idprofile, int idUser)
        {
            List<Droit> droits = new List<Droit>();
            List<Droit> droitsSelected = new List<Droit>();
            HashSet<int> IdvuesListe = new HashSet<int>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DROIT_VUES_USER;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdprofile", MySqlDbType.Int32);
                    comand.Parameters.Add("inIduser", MySqlDbType.Int32);
                    comand.Parameters["inIdprofile"].Value = idprofile;
                    comand.Parameters["inIduser"].Value = idUser;


                    MySqlDataReader reader = comand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        Droit droit = new Droit();
                        droit.ID = reader.GetInt32(0);
                        droit.IProfile = reader.GetInt32(1);
                        droit.IdVues = reader.GetInt32(2);

                        if (!reader.IsDBNull(3))
                            droit.LibelleVue = reader.GetString(3);
                        else droit.LibelleVue = string.Empty;

                        if (!reader.IsDBNull(4))
                            droit.Lecture = reader.GetBoolean(4);
                        else droit.Lecture = false;
                        if (!reader.IsDBNull(5))
                            droit.Ecriture = reader.GetBoolean(5);
                        else droit.Ecriture = false;
                        if (!reader.IsDBNull(6))
                            droit.Suppression = reader.GetBoolean(6);
                        else droit.Suppression = false;
                        if (!reader.IsDBNull(7))
                            droit.Super = reader.GetBoolean(7);
                        else droit.Super = false;

                        if (!reader.IsDBNull(8))
                            droit.Impression = reader.GetBoolean(8);
                        else droit.Impression = false;

                        if (!reader.IsDBNull(9))
                            droit.Edition = reader.GetBoolean(9);
                        else droit.Edition = false;
                        if (!reader.IsDBNull(10))
                            droit.Validation = reader.GetBoolean(10);
                        else droit.Validation = false;

                        if (!reader.IsDBNull(11))
                            droit.Testeur = reader.GetBoolean(11);
                        else droit.Testeur = false;

                        if (!reader.IsDBNull(12))
                            droit.proprietaire = reader.GetBoolean(12);
                        else droit.proprietaire = false;

                        if (!reader.IsDBNull(13))
                            droit.execution = reader.GetBoolean(13);
                        else droit.execution = false;

                        if (!reader.IsDBNull(14))
                            droit.developpeur = reader.GetBoolean(14);
                        else droit.execution = false;


                        if (!reader.IsDBNull(15))
                            droit.Marge = reader.GetBoolean(15);
                        else droit.Marge = false;

                        if (!reader.IsDBNull(16))
                            droit.ArchiveView = reader.GetBoolean(16);
                        else droit.ArchiveView = false;

                        if (!reader.IsDBNull(17))
                            droit.ArchiveExecute = reader.GetBoolean(17);
                        else droit.ArchiveExecute = false;

                        if (!reader.IsDBNull(18))
                            droit.ImportDb = reader.GetBoolean(18);
                        else droit.ImportDb = false;
                        if (!reader.IsDBNull(19))
                            droit.ExportDB = reader.GetBoolean(19);
                        else droit.ExportDB = false;

                        if (!reader.IsDBNull(19))
                            droit.ExportDB = reader.GetBoolean(19);
                        else droit.ExportDB = false;

                        if (!reader.IsDBNull(20))
                            droit.Signature = reader.GetBoolean(20);
                        else droit.Signature = false;

                        if (!reader.IsDBNull(21))
                            droit.Extraction = reader.GetBoolean(21);
                        else droit.Extraction = false;

                        if (!reader.IsDBNull(22))
                            droit.MasterUser = reader.GetBoolean(22);
                        else droit.MasterUser = false;

                        //new

                        if (!reader.IsDBNull(23))
                            droit.StatutSuspension = reader.GetBoolean(23);
                        else droit.StatutSuspension = false;

                        if (!reader.IsDBNull(24))
                            droit.StatutSortie = reader.GetBoolean(24);
                        else droit.StatutSortie = false;

                        if (!reader.IsDBNull(25))
                            droit.StatutSuppression = reader.GetBoolean(25);
                        else droit.StatutSuppression = false;

                        if (!reader.IsDBNull(26))
                            droit.Jvpreparation = reader.GetBoolean(26);
                        else droit.Jvpreparation = false;

                        if (!reader.IsDBNull(27))
                            droit.JvSuppression = reader.GetBoolean(27);
                        else droit.JvSuppression = false;

                        if (!reader.IsDBNull(28))
                            droit.JvLecture = reader.GetBoolean(28);
                        else droit.JvLecture = false;

                        if (!reader.IsDBNull(29))
                            droit.JvExport = reader.GetBoolean(29);
                        else droit.JvExport = false;





                        if (!reader.IsDBNull(30))
                            droit.IDutilisateur = reader.GetInt32(30);
                        else droit.IDutilisateur = 0;

                        if (!reader.IsDBNull(31))
                            droit.IDSousVue = reader.GetInt32(31);
                        else droit.IDSousVue = 0;

                        if (!reader.IsDBNull(32))
                            droit.LibelleSouVue = reader.GetString(32);
                        else droit.LibelleSouVue = string.Empty;

                        if (!reader.IsDBNull(33))
                            droit.IdUserDroits = reader.GetInt32(33);
                        else droit.IdUserDroits = 0;



                        //if (!reader.IsDBNull(23))
                        //    droit.IDutilisateur = reader.GetInt32(23);
                        //else droit.IDutilisateur = 0;

                        //if (!reader.IsDBNull(24))
                        //    droit.IDSousVue = reader.GetInt32(24);
                        //else droit.IDSousVue = 0;

                        //if (!reader.IsDBNull(25))
                        //    droit.LibelleSouVue = reader.GetString(25);
                        //else droit.LibelleSouVue = string.Empty;

                        //if (!reader.IsDBNull(26))
                        //    droit.IdUserDroits = reader.GetInt32(26);
                        //else droit.IdUserDroits = 0;

                        droits.Add(droit);
                        IdvuesListe.Add(droit.IdVues);

                    }

                    foreach (int id in IdvuesListe)
                    {
                        Droit newD = droits.First(d => d.IdVues == id);
                        List<Droit> listeSousVues = droits.FindAll(sv => sv.IdVues == id && sv.IDSousVue > 0);
                        newD.SousDroits = listeSousVues;
                        droitsSelected.Add(newD);
                    }
                }



                return droitsSelected;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Droit> Get_DROITS_PROFILE(int idprofile)
        {
            List<Droit> droits = new List<Droit>();
            List<Droit> droitsSelected = new List<Droit>();
            HashSet<int> IdvuesListe = new HashSet<int>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DROIT_VUES_PROFILE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdprofile", MySqlDbType.Int32);
                    comand.Parameters["inIdprofile"].Value = idprofile;



                    MySqlDataReader reader = comand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        Droit droit = new Droit();
                        droit.ID = reader.GetInt32(0);
                        droit.IProfile = reader.GetInt32(1);
                        droit.IdVues = reader.GetInt32(2);
                        if (!reader.IsDBNull(3))
                            droit.LibelleVue = reader.GetString(3);
                        else droit.LibelleVue = string.Empty;

                        if (!reader.IsDBNull(4))
                            droit.Lecture = reader.GetBoolean(4);
                        else droit.Lecture = false;
                        if (!reader.IsDBNull(5))
                            droit.Ecriture = reader.GetBoolean(5);
                        else droit.Ecriture = false;
                        if (!reader.IsDBNull(6))
                            droit.Suppression = reader.GetBoolean(6);
                        else droit.Suppression = false;
                        if (!reader.IsDBNull(7))
                            droit.Super = reader.GetBoolean(7);
                        else droit.Super = false;

                        if (!reader.IsDBNull(8))
                            droit.Impression = reader.GetBoolean(8);
                        else droit.Impression = false;

                        if (!reader.IsDBNull(9))
                            droit.Edition = reader.GetBoolean(9);
                        else droit.Edition = false;
                        if (!reader.IsDBNull(10))
                            droit.Validation = reader.GetBoolean(10);
                        else droit.Validation = false;

                        if (!reader.IsDBNull(11))
                            droit.Testeur = reader.GetBoolean(11);
                        else droit.Testeur = false;

                        if (!reader.IsDBNull(12))
                            droit.proprietaire = reader.GetBoolean(12);
                        else droit.proprietaire = false;

                        if (!reader.IsDBNull(13))
                            droit.execution = reader.GetBoolean(13);
                        else droit.execution = false;

                        if (!reader.IsDBNull(14))
                            droit.developpeur = reader.GetBoolean(14);
                        else droit.developpeur = false;


                        if (!reader.IsDBNull(15))
                            droit.Marge = reader.GetBoolean(15);
                        else droit.Marge = false;

                        if (!reader.IsDBNull(16))
                            droit.ArchiveView = reader.GetBoolean(16);
                        else droit.ArchiveView = false;

                        if (!reader.IsDBNull(17))
                            droit.ArchiveExecute = reader.GetBoolean(17);
                        else droit.ArchiveExecute = false;

                        if (!reader.IsDBNull(18))
                            droit.ImportDb = reader.GetBoolean(18);
                        else droit.ImportDb = false;
                        if (!reader.IsDBNull(19))
                            droit.ExportDB = reader.GetBoolean(19);
                        else droit.ExportDB = false;

                        if (!reader.IsDBNull(20))
                            droit.Signature = reader.GetBoolean(20);
                        else droit.Signature = false;

                        if (!reader.IsDBNull(21))
                            droit.Extraction = reader.GetBoolean(21);
                        else droit.Extraction = false;

                        if (!reader.IsDBNull(22))
                            droit.MasterUser = reader.GetBoolean(22);
                        else droit.MasterUser = false;


                        //if (!reader.IsDBNull(23))
                        //    droit.IDutilisateur = reader.GetInt32(23);
                        //else droit.IDutilisateur = 0;

                        if (!reader.IsDBNull(23))
                            droit.StatutSuspension = reader.GetBoolean(23);
                        else droit.StatutSuspension = false;

                        if (!reader.IsDBNull(24))
                            droit.StatutSortie = reader.GetBoolean(24);
                        else droit.StatutSortie = false;

                        if (!reader.IsDBNull(25))
                            droit.StatutSuppression = reader.GetBoolean(25);
                        else droit.StatutSuppression = false;

                        if (!reader.IsDBNull(26))
                            droit.Jvpreparation = reader.GetBoolean(26);
                        else droit.Jvpreparation = false;

                        if (!reader.IsDBNull(27))
                            droit.JvSuppression = reader.GetBoolean(27);
                        else droit.JvSuppression = false;

                        if (!reader.IsDBNull(28))
                            droit.JvLecture = reader.GetBoolean(28);
                        else droit.JvLecture = false;

                        if (!reader.IsDBNull(29))
                            droit.JvExport = reader.GetBoolean(29);
                        else droit.JvExport = false;


                        if (!reader.IsDBNull(30))
                            droit.IDSousVue = reader.GetInt32(30);
                        else droit.IDSousVue = 0;

                        if (!reader.IsDBNull(31))
                            droit.LibelleSouVue = reader.GetString(31);
                        else droit.LibelleSouVue = string.Empty;

                     
                        droits.Add(droit);
                        IdvuesListe.Add(droit.IdVues);

                    }
                    foreach (int id in IdvuesListe)
                    {
                        Droit newD = droits.First(d => d.IdVues == id);
                        List<Droit> listeSousVues = droits.FindAll(sv => sv.IdVues == id && sv.IDSousVue > 0);
                        newD.SousDroits = listeSousVues;
                        droitsSelected.Add(newD);
                    }

                }



                return droitsSelected;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DROITS_USER_ADD(Droit droit)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DROIT_USER_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inID", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDuser", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDVues", MySqlDbType.Int32);
                    comand.Parameters.Add("inlecture", MySqlDbType.Bit);
                    comand.Parameters.Add("inEcriture", MySqlDbType.Bit);
                    comand.Parameters.Add("inSupp", MySqlDbType.Bit);
                    comand.Parameters.Add("inSuper", MySqlDbType.Bit);
                    comand.Parameters.Add("inprint", MySqlDbType.Bit);
                    comand.Parameters.Add("inedition", MySqlDbType.Bit);
                    comand.Parameters.Add("inValid", MySqlDbType.Bit);
                    comand.Parameters.Add("inTest", MySqlDbType.Bit);
                    comand.Parameters.Add("inprop", MySqlDbType.Bit);
                    comand.Parameters.Add("inpExec", MySqlDbType.Bit);
                    comand.Parameters.Add("inMarge", MySqlDbType.Bit);
                    comand.Parameters.Add("inArchiveView", MySqlDbType.Bit);
                    comand.Parameters.Add("inArchiveExecute", MySqlDbType.Bit);
                    comand.Parameters.Add("inImports", MySqlDbType.Bit);
                    comand.Parameters.Add("inExport", MySqlDbType.Bit);
                    comand.Parameters.Add("inDeveloppeur", MySqlDbType.Bit);
                    comand.Parameters.Add("inSignature", MySqlDbType.Bit);
                    comand.Parameters.Add("inExtraction", MySqlDbType.Bit);
                    comand.Parameters.Add("inStatutSuspension", MySqlDbType.Bit);
                    comand.Parameters.Add("inStatutSortie", MySqlDbType.Bit);
                    comand.Parameters.Add("inStatutSuppression", MySqlDbType.Bit);
                    comand.Parameters.Add("inJvpreparation", MySqlDbType.Bit);
                    comand.Parameters.Add("inJvSuppression", MySqlDbType.Bit);
                    comand.Parameters.Add("inJvLecture", MySqlDbType.Bit);
                    comand.Parameters.Add("inJvExport", MySqlDbType.Bit);
                    comand.Parameters.Add("inIdSousVue", MySqlDbType.Int32);

                    //
                    comand.Parameters["inID"].Value = droit.ID;
                    comand.Parameters["inIDuser"].Value = droit.IDutilisateur;
                    comand.Parameters["inIDVues"].Value = droit.IdVues;
                    comand.Parameters["inlecture"].Value = droit.Lecture;
                    comand.Parameters["inEcriture"].Value = droit.Ecriture;
                    comand.Parameters["inSupp"].Value = droit.Suppression;
                    comand.Parameters["inSuper"].Value = droit.Super;
                    comand.Parameters["inprint"].Value = droit.Impression;
                    comand.Parameters["inedition"].Value = droit.Edition;
                    comand.Parameters["inValid"].Value = droit.Validation;
                    comand.Parameters["inTest"].Value = droit.Testeur;
                    comand.Parameters["inprop"].Value = droit.proprietaire;
                    comand.Parameters["inpExec"].Value = droit.execution;
                    comand.Parameters["inMarge"].Value = droit.Marge;
                    comand.Parameters["inArchiveView"].Value = droit.ArchiveView;
                    comand.Parameters["inArchiveExecute"].Value = droit.ArchiveExecute;
                    comand.Parameters["inImports"].Value = droit.ImportDb;
                    comand.Parameters["inExport"].Value = droit.ExportDB;
                    comand.Parameters["inDeveloppeur"].Value = droit.developpeur;
                    comand.Parameters["inSignature"].Value = droit.Signature;
                    comand.Parameters["inExtraction"].Value = droit.Extraction;

                    comand.Parameters["inStatutSuspension"].Value = droit.StatutSuspension;
                    comand.Parameters["inStatutSortie"].Value = droit.StatutSortie;
                    comand.Parameters["inStatutSuppression"].Value = droit.StatutSuppression;
                    comand.Parameters["inJvpreparation"].Value = droit.Jvpreparation;
                    comand.Parameters["inJvSuppression"].Value = droit.JvSuppression;
                    comand.Parameters["inJvLecture"].Value = droit.JvLecture;
                    comand.Parameters["inJvExport"].Value = droit.JvExport;
                    comand.Parameters["inIdSousVue"].Value = droit.IDSousVue;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();

                    isValues = true;

                }
                return isValues;

            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool DROITS_PROFILS_ADD(Droit droit)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DROIT_PROFILE_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inID", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDprofile", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDVues", MySqlDbType.Int32);
                    comand.Parameters.Add("inlecture", MySqlDbType.Bit);
                    comand.Parameters.Add("inEcriture", MySqlDbType.Bit);
                    comand.Parameters.Add("inSupp", MySqlDbType.Bit);
                    comand.Parameters.Add("inSuper", MySqlDbType.Bit);
                    comand.Parameters.Add("inprint", MySqlDbType.Bit);
                    comand.Parameters.Add("inedition", MySqlDbType.Bit);
                    comand.Parameters.Add("inValid", MySqlDbType.Bit);
                    comand.Parameters.Add("inTest", MySqlDbType.Bit);
                    comand.Parameters.Add("inprop", MySqlDbType.Bit);
                    comand.Parameters.Add("inpExec", MySqlDbType.Bit);
                    comand.Parameters.Add("inMarge", MySqlDbType.Bit);
                    comand.Parameters.Add("inArchiveView", MySqlDbType.Bit);
                    comand.Parameters.Add("inArchiveExecute", MySqlDbType.Bit);
                    comand.Parameters.Add("inImports", MySqlDbType.Bit);
                    comand.Parameters.Add("inExport", MySqlDbType.Bit);
                    comand.Parameters.Add("inDeveloppeur", MySqlDbType.Bit);
                    comand.Parameters.Add("inSignature", MySqlDbType.Bit);
                    comand.Parameters.Add("inExtraction", MySqlDbType.Bit);

                    comand.Parameters.Add("inStatutSuspension", MySqlDbType.Bit);
                    comand.Parameters.Add("inStatutSortie", MySqlDbType.Bit);
                    comand.Parameters.Add("inStatutSuppression", MySqlDbType.Bit);
                    comand.Parameters.Add("inJvpreparation", MySqlDbType.Bit);
                    comand.Parameters.Add("inJvSuppression", MySqlDbType.Bit);
                    comand.Parameters.Add("inJvLecture", MySqlDbType.Bit);
                    comand.Parameters.Add("inJvExport", MySqlDbType.Bit);
                    comand.Parameters.Add("inIdSousVue", MySqlDbType.Int32);


                    comand.Parameters["inID"].Value = droit.ID;
                    comand.Parameters["inIDprofile"].Value = droit.IProfile;
                    comand.Parameters["inIDVues"].Value = droit.IdVues;
                    comand.Parameters["inlecture"].Value = droit.Lecture;
                    comand.Parameters["inEcriture"].Value = droit.Ecriture;
                    comand.Parameters["inSupp"].Value = droit.Suppression;
                    comand.Parameters["inSuper"].Value = droit.Super;
                    comand.Parameters["inprint"].Value = droit.Impression;
                    comand.Parameters["inedition"].Value = droit.Edition;
                    comand.Parameters["inValid"].Value = droit.Validation;
                    comand.Parameters["inTest"].Value = droit.Testeur;
                    comand.Parameters["inprop"].Value = droit.proprietaire;
                    comand.Parameters["inpExec"].Value = droit.execution;
                    comand.Parameters["inMarge"].Value = droit.Marge;
                    comand.Parameters["inArchiveView"].Value = droit.ArchiveView;
                    comand.Parameters["inArchiveExecute"].Value = droit.ArchiveExecute;
                    comand.Parameters["inImports"].Value = droit.ImportDb;
                    comand.Parameters["inExport"].Value = droit.ExportDB;
                    comand.Parameters["inDeveloppeur"].Value = droit.developpeur;
                    comand.Parameters["inSignature"].Value = droit.Signature;
                    comand.Parameters["inExtraction"].Value = droit.Extraction;

                    comand.Parameters["inStatutSuspension"].Value = droit.StatutSuspension;
                    comand.Parameters["inStatutSortie"].Value = droit.StatutSortie;
                    comand.Parameters["inStatutSuppression"].Value = droit.StatutSuppression;
                    comand.Parameters["inJvpreparation"].Value = droit.Jvpreparation;
                    comand.Parameters["inJvSuppression"].Value = droit.JvSuppression;
                    comand.Parameters["inJvLecture"].Value = droit.JvLecture;
                    comand.Parameters["inJvExport"].Value = droit.JvExport;
                    comand.Parameters["inIdSousVue"].Value = droit.IDSousVue;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();

                    isValues = true;
                    
                }
                return isValues;

            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool DROITS_DELETE(int Id, int idprofile, int iduser)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DROIT_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inID", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDProfile", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDUser", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = Id;
                    comand.Parameters["inIDProfile"].Value = idprofile;
                    comand.Parameters["inIDUser"].Value = iduser;
                    comand.Connection = con;
                    con.Open();

                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }



        #endregion

        #region LANGUAGE

        public List<Langue> GetALLLangue()
        {
            Langue langue = null;
            List<Langue> langues = new List<Langue>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.GetLanguage;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = 0;


                    MySqlDataReader reader = comand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        langue = new Langue();
                        langue.IdLangue = reader.GetInt32(0);
                        langue.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            langue.Shorname = reader.GetString(2);
                        else langue.Shorname = string.Empty;
                        langues.Add(langue);
                    }


                }


                return langues;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Langue GetLANGUEBY_ID(int id)
        {
            Langue langue = null;

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.GetLanguage;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = id;


                    MySqlDataReader reader = comand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        langue = new Langue();
                        langue.IdLangue = reader.GetInt32(0);
                        langue.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            langue.Shorname = reader.GetString(2);
                        else langue.Shorname = string.Empty;
                        break;
                    }


                }


                return langue;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool language_ADD(Langue langue)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LANGUAGE_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inLabelle", MySqlDbType.String);
                    comand.Parameters.Add("inShortName", MySqlDbType.String);


                    comand.Parameters["inId"].Value = langue.IdLangue;
                    comand.Parameters["inLabelle"].Value = langue.Libelle;
                    comand.Parameters["inShortName"].Value = langue.Shorname;


                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool language_DELETE(Int32 id)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LANGUAGE_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = id;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        #endregion

        #region CLIENT

        public List<Client> GetALL_CLIENT_NOT_MONTHLYCREATE(Int32 idsite)
        {
            List<Client> listes = new List<Client>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_PRODUIT_BYCLIENT_PARAMLIST";
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inIdSite", MySqlDbType.VarChar, 255);

                    comand.Parameters["inIdSite"].Value = idsite;
                   
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Client client = new Client();
                        client.IdClient = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            client.BoitePostal = reader.GetString(1);
                        else client.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(2))
                            client.Ville = reader.GetString(2);
                        else client.Ville = string.Empty;

                        if (!reader.IsDBNull(3))
                            client.NumeroContribuable = reader.GetString(3);
                        else client.NumeroContribuable = string.Empty;

                        if (!reader.IsDBNull(5))
                            client.NomClient = reader.GetString(5);
                        else client.NomClient = string.Empty;

                        if (!reader.IsDBNull(3))
                            client.Rue1 = reader.GetString(3);
                        else client.Rue1 = string.Empty;

                        if (!reader.IsDBNull(4))
                            client.Rue2 = reader.GetString(4);
                        else client.Rue2 = string.Empty;



                        if (!reader.IsDBNull(7))
                            client.IdLangue = reader.GetInt16(7);
                        else client.IdLangue = 0;

                        if (!reader.IsDBNull(8))
                            client.Ville = reader.GetString(8);
                        if (!reader.IsDBNull(9))
                            client.TermeNombre = reader.GetInt16(9);


                        if (!reader.IsDBNull(10))
                            client.TermeDescription = reader.GetString(10);
                        else client.TermeDescription = string.Empty;

                        if (!reader.IsDBNull(12))
                            client.Idporata = reader.GetInt32(12);
                        else client.Idporata = 0;

                        if (!reader.IsDBNull(11))
                            client.DateEcheance = reader.GetString(11);
                        else client.DateEcheance = string.Empty;

                        if (!reader.IsDBNull(14))
                            client.IdSite = reader.GetInt32(14);
                        else client.IdSite = 0;
                        if (!reader.IsDBNull(15))
                            client.IdExonere = reader.GetInt32(15);
                        else client.IdExonere = 0;

                        if (!reader.IsDBNull(13))
                            client.NumeroImatriculation = reader.GetString(13);
                        else client.NumeroImatriculation = "";

                        if (!reader.IsDBNull(16))
                            client.IdCompte = reader.GetInt32(16);
                        else client.IdCompte = 0;

                        if (!reader.IsDBNull(17))
                            client.IdDeviseFact = reader.GetInt32(17);
                        else client.IdDeviseFact = 0;

                        if (!reader.IsDBNull(25))
                            client.IdTerme = reader.GetInt32(25);
                        else client.IdTerme = 0;

                        if (!reader.IsDBNull(26))
                        client.NumeroImatriculation = reader.GetString(26);
                        //if (!reader.IsDBNull(18))
                        //{
                        //    client.IdCompteTiers = reader.GetInt32(18);
                        //    //   client.CompteTiers = new CompteTiers(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(18));
                        //}
                        //else client.IdCompteTiers = null;
                        //client.IsActive = reader.GetBoolean(19);
                        ////  var compte = new CompteAnalClient(client.IdClient, FDataBase.GetConnection(ConnectionString));
                        ////  client.ComptesAnalityques = compte.CompteAnalyTiques;


                        //if (client.IdDeviseFact > 0)
                        //    client.Devise = GetAllDeviseById(client.IdDeviseFact, idsite);

                        //// if (client.IdTerme > 0)
                        ////   client.LibelleTerme = Get_LIBELLEById(client.IdTerme);

                        //client.Llangue = GetLANGUEBY_ID(client.IdLangue);
                        //if (client.Idporata > 0)
                        //    client.Porata = Taxes_SELECTById(client.Idporata, idsite);
                        //else client.Porata = new Taxe { ID_Taxe = 0, IdSite = 0, Libelle = string.Empty, Taux = "0%" };
                        //client.Ccompte = Get_COMPTEBYID(client.IdCompte);
                        //client.Exonerate = Get_EXONERATIONBYID(client.IdExonere);
                        listes.Add(client);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Client> GetALL_CLIENTBY(Int32 idsite, string nomClient, string Ville)
        {
            List<Client> listes = new List<Client>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_CLIENT_SELECTBY";
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inNomcleint", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inVille", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters["inNomcleint"].Value = nomClient;
                    comand.Parameters["inVille"].Value = Ville;
                    comand.Parameters["inIdste"].Value = idsite;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Client client = new Client();
                        client.IdClient = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            client.BoitePostal = reader.GetString(1);
                        else client.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(2))
                            client.Ville = reader.GetString(2);
                        else client.Ville = string.Empty;

                        if (!reader.IsDBNull(3))
                            client.NumeroContribuable = reader.GetString(3);
                        else client.NumeroContribuable = string.Empty;

                        if (!reader.IsDBNull(4))
                            client.NomClient = reader.GetString(4);
                        else client.NomClient = string.Empty;

                        if (!reader.IsDBNull(5))
                            client.Rue1 = reader.GetString(5);
                        else client.Rue1 = string.Empty;

                        if (!reader.IsDBNull(6))
                            client.Rue2 = reader.GetString(6);
                        else client.Rue2 = string.Empty;



                        if (!reader.IsDBNull(7))
                            client.IdLangue = reader.GetInt16(7);
                        else client.IdLangue = 0;

                        if (!reader.IsDBNull(8))
                            client.TermeNombre = reader.GetInt32(8);
                        else client.TermeNombre = 0;

                        if (!reader.IsDBNull(9))
                            client.TermeDescription = reader.GetString(9);
                        else client.TermeDescription = string.Empty;

                        if (!reader.IsDBNull(10))
                            client.Idporata = reader.GetInt32(10);
                        else client.Idporata = 0;

                        if (!reader.IsDBNull(11))
                            client.DateEcheance = reader.GetString(11);
                        else client.DateEcheance = string.Empty;

                        if (!reader.IsDBNull(12))
                            client.IdSite = reader.GetInt32(12);
                        else client.IdSite = 0;
                        if (!reader.IsDBNull(13))
                            client.IdExonere = reader.GetInt32(13);
                        else client.IdExonere = 0;
                        if (!reader.IsDBNull(14))
                            client.NumeroImatriculation = reader.GetString(14);
                        else client.NumeroImatriculation = "";

                        if (!reader.IsDBNull(15))
                            client.IdCompte = reader.GetInt32(15);
                        else client.IdCompte = 0;

                        if (!reader.IsDBNull(16))
                            client.IdConversionFact = reader.GetInt32(16);
                        else client.IdConversionFact = 0;

                        if (!reader.IsDBNull(17))
                            client.IdTerme = reader.GetInt32(17);
                        else client.IdTerme = 0;

                        if (!reader.IsDBNull(18))
                        {
                            client.IdCompteTiers = reader.GetInt32(18);
                         //   client.CompteTiers = new CompteTiers(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(18));
                        }
                        else client.IdCompteTiers = null;

                        client.IsActive = reader.GetBoolean(19);
                      //  var compte = new CompteAnalClient(client.IdClient, FDataBase.GetConnection(ConnectionString));
                      //  client.ComptesAnalityques = compte.CompteAnalyTiques;
                        if (!reader.IsDBNull(20))
                            client.NomClientCompta = reader.GetString(20);

                        if (!reader.IsDBNull(21))
                            client.CodeClient = reader.GetString(21);
                        if (!reader.IsDBNull(22))
                            client.IdDeviseFact = reader.GetInt32(22);
                        else client.IdDeviseFact = 0;

                        if (!reader.IsDBNull(23))
                            client.CompteProduit = reader.GetString(23);


                        if (client.IdConversionFact > 0)
                            client.DeviseConversion = GetAllDeviseClientById(client.IdConversionFact, idsite);
                        if (client.IdDeviseFact > 0)
                            client.DeviseFacture = GetAllDeviseById(client.IdDeviseFact, idsite);

                       // if (client.IdTerme > 0)
                         //   client.LibelleTerme = Get_LIBELLEById(client.IdTerme);

                        client.Llangue = GetLANGUEBY_ID(client.IdLangue);
                        if (client.Idporata > 0)
                            client.Porata = Taxes_SELECTById(client.Idporata, idsite);
                        else client.Porata = new Taxe { ID_Taxe = 0, IdSite = 0, Libelle = string.Empty, Taux = "0%" };
                        client.Ccompte = Get_COMPTEBYID(client.IdCompte);
                        client.Exonerate = Get_EXONERATIONBYID(client.IdExonere);
                        listes.Add(client);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<Client> GetALL_CLIENT(Int32 idsite,bool isActif)
        {
            List<Client> listes = new List<Client>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_CLIENT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("actif", MySqlDbType.Bit);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters.Add("inIdFact", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdste"].Value = idsite;
                    comand.Parameters["actif"].Value = isActif;
                    comand.Parameters["isArchive"].Value = "NON";
                    comand.Parameters["inIdFact"].Value = 0;
                    
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Client client = new Client();
                        client.IdClient = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            client.BoitePostal = reader.GetString(1);
                        else client.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(2))
                            client.Ville = reader.GetString(2);
                        else client.Ville = string.Empty;

                        if (!reader.IsDBNull(3))
                            client.NumeroContribuable = reader.GetString(3);
                        else client.NumeroContribuable = string.Empty;

                        if (!reader.IsDBNull(4))
                            client.NomClient = reader.GetString(4);
                        else client.NomClient = string.Empty;

                        if (!reader.IsDBNull(5))
                            client.Rue1 = reader.GetString(5);
                        else client.Rue1 = string.Empty;

                        if (!reader.IsDBNull(6))
                            client.Rue2 = reader.GetString(6);
                        else client.Rue2 = string.Empty;



                        if (!reader.IsDBNull(7))
                            client.IdLangue = reader.GetInt16(7);
                        else client.IdLangue = 0;

                        if (!reader.IsDBNull(8))
                            client.TermeNombre = reader.GetInt32(8);
                        else client.TermeNombre = 0;

                        if (!reader.IsDBNull(9))
                            client.TermeDescription = reader.GetString(9);
                        else client.TermeDescription = string.Empty;

                        if (!reader.IsDBNull(10))
                            client.Idporata = reader.GetInt32(10);
                        else client.Idporata = 0;

                        if (!reader.IsDBNull(11))
                            client.DateEcheance = reader.GetString(11);
                        else client.DateEcheance = string.Empty;

                        if (!reader.IsDBNull(12))
                            client.IdSite = reader.GetInt32(12);
                        else client.IdSite = 0;
                        if (!reader.IsDBNull(13))
                            client.IdExonere = reader.GetInt32(13);
                        else client.IdExonere = 0;
                        if (!reader.IsDBNull(14))
                            client.NumeroImatriculation = reader.GetString(14);
                        else client.NumeroImatriculation = "";

                        if (!reader.IsDBNull(15))
                            client.IdCompte = reader.GetInt32(15);
                        else client.IdCompte = 0;

                        if (!reader.IsDBNull(16))
                            client.IdConversionFact = reader.GetInt32(16);
                        else client.IdConversionFact = 0;

                        if (!reader.IsDBNull(17))
                            client.IdTerme = reader.GetInt32(17);
                        else client.IdTerme = 0;

                        if (!reader.IsDBNull(18))
                        {
                            client.IdCompteTiers = reader.GetInt32(18);
                           // client.CompteTiers = new CompteTiers(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(18));
                        }
                        else client.IdCompteTiers = null;
                        client.IsActive = reader.GetBoolean(19);

                        if (!reader.IsDBNull(20))
                            client.NomClientCompta = reader.GetString(20);

                        if (!reader.IsDBNull(21))
                            client.CodeClient = reader.GetString(21);

                        if (!reader.IsDBNull(22))
                            client.IdDeviseFact = reader.GetInt32(22);
                        else client.IdDeviseFact = 0;


                        if (!reader.IsDBNull(23))
                            client.CompteProduit = reader.GetString(23);

                       // var compte = new CompteAnalClient(client.IdClient, FDataBase.GetConnection(ConnectionString));
                       // client.ComptesAnalityques = compte.CompteAnalyTiques;


                        if (client.IdConversionFact > 0)
                            client.DeviseConversion = GetAllDeviseClientById(client.IdConversionFact, idsite);
                        if (client.IdDeviseFact > 0)
                            client.DeviseFacture = GetAllDeviseById(client.IdDeviseFact, idsite);

                       // if (client.IdTerme > 0)
                          //  client.LibelleTerme = Get_LIBELLEById(client.IdTerme);

                        client.Llangue = GetLANGUEBY_ID(client.IdLangue);
                        if (client.Idporata > 0)
                            client.Porata = Taxes_SELECTById(client.Idporata, idsite);
                        else client.Porata = new Taxe { ID_Taxe = 0, IdSite = 0, Libelle = string.Empty, Taux = "0%" };
                        client.Ccompte = Get_COMPTEBYID(client.IdCompte);
                        client.Exonerate = Get_EXONERATIONBYID(client.IdExonere);
                        listes.Add(client);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Client> GetALL_CLIENT_ARCHIVES(Int32 idsite, bool isActif)
        {
            List<Client> listes = new List<Client>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_CLIENT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("actif", MySqlDbType.Bit);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters.Add("inIdFact", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdste"].Value = idsite;
                    comand.Parameters["actif"].Value = isActif;
                    comand.Parameters["isArchive"].Value = "OUI";
                    comand.Parameters["inIdFact"].Value = 0;

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Client client = new Client();
                        client.IdClient = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            client.BoitePostal = reader.GetString(1);
                        else client.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(2))
                            client.Ville = reader.GetString(2);
                        else client.Ville = string.Empty;

                        if (!reader.IsDBNull(3))
                            client.NumeroContribuable = reader.GetString(3);
                        else client.NumeroContribuable = string.Empty;

                        if (!reader.IsDBNull(4))
                            client.NomClient = reader.GetString(4);
                        else client.NomClient = string.Empty;

                        if (!reader.IsDBNull(5))
                            client.Rue1 = reader.GetString(5);
                        else client.Rue1 = string.Empty;

                        if (!reader.IsDBNull(6))
                            client.Rue2 = reader.GetString(6);
                        else client.Rue2 = string.Empty;



                        if (!reader.IsDBNull(7))
                            client.IdLangue = reader.GetInt16(7);
                        else client.IdLangue = 0;

                        if (!reader.IsDBNull(8))
                            client.TermeNombre = reader.GetInt32(8);
                        else client.TermeNombre = 0;

                        if (!reader.IsDBNull(9))
                            client.TermeDescription = reader.GetString(9);
                        else client.TermeDescription = string.Empty;

                        if (!reader.IsDBNull(10))
                            client.Idporata = reader.GetInt32(10);
                        else client.Idporata = 0;

                        if (!reader.IsDBNull(11))
                            client.DateEcheance = reader.GetString(11);
                        else client.DateEcheance = string.Empty;

                        if (!reader.IsDBNull(12))
                            client.IdSite = reader.GetInt32(12);
                        else client.IdSite = 0;
                        if (!reader.IsDBNull(13))
                            client.IdExonere = reader.GetInt32(13);
                        else client.IdExonere = 0;
                        if (!reader.IsDBNull(14))
                            client.NumeroImatriculation = reader.GetString(14);
                        else client.NumeroImatriculation = "";

                        if (!reader.IsDBNull(15))
                            client.IdCompte = reader.GetInt32(15);
                        else client.IdCompte = 0;

                        if (!reader.IsDBNull(16))
                            client.IdConversionFact = reader.GetInt32(16);
                        else client.IdConversionFact = 0;

                        if (!reader.IsDBNull(17))
                            client.IdTerme = reader.GetInt32(17);
                        else client.IdTerme = 0;

                        if (!reader.IsDBNull(18))
                        {
                            client.IdCompteTiers = reader.GetInt32(18);
                            // client.CompteTiers = new CompteTiers(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(18));
                        }
                        else client.IdCompteTiers = null;
                        client.IsActive = reader.GetBoolean(19);

                        if (!reader.IsDBNull(20))
                            client.NomClientCompta = reader.GetString(20);

                        // var compte = new CompteAnalClient(client.IdClient, FDataBase.GetConnection(ConnectionString));
                        // client.ComptesAnalityques = compte.CompteAnalyTiques;

                        if (!reader.IsDBNull(21))
                            client.CodeClient = reader.GetString(21);
                        if (!reader.IsDBNull(22))
                            client.IdDeviseFact = reader.GetInt32(22);
                        else client.IdDeviseFact = 0;


                        if (!reader.IsDBNull(23))
                            client.CompteProduit = reader.GetString(23);

                        if (client.IdConversionFact > 0)
                            client.DeviseConversion = GetAllDeviseClientById(client.IdConversionFact, idsite);
                        if (client.IdDeviseFact > 0)
                            client.DeviseFacture = GetAllDeviseById(client.IdDeviseFact, idsite);

                        // if (client.IdTerme > 0)
                        //  client.LibelleTerme = Get_LIBELLEById(client.IdTerme);

                        client.Llangue = GetLANGUEBY_ID(client.IdLangue);
                        if (client.Idporata > 0)
                            client.Porata = Taxes_SELECTByIdArchive(client.Idporata, idsite);
                        else client.Porata = new Taxe { ID_Taxe = 0, IdSite = 0, Libelle = string.Empty, Taux = "0%" };
                        client.Ccompte = Get_COMPTEBYIDArchive(client.IdCompte);
                        client.Exonerate = Get_EXONERATIONBYID(client.IdExonere);
                        listes.Add(client);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<Client> GetALL_CLIENT_FACTURER(Int32 idsite,int mode, int archive,DateTime dateFrom,DateTime DateTo)
        {
            List<Client> listes = new List<Client>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_CLIENT_SELECT_FACTURE;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Int32);
                    comand.Parameters.Add("inArchive", MySqlDbType.Int32);
                    comand.Parameters.Add("fromDate", MySqlDbType.DateTime);
                    comand.Parameters.Add("toDate", MySqlDbType.DateTime);

                    comand.Parameters["inIdste"].Value = idsite;
                    comand.Parameters["inMode"].Value = mode;
                    comand.Parameters["inArchive"].Value = archive;
                    comand.Parameters["fromDate"].Value = dateFrom;
                    comand.Parameters["toDate"].Value = DateTo;

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Client client = new Client();
                        client.IdClient = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            client.BoitePostal = reader.GetString(1);
                        else client.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(2))
                            client.Ville = reader.GetString(2);
                        else client.Ville = string.Empty;

                        if (!reader.IsDBNull(3))
                            client.NumeroContribuable = reader.GetString(3);
                        else client.NumeroContribuable = string.Empty;

                        if (!reader.IsDBNull(4))
                            client.NomClient = reader.GetString(4);
                        else client.NomClient = string.Empty;

                        if (!reader.IsDBNull(5))
                            client.Rue1 = reader.GetString(5);
                        else client.Rue1 = string.Empty;

                        if (!reader.IsDBNull(6))
                            client.Rue2 = reader.GetString(6);
                        else client.Rue2 = string.Empty;



                        if (!reader.IsDBNull(7))
                            client.IdLangue = reader.GetInt16(7);
                        else client.IdLangue = 0;

                        if (!reader.IsDBNull(8))
                            client.TermeNombre = reader.GetInt32(8);
                        else client.TermeNombre = 0;

                        if (!reader.IsDBNull(9))
                            client.TermeDescription = reader.GetString(9);
                        else client.TermeDescription = string.Empty;

                        if (!reader.IsDBNull(10))
                            client.Idporata = reader.GetInt32(10);
                        else client.Idporata = 0;

                        if (!reader.IsDBNull(11))
                            client.DateEcheance = reader.GetString(11);
                        else client.DateEcheance = string.Empty;

                        if (!reader.IsDBNull(12))
                            client.IdSite = reader.GetInt32(12);
                        else client.IdSite = 0;
                        if (!reader.IsDBNull(13))
                            client.IdExonere = reader.GetInt32(13);
                        else client.IdExonere = 0;
                        if (!reader.IsDBNull(14))
                            client.NumeroImatriculation = reader.GetString(14);
                        else client.NumeroImatriculation = "";

                        if (!reader.IsDBNull(15))
                            client.IdCompte = reader.GetInt32(15);
                        else client.IdCompte = 0;

                        if (!reader.IsDBNull(16))
                            client.IdConversionFact = reader.GetInt32(16);
                        else client.IdConversionFact = 0;

                        if (!reader.IsDBNull(17))
                            client.IdTerme = reader.GetInt32(17);
                        else client.IdTerme = 0;

                      

                        if (!reader.IsDBNull(18))
                        {
                            client.IdCompteTiers = reader.GetInt32(18);
                         //   client.CompteTiers = new CompteTiers(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(18));
                        }
                        else client.IdCompteTiers = null;
                        client.IsActive = reader.GetBoolean(19);

                      //  var compte = new CompteAnalClient(client.IdClient, FDataBase.GetConnection(ConnectionString));
                       // client.ComptesAnalityques = compte.CompteAnalyTiques;
                        if (!reader.IsDBNull(20))
                            client.CodeClient = reader.GetString(20);

                        if (!reader.IsDBNull(21))
                            client.IdDeviseFact = reader.GetInt32(21);
                        else client.IdDeviseFact = 0;

                        if (!reader.IsDBNull(22))
                            client.CompteProduit = reader.GetString(22);

                        if (client.IdConversionFact > 0)
                            client.DeviseConversion = GetAllDeviseClientById(client.IdConversionFact, idsite);
                        if (client.IdDeviseFact > 0)
                            client.DeviseFacture = GetAllDeviseById(client.IdDeviseFact, idsite);


                       

                       // if (client.IdTerme > 0)
                          //  client.LibelleTerme = Get_LIBELLEById(client.IdTerme);

                        client.Llangue = GetLANGUEBY_ID(client.IdLangue);
                        if (client.Idporata > 0)
                            client.Porata = Taxes_SELECTById(client.Idporata, idsite);
                        else client.Porata = new Taxe { ID_Taxe = 0, IdSite = 0, Libelle = string.Empty, Taux = "0%" };
                        client.Ccompte = Get_COMPTEBYID(client.IdCompte);
                        client.Exonerate = Get_EXONERATIONBYID(client.IdExonere);
                        listes.Add(client);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataTable GetCLIENTByID(int idClient, Int32 idsite)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                DataTable tableResult = new DataTable();
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_CLIENT_SELECT;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                comand.Parameters.Add("actif", MySqlDbType.Bit);
                comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                comand.Parameters.Add("inIdFact", MySqlDbType.Int64);
                comand.Parameters["inId"].Value = idClient;
                comand.Parameters["inIdste"].Value = idsite;
                comand.Parameters["actif"].Value = true;
                comand.Parameters["isArchive"].Value ="NON";
                comand.Parameters["inIdFact"].Value = 0;
                MySqlDataAdapter adapter = new MySqlDataAdapter(comand);
                adapter.Fill(tableResult);
                return tableResult;

            }
        }

        public DataTable GetCLIENT_Archive_ByID(int idClient, Int32 idsite, Int64 idFacture)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                DataTable tableResult = new DataTable();
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_CLIENT_SELECT;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                comand.Parameters.Add("actif", MySqlDbType.Bit);
                comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                comand.Parameters.Add("inIdFact", MySqlDbType.Int64);
                comand.Parameters["inId"].Value = idClient;
                comand.Parameters["inIdste"].Value = idsite;
                comand.Parameters["actif"].Value = true;
                comand.Parameters["isArchive"].Value = "OUI";
                comand.Parameters["inIdFact"].Value = idFacture;
                MySqlDataAdapter adapter = new MySqlDataAdapter(comand);
                adapter.Fill(tableResult);
                return tableResult;

            }
        }


        public Client GetALL_CLIENTByID(int idClient, Int32 idsite)
        {
            Client client = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_CLIENT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("actif", MySqlDbType.Bit);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters.Add("inIdFact", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = idClient;
                    comand.Parameters["inIdste"].Value = idsite;
                    comand.Parameters["actif"].Value = true;
                    comand.Parameters["isArchive"].Value = "NON";
                    comand.Parameters["inIdFact"].Value = 0;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        client = new Client();
                        client.IdClient = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            client.BoitePostal = reader.GetString(1);
                        else client.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(2))
                            client.Ville = reader.GetString(2);
                        else client.Ville = string.Empty;

                        if (!reader.IsDBNull(3))
                            client.NumeroContribuable = reader.GetString(3);
                        else client.NumeroContribuable = string.Empty;

                        if (!reader.IsDBNull(4))
                            client.NomClient = reader.GetString(4);
                        else client.NomClient = string.Empty;

                        if (!reader.IsDBNull(5))
                            client.Rue1 = reader.GetString(5);
                        else client.Rue1 = string.Empty;

                        if (!reader.IsDBNull(6))
                            client.Rue2 = reader.GetString(6);
                        else client.Rue2 = string.Empty;


                        if (!reader.IsDBNull(7))
                            client.IdLangue = reader.GetInt16(7);
                        else client.IdLangue = 0;

                        if (!reader.IsDBNull(8))
                            client.TermeNombre = reader.GetInt32(8);
                        else client.TermeNombre = 0;

                        if (!reader.IsDBNull(9))
                            client.TermeDescription = reader.GetString(9);
                        else client.TermeDescription = string.Empty;

                        if (!reader.IsDBNull(10))
                            client.Idporata = reader.GetInt32(10);
                        else client.Idporata = 0;

                        if (!reader.IsDBNull(11))
                            client.DateEcheance = reader.GetString(11);
                        else client.DateEcheance = string.Empty;

                        if (!reader.IsDBNull(12))
                            client.IdSite = reader.GetInt32(12);
                        else client.IdSite = 0;
                        if (!reader.IsDBNull(13))
                            client.IdExonere = reader.GetInt32(13);
                        else client.IdExonere = 0;
                        if (!reader.IsDBNull(14))
                            client.NumeroImatriculation = reader.GetString(14);
                        else client.NumeroImatriculation = "";
                        if (!reader.IsDBNull(15))
                            client.IdCompte = reader.GetInt32(15);
                        else client.IdCompte = 0;

                        if (!reader.IsDBNull(16))
                            client.IdConversionFact = reader.GetInt32(16);
                        else client.IdConversionFact = 0;

                        if (!reader.IsDBNull(17))
                            client.IdTerme = reader.GetInt32(17);
                        else client.IdTerme = 0;

                        if (!reader.IsDBNull(18))
                        {
                            client.IdCompteTiers = reader.GetInt32(18);
                       //     client.CompteTiers = new CompteTiers(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(18));
                        }
                        else client.IdCompteTiers = null;
                        client.IsActive = reader.GetBoolean(19);

                        if (!reader.IsDBNull(22))
                            client.IdDeviseFact = reader.GetInt32(22);
                        else client.IdDeviseFact = 0;

                        if (!reader.IsDBNull(23))
                            client.CompteProduit = reader.GetString(23);

                       // var compte = new CompteAnalClient(client.IdClient, FDataBase.GetConnection(ConnectionString));
                       // client.ComptesAnalityques = compte.CompteAnalyTiques;

                        if (client.IdConversionFact > 0)
                            client.DeviseConversion = GetAllDeviseClientById(client.IdConversionFact, idsite);
                        if (client.IdDeviseFact > 0)
                            client.DeviseFacture = GetAllDeviseById(client.IdDeviseFact, idsite);

                       // if (client.IdTerme > 0)
                           // client.LibelleTerme = Get_LIBELLEById(client.IdTerme);
                        if (!reader.IsDBNull(21))
                            client.CodeClient = reader.GetString(21);

                     
                        client.Llangue = GetLANGUEBY_ID(client.IdLangue);
                        if (client.Idporata > 0)
                            client.Porata = Taxes_SELECTById(client.Idporata, idsite);
                        else client.Porata = new Taxe { ID_Taxe = 0, IdSite = 0, Libelle = string.Empty, Taux = "0%" };
                        client.Ccompte = Get_COMPTEBYID(client.IdCompte);
                        client.Exonerate = Get_EXONERATIONBYID(client.IdExonere);
                        break;
                    }


                }


                return client;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Client GetALL_CLIENT_ArchiveByID(int idClient, Int32 idsite, Int64 idFacture)
        {
            Client client = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_CLIENT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("actif", MySqlDbType.Bit);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters.Add("inIdFact", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = idClient;
                    comand.Parameters["inIdste"].Value = idsite;
                    comand.Parameters["actif"].Value = true;
                    comand.Parameters["isArchive"].Value = "OUI";
                    comand.Parameters["inIdFact"].Value = idFacture;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        client = new Client();
                        client.IdClient = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            client.BoitePostal = reader.GetString(1);
                        else client.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(2))
                            client.Ville = reader.GetString(2);
                        else client.Ville = string.Empty;

                        if (!reader.IsDBNull(3))
                            client.NumeroContribuable = reader.GetString(3);
                        else client.NumeroContribuable = string.Empty;

                        if (!reader.IsDBNull(4))
                            client.NomClient = reader.GetString(4);
                        else client.NomClient = string.Empty;

                        if (!reader.IsDBNull(5))
                            client.Rue1 = reader.GetString(5);
                        else client.Rue1 = string.Empty;

                        if (!reader.IsDBNull(6))
                            client.Rue2 = reader.GetString(6);
                        else client.Rue2 = string.Empty;


                        if (!reader.IsDBNull(7))
                            client.IdLangue = reader.GetInt16(7);
                        else client.IdLangue = 0;

                        if (!reader.IsDBNull(8))
                            client.TermeNombre = reader.GetInt32(8);
                        else client.TermeNombre = 0;

                        if (!reader.IsDBNull(9))
                            client.TermeDescription = reader.GetString(9);
                        else client.TermeDescription = string.Empty;

                        if (!reader.IsDBNull(10))
                            client.Idporata = reader.GetInt32(10);
                        else client.Idporata = 0;

                        if (!reader.IsDBNull(11))
                            client.DateEcheance = reader.GetString(11);
                        else client.DateEcheance = string.Empty;

                        if (!reader.IsDBNull(12))
                            client.IdSite = reader.GetInt32(12);
                        else client.IdSite = 0;
                        if (!reader.IsDBNull(13))
                            client.IdExonere = reader.GetInt32(13);
                        else client.IdExonere = 0;
                        if (!reader.IsDBNull(14))
                            client.NumeroImatriculation = reader.GetString(14);
                        else client.NumeroImatriculation = "";
                        if (!reader.IsDBNull(15))
                            client.IdCompte = reader.GetInt32(15);
                        else client.IdCompte = 0;

                        if (!reader.IsDBNull(16))
                            client.IdConversionFact = reader.GetInt32(16);
                        else client.IdConversionFact = 0;

                        if (!reader.IsDBNull(17))
                            client.IdTerme = reader.GetInt32(17);
                        else client.IdTerme = 0;

                        if (!reader.IsDBNull(18))
                        {
                            client.IdCompteTiers = reader.GetInt32(18);
                           // client.CompteTiers = new CompteTiers(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(18));
                        }
                        else client.IdCompteTiers = null;
                        client.IsActive = reader.GetBoolean(19);

                        if (!reader.IsDBNull(20))
                            client.NomClientCompta = reader.GetString(20);

                        if (!reader.IsDBNull(22))
                            client.IdDeviseFact = reader.GetInt32(22);
                        else client.IdDeviseFact = 0;

                        if (!reader.IsDBNull(23))
                            client.CompteProduit = reader.GetString(23);

                       // var compte = new CompteAnalClient(client.IdClient, FDataBase.GetConnection(ConnectionString));
                       // client.ComptesAnalityques = compte.CompteAnalyTiques;

                        if (client.IdConversionFact > 0)
                            client.DeviseConversion = GetAllDeviseClientById(client.IdConversionFact, idsite);
                        if (client.IdDeviseFact > 0)
                            client.DeviseFacture = GetAllDeviseById(client.IdDeviseFact, idsite);

                        if (!reader.IsDBNull(21))
                            client.CodeClient = reader.GetString(21);
                       // if (client.IdTerme > 0)
                           // client.LibelleTerme = Get_LIBELLE_ArchiveById(client.IdTerme);


                       

                        client.Llangue = GetLANGUEBY_ID(client.IdLangue);
                        if (client.Idporata > 0)
                            client.Porata = Taxes_SELECTByIdArchive(client.Idporata, idsite);
                        else client.Porata = new Taxe { ID_Taxe = 0, IdSite = 0, Libelle = string.Empty, Taux = "0%" };
                        client.Ccompte = Get_COMPTEBYIDArchive(client.IdCompte);
                        client.Exonerate = Get_EXONERATIONBYID(client.IdExonere);
                        break;
                    }


                }


                return client;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Client GetALL_CLIENTByIDArchive(int idClient, Int64 idFacture)
        {
            Client client = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_CLIENT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("actif", MySqlDbType.Bit);
                   comand.Parameters.Add("isArchive", MySqlDbType.VarChar,5);
                   comand.Parameters.Add("inIdFact", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = idClient;
                    comand.Parameters["inIdste"].Value = 0;
                    comand.Parameters["actif"].Value = true;
                   comand.Parameters["isArchive"].Value ="OUI";
                   comand.Parameters["inIdFact"].Value = idFacture;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        client = new Client();
                        client.IdClient = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            client.BoitePostal = reader.GetString(1);
                        else client.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(2))
                            client.Ville = reader.GetString(2);
                        else client.Ville = string.Empty;

                        if (!reader.IsDBNull(3))
                            client.NumeroContribuable = reader.GetString(3);
                        else client.NumeroContribuable = string.Empty;

                        if (!reader.IsDBNull(4))
                            client.NomClient = reader.GetString(4);
                        else client.NomClient = string.Empty;

                        if (!reader.IsDBNull(5))
                            client.Rue1 = reader.GetString(5);
                        else client.Rue1 = string.Empty;

                        if (!reader.IsDBNull(6))
                            client.Rue2 = reader.GetString(6);
                        else client.Rue2 = string.Empty;



                        if (!reader.IsDBNull(7))
                            client.IdLangue = reader.GetInt16(7);
                        else client.IdLangue = 0;

                        if (!reader.IsDBNull(8))
                            client.TermeNombre = reader.GetInt32(8);
                        else client.TermeNombre = 0;

                        if (!reader.IsDBNull(9))
                            client.TermeDescription = reader.GetString(9);
                        else client.TermeDescription = string.Empty;

                        if (!reader.IsDBNull(10))
                            client.Idporata = reader.GetInt32(10);
                        else client.Idporata = 0;

                        if (!reader.IsDBNull(11))
                            client.DateEcheance = reader.GetString(11);
                        else client.DateEcheance = string.Empty;

                        if (!reader.IsDBNull(12))
                            client.IdSite = reader.GetInt32(12);
                        else client.IdSite = 0;
                        if (!reader.IsDBNull(13))
                            client.IdExonere = reader.GetInt32(13);
                        else client.IdExonere = 0;
                        if (!reader.IsDBNull(14))
                            client.NumeroImatriculation = reader.GetString(14);
                        else client.NumeroImatriculation = "";
                        if (!reader.IsDBNull(15))
                            client.IdCompte = reader.GetInt32(15);
                        else client.IdCompte = 0;

                        if (!reader.IsDBNull(16))
                            client.IdConversionFact = reader.GetInt32(16);
                        else client.IdConversionFact = 0;

                        if (!reader.IsDBNull(17))
                            client.IdTerme = reader.GetInt32(17);
                        else client.IdTerme = 0;

                        if (!reader.IsDBNull(18))
                           client.IdCompteTiers = reader.GetInt32(18);
                        //    client.CompteTiers = new CompteTiers(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(18));
                        //}
                       // else client.IdCompteTiers = null;
                        client.IsActive = reader.GetBoolean(19);

                        if (!reader.IsDBNull(20))
                            client.NomClientCompta = reader.GetString(20);

                        if (!reader.IsDBNull(22))
                            client.IdDeviseFact = reader.GetInt32(22);
                        else client.IdDeviseFact = 0;

                        if (!reader.IsDBNull(23))
                            client.CompteProduit = reader.GetString(23);

                       // var compte = new CompteAnalClient(client.IdClient, FDataBase.GetConnection(ConnectionString));
                       // client.ComptesAnalityques = compte.CompteAnalyTiques;

                        if (client.IdConversionFact > 0)
                            client.DeviseConversion = GetAllDeviseClientById(client.IdConversionFact, 0);
                        if (client.IdDeviseFact > 0)
                            client.DeviseFacture = GetAllDeviseById(client.IdDeviseFact, 0);

                        if (!reader.IsDBNull(21))
                            client.CodeClient = reader.GetString(21);
                       // if (client.IdTerme > 0)
                           // client.LibelleTerme = Get_LIBELLE_ArchiveById(client.IdTerme);


                     
                        client.Llangue = GetLANGUEBY_ID(client.IdLangue);
                        //client.Devise = GetAllDeviseById(client.IdDeviseFact);
                        if (client.Idporata > 0)
                            client.Porata = Taxes_SELECTByIdArchive(client.Idporata, reader.GetInt32(12));
                        else client.Porata = new Taxe { ID_Taxe = 0, IdSite = 0, Libelle = string.Empty, Taux = "0%" };
                        client.Ccompte = Get_COMPTEBYIDArchive(client.IdCompte);
                        client.Exonerate = Get_EXONERATIONBYID(client.IdExonere);

                        break;
                    }


                }


                return client;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Client GetALL_CLIENTByID(int idClient)
        {
            Client client = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_CLIENT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("actif", MySqlDbType.Bit);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters.Add("inIdFact", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = idClient;
                    comand.Parameters["inIdste"].Value = 0;
                    comand.Parameters["actif"].Value = true;
                   comand.Parameters["isArchive"].Value ="NON";
                   comand.Parameters["inIdFact"].Value =0;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        client = new Client();
                        client.IdClient = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            client.BoitePostal = reader.GetString(1);
                        else client.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(2))
                            client.Ville = reader.GetString(2);
                        else client.Ville = string.Empty;

                        if (!reader.IsDBNull(3))
                            client.NumeroContribuable = reader.GetString(3);
                        else client.NumeroContribuable = string.Empty;

                        if (!reader.IsDBNull(4))
                            client.NomClient = reader.GetString(4);
                        else client.NomClient = string.Empty;

                        if (!reader.IsDBNull(5))
                            client.Rue1 = reader.GetString(5);
                        else client.Rue1 = string.Empty;

                        if (!reader.IsDBNull(6))
                            client.Rue2 = reader.GetString(6);
                        else client.Rue2 = string.Empty;



                        if (!reader.IsDBNull(7))
                            client.IdLangue = reader.GetInt16(7);
                        else client.IdLangue = 0;

                        if (!reader.IsDBNull(8))
                            client.TermeNombre = reader.GetInt32(8);
                        else client.TermeNombre = 0;

                        if (!reader.IsDBNull(9))
                            client.TermeDescription = reader.GetString(9);
                        else client.TermeDescription = string.Empty;

                        if (!reader.IsDBNull(10))
                            client.Idporata = reader.GetInt32(10);
                        else client.Idporata = 0;

                        if (!reader.IsDBNull(11))
                            client.DateEcheance = reader.GetString(11);
                        else client.DateEcheance = string.Empty;

                        if (!reader.IsDBNull(12))
                            client.IdSite = reader.GetInt32(12);
                        else client.IdSite = 0;
                        if (!reader.IsDBNull(13))
                            client.IdExonere = reader.GetInt32(13);
                        else client.IdExonere = 0;
                        if (!reader.IsDBNull(14))
                            client.NumeroImatriculation = reader.GetString(14);
                        else client.NumeroImatriculation = "";
                        if (!reader.IsDBNull(15))
                            client.IdCompte = reader.GetInt32(15);
                        else client.IdCompte = 0;

                        if (!reader.IsDBNull(16))
                            client.IdConversionFact = reader.GetInt32(16);
                        else client.IdConversionFact = 0;

                        if (!reader.IsDBNull(17))
                            client.IdTerme = reader.GetInt32(17);
                        else client.IdTerme = 0;

                        if (!reader.IsDBNull(18))
                        {
                            client.IdCompteTiers = reader.GetInt32(18);
                           // client.CompteTiers = new CompteTiers(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(18));
                        }
                        else client.IdCompteTiers = null;
                        client.IsActive = reader.GetBoolean(19);

                        if (!reader.IsDBNull(20))
                            client.NomClientCompta = reader.GetString(20);

                        if (!reader.IsDBNull(22))
                            client.IdDeviseFact = reader.GetInt32(22);
                        else client.IdDeviseFact = 0;
                        if (!reader.IsDBNull(23))
                            client.CompteProduit = reader.GetString(23);

                       // var compte = new CompteAnalClient(client.IdClient, FDataBase.GetConnection(ConnectionString));
                       // client.ComptesAnalityques = compte.CompteAnalyTiques;

                        if (client.IdConversionFact > 0)
                            client.DeviseConversion = GetAllDeviseClientById(client.IdConversionFact, 0);
                        if (client.IdDeviseFact > 0)
                            client.DeviseFacture = GetAllDeviseById(client.IdDeviseFact, 0);

                        if (!reader.IsDBNull(21))
                            client.CodeClient = reader.GetString(21);


                        //if (client.IdDeviseFact > 0)
                        //    client.Devise = GetAllDeviseById(client.IdDeviseFact);
                        client.Llangue = GetLANGUEBY_ID(client.IdLangue);
                        //client.Devise = GetAllDeviseById(client.IdDeviseFact);
                        if (client.Idporata > 0)
                            client.Porata = Taxes_SELECTById(client.Idporata, reader.GetInt32(12));
                        else client.Porata = new Taxe { ID_Taxe = 0, IdSite = 0, Libelle = string.Empty, Taux = "0%" };
                        client.Ccompte = Get_COMPTEBYID(client.IdCompte);
                        client.Exonerate = Get_EXONERATIONBYID(client.IdExonere);

                        break;
                    }


                }


                return client;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool CLIENT_ADD(ref Int32 ID,Client client)
        {
            bool isValues = false;
            try
            {

                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_CLIENT_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Direction = ParameterDirection.InputOutput;
                    comand.Parameters.Add("inBp", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inVille", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inNumContrib", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inNomClient", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inRue1", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inRue2", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inTermeNombre", MySqlDbType.Int32);
                    comand.Parameters.Add("inTermeDescript", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inPorata", MySqlDbType.Int32);
                    comand.Parameters.Add("inDateEcheance", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdExonere", MySqlDbType.Int32);
                    comand.Parameters.Add("inNumeroImat", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdcompte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDevise", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdTerme", MySqlDbType.Int32);
                    comand.Parameters.Add("inCompteTiers", MySqlDbType.Int32);
                    comand.Parameters.Add("InNomClientCompta", MySqlDbType.VarChar, 45);
                    comand.Parameters.Add("inCodeClient", MySqlDbType.VarChar, 45);
                    comand.Parameters.Add("inIDDeviseFacture", MySqlDbType.Int32);
                    comand.Parameters.Add("inCompteProduit", MySqlDbType.VarChar, 45);


                    comand.Parameters["inId"].Value = client.IdClient;
                    comand.Parameters["inBp"].Value = client.BoitePostal;
                    comand.Parameters["inVille"].Value = client.Ville;
                    comand.Parameters["inNumContrib"].Value = client.NumeroContribuable;
                    comand.Parameters["inNomClient"].Value = client.NomClient;
                    comand.Parameters["inRue1"].Value = client.Rue1;
                    comand.Parameters["inRue2"].Value = client.Rue2;

                    comand.Parameters["inIdLangue"].Value = client.IdLangue;
                    comand.Parameters["inTermeNombre"].Value = client.TermeNombre;
                    comand.Parameters["inTermeDescript"].Value = client.TermeDescription;
                    comand.Parameters["inPorata"].Value = client.Idporata;
                    comand.Parameters["inDateEcheance"].Value = client.DateEcheance;
                    comand.Parameters["inIdSite"].Value = client.IdSite;
                    comand.Parameters["inIdExonere"].Value = client.IdExonere;
                    comand.Parameters["inNumeroImat"].Value = client.NumeroImatriculation;
                    comand.Parameters["inIdcompte"].Value = client.IdCompte;
                    comand.Parameters["inIDevise"].Value = client.IdConversionFact;
                    comand.Parameters["inIdTerme"].Value = client.IdTerme;
                    comand.Parameters["inCompteTiers"].Value = client.IdCompteTiers;
                    comand.Parameters["InNomClientCompta"].Value = client.NomClientCompta;
                    comand.Parameters["inCodeClient"].Value = client.CodeClient;
                    comand.Parameters["inIDDeviseFacture"].Value = client.IdDeviseFact;
                    comand.Parameters["inCompteProduit"].Value = client.CompteProduit;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                    ID = comand.Parameters["inId"].Value != DBNull.Value ? Int32.Parse(comand.Parameters["inId"].Value.ToString()) : 0;
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public bool CLIENT_ADD_Archive(Int32 IDcLient,Int32 IdComptetiers,string nomClientCompta,Int32 idSite)
        {
            bool isValues = false;
            try
            {

                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_CLIENT_ADD_Archivage";
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inCompteTiers", MySqlDbType.Int32);
                    comand.Parameters.Add("InNomClientCompta", MySqlDbType.VarChar, 45);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = IDcLient;
                    comand.Parameters["inCompteTiers"].Value = IdComptetiers;
                    comand.Parameters["InNomClientCompta"].Value = nomClientCompta;
                    comand.Parameters["inIdSite"].Value = idSite;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool CLIENT_ACTIF(int IdClient,int idSite,bool isActive)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_ClientActive;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("inActif", MySqlDbType.Int32);
                    comand.Parameters["inIdClient"].Value = IdClient;
                    comand.Parameters["inIdste"].Value = idSite;
                    comand.Parameters["inActif"].Value = isActive;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool CLIENT_DELETE(int Id)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_CLIENT_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = Id;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }
        #endregion

        #region COMPTE

        public List<Compte> GetAll_COMPTE()
        {
            List<Compte> listes = new List<Compte>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_COMPTE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdsite"].Value = 0;
                    comand.Parameters["isArchive"].Value ="NON";


                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Compte objet = new Compte();
                        objet.ID = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.IDSite = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.NumeroCompte = reader.GetString(2);

                        if (!reader.IsDBNull(3))
                            objet.NomBanque = reader.GetString(3);
                        else objet.NomBanque = string.Empty;

                        if (!reader.IsDBNull(4))
                            objet.Ville = reader.GetString(4);
                        else objet.Ville = string.Empty;

                        if (!reader.IsDBNull(5))
                            objet.Agence = reader.GetString(5);
                        else objet.Agence = string.Empty;

                        if (!reader.IsDBNull(6))
                            objet.Rue = reader.GetString(6);
                        else objet.Rue = string.Empty;

                        if (!reader.IsDBNull(7))
                            objet.BoitePostal = reader.GetString(7);
                        else objet.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(8))
                            objet.Telephone = reader.GetString(8);
                        else objet.Telephone = string.Empty;

                        if (!reader.IsDBNull(9))
                            objet.Pays = reader.GetString(9);
                        else objet.Pays = string.Empty;
                        if (!reader.IsDBNull(10))
                            objet.Quartier = reader.GetString(10);
                        else objet.Quartier = string.Empty;

                        listes.Add(objet);
                    }
                }

                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Compte> GetAll_COMPTEARchive()
        {
            List<Compte> listes = new List<Compte>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_COMPTE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdsite"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";


                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Compte objet = new Compte();
                        objet.ID = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.IDSite = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.NumeroCompte = reader.GetString(2);

                        if (!reader.IsDBNull(3))
                            objet.NomBanque = reader.GetString(3);
                        else objet.NomBanque = string.Empty;

                        if (!reader.IsDBNull(4))
                            objet.Ville = reader.GetString(4);
                        else objet.Ville = string.Empty;

                        if (!reader.IsDBNull(5))
                            objet.Agence = reader.GetString(5);
                        else objet.Agence = string.Empty;

                        if (!reader.IsDBNull(6))
                            objet.Rue = reader.GetString(6);
                        else objet.Rue = string.Empty;

                        if (!reader.IsDBNull(7))
                            objet.BoitePostal = reader.GetString(7);
                        else objet.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(8))
                            objet.Telephone = reader.GetString(8);
                        else objet.Telephone = string.Empty;

                        if (!reader.IsDBNull(9))
                            objet.Pays = reader.GetString(9);
                        else objet.Pays = string.Empty;
                        if (!reader.IsDBNull(10))
                            objet.Quartier = reader.GetString(10);
                        else objet.Quartier = string.Empty;

                        listes.Add(objet);
                    }
                }

                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Compte> GetAll_COMPTESite(int idSite)
        {
            List<Compte> listes = new List<Compte>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_COMPTE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdsite"].Value = idSite;
                    comand.Parameters["isArchive"].Value ="NON";


                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Compte objet = new Compte();
                        objet.ID = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.IDSite = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.NumeroCompte = reader.GetString(2);

                        if (!reader.IsDBNull(3))
                            objet.NomBanque = reader.GetString(3);
                        else objet.NomBanque = string.Empty;

                        if (!reader.IsDBNull(4))
                            objet.Ville = reader.GetString(4);
                        else objet.Ville = string.Empty;

                        if (!reader.IsDBNull(5))
                            objet.Agence = reader.GetString(5);
                        else objet.Agence = string.Empty;

                        if (!reader.IsDBNull(6))
                            objet.Rue = reader.GetString(6);
                        else objet.Rue = string.Empty;

                        if (!reader.IsDBNull(7))
                            objet.BoitePostal = reader.GetString(7);
                        else objet.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(8))
                            objet.Telephone = reader.GetString(8);
                        else objet.Telephone = string.Empty;

                        if (!reader.IsDBNull(9))
                            objet.Pays = reader.GetString(9);
                        else objet.Pays = string.Empty;
                        if (!reader.IsDBNull(10))
                            objet.Quartier = reader.GetString(10);
                        else objet.Quartier = string.Empty;

                        listes.Add(objet);
                    }
                }

                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Compte> GetAll_COMPTESiteArchive(int idSite)
        {
            List<Compte> listes = new List<Compte>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_COMPTE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdsite"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "OUI";


                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Compte objet = new Compte();
                        objet.ID = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.IDSite = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.NumeroCompte = reader.GetString(2);

                        if (!reader.IsDBNull(3))
                            objet.NomBanque = reader.GetString(3);
                        else objet.NomBanque = string.Empty;

                        if (!reader.IsDBNull(4))
                            objet.Ville = reader.GetString(4);
                        else objet.Ville = string.Empty;

                        if (!reader.IsDBNull(5))
                            objet.Agence = reader.GetString(5);
                        else objet.Agence = string.Empty;

                        if (!reader.IsDBNull(6))
                            objet.Rue = reader.GetString(6);
                        else objet.Rue = string.Empty;

                        if (!reader.IsDBNull(7))
                            objet.BoitePostal = reader.GetString(7);
                        else objet.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(8))
                            objet.Telephone = reader.GetString(8);
                        else objet.Telephone = string.Empty;

                        if (!reader.IsDBNull(9))
                            objet.Pays = reader.GetString(9);
                        else objet.Pays = string.Empty;
                        if (!reader.IsDBNull(10))
                            objet.Quartier = reader.GetString(10);
                        else objet.Quartier = string.Empty;

                        listes.Add(objet);
                    }
                }

                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Compte Get_COMPTEBYID(Int32 id)
        {
            Compte objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_COMPTE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdsite"].Value = 0;
                    comand.Parameters["isArchive"].Value ="NON";


                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new Compte();
                        objet.ID = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.IDSite = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.NumeroCompte = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.NomBanque = reader.GetString(3);
                        else objet.NomBanque = string.Empty;

                        if (!reader.IsDBNull(4))
                            objet.Ville = reader.GetString(4);
                        else objet.Ville = string.Empty;

                        if (!reader.IsDBNull(5))
                            objet.Agence = reader.GetString(5);
                        else objet.Agence = string.Empty;

                        if (!reader.IsDBNull(6))
                            objet.Rue = reader.GetString(6);
                        else objet.Rue = string.Empty;

                        if (!reader.IsDBNull(7))
                            objet.BoitePostal = reader.GetString(7);
                        else objet.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(8))
                            objet.Telephone = reader.GetString(8);
                        else objet.Telephone = string.Empty;

                        if (!reader.IsDBNull(9))
                            objet.Pays = reader.GetString(9);
                        else objet.Pays = string.Empty;

                        if (!reader.IsDBNull(10))
                            objet.Quartier = reader.GetString(10);
                        else objet.Quartier = string.Empty;


                        break;

                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Compte Get_COMPTEBYIDArchive(Int32 id)
        {
            Compte objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_COMPTE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdsite"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";


                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new Compte();
                        objet.ID = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.IDSite = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.NumeroCompte = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.NomBanque = reader.GetString(3);
                        else objet.NomBanque = string.Empty;

                        if (!reader.IsDBNull(4))
                            objet.Ville = reader.GetString(4);
                        else objet.Ville = string.Empty;

                        if (!reader.IsDBNull(5))
                            objet.Agence = reader.GetString(5);
                        else objet.Agence = string.Empty;

                        if (!reader.IsDBNull(6))
                            objet.Rue = reader.GetString(6);
                        else objet.Rue = string.Empty;

                        if (!reader.IsDBNull(7))
                            objet.BoitePostal = reader.GetString(7);
                        else objet.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(8))
                            objet.Telephone = reader.GetString(8);
                        else objet.Telephone = string.Empty;

                        if (!reader.IsDBNull(9))
                            objet.Pays = reader.GetString(9);
                        else objet.Pays = string.Empty;

                        if (!reader.IsDBNull(10))
                            objet.Quartier = reader.GetString(10);
                        else objet.Quartier = string.Empty;


                        break;

                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public object COMPTE_ADD(Compte compte)
        {
            object isValues = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_COMPTE_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("innumcompte", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inNom_banque", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inVille", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inAgence", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inRue", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inBoitePostal", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inPays", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inTelephone", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("pcIdinsert", MySqlDbType.VarChar, 255);
                    comand.Parameters["pcIdinsert"].Direction = ParameterDirection.Output;
                    comand.Parameters.Add("inQuartier", MySqlDbType.VarChar, 255);

                    comand.Parameters["inId"].Value = compte.ID;
                    comand.Parameters["inIdste"].Value = compte.IDSite;
                    comand.Parameters["innumcompte"].Value = compte.NumeroCompte;
                    comand.Parameters["inNom_banque"].Value = compte.NomBanque;
                    comand.Parameters["inBoitePostal"].Value = compte.BoitePostal;
                    comand.Parameters["inVille"].Value = compte.Ville;
                    comand.Parameters["inAgence"].Value = compte.Agence;
                    comand.Parameters["inRue"].Value = compte.Rue;
                    comand.Parameters["inPays"].Value = compte.Pays;
                    comand.Parameters["inTelephone"].Value = compte.Telephone;
                    comand.Parameters["pcIdinsert"].Value = 0;
                    comand.Parameters["inQuartier"].Value = compte.Quartier;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                    isValues = comand.Parameters["pcIdinsert"].Value;


                }

            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool COMPTE_DELETE(int Id)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_COMPTE_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = Id;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }
        #endregion

        #region LIBELLE TERME


        public List<Libelle_Terme> GetAll_LIBELLE(int idlangue)
        {
            List<Libelle_Terme> listes = new List<Libelle_Terme>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LIBELLE_TERME_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar,5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = idlangue;
                    comand.Parameters["isArchive"].Value = "NON";


                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Libelle_Terme objet = new Libelle_Terme();
                        objet.ID = reader.GetInt32(0);
                        if (!reader.IsDBNull(2))
                            objet.Desciption = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.CourtDesc = reader.GetString(3);

                        listes.Add(objet);
                    }
                }

                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Libelle_Terme> GetAll_LIBELLEArchive(int idlangue)
        {
            List<Libelle_Terme> listes = new List<Libelle_Terme>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LIBELLE_TERME_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = idlangue;
                    comand.Parameters["isArchive"].Value = "OUI";


                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Libelle_Terme objet = new Libelle_Terme();
                        objet.ID = reader.GetInt32(0);
                        if (!reader.IsDBNull(2))
                            objet.Desciption = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.CourtDesc = reader.GetString(3);

                        listes.Add(objet);
                    }
                }

                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Libelle_Terme Get_LIBELLEById(int id)
        {
            Libelle_Terme objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LIBELLE_TERME_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["isArchive"].Value = "NON";


                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new Libelle_Terme();
                        objet.ID = reader.GetInt32(0);
                        if (!reader.IsDBNull(2))
                            objet.Desciption = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.CourtDesc = reader.GetString(3);
                        break;
                    }
                }

                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Libelle_Terme Get_LIBELLE_ArchiveById(int id)
        {
            Libelle_Terme objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LIBELLE_TERME_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";


                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new Libelle_Terme();
                        objet.ID = reader.GetInt32(0);
                        if (!reader.IsDBNull(2))
                            objet.Desciption = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.CourtDesc = reader.GetString(3);
                        break;
                    }
                }

                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool LIBELLE_TERM_ADD(Libelle_Terme libelle, int idSite)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LIBELLE_TERME_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inJour", MySqlDbType.Int32);
                    comand.Parameters.Add("inDescription", MySqlDbType.VarChar, 200);
                    comand.Parameters.Add("inShortDesc", MySqlDbType.VarChar, 200);

                    comand.Parameters["inId"].Value = libelle.ID;
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inJour"].Value = libelle.Jour;
                    comand.Parameters["inDescription"].Value = libelle.Desciption;
                    comand.Parameters["inShortDesc"].Value = libelle.CourtDesc;


                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                    isValues = true;
                }
            }
            catch (MySqlException ex)
            {
                isValues = false;
                throw new Exception(ex.Message);

            }
            return isValues;
        }

        public bool LIBELLE_DELETE(int idLibelle)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LIBELLE_TERME_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = idLibelle;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();

                    isValues = true;
                }
            }
            catch (MySqlException ex)
            {
                isValues = false;
                throw new Exception(ex.Message);

            }
            return isValues;
        }
        #endregion

        #region EXONERATION

        public List<Exoneration> GetAll_EXONERATION()
        {
            List<Exoneration> listes = new List<Exoneration>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXONERATION_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = 0;

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Exoneration objet = new Exoneration();
                        objet.ID = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.ShortName = reader.GetString(2);
                        else objet.ShortName = string.Empty;



                        listes.Add(objet);
                    }
                }

                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Exoneration Get_EXONERATIONBYID(Int32 id)
        {
            Exoneration objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXONERATION_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = id;

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new Exoneration();
                        objet.ID = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.ShortName = reader.GetString(2);
                        else objet.ShortName = string.Empty;
                        break;
                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EXONERATION_ADD(Exoneration exo)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXONERATION_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inLibelle", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inShortName", MySqlDbType.VarChar, 100);


                    comand.Parameters["inId"].Value = exo.ID;
                    comand.Parameters["inLibelle"].Value = exo.Libelle;
                    comand.Parameters["inShortName"].Value = exo.ShortName;


                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool EXONERATION_DELETE(int Id)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXONERATION_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = Id;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }
        #endregion

        #region OBJET FACTURE

        public List<ObjetFacture> GetAll_OBJET_FACTURE(Int32 idSite)
        {
            List<ObjetFacture> listes = new List<ObjetFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["inIDClient"].Value = 0;
                    comand.Parameters["isArchive"].Value ="NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ObjetFacture objet = new ObjetFacture();
                        objet.IdObjet = reader.GetInt32(0);
                        objet.IdobjetGen = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.Libelle = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.IdClient = reader.GetInt32(3);

                        if (!reader.IsDBNull(3))
                            objet.Client = GetALL_CLIENTByID(objet.IdClient);
                        objet.ObjetGeneric = GetAll_OBJET_GENERIQUEBYID(objet.IdobjetGen);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ObjetFacture> GetAll_OBJET_Archive_FACTURE(Int32 idSite)
        {
            List<ObjetFacture> listes = new List<ObjetFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["inIDClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ObjetFacture objet = new ObjetFacture();
                        objet.IdObjet = reader.GetInt32(0);
                        objet.IdobjetGen = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.Libelle = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.IdClient = reader.GetInt32(3);

                        if (!reader.IsDBNull(3))
                            objet.Client =GetALL_CLIENTByIDArchive(objet.IdClient,0);
                        objet.ObjetGeneric = GetAll_OBJET_GENERIQUEBYID_Archive(objet.IdobjetGen);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ObjetFacture> GetAll_OBJET_FACTUREBYLangue(Int32 idlangue, Int32 idSite)
        {
            List<ObjetFacture> listes = new List<ObjetFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = idlangue;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["inIDClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ObjetFacture objet = new ObjetFacture();
                        objet.IdObjet = reader.GetInt32(0);
                        objet.IdobjetGen = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.Libelle = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.IdClient = reader.GetInt32(3);

                        if (!reader.IsDBNull(3))
                            objet.Client = GetALL_CLIENTByID(objet.IdClient);
                        objet.ObjetGeneric = GetAll_OBJET_GENERIQUEBYID(objet.IdobjetGen);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ObjetFacture> GetAll_OBJET_Archive_FACTUREBYLangue(Int32 idlangue, Int32 idSite)
        {
            List<ObjetFacture> listes = new List<ObjetFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = idlangue;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["inIDClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ObjetFacture objet = new ObjetFacture();
                        objet.IdObjet = reader.GetInt32(0);
                        objet.IdobjetGen = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.Libelle = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.IdClient = reader.GetInt32(3);

                        if (!reader.IsDBNull(3))
                            objet.Client = GetALL_CLIENTByIDArchive(objet.IdClient,0);
                        objet.ObjetGeneric = GetAll_OBJET_GENERIQUEBYID_Archive(objet.IdobjetGen);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<ObjetFacture> GetAll_OBJET_FACTUREBYByClient(Int32 idSite, Int32 idClient)
        {
            List<ObjetFacture> listes = new List<ObjetFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["inIDClient"].Value = idClient;
                    comand.Parameters["isArchive"].Value = "NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ObjetFacture objet = new ObjetFacture();
                        objet.IdObjet = reader.GetInt32(0);
                        objet.IdobjetGen = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.Libelle = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.IdClient = reader.GetInt32(3);

                        if (!reader.IsDBNull(3))
                            objet.Client = GetALL_CLIENTByID(objet.IdClient);
                        objet.ObjetGeneric = GetAll_OBJET_GENERIQUEBYID(objet.IdobjetGen);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ObjetFacture> GetAll_OBJET_Archive_FACTUREBYByClient(Int32 idSite, Int32 idClient)
        {
            List<ObjetFacture> listes = new List<ObjetFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["inIDClient"].Value = idClient;
                    comand.Parameters["isArchive"].Value = "OUI";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ObjetFacture objet = new ObjetFacture();
                        objet.IdObjet = reader.GetInt32(0);
                        objet.IdobjetGen = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.Libelle = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.IdClient = reader.GetInt32(3);

                        if (!reader.IsDBNull(3))
                            objet.Client = GetALL_CLIENTByIDArchive(objet.IdClient,0);
                        objet.ObjetGeneric = GetAll_OBJET_GENERIQUEBYID_Archive(objet.IdobjetGen);
                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public ObjetFacture GetAll_OBJET_FACTUREBYID(Int32 id, Int32 idSite)
        {
            ObjetFacture objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["inIDClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "NON";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new ObjetFacture();
                        objet.IdObjet = reader.GetInt32(0);
                        objet.IdobjetGen = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.Libelle = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.IdClient = reader.GetInt32(3);

                        if (!reader.IsDBNull(3))
                            objet.Client = GetALL_CLIENTByID(objet.IdClient);
                        objet.ObjetGeneric = GetAll_OBJET_GENERIQUEBYID(objet.IdobjetGen);

                        break;
                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ObjetFacture GetAll_OBJET_Archive_FACTUREBYID(Int32 id, Int32 idSite)
        {
            ObjetFacture objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["inIDClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new ObjetFacture();
                        objet.IdObjet = reader.GetInt32(0);
                        objet.IdobjetGen = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.Libelle = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.IdClient = reader.GetInt32(3);

                        if (!reader.IsDBNull(3))
                            objet.Client = GetALL_CLIENTByIDArchive(objet.IdClient,0);
                        objet.ObjetGeneric = GetAll_OBJET_GENERIQUEBYID_Archive(objet.IdobjetGen);
                        break;

                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ObjetFacture GetAll_OBJET_FACTUREBYID(Int32 id)
        {
            ObjetFacture objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = 0;
                    comand.Parameters["inIDClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "NON";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new ObjetFacture();
                        objet.IdObjet = reader.GetInt32(0);
                        objet.IdobjetGen = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.Libelle = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.IdClient = reader.GetInt32(3);

                        if (!reader.IsDBNull(3))
                            objet.Client = GetALL_CLIENTByID(objet.IdClient);
                        objet.ObjetGeneric = GetAll_OBJET_GENERIQUEBYID(objet.IdobjetGen);


                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ObjetFacture GetAll_OBJET_Archive_FACTUREBYID(Int32 id)
        {
            ObjetFacture objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = 0;
                    comand.Parameters["inIDClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new ObjetFacture();
                        objet.IdObjet = reader.GetInt32(0);
                        objet.IdobjetGen = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.Libelle = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.IdClient = reader.GetInt32(3);

                        if (!reader.IsDBNull(3))
                            objet.Client = new Client();// GetALL_CLIENTByIDArchive(objet.IdClient, 0);
                        objet.ObjetGeneric = GetAll_OBJET_GENERIQUEBYID_Archive(objet.IdobjetGen);
                        break;
                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<ObjetFacture> GetAll_OBJET_FACTUREBYCLIENT(Int32 id, Int32 idClient)
        {
            ObjetFacture objet = null;
            List<ObjetFacture> listes = new List<ObjetFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = id;
                    comand.Parameters["inIDClient"].Value = idClient;
                    comand.Parameters["isArchive"].Value = "NON";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new ObjetFacture();
                        objet.IdObjet = reader.GetInt32(0);
                        objet.IdobjetGen = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.Libelle = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.IdClient = reader.GetInt32(3);

                        if (!reader.IsDBNull(3))
                            objet.Client = GetALL_CLIENTByID(objet.IdClient);
                        objet.ObjetGeneric = GetAll_OBJET_GENERIQUEBYID(objet.IdobjetGen);

                        listes.Add(objet);

                    }


                }

                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ObjetFacture> GetAll_OBJET_Archive_FACTUREBYCLIENT(Int32 id, Int32 idClient)
        {
            ObjetFacture objet = null;
            List<ObjetFacture> listes = new List<ObjetFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = id;
                    comand.Parameters["inIDClient"].Value = idClient;
                    comand.Parameters["isArchive"].Value = "OUI";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new ObjetFacture();
                        objet.IdObjet = reader.GetInt32(0);
                        objet.IdobjetGen = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            objet.Libelle = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.IdClient = reader.GetInt32(3);

                        if (!reader.IsDBNull(3))
                            objet.Client = GetALL_CLIENTByIDArchive(objet.IdClient,0);
                        objet.ObjetGeneric = GetAll_OBJET_GENERIQUEBYID_Archive(objet.IdobjetGen);

                        listes.Add(objet);

                    }


                }

                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool OBJET_FACTURE_ADD(ObjetFacture objFacture, Int32 idSite)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_FACTURE_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdObjet", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = objFacture.IdObjet;
                    comand.Parameters["inIdObjet"].Value = objFacture.IdobjetGen;
                    comand.Parameters["inIDClient"].Value = objFacture.IdClient;
                    comand.Parameters["inIdSte"].Value = idSite;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public int OBJET_FACTURE_DELETE(int Id)
        {
            int isValues = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_FACTURE_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inMessage", MySqlDbType.Int32);
                    comand.Parameters["inMessage"].Direction = ParameterDirection.Output;

                    comand.Parameters["inId"].Value = Id;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                    isValues = int.Parse(comand.Parameters["inMessage"].Value.ToString());
                }

            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }
        #endregion

        #region OBJET GENERIQUE

        public List<ObjetGenerique> GetAll_OBJET_GENERIQUEBYSITE(Int32 idSite)
        {
            List<ObjetGenerique> listes = new List<ObjetGenerique>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar,5);

                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "NON";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ObjetGenerique objet = new ObjetGenerique();
                        objet.IdObjetg = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);

                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt16(2);

                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt32(3);
                      

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ObjetGenerique> GetAll_OBJET_GENERIQUEBYSITE_Archive(Int32 idSite)
        {
            List<ObjetGenerique> listes = new List<ObjetGenerique>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar,5);

                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "OUI";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ObjetGenerique objet = new ObjetGenerique();
                        objet.IdObjetg = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);

                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt16(2);

                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt32(3);
                      

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ObjetGenerique> GetAll_OBJET_GENERIQUELangue(Int32 idSite, Int32 idlangue)
        {
            List<ObjetGenerique> listes = new List<ObjetGenerique>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar,5);

                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = idlangue;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "NON";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ObjetGenerique objet = new ObjetGenerique();
                        objet.IdObjetg = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);

                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt16(2);

                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt32(3);
                      

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ObjetGenerique> GetAll_OBJET_GENERIQUEBY_NON_Client(Int32 idClient, Int32 idSite, Int32 idlangue)
        {
            List<ObjetGenerique> listes = new List<ObjetGenerique>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_OBJET_SELECTBYCLient";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inidClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);

                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inidClient"].Value = idClient;
                    comand.Parameters["inIdSte"].Value = idSite;

                    comand.Parameters["isArchive"].Value = "NON";

                    //inidClient integer,inIdSte integer,isArchive nchar(5))
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ObjetGenerique objet = new ObjetGenerique();
                        objet.IdObjetg = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);

                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt16(2);

                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt32(3);

                        objet.Compte = "";

                        // objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ObjetGenerique> GetAll_OBJET_GENERIQUELangue_Archive(Int32 idSite, Int32 idlangue)
        {
            List<ObjetGenerique> listes = new List<ObjetGenerique>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = idlangue;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "NON";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ObjetGenerique objet = new ObjetGenerique();
                        objet.IdObjetg = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);

                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt16(2);

                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt32(3);
                       

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ObjetGenerique GetAll_OBJET_GENERIQUEBYID(Int32 id)
        {
            ObjetGenerique objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar,5);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = 0;
                    comand.Parameters["isArchive"].Value = "NON";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new ObjetGenerique();
                        objet.IdObjetg = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);

                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt16(2);

                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt32(3);
                      

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);





                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ObjetGenerique GetAll_OBJET_GENERIQUEBYID_Archive(Int32 id)
        {
            ObjetGenerique objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar,5);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new ObjetGenerique();
                        objet.IdObjetg = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);

                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt16(2);

                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt32(3);
                       

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);





                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataTable GetAll_OBJET_GENERIQUEBYID(Int32 id, bool isArchive)
        {
            DataTable dtblResult = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();

                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar,5);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = 0;
                    comand.Parameters["inIDClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "NON";
                    MySqlDataAdapter ada = new MySqlDataAdapter(comand);
                    ada.Fill(dtblResult);

                }


                return dtblResult;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetAll_OBJET_GENERIQUEBYID_Archive(Int32 id, bool isArchive)
        {
            DataTable dtblResult = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();

                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar,5);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = 0;
                    comand.Parameters["inIDClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";
                    MySqlDataAdapter ada = new MySqlDataAdapter(comand);
                    ada.Fill(dtblResult);

                }


                return dtblResult;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public bool OBJET_GENERIQUE_ADD(ObjetGenerique objFacture)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inLabell", MySqlDbType.String);
                    comand.Parameters.Add("inIdlangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inCompte", MySqlDbType.String);

                    comand.Parameters["inId"].Value = objFacture.IdObjetg;
                    comand.Parameters["inLabell"].Value = objFacture.Libelle;
                    comand.Parameters["inIdlangue"].Value = objFacture.IdLangue;
                    comand.Parameters["inIdSte"].Value = objFacture.IdSite;
                    comand.Parameters["inCompte"].Value = objFacture.Compte;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public bool OBJET_GEERIQUE_DELETE(int Id)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_OBJET_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = Id;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        #endregion

        #region EXPLOITATION FACTURE



        public List<ExploitationFacture> GetAll_EXPLOITATION_BYCLIENT(Int32 idSite, Int32 idClient)
        {
            List<ExploitationFacture> listes = new List<ExploitationFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXPLOTATION_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["inIdClient"].Value = idClient;
                    comand.Parameters["isArchive"].Value ="NON" ;

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ExploitationFacture objet = new ExploitationFacture();
                        objet.IdExploitation = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt32(2);
                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt32(3);

                        if (!reader.IsDBNull(4))
                            objet.IdClient = reader.GetInt32(4);
                        if (!reader.IsDBNull(5))
                        {
                            objet.IdCompteTiers = reader.GetInt32(5);
                            objet.CompteAnalityque = new CompteAnalytique(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(5));
                        }

                        if (!reader.IsDBNull(4))
                            objet.Client = GetALL_CLIENTByID(objet.IdClient);
                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ExploitationFacture> GetAll_EXPLOITATION_Archive_BYCLIENT(Int32 idSite, Int32 idClient)
        {
            List<ExploitationFacture> listes = new List<ExploitationFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXPLOTATION_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["inIdClient"].Value = idClient;
                    comand.Parameters["isArchive"].Value = "OUI";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ExploitationFacture objet = new ExploitationFacture();
                        objet.IdExploitation = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt32(2);
                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt32(3);

                        if (!reader.IsDBNull(4))
                            objet.IdClient = reader.GetInt32(4);
                        if (!reader.IsDBNull(5))
                        {
                            objet.IdCompteTiers = reader.GetInt32(5);
                            objet.CompteAnalityque = new CompteAnalytique(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(5));
                        }

                        if (!reader.IsDBNull(4))
                            objet.Client = GetALL_CLIENTByID(objet.IdClient);
                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<ExploitationFacture> GetAll_EXPLOITATION_FACTURE(Int32 idSite)
        {
            List<ExploitationFacture> listes = new List<ExploitationFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXPLOTATION_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["inIdClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "NON";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ExploitationFacture objet = new ExploitationFacture();
                        objet.IdExploitation = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt32(2);
                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt32(3);

                        if (!reader.IsDBNull(4))
                            objet.IdClient = reader.GetInt32(4);

                        if (!reader.IsDBNull(4))
                            objet.Client = GetALL_CLIENTByID(objet.IdClient);

                        if (!reader.IsDBNull(5))
                        {
                            objet.IdCompteTiers = reader.GetInt32(5);
                            objet.CompteAnalityque = new CompteAnalytique(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(5));
                        }

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ExploitationFacture> GetAll_EXPLOITATION_Archive_FACTURE(Int32 idSite)
        {
            List<ExploitationFacture> listes = new List<ExploitationFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXPLOTATION_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["inIdClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ExploitationFacture objet = new ExploitationFacture();
                        objet.IdExploitation = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt32(2);
                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt32(3);

                        if (!reader.IsDBNull(4))
                            objet.IdClient = reader.GetInt32(4);

                        if (!reader.IsDBNull(4))
                            objet.Client = GetALL_CLIENTByIDArchive(objet.IdClient,0);

                        if (!reader.IsDBNull(5))
                        {
                            objet.IdCompteTiers = reader.GetInt32(5);
                            objet.CompteAnalityque = new CompteAnalytique(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(5));
                        }

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ExploitationFacture> GetAll_EXPLOITATION_FACTUREBYLangue(Int32 idlangue, Int32 idSite)
        {
            List<ExploitationFacture> listes = new List<ExploitationFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXPLOTATION_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = idlangue;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["inIdClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "NON";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ExploitationFacture objet = new ExploitationFacture();
                        objet.IdExploitation = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt32(2);
                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            objet.IdClient = reader.GetInt32(4);
                        if (!reader.IsDBNull(4))
                            objet.Client = GetALL_CLIENTByID(objet.IdClient);
                        if (!reader.IsDBNull(5))
                        {
                            objet.IdCompteTiers = reader.GetInt32(5);
                            objet.CompteAnalityque = new CompteAnalytique(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(5));
                        }

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ExploitationFacture> GetAll_EXPLOITATION_Archive_FACTUREBYLangue(Int32 idlangue, Int32 idSite)
        {
            List<ExploitationFacture> listes = new List<ExploitationFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXPLOTATION_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = idlangue;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["inIdClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ExploitationFacture objet = new ExploitationFacture();
                        objet.IdExploitation = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt32(2);
                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            objet.IdClient = reader.GetInt32(4);
                        if (!reader.IsDBNull(4))
                            objet.Client = GetALL_CLIENTByIDArchive(objet.IdClient,0);
                        if (!reader.IsDBNull(5))
                        {
                            objet.IdCompteTiers = reader.GetInt32(5);
                            objet.CompteAnalityque = new CompteAnalytique(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(5));
                        }

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public ExploitationFacture GetAll_EXPLOITATION_FACTUREBYID(int id, Int32 idSite)
        {
            ExploitationFacture objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXPLOTATION_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["inIdClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "NON";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new ExploitationFacture();
                        objet.IdExploitation = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt16(2);
                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt16(3);
                        if (!reader.IsDBNull(4))
                            objet.IdClient = reader.GetInt16(4);
                        if (!reader.IsDBNull(4))
                            objet.Client = GetALL_CLIENTByID(objet.IdClient);

                        if (!reader.IsDBNull(5))
                        {
                            objet.IdCompteTiers = reader.GetInt32(5);
                            objet.CompteAnalityque = new CompteAnalytique(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(5));

                        }
                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ExploitationFacture GetAll_EXPLOITATION_Archive_FACTUREBYID(int id, Int32 idSite)
        {
            ExploitationFacture objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXPLOTATION_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = idSite;
                    comand.Parameters["inIdClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new ExploitationFacture();
                        objet.IdExploitation = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt16(2);
                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt16(3);
                        if (!reader.IsDBNull(4))
                            objet.IdClient = reader.GetInt16(4);
                        if (!reader.IsDBNull(4))
                            objet.Client =new Client() ;// GetALL_CLIENTByIDArchive(objet.IdClient,0);

                        if (!reader.IsDBNull(5))
                        {
                            objet.IdCompteTiers = reader.GetInt32(5);
                            objet.CompteAnalityque = new CompteAnalytique(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(5));

                        }
                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ExploitationFacture GetAll_EXPLOITATION_FACTUREBYID(int id)
        {
            ExploitationFacture objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXPLOTATION_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = 0;
                    comand.Parameters["inIdClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "NON";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new ExploitationFacture();
                        objet.IdExploitation = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt32(2);
                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            objet.IdClient = reader.GetInt32(4);
                        if (!reader.IsDBNull(4))
                            objet.Client = GetALL_CLIENTByID(objet.IdClient);
                        if (!reader.IsDBNull(5))
                        {
                            objet.IdCompteTiers = reader.GetInt32(5);
                            objet.CompteAnalityque = new CompteAnalytique(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(5));
                        }

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ExploitationFacture GetAll_EXPLOITATION_Archive_FACTUREBYID(int id)
        {
            ExploitationFacture objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXPLOTATION_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSte"].Value = 0;
                    comand.Parameters["inIdClient"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new ExploitationFacture();
                        objet.IdExploitation = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt32(2);
                        if (!reader.IsDBNull(3))
                            objet.IdSite = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            objet.IdClient = reader.GetInt32(4);
                       // if (!reader.IsDBNull(4))
                           // objet.Client = GetALL_CLIENTByIDArchive(objet.IdClient,0);
                        if (!reader.IsDBNull(5))
                        {
                            objet.IdCompteTiers = reader.GetInt32(5);
                            objet.CompteAnalityque = new CompteAnalytique(FDataBase.GetConnection(ConnectionString)).SelectByid(reader.GetInt32(5));
                        }

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EXPLOITATION_FACTURE_ADD(ExploitationFacture objFacture)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXPLOTATION_FACTURE_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inLabell", MySqlDbType.String);
                    comand.Parameters.Add("inIdlangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdCompteTiers", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = objFacture.IdExploitation;
                    comand.Parameters["inLabell"].Value = objFacture.Libelle;
                    comand.Parameters["inIdlangue"].Value = objFacture.IdLangue;
                    comand.Parameters["inIdSte"].Value = objFacture.IdSite;
                    comand.Parameters["inIdClient"].Value = objFacture.IdClient;
                    comand.Parameters["inIdCompteTiers"].Value = objFacture.IdCompteTiers;
                    comand.Parameters["inMode"].Value =0;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool EXPLOITATION_FACTURE_ADD_ARCHIVE(ExploitationFacture objFacture)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXPLOTATION_FACTURE_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inLabell", MySqlDbType.String);
                    comand.Parameters.Add("inIdlangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdCompteTiers", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = objFacture.IdExploitation;
                    comand.Parameters["inLabell"].Value = objFacture.Libelle;
                    comand.Parameters["inIdlangue"].Value = objFacture.IdLangue;
                    comand.Parameters["inIdSte"].Value = objFacture.IdSite;
                    comand.Parameters["inIdClient"].Value = objFacture.IdClient;
                    comand.Parameters["inIdCompteTiers"].Value = objFacture.IdCompteTiers;
                    comand.Parameters["inMode"].Value =1;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public bool EXPLOITATION_FACTUREE_ADD(Int64  idFacture, int idexploitation,int mode)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_ExploitationFactureeAdd";
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("intIdFacture", MySqlDbType.Int64);
                    comand.Parameters.Add("inIdExploitation", MySqlDbType.String);
                    comand.Parameters.Add("Ismode", MySqlDbType.Int32);

                    comand.Parameters["intIdFacture"].Value = idFacture;
                    comand.Parameters["inIdExploitation"].Value = idexploitation;
                    comand.Parameters["Ismode"].Value = mode;
                  

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public int EXPLOITATION_FACTURE_DELETE(int Id)
        {
            int isValues = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXPLOTATION_FACTURE_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inMessage", MySqlDbType.Int32);
                    comand.Parameters["inMessage"].Direction = ParameterDirection.Output;
                    comand.Parameters["inId"].Value = Id;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                    isValues = int.Parse(comand.Parameters["inMessage"].Value.ToString());
                }

            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }
        #endregion

        #region SOCIETE


        #region SIGNATURE


        public byte[] Get_SOCIETE_SIGNATURE()
        {

            byte[] imageBytes = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_SOCIETE_SELECT_SIGNATURE;
                    comand.CommandTimeout = Constants.TimeOut;

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                            imageBytes = (byte[])reader.GetValue(0);
                        else imageBytes = null;
                        break;
                    }

                }
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return imageBytes;
        }

        public bool SOCIETE_ADD_SIGNATURE(Int32 idste, byte[] signature)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_SOCIETE_CREATE_SIGNATURE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inSignature", MySqlDbType.LongBlob);

                    comand.Parameters["inId"].Value = idste;
                    comand.Parameters["inSignature"].Value = signature;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();

                    isValues = true;
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;

        }


        public bool SOCIETE_DELETE_SIGNATURE(Int32 idste)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_SOCIETE_DELETE_SIGNATURE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = idste;


                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();

                    isValues = true;
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;

        }


        #endregion

        public Societe Get_SOCIETEById(int id)
        {
            Societe societe = null;
            byte[] imageBytes = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_SOCIETE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = id;

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        societe = new Societe();
                        societe.IdSociete = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            societe.RaisonSocial = reader.GetString(1);
                        else societe.RaisonSocial = string.Empty;

                        if (!reader.IsDBNull(2))
                            societe.TitreManager = reader.GetString(2);
                        else societe.TitreManager = string.Empty;

                        if (!reader.IsDBNull(3))
                            societe.NomManager = reader.GetString(3);
                        else societe.NomManager = string.Empty;

                        if (!reader.IsDBNull(4))
                            societe.NumContribualbe = reader.GetString(4);
                        else societe.NumContribualbe = string.Empty;

                        if (!reader.IsDBNull(5))
                            societe.NumImmatriculation = reader.GetString(5);
                        else societe.NumImmatriculation = string.Empty;

                        if (!reader.IsDBNull(6))
                            societe.Pays = reader.GetString(6);
                        else societe.Pays = string.Empty;

                        if (!reader.IsDBNull(7))
                            societe.Ville = reader.GetString(7);
                        else societe.Ville = string.Empty;

                        if (!reader.IsDBNull(8))
                            societe.Adesse_1 = reader.GetString(8);
                        else societe.Adesse_1 = string.Empty;

                        if (!reader.IsDBNull(9))
                            societe.Adresse_2 = reader.GetString(9);
                        else societe.Adresse_2 = string.Empty;

                        if (!reader.IsDBNull(10))
                            societe.BoitePostal = reader.GetString(10);
                        else societe.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(11))
                            societe.Telephone = reader.GetString(11);
                        else societe.Telephone = string.Empty;

                        if (!reader.IsDBNull(12))
                            societe.Faxe = reader.GetString(12);
                        else societe.Faxe = string.Empty;

                        if (!reader.IsDBNull(13))
                            societe.SiteIntenet = reader.GetString(13);
                        else societe.SiteIntenet = string.Empty;
                        if (!reader.IsDBNull(14))
                            societe.SigleSite = reader.GetString(14);
                        else societe.SigleSite = string.Empty;

                        if (!reader.IsDBNull(15))
                            societe.LogoSociete = (byte[])reader.GetValue(15);
                        else societe.LogoSociete = null;

                        if (!reader.IsDBNull(16))
                            societe.LogoPiedPage = (byte[])reader.GetValue(16);
                        else societe.LogoPiedPage = null;

                        if (!reader.IsDBNull(17))
                            societe.Capital = reader.GetString(17);
                        else societe.Capital = string.Empty;

                        if (!reader.IsDBNull(18))
                            societe.Rc = reader.GetString(18);
                        else societe.Rc = string.Empty;

                        if (!reader.IsDBNull(19))
                            societe.NumeroNif = reader.GetString(19);
                        else societe.NumeroNif = string.Empty;

                        if (!reader.IsDBNull(20))
                            societe.Regime = reader.GetString(20);
                        else societe.Regime = string.Empty;

                        if (!reader.IsDBNull(21))
                            societe.TaxeNu = reader.GetString(21);
                        else societe.TaxeNu = string.Empty;


                        break;
                    }


                }


                return societe;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Societe Get_SOCIETE_FEFAULT()
        {
            Societe societe = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_SOCIETE_SELECT_DEFAULT;
                    comand.CommandTimeout = Constants.TimeOut;

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        societe = new Societe();
                        societe.IdSociete = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            societe.RaisonSocial = reader.GetString(1);
                        else societe.RaisonSocial = string.Empty;

                        if (!reader.IsDBNull(2))
                            societe.TitreManager = reader.GetString(2);
                        else societe.TitreManager = string.Empty;

                        if (!reader.IsDBNull(3))
                            societe.NomManager = reader.GetString(3);
                        else societe.NomManager = string.Empty;

                        if (!reader.IsDBNull(4))
                            societe.NumContribualbe = reader.GetString(4);
                        else societe.NumContribualbe = string.Empty;

                        if (!reader.IsDBNull(5))
                            societe.NumImmatriculation = reader.GetString(5);
                        else societe.NumImmatriculation = string.Empty;

                        if (!reader.IsDBNull(6))
                            societe.Pays = reader.GetString(6);
                        else societe.Pays = string.Empty;

                        if (!reader.IsDBNull(7))
                            societe.Ville = reader.GetString(7);
                        else societe.Ville = string.Empty;

                        if (!reader.IsDBNull(8))
                            societe.Adesse_1 = reader.GetString(8);
                        else societe.Adesse_1 = string.Empty;

                        if (!reader.IsDBNull(9))
                            societe.Adresse_2 = reader.GetString(9);
                        else societe.Adresse_2 = string.Empty;

                        if (!reader.IsDBNull(10))
                            societe.BoitePostal = reader.GetString(10);
                        else societe.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(11))
                            societe.Telephone = reader.GetString(11);
                        else societe.Telephone = string.Empty;

                        if (!reader.IsDBNull(12))
                            societe.Faxe = reader.GetString(12);
                        else societe.Faxe = string.Empty;

                        if (!reader.IsDBNull(13))
                            societe.SiteIntenet = reader.GetString(13);
                        else societe.SiteIntenet = string.Empty;

                        if (!reader.IsDBNull(14))
                            societe.SigleSite = reader.GetString(14);
                        else societe.SigleSite = string.Empty;

                        if (!reader.IsDBNull(15))
                            societe.LogoSociete = (byte[])reader.GetValue(15);
                        else societe.LogoSociete = null;

                        if (!reader.IsDBNull(16))
                            societe.LogoPiedPage = (byte[])reader.GetValue(16);
                        else societe.LogoPiedPage = null;

                        if (!reader.IsDBNull(17))
                            societe.Capital = reader.GetString(17);
                        else societe.Capital = string.Empty;

                        if (!reader.IsDBNull(18))
                            societe.Rc = reader.GetString(18);
                        else societe.Rc = string.Empty;

                        if (!reader.IsDBNull(19))
                            societe.NumeroNif = reader.GetString(19);
                        else societe.NumeroNif = string.Empty;

                        if (!reader.IsDBNull(20))
                            societe.Regime = reader.GetString(20);
                        else societe.Regime = string.Empty;

                        if (!reader.IsDBNull(21))
                            societe.TaxeNu = reader.GetString(21);
                        else societe.TaxeNu = string.Empty;

                        break;
                    }


                }


                return societe;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<Societe> GetAll_SOCIETE_LIST()
        {
            List<Societe> societes = new List<Societe>();
            Societe societe = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_SOCIETE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = 0;

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        societe = new Societe();
                        societe.IdSociete = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            societe.RaisonSocial = reader.GetString(1);
                        else societe.RaisonSocial = string.Empty;

                        if (!reader.IsDBNull(2))
                            societe.TitreManager = reader.GetString(2);
                        else societe.TitreManager = string.Empty;

                        if (!reader.IsDBNull(3))
                            societe.NomManager = reader.GetString(3);
                        else societe.NomManager = string.Empty;

                        if (!reader.IsDBNull(4))
                            societe.NumContribualbe = reader.GetString(4);
                        else societe.NumContribualbe = string.Empty;

                        if (!reader.IsDBNull(5))
                            societe.NumImmatriculation = reader.GetString(5);
                        else societe.NumImmatriculation = string.Empty;

                        if (!reader.IsDBNull(6))
                            societe.Pays = reader.GetString(6);
                        else societe.Pays = string.Empty;

                        if (!reader.IsDBNull(7))
                            societe.Ville = reader.GetString(7);
                        else societe.Ville = string.Empty;

                        if (!reader.IsDBNull(8))
                            societe.Adesse_1 = reader.GetString(8);
                        else societe.Adesse_1 = string.Empty;

                        if (!reader.IsDBNull(9))
                            societe.Adresse_2 = reader.GetString(9);
                        else societe.Adresse_2 = string.Empty;

                        if (!reader.IsDBNull(10))
                            societe.BoitePostal = reader.GetString(10);
                        else societe.BoitePostal = string.Empty;

                        if (!reader.IsDBNull(11))
                            societe.Telephone = reader.GetString(11);
                        else societe.Telephone = string.Empty;

                        if (!reader.IsDBNull(12))
                            societe.Faxe = reader.GetString(12);
                        else societe.Faxe = string.Empty;

                        if (!reader.IsDBNull(13))
                            societe.SiteIntenet = reader.GetString(13);
                        else societe.SiteIntenet = string.Empty;

                        if (!reader.IsDBNull(14))
                            societe.SigleSite = reader.GetString(14);
                        else societe.SigleSite = string.Empty;

                        if (!reader.IsDBNull(15))
                            societe.LogoSociete = (byte[])reader.GetValue(15);
                        else societe.LogoSociete = null;

                        if (!reader.IsDBNull(16))
                            societe.LogoPiedPage = (byte[])reader.GetValue(16);
                        else societe.LogoPiedPage = null;

                        if (!reader.IsDBNull(17))
                            societe.Capital = reader.GetString(17);
                        else societe.Capital = string.Empty;

                        if (!reader.IsDBNull(18))
                            societe.Rc = reader.GetString(18);
                        else societe.Rc = string.Empty;

                        if (!reader.IsDBNull(19))
                            societe.NumeroNif = reader.GetString(19);
                        else societe.NumeroNif = string.Empty;

                        if (!reader.IsDBNull(20))
                            societe.Regime = reader.GetString(20);
                        else societe.Regime = string.Empty;

                        if (!reader.IsDBNull(21))
                            societe.TaxeNu = reader.GetString(21);
                        else societe.TaxeNu = string.Empty;

                        societes.Add(societe);
                    }


                }


                return societes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool SOCIETE_ADD(Societe societe)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_SOCIETE_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inRaisonSocial", MySqlDbType.String);
                    comand.Parameters.Add("inTitreManeger", MySqlDbType.String);
                    comand.Parameters.Add("inNomManager", MySqlDbType.String);
                    comand.Parameters.Add("inNumContrib", MySqlDbType.String);
                    comand.Parameters.Add("inNumImat", MySqlDbType.String);
                    comand.Parameters.Add("inPays", MySqlDbType.String);
                    comand.Parameters.Add("inVille", MySqlDbType.String);
                    comand.Parameters.Add("inAdress1", MySqlDbType.String);
                    comand.Parameters.Add("inAdress2", MySqlDbType.String);
                    comand.Parameters.Add("inBoitePostal", MySqlDbType.String);
                    comand.Parameters.Add("inTelefone", MySqlDbType.String);
                    comand.Parameters.Add("inFaxe", MySqlDbType.String);
                    comand.Parameters.Add("inSite", MySqlDbType.String);
                    comand.Parameters.Add("inSigleSite", MySqlDbType.VarChar, 10);
                    comand.Parameters.Add("inLogo", MySqlDbType.LongBlob);
                    comand.Parameters.Add("inlogoPiedPage", MySqlDbType.LongBlob);
                    comand.Parameters.Add("inCapital", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inRc", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inNumeroNif", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inRegime", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inTaxeNu", MySqlDbType.VarChar, 255);


                    comand.Parameters["inId"].Value = societe.IdSociete;
                    comand.Parameters["inRaisonSocial"].Value = societe.RaisonSocial;
                    comand.Parameters["inTitreManeger"].Value = societe.TitreManager;
                    comand.Parameters["inNomManager"].Value = societe.NomManager;
                    comand.Parameters["inNumContrib"].Value = societe.NumContribualbe;
                    comand.Parameters["inNumImat"].Value = societe.NumImmatriculation;
                    comand.Parameters["inPays"].Value = societe.Pays;
                    comand.Parameters["inVille"].Value = societe.Ville;
                    comand.Parameters["inAdress1"].Value = societe.Adesse_1;
                    comand.Parameters["inAdress2"].Value = societe.Adresse_2;
                    comand.Parameters["inBoitePostal"].Value = societe.BoitePostal;
                    comand.Parameters["inTelefone"].Value = societe.Telephone;
                    comand.Parameters["inFaxe"].Value = societe.Faxe;
                    comand.Parameters["inSite"].Value = societe.SiteIntenet;
                    comand.Parameters["inSigleSite"].Value = societe.SigleSite;
                    comand.Parameters["inLogo"].Value = societe.LogoSociete;
                    comand.Parameters["inlogoPiedPage"].Value = societe.LogoPiedPage;
                    comand.Parameters["inCapital"].Value = societe.Capital;
                    comand.Parameters["inRc"].Value = societe.Rc;
                    comand.Parameters["inNumeroNif"].Value = societe.NumeroNif;
                    comand.Parameters["inRegime"].Value = societe.Regime;
                    comand.Parameters["inTaxeNu"].Value = societe.TaxeNu;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public bool SOCIETE_DELETE_LOGO(int type, int idSociete)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_SOCIETE_DEL_LOGO;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inType", MySqlDbType.Int32);
                    comand.Parameters.Add("inID", MySqlDbType.Int32);

                    comand.Parameters["inType"].Value = type;
                    comand.Parameters["inID"].Value = idSociete;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public bool SOCIETE_DELETE()
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_SOCIETE_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }
        #endregion

        #region TAXES DEVISES


        public List<Taxe> Taxes_SELECT(int id, int idSite)
        {
            List<Taxe> listes = new List<Taxe>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_TAXES_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["idSite"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "NON";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Taxe taxe = new Taxe();
                        taxe.ID_Taxe = reader.GetInt32(0);
                        taxe.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            taxe.Taux = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            taxe.IdSite = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            taxe.Taxedefault = reader.GetBoolean(4);
                        else taxe.Taxedefault = false;

                        listes.Add(taxe);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Taxe> Taxes_SELECTARCHIVE(int id, int idSite)
        {
            List<Taxe> listes = new List<Taxe>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_TAXES_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["idSite"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "OUI";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Taxe taxe = new Taxe();
                        taxe.ID_Taxe = reader.GetInt32(0);
                        taxe.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            taxe.Taux = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            taxe.IdSite = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            taxe.Taxedefault = reader.GetBoolean(4);
                        else taxe.Taxedefault = false;

                        listes.Add(taxe);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Taxe> Taxes_SELECTByRef(string reference, int idSite)
        {
            Taxe taxe = null;
            List<Taxe> listes = new List<Taxe>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_TAXES_SELECTBY_REF";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inRef", MySqlDbType.VarChar,10);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Int32);
                    comand.Parameters["inRef"].Value = reference;
                    comand.Parameters["idSite"].Value = idSite;
                    comand.Parameters["inMode"].Value = 0;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        taxe = new Taxe();
                        taxe.ID_Taxe = reader.GetInt32(0);
                        taxe.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            taxe.Taux = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            taxe.IdSite = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            taxe.Taxedefault = reader.GetBoolean(4);
                        else taxe.Taxedefault = false;
                       
                        listes.Add(taxe);
                    }

                }
                
                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Taxe> Taxes_SELECTByRef_Archive(string reference, int idSite)
        {
            Taxe taxe = null;
            List<Taxe> listes = new List<Taxe>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_TAXES_SELECTBY_REF";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inRef", MySqlDbType.VarChar, 10);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Int32);
                    comand.Parameters["inMode"].Value = 1;
                    comand.Parameters["inRef"].Value = reference;
                    comand.Parameters["idSite"].Value = idSite;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        taxe = new Taxe();
                        taxe.ID_Taxe = reader.GetInt32(0);
                        taxe.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            taxe.Taux = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            taxe.IdSite = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            taxe.Taxedefault = reader.GetBoolean(4);
                        else taxe.Taxedefault = false;

                        listes.Add(taxe);
                    }

                }

                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Taxe Taxes_SELECTById(int id, int idSite)
        {
            Taxe taxe = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_TAXES_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["idSite"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        taxe = new Taxe();
                        taxe.ID_Taxe = reader.GetInt32(0);
                        taxe.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            taxe.Taux = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            taxe.IdSite = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            taxe.Taxedefault = reader.GetBoolean(4);
                        else taxe.Taxedefault = false;

                        break;
                    }


                }


                return taxe;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Taxe Taxes_SELECTByIdArchive(int id, int idSite)
        {
            Taxe taxe = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_TAXES_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["idSite"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "OUI";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        taxe = new Taxe();
                        taxe.ID_Taxe = reader.GetInt32(0);
                        taxe.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            taxe.Taux = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            taxe.IdSite = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            taxe.Taxedefault = reader.GetBoolean(4);
                        else taxe.Taxedefault = false;

                        break;
                    }


                }


                return taxe;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable Taxes_SELECTByDataTAbleArchive(int idSite)
        {
            DataTable tableListe = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_TAXES_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["idSite"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "OUI";
                    MySqlDataAdapter ad = new MySqlDataAdapter(comand);
                    ad.Fill(tableListe);



                }


                return tableListe;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataTable Taxes_SELECTByDataTAble(int idSite)
        {
            DataTable tableListe = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_TAXES_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["idSite"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "NON";
                    MySqlDataAdapter ad = new MySqlDataAdapter(comand);
                    ad.Fill(tableListe);



                }


                return tableListe;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public bool TaxeADD(Taxe taxe)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_TAXES_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inLibelle", MySqlDbType.String);
                    comand.Parameters.Add("inTaux", MySqlDbType.String);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);
                    comand.Parameters.Add("intaxeDefault", MySqlDbType.Bit);

                    comand.Parameters["inId"].Value = taxe.ID_Taxe;
                    comand.Parameters["inLibelle"].Value = taxe.Libelle;
                    comand.Parameters["inTaux"].Value = taxe.Taux;
                    comand.Parameters["idSite"].Value = taxe.IdSite;
                    comand.Parameters["intaxeDefault"].Value = taxe.Taxedefault;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public bool TaxeDELETE(int Id)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_TAXES_DELETES;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = Id;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }



        public List<Devise> GetAllDevise(int idsite)
        {
            List<Devise> devises = new List<Devise>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DEVISES_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar,5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdste"].Value = idsite;
                    comand.Parameters["isArchive"].Value = "NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Devise devise = new Devise();
                        devise.ID_Devise = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            devise.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            devise.Taux = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            devise.IdSite = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            devise.IsDefault = reader.GetBoolean(4);
                        else devise.IsDefault = false;
                        devises.Add(devise);
                    }


                }


                return devises;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Devise> GetAllDeviseArchive(int idsite)
        {
            List<Devise> devises = new List<Devise>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DEVISES_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdste"].Value = idsite;
                    comand.Parameters["isArchive"].Value = "OUI";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Devise devise = new Devise();
                        devise.ID_Devise = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            devise.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            devise.Taux = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            devise.IdSite = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            devise.IsDefault = reader.GetBoolean(4);
                        else devise.IsDefault = false;

                        devises.Add(devise);
                    }


                }


                return devises;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Devise GetAllDeviseById(int id, int idSite)
        {
            Devise devise = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DEVISES_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdste"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        devise = new Devise();
                        devise.ID_Devise = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            devise.Libelle = reader.GetString(1);
                        else devise.Libelle = "";
                        if (!reader.IsDBNull(2))
                            devise.Taux = reader.GetString(2);
                        else devise.Taux = "";
                        if (!reader.IsDBNull(3))
                            devise.IdSite = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            devise.IsDefault = reader.GetBoolean(4);
                        else devise.IsDefault = false;

                        break;
                    }

                }

                return devise;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Devise GetAllDeviseArchiveById(int id, int idSite)
        {
            Devise devise = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DEVISES_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdste"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "OUI";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        devise = new Devise();
                        devise.ID_Devise = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            devise.Libelle = reader.GetString(1);
                        else devise.Libelle = "";
                        if (!reader.IsDBNull(2))
                            devise.Taux = reader.GetString(2);
                        else devise.Taux = "";
                        if (!reader.IsDBNull(3))
                            devise.IdSite = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            devise.IsDefault = reader.GetBoolean(4);
                        else devise.IsDefault = false;
                        break;
                    }

                }

                return devise;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeviseADD(Devise devise)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DEVISES_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inLibelle", MySqlDbType.String);
                    comand.Parameters.Add("inTaux", MySqlDbType.String);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("inDeviseDefault", MySqlDbType.Bit);
                    comand.Parameters["inId"].Value = devise.ID_Devise;
                    comand.Parameters["inLibelle"].Value = devise.Libelle;
                    comand.Parameters["inTaux"].Value = devise.Taux;
                    comand.Parameters["inIdste"].Value = devise.IdSite;
                    comand.Parameters["inDeviseDefault"].Value = devise.IsDefault;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool DeviseDELETE(int Id)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DEVISES_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = Id;
                    comand.Connection = con;

                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        // devise client

        public List<Devise> GetAllDeviseClient(int idsite)
        {
            List<Devise> devises = new List<Devise>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_DEVISES_CLIENT_SELECT";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdste"].Value = idsite;
                    comand.Parameters["isArchive"].Value = "NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Devise devise = new Devise();
                        devise.ID_Devise = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            devise.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            devise.Taux = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            devise.IdSite = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            devise.IsDefault = reader.GetBoolean(4);
                        else devise.IsDefault = false;

                        if (!reader.IsDBNull(5))
                            devise.Sigle = reader.GetString(5);

                        devises.Add(devise);
                    }


                }


                return devises;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Devise GetAllDeviseClientById(int id, int idSite)
        {
            Devise devise = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_DEVISES_CLIENT_SELECT";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdste"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        devise = new Devise();
                        devise.ID_Devise = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            devise.Libelle = reader.GetString(1);
                        else devise.Libelle = "";
                        if (!reader.IsDBNull(2))
                            devise.Taux = reader.GetString(2);
                        else devise.Taux = "";
                        if (!reader.IsDBNull(3))
                            devise.IdSite = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            devise.IsDefault = reader.GetBoolean(4);
                        else devise.IsDefault = false;

                        if (!reader.IsDBNull(5))
                            devise.Sigle = reader.GetString(5);

                        break;
                    }

                }

                return devise;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeviseClientADD(Devise devise)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_DEVISES_CLIENT_ADD";
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inLibelle", MySqlDbType.String);
                    comand.Parameters.Add("inTaux", MySqlDbType.String);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("inDeviseDefault", MySqlDbType.Bit);
                    comand.Parameters.Add("inSigle", MySqlDbType.String);
                    comand.Parameters["inId"].Value = devise.ID_Devise;
                    comand.Parameters["inLibelle"].Value = devise.Libelle;
                    comand.Parameters["inTaux"].Value = devise.Taux;
                    comand.Parameters["inIdste"].Value = devise.IdSite;
                    comand.Parameters["inDeviseDefault"].Value = devise.IsDefault;
                    comand.Parameters["inSigle"].Value = devise.Sigle;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public bool DeviseClientDELETE(int Id)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_DEVISES_CLIENT_DELETE";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = Id;
                    comand.Connection = con;

                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }



        #endregion

        #region PRODUIT

        public DataTable GetAll_PRODUIT_ByCLIENT(Int32 idSite, int idClient)
        {
            DataTable listes = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_PRODUIT_BYCLIENT";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inidSite", MySqlDbType.Int32);

                    comand.Parameters["inIdClient"].Value = idClient;
                    comand.Parameters["inidSite"].Value = idSite;
                    MySqlDataAdapter ada = new MySqlDataAdapter(comand);
                    ada.Fill(listes);
                   
                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Produit> GetAll_PRODUIT(Int32 idSite)
        {
            List<Produit> listes = new List<Produit>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PRODUIT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "NON";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Produit objet = new Produit();
                        objet.IdProduit = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.PrixUnitaire = reader.GetDecimal(2);
                        if (!reader.IsDBNull(3))
                            objet.IdLangue = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            objet.IdSite = reader.GetInt32(4);
                        if (!reader.IsDBNull(5))
                            objet.CompteOhada = reader.GetString(5);
                        if (!reader.IsDBNull(6))
                            objet.CompteExonere = reader.GetString(6);

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        //objet.DetailProduit = GetAll_DETAIL_PRODUITBY_PRODUIT(objet.IdProduit,0);
                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Produit> GetAll_PRODUIT_Archive(Int32 idSite)
        {
            List<Produit> listes = new List<Produit>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PRODUIT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "OUI";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Produit objet = new Produit();
                        objet.IdProduit = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.PrixUnitaire = reader.GetDecimal(2);
                        if (!reader.IsDBNull(3))
                            objet.IdLangue = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            objet.IdSite = reader.GetInt32(4);
                        if (!reader.IsDBNull(5))
                            objet.CompteOhada = reader.GetString(5);
                        if (!reader.IsDBNull(6))
                            objet.CompteExonere = reader.GetString(6);

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        //objet.DetailProduit = GetAll_DETAIL_PRODUITBY_PRODUIT(objet.IdProduit,0);
                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Produit> GetAll_PRODUIT_BYLangue(Int32 idlangue, Int32 idSite)
        {
            List<Produit> listes = new List<Produit>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PRODUIT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = idlangue;
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Produit objet = new Produit();
                        objet.IdProduit = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.PrixUnitaire = reader.GetDecimal(2);
                        if (!reader.IsDBNull(3))
                            objet.IdLangue = reader.GetInt16(3);

                        if (!reader.IsDBNull(4))
                            objet.IdSite = reader.GetInt32(4);
                        if (!reader.IsDBNull(5))
                            objet.CompteOhada = reader.GetString(5);
                        if (!reader.IsDBNull(6))
                            objet.CompteExonere = reader.GetString(6);


                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        //objet.DetailProduit = GetAll_DETAIL_PRODUITBY_PRODUIT(objet.IdProduit, 0);
                        listes.Add(objet);
                    }
                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Produit> GetAll_PRODUIT_BYLangue_Archive(Int32 idlangue, Int32 idSite)
        {
            List<Produit> listes = new List<Produit>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PRODUIT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = idlangue;
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "OUI";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Produit objet = new Produit();
                        objet.IdProduit = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.PrixUnitaire = reader.GetDecimal(2);
                        if (!reader.IsDBNull(3))
                            objet.IdLangue = reader.GetInt16(3);

                        if (!reader.IsDBNull(4))
                            objet.IdSite = reader.GetInt32(4);
                        if (!reader.IsDBNull(5))
                            objet.CompteOhada = reader.GetString(5);
                        if (!reader.IsDBNull(6))
                            objet.CompteExonere = reader.GetString(6);


                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        //objet.DetailProduit = GetAll_DETAIL_PRODUITBY_PRODUIT(objet.IdProduit, 0);
                        listes.Add(objet);
                    }
                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Produit Get_PRODUITBYID(Int32 id)
        {
            Produit objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PRODUIT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSite"].Value = 0;
                    comand.Parameters["isArchive"].Value = "NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new Produit();
                        objet.IdProduit = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.PrixUnitaire = reader.GetDecimal(2);
                        if (!reader.IsDBNull(3))
                            objet.IdLangue = reader.GetInt16(3);

                        if (!reader.IsDBNull(4))
                            objet.IdSite = reader.GetInt32(4);
                        if (!reader.IsDBNull(5))
                            objet.CompteOhada = reader.GetString(5);
                        if (!reader.IsDBNull(6))
                            objet.CompteExonere = reader.GetString(6);

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        //objet.DetailProduit = GetAll_DETAIL_PRODUITBY_PRODUIT(objet.IdProduit, 0);
                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Produit Get_PRODUITBYID_Archive(Int32 id)
        {
            Produit objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PRODUIT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSite"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new Produit();
                        objet.IdProduit = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.PrixUnitaire = reader.GetDecimal(2);
                        if (!reader.IsDBNull(3))
                            objet.IdLangue = reader.GetInt16(3);

                        if (!reader.IsDBNull(4))
                            objet.IdSite = reader.GetInt32(4);
                        if (!reader.IsDBNull(5))
                            objet.CompteOhada = reader.GetString(5);
                        if (!reader.IsDBNull(6))
                            objet.CompteExonere = reader.GetString(6);

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        //objet.DetailProduit = GetAll_DETAIL_PRODUITBY_PRODUIT(objet.IdProduit, 0);
                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Produit Get_PRODUITBYID(Int32 id, Int32 idSite)
        {
            Produit objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PRODUIT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new Produit();
                        objet.IdProduit = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.PrixUnitaire = reader.GetDecimal(2);
                        if (!reader.IsDBNull(3))
                            objet.IdLangue = reader.GetInt16(3);

                        if (!reader.IsDBNull(4))
                            objet.IdSite = reader.GetInt32(4);
                        if (!reader.IsDBNull(5))
                            objet.CompteOhada = reader.GetString(5);
                        if (!reader.IsDBNull(6))
                            objet.CompteExonere = reader.GetString(6);

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        // objet.DetailProduit = GetAll_DETAIL_PRODUITBY_PRODUIT(objet.IdProduit, 0);
                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Produit Get_PRODUITBYID_Archive(Int32 id, Int32 idSite)
        {
            Produit objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PRODUIT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "OUI";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new Produit();
                        objet.IdProduit = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.PrixUnitaire = reader.GetDecimal(2);
                        if (!reader.IsDBNull(3))
                            objet.IdLangue = reader.GetInt16(3);

                        if (!reader.IsDBNull(4))
                            objet.IdSite = reader.GetInt32(4);
                        if (!reader.IsDBNull(5))
                            objet.CompteOhada = reader.GetString(5);
                        if (!reader.IsDBNull(6))
                            objet.CompteExonere = reader.GetString(6);

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        // objet.DetailProduit = GetAll_DETAIL_PRODUITBY_PRODUIT(objet.IdProduit, 0);
                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<Produit> Get_PRODUISEARCH(Int32 idSite, string libelle)
        {
            List<Produit> listes = new List<Produit>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PRODUIT_SELECTBY;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inLibelle", MySqlDbType.VarChar,255);
                 
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inLibelle"].Value = libelle;
                   
                    MySqlDataReader reader = comand.ExecuteReader();

                    Produit objet = null;
                    while (reader.Read())
                    {
                        objet = new Produit();
                        objet.IdProduit = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.PrixUnitaire = reader.GetDecimal(2);
                        if (!reader.IsDBNull(3))
                            objet.IdLangue = reader.GetInt16(3);

                        if (!reader.IsDBNull(4))
                            objet.IdSite = reader.GetInt32(4);
                        if (!reader.IsDBNull(5))
                            objet.CompteOhada = reader.GetString(5);
                        if (!reader.IsDBNull(6))
                            objet.CompteExonere = reader.GetString(6);

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        //objet.DetailProduit = GetAll_DETAIL_PRODUITBY_PRODUIT(objet.IdProduit, 0);
                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool PRODUIT_SUIVI_ADD(int ID,int idClient,int idProduit,int idSite,bool isParam)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_PRODUIT_SUIVI_ADD";
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inidClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inidProduit", MySqlDbType.Int32);
                    comand.Parameters.Add("inidSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inisPram", MySqlDbType.Bit);


                    comand.Parameters["inId"].Value = ID;
                    comand.Parameters["inidClient"].Value = idClient;
                    comand.Parameters["inidProduit"].Value = idProduit;
                    comand.Parameters["inidSite"].Value = idSite;
                    comand.Parameters["inisPram"].Value = isParam;
                  
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool PRODUIT_ADD(Produit objFacture)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PRODUIT_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inLabell", MySqlDbType.String);
                    comand.Parameters.Add("inPrixUnit", MySqlDbType.Float);
                    comand.Parameters.Add("inIdlangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inCompteOhada", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inCompteExonere", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inMode", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = objFacture.IdProduit;
                    comand.Parameters["inLabell"].Value = objFacture.Libelle;
                    comand.Parameters["inPrixUnit"].Value = objFacture.PrixUnitaire;
                    comand.Parameters["inIdlangue"].Value = objFacture.IdLangue;
                    comand.Parameters["inIdSite"].Value = objFacture.IdSite;
                    comand.Parameters["inCompteOhada"].Value = objFacture.CompteOhada;
                    comand.Parameters["inCompteExonere"].Value = objFacture.CompteExonere;
                    comand.Parameters["inMode"].Value = objFacture.ModeFacturation;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public bool PRODUIT_DELETE(int Id)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PRODUIT_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = Id;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        #endregion

        #region DETAIL PRODUIT

        public bool DETAIL_PRODUIT_FACT_EXIST(int idsite, int idDetailProduit)
        {
            bool values = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.F_DETAIL_PRODUIT_FACT_EXIST;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                    comand.Parameters.Add("intIDdetail", MySqlDbType.Int32);
                    comand.Parameters.Add("idValues", MySqlDbType.Bit);
                    comand.Parameters["idValues"].Direction = ParameterDirection.Output;

                    comand.Parameters["inIdsite"].Value = idsite;
                    comand.Parameters["intIDdetail"].Value = idDetailProduit;
                    comand.ExecuteNonQuery();
                    object va = comand.Parameters["idValues"].Value;
                    return values = int.Parse(va.ToString()) == 1 ? true : false;

                }
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ProduitDetail> GetAll_DETAIL_PRODUITBY_PRODUIT(int idProduit, int idClient)
        {
            List<ProduitDetail> listes = new List<ProduitDetail>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DETAIL_PRODUIT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdProduit", MySqlDbType.Int32);
                    comand.Parameters.Add("intIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar,5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdProduit"].Value = idProduit;
                    comand.Parameters["intIdClient"].Value = idClient;
                    comand.Parameters["inIdSite"].Value = 0;
                    comand.Parameters["isArchive"].Value ="NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ProduitDetail objet = new ProduitDetail();

                        if (!reader.IsDBNull(0))
                        {
                            //objet.Quantite = reader.GetFloat(1);

                            objet.IdDetail = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                objet.Quantite = reader.GetFloat(1);
                            if (!reader.IsDBNull(2))
                                objet.PrixUnitaire = reader.GetDecimal(2);
                            if (!reader.IsDBNull(3))
                                objet.IdProduit = reader.GetInt32(3);
                            if (!reader.IsDBNull(4))
                                objet.IdClient = reader.GetInt32(4);
                            if (!reader.IsDBNull(5))
                                objet.Exonerer = reader.GetBoolean(5);


                            if (!reader.IsDBNull(6))
                                objet.NomProduit = reader.GetString(6);

                            if (!reader.IsDBNull(7))
                                objet.IsProrata = reader.GetBoolean(7);
                            if (!reader.IsDBNull(8))
                                objet.CompteOhada = reader.GetString(8);
                            if (!reader.IsDBNull(9))
                                objet.IdSite = reader.GetInt32(9);
                            if (!reader.IsDBNull(10))
                                objet.IdExploitation = reader.GetInt32(10);
                            else objet.IdExploitation = 0;

                            if (!reader.IsDBNull(11))
                                objet.SpecialFact = reader.GetBoolean(11);

                            objet.Client = GetALL_CLIENTByID(objet.IdClient);
                            objet.Produit = Get_PRODUITBYID(objet.IdProduit);

                            if (objet.IdExploitation != 0)
                                objet.Exploitation = GetAll_EXPLOITATION_FACTUREBYID(objet.IdExploitation);
                            else objet.Exploitation = null;

                            listes.Add(objet);
                        }
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ProduitDetail> GetAll_DETAIL_PRODUITBY_PRODUIT_Archive(int idProduit, int idClient)
        {
            List<ProduitDetail> listes = new List<ProduitDetail>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DETAIL_PRODUIT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdProduit", MySqlDbType.Int32);
                    comand.Parameters.Add("intIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdProduit"].Value = idProduit;
                    comand.Parameters["intIdClient"].Value = idClient;
                    comand.Parameters["inIdSite"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ProduitDetail objet = new ProduitDetail();
                        if (!reader.IsDBNull(0))
                        {
                            objet.IdDetail = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                objet.Quantite = reader.GetFloat(1);
                            if (!reader.IsDBNull(2))
                                objet.PrixUnitaire = reader.GetDecimal(2);
                            if (!reader.IsDBNull(3))
                                objet.IdProduit = reader.GetInt32(3);
                            if (!reader.IsDBNull(4))
                                objet.IdClient = reader.GetInt32(4);
                            if (!reader.IsDBNull(5))
                                objet.Exonerer = reader.GetBoolean(5);


                            if (!reader.IsDBNull(6))
                                objet.NomProduit = reader.GetString(6);

                            if (!reader.IsDBNull(7))
                                objet.IsProrata = reader.GetBoolean(7);
                            if (!reader.IsDBNull(8))
                                objet.CompteOhada = reader.GetString(8);
                            if (!reader.IsDBNull(9))
                                objet.IdSite = reader.GetInt32(9);
                            if (!reader.IsDBNull(10))
                                objet.IdExploitation = reader.GetInt32(10);
                            else objet.IdExploitation = 0;

                            if (!reader.IsDBNull(11))
                                objet.SpecialFact = reader.GetBoolean(11);

                            objet.Client = GetALL_CLIENTByIDArchive(objet.IdClient, 0);
                            objet.Produit = Get_PRODUITBYID(objet.IdProduit);

                            if (objet.IdExploitation != 0)
                                objet.Exploitation = GetAll_EXPLOITATION_Archive_FACTUREBYID(objet.IdExploitation);
                            else objet.Exploitation = null;
                        }

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ProduitDetail GetAll_DETAIL_PRODUITBy_Id(int id)
        {
            ProduitDetail objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DETAIL_PRODUIT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdProduit", MySqlDbType.Int32);
                    comand.Parameters.Add("intIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdProduit"].Value = 0;
                    comand.Parameters["intIdClient"].Value = 0;
                    comand.Parameters["inIdSite"].Value = 0;
                    comand.Parameters["isArchive"].Value = "NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new ProduitDetail();
                        if (!reader.IsDBNull(0))
                        {
                            objet.IdDetail = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                objet.Quantite = reader.GetFloat(1);
                            if (!reader.IsDBNull(2))
                                objet.PrixUnitaire = reader.GetDecimal(2);
                            if (!reader.IsDBNull(3))
                                objet.IdProduit = reader.GetInt32(3);
                            if (!reader.IsDBNull(4))
                                objet.IdClient = reader.GetInt32(4);
                            if (!reader.IsDBNull(5))
                                objet.Exonerer = reader.GetBoolean(5);
                            if (!reader.IsDBNull(6))
                                objet.NomProduit = reader.GetString(6);
                            if (!reader.IsDBNull(7))
                                objet.IsProrata = reader.GetBoolean(7);
                            if (!reader.IsDBNull(8))
                                objet.CompteOhada = reader.GetString(8);
                            if (!reader.IsDBNull(9))
                                objet.IdSite = reader.GetInt32(9);
                            if (!reader.IsDBNull(10))
                                objet.IdExploitation = reader.GetInt32(10);
                            else objet.IdExploitation = 0;

                            if (!reader.IsDBNull(11))
                                objet.SpecialFact = reader.GetBoolean(11);

                            // objet.Client = GetALL_CLIENTByID(objet.IdClient);
                            objet.Produit = Get_PRODUITBYID(objet.IdProduit);
                        }
                        break;
                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ProduitDetail GetAll_DETAIL_PRODUITBy_Id_Archive(int id)
        {
            ProduitDetail objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DETAIL_PRODUIT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdProduit", MySqlDbType.Int32);
                    comand.Parameters.Add("intIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdProduit"].Value = 0;
                    comand.Parameters["intIdClient"].Value = 0;
                    comand.Parameters["inIdSite"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new ProduitDetail();
                        if (!reader.IsDBNull(0))
                        {
                            objet.IdDetail = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                objet.Quantite = reader.GetFloat(1);
                            if (!reader.IsDBNull(2))
                                objet.PrixUnitaire = reader.GetDecimal(2);
                            if (!reader.IsDBNull(3))
                                objet.IdProduit = reader.GetInt32(3);
                            if (!reader.IsDBNull(4))
                                objet.IdClient = reader.GetInt32(4);
                            if (!reader.IsDBNull(5))
                                objet.Exonerer = reader.GetBoolean(5);
                            if (!reader.IsDBNull(6))
                                objet.NomProduit = reader.GetString(6);
                            if (!reader.IsDBNull(7))
                                objet.IsProrata = reader.GetBoolean(7);
                            if (!reader.IsDBNull(8))
                                objet.CompteOhada = reader.GetString(8);
                            if (!reader.IsDBNull(9))
                                objet.IdSite = reader.GetInt32(9);
                            if (!reader.IsDBNull(10))
                                objet.IdExploitation = reader.GetInt32(10);
                            else objet.IdExploitation = 0;

                            if (!reader.IsDBNull(11))
                                objet.SpecialFact = reader.GetBoolean(11);

                            // objet.Client = GetALL_CLIENTByID(objet.IdClient);
                            objet.Produit = Get_PRODUITBYID(objet.IdProduit);
                        }
                        break;
                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ProduitDetail> GetAll_DETAIL_PRODUITBY_CLIENT(int idClient, Int32 idSite)
        {
            List<ProduitDetail> listes = new List<ProduitDetail>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DETAIL_PRODUIT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdProduit", MySqlDbType.Int32);
                    comand.Parameters.Add("intIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdProduit"].Value = 0;
                    comand.Parameters["intIdClient"].Value = idClient;
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "NON";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ProduitDetail objet = new ProduitDetail();
                        if (!reader.IsDBNull(0))
                        {
                            objet.IdDetail = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                objet.Quantite = reader.GetFloat(1);
                            if (!reader.IsDBNull(2))
                                objet.PrixUnitaire = reader.GetDecimal(2);
                            if (!reader.IsDBNull(3))
                                objet.IdProduit = reader.GetInt32(3);
                            if (!reader.IsDBNull(4))
                                objet.IdClient = reader.GetInt32(4);
                            if (!reader.IsDBNull(5))
                                objet.Exonerer = reader.GetBoolean(5);


                            if (!reader.IsDBNull(6))
                                objet.NomProduit = reader.GetString(6);

                            if (!reader.IsDBNull(7))
                                objet.IsProrata = reader.GetBoolean(7);
                            if (!reader.IsDBNull(8))
                                objet.CompteOhada = reader.GetString(8);
                            if (!reader.IsDBNull(9))
                                objet.IdSite = reader.GetInt32(9);
                            if (!reader.IsDBNull(10))
                                objet.IdExploitation = reader.GetInt32(10);
                            else objet.IdExploitation = 0;

                            if (!reader.IsDBNull(11))
                                objet.SpecialFact = reader.GetBoolean(11);

                            objet.Client = GetALL_CLIENTByID(objet.IdClient);

                            objet.Produit = Get_PRODUITBYID(objet.IdProduit);
                        }

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ProduitDetail> GetAll_DETAIL_PRODUITBY_CLIENT_Archive(int idClient, Int32 idSite)
        {
            List<ProduitDetail> listes = new List<ProduitDetail>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DETAIL_PRODUIT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdProduit", MySqlDbType.Int32);
                    comand.Parameters.Add("intIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);

                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdProduit"].Value = 0;
                    comand.Parameters["intIdClient"].Value = idClient;
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "OUI";
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ProduitDetail objet = new ProduitDetail();
                        if (!reader.IsDBNull(0))
                        {
                            objet.IdDetail = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                objet.Quantite = reader.GetFloat(1);
                            if (!reader.IsDBNull(2))
                                objet.PrixUnitaire = reader.GetDecimal(2);
                            if (!reader.IsDBNull(3))
                                objet.IdProduit = reader.GetInt32(3);
                            if (!reader.IsDBNull(4))
                                objet.IdClient = reader.GetInt32(4);
                            if (!reader.IsDBNull(5))
                                objet.Exonerer = reader.GetBoolean(5);


                            if (!reader.IsDBNull(6))
                                objet.NomProduit = reader.GetString(6);

                            if (!reader.IsDBNull(7))
                                objet.IsProrata = reader.GetBoolean(7);
                            if (!reader.IsDBNull(8))
                                objet.CompteOhada = reader.GetString(8);
                            if (!reader.IsDBNull(9))
                                objet.IdSite = reader.GetInt32(9);
                            if (!reader.IsDBNull(10))
                                objet.IdExploitation = reader.GetInt32(10);
                            else objet.IdExploitation = 0;

                            if (!reader.IsDBNull(11))
                                objet.SpecialFact = reader.GetBoolean(11);

                            objet.Client = GetALL_CLIENTByIDArchive(objet.IdClient, 0);

                            objet.Produit = Get_PRODUITBYID(objet.IdProduit);
                        }

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PRODUIT_DETAIL_ADD(ProduitDetail detailProd)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DETAIL_PRODUIT_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdProduit", MySqlDbType.Int32);
                    comand.Parameters.Add("inQuantite", MySqlDbType.Float);
                    comand.Parameters.Add("inPrixunit", MySqlDbType.Decimal);
                    comand.Parameters.Add("inExonerate", MySqlDbType.Bit);
                    comand.Parameters.Add("inIsProrata", MySqlDbType.Bit);
                    comand.Parameters.Add("inCompteOhada", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdexploit", MySqlDbType.Int32);
                    comand.Parameters.Add("inSpecialFact", MySqlDbType.Bit);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                   


                    comand.Parameters["inId"].Value = detailProd.IdDetail;
                    comand.Parameters["inIdClient"].Value = detailProd.IdClient;
                    comand.Parameters["inIdProduit"].Value = detailProd.IdProduit;
                    comand.Parameters["inQuantite"].Value = detailProd.Quantite;
                    comand.Parameters["inPrixunit"].Value = detailProd.PrixUnitaire;
                    comand.Parameters["inExonerate"].Value = detailProd.Exonerer;
                    comand.Parameters["inIsProrata"].Value = detailProd.IsProrata;
                    comand.Parameters["inCompteOhada"].Value = detailProd.IsProrata;
                    comand.Parameters["inIdexploit"].Value = detailProd.IdExploitation;
                    comand.Parameters["inSpecialFact"].Value = detailProd.SpecialFact;
                    comand.Parameters["inIdSite"].Value = detailProd.IdSite;
                 


                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public bool DETAIL_PRODUIT_ANNULER_EXPLOITATION(int Id, int idExploit, int idSite)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DETAIL_PRODUIT_ANNUEL_EXPLOIT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIDetail", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdExploitation", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters["inIDetail"].Value = Id;
                    comand.Parameters["inIdExploitation"].Value = idExploit;
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool DETAIL_PRODUIT_DELETE(int Id)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DETAIL_PRODUIT_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = Id;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }
        #endregion

        #region STATUT FACTURE

        public List<StatutFacture> GetAll_STATUT_FACTURE()
        {
            List<StatutFacture> listes = new List<StatutFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_STATUT_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = 0;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        StatutFacture objet = new StatutFacture();
                        objet.IdStatut = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt16(2);
                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        if (!reader.IsDBNull(3))
                            objet.ShortName = reader.GetString(3);
                        else objet.ShortName = string.Empty;

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<StatutFacture> GetAll_STATUT_FACTUREBYLangue(Int32 idlangue)
        {
            List<StatutFacture> listes = new List<StatutFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_STATUT_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = idlangue;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        StatutFacture objet = new StatutFacture();
                        objet.IdStatut = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt16(2);
                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        if (!reader.IsDBNull(3))
                            objet.ShortName = reader.GetString(3);
                        else objet.ShortName = string.Empty;

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public StatutFacture GetAll_STATUT_FACTUREBYID(Int32 id)
        {
            StatutFacture objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_STATUT_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new StatutFacture();
                        objet.IdStatut = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt16(2);
                        if (!reader.IsDBNull(3))
                            objet.ShortName = reader.GetString(3);
                        else objet.ShortName = string.Empty;

                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool STATUT_FACTURE_ADD(StatutFacture objFacture)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_STATUT_FACTURE_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inLabell", MySqlDbType.String);
                    comand.Parameters.Add("inIdlangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inShortName", MySqlDbType.VarChar, 100);
                    //
                    comand.Parameters["inId"].Value = objFacture.IdStatut;
                    comand.Parameters["inLabell"].Value = objFacture.Libelle;
                    comand.Parameters["inIdlangue"].Value = objFacture.IdLangue;
                    comand.Parameters["inShortName"].Value = objFacture.ShortName;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public bool STATUT_FACTURE_DELETE(int Id)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_STATUT_FACTURE_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = Id;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }
        #endregion

        #region REPORT

        public DataTable ReportClient(int idclient)
        {
            DataTable tblClient = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_REPORT_CLIENT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = idclient;
                    comand.Parameters["inMode"].Value = 0;
                    comand.Parameters["inIdFacture"].Value = 0;

                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ReportClientArchive(int idclient, Int64 idFacture)
        {
            DataTable tblClient = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_REPORT_CLIENT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = idclient;
                    comand.Parameters["inMode"].Value = 1;
                    comand.Parameters["inIdFacture"].Value = idFacture;

                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ReportSociete(int id)
        {
            DataTable tblClient = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_REPORT_SOCIETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Int32);
                  
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inMode"].Value = 0;
                  


                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ReportSocieteArchive(int id)
        {
            DataTable tblClient = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_REPORT_SOCIETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Int32);
                  
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inMode"].Value = 1;
                  


                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public double ReportMontantFactureLettre()
        {
            double val = 0;
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_REPORT_MONTANT_LETTRE_FACTURE";
                comand.CommandTimeout = Constants.TimeOut;
                try
                {

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Compte objet = new Compte();

                        if (!reader.IsDBNull(0))
                            val = reader.GetDouble(0);
                        else val = 0;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return val;
        }


        public DataTable ReportPiedPAge(int idSociete)
        {
            DataTable tblClient = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_REPORT_PIEDPAGE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = idSociete;
                    comand.Parameters["inMode"].Value =0;

                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ReportPiedPAgeArchive(int idSociete)
        {
            DataTable tblClient = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_REPORT_PIEDPAGE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = idSociete;
                    comand.Parameters["inMode"].Value =1;

                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataTable ReportLIBELLE(int idlangue)
        {
            DataTable tblClient = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LIBELLE_FACTURE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Int32);
                    comand.Parameters["inIdLangue"].Value = idlangue;
                    comand.Parameters["inMode"].Value = 0;


                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ReportLIBELLEArchive(int idlangue)
        {
            DataTable tblClient = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LIBELLE_FACTURE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inMode", MySqlDbType.Int32);
                    comand.Parameters["inIdLangue"].Value = idlangue;
                    comand.Parameters["inMode"].Value = 1;


                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataTable ReportFACTURE(long idfacture)
        {
            DataTable tblClient = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_REPORT_FACTURE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inID", MySqlDbType.Int64);
                    comand.Parameters["inID"].Value = idfacture;


                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ReportLIGNEFACTURE(long idfacture)
        {
            DataTable tblClient = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_REPORT_LIGNEFACTURE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = idfacture;


                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ReportLIGNEFACTURE_NONEXO_PARTIEL(long idfacture)
        {
            DataTable tblClient = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_REPORT_LIGNEFACTURE_NONEXO;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = idfacture;


                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataSet ReportFACTURENEW(long idfacture,int idSite)
        {
            DataSet tblClient = new DataSet();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_REPORT_FACTURE_VALIDE";
                    comand.CommandTimeout = Constants.TimeOut;

                 
                    comand.Parameters.Add("inID", MySqlDbType.Int64);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters["inID"].Value = idfacture;
                    comand.Parameters["inIdSite"].Value = idSite;


                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataSet ReportFACTURE_AVOIR(long idfacture,int idSite)
        {
            DataSet tblClient = new DataSet();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_REPORT_FACTURE_AVOIR";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inID", MySqlDbType.Int64);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters["inID"].Value = idfacture;
                    comand.Parameters["inIdSite"].Value = idSite;


                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ReportFACTURE_Archive(long idfacture,int idSite)
        {
            DataSet tblClient = new DataSet();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_REPORT_FACTURE_Archive";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inID", MySqlDbType.Int64);
                    comand.Parameters["inID"].Value = idfacture;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters["inIdSite"].Value = idSite;


                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        #endregion

        #region MODE PAIEMENT


        public List<ModePaiement> GetAll_MODE_PAIEMENT()
        {
            List<ModePaiement> listes = new List<ModePaiement>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_MODE_PAIEMENT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = 0;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ModePaiement objet = new ModePaiement();
                        objet.IdMode = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt16(2);
                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ModePaiement> GetAll_MODEPAIEMENT_BYLangue(Int32 idlangue)
        {
            List<ModePaiement> listes = new List<ModePaiement>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_MODE_PAIEMENT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdLangue"].Value = idlangue;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        ModePaiement objet = new ModePaiement();
                        objet.IdMode = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt16(2);
                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        listes.Add(objet);
                    }
                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ModePaiement GetAll_MODE_PAIEMENTBYID(Int32 id)
        {
            ModePaiement objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_MODE_PAIEMENT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdLangue"].Value = 0;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new ModePaiement();
                        objet.IdMode = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.IdLangue = reader.GetInt16(2);
                        objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);

                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool MODE_PAIEMENT_ADD(ModePaiement objFacture)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_MODE_PAIEMENT_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inLabell", MySqlDbType.String);
                    comand.Parameters.Add("inIdlangue", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = objFacture.IdMode;
                    comand.Parameters["inLabell"].Value = objFacture.Libelle;
                    comand.Parameters["inIdlangue"].Value = objFacture.IdLangue;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool MODE_PAIEMENT_DELETE(int Id)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_MODE_PAIEMENT_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = Id;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        #endregion

        #region FACTURE et ARchives
        //
        #region Archives

        public bool IsfactureArchiveExists(int idSite)
        {
            bool valuesReturn = false;
           
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {

                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "F_ARCHIVES_EXISTS";
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters["inIdSite"].Value = idSite;
                    MySqlParameter myRetParam = new MySqlParameter();
                    myRetParam.Direction = System.Data.ParameterDirection.ReturnValue;
                    comand.Parameters.Add(myRetParam);

                    object val = comand.ExecuteScalar();
                    valuesReturn = int.Parse ( myRetParam.Value.ToString())==1?true:false;
                    MySqlDataReader reader = comand.ExecuteReader();
                   
                }

                return valuesReturn;
               
          
        }

        public List<Facture> GetAll_FACTURE_Archive(Int32 idSite, DateTime periode)
        {
            List<Facture> listes = new List<Facture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_FACTURE_Archive_SELECT";
                    comand.CommandTimeout = Constants.TimeOut;
                    //comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("idSite", MySqlDbType.Int64);
                    comand.Parameters.Add("inDate", MySqlDbType.DateTime);
                    comand.Parameters["idSite"].Value = idSite;
                    comand.Parameters["inDate"].Value = periode;

                    MySqlDataReader reader = comand.ExecuteReader();


                    while (reader.Read())
                    {
                        Facture objet = new Facture();
                        objet.IdFacture = reader.GetInt64(0);

                        if (!reader.IsDBNull(1))
                            objet.NumeroFacture = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.DateCreation = reader.GetDateTime(2);
                        if (!reader.IsDBNull(3))
                        {
                            objet.IdCreerpar = reader.GetInt32(3);
                            if (!reader.IsDBNull(4))
                                objet.LibelleUser = reader.GetString(4);
                        }
                        // if (!reader.IsDBNull(7))
                        // objet.IdModifierPar = reader.GetInt32(7);
                        // else objet.IdModifierPar = 0;
                        //if (!reader.IsDBNull(5))
                        // objet.DateModification = reader.GetDateTime(5);
                        // if (!reader.IsDBNull(6))
                        // objet.DateCreation = reader.GetDateTime(6);
                        if (!reader.IsDBNull(15))
                            objet.DateCloture = reader.GetDateTime(15);
                        else objet.DateCloture = null;

                        if (!reader.IsDBNull(5))
                            objet.MoisPrestation = reader.GetDateTime(5);
                        if (!reader.IsDBNull(6))
                        {
                            objet.IdObjetFacture = reader.GetInt32(6);
                            if (!reader.IsDBNull(7))
                                objet.LibelleClientObjet = reader.GetString(7);
                        }


                        //if (!reader.IsDBNull(10))
                        //    objet.IdTaxe = reader.GetInt32(10);
                        //if (!reader.IsDBNull(11))
                        //    objet.IdDevise = reader.GetInt32(11);
                        //if (!reader.IsDBNull(12))
                        //    objet.IdModePaiement = reader.GetInt32(12);
                        if (!reader.IsDBNull(8))
                        {
                            objet.IdStatut = reader.GetInt32(8);
                            if (!reader.IsDBNull(9))
                                objet.LibelleStatut = reader.GetString(9);
                        }

                        if (!reader.IsDBNull(10))
                            objet.LibelleBackgorund = reader.GetString(10);
                        if (!reader.IsDBNull(11))
                            objet.LibelleClientOk = reader.GetString(11);
                        if (!reader.IsDBNull(12))
                            objet.LibelleClientBackgorund = reader.GetString(12);

                        if (!reader.IsDBNull(13))
                        {
                            objet.IdClient = reader.GetInt32(13);
                            if (!reader.IsDBNull(14))
                                objet.LibelleClient = reader.GetString(14);
                        }
                        else objet.IdClient = 0;



                        if (!reader.IsDBNull(16))
                            objet.DateSortie = reader.GetDateTime(16);
                        if (!reader.IsDBNull(17))
                            objet.DateSuspension = reader.GetDateTime(17);
                        if (!reader.IsDBNull(18))
                            objet.IdSite = reader.GetInt32(18);

                        if (!reader.IsDBNull(19))
                            objet.IsProrata = reader.GetBoolean(19);
                        if (!reader.IsDBNull(20))
                            objet.JourFinEcheance = reader.GetString(20);

                        if (!reader.IsDBNull(21))
                            objet.DateDepot = reader.GetDateTime(21);
                        if (!reader.IsDBNull(22))
                            objet.Idexploitation = reader.GetInt32(22);
                        //Libelle_Objet

                        //if (!reader.IsDBNull(22))
                        //    objet.LibelleObjet = reader.GetString(22);
                        //else objet.LibelleObjet = string.Empty;

                        //  if (!reader.IsDBNull(23))
                        //  objet.IdDepartement = reader.GetInt32(23);
                        //else objet.IdDepartement = 0;

                        if (!reader.IsDBNull(23))
                            objet.TotalTTC = reader.GetDouble(23);
                        else objet.TotalTTC = 0;

                        if (!reader.IsDBNull(24))
                            objet.DateNonValide = reader.GetDateTime(24);
                        else objet.DateNonValide = null;

                        if (!reader.IsDBNull(25))
                            objet.DatePaiement = reader.GetDateTime(25);
                        else objet.DatePaiement = null;
                        if (!reader.IsDBNull(26))
                            objet.DateFacture = reader.GetDateTime(26);
                        else objet.DateFacture = null;

                        if (!reader.IsDBNull(27))
                            objet.TotalHT = reader.GetDouble(27);
                        else objet.TotalHT = 0;
                        if (!reader.IsDBNull(28))
                            objet.TotalTVA = reader.GetDouble(28);
                        if (!reader.IsDBNull(29))
                            objet.TotalPRORATA = reader.GetDouble(29);
                        if (!reader.IsDBNull(30))
                            objet.IdClientLangue = reader.GetInt32(30);
                        if (!reader.IsDBNull(31))
                            objet.IdClientExoneration = reader.GetInt32(31);
                        if (!reader.IsDBNull(32))
                            objet.IdDepartement = reader.GetInt32(32);
                        if (!reader.IsDBNull(33))
                            objet.Libelle_Dep = reader.GetString(33);
                        if (!reader.IsDBNull(34))
                            objet.MaregeBeneficiaireId = reader.GetInt32(34);
                        if (!reader.IsDBNull(35))
                            objet.TotalMarge = reader.GetDouble(35);

                        if (!reader.IsDBNull(36))
                            objet.IdTaxe = reader.GetInt32(36);

                        if (!reader.IsDBNull(37))
                            objet.ExploitationIds = reader.GetString(37);
                        if (!reader.IsDBNull(38))
                            objet.ExploitationList = reader.GetString(38);

                        if (!reader.IsDBNull(39))
                            objet.IdModifierPar = reader.GetInt32(39);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<Facture> SEARCH_LIST_FACTURE_Archive(Int32 idSite, DateTime dateDebut, DateTime dateFin, int idClient)
        {
            List<Facture> listes = new List<Facture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_FACTURE_Archive_SEARCH_BYDATE";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inDateDebut", MySqlDbType.DateTime);
                    comand.Parameters.Add("inDateFin", MySqlDbType.DateTime);
                    comand.Parameters.Add("inIdclient", MySqlDbType.Int32);

                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inDateDebut"].Value = dateDebut;
                    comand.Parameters["inDateFin"].Value = dateFin;
                    comand.Parameters["inIdclient"].Value = idClient;


                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Facture objet = new Facture();
                        objet.IdFacture = reader.GetInt64(0);

                        if (!reader.IsDBNull(1))
                            objet.NumeroFacture = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.DateCreation = reader.GetDateTime(2);
                        if (!reader.IsDBNull(3))
                        {
                            objet.IdCreerpar = reader.GetInt32(3);
                            if (!reader.IsDBNull(4))
                                objet.LibelleUser = reader.GetString(4);
                        }
                        if (!reader.IsDBNull(39))
                            objet.IdModifierPar = reader.GetInt32(39);
                        else objet.IdModifierPar = 0;
                        //if (!reader.IsDBNull(5))
                        // objet.DateModification = reader.GetDateTime(5);
                        // if (!reader.IsDBNull(6))
                        // objet.DateCreation = reader.GetDateTime(6);
                        if (!reader.IsDBNull(15))
                            objet.DateCloture = reader.GetDateTime(15);
                        else objet.DateCloture = null;

                        if (!reader.IsDBNull(5))
                            objet.MoisPrestation = reader.GetDateTime(5);

                        if (!reader.IsDBNull(6))
                        {
                            objet.IdObjetFacture = reader.GetInt32(6);

                            if (!reader.IsDBNull(7))
                                objet.LibelleClientObjet = reader.GetString(7);
                        }


                        //if (!reader.IsDBNull(10))
                        //    objet.IdTaxe = reader.GetInt32(10);
                        //if (!reader.IsDBNull(11))
                        //    objet.IdDevise = reader.GetInt32(11);
                        //if (!reader.IsDBNull(12))
                        //    objet.IdModePaiement = reader.GetInt32(12);
                        if (!reader.IsDBNull(8))
                        {
                            objet.IdStatut = reader.GetInt32(8);
                            if (!reader.IsDBNull(9))
                                objet.LibelleStatut = reader.GetString(9);
                        }

                        if (!reader.IsDBNull(10))
                            objet.LibelleBackgorund = reader.GetString(10);
                        if (!reader.IsDBNull(11))
                            objet.LibelleClientOk = reader.GetString(11);
                        if (!reader.IsDBNull(12))
                            objet.LibelleClientBackgorund = reader.GetString(12);

                        if (!reader.IsDBNull(13))
                        {
                            objet.IdClient = reader.GetInt32(13);
                            if (!reader.IsDBNull(14))
                                objet.LibelleClient = reader.GetString(14);
                        }
                        else objet.IdClient = 0;



                        if (!reader.IsDBNull(16))
                            objet.DateSortie = reader.GetDateTime(16);
                        if (!reader.IsDBNull(17))
                            objet.DateSuspension = reader.GetDateTime(17);
                        if (!reader.IsDBNull(18))
                            objet.IdSite = reader.GetInt32(18);

                        if (!reader.IsDBNull(19))
                            objet.IsProrata = reader.GetBoolean(19);
                        if (!reader.IsDBNull(20))
                            objet.JourFinEcheance = reader.GetString(20);

                        if (!reader.IsDBNull(21))
                            objet.DateDepot = reader.GetDateTime(21);
                        if (!reader.IsDBNull(22))
                            objet.Idexploitation = reader.GetInt32(22);
                        //Libelle_Objet

                        //if (!reader.IsDBNull(22))
                        //    objet.LibelleObjet = reader.GetString(22);
                        //else objet.LibelleObjet = string.Empty;

                        //if (!reader.IsDBNull(23))
                        //    objet.IdDepartement = reader.GetInt32(23);
                        //else objet.IdDepartement = 0;

                        if (!reader.IsDBNull(23))
                            objet.TotalTTC = reader.GetDouble(23);
                        else objet.TotalTTC = 0;

                        if (!reader.IsDBNull(24))
                            objet.DateNonValide = reader.GetDateTime(24);
                        else objet.DateNonValide = null;

                        if (!reader.IsDBNull(25))
                            objet.DatePaiement = reader.GetDateTime(25);
                        else objet.DatePaiement = null;
                        if (!reader.IsDBNull(26))
                            objet.DateFacture = reader.GetDateTime(26);
                        else objet.DateFacture = null;

                        if (!reader.IsDBNull(27))
                            objet.TotalHT = reader.GetDouble(27);
                        else objet.TotalHT = 0;
                        if (!reader.IsDBNull(28))
                            objet.TotalTVA = reader.GetDouble(28);
                        if (!reader.IsDBNull(29))
                            objet.TotalPRORATA = reader.GetDouble(29);
                        if (!reader.IsDBNull(30))
                            objet.IdClientLangue = reader.GetInt32(30);
                        if (!reader.IsDBNull(31))
                            objet.IdClientExoneration = reader.GetInt32(31);
                        if (!reader.IsDBNull(32))
                            objet.IdDepartement = reader.GetInt32(32);
                        if (!reader.IsDBNull(33))
                            objet.Libelle_Dep = reader.GetString(33);
                        if (!reader.IsDBNull(34))
                            objet.MaregeBeneficiaireId = reader.GetInt32(34);
                        if (!reader.IsDBNull(35))
                            objet.TotalMarge = reader.GetDouble(35);

                        if (!reader.IsDBNull(36))
                            objet.IdTaxe = reader.GetInt32(36);

                        if (!reader.IsDBNull(37))
                            objet.ExploitationIds = reader.GetString(37);
                        if (!reader.IsDBNull(38))
                            objet.ExploitationList = reader.GetString(38);

                        if (!reader.IsDBNull(39))
                            objet.IdModifierPar = reader.GetInt32(39);

                        if (!reader.IsDBNull(40))
                            objet.IdProrata = reader.GetInt32(40);
                        else objet.IdProrata = 0;

                        if (!reader.IsDBNull(41))
                            objet.OrderNumber = reader.GetString(41);
                        if (!reader.IsDBNull(42))
                            objet.CompteTier = reader.GetString(42);
                        if (!reader.IsDBNull(43))
                            objet.CompteAnalytique = reader.GetString(43);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable Facture_Periode_Archive(int IdSite)
        {
            DataTable tableResult = new DataTable();

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_FACTURE_Archive_Periode";
                comand.CommandTimeout = Constants.TimeOut;

                comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                comand.Parameters["inIdSite"].Value = IdSite;
                MySqlDataAdapter adapter = new MySqlDataAdapter(comand);
                adapter.Fill(tableResult);

                return tableResult;


            }

        }
      

        #endregion


        public DataTable FactureVersion(Int64 idFacture)
        {
            DataTable tableResult = new DataTable();

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.Ps_FactureGetVersionById;
                comand.CommandTimeout = Constants.TimeOut;

                comand.Parameters.Add("IdFacture", MySqlDbType.Int64);
                comand.Parameters["IdFacture"].Value = idFacture;
                MySqlDataReader reader = comand.ExecuteReader();

                DataColumn col1 = new DataColumn("idfacture", typeof(Int64));
                DataColumn col2 = new DataColumn("iduser", typeof(int));
                DataColumn col3 = new DataColumn("Nom", typeof(string));
                DataColumn col4 = new DataColumn("Prenom", typeof(string));
                DataColumn col5 = new DataColumn("Date_Modification", typeof(DateTime));
                DataColumn col6 = new DataColumn("version", typeof(DateTime));
                DataColumn col7 = new DataColumn("statuts", typeof(string));
                tableResult.Columns.Add(col1);
                tableResult.Columns.Add(col2);
                tableResult.Columns.Add(col3);
                tableResult.Columns.Add(col4);
                tableResult.Columns.Add(col5);
                tableResult.Columns.Add(col6);
                tableResult.Columns.Add(col7);
                DataRow row = null;


                while (reader.Read())
                {
                    row = tableResult.NewRow();

                    row[0] = reader.GetInt64(0);
                    if (!reader.IsDBNull(1))
                        row[1] = reader.GetInt32(1);
                    else row[1] = 0;
                    if (!reader.IsDBNull(2))
                        row[2] = reader.GetString(2);
                    else row[2] = "";
                    if (!reader.IsDBNull(3))
                        row[3] = reader.GetString(3);
                    else row[3] = "";

                    if (!reader.IsDBNull(4))
                        row[4] = reader.GetDateTime(4);


                    MySql.Data.Types.MySqlDateTime vamp = reader.GetMySqlDateTime(5);
                    DateTime localVal;
                    if (DateTime.TryParse(vamp.ToString(), out  localVal))
                        row[5] = localVal;

                    if (!reader.IsDBNull(6))
                        row[6] = reader.GetString(6);
                    else row[6] = "";

                    tableResult.Rows.Add(row);

                    break;
                }

                return tableResult;


            }

        }

        public DataTable FactureList(Int32 idSite)
        {
            DataTable tableResult = new DataTable();

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.Ps_FactureListBySite;
                comand.CommandTimeout = Constants.TimeOut;

                comand.Parameters.Add("idSite", MySqlDbType.Int16);
                comand.Parameters["idSite"].Value = idSite;
                MySqlDataAdapter adapter = new MySqlDataAdapter(comand);
                adapter.Fill(tableResult);
                return tableResult;

            }

        }

        public DataRow FactureByID(Int64 ID)
        {
            DataTable tableResult = new DataTable();

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.Ps_FactureListById;
                comand.CommandTimeout = Constants.TimeOut;

                comand.Parameters.Add("inID", MySqlDbType.Int64);
                comand.Parameters["inID"].Value = ID;
                MySqlDataAdapter adapter = new MySqlDataAdapter(comand);
                adapter.Fill(tableResult);
                return tableResult.Rows[0];

            }

        }

        public DataTable FactureListeByDate(Int32 idSite, DateTime dateDebut, DateTime dateFin, int idClient)
        {
            DataTable tableResult = new DataTable();

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {

                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.Ps_FactureListeByDate;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                comand.Parameters.Add("inDateDebut", MySqlDbType.DateTime);
                comand.Parameters.Add("inDateFin", MySqlDbType.DateTime);
                comand.Parameters.Add("inIdclient", MySqlDbType.Int32);

                comand.Parameters["inIdSite"].Value = idSite;
                comand.Parameters["inDateDebut"].Value = dateDebut;
                comand.Parameters["inDateFin"].Value = dateFin;
                comand.Parameters["inIdclient"].Value = idClient;

                MySqlDataAdapter adapter = new MySqlDataAdapter(comand);
                adapter.Fill(tableResult);
                return tableResult;
            }


        }

        public List<Facture> GetAll_FACTURE(Int32 idSite)
        {
            List<Facture> listes = new List<Facture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("idSite", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["idSite"].Value = idSite;

                    MySqlDataReader reader = comand.ExecuteReader();


                    while (reader.Read())
                    {
                        Facture objet = new Facture();
                        objet.IdFacture = reader.GetInt64(0);

                        if (!reader.IsDBNull(1))
                            objet.NumeroFacture = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.DateCreation = reader.GetDateTime(2);
                        if (!reader.IsDBNull(3))
                            objet.IdCreerpar = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            objet.IdModifierPar = reader.GetInt32(4);
                        else objet.IdModifierPar = 0;
                        //if (!reader.IsDBNull(5))
                        //    objet.DateModification = reader.GetDateTime(5);
                        if (!reader.IsDBNull(6))
                            objet.DateCreation = reader.GetDateTime(6);
                        if (!reader.IsDBNull(7))
                            objet.DateCloture = reader.GetDateTime(7);
                        else objet.DateCloture = null;

                        if (!reader.IsDBNull(8))
                            objet.MoisPrestation = reader.GetDateTime(8);
                        if (!reader.IsDBNull(9))
                            objet.IdObjetFacture = reader.GetInt32(9);
                        if (!reader.IsDBNull(10))
                            objet.IdTaxe = reader.GetInt32(10);
                        if (!reader.IsDBNull(11))
                            objet.IdDevise = reader.GetInt32(11);
                        if (!reader.IsDBNull(12))
                            objet.IdModePaiement = reader.GetInt32(12);
                        if (!reader.IsDBNull(13))
                            objet.IdStatut = reader.GetInt32(13);
                        if (!reader.IsDBNull(14))
                            objet.IdClient = reader.GetInt32(14);
                        else objet.IdClient = 0;



                        if (!reader.IsDBNull(15))
                            objet.DateSortie = reader.GetDateTime(15);
                        if (!reader.IsDBNull(16))
                            objet.DateSuspension = reader.GetDateTime(16);

                        if (!reader.IsDBNull(17))
                            objet.IdSite = reader.GetInt32(17);
                        if (!reader.IsDBNull(18))
                            objet.IsProrata = reader.GetBoolean(18);
                        if (!reader.IsDBNull(19))
                            objet.JourFinEcheance = reader.GetString(19);

                        if (!reader.IsDBNull(20))
                            objet.DateDepot = reader.GetDateTime(20);
                        if (!reader.IsDBNull(21))
                            objet.Idexploitation = reader.GetInt32(21);
                        //Libelle_Objet

                        if (!reader.IsDBNull(22))
                            objet.LibelleObjet = reader.GetString(22);
                        else objet.LibelleObjet = string.Empty;

                        if (!reader.IsDBNull(23))
                            objet.IdDepartement = reader.GetInt32(23);
                        else objet.IdDepartement = 0;

                        if (!reader.IsDBNull(24))
                            objet.TotalTTC = reader.GetDouble(24);
                        else objet.TotalTTC = 0;

                        if (!reader.IsDBNull(25))
                            objet.DateNonValide = reader.GetDateTime(25);
                        else objet.DateNonValide = null;

                        if (!reader.IsDBNull(26))
                            objet.DatePaiement = reader.GetDateTime(26);
                        else objet.DatePaiement = null;
                        if (!reader.IsDBNull(27))
                            objet.DateFacture = reader.GetDateTime(27);
                        else objet.DateFacture = null;

                        if (!reader.IsDBNull(28))
                            objet.TotalHT = reader.GetDouble(28);
                        else objet.TotalHT = 0;
                        if (!reader.IsDBNull(29))
                            objet.TotalTVA = reader.GetDouble(29);
                        if (!reader.IsDBNull(30))
                            objet.TotalPRORATA = reader.GetDouble(30);

                        if (!reader.IsDBNull(31))
                            objet.ExploitationIds = reader.GetString(31);
                        if (!reader.IsDBNull(32))
                            objet.ExploitationList = reader.GetString(32);
                        if (!reader.IsDBNull(33))
                            objet.MaregeBeneficiaireId = reader.GetInt32(33);
                        else objet.MaregeBeneficiaireId = 0;
                        if (!reader.IsDBNull(34))
                            objet.TotalMarge = reader.GetDouble(34);

                        if (!reader.IsDBNull(35))
                            objet.OrderNumber = reader.GetString(35);

                        //objet.CurrentExploitationFacture = GetAll_EXPLOITATION_FACTUREBYID(objet.Idexploitation);

                        //objet.CurrentClient = GetALL_CLIENTByID(objet.IdClient);

                        //objet.CurrentObjetFacture = GetAll_OBJET_FACTUREBYID(objet.IdObjetFacture);
                        //objet.CurrentModePaiement = GetAll_MODE_PAIEMENTBYID(objet.IdModePaiement);
                        objet.CurrentStatut = GetAll_STATUT_FACTUREBYID(objet.IdStatut);
                        //objet.CurrentTaxe = Taxes_SELECTById(objet.IdTaxe, idSite);
                        //objet.CurrentDevise = GetAllDeviseById(objet.IdDevise, idSite);
                        //objet.UserCreate = GetUtilisateur_ById(objet.IdCreerpar);


                        //if (objet.IdDepartement != null && objet.IdDepartement > 0)
                        //{
                        //    objet.CurrentDepartement = Get_DEPARTMENTSByID((int)objet.IdDepartement);
                        //    objet.Libelle_Dep = objet.CurrentDepartement.Libelle;
                        //}
                        //else objet.Libelle_Dep = string.Empty;

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public List<Facture> GetAll_FACTURENEW(Int32 idSite,bool modefacture)
        {
            List<Facture> listes = new List<Facture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_FACTURE_SELECT_NEW";
                    comand.CommandTimeout = Constants.TimeOut;
                    //comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("idSite", MySqlDbType.Int64);
                    comand.Parameters.Add("inMode", MySqlDbType.Bit);
                    comand.Parameters["idSite"].Value = idSite;
                    comand.Parameters["inMode"].Value = modefacture;

                    MySqlDataReader reader = comand.ExecuteReader();
                    

                    while (reader.Read())
                    {
                        Facture objet = new Facture();
                        objet.IdFacture = reader.GetInt64(0);

                        if (!reader.IsDBNull(1))
                            objet.NumeroFacture = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.DateCreation = reader.GetDateTime(2);
                        if (!reader.IsDBNull(3))
                        {
                            objet.IdCreerpar = reader.GetInt32(3);
                            if (!reader.IsDBNull(4))
                            objet.LibelleUser = reader.GetString(4);
                        }
                       // if (!reader.IsDBNull(7))
                           // objet.IdModifierPar = reader.GetInt32(7);
                       // else objet.IdModifierPar = 0;
                        //if (!reader.IsDBNull(5))
                          // objet.DateModification = reader.GetDateTime(5);
                       // if (!reader.IsDBNull(6))
                           // objet.DateCreation = reader.GetDateTime(6);
                        if (!reader.IsDBNull(15))
                            objet.DateCloture = reader.GetDateTime(15);
                        else objet.DateCloture = null;

                        if (!reader.IsDBNull(5))
                            objet.MoisPrestation = reader.GetDateTime(5);
                        if (!reader.IsDBNull(6))
                        {
                            objet.IdObjetFacture = reader.GetInt32(6);
                            if (!reader.IsDBNull(7))
                            objet.LibelleClientObjet = reader.GetString(7);
                        }
                          
                        
                        //if (!reader.IsDBNull(10))
                        //    objet.IdTaxe = reader.GetInt32(10);
                        //if (!reader.IsDBNull(11))
                        //    objet.IdDevise = reader.GetInt32(11);
                        //if (!reader.IsDBNull(12))
                        //    objet.IdModePaiement = reader.GetInt32(12);
                        if (!reader.IsDBNull(8))
                        {
                            objet.IdStatut = reader.GetInt32(8);
                            if (!reader.IsDBNull(9))
                                objet.LibelleStatut = reader.GetString(9);
                        }

                        if (!reader.IsDBNull(10))
                            objet.LibelleBackgorund = reader.GetString(10);
                        if (!reader.IsDBNull(11))
                            objet.LibelleClientOk = reader.GetString(11);
                        if (!reader.IsDBNull(12))
                            objet.LibelleClientBackgorund = reader.GetString(12);

                        if (!reader.IsDBNull(13))
                        {
                            objet.IdClient = reader.GetInt32(13);
                            if (!reader.IsDBNull(14))
                            objet.LibelleClient = reader.GetString(14);
                        }
                        else objet.IdClient = 0;
                       


                        if (!reader.IsDBNull(16))
                            objet.DateSortie = reader.GetDateTime(16);
                        if (!reader.IsDBNull(17))
                            objet.DateSuspension = reader.GetDateTime(17);
                        if (!reader.IsDBNull(18))
                            objet.IdSite = reader.GetInt32(18);

                        if (!reader.IsDBNull(19))
                            objet.IsProrata = reader.GetBoolean(19);
                        if (!reader.IsDBNull(20))
                            objet.JourFinEcheance = reader.GetString(20);

                        if (!reader.IsDBNull(21))
                            objet.DateDepot = reader.GetDateTime(21);
                        if (!reader.IsDBNull(22))
                            objet.Idexploitation = reader.GetInt32(22);
                        //Libelle_Objet

                        //if (!reader.IsDBNull(22))
                        //    objet.LibelleObjet = reader.GetString(22);
                        //else objet.LibelleObjet = string.Empty;

                      //  if (!reader.IsDBNull(23))
                         //  objet.IdDepartement = reader.GetInt32(23);
                        //else objet.IdDepartement = 0;

                        if (!reader.IsDBNull(23))
                            objet.TotalTTC = reader.GetDouble(23);
                        else objet.TotalTTC = 0;

                        if (!reader.IsDBNull(24))
                            objet.DateNonValide = reader.GetDateTime(24);
                        else objet.DateNonValide = null;

                        if (!reader.IsDBNull(25))
                            objet.DatePaiement = reader.GetDateTime(25);
                        else objet.DatePaiement = null;
                        if (!reader.IsDBNull(26))
                            objet.DateFacture = reader.GetDateTime(26);
                        else objet.DateFacture = null;

                        if (!reader.IsDBNull(27))
                            objet.TotalHT = reader.GetDouble(27);
                        else objet.TotalHT = 0;
                        if (!reader.IsDBNull(28))
                        objet.TotalTVA = reader.GetDouble(28);
                        if (!reader.IsDBNull(29))
                        objet.TotalPRORATA = reader.GetDouble(29);
                        if (!reader.IsDBNull(30))
                            objet.IdClientLangue = reader.GetInt32(30);
                        if (!reader.IsDBNull(31))
                            objet.IdClientExoneration = reader.GetInt32(31);
                        if (!reader.IsDBNull(32))
                            objet.IdDepartement = reader.GetInt32(32);
                        if (!reader.IsDBNull(33))
                            objet.Libelle_Dep = reader.GetString(33);
                        if (!reader.IsDBNull(34))
                            objet.MaregeBeneficiaireId = reader.GetInt32(34);
                        if (!reader.IsDBNull(35))
                            objet.TotalMarge = reader.GetDouble(35);

                        if (!reader.IsDBNull(36))
                            objet.IdTaxe = reader.GetInt32(36);

                        if (!reader.IsDBNull(37))
                            objet.ExploitationIds = reader.GetString(37);
                        if (!reader.IsDBNull(38))
                            objet.ExploitationList = reader.GetString(38);

                        if (!reader.IsDBNull(39))
                            objet.IdModifierPar = reader.GetInt32(39);

                        if (!reader.IsDBNull(40))
                            objet.IdProrata = reader.GetInt32(40);
                        else objet.IdProrata = 0;

                        if (!reader.IsDBNull(41))
                            objet.OrderNumber = reader.GetString(41);
                        if (!reader.IsDBNull(42))
                            objet.CompteTier = reader.GetString(42);
                        if (!reader.IsDBNull(43))
                            objet.CompteAnalytique = reader.GetString(43);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<Facture> SEARCH_FACTURE_BYNUM(string numFacture, string mode)
        {
            List<Facture> listes = new List<Facture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_FACTURE_SEARCH_BY";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inNumFacture", MySqlDbType.VarChar, 100);
                    comand.Parameters.Add("inMode", MySqlDbType.VarChar, 5);


                    comand.Parameters["inNumFacture"].Value = numFacture;
                    comand.Parameters["inMode"].Value = mode;


                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Facture objet = new Facture();
                        objet.IdFacture = reader.GetInt64(0);

                        if (!reader.IsDBNull(1))
                            objet.NumeroFacture = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.DateCreation = reader.GetDateTime(2);
                        if (!reader.IsDBNull(3))
                        {
                            objet.IdCreerpar = reader.GetInt32(3);
                            if (!reader.IsDBNull(4))
                                objet.LibelleUser = reader.GetString(4);
                        }
                        if (!reader.IsDBNull(39))
                            objet.IdModifierPar = reader.GetInt32(39);
                        else objet.IdModifierPar = 0;
                        //if (!reader.IsDBNull(5))
                        // objet.DateModification = reader.GetDateTime(5);
                        // if (!reader.IsDBNull(6))
                        // objet.DateCreation = reader.GetDateTime(6);
                        if (!reader.IsDBNull(15))
                            objet.DateCloture = reader.GetDateTime(15);
                        else objet.DateCloture = null;

                        if (!reader.IsDBNull(5))
                            objet.MoisPrestation = reader.GetDateTime(5);
                        if (!reader.IsDBNull(6))
                        {
                            objet.IdObjetFacture = reader.GetInt32(6);
                            if (!reader.IsDBNull(7))
                                objet.LibelleClientObjet = reader.GetString(7);
                        }


                        //if (!reader.IsDBNull(10))
                        //    objet.IdTaxe = reader.GetInt32(10);
                        //if (!reader.IsDBNull(11))
                        //    objet.IdDevise = reader.GetInt32(11);
                        //if (!reader.IsDBNull(12))
                        //    objet.IdModePaiement = reader.GetInt32(12);
                        if (!reader.IsDBNull(8))
                        {
                            objet.IdStatut = reader.GetInt32(8);
                            if (!reader.IsDBNull(9))
                                objet.LibelleStatut = reader.GetString(9);
                        }

                        if (!reader.IsDBNull(10))
                            objet.LibelleBackgorund = reader.GetString(10);
                        if (!reader.IsDBNull(11))
                            objet.LibelleClientOk = reader.GetString(11);
                        if (!reader.IsDBNull(12))
                            objet.LibelleClientBackgorund = reader.GetString(12);

                        if (!reader.IsDBNull(13))
                        {
                            objet.IdClient = reader.GetInt32(13);
                            if (!reader.IsDBNull(14))
                                objet.LibelleClient = reader.GetString(14);
                        }
                        else objet.IdClient = 0;



                        if (!reader.IsDBNull(16))
                            objet.DateSortie = reader.GetDateTime(16);
                        if (!reader.IsDBNull(17))
                            objet.DateSuspension = reader.GetDateTime(17);
                        if (!reader.IsDBNull(18))
                            objet.IdSite = reader.GetInt32(18);

                        if (!reader.IsDBNull(19))
                            objet.IsProrata = reader.GetBoolean(19);
                        if (!reader.IsDBNull(20))
                            objet.JourFinEcheance = reader.GetString(20);

                        if (!reader.IsDBNull(21))
                            objet.DateDepot = reader.GetDateTime(21);
                        if (!reader.IsDBNull(22))
                            objet.Idexploitation = reader.GetInt32(22);
                        //Libelle_Objet

                        //if (!reader.IsDBNull(22))
                        //    objet.LibelleObjet = reader.GetString(22);
                        //else objet.LibelleObjet = string.Empty;

                        //if (!reader.IsDBNull(23))
                        //    objet.IdDepartement = reader.GetInt32(23);
                        //else objet.IdDepartement = 0;

                        if (!reader.IsDBNull(23))
                            objet.TotalTTC = reader.GetDouble(23);
                        else objet.TotalTTC = 0;

                        if (!reader.IsDBNull(24))
                            objet.DateNonValide = reader.GetDateTime(24);
                        else objet.DateNonValide = null;

                        if (!reader.IsDBNull(25))
                            objet.DatePaiement = reader.GetDateTime(25);
                        else objet.DatePaiement = null;
                        if (!reader.IsDBNull(26))
                            objet.DateFacture = reader.GetDateTime(26);
                        else objet.DateFacture = null;

                        if (!reader.IsDBNull(27))
                            objet.TotalHT = reader.GetDouble(27);
                        else objet.TotalHT = 0;
                        if (!reader.IsDBNull(28))
                            objet.TotalTVA = reader.GetDouble(28);
                        if (!reader.IsDBNull(29))
                            objet.TotalPRORATA = reader.GetDouble(29);
                        if (!reader.IsDBNull(30))
                            objet.IdClientLangue = reader.GetInt32(30);
                        if (!reader.IsDBNull(31))
                            objet.IdClientExoneration = reader.GetInt32(31);
                        if (!reader.IsDBNull(32))
                            objet.IdDepartement = reader.GetInt32(32);
                        if (!reader.IsDBNull(33))
                            objet.Libelle_Dep = reader.GetString(33);
                        if (!reader.IsDBNull(34))
                            objet.MaregeBeneficiaireId = reader.GetInt32(34);
                        if (!reader.IsDBNull(35))
                            objet.TotalMarge = reader.GetDouble(35);

                        if (!reader.IsDBNull(36))
                            objet.IdTaxe = reader.GetInt32(36);
                        if (!reader.IsDBNull(40))
                            objet.IdProrata = reader.GetInt32(40);
                        else objet.IdProrata = 0;

                        if (!reader.IsDBNull(41))
                            objet.OrderNumber = reader.GetString(41);
                        if (!reader.IsDBNull(42))
                            objet.CompteTier = reader.GetString(42);
                        if (!reader.IsDBNull(43))
                            objet.CompteAnalytique = reader.GetString(43);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Facture> SEARCH_LIST_FACTURE(Int32 idSite, DateTime dateDebut, DateTime dateFin, int idClient)
        {
            List<Facture> listes = new List<Facture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_FACTURE_SEARCH_BYDATE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inDateDebut", MySqlDbType.DateTime);
                    comand.Parameters.Add("inDateFin", MySqlDbType.DateTime);
                    comand.Parameters.Add("inIdclient", MySqlDbType.Int32);

                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inDateDebut"].Value = dateDebut;
                    comand.Parameters["inDateFin"].Value = dateFin;
                    comand.Parameters["inIdclient"].Value = idClient;


                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Facture objet = new Facture();
                        objet.IdFacture = reader.GetInt64(0);

                        if (!reader.IsDBNull(1))
                            objet.NumeroFacture = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.DateCreation = reader.GetDateTime(2);
                        if (!reader.IsDBNull(3))
                        {
                            objet.IdCreerpar = reader.GetInt32(3);
                            if (!reader.IsDBNull(4))
                                objet.LibelleUser = reader.GetString(4);
                        }
                        if (!reader.IsDBNull(39))
                            objet.IdModifierPar = reader.GetInt32(39);
                        else objet.IdModifierPar = 0;
                        //if (!reader.IsDBNull(5))
                        // objet.DateModification = reader.GetDateTime(5);
                        // if (!reader.IsDBNull(6))
                        // objet.DateCreation = reader.GetDateTime(6);
                        if (!reader.IsDBNull(15))
                            objet.DateCloture = reader.GetDateTime(15);
                        else objet.DateCloture = null;

                        if (!reader.IsDBNull(5))
                            objet.MoisPrestation = reader.GetDateTime(5);
                        if (!reader.IsDBNull(6))
                        {
                            objet.IdObjetFacture = reader.GetInt32(6);
                            if (!reader.IsDBNull(7))
                                objet.LibelleClientObjet = reader.GetString(7);
                        }


                        //if (!reader.IsDBNull(10))
                        //    objet.IdTaxe = reader.GetInt32(10);
                        //if (!reader.IsDBNull(11))
                        //    objet.IdDevise = reader.GetInt32(11);
                        //if (!reader.IsDBNull(12))
                        //    objet.IdModePaiement = reader.GetInt32(12);
                        if (!reader.IsDBNull(8))
                        {
                            objet.IdStatut = reader.GetInt32(8);
                            if (!reader.IsDBNull(9))
                                objet.LibelleStatut = reader.GetString(9);
                        }

                        if (!reader.IsDBNull(10))
                            objet.LibelleBackgorund = reader.GetString(10);
                        if (!reader.IsDBNull(11))
                            objet.LibelleClientOk = reader.GetString(11);
                        if (!reader.IsDBNull(12))
                            objet.LibelleClientBackgorund = reader.GetString(12);

                        if (!reader.IsDBNull(13))
                        {
                            objet.IdClient = reader.GetInt32(13);
                            if (!reader.IsDBNull(14))
                                objet.LibelleClient = reader.GetString(14);
                        }
                        else objet.IdClient = 0;



                        if (!reader.IsDBNull(16))
                            objet.DateSortie = reader.GetDateTime(16);
                        if (!reader.IsDBNull(17))
                            objet.DateSuspension = reader.GetDateTime(17);
                        if (!reader.IsDBNull(18))
                            objet.IdSite = reader.GetInt32(18);

                        if (!reader.IsDBNull(19))
                            objet.IsProrata = reader.GetBoolean(19);
                        if (!reader.IsDBNull(20))
                            objet.JourFinEcheance = reader.GetString(20);

                        if (!reader.IsDBNull(21))
                            objet.DateDepot = reader.GetDateTime(21);
                        if (!reader.IsDBNull(22))
                            objet.Idexploitation = reader.GetInt32(22);
                        //Libelle_Objet

                        //if (!reader.IsDBNull(22))
                        //    objet.LibelleObjet = reader.GetString(22);
                        //else objet.LibelleObjet = string.Empty;

                        //if (!reader.IsDBNull(23))
                        //    objet.IdDepartement = reader.GetInt32(23);
                        //else objet.IdDepartement = 0;

                        if (!reader.IsDBNull(23))
                            objet.TotalTTC = reader.GetDouble(23);
                        else objet.TotalTTC = 0;

                        if (!reader.IsDBNull(24))
                            objet.DateNonValide = reader.GetDateTime(24);
                        else objet.DateNonValide = null;

                        if (!reader.IsDBNull(25))
                            objet.DatePaiement = reader.GetDateTime(25);
                        else objet.DatePaiement = null;
                        if (!reader.IsDBNull(26))
                            objet.DateFacture = reader.GetDateTime(26);
                        else objet.DateFacture = null;

                        if (!reader.IsDBNull(27))
                            objet.TotalHT = reader.GetDouble(27);
                        else objet.TotalHT = 0;
                        if (!reader.IsDBNull(28))
                            objet.TotalTVA = reader.GetDouble(28);
                        if (!reader.IsDBNull(29))
                            objet.TotalPRORATA = reader.GetDouble(29);
                        if (!reader.IsDBNull(30))
                            objet.IdClientLangue = reader.GetInt32(30);
                        if (!reader.IsDBNull(31))
                            objet.IdClientExoneration = reader.GetInt32(31);
                        if (!reader.IsDBNull(32))
                            objet.IdDepartement = reader.GetInt32(32);
                        if (!reader.IsDBNull(33))
                            objet.Libelle_Dep = reader.GetString(33);
                        if (!reader.IsDBNull(34))
                            objet.MaregeBeneficiaireId = reader.GetInt32(34);
                        if (!reader.IsDBNull(35))
                            objet.TotalMarge = reader.GetDouble(35);

                        if (!reader.IsDBNull(36))
                            objet.IdTaxe = reader.GetInt32(36);
                        if (!reader.IsDBNull(40))
                            objet.IdProrata = reader.GetInt32(40);
                        else objet.IdProrata = 0;

                        if (!reader.IsDBNull(41))
                            objet.OrderNumber = reader.GetString(41);
                        if (!reader.IsDBNull(42))
                            objet.CompteTier = reader.GetString(42);
                        if (!reader.IsDBNull(43))
                            objet.CompteAnalytique = reader.GetString(43);

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public Facture GetAll_FACTUREVALIDATE_BYID(Int64 idFacture, int idSite, int Mode)
        {
            Facture objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_FACTURE_SELECT_BYID_NEW";
                    comand.CommandTimeout = Constants.TimeOut;
                    //comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inmode", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = idFacture;
                    comand.Parameters["idSite"].Value = idSite;
                    comand.Parameters["inmode"].Value = Mode;

                    MySqlDataReader reader = comand.ExecuteReader();
                    string exploitationNom=string.Empty;
                    string taxeTaux = string.Empty;
                    string deviseNom = string.Empty;
                    string deviseTaux = string.Empty;
                    string userCreate = string.Empty;
                    string userupdate = string.Empty;
                    string clientLabel = string.Empty;
                    string clientCompta = string.Empty;

                    while (reader.Read())
                    {
                        objet = new Facture();
                        objet.IdFacture = reader.GetInt64(0);

                        if (!reader.IsDBNull(1))
                            objet.NumeroFacture = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.DateCreation = reader.GetDateTime(2);
                        if (!reader.IsDBNull(3))
                            objet.IdCreerpar = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            objet.IdModifierPar = reader.GetInt32(4);
                        if (!reader.IsDBNull(5))
                            objet.DateModification = reader.GetDateTime(5);
                        //if (!reader.IsDBNull(6))
                        //    objet.DateCreation = reader.GetDateTime(6);
                        if (!reader.IsDBNull(7))
                            objet.DateCloture = reader.GetDateTime(7);
                        else objet.DateCloture = null;
                        if (!reader.IsDBNull(8))
                            objet.MoisPrestation = reader.GetDateTime(8);
                        if (!reader.IsDBNull(9))
                            objet.IdObjetFacture = reader.GetInt32(9);

                        if (!reader.IsDBNull(10))
                            objet.IdTaxe = reader.GetInt32(10);
                        if (!reader.IsDBNull(11))
                            objet.IdDevise = reader.GetInt32(11);
                        if (!reader.IsDBNull(12))
                            objet.IdModePaiement = reader.GetInt32(12);
                        if (!reader.IsDBNull(13))
                            objet.IdStatut = reader.GetInt32(13);
                        if (!reader.IsDBNull(14))
                            objet.IdClient = reader.GetInt32(14);
                        else objet.IdClient = 0;



                        if (!reader.IsDBNull(15))
                            objet.DateSortie = reader.GetDateTime(15);
                        if (!reader.IsDBNull(16))
                            objet.DateSuspension = reader.GetDateTime(16);

                        if (!reader.IsDBNull(17))
                            objet.IdSite = reader.GetInt32(17);
                        if (!reader.IsDBNull(18))
                            objet.IsProrata = reader.GetBoolean(18);
                        if (!reader.IsDBNull(19))
                            objet.JourFinEcheance = reader.GetString(19);

                        if (!reader.IsDBNull(20))
                            objet.DateDepot = reader.GetDateTime(20);

                        if (!reader.IsDBNull(21))
                            objet.Idexploitation = reader.GetInt32(21);

                        if (!reader.IsDBNull(22))
                            objet.LibelleObjet = reader.GetString(22);

                        if (!reader.IsDBNull(23))
                            objet.IdDepartement = reader.GetInt32(23);
                        else objet.IdDepartement = 0;
                        if (!reader.IsDBNull(24))
                            objet.TotalTTC = reader.GetDouble(24);
                        else objet.TotalTTC = 0;
                        if (!reader.IsDBNull(25))
                            objet.DateNonValide = reader.GetDateTime(25);
                        else objet.DateNonValide = null;
                        if (!reader.IsDBNull(26))
                            objet.DatePaiement = reader.GetDateTime(26);
                        else objet.DatePaiement = null;

                        if (!reader.IsDBNull(27))
                            objet.DateFacture = reader.GetDateTime(27);
                        else objet.DateFacture = null;

                        if (!reader.IsDBNull(28))
                            objet.TotalHT = reader.GetDouble(28);
                        else objet.TotalHT = 0;
                        if (!reader.IsDBNull(29))
                            objet.TotalTVA = reader.GetDouble(29);
                        if (!reader.IsDBNull(30))
                            objet.TotalPRORATA = reader.GetDouble(30);

                        if (!reader.IsDBNull(31))
                            objet.ExploitationIds = reader.GetString(31);
                        if (!reader.IsDBNull(32))
                            objet.ExploitationList = reader.GetString(32);
                        if (!reader.IsDBNull(33))
                            objet.MaregeBeneficiaireId = reader.GetInt32(33);
                        else objet.MaregeBeneficiaireId = 0;
                        if (!reader.IsDBNull(34))
                            objet.TotalMarge = reader.GetDouble(34);

                        if (!reader.IsDBNull(35))
                            objet.LibelleClientObjet = reader.GetString(35);
                        if (!reader.IsDBNull(36))
                            objet.LibelleDepartement = reader.GetString(36);
                        if (!reader.IsDBNull(37))
                            objet.LibelleStatut = reader.GetString(37);

                        if (!reader.IsDBNull(38))
                            objet.OrderNumber = reader.GetString(38);

                        if (!reader.IsDBNull(39)) 
                            exploitationNom = reader.GetString(39);
                        if (!reader.IsDBNull(40))
                            taxeTaux = reader.GetString(40);
                        if (!reader.IsDBNull(41))
                            deviseNom = reader.GetString(41);
                        if (!reader.IsDBNull(42))
                            deviseTaux = reader.GetString(42);
                        if (!reader.IsDBNull(43))
                            userCreate = reader.GetString(43);
                        if (!reader.IsDBNull(44))
                            userupdate = reader.GetString(44);

                        if (!reader.IsDBNull(45))
                            clientLabel = reader.GetString(45);
                        if (!reader.IsDBNull(46))
                            clientCompta = reader.GetString(46);




                        objet.CurrentExploitationFacture = GetAll_EXPLOITATION_FACTUREBYID(objet.Idexploitation);

                        objet.CurrentClient = GetALL_CLIENTByID(objet.IdClient);
                        objet.IdClientLangue = objet.CurrentClient.IdLangue;
                        objet.UserCreate = GetUtilisateur_ById(objet.IdCreerpar);
                        if (objet.IdModifierPar != null)
                            objet.UserModified = GetUtilisateur_ById(objet.IdModifierPar);
                        objet.CurrentObjetFacture = GetAll_OBJET_FACTUREBYID(objet.IdObjetFacture);
                        objet.CurrentModePaiement = GetAll_MODE_PAIEMENTBYID(objet.IdModePaiement);
                        objet.CurrentStatut = GetAll_STATUT_FACTUREBYID(objet.IdStatut);
                        objet.CurrentTaxe = Taxes_SELECTById(objet.IdTaxe, objet.IdSite);
                        objet.CurrentMarge = Taxes_SELECTById(objet.MaregeBeneficiaireId.Value, objet.IdSite);
                        objet.CurrentDevise = GetAllDeviseById(objet.IdDevise, objet.IdSite);

                        if (objet.IdDepartement != null && objet.IdDepartement > 0)
                        {
                            objet.CurrentDepartement = Get_DEPARTMENTSByID((int)objet.IdDepartement);
                             objet.Libelle_Dep = objet.CurrentDepartement.Libelle;
                        }
                        else objet.Libelle_Dep = string.Empty;

                        break;


                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Facture GetAll_FACTUREBYID(long id, Int32 idSite)
        {
            Facture objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("idSite", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["idSite"].Value = idSite;

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new Facture();
                        objet.IdFacture = reader.GetInt64(0);

                        if (!reader.IsDBNull(1))
                            objet.NumeroFacture = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.DateCreation = reader.GetDateTime(2);
                        if (!reader.IsDBNull(3))
                            objet.IdCreerpar = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            objet.IdModifierPar = reader.GetInt32(4);
                        if (!reader.IsDBNull(5))
                            objet.DateModification = reader.GetDateTime(5);
                        //if (!reader.IsDBNull(6))
                        //    objet.DateCreation = reader.GetDateTime(6);
                        if (!reader.IsDBNull(7))
                            objet.DateCloture = reader.GetDateTime(7);
                        else objet.DateCloture = null;
                        if (!reader.IsDBNull(8))
                            objet.MoisPrestation = reader.GetDateTime(8);
                        if (!reader.IsDBNull(9))
                            objet.IdObjetFacture = reader.GetInt32(9);

                        if (!reader.IsDBNull(10))
                            objet.IdTaxe = reader.GetInt32(10);
                        if (!reader.IsDBNull(11))
                            objet.IdDevise = reader.GetInt32(11);
                        if (!reader.IsDBNull(12))
                            objet.IdModePaiement = reader.GetInt32(12);
                        if (!reader.IsDBNull(13))
                            objet.IdStatut = reader.GetInt32(13);
                        if (!reader.IsDBNull(14))
                            objet.IdClient = reader.GetInt32(14);
                        else objet.IdClient = 0;



                        if (!reader.IsDBNull(15))
                            objet.DateSortie = reader.GetDateTime(15);
                        if (!reader.IsDBNull(16))
                            objet.DateSuspension = reader.GetDateTime(16);

                        if (!reader.IsDBNull(17))
                            objet.IdSite = reader.GetInt32(17);
                        if (!reader.IsDBNull(18))
                            objet.IsProrata = reader.GetBoolean(18);
                        if (!reader.IsDBNull(19))
                            objet.JourFinEcheance = reader.GetString(19);

                        if (!reader.IsDBNull(20))
                            objet.DateDepot = reader.GetDateTime(20);
                        if (!reader.IsDBNull(21))
                            objet.Idexploitation = reader.GetInt32(21);

                        if (!reader.IsDBNull(22))
                            objet.LibelleObjet = reader.GetString(22);

                        if (!reader.IsDBNull(23))
                            objet.IdDepartement = reader.GetInt32(23);
                        else objet.IdDepartement = 0;

                        if (!reader.IsDBNull(24))
                            objet.TotalTTC = reader.GetDouble(24);
                        else objet.TotalTTC = 0;

                        if (!reader.IsDBNull(25))
                            objet.DateNonValide = reader.GetDateTime(25);
                        else objet.DateNonValide = null;
                        if (!reader.IsDBNull(26))
                            objet.DatePaiement = reader.GetDateTime(26);
                        else objet.DatePaiement = null;

                        if (!reader.IsDBNull(27))
                            objet.DateFacture = reader.GetDateTime(27);
                        else objet.DateFacture = null;

                        if (!reader.IsDBNull(28))
                            objet.TotalHT = reader.GetDouble(28);
                        else objet.TotalHT = 0;
                        if (!reader.IsDBNull(29))
                            objet.TotalTVA = reader.GetDouble(29);
                        if (!reader.IsDBNull(30))
                            objet.TotalPRORATA = reader.GetDouble(30);

                        if (!reader.IsDBNull(31))
                            objet.ExploitationIds = reader.GetString(31);
                        if (!reader.IsDBNull(32))
                            objet.ExploitationList = reader.GetString(32);
                        if (!reader.IsDBNull(33))
                            objet.MaregeBeneficiaireId = reader.GetInt32(33);
                        else objet.MaregeBeneficiaireId = 0;
                        if (!reader.IsDBNull(34))
                            objet.TotalMarge = reader.GetDouble(34);

                        if (!reader.IsDBNull(35))
                            objet.OrderNumber = reader.GetString(35);

                        objet.CurrentExploitationFacture = GetAll_EXPLOITATION_FACTUREBYID(objet.Idexploitation);

                        objet.CurrentClient = GetALL_CLIENTByID(objet.IdClient);

                        objet.CurrentObjetFacture = GetAll_OBJET_FACTUREBYID(objet.IdObjetFacture);
                        objet.CurrentModePaiement = GetAll_MODE_PAIEMENTBYID(objet.IdModePaiement);
                        objet.CurrentStatut = GetAll_STATUT_FACTUREBYID(objet.IdStatut);
                        objet.CurrentTaxe = Taxes_SELECTById(objet.IdTaxe, idSite);
                        objet.CurrentDevise = GetAllDeviseById(objet.IdDevise, idSite);
                        objet.CurrentMarge = Taxes_SELECTByIdArchive(objet.MaregeBeneficiaireId.Value, objet.IdSite);
                        if (objet.IdDepartement != null && objet.IdDepartement > 0)
                        {
                            objet.CurrentDepartement = Get_DEPARTMENTSByID((int)objet.IdDepartement);
                            objet.Libelle_Dep = objet.CurrentDepartement.Libelle;
                        }
                        else objet.Libelle_Dep = string.Empty;

                        break;
                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Facture GetAll_FACTUREBYID(long id)
        {
            Facture objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("idSite", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["idSite"].Value = 0;

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new Facture();
                        objet.IdFacture = reader.GetInt64(0);

                        if (!reader.IsDBNull(1))
                            objet.NumeroFacture = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.DateCreation = reader.GetDateTime(2);
                        if (!reader.IsDBNull(3))
                            objet.IdCreerpar = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            objet.IdModifierPar = reader.GetInt32(4);
                        if (!reader.IsDBNull(5))
                            objet.DateModification = reader.GetDateTime(5);
                        //if (!reader.IsDBNull(6))
                        //    objet.DateCreation = reader.GetDateTime(6);
                        if (!reader.IsDBNull(7))
                            objet.DateCloture = reader.GetDateTime(7);
                        else objet.DateCloture = null;
                        if (!reader.IsDBNull(8))
                            objet.MoisPrestation = reader.GetDateTime(8);
                        if (!reader.IsDBNull(9))
                            objet.IdObjetFacture = reader.GetInt32(9);

                        if (!reader.IsDBNull(10))
                            objet.IdTaxe = reader.GetInt32(10);
                        if (!reader.IsDBNull(11))
                            objet.IdDevise = reader.GetInt32(11);
                        if (!reader.IsDBNull(12))
                            objet.IdModePaiement = reader.GetInt32(12);
                        if (!reader.IsDBNull(13))
                            objet.IdStatut = reader.GetInt32(13);
                        if (!reader.IsDBNull(14))
                            objet.IdClient = reader.GetInt32(14);
                        else objet.IdClient = 0;



                        if (!reader.IsDBNull(15))
                            objet.DateSortie = reader.GetDateTime(15);
                        if (!reader.IsDBNull(16))
                            objet.DateSuspension = reader.GetDateTime(16);

                        if (!reader.IsDBNull(17))
                            objet.IdSite = reader.GetInt32(17);
                        if (!reader.IsDBNull(18))
                            objet.IsProrata = reader.GetBoolean(18);
                        if (!reader.IsDBNull(19))
                            objet.JourFinEcheance = reader.GetString(19);

                        if (!reader.IsDBNull(20))
                            objet.DateDepot = reader.GetDateTime(20);

                        if (!reader.IsDBNull(21))
                            objet.Idexploitation = reader.GetInt32(21);

                        if (!reader.IsDBNull(22))
                            objet.LibelleObjet = reader.GetString(22);

                        if (!reader.IsDBNull(23))
                            objet.IdDepartement = reader.GetInt32(23);
                        else objet.IdDepartement = 0;
                        if (!reader.IsDBNull(24))
                            objet.TotalTTC = reader.GetDouble(24);
                        else objet.TotalTTC = 0;
                        if (!reader.IsDBNull(25))
                            objet.DateNonValide = reader.GetDateTime(25);
                        else objet.DateNonValide = null;
                        if (!reader.IsDBNull(26))
                            objet.DatePaiement = reader.GetDateTime(26);
                        else objet.DatePaiement = null;

                        if (!reader.IsDBNull(27))
                            objet.DateFacture = reader.GetDateTime(27);
                        else objet.DateFacture = null;

                        if (!reader.IsDBNull(28))
                            objet.TotalHT = reader.GetDouble(28);
                        else objet.TotalHT = 0;
                        if (!reader.IsDBNull(29))
                            objet.TotalTVA = reader.GetDouble(29);
                        if (!reader.IsDBNull(30))
                            objet.TotalPRORATA = reader.GetDouble(30);

                        if (!reader.IsDBNull(31))
                            objet.ExploitationIds = reader.GetString(31);
                        if (!reader.IsDBNull(32))
                            objet.ExploitationList = reader.GetString(32);
                        if (!reader.IsDBNull(33))
                            objet.MaregeBeneficiaireId = reader.GetInt32(33);
                        else objet.MaregeBeneficiaireId = 0;
                        if (!reader.IsDBNull(34))
                            objet.TotalMarge = reader.GetDouble(34);

                        if (!reader.IsDBNull(35))
                            objet.OrderNumber = reader.GetString(35);

                        //objet.CurrentExploitationFacture = GetAll_EXPLOITATION_FACTUREBYID(objet.Idexploitation);

                        objet.CurrentClient = GetALL_CLIENTByID(objet.IdClient);
                        objet.IdClientLangue = objet.CurrentClient.IdLangue;
                        objet.CurrentMarge = Taxes_SELECTByIdArchive(objet.MaregeBeneficiaireId.Value, objet.IdSite);
                        //objet.UserCreate = GetUtilisateur_ById(objet.IdCreerpar);
                        //if (objet.IdModifierPar != null)
                        //    objet.UserModified = GetUtilisateur_ById(objet.IdModifierPar);
                        //objet.CurrentObjetFacture = GetAll_OBJET_FACTUREBYID(objet.IdObjetFacture);
                        //objet.CurrentModePaiement = GetAll_MODE_PAIEMENTBYID(objet.IdModePaiement);
                        objet.CurrentStatut = GetAll_STATUT_FACTUREBYID(objet.IdStatut);
                        objet.CurrentTaxe = Taxes_SELECTById(objet.IdTaxe, objet.IdSite);
                        objet.CurrentDevise = GetAllDeviseById(objet.IdDevise, objet.IdSite);

                  

                        break;
                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Facture> LIST_FACTURE_SORTIE(Int32 idSite, DateTime dateDebut, DateTime dateFin, int mode)
        {
            List<Facture> listes = new List<Facture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_FACTURES_SORTIES_RESEARCH;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inDateDebut", MySqlDbType.DateTime);
                    comand.Parameters.Add("inDateFin", MySqlDbType.DateTime);
                    comand.Parameters.Add("inMode", MySqlDbType.Int32);

                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inDateDebut"].Value = dateDebut;
                    comand.Parameters["inDateFin"].Value = dateFin;
                    comand.Parameters["inMode"].Value = mode;


                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Facture objet = new Facture();
                        objet.IdFacture = reader.GetInt64(0);

                        if (!reader.IsDBNull(1))
                            objet.NumeroFacture = reader.GetString(1);
                        if (!reader.IsDBNull(2))
                            objet.DateCreation = reader.GetDateTime(2);
                        if (!reader.IsDBNull(3))
                            objet.IdCreerpar = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                            objet.IdModifierPar = reader.GetInt32(4);
                        else objet.IdModifierPar = 0;
                        if (!reader.IsDBNull(5))
                            objet.DateModification = reader.GetDateTime(5);
                        //if (!reader.IsDBNull(6))
                        //    objet.DateCreation = reader.GetDateTime(6);
                        if (!reader.IsDBNull(7))
                            objet.DateCloture = reader.GetDateTime(7);
                        else objet.DateCloture = null;

                        if (!reader.IsDBNull(8))
                            objet.MoisPrestation = reader.GetDateTime(8);
                        if (!reader.IsDBNull(9))
                            objet.IdObjetFacture = reader.GetInt32(9);
                        if (!reader.IsDBNull(10))
                            objet.IdTaxe = reader.GetInt32(10);
                        if (!reader.IsDBNull(11))
                            objet.IdDevise = reader.GetInt32(11);
                        if (!reader.IsDBNull(12))
                            objet.IdModePaiement = reader.GetInt32(12);
                        if (!reader.IsDBNull(13))
                            objet.IdStatut = reader.GetInt32(13);
                        if (!reader.IsDBNull(14))
                            objet.IdClient = reader.GetInt32(14);
                        else objet.IdClient = 0;



                        if (!reader.IsDBNull(15))
                            objet.DateSortie = reader.GetDateTime(15);
                        if (!reader.IsDBNull(16))
                            objet.DateSuspension = reader.GetDateTime(16);

                        if (!reader.IsDBNull(17))
                            objet.IdSite = reader.GetInt32(17);
                        if (!reader.IsDBNull(18))
                            objet.IsProrata = reader.GetBoolean(18);
                        if (!reader.IsDBNull(19))
                            objet.JourFinEcheance = reader.GetString(19);

                        if (!reader.IsDBNull(20))
                            objet.DateDepot = reader.GetDateTime(20);

                        if (!reader.IsDBNull(21))
                            objet.Idexploitation = reader.GetInt32(21);

                        if (!reader.IsDBNull(22))
                            objet.LibelleObjet = reader.GetString(22);

                        if (!reader.IsDBNull(23))
                            objet.IdDepartement = reader.GetInt32(23);
                        else objet.IdDepartement = 0;

                        if (!reader.IsDBNull(24))
                            objet.TotalTTC = reader.GetDouble(24);
                        else objet.TotalTTC = 0;

                        if (!reader.IsDBNull(25))
                            objet.DateNonValide = reader.GetDateTime(25);
                        else objet.DateNonValide = null;

                        if (!reader.IsDBNull(26))
                            objet.DatePaiement = reader.GetDateTime(26);
                        else objet.DatePaiement = null;

                        if (!reader.IsDBNull(27))
                            objet.DateFacture = reader.GetDateTime(27);
                        else objet.DateFacture = null;

                        if (!reader.IsDBNull(28))
                            objet.TotalHT = reader.GetDouble(28);
                        else objet.TotalHT = 0;
                        if (!reader.IsDBNull(29))
                            objet.TotalTVA = reader.GetDouble(29);
                        if (!reader.IsDBNull(30))
                            objet.TotalPRORATA = reader.GetDouble(30);
                        if (!reader.IsDBNull(32))
                            objet.LibelleClientObjet = reader.GetString(32);
                        if (!reader.IsDBNull(34))
                            objet.LibelleDepartement = reader.GetString(34);
                        if (!reader.IsDBNull(33))
                            objet.LibelleStatut = reader.GetString(33);
                        //if (!reader.IsDBNull(31))
                        //    objet.ExploitationIds = reader.GetString(31);
                        if (!reader.IsDBNull(35))
                            objet.ExploitationList = reader.GetString(35);

                        if (!reader.IsDBNull(36))
                            objet.OrderNumber = reader.GetString(36);

                        objet.CurrentExploitationFacture = new ExploitationFacture { IdExploitation = objet.Idexploitation, Libelle = objet.ExploitationList, IdSite = objet.IdSite };// GetAll_EXPLOITATION_FACTUREBYID(objet.Idexploitation);
                     

                        objet.CurrentClient = GetALL_CLIENTByIDArchive(objet.IdClient, objet.IdFacture);

                        objet.CurrentObjetFacture = new ObjetFacture { IdObjet = objet.IdObjetFacture, Libelle = objet.LibelleClientObjet };// GetAll_OBJET_FACTUREBYID(objet.IdObjetFacture);
                        objet.CurrentModePaiement = GetAll_MODE_PAIEMENTBYID(objet.IdModePaiement);
                        objet.CurrentStatut = new StatutFacture { IdStatut = objet.IdStatut, Libelle = objet.LibelleStatut }; // GetAll_STATUT_FACTUREBYID(objet.IdStatut);
                        objet.CurrentTaxe = Taxes_SELECTByIdArchive(objet.IdTaxe, idSite);
                        objet.CurrentDevise = GetAllDeviseArchiveById(objet.IdDevise, idSite);
                        objet.UserCreate = GetUtilisateur_ById(objet.IdCreerpar);

                        if (objet.IdDepartement != null && objet.IdDepartement > 0)
                        {
                            objet.CurrentDepartement = new Departement { IdDepartement = objet.IdDepartement.Value, Libelle = objet.LibelleDepartement }; //Get_DEPARTMENTSByID((int)objet.IdDepartement);
                            objet.Libelle_Dep = objet.LibelleDepartement;
                        }
                        else objet.Libelle_Dep = string.Empty;

                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public object FACTURE_ADD_ITEMS(Facture objFacture, List<LigneFacture> items, bool estNormalAvoir)
        {
            MySqlTransaction trans = null;
            object idfacture = 0;
            string storeprocedure = string.Empty;
            if (estNormalAvoir)
                storeprocedure = Constants.PS_FACTURE_ADD;
            else storeprocedure = Constants.PS_FactureAvoirADD;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();

                    string psExecute = string.Empty;
                    string psUser = string.Empty;
                    //

                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;

                    comand.CommandText = storeprocedure;
                    comand.CommandTimeout = Constants.TimeOut;
                    trans = con.BeginTransaction();
                    comand.Connection = con;
                    comand.Transaction = trans;

                    comand.Parameters.Add("inId", MySqlDbType.VarChar, 255);
                    comand.Parameters["inId"].Direction = ParameterDirection.Output;

                    comand.Parameters.Add("inIdUserCreer", MySqlDbType.Int32);
                    comand.Parameters.Add("inDatePrestation", MySqlDbType.DateTime);
                    comand.Parameters.Add("inDateFacture", MySqlDbType.DateTime);
                    comand.Parameters.Add("inIdObjet", MySqlDbType.Int32);

                    comand.Parameters.Add("inIdStatut", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdModepaiement", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdtaxe", MySqlDbType.Int32);
                    comand.Parameters.Add("inIddevise", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inIsprorata", MySqlDbType.Bit);
                    comand.Parameters.Add("inJourLimit", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inDatedepot", MySqlDbType.DateTime);
                    comand.Parameters.Add("inIDExploitation", MySqlDbType.Int32);
                    comand.Parameters.Add("inlibelleObjet", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIDepartment", MySqlDbType.Int32);
                    comand.Parameters.Add("inTotalTTC", MySqlDbType.Double);
                    comand.Parameters.Add("inTotalHT", MySqlDbType.Double);
                    comand.Parameters.Add("inTotalTVA", MySqlDbType.Double);
                    comand.Parameters.Add("inTotalPRORATA", MySqlDbType.Double);
                    comand.Parameters.Add("inExploitationsId", MySqlDbType.VarChar, 50);
                    comand.Parameters.Add("inExploittaionlibelle", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdMarge", MySqlDbType.Int32);
                    comand.Parameters.Add("inTotalMarge", MySqlDbType.Double);
                    comand.Parameters.Add("inOrderNumner", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inTauxMarge", MySqlDbType.VarChar, 15);
                  


                    comand.Parameters["inId"].Value = objFacture.IdFacture;
                    comand.Parameters["inIdUserCreer"].Value = objFacture.IdCreerpar;
                    comand.Parameters["inDatePrestation"].Value = objFacture.MoisPrestation;
                    comand.Parameters["inDateFacture"].Value = objFacture.DateCreation;
                    comand.Parameters["inIdObjet"].Value = objFacture.IdObjetFacture;

                    comand.Parameters["inIdStatut"].Value = objFacture.IdStatut;
                    comand.Parameters["inIdModepaiement"].Value = objFacture.IdModePaiement;
                    comand.Parameters["inIdtaxe"].Value = objFacture.IdTaxe;
                    comand.Parameters["inIddevise"].Value = objFacture.IdDevise;
                    comand.Parameters["inIdClient"].Value = objFacture.IdClient;
                    comand.Parameters["idSite"].Value = objFacture.IdSite;
                    comand.Parameters["inIsprorata"].Value = objFacture.IsProrata;
                    comand.Parameters["inJourLimit"].Value = objFacture.JourFinEcheance;
                    comand.Parameters["inDatedepot"].Value = objFacture.DateDepot;
                    comand.Parameters["inIDExploitation"].Value = objFacture.Idexploitation;
                    comand.Parameters["inlibelleObjet"].Value = objFacture.LibelleObjet;
                    comand.Parameters["inIDepartment"].Value = objFacture.IdDepartement;
                    comand.Parameters["inTotalTTC"].Value = objFacture.TotalTTC;
                    comand.Parameters["inTotalHT"].Value = objFacture.TotalHT;
                    comand.Parameters["inTotalTVA"].Value = objFacture.TotalTVA;
                    comand.Parameters["inTotalPRORATA"].Value = objFacture.TotalPRORATA;
                    comand.Parameters["inExploitationsId"].Value = objFacture.ExploitationIds;
                    comand.Parameters["inExploittaionlibelle"].Value = objFacture.ExploitationList;
                    comand.Parameters["inIdMarge"].Value = objFacture.MaregeBeneficiaireId;
                    comand.Parameters["inTotalMarge"].Value = objFacture.TotalMarge;
                    comand.Parameters["inOrderNumner"].Value = objFacture.OrderNumber;
                    comand.Parameters["inTauxMarge"].Value = objFacture.TauxMargeBeneficiaire;

                    comand.ExecuteNonQuery();
                    idfacture = comand.Parameters["inId"].Value;
                    if (long.Parse(idfacture.ToString()) == 0)
                    {
                        trans.Rollback();
                        throw new Exception("Facture non créer");
                    }

                    MySqlCommand comands = new MySqlCommand();
                    comands.CommandType = CommandType.StoredProcedure;
                    comands.CommandTimeout = Constants.TimeOut;
                    comands.CommandText = Constants.PS_LIGNE_FACTURE_ADD;
                    comands.Connection = con;
                    comands.Transaction = trans;
                    //
                    comands.Parameters.Add("inId", MySqlDbType.Int64);
                    comands.Parameters.Add("idSite", MySqlDbType.Int32);
                    comands.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                    comands.Parameters.Add("inDescription", MySqlDbType.VarChar, 255);
                    comands.Parameters.Add("inQte", MySqlDbType.Float);
                    comands.Parameters.Add("inExonere", MySqlDbType.Bit);
                    comands.Parameters.Add("inIdProduit", MySqlDbType.Int32);
                    comands.Parameters.Add("inIdDetailProduit", MySqlDbType.Int32);
                    comands.Parameters.Add("inIdUser", MySqlDbType.Int32);
                    comands.Parameters.Add("inPrixunit", MySqlDbType.Float);
                    comands.Parameters.Add("inmontantHt", MySqlDbType.Float);
                    comands.Parameters.Add("inDateCreate", MySqlDbType.DateTime);
                    comands.Parameters.Add("inIdExploitation", MySqlDbType.Int32);
                    comands.Parameters.Add("inIdSatut", MySqlDbType.Int32);
                    comands.Parameters.Add("inIdclient", MySqlDbType.Int32);
                    comands.Parameters.Add("inTauxTva", MySqlDbType.VarChar,100);
                    comands.Parameters.Add("inspecialFacture", MySqlDbType.Bit);
                    comands.Parameters.Add("inProduitLabel", MySqlDbType.VarChar, 255);

                    foreach (LigneFacture item in items)
                    {
                        item.DateCreation = objFacture.DateCreation;
                        comands.Parameters["inId"].Value = item.IdLigneFacture;
                        comands.Parameters["idSite"].Value = objFacture.IdSite;
                        comands.Parameters["inIdFacture"].Value = long.Parse(idfacture.ToString());
                        comands.Parameters["inDescription"].Value = item.Description;
                        comands.Parameters["inQte"].Value = item.Quantite;
                        comands.Parameters["inExonere"].Value = item.Exonere;
                        comands.Parameters["inIdProduit"].Value = item.IdProduit;
                        comands.Parameters["inIdDetailProduit"].Value = item.IdDetailProduit;
                        comands.Parameters["inIdUser"].Value = item.IdUtilisateur;
                        comands.Parameters["inPrixunit"].Value = item.Prixunit;
                        comands.Parameters["inmontantHt"].Value = item.MontantHT;
                        comands.Parameters["inDateCreate"].Value = item.DateCreation;
                        comands.Parameters["inIdExploitation"].Value = item.Idexploitation;
                        comands.Parameters["inIdSatut"].Value = objFacture.IdStatut;
                        comands.Parameters["inIdclient"].Value = objFacture.IdClient;
                        comands.Parameters["inTauxTva"].Value = item.TauxTva;
                        comands.Parameters["inspecialFacture"].Value = item.SpecialFacture;
                        comands.Parameters["inProduitLabel"].Value = item.ProduitLabel;

                        comands.ExecuteNonQuery();
                    }

                    trans.Commit();

                }

                return idfacture;
            }
            catch (MySqlException ex)
            {

                trans.Rollback();
                throw new DALException(ex.Message);

            }

        }


        public void FACTURE_ADD_VerifHTTC(Int64 idfacture)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_FACTURE_VERIFICATION_TTC";
                comand.CommandTimeout = Constants.TimeOut;

                comand.Parameters.Add("inIdfacture", MySqlDbType.Int64);
                comand.Parameters["inIdfacture"].Value = idfacture;
                comand.Connection = con;
                con.Open();
                comand.ExecuteNonQuery();
            }
        }

        public bool FACTURE_ADD(Facture objFacture, out object idfacture)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_FACTURE_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters["inId"].Direction = ParameterDirection.Output;
                    comand.Parameters.Add("inIdUserCreer", MySqlDbType.Int32);
                    comand.Parameters.Add("inDatePrestation", MySqlDbType.DateTime);
                    comand.Parameters.Add("inDateFacture", MySqlDbType.DateTime);
                    comand.Parameters.Add("inIdObjet", MySqlDbType.Int32);

                    comand.Parameters.Add("inIdStatut", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdModepaiement", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdtaxe", MySqlDbType.Int32);
                    comand.Parameters.Add("inIddevise", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inJourLimit", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inDatedepot", MySqlDbType.DateTime);
                    comand.Parameters.Add("inIDExploitation", MySqlDbType.Int32);
                    comand.Parameters.Add("inlibelleObjet", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIDepartment", MySqlDbType.Int32);
                    comand.Parameters.Add("inTotalTTC", MySqlDbType.Double);
                    comand.Parameters.Add("inTotalHT", MySqlDbType.Double);
                    comand.Parameters.Add("inTotalTVA", MySqlDbType.Double);
                    comand.Parameters.Add("inTotalPRORATA", MySqlDbType.Double);
                    comand.Parameters.Add("inExploitationsId", MySqlDbType.VarChar, 50);
                    comand.Parameters.Add("inExploittaionlibelle", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdMarge", MySqlDbType.Int32);
                    comand.Parameters.Add("inTotalMarge", MySqlDbType.Double);
                    comand.Parameters.Add("inOrderNumner", MySqlDbType.VarChar, 255);

                    comand.Parameters["inId"].Value = objFacture.IdFacture;
                    comand.Parameters["inIdUserCreer"].Value = objFacture.IdCreerpar;
                    comand.Parameters["inDatePrestation"].Value = objFacture.MoisPrestation;
                    comand.Parameters["inDateFacture"].Value = objFacture.DateCreation;
                    comand.Parameters["inIdObjet"].Value = objFacture.IdObjetFacture;

                    comand.Parameters["inIdStatut"].Value = objFacture.IdStatut;
                    comand.Parameters["inIdModepaiement"].Value = objFacture.IdModePaiement;
                    comand.Parameters["inIdtaxe"].Value = objFacture.IdTaxe;
                    comand.Parameters["inIddevise"].Value = objFacture.IdDevise;
                    comand.Parameters["inIdClient"].Value = objFacture.IdClient;
                    comand.Parameters["inJourLimit"].Value = objFacture.JourFinEcheance;
                    comand.Parameters["inDatedepot"].Value = objFacture.DateDepot;
                    comand.Parameters["inIDExploitation"].Value = objFacture.Idexploitation;
                    comand.Parameters["inlibelleObjet"].Value = objFacture.LibelleObjet;
                    comand.Parameters["inIDepartment"].Value = objFacture.IdDepartement;
                    comand.Parameters["inTotalTTC"].Value = objFacture.TotalTTC;
                    comand.Parameters["inTotalHT"].Value = objFacture.TotalHT;
                    comand.Parameters["inTotalTVA"].Value = objFacture.TotalTVA;
                    comand.Parameters["inTotalPRORATA"].Value = objFacture.TotalPRORATA;
                    comand.Parameters["inExploitationsId"].Value = objFacture.ExploitationIds;
                    comand.Parameters["inExploittaionlibelle"].Value = objFacture.ExploitationList;
                    comand.Parameters["inIdMarge"].Value = objFacture.MaregeBeneficiaireId;
                    comand.Parameters["inTotalMarge"].Value = objFacture.TotalMarge;
                    comand.Parameters["inOrderNumner"].Value = objFacture.OrderNumber;


                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                    idfacture = comand.Parameters["inId"].Value;
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
            return isValues;
        }


        /// <summary>
        /// facture avoir
        /// </summary>
        /// <param name="objFacture"></param>
        /// <returns></returns>
        public bool FACTURE_UPDATE(Facture objFacture)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_FACTURE_UPDATE_AVOIR;
                comand.CommandTimeout = Constants.TimeOut;

                comand.Parameters.Add("inId", MySqlDbType.Int64);
                comand.Parameters.Add("inIdUserModif", MySqlDbType.Int32);
                comand.Parameters.Add("inDatePrestation", MySqlDbType.DateTime);
                comand.Parameters.Add("inDateFacture", MySqlDbType.DateTime);
                comand.Parameters.Add("inIdObjet", MySqlDbType.Int32);
                comand.Parameters.Add("inIdStatut", MySqlDbType.Int32);
                comand.Parameters.Add("inIdModepaiement", MySqlDbType.Int32);
                comand.Parameters.Add("inIdtaxe", MySqlDbType.Int32);
                comand.Parameters.Add("idSite", MySqlDbType.Int32);
                comand.Parameters.Add("inIddevise", MySqlDbType.Int32);
                comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                comand.Parameters.Add("inIsprorata", MySqlDbType.Bit);
                comand.Parameters.Add("inJourLimit", MySqlDbType.VarChar, 255);
                comand.Parameters.Add("inDatedepot", MySqlDbType.DateTime);
                comand.Parameters.Add("inIDExploitation", MySqlDbType.Int32);
                comand.Parameters.Add("inlibelleObjet", MySqlDbType.VarChar, 255);
                comand.Parameters.Add("inIDepartment", MySqlDbType.Int32);
                comand.Parameters.Add("inTotalTTC", MySqlDbType.Double);
                comand.Parameters.Add("inTotalHT", MySqlDbType.Double);
                comand.Parameters.Add("inTotalTVA", MySqlDbType.Double);
                comand.Parameters.Add("inTotalPRORATA", MySqlDbType.Double);
                comand.Parameters.Add("inExploitationsId", MySqlDbType.VarChar, 50);
                comand.Parameters.Add("inExploittaionlibelle", MySqlDbType.VarChar, 255);
                comand.Parameters.Add("inIdMarge", MySqlDbType.Int32);
                comand.Parameters.Add("inTotalMarge", MySqlDbType.Double);
                comand.Parameters.Add("inTauxMarge", MySqlDbType.VarChar, 15);
                comand.Parameters.Add("inOrderNumner", MySqlDbType.VarChar, 255);



                comand.Parameters["inId"].Value = objFacture.IdFacture;
                comand.Parameters["inIdUserModif"].Value = objFacture.IdModifierPar;
                comand.Parameters["inDatePrestation"].Value = objFacture.MoisPrestation;
                comand.Parameters["inDateFacture"].Value = objFacture.DateCreation;
                comand.Parameters["inIdObjet"].Value = objFacture.IdObjetFacture;
                comand.Parameters["inIdStatut"].Value = objFacture.IdStatut;
                comand.Parameters["inIdModepaiement"].Value = objFacture.IdModePaiement;
                comand.Parameters["inIdtaxe"].Value = objFacture.IdTaxe;
                comand.Parameters["idSite"].Value = objFacture.IdSite;
                comand.Parameters["inIddevise"].Value = objFacture.IdDevise;
                comand.Parameters["inIdClient"].Value = objFacture.IdClient;
                comand.Parameters["inIsprorata"].Value = objFacture.IsProrata;
                comand.Parameters["inJourLimit"].Value = objFacture.JourFinEcheance;
                comand.Parameters["inDatedepot"].Value = objFacture.DateDepot;
                comand.Parameters["inIDExploitation"].Value = objFacture.Idexploitation;
                comand.Parameters["inlibelleObjet"].Value = objFacture.LibelleObjet;
                comand.Parameters["inIDepartment"].Value = objFacture.IdDepartement;
                comand.Parameters["inTotalTTC"].Value = objFacture.TotalTTC;
                comand.Parameters["inTotalHT"].Value = objFacture.TotalHT;
                comand.Parameters["inTotalTVA"].Value = objFacture.TotalTVA;
                comand.Parameters["inTotalPRORATA"].Value = objFacture.TotalPRORATA;
                comand.Parameters["inExploitationsId"].Value = objFacture.ExploitationIds;
                comand.Parameters["inExploittaionlibelle"].Value = objFacture.ExploitationList;
                comand.Parameters["inIdMarge"].Value = objFacture.MaregeBeneficiaireId;
                comand.Parameters["inTotalMarge"].Value = objFacture.TotalMarge;
                comand.Parameters["inTauxMarge"].Value = objFacture.TauxMargeBeneficiaire;
                comand.Parameters["inOrderNumner"].Value = objFacture.OrderNumber;
                comand.Connection = con;
                con.Open();
                comand.ExecuteNonQuery();
                return true;

            }
        }


        /// <summary>
        /// cloture de la fcture et archivage des tiers depuis la liste facture
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateCloture"></param>
        /// <param name="idstatut"></param>
        /// <param name="iduserModif"></param>
        /// <param name="ops"></param>
        /// <param name="facture"></param>
        /// <returns></returns>
        public bool FACTURE_CLOSING(long id, DateTime dateCloture, int idstatut, int iduserModif, bool ops, Facture facture)
        {
            bool isValues = false;
            MySqlTransaction trans = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();

                    //MySqlCommand comand = new MySqlCommand();
                    //comand.CommandType = CommandType.StoredProcedure;
                    //comand.CommandText = Constants.PS_FACTURE_DATECLOTURE;
                    //comand.CommandTimeout = Constants.TimeOut;
                    //comand.Connection = con;
                    //if (facture != null)
                    //{
                        trans = con.BeginTransaction();
                    //   comand.Transaction = trans;
                    //}

                    //comand.Parameters.Add("inId", MySqlDbType.Int64);
                    //comand.Parameters.Add("inDateClosing", MySqlDbType.DateTime);
                    //comand.Parameters.Add("inIdstatut", MySqlDbType.Int32);
                    //comand.Parameters.Add("inUserModif", MySqlDbType.Int32);
                    //comand.Parameters.Add("inoperation", MySqlDbType.Bit);

                    //comand.Parameters["inId"].Value = id;
                    //comand.Parameters["inDateClosing"].Value = dateCloture;
                    //comand.Parameters["inIdstatut"].Value = idstatut;
                    //comand.Parameters["inUserModif"].Value = iduserModif;
                    //comand.Parameters["inoperation"].Value = ops;

                    //inValNomSociete varchar(255),inValNomClient varchar(255),inValNomUser varchar(255),inValPrenomuser

                  
                

                    if (facture != null)
                    {
                        facture.IdModifierPar = iduserModif;
                        if (FACTURE_CLOSING_UPDATE(facture, trans, con))
                        {
                            List<LigneFacture> items = GetAll_LIGNE_FACTURE(facture.IdFacture);
                            string tauxTaxe = Taxes_SELECTById(facture.IdTaxe, facture.IdSite).Taux;
                            foreach (LigneFacture item in items)
                            {
                                item.IdClient = facture.IdClient;
                                item.TauxTva = tauxTaxe??"0";
                                item.IdStatut = 14003;
                                item.ProduitLabel =item.CurrentProduit !=null ? item.CurrentProduit.Libelle:string.Empty;
                                if (!LIGNE_FACTURE_ADD(item))
                                    trans.Rollback();


                                

                            }
                            trans.Commit();
                           
                        }
                        else trans.Rollback();
                    }

                  //  comand.ExecuteNonQuery();

                 
                     
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                if (facture != null)
               // trans.Rollback();

                throw new DALException(ex.Message);
            }
            return isValues;
        }


        public bool FACTURE_CLOSING_UPDATE(Facture objFacture,MySqlTransaction transaction ,MySqlConnection con)
        {
            bool isValues = false;

            try
            {
                //using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                //{
                //    con.Open();

                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_FACTURE_CLOSING_UPDATE";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Connection = con;
                    comand.Transaction = transaction;

                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                 
                    comand.Parameters.Add("inValNomUser", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inValPrenomuser", MySqlDbType.VarChar, 255);

                    comand.Parameters.Add("inDatedepot", MySqlDbType.DateTime);
                    comand.Parameters.Add("inIDExploitation", MySqlDbType.Int32);
                    comand.Parameters.Add("inlibelleObjet", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIDepartment", MySqlDbType.Int32);
                    comand.Parameters.Add("inTotalTTC", MySqlDbType.Double);
                    comand.Parameters.Add("inTotalHT", MySqlDbType.Double);
                    comand.Parameters.Add("inTotalTVA", MySqlDbType.Double);
                    comand.Parameters.Add("inTotalPRORATA", MySqlDbType.Double);

                    comand.Parameters.Add("inExploitationsId", MySqlDbType.VarChar, 50);
                    comand.Parameters.Add("inExploittaionlibelle", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inValideObjetLibelle", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdMarge", MySqlDbType.Int32);
                    comand.Parameters.Add("inTotalMarge", MySqlDbType.Double);
                    comand.Parameters.Add("inTauxMarge", MySqlDbType.VarChar, 100);
                    comand.Parameters.Add("inLibelleStatut", MySqlDbType.VarChar, 100);
                    comand.Parameters.Add("inIdObjet", MySqlDbType.Int32);
                    comand.Parameters.Add("inIddepartement", MySqlDbType.Int32);
                    comand.Parameters.Add("iniduserCreate", MySqlDbType.Int32);
                   

                    comand.Parameters["inId"].Value = objFacture.IdFacture;
                  
                    comand.Parameters["inValNomUser"].Value = objFacture.LibelleUserNom;
                    comand.Parameters["inValPrenomuser"].Value = objFacture.LibelleUserPrenom;

                    comand.Parameters["inDatedepot"].Value = objFacture.DateDepot;
                    comand.Parameters["inIDExploitation"].Value = objFacture.IdStatut;
                    comand.Parameters["inlibelleObjet"].Value = objFacture.LibelleObjet;
                    comand.Parameters["inIDepartment"].Value = objFacture.IdDepartement;
                    comand.Parameters["inTotalTTC"].Value = objFacture.TotalTTC;
                    comand.Parameters["inTotalHT"].Value = objFacture.TotalHT;
                    comand.Parameters["inTotalTVA"].Value = objFacture.TotalTVA;
                    comand.Parameters["inTotalPRORATA"].Value = objFacture.TotalPRORATA;

                    comand.Parameters["inExploitationsId"].Value = objFacture.ExploitationIds;
                    comand.Parameters["inExploittaionlibelle"].Value = objFacture.ExploitationList;
                    comand.Parameters["inValideObjetLibelle"].Value = objFacture.LibelleClientObjet;
                    comand.Parameters["inIdMarge"].Value = objFacture.MaregeBeneficiaireId;
                    comand.Parameters["inTotalMarge"].Value = objFacture.TotalMarge;
                    comand.Parameters["inTauxMarge"].Value = objFacture.TauxMargeBeneficiaire;
                    comand.Parameters["inLibelleStatut"].Value = objFacture.LibelleStatut;
                    comand.Parameters["inIdObjet"].Value = objFacture.IdObjetFacture;
                    comand.Parameters["inIddepartement"].Value = objFacture.IdDepartement;
                    comand.Parameters["iniduserCreate"].Value = objFacture.IdModifierPar;

                   
                   
                    comand.ExecuteNonQuery();
               // }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                transaction.Rollback();
                throw new DALException(ex.Message);
            }
            return isValues;
        }


        /// <summary>
        /// validation depuis edition voiture
        /// </summary>
        /// <param name="oldfactureMax"></param>
        /// <param name="newfacture"></param>
        /// <param name="objFacture"></param>
        /// <returns></returns>
        public bool FACTURE_UPDATE(ref string oldfactureMax, ref string newfacture, Facture objFacture)
        {
            bool isValues = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_FACTURE_ADD_NEW;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inIdUserModif", MySqlDbType.Int32);
                    comand.Parameters.Add("inUserCreate", MySqlDbType.Int32);
                    comand.Parameters.Add("inDatePrestation", MySqlDbType.DateTime);
                    comand.Parameters.Add("inDateFacture", MySqlDbType.DateTime);
                    comand.Parameters.Add("inIdObjet", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdStatut", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdModepaiement", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdtaxe", MySqlDbType.Int32);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inIddevise", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIsprorata", MySqlDbType.Bit);
                    comand.Parameters.Add("inJourLimit", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inDatedepot", MySqlDbType.DateTime);
                    comand.Parameters.Add("inIDExploitation", MySqlDbType.Int32);
                    comand.Parameters.Add("inlibelleObjet", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIDepartment", MySqlDbType.Int32);
                    comand.Parameters.Add("inTotalTTC", MySqlDbType.Double);
                    comand.Parameters.Add("inTotalHT", MySqlDbType.Double);
                    comand.Parameters.Add("inTotalTVA", MySqlDbType.Double);
                    comand.Parameters.Add("inTotalPRORATA", MySqlDbType.Double);
                    comand.Parameters.Add("inExploitationsId", MySqlDbType.VarChar, 50);
                    comand.Parameters.Add("inExploittaionlibelle", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inValideObjetLibelle", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inClientLibelle", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inTaxeTauxLibelle", MySqlDbType.VarChar, 55);
                    comand.Parameters.Add("inDeviseLibelle", MySqlDbType.VarChar, 55);
                    comand.Parameters.Add("inDeviseTAux", MySqlDbType.VarChar, 15);
                   
               
                    comand.Parameters.Add("inValNomUser", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inValPrenomuser", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inValLibelleDepart", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdMarge", MySqlDbType.Int32);
                    comand.Parameters.Add("inTotalMarge", MySqlDbType.Double);
                    comand.Parameters.Add("inTauxMarge", MySqlDbType.VarChar, 100);
                    comand.Parameters.Add("inLibelleStatut", MySqlDbType.VarChar, 100);
                    comand.Parameters.Add("inOrderNumner", MySqlDbType.VarChar, 255);



                    comand.Parameters["inId"].Value = objFacture.IdFacture;
                    comand.Parameters["inIdUserModif"].Value = objFacture.IdModifierPar;
                    comand.Parameters["inUserCreate"].Value = objFacture.IdCreerpar;
                    comand.Parameters["inDatePrestation"].Value = objFacture.MoisPrestation;
                    comand.Parameters["inDateFacture"].Value = objFacture.DateCreation;
                    comand.Parameters["inIdObjet"].Value = objFacture.IdObjetFacture;
                    comand.Parameters["inIdStatut"].Value = objFacture.IdStatut;
                    comand.Parameters["inIdModepaiement"].Value = objFacture.IdModePaiement;
                    comand.Parameters["inIdtaxe"].Value = objFacture.IdTaxe;
                    comand.Parameters["idSite"].Value = objFacture.IdSite;
                    comand.Parameters["inIddevise"].Value = objFacture.IdDevise;
                    comand.Parameters["inIdClient"].Value = objFacture.IdClient;
                    comand.Parameters["inIsprorata"].Value = objFacture.IsProrata;
                    comand.Parameters["inJourLimit"].Value = objFacture.JourFinEcheance;
                    comand.Parameters["inDatedepot"].Value = objFacture.DateDepot;
                    comand.Parameters["inIDExploitation"].Value = objFacture.Idexploitation;
                    comand.Parameters["inlibelleObjet"].Value = objFacture.LibelleObjet;
                    comand.Parameters["inIDepartment"].Value = objFacture.IdDepartement;
                    comand.Parameters["inTotalTTC"].Value = objFacture.TotalTTC;
                    comand.Parameters["inTotalHT"].Value = objFacture.TotalHT;
                    comand.Parameters["inTotalTVA"].Value = objFacture.TotalTVA;
                    comand.Parameters["inTotalPRORATA"].Value = objFacture.TotalPRORATA;
                    comand.Parameters["inExploitationsId"].Value = objFacture.ExploitationIds;
                    comand.Parameters["inExploittaionlibelle"].Value = objFacture.ExploitationList;
                    comand.Parameters["inValideObjetLibelle"].Value = objFacture.LibelleClientObjet;
                    comand.Parameters["inClientLibelle"].Value = objFacture.LibelleClient;
                    comand.Parameters["inTaxeTauxLibelle"].Value = objFacture.LibelleTauxTaxe;
                    comand.Parameters["inDeviseLibelle"].Value = objFacture.LibelleDeviseConversion;
                    comand.Parameters["inDeviseTAux"].Value = objFacture.DeviseTaux;
                  
                 
                    comand.Parameters["inValNomUser"].Value = objFacture.LibelleUserNom;
                    comand.Parameters["inValPrenomuser"].Value = objFacture.LibelleUserPrenom;
                    comand.Parameters["inValLibelleDepart"].Value = objFacture.LibelleDepartement;
                    comand.Parameters["inIdMarge"].Value = objFacture.MaregeBeneficiaireId;
                    comand.Parameters["inTotalMarge"].Value = objFacture.TotalMarge;
                    comand.Parameters["inTauxMarge"].Value = objFacture.TauxMargeBeneficiaire;
                    comand.Parameters["inLibelleStatut"].Value = objFacture.LibelleStatut;
                    comand.Parameters["inOrderNumner"].Value = objFacture.OrderNumber;


                    object errorMessage = null;


                    if (objFacture.NumeroFacture.IndexOf("FB") != -1 && objFacture.factureValide)
                    {
                        FACTURE_UPDATE_NUM(ref errorMessage, ref oldfactureMax, ref newfacture, objFacture.NumeroFacture, objFacture.IdFacture, objFacture.IdSite);

                        if (errorMessage != null)
                        {
                            if (int.Parse(errorMessage.ToString()) == 100)
                            {
                                comand.Connection = con;
                                con.Open();
                                comand.ExecuteNonQuery();

                            }
                            else
                                throw new DALException(" Erreure validation facture");
                        }
                    }
                    else
                    {
                        comand.Connection = con;
                        con.Open();
                        comand.ExecuteNonQuery();



                    }




                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
            return isValues;
        }

        public bool FACTURE_UPDATE_WITHOUT_UPDATE(ref string oldfactureMax, ref string newfacture, Facture objFacture)
        {
            object errorMessage = null;

            try
            {
                FACTURE_UPDATE_NUM(ref errorMessage, ref oldfactureMax, ref newfacture, objFacture.NumeroFacture, objFacture.IdFacture, objFacture.IdSite);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(errorMessage.ToString());
            }
        }

        public bool FACTURE_ADD_IMPORT(Facture objFacture)
        {
            bool isValues = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_FACTURE_UPDATE_NEW;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inNumeFacture", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdUserModif", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdUserCreer", MySqlDbType.Int32);
                    comand.Parameters.Add("indateCreation", MySqlDbType.DateTime);
                    comand.Parameters.Add("inDatePrestation", MySqlDbType.DateTime);
                    comand.Parameters.Add("inIdObjet", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdStatut", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdModepaiement", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdtaxe", MySqlDbType.Int32);
                    comand.Parameters.Add("inIddevise", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIsprorata", MySqlDbType.Bit);
                    comand.Parameters.Add("inJourLimit", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inDatedepot", MySqlDbType.DateTime);
                    comand.Parameters.Add("inIDExploitation", MySqlDbType.Int32);
                    comand.Parameters.Add("inlibelleObjet", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIDepartment", MySqlDbType.Int32);
                    comand.Parameters.Add("inTotalTTC", MySqlDbType.Double);
                    comand.Parameters.Add("inDateEcheance", MySqlDbType.DateTime);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = objFacture.IdFacture;
                    comand.Parameters["inNumeFacture"].Value = objFacture.NumeroFacture;
                    comand.Parameters["inIdUserModif"].Value = objFacture.IdModifierPar;
                    comand.Parameters["inIdUserCreer"].Value = objFacture.IdCreerpar;
                    comand.Parameters["indateCreation"].Value = objFacture.DateCreation;
                    comand.Parameters["inDatePrestation"].Value = objFacture.MoisPrestation;
                    comand.Parameters["inIdObjet"].Value = objFacture.IdObjetFacture;
                    comand.Parameters["inIdStatut"].Value = objFacture.IdStatut;
                    comand.Parameters["inIdModepaiement"].Value = objFacture.IdModePaiement;
                    comand.Parameters["inIdtaxe"].Value = objFacture.IdTaxe;
                    comand.Parameters["inIddevise"].Value = objFacture.IdDevise;
                    comand.Parameters["inIdClient"].Value = objFacture.IdClient;
                    comand.Parameters["inIsprorata"].Value = objFacture.IsProrata;
                    comand.Parameters["inJourLimit"].Value = objFacture.JourFinEcheance;
                    comand.Parameters["inDatedepot"].Value = objFacture.DateDepot;
                    comand.Parameters["inIDExploitation"].Value = objFacture.Idexploitation;
                    comand.Parameters["inlibelleObjet"].Value = objFacture.LibelleObjet;
                    comand.Parameters["inIDepartment"].Value = objFacture.IdDepartement;
                    comand.Parameters["inTotalTTC"].Value = objFacture.TotalTTC;
                    comand.Parameters["inDateEcheance"].Value = objFacture.DateEcheance;
                    comand.Parameters["idSite"].Value = objFacture.IdSite;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
            return isValues;
        }



        bool FACTURE_UPDATE_NUM(ref object errorMessage, ref string oldfactureMax, ref string newfacture, string currentfacture, long idFactue, Int32 idsite)
        {
            bool isValues = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_FACTURE_ADDNUM_VALIDATE;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inCurrentFacture", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("errorMessage", MySqlDbType.Int32);
                    comand.Parameters["errorMessage"].Direction = ParameterDirection.Output;
                    comand.Parameters.Add("pn_oldfactureNumber", MySqlDbType.VarChar, 255);
                    comand.Parameters["pn_oldfactureNumber"].Direction = ParameterDirection.Output;
                    comand.Parameters.Add("pn_newfactureNumber", MySqlDbType.VarChar, 255);
                    comand.Parameters["pn_newfactureNumber"].Direction = ParameterDirection.Output;

                    comand.Parameters["inId"].Value = idFactue;
                    comand.Parameters["idSite"].Value = idsite;
                    comand.Parameters["inCurrentFacture"].Value = currentfacture;
                    comand.Parameters["errorMessage"].Value = 0;
                    comand.Parameters["pn_oldfactureNumber"].Value = "";
                    comand.Parameters["pn_newfactureNumber"].Value = "";

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                    errorMessage = comand.Parameters["errorMessage"].Value;
                    oldfactureMax = comand.Parameters["pn_oldfactureNumber"].Value.ToString();
                    newfacture = comand.Parameters["pn_newfactureNumber"].Value.ToString();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
            return isValues;
        }


        public bool FACTURE_UPDATE_STATUS(long id, int idstatut, int iduserModif, int ops)
        {
            bool isValues = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_FACTURE_UPDATE_STATUS";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inIdstatut", MySqlDbType.Int32);
                    comand.Parameters.Add("inUserModif", MySqlDbType.Int32);
                    comand.Parameters.Add("inoperation", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdstatut"].Value = idstatut;
                    comand.Parameters["inUserModif"].Value = iduserModif;
                    comand.Parameters["inoperation"].Value = ops;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
            return isValues;
        }

     

        public bool FACTURE_NONVALABLE(long id, int idstatut, int iduserModif, bool ops)
        {
            bool isValues = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_FACTURE_CLOSING;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inIdstatut", MySqlDbType.Int32);
                    comand.Parameters.Add("inUserModif", MySqlDbType.Int32);
                    comand.Parameters.Add("inoperation", MySqlDbType.Bit);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdstatut"].Value = idstatut;
                    comand.Parameters["inUserModif"].Value = iduserModif;
                    comand.Parameters["inoperation"].Value = ops;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
            return isValues;
        }

        public bool FACTURE_SORTIE(long id, DateTime dateSortie, int idstatut, int iduserModif, bool ops)
        {
            bool isValues = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_FACTURE_DATE_SORTIE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inDate", MySqlDbType.DateTime);
                    comand.Parameters.Add("inIdstatut", MySqlDbType.Int32);
                    comand.Parameters.Add("inUserModif", MySqlDbType.Int32);
                    comand.Parameters.Add("inoperation", MySqlDbType.Bit);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inDate"].Value = dateSortie;
                    comand.Parameters["inIdstatut"].Value = idstatut;
                    comand.Parameters["inUserModif"].Value = iduserModif;
                    comand.Parameters["inoperation"].Value = ops;



                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {

                throw new DALException(ex.Message);
            }
            return isValues;
        }

        public bool FACTURE_SUSPENSION(long id, DateTime dateCloture, int idstatut, int iduserModif, bool ops)
        {
            bool isValues = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_FACTURE_DATE_SUPENSION;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inDate", MySqlDbType.DateTime);
                    comand.Parameters.Add("inIdstatut", MySqlDbType.Int32);
                    comand.Parameters.Add("inUserModif", MySqlDbType.Int32);
                    comand.Parameters.Add("inoperation", MySqlDbType.Bit);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inDate"].Value = dateCloture;
                    comand.Parameters["inIdstatut"].Value = idstatut;
                    comand.Parameters["inUserModif"].Value = iduserModif;
                    comand.Parameters["inoperation"].Value = ops;


                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {


                throw new DALException(ex.Message);
            }
            return isValues;
        }

        public bool FACTURE_PAIEMENT(long id, DateTime dateCloture, int iduserModif, bool ops)
        {
            bool isValues = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_FACTURE_DATE_PAIEMENT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inDate", MySqlDbType.DateTime);
                    comand.Parameters.Add("inUserModif", MySqlDbType.Int32);
                    comand.Parameters.Add("inoperation", MySqlDbType.Bit);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inDate"].Value = dateCloture;
                    comand.Parameters["inUserModif"].Value = iduserModif;
                    comand.Parameters["inoperation"].Value = ops;


                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {

                throw new DALException(ex.Message);
            }
            return isValues;
        }


        public bool FACTURE_DELETE(long Id, int estDel, int idSite, int idUser)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_FACTURE_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inDel", MySqlDbType.Int32);
                    comand.Parameters.Add("inUserModif", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = Id;
                    comand.Parameters["inDel"].Value = estDel;
                    comand.Parameters["inUserModif"].Value = idUser;
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
            return isValues;
        }


        public DataTable LISTE_FACTURE_TO_DEL(int idsite)
        {
            DataTable tblClient = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_FACTURE_A_SUPPRIMER;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);
                    comand.Parameters["idSite"].Value = idsite;

                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region OVERVIEW

        public List<OverviewFacture> GetAll_OVERVIEW(int idUser)
        {
            List<OverviewFacture> listes = new List<OverviewFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_OVERVIEW_SELECT";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdUser", MySqlDbType.Int32);
                    comand.Parameters["inIdUser"].Value = idUser;

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        OverviewFacture objet = new OverviewFacture();
                        
                        objet.Idfacture = reader.GetInt64(0);
                        objet.NumeroFacture = reader.GetString(1);
                        objet.NomClient = reader.GetString(2);
                        objet.statut = reader.GetString(3);
                        objet.Dateoperation = reader.GetDateTime(4);
                        objet.IdStatut = reader.GetInt32(5);
                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool OVERVIEW_ADD(OverviewFacture ov)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_OVERVIEW_ADD";
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("id", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                    comand.Parameters.Add("inidClient", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdUser", MySqlDbType.VarChar, 255);

                    comand.Parameters["id"].Value = ov.Idice;
                    comand.Parameters["inIdFacture"].Value = ov.Idfacture;
                    comand.Parameters["inidClient"].Value =ov.IdClient;
                    comand.Parameters["inIdUser"].Value =ov.Iduser;
                

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }
        #endregion

        #region LIGNE FACTURE

        public DataTable GEt_DESCRIPTION_LIGNE(string valeur)
        {
            DataTable tblClient = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DESCRIPTION_LIGNE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("intable", MySqlDbType.VarChar, 100);
                    comand.Parameters["intable"].Value = valeur;

                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);


                }

                return tblClient;


            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        ///  sysfact douala
        /// </summary>
        /// <param name="idFacture"></param>
        /// <returns></returns>

        public List<LigneFacture> GetAll_LIGNE_FACTURE(long idFacture)
        {
            List<LigneFacture> listes = new List<LigneFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LIGNE_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdFacture"].Value = idFacture;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        LigneFacture objet = new LigneFacture();
                        objet.IdLigneFacture = reader.GetInt64(0);
                        if (!reader.IsDBNull(1))
                            objet.IdFacture = reader.GetInt64(1);
                        if (!reader.IsDBNull(2))
                            objet.Description = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.Quantite = reader.GetDecimal(3);
                        if (!reader.IsDBNull(4))
                            objet.Exonere = reader.GetBoolean(4);
                        if (!reader.IsDBNull(5))
                            objet.IdProduit = reader.GetInt32(5);
                        if (!reader.IsDBNull(6))
                            objet.IdDetailProduit = reader.GetInt32(6);
                        if (!reader.IsDBNull(7))
                            objet.IdUtilisateur = reader.GetInt32(7);

                        if (!reader.IsDBNull(8))
                            objet.Prixunit = reader.GetDecimal(8);
                        if (!reader.IsDBNull(9))
                            objet.MontantHT = reader.GetDecimal(9);
                        if (!reader.IsDBNull(10))
                            objet.IdSite = reader.GetInt32(10);

                        if (!reader.IsDBNull(11))
                            objet.DateCreation = reader.GetDateTime(11);
                        else objet.DateCreation = null;

                        if (!reader.IsDBNull(12))
                            objet.DateModif = reader.GetDateTime(12);
                        else objet.DateModif = null;



                        if (!reader.IsDBNull(13))
                            objet.IdUtililModif = reader.GetInt32(13);
                        else objet.IdUtililModif = 0;

                        if (!reader.IsDBNull(14))
                            objet.isdlete = reader.GetBoolean(14);
                        if (!reader.IsDBNull(15))
                            objet.Idexploitation = reader.GetInt32(15);
                        if (!reader.IsDBNull(16))
                            objet.SpecialFacture = reader.GetBoolean(16);

                        if (objet.IdDetailProduit > 0)
                                objet.CurrentDetailProduit = GetAll_DETAIL_PRODUITBy_Id(objet.IdDetailProduit);
                            objet.CurrentProduit = Get_PRODUITBYID(objet.IdProduit);
                       
                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
        }

        public List<LigneFacture> GetAll_LIGNE_FACTURE_Validate(long idFacture)
        {
            List<LigneFacture> listes = new List<LigneFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LIGNE_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);

                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdFacture"].Value = idFacture;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        LigneFacture objet = new LigneFacture();
                        objet.IdLigneFacture = reader.GetInt64(0);
                        if (!reader.IsDBNull(1))
                            objet.IdFacture = reader.GetInt64(1);
                        if (!reader.IsDBNull(2))
                            objet.Description = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.Quantite = reader.GetDecimal(3);
                        if (!reader.IsDBNull(4))
                            objet.Exonere = reader.GetBoolean(4);
                        if (!reader.IsDBNull(5))
                            objet.IdProduit = reader.GetInt32(5);
                        if (!reader.IsDBNull(6))
                            objet.IdDetailProduit = reader.GetInt32(6);
                        if (!reader.IsDBNull(7))
                            objet.IdUtilisateur = reader.GetInt32(7);

                        if (!reader.IsDBNull(8))
                            objet.Prixunit = reader.GetDecimal(8);
                        if (!reader.IsDBNull(9))
                            objet.MontantHT = reader.GetDecimal(9);
                        if (!reader.IsDBNull(10))
                            objet.IdSite = reader.GetInt32(10);

                        if (!reader.IsDBNull(11))
                            objet.DateCreation = reader.GetDateTime(11);
                        else objet.DateCreation = null;

                        if (!reader.IsDBNull(12))
                            objet.DateModif = reader.GetDateTime(12);
                        else objet.DateModif = null;



                        if (!reader.IsDBNull(13))
                            objet.IdUtililModif = reader.GetInt32(13);
                        else objet.IdUtililModif = 0;

                        if (!reader.IsDBNull(14))
                            objet.isdlete = reader.GetBoolean(14);
                        if (!reader.IsDBNull(15))
                            objet.Idexploitation = reader.GetInt32(15);
                        if (!reader.IsDBNull(16))
                            objet.SpecialFacture = reader.GetBoolean(16);

                        //if (!reader.IsDBNull(16))
                        //    objet.LibelleProduit = reader.GetString(16);
                        //if (!reader.IsDBNull(17))
                        //    objet.IsPecialFacture = reader.GetBoolean(17);
                        //else objet.IsPecialFacture = false;

                        //if (!reader.IsDBNull(18))
                        //    objet.QuantiteUnitaireDet = reader.GetDecimal(18);
                        //else objet.QuantiteUnitaireDet = 0;

                        //if (!reader.IsDBNull(19))
                        //    objet.PrixUnitaireDet = reader.GetDecimal(19);
                        //else objet.PrixUnitaireDet = 0;

                        //if (!reader.IsDBNull(20))
                        //    objet.IsProrata = reader.GetBoolean(20);
                        //else objet.IsProrata = false;

                        //if (!reader.IsDBNull(21))
                        //    objet.CodeCompteGeneProduit = reader.GetString(21);


                        if (objet.IdDetailProduit > 0)
                        {
                            //if (!string.IsNullOrEmpty(objet.LibelleProduit))
                            //    objet.CurrentDetailProduit = new ProduitDetail { IdDetail = objet.IdDetailProduit, Quantite = (float)objet.Quantite,
                            //                                                     IdProduit = objet.IdProduit,
                            //                                                     NomProduit = objet.LibelleProduit,
                            //                                                     IdExploitation = objet.Idexploitation,
                            //                                                     SpecialFact = objet.IsPecialFacture,
                            //                                                     Exonerer = objet.Exonere,
                            //                                                     IsProrata = objet.IsProrata,
                            //                                                     PrixUnitaire = objet.PrixUnitaireDet,
                            //                                                     IdSite = objet.IdSite
                            //    };
                            //else
                            objet.CurrentDetailProduit = GetAll_DETAIL_PRODUITBy_Id_Archive(objet.IdDetailProduit);
                        }

                        // objet.CurrentFacture  = GetAll_FACTUREBYID(objet.IdFacture,0);
                        //if (!string.IsNullOrEmpty(objet.LibelleProduit))
                        //    objet.CurrentProduit = new Produit { IdProduit = objet.IdProduit, IdSite = objet.IdSite,
                        //        Libelle=objet.LibelleProduit, 
                        //        CompteOhada=objet.Exonere!=false?  objet.CodeCompteGeneProduit:"",
                        //         CompteExonere = objet.Exonere != true ? objet.CodeCompteGeneProduit : ""
                        //    };
                        //else
                        objet.CurrentProduit = Get_PRODUITBYID_Archive(objet.IdProduit);
                        // objet.CurrentUtilisateur  =GetUtilisateur_ById(objet .IdUtilisateur );


                        // objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
        }

        public List<LigneFacture> GetAll_LIGNE_FACTURE_Archive(long idFacture)
        {
            List<LigneFacture> listes = new List<LigneFacture>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_LIGNE_FACTURE_Archive_SELECT";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);

                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdFacture"].Value = idFacture;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        LigneFacture objet = new LigneFacture();
                        objet.IdLigneFacture = reader.GetInt64(0);
                        if (!reader.IsDBNull(1))
                            objet.IdFacture = reader.GetInt64(1);
                        if (!reader.IsDBNull(2))
                            objet.Description = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.Quantite = reader.GetDecimal(3);
                        if (!reader.IsDBNull(4))
                            objet.Exonere = reader.GetBoolean(4);
                        if (!reader.IsDBNull(5))
                            objet.IdProduit = reader.GetInt32(5);
                        if (!reader.IsDBNull(6))
                            objet.IdDetailProduit = reader.GetInt32(6);
                        if (!reader.IsDBNull(7))
                            objet.IdUtilisateur = reader.GetInt32(7);

                        if (!reader.IsDBNull(8))
                            objet.Prixunit = reader.GetDecimal(8);
                        if (!reader.IsDBNull(9))
                            objet.MontantHT = reader.GetDecimal(9);
                        if (!reader.IsDBNull(10))
                            objet.IdSite = reader.GetInt32(10);

                        if (!reader.IsDBNull(11))
                            objet.DateCreation = reader.GetDateTime(11);
                        else objet.DateCreation = null;

                        if (!reader.IsDBNull(12))
                            objet.DateModif = reader.GetDateTime(12);
                        else objet.DateModif = null;



                        if (!reader.IsDBNull(13))
                            objet.IdUtililModif = reader.GetInt32(13);
                        else objet.IdUtililModif = 0;

                        if (!reader.IsDBNull(14))
                            objet.isdlete = reader.GetBoolean(14);
                        if (!reader.IsDBNull(15))
                            objet.Idexploitation = reader.GetInt32(15);

                        if (!reader.IsDBNull(16))
                            objet.SpecialFacture = reader.GetBoolean(16);

                        //if (!reader.IsDBNull(16))
                        //    objet.LibelleProduit = reader.GetString(16);
                        //if (!reader.IsDBNull(17))
                        //    objet.IsPecialFacture = reader.GetBoolean(17);
                        //else objet.IsPecialFacture = false;

                        //if (!reader.IsDBNull(18))
                        //    objet.QuantiteUnitaireDet = reader.GetDecimal(18);
                        //else objet.QuantiteUnitaireDet = 0;

                        //if (!reader.IsDBNull(19))
                        //    objet.PrixUnitaireDet = reader.GetDecimal(19);
                        //else objet.PrixUnitaireDet = 0;

                        //if (!reader.IsDBNull(20))
                        //    objet.IsProrata = reader.GetBoolean(20);
                        //else objet.IsProrata = false;

                        //if (!reader.IsDBNull(21))
                        //    objet.CodeCompteGeneProduit = reader.GetString(21);


                        if (objet.IdDetailProduit > 0)
                        {
                            //if (!string.IsNullOrEmpty(objet.LibelleProduit))
                            //    objet.CurrentDetailProduit = new ProduitDetail { IdDetail = objet.IdDetailProduit, Quantite = (float)objet.Quantite,
                            //                                                     IdProduit = objet.IdProduit,
                            //                                                     NomProduit = objet.LibelleProduit,
                            //                                                     IdExploitation = objet.Idexploitation,
                            //                                                     SpecialFact = objet.IsPecialFacture,
                            //                                                     Exonerer = objet.Exonere,
                            //                                                     IsProrata = objet.IsProrata,
                            //                                                     PrixUnitaire = objet.PrixUnitaireDet,
                            //                                                     IdSite = objet.IdSite
                            //    };
                            //else
                            objet.CurrentDetailProduit = GetAll_DETAIL_PRODUITBy_Id_Archive(objet.IdDetailProduit);
                        }

                        // objet.CurrentFacture  = GetAll_FACTUREBYID(objet.IdFacture,0);
                        //if (!string.IsNullOrEmpty(objet.LibelleProduit))
                        //    objet.CurrentProduit = new Produit { IdProduit = objet.IdProduit, IdSite = objet.IdSite,
                        //        Libelle=objet.LibelleProduit, 
                        //        CompteOhada=objet.Exonere!=false?  objet.CodeCompteGeneProduit:"",
                        //         CompteExonere = objet.Exonere != true ? objet.CodeCompteGeneProduit : ""
                        //    };
                        //else
                        objet.CurrentProduit = Get_PRODUITBYID_Archive(objet.IdProduit);
                        // objet.CurrentUtilisateur  =GetUtilisateur_ById(objet .IdUtilisateur );


                        // objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
        }


        public LigneFacture Get_LIGNE_FACTUREById(long idLigne)
        {
            LigneFacture objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LIGNE_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = idLigne;
                    comand.Parameters["inIdFacture"].Value = 0;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new LigneFacture();
                        objet.IdLigneFacture = reader.GetInt64(0);
                        if (!reader.IsDBNull(1))
                            objet.IdFacture = reader.GetInt64(1);
                        if (!reader.IsDBNull(2))
                            objet.Description = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.Quantite = reader.GetDecimal(3);
                        if (!reader.IsDBNull(4))
                            objet.Exonere = reader.GetBoolean(4);
                        if (!reader.IsDBNull(5))
                            objet.IdProduit = reader.GetInt32(5);
                        if (!reader.IsDBNull(6))
                            objet.IdDetailProduit = reader.GetInt32(6);
                        if (!reader.IsDBNull(7))
                            objet.IdUtilisateur = reader.GetInt32(7);
                        if (!reader.IsDBNull(8))
                            objet.Prixunit = reader.GetDecimal(8);
                        if (!reader.IsDBNull(9))
                            objet.MontantHT = reader.GetDecimal(9);
                        if (!reader.IsDBNull(10))
                            objet.IdSite = reader.GetInt32(10);

                        if (!reader.IsDBNull(11))
                            objet.DateCreation = reader.GetDateTime(11);
                        else objet.DateCreation = null;

                        if (!reader.IsDBNull(12))
                            objet.DateModif = reader.GetDateTime(12);
                        else objet.DateModif = null;


                        if (!reader.IsDBNull(13))
                            objet.IdUtililModif = reader.GetInt32(13);
                        else objet.IdUtililModif = 0;

                        if (!reader.IsDBNull(14))
                            objet.isdlete = reader.GetBoolean(14);
                        if (!reader.IsDBNull(15))
                            objet.Idexploitation = reader.GetInt32(15);
                        if (!reader.IsDBNull(16))
                            objet.SpecialFacture = reader.GetBoolean(16);



                        //objet.CurrentFacture = GetAll_FACTUREBYID(objet.IdFacture);
                        objet.CurrentProduit = Get_PRODUITBYID(objet.IdProduit);
                        if (objet.IdDetailProduit > 0)
                            objet.CurrentDetailProduit = GetAll_DETAIL_PRODUITBy_Id(objet.IdDetailProduit);
                        //objet.CurrentUtilisateur = GetUtilisateur_ById(objet.IdUtilisateur);
                        // objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        break;
                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
        }

        public LigneFacture Get_LIGNE_FACTUREById_Archive(long idLigne)
        {
            LigneFacture objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_LIGNE_FACTURE_Archive_SELECT";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = idLigne;
                    comand.Parameters["inIdFacture"].Value = 0;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new LigneFacture();
                        objet.IdLigneFacture = reader.GetInt64(0);
                        if (!reader.IsDBNull(1))
                            objet.IdFacture = reader.GetInt64(1);
                        if (!reader.IsDBNull(2))
                            objet.Description = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.Quantite = reader.GetDecimal(3);
                        if (!reader.IsDBNull(4))
                            objet.Exonere = reader.GetBoolean(4);
                        if (!reader.IsDBNull(5))
                            objet.IdProduit = reader.GetInt32(5);
                        if (!reader.IsDBNull(6))
                            objet.IdDetailProduit = reader.GetInt32(6);
                        if (!reader.IsDBNull(7))
                            objet.IdUtilisateur = reader.GetInt32(7);
                        if (!reader.IsDBNull(8))
                            objet.Prixunit = reader.GetDecimal(8);
                        if (!reader.IsDBNull(9))
                            objet.MontantHT = reader.GetDecimal(9);
                        if (!reader.IsDBNull(10))
                            objet.IdSite = reader.GetInt32(10);

                        if (!reader.IsDBNull(11))
                            objet.DateCreation = reader.GetDateTime(11);
                        else objet.DateCreation = null;

                        if (!reader.IsDBNull(12))
                            objet.DateModif = reader.GetDateTime(12);
                        else objet.DateModif = null;


                        if (!reader.IsDBNull(13))
                            objet.IdUtililModif = reader.GetInt32(13);
                        else objet.IdUtililModif = 0;

                        if (!reader.IsDBNull(14))
                            objet.isdlete = reader.GetBoolean(14);
                        if (!reader.IsDBNull(15))
                            objet.Idexploitation = reader.GetInt32(15);
                        if (!reader.IsDBNull(16))
                            objet.SpecialFacture = reader.GetBoolean(16);



                        //objet.CurrentFacture = GetAll_FACTUREBYID(objet.IdFacture);
                        objet.CurrentProduit = Get_PRODUITBYID_Archive(objet.IdProduit);
                        if (objet.IdDetailProduit > 0)
                            objet.CurrentDetailProduit = GetAll_DETAIL_PRODUITBy_Id_Archive(objet.IdDetailProduit);
                        //objet.CurrentUtilisateur = GetUtilisateur_ById(objet.IdUtilisateur);
                        // objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        break;
                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
        }

        public LigneFacture Get_LIGNE_FACTUREById_Validate(long idLigne)
        {
            LigneFacture objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LIGNE_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = idLigne;
                    comand.Parameters["inIdFacture"].Value = 0;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new LigneFacture();
                        objet.IdLigneFacture = reader.GetInt64(0);
                        if (!reader.IsDBNull(1))
                            objet.IdFacture = reader.GetInt64(1);
                        if (!reader.IsDBNull(2))
                            objet.Description = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.Quantite = reader.GetDecimal(3);
                        if (!reader.IsDBNull(4))
                            objet.Exonere = reader.GetBoolean(4);
                        if (!reader.IsDBNull(5))
                            objet.IdProduit = reader.GetInt32(5);
                        if (!reader.IsDBNull(6))
                            objet.IdDetailProduit = reader.GetInt32(6);
                        if (!reader.IsDBNull(7))
                            objet.IdUtilisateur = reader.GetInt32(7);
                        if (!reader.IsDBNull(8))
                            objet.Prixunit = reader.GetDecimal(8);
                        if (!reader.IsDBNull(9))
                            objet.MontantHT = reader.GetDecimal(9);
                        if (!reader.IsDBNull(10))
                            objet.IdSite = reader.GetInt32(10);

                        if (!reader.IsDBNull(11))
                            objet.DateCreation = reader.GetDateTime(11);
                        else objet.DateCreation = null;

                        if (!reader.IsDBNull(12))
                            objet.DateModif = reader.GetDateTime(12);
                        else objet.DateModif = null;


                        if (!reader.IsDBNull(13))
                            objet.IdUtililModif = reader.GetInt32(13);
                        else objet.IdUtililModif = 0;

                        if (!reader.IsDBNull(14))
                            objet.isdlete = reader.GetBoolean(14);
                        if (!reader.IsDBNull(15))
                            objet.Idexploitation = reader.GetInt32(15);
                        if (!reader.IsDBNull(16))
                            objet.SpecialFacture = reader.GetBoolean(16);



                        //objet.CurrentFacture = GetAll_FACTUREBYID(objet.IdFacture);
                        objet.CurrentProduit = Get_PRODUITBYID_Archive(objet.IdProduit);
                        if (objet.IdDetailProduit > 0)
                            objet.CurrentDetailProduit = GetAll_DETAIL_PRODUITBy_Id_Archive(objet.IdDetailProduit);
                        //objet.CurrentUtilisateur = GetUtilisateur_ById(objet.IdUtilisateur);
                        // objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        break;
                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
        }

        public LigneFacture Get_LIGNE_FACTUREById_archive(long idLigne)
        {
            LigneFacture objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_LIGNE_FACTURE_Archive_SELECT";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = idLigne;
                    comand.Parameters["inIdFacture"].Value = 0;
                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new LigneFacture();
                        objet.IdLigneFacture = reader.GetInt64(0);
                        if (!reader.IsDBNull(1))
                            objet.IdFacture = reader.GetInt64(1);
                        if (!reader.IsDBNull(2))
                            objet.Description = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            objet.Quantite = reader.GetDecimal(3);
                        if (!reader.IsDBNull(4))
                            objet.Exonere = reader.GetBoolean(4);
                        if (!reader.IsDBNull(5))
                            objet.IdProduit = reader.GetInt32(5);
                        if (!reader.IsDBNull(6))
                            objet.IdDetailProduit = reader.GetInt32(6);
                        if (!reader.IsDBNull(7))
                            objet.IdUtilisateur = reader.GetInt32(7);
                        if (!reader.IsDBNull(8))
                            objet.Prixunit = reader.GetDecimal(8);
                        if (!reader.IsDBNull(9))
                            objet.MontantHT = reader.GetDecimal(9);
                        if (!reader.IsDBNull(10))
                            objet.IdSite = reader.GetInt32(10);

                        if (!reader.IsDBNull(11))
                            objet.DateCreation = reader.GetDateTime(11);
                        else objet.DateCreation = null;

                        if (!reader.IsDBNull(12))
                            objet.DateModif = reader.GetDateTime(12);
                        else objet.DateModif = null;


                        if (!reader.IsDBNull(13))
                            objet.IdUtililModif = reader.GetInt32(13);
                        else objet.IdUtililModif = 0;

                        if (!reader.IsDBNull(14))
                            objet.isdlete = reader.GetBoolean(14);
                        if (!reader.IsDBNull(15))
                            objet.Idexploitation = reader.GetInt32(15);
                        if (!reader.IsDBNull(16))
                            objet.SpecialFacture = reader.GetBoolean(16);



                        //objet.CurrentFacture = GetAll_FACTUREBYID(objet.IdFacture);
                        objet.CurrentProduit = Get_PRODUITBYID_Archive(objet.IdProduit);
                        if (objet.IdDetailProduit > 0)
                            objet.CurrentDetailProduit = GetAll_DETAIL_PRODUITBy_Id_Archive(objet.IdDetailProduit);
                        //objet.CurrentUtilisateur = GetUtilisateur_ById(objet.IdUtilisateur);
                        // objet.Llangue = GetLANGUEBY_ID(objet.IdLangue);
                        break;
                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
        }


        public bool LIGNE_FACTURE_ADD(LigneFacture objFacture)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LIGNE_FACTURE_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                    comand.Parameters.Add("inDescription", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inQte", MySqlDbType.Double);
                    comand.Parameters.Add("inExonere", MySqlDbType.Int64);
                    comand.Parameters.Add("inIdProduit", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdDetailProduit", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdUser", MySqlDbType.Int32);
                    comand.Parameters.Add("inPrixunit", MySqlDbType.Decimal);
                    comand.Parameters.Add("inmontantHt", MySqlDbType.Decimal);
                    comand.Parameters.Add("inDateCreate", MySqlDbType.DateTime);
                    comand.Parameters.Add("inIdExploitation", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSatut", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdclient", MySqlDbType.Int32);
                    comand.Parameters.Add("inTauxTva", MySqlDbType.VarChar,100);
                    comand.Parameters.Add("inspecialFacture", MySqlDbType.Bit);
                    comand.Parameters.Add("inProduitLabel", MySqlDbType.VarChar,255);
                 

                    
                    comand.Parameters["inId"].Value = objFacture.IdLigneFacture;
                    comand.Parameters["idSite"].Value = objFacture.IdSite;
                    comand.Parameters["inIdFacture"].Value = objFacture.IdFacture;
                    comand.Parameters["inDescription"].Value = objFacture.Description;
                    comand.Parameters["inQte"].Value = objFacture.Quantite;
                    comand.Parameters["inExonere"].Value = objFacture.Exonere;
                    comand.Parameters["inIdProduit"].Value = objFacture.IdProduit;
                    comand.Parameters["inIdDetailProduit"].Value = objFacture.IdDetailProduit;
                    comand.Parameters["inIdUser"].Value = objFacture.IdUtilisateur;
                    comand.Parameters["inPrixunit"].Value = objFacture.Prixunit;
                    comand.Parameters["inmontantHt"].Value = objFacture.MontantHT;
                    comand.Parameters["inDateCreate"].Value = objFacture.DateModif;
                    comand.Parameters["inIdExploitation"].Value = objFacture.Idexploitation;
                    comand.Parameters["inIdSatut"].Value = objFacture.IdStatut;
                    comand.Parameters["inIdclient"].Value = objFacture.IdClient;
                    comand.Parameters["inTauxTva"].Value = objFacture.TauxTva;
                    comand.Parameters["inspecialFacture"].Value = objFacture.SpecialFacture;
                    comand.Parameters["inProduitLabel"].Value = objFacture.ProduitLabel;
                

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
            return isValues;
        }


        public bool LIGNE_FACTURE_DELETE(long Id,int mode)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LIGNE_FACTURE_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = Id;
                
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
            return isValues;
        }
        #endregion

        #region DEPARTEMENT


        public List<Departement> GetAll_DEPARTMENTS(int idSite)
        {
            List<Departement> listes = new List<Departement>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DEPARTEMENT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inID", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar,5);
                    comand.Parameters["inID"].Value = 0;
                    comand.Parameters["inIdste"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "NON";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Departement objet = new Departement();
                        objet.IdDepartement = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);

                        if (!reader.IsDBNull(2))
                            objet.CourtLibelle = reader.GetString(2);

                        if (!reader.IsDBNull(3))
                            objet.Autre = reader.GetString(3);
                        if (!reader.IsDBNull(4))
                            objet.IdSite = reader.GetInt32(4);


                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Departement> GetAll_Archive_DEPARTMENTS(int idSite)
        {
            List<Departement> listes = new List<Departement>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DEPARTEMENT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inID", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inID"].Value = 0;
                    comand.Parameters["inIdste"].Value = idSite;
                    comand.Parameters["isArchive"].Value = "NON";

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Departement objet = new Departement();
                        objet.IdDepartement = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);

                        if (!reader.IsDBNull(2))
                            objet.CourtLibelle = reader.GetString(2);

                        if (!reader.IsDBNull(3))
                            objet.Autre = reader.GetString(3);
                        if (!reader.IsDBNull(4))
                            objet.IdSite = reader.GetInt32(4);


                        listes.Add(objet);
                    }


                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Departement Get_DEPARTMENTSByID(int idDept)
        {
            Departement objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DEPARTEMENT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inID", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inID"].Value = idDept;
                    comand.Parameters["inIdste"].Value = 0;
                    comand.Parameters["isArchive"].Value = "NON";


                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new Departement();
                        objet.IdDepartement = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);

                        if (!reader.IsDBNull(2))
                            objet.CourtLibelle = reader.GetString(2);

                        if (!reader.IsDBNull(3))
                            objet.Autre = reader.GetString(3);
                        if (!reader.IsDBNull(4))
                            objet.IdSite = reader.GetInt32(4);

                        break;

                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Departement Get_Archive_DEPARTMENTSByID(int idDept)
        {
            Departement objet = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DEPARTEMENT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inID", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);
                    comand.Parameters.Add("isArchive", MySqlDbType.VarChar, 5);
                    comand.Parameters["inID"].Value = idDept;
                    comand.Parameters["inIdste"].Value = 0;
                    comand.Parameters["isArchive"].Value = "OUI";


                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new Departement();
                        objet.IdDepartement = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            objet.Libelle = reader.GetString(1);

                        if (!reader.IsDBNull(2))
                            objet.CourtLibelle = reader.GetString(2);

                        if (!reader.IsDBNull(3))
                            objet.Autre = reader.GetString(3);
                        if (!reader.IsDBNull(4))
                            objet.IdSite = reader.GetInt32(4);

                        break;

                    }


                }


                return objet;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool DEPARTEMENT_ADD(Departement dep)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DEPARTEMENT_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inID", MySqlDbType.Int32);
                    comand.Parameters.Add("inLibelle", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inCourDesc", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inAutre", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdste", MySqlDbType.Int32);

                    comand.Parameters["inID"].Value = dep.IdDepartement;
                    comand.Parameters["inLibelle"].Value = dep.Libelle;
                    comand.Parameters["inCourDesc"].Value = dep.CourtLibelle;
                    comand.Parameters["inAutre"].Value = dep.Autre;
                    comand.Parameters["inIdste"].Value = dep.IdSite;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public bool DEPARTEMENT_DELETE(int Id)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_DEPARTEMENT_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inID", MySqlDbType.Int32);
                    comand.Parameters["inID"].Value = Id;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }
        #endregion

        #region LOG DATAS


        public DataTable Extraction_Select(int idsite)
        {
            DataTable tblClient = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXTRACTFILE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters["inIdSite"].Value = idsite;

                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool EXtractionFile_ADD(int id, DateTime dateExtraction, DateTime dateImport, DateTime dateValide, string periode, int idSite,
            int idSiteMaj, string statut, int ordreStatut, string nomFichier)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXTRACTFILE_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inDateExtraction", MySqlDbType.DateTime);
                    comand.Parameters.Add("inDateImport", MySqlDbType.DateTime);
                    comand.Parameters.Add("inDateExtractValide", MySqlDbType.DateTime);
                    comand.Parameters.Add("inPeriode", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdsiteMaj", MySqlDbType.Int32);
                    comand.Parameters.Add("inStatut", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inOrdreStatut", MySqlDbType.Int32);
                    comand.Parameters.Add("inNomfichier", MySqlDbType.VarChar, 255);


                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inDateExtraction"].Value = dateExtraction;
                    comand.Parameters["inDateImport"].Value = dateImport;
                    comand.Parameters["inDateExtractValide"].Value = dateValide;
                    comand.Parameters["inPeriode"].Value = periode;
                    comand.Parameters["idSite"].Value = idSite;
                    comand.Parameters["inIdsiteMaj"].Value = idSiteMaj;
                    comand.Parameters["inStatut"].Value = statut;
                    comand.Parameters["inOrdreStatut"].Value = ordreStatut;
                    comand.Parameters["inNomfichier"].Value = nomFichier;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public bool EXtractionFile_DELETE(int id, int idSite, string fichier)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXTRACTFILE_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("infichier", MySqlDbType.VarChar, 255);
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["infichier"].Value = fichier;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        /// <summary>
        /// fonction de selection des log des imports qui ont été exécutées
        /// </summary>
        /// <returns></returns>

        public DataTable LogDatas_Import_Select(int idsite)
        {
            DataTable tblClient = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LOG_IMPORT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters["inIdSite"].Value = idsite;

                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Procedure de sauvegarde dun operation dimporttation
        /// </summary>
        /// <param name="periode"></param>
        /// <param name="idSite"></param>
        /// <param name="statut"></param>
        /// <returns></returns>
        public bool LogDataImport_ADD(int id, string periode, int idSite, string statut, string nomFichier)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LOG_IMPORT_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inPeriode", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inStatut", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inNomFichier", MySqlDbType.VarChar, 255);
                    //
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inPeriode"].Value = periode;
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inStatut"].Value = statut;
                    comand.Parameters["inNomFichier"].Value = nomFichier;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        /// <summary>
        /// fonction de sélection des log des operations en export
        /// </summary>
        /// <returns></returns>
        public DataTable LogDatas_Export_Select(int idsite)
        {
            DataTable tblClient = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LOG_EXPORT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters["inIdSite"].Value = idsite;


                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblClient);
                }



                return tblClient;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Procédure de sauvegarde dune operation d'exportation
        /// </summary>
        /// <param name="periode"></param>
        /// <param name="idSite"></param>
        /// <param name="statut"></param>
        /// <returns></returns>
        public bool LogDataExport_ADD(int id, string periode, int idSite, string statut, string nomFichier)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LOG_EXPORT_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inPeriode", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inStatut", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inNomFichier", MySqlDbType.VarChar, 255);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inPeriode"].Value = periode;
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inStatut"].Value = statut;
                    comand.Parameters["inNomFichier"].Value = nomFichier;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public DataSet Get_Export_ListeFacture(DateTime dateDebut, DateTime dateFin, Int32 idSite)
        {
            DataSet listes =new DataSet();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_EXPORT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inPcDateDebut", MySqlDbType.DateTime);
                    comand.Parameters.Add("inPcDateFin", MySqlDbType.DateTime);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);

                    comand.Parameters["inPcDateDebut"].Value = dateDebut;
                    comand.Parameters["inPcDateFin"].Value = dateFin;
                    comand.Parameters["inIdSite"].Value = idSite;
                    MySqlDataAdapter da = new MySqlDataAdapter(comand);
                    da.Fill(listes);

                }


                return listes;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool LogDataExport_DELETE(int id, int idSite)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LOG_EXPORT_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdSite"].Value = idSite;


                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool SetImport_Facture_UPDATE(Facture objFacture)
        {
            bool isValues = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_IMPORT_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inIdUserCreer", MySqlDbType.Int32);
                    comand.Parameters.Add("inUserUpdate", MySqlDbType.Int32);
                    comand.Parameters.Add("inDatePrestation", MySqlDbType.DateTime);

                    comand.Parameters.Add("inIdObjet", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdStatut", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdModepaiement", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdtaxe", MySqlDbType.Int32);
                    comand.Parameters.Add("inIddevise", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIsprorata", MySqlDbType.Bit);
                    comand.Parameters.Add("inJourLimit", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inDatedepot", MySqlDbType.DateTime);
                    comand.Parameters.Add("inIDExploitation", MySqlDbType.Int32);
                    comand.Parameters.Add("inlibelleObjet", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIDepartment", MySqlDbType.Int32);
                    comand.Parameters.Add("inTotalTTC", MySqlDbType.Double);
                    comand.Parameters.Add("inDateEcheance", MySqlDbType.DateTime);
                    comand.Parameters.Add("indateCreation", MySqlDbType.DateTime);



                    comand.Parameters["inId"].Value = objFacture.IdFacture;
                    comand.Parameters["inIdUserCreer"].Value = objFacture.IdCreerpar;
                    comand.Parameters["inUserUpdate"].Value = objFacture.IdModifierPar;
                    comand.Parameters["inDatePrestation"].Value = objFacture.MoisPrestation;

                    comand.Parameters["inIdObjet"].Value = objFacture.IdObjetFacture;
                    comand.Parameters["inIdStatut"].Value = objFacture.IdStatut;
                    comand.Parameters["inIdModepaiement"].Value = objFacture.IdModePaiement;
                    comand.Parameters["inIdtaxe"].Value = objFacture.IdTaxe;
                    comand.Parameters["inIddevise"].Value = objFacture.IdDevise;
                    comand.Parameters["inIdClient"].Value = objFacture.IdClient;
                    comand.Parameters["inIsprorata"].Value = objFacture.IsProrata;
                    comand.Parameters["inJourLimit"].Value = objFacture.JourFinEcheance;
                    comand.Parameters["inDatedepot"].Value = objFacture.DateDepot;
                    comand.Parameters["inIDExploitation"].Value = objFacture.Idexploitation;
                    comand.Parameters["inlibelleObjet"].Value = objFacture.LibelleObjet;
                    comand.Parameters["inIDepartment"].Value = objFacture.IdDepartement;
                    comand.Parameters["inTotalTTC"].Value = objFacture.TotalTTC;

                    comand.Parameters["inDateEcheance"].Value = objFacture.DateEcheance;
                    comand.Parameters["indateCreation"].Value = objFacture.DateCreation;


                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
            return isValues;
        }


        public bool LogDataIMPORT_DELETE(int id, int idSite)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LOG_IMPORT_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdSite"].Value = idSite;


                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        #endregion

        #region MyRegion

        #region PARAMETRES LICENSE

        public object[] PARAMETRES_SELECTBYMODE(string mode)
        {

            object[] tabResult;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PARAMETER_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inMode", MySqlDbType.VarChar, 45);
                    comand.Parameters["inMode"].Value = mode;
                    comand.Connection = con;


                    //MySqlDataReader reader = comand.ExecuteReader();
                    DataTable tblComputer = new DataTable();
                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblComputer);

                    tabResult = new object[3];
                    foreach (DataRow row in tblComputer.Rows)
                    {
                        tabResult[0] = System.Text.Encoding.Default.GetString((byte[])row[0]);
                        tabResult[1] = System.Text.Encoding.Default.GetString((byte[])row[1]);
                        tabResult[2] = row[2];

                        break;
                    }




                }

            }
            catch (MySqlException ex)
            {
                throw new DALException(ex.Message);
            }
            return tabResult;
        }


        public bool PARAMETRES_ADD(string mode, string valeur, DateTime? datedebut)
        {
            bool isValues = false;

            byte[] bmode = Encoding.ASCII.GetBytes(mode);
            byte[] bvaleur = Encoding.ASCII.GetBytes(valeur);


            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_PARAMETER_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inMode", MySqlDbType.Blob);
                    comand.Parameters.Add("inValeur", MySqlDbType.Blob);
                    comand.Parameters.Add("indate", MySqlDbType.DateTime);


                    comand.Parameters["inMode"].Value = bmode;
                    comand.Parameters["inValeur"].Value = bvaleur;
                    comand.Parameters["indate"].Value = datedebut;


                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }
        #endregion 

        #endregion

        #region CONFIGUARTIONS


        public bool CONFIGURATION_ADD(Settings config)
        {
            bool isValues = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_CONFIG_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inCode", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inValeur", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdsite", MySqlDbType.Int32);

                    comand.Parameters["inCode"].Value = config.Code;
                    comand.Parameters["inValeur"].Value = config.Libelle;
                    comand.Parameters["inIdsite"].Value = config.IdSite;



                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public List<Settings> CONFIGURATION_SELECT(string code, Int32 idSite)
        {
            List<Settings> listes = new List<Settings>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();

                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_CONFIG_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;



                    comand.Parameters.Add("inCode", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);

                    comand.Parameters["inCode"].Value = string.Empty;
                    comand.Parameters["inIdsite"].Value = idSite;

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        Settings objet = new Settings();
                        objet.Code = reader.GetString(0);
                        objet.Libelle = reader.GetString(1);
                        objet.IdSite = reader.GetInt32(2);

                        listes.Add(objet);
                    }

                    return listes;
                }

            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Settings CONFIGURATION_SELECTBYCODE(string code, Int32 idSite)
        {
            Settings objet = null;

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_CONFIG_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Connection = con;
                    con.Open();

                    comand.Parameters.Add("inCode", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);

                    comand.Parameters["inCode"].Value = code;
                    comand.Parameters["inIdsite"].Value = idSite;

                    MySqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        objet = new Settings();
                        objet.Code = reader.GetString(0);
                        objet.Libelle = reader.GetString(1);
                        objet.IdSite = reader.GetInt32(2);
                    }

                    return objet;
                }

            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public bool CONFIGURATION_DELETE(string code, Int32 idSite)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_CONFIG_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inCode", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters["inCode"].Value = code;
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }
        #endregion

        #region COMPUTER LOGGIN

        public DataTable Computer_connect_select()
        {
            DataTable tblComputer = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_COMPUTER_CONNECT_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    //comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    //comand.Parameters["inIdSite"].Value = idsite;

                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblComputer);
                    // foreach (DataRow row in tableComputer.Rows)
                    // Console.WriteLine(" host name:{0}  - user name: {1}", System.Text.Encoding.Default.GetString((byte[])row[0]), row[1]);
                }



                return tblComputer;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Computer_Connect_ADD(string NomMachine, string nomUtilisateur)
        {
            bool isValues = false;
            byte[] bNomMachine = Encoding.ASCII.GetBytes(NomMachine);
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_COMPUTER_CONNECT_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inHostname", MySqlDbType.Blob);
                    comand.Parameters.Add("inUserName", MySqlDbType.VarChar, 255);

                    comand.Parameters["inHostname"].Value = bNomMachine;
                    comand.Parameters["inUserName"].Value = nomUtilisateur;



                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public bool Computer_Connect_Delete(string NomMachine)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_COMPUTER_CONNECT_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inHostname", MySqlDbType.VarChar, 255);
                    comand.Parameters["inHostname"].Value = NomMachine;
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }
        #endregion


        #region DISPLAY LANGUAGE

        public DataTable ListeLangue()
        {
            DataTable tbble = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LISTELANGUE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;


                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tbble);
                }

            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return tbble;
        }

        public DataTable ListeLabelInVieuw(int idlanguages, int idMenus)
        {
            DataTable tbble = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_GET_DETAIL_DISPLAYVIEWS;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdVues", MySqlDbType.Int32);
                    comand.Parameters["inIdLangue"].Value = idlanguages;
                    comand.Parameters["inIdVues"].Value = idMenus;

                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tbble);
                }

            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return tbble;
        }
        #endregion

        #region LICENSES


        public DataTable LicenseSelect(int idSite, string nomMachine, string diskdur, Guid gui)
        {
            DataTable tblComputer = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LICENSE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                    comand.Parameters.Add("inNumMachine", MySqlDbType.Blob);
                    comand.Parameters.Add("inguidMachine", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inHdd", MySqlDbType.Blob);
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inNumMachine"].Value = Encoding.ASCII.GetBytes(nomMachine);
                    comand.Parameters["inguidMachine"].Value = gui.ToString();
                    comand.Parameters["inHdd"].Value = Encoding.ASCII.GetBytes(diskdur);

                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblComputer);

                }

                return tblComputer;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable LicenseSelectListComputer(int idSite)
        {
            DataTable tblComputer = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LICENSE_SELECTLIST;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                    comand.Parameters["inIdSite"].Value = idSite;


                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblComputer);

                }

                return tblComputer;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int LicenseSelectAll(int idSite)
        {

            int valuesBack = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LICENSE_SELECTALL;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdsite", MySqlDbType.Int32);
                    comand.Parameters["inIdSite"].Value = idSite;
                    object de = comand.ExecuteScalar();
                    valuesBack = int.Parse(de.ToString());

                }

                return valuesBack;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool LicenseADD(int idSite, string NomMachine, string numeroLicense, string diskdur, Guid gui, string hostName, string ipName)
        {
            bool isValues = false;
            // byte[] bNomMachine = Encoding.ASCII.GetBytes(NomMachine);
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LICENSE_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inLicenseNumero", MySqlDbType.Blob);
                    comand.Parameters.Add("inNumMachine", MySqlDbType.Blob);
                    comand.Parameters.Add("inHdd", MySqlDbType.Blob);
                    comand.Parameters.Add("inguidMachine", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inHostname", MySqlDbType.Blob);
                    comand.Parameters.Add("inIpName", MySqlDbType.Blob);


                    byte[] bNomMachine = Encoding.ASCII.GetBytes(numeroLicense);

                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inLicenseNumero"].Value = Encoding.ASCII.GetBytes(numeroLicense);
                    comand.Parameters["inNumMachine"].Value = Encoding.ASCII.GetBytes(NomMachine);
                    comand.Parameters["inHdd"].Value = Encoding.ASCII.GetBytes(diskdur);
                    comand.Parameters["inguidMachine"].Value = gui.ToString();
                    comand.Parameters["inHostname"].Value = Encoding.ASCII.GetBytes(hostName);
                    comand.Parameters["inIpName"].Value = Encoding.ASCII.GetBytes(ipName);


                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool LicenseACTIVE(int idSite, string NomMachine, bool isActive)
        {
            bool isValues = false;
            // byte[] bNomMachine = Encoding.ASCII.GetBytes(NomMachine);
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LICENSE_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inNumMachine", MySqlDbType.Blob);
                    comand.Parameters.Add("inactive", MySqlDbType.Bit);
                    byte[] bNomMachine = Encoding.ASCII.GetBytes(NomMachine);

                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inNumMachine"].Value = bNomMachine;
                    comand.Parameters["inactive"].Value = isActive;


                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool LicenseUPDATE(int idSite, string NomMachine, string numeroLicense, string diskdur, Guid gui)
        {
            bool isValues = false;
            // byte[] bNomMachine = Encoding.ASCII.GetBytes(NomMachine);
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LICENSE_UPDATE;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inLicenseNumero", MySqlDbType.Blob);
                    comand.Parameters.Add("inNumMachine", MySqlDbType.Blob);
                    comand.Parameters.Add("inguidMachine", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inHdd", MySqlDbType.Blob);

                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inLicenseNumero"].Value = Encoding.ASCII.GetBytes(NomMachine);
                    comand.Parameters["inNumMachine"].Value = Encoding.ASCII.GetBytes(numeroLicense);
                    comand.Parameters["inguidMachine"].Value = gui.ToString();
                    comand.Parameters["inHdd"].Value = Encoding.ASCII.GetBytes(diskdur);

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool LicenseLASTCONNECTION(int idSite, string NomMachine, string numeroLicense, string diskdur, Guid gui)
        {
            bool isValues = false;
            // byte[] bNomMachine = Encoding.ASCII.GetBytes(NomMachine);
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LICENSE_UPDATE_DATECONECTION;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inNumMachine", MySqlDbType.Blob);
                    comand.Parameters.Add("inDateConnecte", MySqlDbType.Blob);
                    comand.Parameters.Add("inguidMachine", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inHdd", MySqlDbType.Blob);

                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inNumMachine"].Value = Encoding.ASCII.GetBytes(NomMachine);
                    comand.Parameters["inDateConnecte"].Value = Encoding.ASCII.GetBytes(numeroLicense);
                    comand.Parameters["inguidMachine"].Value = gui.ToString();
                    comand.Parameters["inHdd"].Value = Encoding.ASCII.GetBytes(diskdur);

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        #endregion

        #region BACKUP & Archives


        public bool BackUpBD_ADD(Int32 ID, string CreateBY,string ModeExportimport,string BackupDump,string FromDB,string ToDB)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                try
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_BACKUP_DB_ADD";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inCreateBY", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inModeExportimport", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inBackupDump", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inFromDB", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inToDB", MySqlDbType.VarChar, 255);

                    comand.Parameters["inId"].Value = ID;
                    comand.Parameters["inCreateBY"].Value = CreateBY;
                    comand.Parameters["inModeExportimport"].Value = ModeExportimport;
                    comand.Parameters["inBackupDump"].Value = BackupDump;
                    comand.Parameters["inFromDB"].Value = FromDB;
                    comand.Parameters["inToDB"].Value = ToDB;


                    comand.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return true;

            }

        }

        public DataTable BackUpBD_SELECT(int ID)
        {
            DataTable tblComputer = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_BACKUP_DB_SELECT";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = ID;
                  
                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblComputer);

                }

                return tblComputer;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool BackUpBD_DELETE(Int32 ID)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                try
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_BACKUP_DB_DELETE";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = ID;
                    comand.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return true;

            }

        }



        public bool BackUp_Clean_tables(Int32 idSite)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                try
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_BACKUP_CLEAN_TABLES";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);

                    comand.Parameters["inIdSite"].Value = idSite;


                    comand.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return true;

            }

        }

        /// <summary>
        /// retourne la liste des ligne de fature à archiver
        /// </summary>
        /// <param name="idSite"></param>
        /// <param name="periodeDebut"></param>
        /// <param name="periodeFin"></param>
        /// <returns></returns>
        public DataTable ArchiveSelecte(int idSite, DateTime periodeDebut, DateTime periodeFin)
        {
            DataTable tblComputer = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_ARCHIVE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inDateFrom", MySqlDbType.DateTime);
                    comand.Parameters.Add("inDateTo", MySqlDbType.DateTime);
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inDateFrom"].Value = periodeDebut;
                    comand.Parameters["inDateTo"].Value = periodeFin;
                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblComputer);

                }

                return tblComputer;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


       

        public bool ArchiveGenerate(Int64 IdFacture,Int32 idSite)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                try { 
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_ARCHIVES_GENERATE";
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
             
                comand.Parameters["inIdFacture"].Value = IdFacture;
                comand.Parameters["inIdSite"].Value = idSite;
              
              
                comand.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return true;

            }
          
        }


        public DataTable ArchiveGenerate_List(int idSite, DateTime periodeDebut, DateTime periodeFin, bool estValider, bool estEncours, bool EstSortie, bool EstAvoir, bool EstSupendu, string cheminLog)
        {
            DataTable tblComputer = new DataTable();

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                try
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_ARCHIVES_GENERATE_LISTE";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inDateFrom", MySqlDbType.DateTime);
                    comand.Parameters.Add("inDateTo", MySqlDbType.DateTime);
                    comand.Parameters.Add("estValider", MySqlDbType.Int32);
                    comand.Parameters.Add("estEncours", MySqlDbType.Int32);
                    comand.Parameters.Add("EstAvoir", MySqlDbType.Int32);
                    comand.Parameters.Add("EstSupendu", MySqlDbType.Int32);
                    comand.Parameters.Add("EstSortie", MySqlDbType.Int32);
                    comand.Parameters.Add("backUpBy", MySqlDbType.VarChar, 255);
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inDateFrom"].Value = periodeDebut;
                    comand.Parameters["inDateTo"].Value = periodeFin;
                    comand.Parameters["estValider"].Value = estValider == true ? 14003 : 0;
                    comand.Parameters["estEncours"].Value = estEncours == true ? 14002 : 0;
                    comand.Parameters["EstAvoir"].Value = EstAvoir == true ? 14007 : 0;
                    comand.Parameters["EstSupendu"].Value = EstSupendu == true ? 14005 : 0;
                    comand.Parameters["EstSortie"].Value = EstSortie == true ? 14004 : 0;
                    comand.Parameters["backUpBy"].Value = cheminLog;

                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblComputer);
                    return tblComputer;
                    //comand.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.Message);
                }
              

            }

        }


        public DataSet Archive_previous_FreeProduct(int idsite)
        {
            DataSet tblComputer = new DataSet();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_ARCHIVES_SELECT_FREE_PRODUCT";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);

                    comand.Parameters["inIdSite"].Value = idsite;


                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblComputer);

                }

                return tblComputer;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Archive_previous_FreeProductsDelete(int idsite, Int32 inidProduit)
        {

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_ARCHIVES_SDELETE_FREE_PRODUCT";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdProduit", MySqlDbType.Int32);

                    comand.Parameters["inIdSite"].Value = idsite;
                    comand.Parameters["inIdProduit"].Value = inidProduit;
                    comand.ExecuteNonQuery();

                    return true;
                   
                }


            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }






        public DataTable Archive_previous_FreeClients(int idsite)
        {
            DataTable tblComputer = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_ARCHIVES_SELECT_FREE_CLT_ADD";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters["inIdSite"].Value = idsite;
                 

                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblComputer);

                }

                return tblComputer;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Archive_previous_FreeClientsDelete(int idsite,Int32 inidClient)
        {
         
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_ARCHIVES_SDELETE_FREE_CLT_ADD";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);

                    comand.Parameters["inIdClient"].Value = inidClient;
                    comand.Parameters["inIdSite"].Value = idsite;
                    comand.ExecuteNonQuery();

                    return true;

                }

                
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ArchiveSelecteLignesFactures(Int64 idfacture)
        {
            DataTable tblComputer = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_LIGNE_FACTURE_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inId", MySqlDbType.Int64);
                    comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                    comand.Parameters["inId"].Value = 0;
                    comand.Parameters["inIdFacture"].Value = idfacture;

                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblComputer);

                }

                return tblComputer;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// suprime les de factures archiver
        /// </summary>
        /// <param name="idSite"></param>
        /// <param name="periodeDebut"></param>
        /// <param name="periodeFin"></param>
        /// <returns></returns>
        public bool ArchiveDelete(int idSite, DateTime periodeDebut, DateTime periodeFin)
        {

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_ARCHIVE_DELETE;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inDateFrom", MySqlDbType.DateTime);
                    comand.Parameters.Add("inDateTo", MySqlDbType.DateTime);
                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inDateFrom"].Value = periodeDebut;
                    comand.Parameters["inDateTo"].Value = periodeFin;
                    comand.ExecuteNonQuery();
                    return true;
                }


            }
            catch (MySqlException ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }


        public DataSet BackUpList(int idSite)
        {
            DataSet tblComputer = new DataSet();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_BACKUP_LOG_SELECT;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters["inIdSite"].Value = idSite;
                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblComputer);

                }

                return tblComputer;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable BackUmaxMindate(int idSite)
        {
            DataTable tblComputer = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_FactureSelectMinMaxDateCreate;
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters["inIdSite"].Value = idSite;
                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblComputer);

                }

                return tblComputer;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool BackUpLoggAdd(ref Int32 id , int idSite, DateTime dateFrom, DateTime dateTo, string backUpby, string observation,
            Int32 stat1, Int32 stat2, Int32 stat3, Int32 stat4, Int32 stat5)
        {
            bool isValues = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_BACKUP_LOG_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inDateFrom", MySqlDbType.DateTime);
                    comand.Parameters.Add("inDateTo", MySqlDbType.DateTime);
                    comand.Parameters.Add("inBackupBy", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inObs", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inP1", MySqlDbType.Int32);
                    comand.Parameters.Add("inP2", MySqlDbType.Int32);
                    comand.Parameters.Add("inP3", MySqlDbType.Int32);
                    comand.Parameters.Add("inP4", MySqlDbType.Int32);
                    comand.Parameters.Add("inP5", MySqlDbType.Int32);
                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Direction = ParameterDirection.Output;

                    comand.Parameters["inIdSite"].Value = idSite;
                    comand.Parameters["inDateFrom"].Value = dateFrom;
                    comand.Parameters["inDateTo"].Value = dateTo;
                    comand.Parameters["inBackupBy"].Value = backUpby;
                    comand.Parameters["inObs"].Value = observation;
                    comand.Parameters["inP1"].Value = stat1;
                    comand.Parameters["inP2"].Value = stat2;
                    comand.Parameters["inP3"].Value = stat3;
                    comand.Parameters["inP4"].Value = stat4;
                    comand.Parameters["inP5"].Value = stat5;
                    comand.Parameters["inId"].Value = stat5;
                  
                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                    id =Convert.ToInt32( comand.Parameters["inId"].Value);
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }

        public bool BackUpLoggDelete(int id)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_BACKUP_LOG_DELETE";
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters["inId"].Value = id;
                   

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }
      
        #endregion

        #region Archive Add


      

        public bool OBJET_ARCHIVE_ADD(int id, Int32 idSite, string libelle, int idLangue)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_ARCHIVE_OBJET_INSERT";
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inLabell", MySqlDbType.String);
                    comand.Parameters.Add("inIdlangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inLabell"].Value = libelle;
                    comand.Parameters["inIdlangue"].Value = idLangue;
                    comand.Parameters["inIdSte"].Value = idSite;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public bool OBJET_FACTURE_ARCHIVE_ADD(int id, int idOjetGen, int idClient, int idSte)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_ARCHIVE_OBJET_FACTURE_INSERT";
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdObjet", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDClient", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = id;
                    comand.Parameters["inIdObjet"].Value = idOjetGen;
                    comand.Parameters["inIDClient"].Value = idClient;
                    comand.Parameters["inIdSte"].Value = idSte;


                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public bool ARCHIVE_EXPLOITATION_ADD(ExploitationFacture objFacture)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_ARCHIVE_EXPLOITATION_ADD";
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inLabell", MySqlDbType.String);
                    comand.Parameters.Add("inIdlangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdSte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdClient", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = objFacture.IdExploitation;
                    comand.Parameters["inLabell"].Value = objFacture.Libelle;
                    comand.Parameters["inIdlangue"].Value = objFacture.IdLangue;
                    comand.Parameters["inIdSte"].Value = objFacture.IdSite;
                    comand.Parameters["inIdClient"].Value = objFacture.IdClient;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


        public bool ARCHIVE_TAXE_ADD(Taxe taxe)
        {
            bool isValues = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_ARCHIVE_TAXE_ADD";
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inLibelle", MySqlDbType.String);
                    comand.Parameters.Add("inTaux", MySqlDbType.String);
                    comand.Parameters.Add("idSite", MySqlDbType.Int32);

                    comand.Parameters["inId"].Value = taxe.ID_Taxe;
                    comand.Parameters["inLibelle"].Value = taxe.Libelle;
                    comand.Parameters["inTaux"].Value = taxe.Taux;
                    comand.Parameters["idSite"].Value = taxe.IdSite;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }



        public bool ARCHIVE_CLIENT_ADD(int IdClient, int IdLangue, int Idporata, int IdSite, int IdExonere, int IdCompte, int IdDeviseFact, int IdTerme, string NumeroImatriculation,
            string TermeDescription, int TermeNombre, string Rue1, string Rue2, string NomClient, string NumeroContribuable, string Ville, string BoitePostal, string DateEcheance)
        {
            bool isValues = false;
            try
            {

                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    MySqlCommand comand = new MySqlCommand();
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = Constants.PS_CLIENT_ADD;
                    comand.CommandTimeout = Constants.TimeOut;

                    comand.Parameters.Add("inId", MySqlDbType.Int32);
                    comand.Parameters.Add("inBp", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inVille", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inNumContrib", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inNomClient", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inRue1", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inRue2", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdLangue", MySqlDbType.Int32);
                    comand.Parameters.Add("inTermeNombre", MySqlDbType.Int32);
                    comand.Parameters.Add("inTermeDescript", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inPorata", MySqlDbType.Int32);
                    comand.Parameters.Add("inDateEcheance", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdExonere", MySqlDbType.Int32);
                    comand.Parameters.Add("inNumeroImat", MySqlDbType.VarChar, 255);
                    comand.Parameters.Add("inIdcompte", MySqlDbType.Int32);
                    comand.Parameters.Add("inIDevise", MySqlDbType.Int32);
                    comand.Parameters.Add("inIdTerme", MySqlDbType.Int32);


                    comand.Parameters["inId"].Value = IdClient;
                    comand.Parameters["inBp"].Value = BoitePostal;
                    comand.Parameters["inVille"].Value = Ville;
                    comand.Parameters["inNumContrib"].Value = NumeroContribuable;
                    comand.Parameters["inNomClient"].Value = NomClient;
                    comand.Parameters["inRue1"].Value = Rue1;
                    comand.Parameters["inRue2"].Value = Rue2;

                    comand.Parameters["inIdLangue"].Value = IdLangue;
                    comand.Parameters["inTermeNombre"].Value = TermeNombre;
                    comand.Parameters["inTermeDescript"].Value = TermeDescription;
                    comand.Parameters["inPorata"].Value = Idporata;
                    comand.Parameters["inDateEcheance"].Value = string.IsNullOrEmpty(DateEcheance) ? null : DateTime.Parse(DateEcheance).ToString();
                    comand.Parameters["inIdSite"].Value = IdSite;
                    comand.Parameters["inIdExonere"].Value = IdExonere;
                    comand.Parameters["inNumeroImat"].Value = NumeroImatriculation;
                    comand.Parameters["inIdcompte"].Value = IdCompte;
                    comand.Parameters["inIDevise"].Value = IdDeviseFact;
                    comand.Parameters["inIdTerme"].Value = IdTerme;

                    comand.Connection = con;
                    con.Open();
                    comand.ExecuteNonQuery();
                }
                isValues = true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return isValues;
        }


      

        #endregion

        #region LOG COMPTA

        public DataTable GetLogComptaByJv(int idDateJv, string typeMesage)
        {
            DataTable table = new DataTable();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_LogComtaSelect";
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdJv", MySqlDbType.Int32);
                comand.Parameters.Add("inTypeMessage", MySqlDbType.VarChar, 5);
                comand.Parameters["inIdJv"].Value = idDateJv;
                comand.Parameters["inTypeMessage"].Value = typeMesage;

                MySqlDataAdapter data = new MySqlDataAdapter(comand);
                data.Fill(table);

                return table;
            }
        }

        public bool LogComptaAdd(int idDateJv, string numerofact,string MessageError,string typeMesage , bool valMessage )
        {
           
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_LOgComptaAdd";
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdJv", MySqlDbType.Int32);
                comand.Parameters.Add("inErrorMsg", MySqlDbType.VarChar,255);
                comand.Parameters.Add("inNumeroFacture", MySqlDbType.VarChar,255);
                comand.Parameters.Add("inTypeMessage", MySqlDbType.VarChar, 5);
                comand.Parameters.Add("invalMessage", MySqlDbType.Bit);
                comand.Parameters["inIdJv"].Value = idDateJv;
                comand.Parameters["inErrorMsg"].Value = MessageError;
                comand.Parameters["inNumeroFacture"].Value = numerofact;
                comand.Parameters["inTypeMessage"].Value = typeMesage;
                comand.Parameters["invalMessage"].Value = valMessage;
            
                comand.ExecuteNonQuery();

                return true;
            }
        }

        public bool LogComptaDeLETE(int idDateJv)
        {
            //(inIdJv int,inErrorMsg varchar(255),inNumeroFacture varchar(255))
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_LogComtaDelete";
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdJv", MySqlDbType.Int32);
                comand.Parameters["inIdJv"].Value = idDateJv;
                comand.ExecuteNonQuery();

                return true;
            }
        }

        #endregion

        #region COMPTABILITE



        #region Compte ohada


        #endregion

        public DataTable GetComptaGene_Liste_ChampFichier()
        {
            DataTable table = new DataTable();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_CMPTA_CHAMP_PARAMETRE;
                comand.CommandTimeout = Constants.TimeOut;
              

                MySqlDataAdapter data = new MySqlDataAdapter(comand);
                data.Fill(table);

                return table;
            }
        }


        // champs selecyionnee
        public DataTable GetComptaGene_Param_Liste()
        {
            DataTable table = new DataTable();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_CMPTA_CHAMP_SELECT;
                comand.CommandTimeout = Constants.TimeOut;

                MySqlDataAdapter data = new MySqlDataAdapter(comand);
                data.Fill(table);

                return table;
            }
        }


        public bool GetComptaGene_Param_Add(int id,int idChamp,int taille,int position)
        {

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_CMPTA_CHAMP_ADD;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters.Add("inIdChamp", MySqlDbType.Int32);
                comand.Parameters.Add("inPosition", MySqlDbType.Int32);
                comand.Parameters.Add("inTaille", MySqlDbType.Int32);
                comand.Parameters["inId"].Value = id;
                comand.Parameters["inIdChamp"].Value = idChamp;
                comand.Parameters["inPosition"].Value = position;
                comand.Parameters["inTaille"].Value = taille;
            
                comand.ExecuteNonQuery();

                return true;
            }
        }

        public bool GetComptaGene_Param_Delete(int id)
        {

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_CMPTA_CHAMP_DELETE;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters["inId"].Value = id;

                comand.ExecuteNonQuery();

                return true;
            }
        }
        /// <summary>
        /// liste des champ du fichier de parametrage
        /// </summary>
        /// <returns></returns>

        public DataTable GetComptaGene_Liste()
        {
            DataTable table = new DataTable();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_CMPTA_LISTE_SELECT;
                comand.CommandTimeout = Constants.TimeOut;
               
                MySqlDataAdapter data = new MySqlDataAdapter(comand);
                data.Fill(table);

                return table;
            }
        }

        /// <summary>
        /// ajout dun nouveau libelle de paarmetrage
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libelle"></param>
        /// <returns></returns>
        public bool GetComptaGene_Add(int id,string libelle,int code)
        {
          
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_CMPTA_LISTE_ADD;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters.Add("inLibelle", MySqlDbType.VarChar,255);
                comand.Parameters.Add("inCode", MySqlDbType.Int32);
                comand.Parameters["inId"].Value = id;
                comand.Parameters["inLibelle"].Value = libelle;
                comand.Parameters["inCode"].Value = code;
                comand.ExecuteNonQuery();
           
                return true;
            }
        }

        //suppression dun champ
        public bool GetComptaGene_Delete(int id)
        {

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_CMPTA_LISTE_DELETE;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters["inId"].Value = id;
              
                comand.ExecuteNonQuery();

                return true;
            }
        }




        public DataSet GetJournalListToExport(int idDate,string mode)
        {
            DataSet table = new DataSet();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_JV_ExportGererate;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdDate", MySqlDbType.Int32);
                comand.Parameters.Add("inIntMode", MySqlDbType.VarChar,5);
                comand.Parameters["inIdDate"].Value = idDate;
                comand.Parameters["inIntMode"].Value = mode;
                MySqlDataAdapter data = new MySqlDataAdapter(comand);
                data.Fill(table);

                return table;
            }
        }

        public bool JournalVenteToxexportupdate(int inIdDate)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_JV_ExportUpdate;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdDate", MySqlDbType.Int32);
                comand.Parameters["inIdDate"].Value = inIdDate;
                comand.ExecuteNonQuery();
                return true;
            }

        }
        
        /// <summary>
        /// return listes du journal selelectionne
        /// </summary>
        /// <param name="idDate"></param>
        /// <returns></returns>

        public List<JournalVenteGroupe> GetJournalVente_HistoriqueGroupBy(int idDate)
        {
            List<JournalVenteGroupe> listes = new List<JournalVenteGroupe>();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_JV_HSTCMPGROUPE;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdDateGenerate", MySqlDbType.Int32);
                comand.Parameters["inIdDateGenerate"].Value = idDate;
                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    JournalVenteGroupe jv = new JournalVenteGroupe();

                    jv.CodeJournal = reader.GetString(0);
                    jv.Datefacture = reader.GetDateTime(1);
                    if (!reader.IsDBNull(2))
                        jv.Numerofacture = reader.GetString(2);
                    if (!reader.IsDBNull(3))
                        jv.MontantDebit = reader.GetDouble(3);
                    if (!reader.IsDBNull(4))
                        jv.MontantCredit = reader.GetDouble(4);
                    jv.IdFactures = reader.GetInt64(5);
                    if (!reader.IsDBNull(6))
                    jv.LibelleStatut = reader.GetString(6);
                    if (!reader.IsDBNull(7))
                    jv.Libellefacture = reader.GetString(7);
                    if (!reader.IsDBNull(8))
                    jv.LibelleStatutOperation = reader.GetString(8);
                    jv.IdStatut = reader.GetInt32(9);
                    if (!reader.IsDBNull(5))
                    {
                      jv.JournalVentes = GetJournalVente_Historique(reader.GetInt64(5));
                       // jv.JournalVentesAnalytiques = GetJournalVente_HistoriqueAnal(reader.GetInt64(5));
                    }
                    jv.Id = reader.GetInt32(10);
                    listes.Add(jv);
                }
            }

            return listes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Idfactures"></param>
        /// <returns></returns>
        public List<JournalVentes> GetJournalVente_Historique(Int64 Idfactures)
        {
            List<JournalVentes> listes = new List<JournalVentes>();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_JV_HSTCMPT;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                comand.Parameters["inIdFacture"].Value = Idfactures;
                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    JournalVentes jv = new JournalVentes();
                   
                    if (!reader.IsDBNull(0))
                        jv.NumCompteGeneral = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        jv.NumeroCompteTiers = reader.GetString(1);
                    else jv.NumeroCompteTiers = string.Empty;

                    if (!reader.IsDBNull(2))
                        jv.NumeroCompteAnalytique = reader.GetString(2);
                    else jv.NumeroCompteAnalytique = string.Empty;

                    if (!reader.IsDBNull(3))
                        jv.LibelleSectionAnal = reader.GetString(3);
                    else jv.LibelleSectionAnal = string.Empty;

                    if (!reader.IsDBNull(4))
                        jv.MontantDebit = reader.GetDouble(4);
                    if (!reader.IsDBNull(5))
                        jv.MontantCredit = reader.GetDouble(5);
               
                    if (!reader.IsDBNull(6))
                        jv.Ordre = reader.GetString(6);
                    if (!reader.IsDBNull(7))
                        jv.LibelleOpertion = reader.GetString(7);
                    if (!reader.IsDBNull(8))
                        jv.StatutOperation = reader.GetString(8);
                    if (!reader.IsDBNull(9))
                        jv.IdStatut = reader.GetInt32(9);
                    jv.Id = reader.GetInt32(10);
                    listes.Add(jv);
                }
            }

            return listes;
        }

        public List<JournalVentes> GetJournalVente_Historiquegenerate(ref DataTable ListeFact, int idDate,string mode)
        {
            List<JournalVentes> listes = new List<JournalVentes>();
            DataSet table = new DataSet();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_JV_HSTCMPT;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdDate", MySqlDbType.Int64);
                comand.Parameters.Add("inIntMode", MySqlDbType.VarChar,5);
                comand.Parameters["inIdDate"].Value = idDate;
                comand.Parameters["inIntMode"].Value = mode;
                MySqlDataAdapter data = new MySqlDataAdapter(comand);
                data.Fill(table);

                IDataReader reader =table.Tables[0].CreateDataReader();
                ListeFact = table.Tables[1];

                //MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    JournalVentes jv = new JournalVentes();

                    if (!reader.IsDBNull(0))
                        jv.NumCompteGeneral = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        jv.NumeroCompteTiers = reader.GetString(1);
                    else jv.NumeroCompteTiers = string.Empty;

                    if (!reader.IsDBNull(2))
                        jv.NumeroCompteAnalytique = reader.GetString(2);
                    else jv.NumeroCompteAnalytique = string.Empty;

                    if (!reader.IsDBNull(3))
                        jv.LibelleSectionAnal = reader.GetString(3);
                    else jv.LibelleSectionAnal = string.Empty;

                    if (!reader.IsDBNull(4))
                        jv.MontantDebit = reader.GetDouble(4);
                    if (!reader.IsDBNull(5))
                        jv.MontantCredit = reader.GetDouble(5);

                    if (!reader.IsDBNull(6))
                    {
                        object vs = reader.GetValue(6);
                        jv.Ordre = vs.ToString();
                       // jv.Ordre = reader.GetInt32(6).ToString();
                    }
                    if (!reader.IsDBNull(7))
                        jv.LibelleOpertion = reader.GetString(7);
                    if (!reader.IsDBNull(8))
                        jv.StatutOperation = reader.GetString(8);
                    if (!reader.IsDBNull(9))
                        jv.IdStatut = (int?)Convert.ToUInt32(reader["ID_Statut"]);
                    jv.Id =(int) Convert.ToUInt32(reader["ID"]);// reader.GetInt32(10);

                    jv.Datefacture = reader.GetDateTime(11);
                    if (!reader.IsDBNull(12))
                        jv.CodeJournal = reader.GetString(12);
                    if (!reader.IsDBNull(13))
                        jv.NumeroJournal = reader.GetString(13);
                    if (!reader.IsDBNull(14))
                        jv.NumeroFacture = reader.GetString(14);
                    if (!reader.IsDBNull(15))
                        jv.IdDate = (int)Convert.ToUInt32(reader["Id_Date"]);// reader.GetInt32(15);
                    if (!reader.IsDBNull(18))
                        jv.IdProduit = (int)Convert.ToUInt32(reader["ID_Produit"]);// reader.GetInt32(18);
                    if (!reader.IsDBNull(19))
                        jv.IdClient = (int)Convert.ToUInt32(reader["ID_Client"]);// reader.GetInt32(19);
                    if (!reader.IsDBNull(20))
                        jv.LibelleClient = reader.GetString(20);

                    listes.Add(jv);
                }
            }

            return listes;
        }


        public List<JournalVentes> GetJournalVente_Search(ref DataTable ListeFact, string numeroFacture)
        {
            List<JournalVentes> listes = new List<JournalVentes>();
            DataSet table = new DataSet();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_JournalVente_SearchBy";
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inNumFacture", MySqlDbType.VarChar,100);
                comand.Parameters["inNumFacture"].Value = numeroFacture;
                
                MySqlDataAdapter data = new MySqlDataAdapter(comand);
                data.Fill(table);

                IDataReader reader = table.Tables[0].CreateDataReader();
                ListeFact = table.Tables[1];

                //MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    JournalVentes jv = new JournalVentes();

                    if (!reader.IsDBNull(0))
                        jv.NumCompteGeneral = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        jv.NumeroCompteTiers = reader.GetString(1);
                    else jv.NumeroCompteTiers = string.Empty;

                    if (!reader.IsDBNull(2))
                        jv.NumeroCompteAnalytique = reader.GetString(2);
                    else jv.NumeroCompteAnalytique = string.Empty;

                    if (!reader.IsDBNull(3))
                        jv.LibelleSectionAnal = reader.GetString(3);
                    else jv.LibelleSectionAnal = string.Empty;

                    if (!reader.IsDBNull(4))
                        jv.MontantDebit = reader.GetDouble(4);
                    if (!reader.IsDBNull(5))
                        jv.MontantCredit = reader.GetDouble(5);

                    if (!reader.IsDBNull(6))
                    {
                        object vs = reader.GetValue(6);
                        jv.Ordre = vs.ToString();
                        // jv.Ordre = reader.GetInt32(6).ToString();
                    }
                    if (!reader.IsDBNull(7))
                        jv.LibelleOpertion = reader.GetString(7);
                    if (!reader.IsDBNull(8))
                        jv.StatutOperation = reader.GetString(8);
                    if (!reader.IsDBNull(9))
                        jv.IdStatut = (int?)Convert.ToUInt32(reader["ID_Statut"]);
                    jv.Id = (int)Convert.ToUInt32(reader["ID"]);// reader.GetInt32(10);

                    jv.Datefacture = reader.GetDateTime(11);
                    if (!reader.IsDBNull(12))
                        jv.CodeJournal = reader.GetString(12);
                    if (!reader.IsDBNull(13))
                        jv.NumeroJournal = reader.GetString(13);
                    if (!reader.IsDBNull(14))
                        jv.NumeroFacture = reader.GetString(14);
                    if (!reader.IsDBNull(15))
                        jv.IdDate = (int)Convert.ToUInt32(reader["Id_Date"]);// reader.GetInt32(15);
                    if (!reader.IsDBNull(18))
                        jv.IdProduit = (int)Convert.ToUInt32(reader["ID_Produit"]);// reader.GetInt32(18);
                    if (!reader.IsDBNull(19))
                        jv.IdClient = (int)Convert.ToUInt32(reader["ID_Client"]);// reader.GetInt32(19);
                    if (!reader.IsDBNull(20))
                        jv.LibelleClient = reader.GetString(20);

                    listes.Add(jv);
                }
            }

            return listes;
        }


        public List<JournalVentes> GetJournalVente_HistoriqueToExport(int idDategenerate)
        {
            List<JournalVentes> listes = new List<JournalVentes>();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_JV_HSTCMPT;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                comand.Parameters["inIdFacture"].Value = idDategenerate;
                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    JournalVentes jv = new JournalVentes();

                    if (!reader.IsDBNull(0))
                        jv.NumCompteGeneral = reader.GetString(0);
                    //if (!reader.IsDBNull(1))
                    //    jv.ChampX = reader.GetString(1);
                    //else jv.ChampX = string.Empty;

                    //if (!reader.IsDBNull(2))
                    //    jv.NumerocompteTiers = reader.GetString(2);
                    //jv.LibelleMontant = reader.GetString(3);
                    //if (!reader.IsDBNull(4))
                    //    jv.Numerofacture = reader.GetString(4);
                    //if (!reader.IsDBNull(5))
                    //    jv.MontantFacture = reader.GetDouble(5);
                    listes.Add(jv);
                }
            }

            return listes;
        }

        public List<JournalVenteCompteAnalytique> GetJournalVente_HistoriqueAnal(Int64 IdFacture)
        {
            List<JournalVenteCompteAnalytique> liste = new List<JournalVenteCompteAnalytique>();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_JV_HSTCMPANAL;
                comand.CommandTimeout = Constants.TimeOut;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                // comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters["inIdFacture"].Value = IdFacture;
                // comand.Parameters["inId"].Value = id;
                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    JournalVenteCompteAnalytique jv = new JournalVenteCompteAnalytique();
                    jv.ID_Client = reader.GetInt32(0);
                    jv.IDCompteAnal = reader.GetInt32(1);
                    if (!reader.IsDBNull(2))
                        jv.NumeroCmptAnal = reader.GetString(2);
                    if (!reader.IsDBNull(3))
                        jv.Datefacture = reader.GetDateTime(3);
                    if (!reader.IsDBNull(4))
                        jv.Numerofacture = reader.GetString(4);
                    if (!reader.IsDBNull(5))
                        jv.MontantFacture = reader.GetDouble(5);
                    if (!reader.IsDBNull(6))
                        jv.LibelleMontant = reader.GetString(6);
                    if (!reader.IsDBNull(7))
                        jv.ID_Datejournal = reader.GetInt32(7);

                    liste.Add(jv);
                }
            }
            return liste;
        }



        public DataTable JournalVenteGetListe(int idSite, DateTime dateDebut, DateTime dateFin)
        {
            DataTable tableListe = new DataTable();

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                con.Open();

                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_JournalVente_ListFactures";
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                comand.Parameters.Add("inDateDebut", MySqlDbType.DateTime);
                comand.Parameters.Add("inDatefin", MySqlDbType.DateTime);
                comand.Parameters["inIdSite"].Value = idSite;
                comand.Parameters["inDateDebut"].Value = dateDebut;
                comand.Parameters["inDatefin"].Value = dateFin;

                try
                {
                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tableListe);
                    return tableListe;
                }
                catch (Exception ex)
                {
                    con.Close();
                    con.Dispose();
                    throw new Exception(ex.Message);



                }
            }


        }

        public DataTable JournalVente_periodeMois(string  Annee)
        {
            DataTable tableListe = new DataTable();

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                con.Open();

                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_JlVente_ListeMoisParAnnee";
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inAnnee", MySqlDbType.String);
                comand.Parameters["inAnnee"].Value = Annee;
                

                try
                {
                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tableListe);
                    return tableListe;
                }
                catch (Exception ex)
                {
                    con.Close();
                    con.Dispose();
                    throw new Exception(ex.Message);



                }
            }


        }


        public bool JournalVenteTesteDelete(int idDate)
        {
            bool valReturnteste = false;

            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                con.Open();

                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_JlVente_Teste_delete";
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters.Add("valteste", MySqlDbType.Bit);
                comand.Parameters["valteste"].Direction = ParameterDirection.InputOutput;
               
                comand.Parameters["inId"].Value = idDate;
                comand.Parameters["valteste"].Value =false;
              
                try
                {
                    comand.ExecuteNonQuery();
                    valReturnteste = comand.Parameters["valteste"].Value != DBNull.Value ? Convert.ToBoolean(comand.Parameters["valteste"].Value) : false;
                    return valReturnteste;
                }
                catch (Exception ex)
                {
                    con.Close();
                    con.Dispose();
                    throw new Exception(ex.Message);



                }
            }


        }


        public bool DateJournalInsert(ref int idDate,int nbreFacture, int idSite, DateTime dateDebut, DateTime dateFin)
        {
            int idreturn = -1;
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                MySqlCommand comandd = new MySqlCommand();
                comandd.CommandType = CommandType.StoredProcedure;
                comandd.CommandText = Constants.PS_JV_DATE_ADD;
                comandd.CommandTimeout = Constants.TimeOut;
                con.Open();
                comandd.Parameters.Add("idId", MySqlDbType.Int32);
                comandd.Parameters["idId"].Direction = ParameterDirection.Output;
                comandd.Parameters.Add("inIdSite", MySqlDbType.Int32);
                comandd.Parameters.Add("inDatedebut", MySqlDbType.DateTime);
                comandd.Parameters.Add("inDateFin", MySqlDbType.DateTime);
                comandd.Parameters.Add("inNbre", MySqlDbType.Int32);

                comandd.Parameters["idId"].Value = idreturn;
                comandd.Parameters["inIdSite"].Value = idSite;
                comandd.Parameters["inDatedebut"].Value = dateDebut;
                comandd.Parameters["inDateFin"].Value = dateFin;
                comandd.Parameters["inNbre"].Value = nbreFacture;
                comandd.Connection = con;
               
                comandd.ExecuteNonQuery();
                idDate = Int32.Parse(comandd.Parameters["idId"].Value.ToString());

                return true;
            }

        }


        public bool DateJournalUpdate(int idDate,int nombrefActure)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();

                MySqlCommand comand= new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_JlVente_DateUpdate";
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("idId", MySqlDbType.Int32);
                comand.Parameters.Add("inNbre", MySqlDbType.Int32);
                comand.Parameters["idId"].Value = idDate;
                comand.Parameters["inNbre"].Value = nombrefActure;
                comand.Connection = con;
                comand.ExecuteNonQuery();

                return true;
            }
        }


        public bool GenerationJournalVentesGlobal(int idDate, DataRow row,bool isnextDay)
        {
            bool values = false;
          //  int nbreTraite = 0;
            // DataTable tableListe = new DataTable();
          //  MySqlTransaction trans = null;
           // int idreturn = 0;
           // string lignecvourante = null;
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
               
                con.Open();
             
                try
                {

                   

                    MySqlCommand comands = new MySqlCommand();
                    comands.Connection = con;
                    comands.CommandType = CommandType.StoredProcedure;
                    comands.CommandText = "PS_JournalVente_Generate";
                    comands.CommandTimeout = Constants.TimeOut;

                    comands.Parameters.Add("inId", MySqlDbType.Int32);
                    comands.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                    comands.Parameters.Add("inIdClient", MySqlDbType.Int32);
                    comands.Parameters.Add("inModeGeneration", MySqlDbType.Int32);
                    comands.Parameters.Add("inIsNextMonth", MySqlDbType.Bit);

                    comands.Parameters["inId"].Value = idDate;
                    comands.Parameters["inIdFacture"].Value = row["ID"];
                     comands.Parameters["inIdClient"].Value = row["ID_Client"];
                    comands.Parameters["inModeGeneration"].Value = row["ID_Exoneration"];
                    comands.Parameters["inIsNextMonth"].Value = isnextDay;

                       // lignecvourante = string.Format(" {0};{1}", row["ID"], row["ID_Client"]);
                        comands.ExecuteNonQuery();
                       // nbreTraite++;
                  
                    values = true;
                }
                catch (Exception ex)
                {
                    values = false;
                    // logUserActions(string.Format("<-- ;55;Ui Edition facture; Erreur lors du changement statut facture <<Non valide>>  facture par : {0} ;{1} ", UserConnected.Nom, ex.Message), "");

                    con.Close();
                    con.Dispose();

                    throw new Exception(ex.Message );
                }

               
            }
            return values;
        }
        public bool InsertJournalVentes(int id, Int64 idFacture, int idClient)
        {
            DataTable tableListe = new DataTable();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_JournalVente_Generate";
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters.Add("inIdFacture", MySqlDbType.Int64);
                comand.Parameters.Add("inIdClient", MySqlDbType.Int32);
                comand.Parameters["inId"].Value = id;
                comand.Parameters["inIdFacture"].Value = idFacture;
                comand.Parameters["inIdClient"].Value = idClient;
                comand.ExecuteNonQuery();
                return true;

            }
        }

        public DataTable GetJournalVentesListeByPeriode(int idSite, DateTime dateDebut, DateTime dateFin)
        {
            DataTable tableListe = new DataTable();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_JournalVente_ListFactures";
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                comand.Parameters.Add("inDateDebut", MySqlDbType.DateTime);
                comand.Parameters.Add("inDatefin", MySqlDbType.DateTime);
                comand.Parameters["inIdSite"].Value = idSite;
                comand.Parameters["inDateDebut"].Value = dateDebut;
                comand.Parameters["inDatefin"].Value = dateFin;

                MySqlDataAdapter data = new MySqlDataAdapter(comand);
                data.Fill(tableListe);

                return tableListe;

            }
        }


        public DataTable GetJournalVentesDates_Periodes(int idSite)
        {
            DataTable tableListe = new DataTable();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_JV_DATE_PERIODE;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                comand.Parameters["inIdSite"].Value = idSite;

                MySqlDataAdapter data = new MySqlDataAdapter(comand);
                data.Fill(tableListe);

                return tableListe;

            }
        }


        public DataTable GetFactyreByIdjv(Int64 IdFacture)
        {
            DataTable tableListe = new DataTable();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_FACTURE_SELECTBYJV";
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("Idfacture", MySqlDbType.Int64);
                comand.Parameters["Idfacture"].Value = IdFacture;

                MySqlDataAdapter data = new MySqlDataAdapter(comand);
                data.Fill(tableListe);

                return tableListe;

            }
        }

        public List<JournalVentesDates> GetJournalVentesDates_List(int idSite, DateTime periode)
        {
            List<JournalVentesDates> liste = new List<JournalVentesDates>();
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                con.Open();
                MySqlCommand comand = new MySqlCommand();
                comand.Connection = con;
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_JV_DATE_SELECT;
                comand.CommandTimeout = Constants.TimeOut;
                comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                comand.Parameters.Add("inDateperiode", MySqlDbType.DateTime);
                comand.Parameters["inIdSite"].Value = idSite;
                comand.Parameters["inDateperiode"].Value = periode;

                MySqlDataReader reader = comand.ExecuteReader();
                JournalVentesDates jv = null;
                while (reader.Read())
                {
                    jv = new JournalVentesDates();
                    jv.ID = reader.GetInt32(0);
                    jv.DateDebut = reader.GetDateTime(1);
                    jv.DateFin = reader.GetDateTime(2);
                    jv.NumeroJournal = reader.GetString(4);
                    jv.IdSite = reader.GetInt32(5);
                    if (!reader.IsDBNull(6))
                    jv.NbreFactures = reader.GetInt32(6);
                    liste.Add(jv);
                }
            }
            return liste;
        }


      


        public bool JournalVenteDatesADD(ref int idreturn, JournalVentesDates jv)
        {
            bool isValues = false;
            idreturn = 0;
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_JV_DATE_ADD;
                comand.CommandTimeout = Constants.TimeOut;

                comand.Parameters.Add("idId", MySqlDbType.Int32);
                comand.Parameters["idId"].Direction = ParameterDirection.Output;
                comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                comand.Parameters.Add("inDatedebut", MySqlDbType.DateTime);
                comand.Parameters.Add("inDateFin", MySqlDbType.DateTime);

                comand.Parameters["idId"].Value = idreturn;
                comand.Parameters["inIdSite"].Value = jv.IdSite;
                comand.Parameters["inDatedebut"].Value = jv.DateDebut;
                comand.Parameters["inDateFin"].Value = jv.DateFin;
                comand.Connection = con;
                con.Open();
                comand.ExecuteNonQuery();
                idreturn = Int32.Parse(comand.Parameters["idId"].Value.ToString());
                isValues = true;

                return isValues;
            }
        }


        public bool JournalVenteDatesDELETE(int id)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = Constants.PS_JV_DATE_DELETE;
                comand.CommandTimeout = Constants.TimeOut;

                comand.Parameters.Add("inId", MySqlDbType.Int32);
                comand.Parameters["inId"].Value = id;

                comand.Connection = con;
                con.Open();
                comand.ExecuteNonQuery();
                return true;
            }
        }


        /// <summary>
        /// mise jour numero avoir
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idLigne"></param>
        /// <param name="numero"></param>
        /// <returns></returns>

        public bool JournalVenteHistoriqueUpdateNote(int idDate, int idLigne,string numero)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_journalVente_updateAvoir";
                comand.CommandTimeout = Constants.TimeOut;

                comand.Parameters.Add("inNumeroPiece", MySqlDbType.VarChar,255);
                comand.Parameters.Add("inIdligne", MySqlDbType.Int32);
                comand.Parameters.Add("inIdDate", MySqlDbType.Int32);
                comand.Parameters["inNumeroPiece"].Value = numero;
                comand.Parameters["inIdligne"].Value = idLigne;
                comand.Parameters["inIdDate"].Value = idDate;

                comand.Connection = con;
                con.Open();
                comand.ExecuteNonQuery();
                return true;
            }
        }
        #endregion

        #region Management DataBase

        public DataTable GetListFactures(int idSite)
        {
            DataTable tblComputer = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
                {
                    con.Open();
                    MySqlCommand comand = new MySqlCommand();
                    comand.Connection = con;
                    comand.CommandType = CommandType.StoredProcedure;
                    comand.CommandText = "PS_W_UPDATE_DATABASES_DATAS";
                    comand.CommandTimeout = Constants.TimeOut;
                    comand.Parameters.Add("inIdSite", MySqlDbType.Int32);
                    //comand.Parameters.Add("inDateFrom", MySqlDbType.DateTime);
                    //comand.Parameters.Add("inDateTo", MySqlDbType.DateTime);
                    comand.Parameters["inIdSite"].Value = idSite;
                  //  comand.Parameters["inDateFrom"].Value = periodeDebut;
                      //comand.Parameters["inDateTo"].Value = periodeFin;
                    MySqlDataAdapter data = new MySqlDataAdapter(comand);
                    data.Fill(tblComputer);

                }

                return tblComputer;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool UpdateDatas_from_database(Int64 idFacture, Int32 idSite)
        {
            using (MySqlConnection con = new MySqlConnection(FDataBase.GetConnection(ConnectionString)))
            {
                MySqlCommand comand = new MySqlCommand();
                comand.CommandType = CommandType.StoredProcedure;
                comand.CommandText = "PS_W_UPDATE_DATABASES_DATAS_ADD";
                comand.CommandTimeout = Constants.TimeOut;

                comand.Parameters.Add("inidfacture", MySqlDbType.Int64);
                comand.Parameters.Add("inidSite", MySqlDbType.Int32);
             
                comand.Parameters["inidfacture"].Value = idFacture;
                comand.Parameters["inidSite"].Value = idSite;
              

                comand.Connection = con;
                con.Open();
                comand.ExecuteNonQuery();
                return true;

            }
          
        }
        
        
        #endregion


    }



    public class DALException : Exception
    {
        public DALException() { }
        public DALException(string message) : base(message) { }
        public DALException(string message, Exception inner) : base(message, inner) { }
    }

    public class DALValidationException : DALException
    {
        public DALValidationException() { }
        public DALValidationException(string message) : base(message) { }
        public DALValidationException(string message, Exception inner) : base(message, inner) { }
    }
}
