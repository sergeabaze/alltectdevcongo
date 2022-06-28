using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace AllTech.MysqlbackUp
{
   public  class BackUpHelpers
    {

       public static void Export(string connection, string targerDb)
       {
           using (MySqlConnection conn = new MySqlConnection(connection))
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
                       mb.ExportToFile(targerDb);
                   }
               }
           }
       }
    }
}
