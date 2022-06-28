using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FACTURATION_DAL.Model
{
   public  class Utils
    {

        public static bool CreateDirectory(string cheminDossier)
        {
            bool result = false;
            if (!Directory.Exists(cheminDossier))
            {
                try
                {
                    Directory.CreateDirectory(cheminDossier);
                    result = true;
                }
                catch (IOException en)
                {
                    throw new Exception(" File Failed" + en.ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception("Create File Failed" + ex.ToString());
                }

            }
            else result = true;

            return result;
        }

        public static void logConnection(string Message)
        {

            string paths = AppDomain.CurrentDomain.BaseDirectory;
            string newPath = string.Empty;

            int chemin = paths.IndexOf("bin");
            if (chemin == -1)
            {
                newPath = paths;
            }
            else
            {
                newPath = paths.Substring(0, chemin);
            }

            newPath = newPath + "\\LOGS";

            try
            {
                if (CreateDirectory(newPath))
                {
                    if (!File.Exists(newPath + "\\" + "Connection.data"))
                    {
                        //StreamWriter sw = File.CreateText(newPath + "\\" + "Connection.data");
                        //sw.WriteLine(Message);
                        //sw.WriteLine(" {0} {1}: ", DateTime.Now.ToShortTimeString(), DateTime.Now.ToLongDateString());
                        //sw.WriteLine("");
                        //sw.Close();
                        //sw.Dispose();
                    }
                    else
                    {
                        StreamWriter sw = File.AppendText(newPath + "\\" + "Connection.data");
                        sw.Write( Message);
                        sw.WriteLine("  {0} {1}: ", DateTime.Now, DateTime.Now.ToLongDateString());
                        sw.WriteLine();
                        sw.Close();
                        sw.Dispose();
                    }
                }
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message);
            }

        }


        public static void c(string Message)
        {

            string paths = AppDomain.CurrentDomain.BaseDirectory;
            string newPath = string.Empty;

            int chemin = paths.IndexOf("bin");
            if (chemin == -1)
            {
                newPath = paths;
            }
            else
            {
                newPath = paths.Substring(0, chemin);
            }

            newPath = newPath + "\\LOGS";

            try
            {
                if (CreateDirectory(newPath))
                {
                    if (!File.Exists(newPath + "\\" + "logUsers.data"))
                    {
                        //StreamWriter sw = File.CreateText(newPath + "\\" + "Connection.data");
                        //sw.WriteLine(Message);
                        //sw.WriteLine(" {0} {1}: ", DateTime.Now.ToShortTimeString(), DateTime.Now.ToLongDateString());
                        //sw.WriteLine("");
                        //sw.Close();
                        //sw.Dispose();
                    }
                    else
                    {
                        StreamWriter sw = File.AppendText(newPath + "\\" + "logUsers.data");
                        sw.Write(Message);
                        sw.WriteLine("  {0} {1}: ", DateTime.Now, DateTime.Now.ToLongDateString());
                        sw.WriteLine();
                        sw.Close();
                        sw.Dispose();
                    }
                }
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message);
            }

        }
    }
}
