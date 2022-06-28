using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace FACTURATION_DAL
{
    internal  class FDataBase
    {
       
        //public static FDataBase() { }

        public static MySqlConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection();
            try
            {
                string DatabaseConnectionString = ConfigurationManager.ConnectionStrings["mysqlReport"].ConnectionString;
                connection.ConnectionString = DatabaseConnectionString;
                connection.Open();

                return connection;
            }
            catch (MySqlException  ex)
            {
                return null;
                throw new DALException(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new DALException(ex.ToString());
            }

        }

        public static string  GetConnection(string chaine)
        {
            //string valreturn = string.Empty;
         
            //string[] listevaleur = chaine.Split(new char[] { ';' });
            ////server=localhost
            //string DatabaseConnectionString = ConfigurationManager.ConnectionStrings["mysqlReport"].ConnectionString;
            //string[] databaseValues=DatabaseConnectionString.Split(new char[] { ';' });

            //string server = databaseValues[0];
            //int ide = server.IndexOf("=");
            //string sub = server.Substring(0,server.IndexOf("=")+1 );
            //if (!listevaleur[0].Contains("="))
            //{
            //    databaseValues[0] = databaseValues[0].Substring(0, databaseValues[0].IndexOf("=") + 1) + listevaleur[0];
            //    databaseValues[1] = databaseValues[1].Substring(0, databaseValues[1].IndexOf("=") + 1) + listevaleur[2];
            //    databaseValues[2] = databaseValues[2].Substring(0, databaseValues[2].IndexOf("=") + 1) + listevaleur[4];
            //    databaseValues[3] = databaseValues[3].Substring(0, databaseValues[3].IndexOf("=") + 1) + listevaleur[3];

            //    StringBuilder builder = new StringBuilder();
            //    builder.Append(databaseValues[0]); builder.Append(";");
            //    builder.Append(databaseValues[1]); builder.Append(";");
            //    builder.Append(databaseValues[2]); builder.Append(";");
            //    builder.Append(databaseValues[3]); builder.Append(";");
            //    builder.Append(databaseValues[4]);
            //    valreturn = builder.ToString();
            //}
            //else
            //    valreturn = chaine;
            


           // return valreturn;
            return chaine;
        }
    }
}
