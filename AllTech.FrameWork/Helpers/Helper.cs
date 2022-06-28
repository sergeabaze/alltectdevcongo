using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FACTURATION_DAL;
using AllTech.FrameWork.Model;
using MySql.Data.MySqlClient;
using System.Data;


namespace AllTech.FrameWork.Helpers
{
    public  class Helper
    {
        static IFacturation DAL = null;

        public static bool IsConnected(ref List<Databaseinfo> databases )
        {
           

            //try
            //{
                //"localhost;3306;root;sysfact;serge_it"
                databases = new List<Databaseinfo>();
             // string ChaineConnection=  DataProviderObject.GetStringConnection;
             // string[] tablist = ChaineConnection.Split(new char[]{';'});

                string myConnectionString = string.Format("SERVER={0};UID='{1}'; PASSWORD='{2}';", Global.GlobalDatas.dataBasparameter.Nomserveur, Global.GlobalDatas.dataBasparameter.Utilisateur, Global.GlobalDatas.dataBasparameter.Motpasse);
                //Global.GlobalDatas.dataBasparameter.Motpasse
                  //string.Format("SERVER={0};UID='{1}'; PASSWORD='{2}';", tablist[0], tablist[2], tablist[4]);

              MySqlConnection con = new MySqlConnection(myConnectionString);
              MySqlCommand cmd = con.CreateCommand();
              cmd.CommandText = "show databases";

              con.Open();
              MySqlDataReader reader = cmd.ExecuteReader();
             
              while (reader.Read())
              {
                  string row = "";
                  for (int i = 0; i < reader.FieldCount; i++)
                  {
                      Databaseinfo db = new Databaseinfo();
                      if (reader.GetValue(i).ToString().Contains("sysfact"))
                      {
                          db.ID = i + 1;
                          db.DatabaseNAme = reader.GetValue(i).ToString();
                          if (Global.GlobalDatas.dataBasparameter.NomBd.Equals(db.DatabaseNAme))
                              db.ISfefault = true;
                          else db.ISfefault = false;
                          db.DatabaseNAme.ToLower();
                          databases.Add(db);
                      }
                  }
                     // Console.WriteLine(reader.GetValue(i).ToString());
                  // row += reader.GetValue(i).ToString();
                  // listBox1.Items.Add(row);
              }

              //  DAL = (Facturation)DataProviderObject.FacturationDal;
               // return DAL.ListeLangue();
              if (reader.HasRows)
                  return true;

              return false;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }

        public static List<Databaseinfo> GetDatabases()
        {
           // string ChaineConnection = DataProviderObject.GetStringConnection;
          //  string[] tablist = ChaineConnection.Split(new char[] { ';' });

            string myConnectionString = string.Format("SERVER={0};UID='{1}'; PASSWORD='{2}';", Global.GlobalDatas.dataBasparameter.Nomserveur, Global.GlobalDatas.dataBasparameter.Utilisateur, Global.GlobalDatas.dataBasparameter.Motpasse);
            List<Databaseinfo> databases = new List<Databaseinfo>();
          
            MySqlConnection con = new MySqlConnection(myConnectionString);
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "show databases";

            try
            {
                  con.Open();
              MySqlDataReader reader = cmd.ExecuteReader();

              while (reader.Read())
              {
                  string row = "";
                  for (int i = 0; i < reader.FieldCount; i++)
                  {
                      Databaseinfo db = new Databaseinfo();
                      if (reader.GetValue(i).ToString().Contains("sysfact"))
                      {
                          db.ID = i + 1;
                          db.DatabaseNAme = reader.GetValue(i).ToString().ToUpper();
                          if (Global.GlobalDatas.dataBasparameter.NomBd.Equals(db.DatabaseNAme))
                              db.ISfefault = true;
                          else db.ISfefault = false;

                          List<Tables> tables = new List<Tables>();
                          tables.Add(new Tables { Ischeckd = false, tablename = "t_client" });
                          tables.Add(new Tables { Ischeckd=false, tablename="t_factures" });
                          db.TablesDb = loadTables(db.DatabaseNAme.ToLower());
                          databases.Add(db);
                      }
                  }
              }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return databases;
        }


        public static DataTable executeScripte2(string scriptSQL)
        {
            DataTable dt = null;
             string ChaineConnection = DataProviderObject.GetStringConnection;
            string[] tablist = ChaineConnection.Split(new char[] { ';' });

            string myConnectionString = Global.GlobalDatas.ConnectionString; 
                //string.Format("SERVER={0};UID='{1}'; PASSWORD='{2}';", Global.GlobalDatas.dataBasparameter.Nomserveur, Global.GlobalDatas.dataBasparameter.Utilisateur, Global.GlobalDatas.dataBasparameter.Motpasse);
                //string.Format("SERVER={0};UID='{1}'; PASSWORD='{2}';database='{3}'", tablist[0], tablist[2], tablist[4], tablist[3]);

            using (MySqlConnection conn = new MySqlConnection(myConnectionString))
            {
                MySqlScript script = new MySqlScript(conn);
                script.Query = scriptSQL;
                int i = script.Execute();
                 dt = new DataTable();
                dt.Columns.Add("Result");
                dt.Rows.Add(i + " statement(s) executed.");
            }

            return dt;
        }


        public static DataTable executeScripte1(ref string newSql , string sql)
        {
            DataTable dt = null;
            string ChaineConnection = DataProviderObject.GetStringConnection;
            string[] tablist = ChaineConnection.Split(new char[] { ';' });

            string myConnectionString = Global.GlobalDatas.ConnectionString;
                //string.Format("SERVER={0};UID='{1}'; PASSWORD='{2}';database='{3}'", tablist[0], tablist[2], tablist[4], tablist[3]);

            using (MySqlConnection conn = new MySqlConnection(myConnectionString))
            {
                if (sql.StartsWith("select", StringComparison.OrdinalIgnoreCase) || sql.StartsWith("show", StringComparison.OrdinalIgnoreCase))
                {
                    if (sql.StartsWith("select", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!sql.ToLower().Contains(" limit "))
                        {
                            if (sql.EndsWith(";"))
                            {
                                sql = sql.Remove(sql.Length - 1);

                            }
                            sql += " LIMIT 0,3000;";
                            newSql = sql;
                           // textBox1.Refresh();
                        }
                    }

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        dt = QueryExpress.GetTable(cmd, sql);
                       // BindData();
                    }
                }
                else
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        newSql = null;
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.CommandText = sql;
                        int i = cmd.ExecuteNonQuery();
                        dt = new DataTable();
                        dt.Columns.Add("Results");
                        dt.Rows.Add(i + " row(s) affected by the last command.");
                       // BindData();
                    }
                }
            }

            return dt;
        }


        public static string GetconnectionString()
        {
            string ChaineConnection = DataProviderObject.GetStringConnection;
            string[] tablist = ChaineConnection.Split(new char[] { ';' });
            string myConnectionString = Global.GlobalDatas.ConnectionString;
                //string.Format("SERVER={0};UID='{1}'; PASSWORD='{2}';database='{3}'", tablist[0], tablist[2], tablist[4], tablist[3]);
            return myConnectionString;

        }

       static List<Tables> loadTables(string database)
        {
            List<Tables> liste = new List<Tables>();
            string ChaineConnection = DataProviderObject.GetStringConnection;
            string[] tablist = ChaineConnection.Split(new char[] { ';' });

            string myConnectionString = Global.GlobalDatas.ConnectionString; ;
                //string.Format("SERVER={0};UID='{1}'; PASSWORD='{2}';database='{3}'", tablist[0], tablist[2], tablist[4], tablist[3]);

            using (MySqlConnection conn = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = "SELECT DATABASE();";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    object db =cmd.ExecuteScalar();
                 //  object db = QueryExpress.ExecuteScalarStr(cmd, "SELECT DATABASE();");

                    if (string.IsNullOrEmpty(db.ToString()))
                    {
                       // labe.Text = "Database: No database in use/selected.";
                        return null;
                    }
                    DataTable dtb = new DataTable();
                   // lbDb.Text = "Database: " + db;
                    using (MySqlCommand cmdq = new MySqlCommand())
                    {
                        cmdq.Connection = conn;
                        cmdq.CommandText = string.Format("SHOW FULL TABLES FROM {0} WHERE `Table_type` = 'BASE TABLE';", database);
                        cmdq.CommandType = CommandType.Text;
                        MySqlDataAdapter dt = new MySqlDataAdapter(cmdq);
                        dt.Fill(dtb);

                        if (dtb.Rows.Count > 0)
                        {
                            for (int d = 0; d <= dtb.Rows.Count-1; d++)
                            {
                                Tables tb = new Tables();
                               
                           
                                string table = dtb.Rows[d][0]+"";
                                string substrin = string.Empty;

                                if (table.Contains("t_archive"))
                                {
                                    tb.Ischeckd = false;
                                }
                                else tb.Ischeckd = true;

                                tb.tablename= table;//dtb.Rows[d][""]
                                tb.sql = string.Format("SELECT * FROM `{0}`;", table);
                                liste.Add(tb);
                            }
                        }
                        
                    }
                   // dt = QueryExpress.GetTable(cmd, "SHOW FULL TABLES WHERE `Table_type` = 'BASE TABLE';");

                    conn.Close();
                }
            }
            return liste;
        }

       public static void Export_DB(string targetDB,List<Tables> tableToExport)
       {
           try
           {
               string ChaineConnection = DataProviderObject.GetStringConnection;
               string[] tablist = ChaineConnection.Split(new char[] { ';' });
               string myConnectionString = Global.GlobalDatas.ConnectionString;
                   //string.Format("SERVER={0};UID='{1}'; PASSWORD='{2}';database='{3}'", tablist[0], tablist[2], tablist[4], tablist[3]);
             //  BackUpHelpers.Export(myConnectionString, targetDB);

               Dictionary<string, string> dic = new Dictionary<string, string>();

               foreach (Tables r in tableToExport)
               {
                   if (r.Ischeckd)
                   {
                       string tableName = r.tablename + "";
                       string sql = r.sql + "";

                       dic[tableName] = sql;
                   }
               }
               if (dic.Count == 0)
                   throw new Exception("Pas de tables sélectionner pour l'export");

               using (MySqlConnection conn = new MySqlConnection(myConnectionString))
               {
                   using (MySqlCommand cmd = new MySqlCommand())
                   {
                       using (MySqlBackup mb = new MySqlBackup(cmd))
                       {
                           cmd.Connection = conn;
                           conn.Open();
                           mb.ExportInfo.AddCreateDatabase = true;
                           mb.ExportInfo.ExportTableStructure = true;
                           mb.ExportInfo.ExportRows = true;
                           mb.ExportInfo.TablesToBeExportedDic = dic;
                           mb.ExportToFile(targetDB);
                       }
                   }
               }

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }


       public static bool ImportDB(string dbTarget,string pathDB)
       {
           string ChaineConnection = DataProviderObject.GetStringConnection;
           string[] tablist = ChaineConnection.Split(new char[] { ';' });
           string myConnectionString = string.Format("SERVER={0};UID='{1}'; PASSWORD='{2}';", Global.GlobalDatas.dataBasparameter.Nomserveur, Global.GlobalDatas.dataBasparameter.Utilisateur, Global.GlobalDatas.dataBasparameter.Motpasse);
               //string.Format("SERVER={0};UID='{1}'; PASSWORD='{2}';", tablist[0], tablist[2], tablist[4]);
           List<Databaseinfo> databases = new List<Databaseinfo>();

           try
           {
               using (MySqlConnection conn = new MySqlConnection(myConnectionString))
               {
                   using (MySqlCommand cmd = new MySqlCommand())
                   {
                       using (MySqlBackup mb = new MySqlBackup(cmd))
                       {
                           cmd.Connection = conn;
                           conn.Open();

                       
                           mb.ImportInfo.IgnoreSqlError = true;
                           mb.ImportInfo.TargetDatabase = dbTarget;
                           mb.ImportFromFile(pathDB);
                           mb.ImportInfo.ErrorLogFile = Global.Utils.GetErrorImportFile();
                           conn.Close();
                           return true;
                       }
                   }
               }
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }
    }





    public class Databaseinfo
    {
        public int ID { get; set; }
        public string DatabaseNAme { get; set; }
        public bool ISfefault { get; set; }
        private List<Tables> tablesDb = new List<Tables>();

        public List<Tables> TablesDb
        {
            get { return tablesDb; }
            set { tablesDb = value; }
        }
    }

    public class Tables
    {
        public bool Ischeckd { get; set; }
        public string tablename { get; set; }
        public string sql { get; set; }
    }
}
