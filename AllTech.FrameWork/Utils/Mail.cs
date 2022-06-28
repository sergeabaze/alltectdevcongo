using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AllTech.FrameWork.Utils
{
   public  class Mail
    {
      

       public static bool SendMail(string strFrom, string strUserPwd, string strTo,
                            string strSMTP_Server, string strSMTP_Port, string strAttachFile, string strNature)
       {
           bool bRetVal = true;
           MailMessage oMailMsg = new MailMessage();
           SmtpClient smtpClient = new SmtpClient();
           
           //Définition de l'objet et du corps du message            
           if (strNature.Equals("Transmission"))
           {
               oMailMsg.Subject = string.Format("Transmission d'un fichier de commandes à SAGE 1000 le {0}",
                   DateTime.Now.ToString("G"));
               oMailMsg.Body = string.Format("Bonjour,\n\n");
               oMailMsg.Body += string.Format("Le fichier '{0}' a été mis à la disposition de l'automate de Sage 1000 à {1}.\n\n",
                   strAttachFile.Substring(strAttachFile.LastIndexOf(@"\") + 1), DateTime.Now.ToString("T"));
               oMailMsg.Body += string.Format("Cordialement.\n\n");
               oMailMsg.Body += string.Format("Interface Gelodia - Sage 1000");
           }
           else if (strNature.Equals("Importation"))
           {
               oMailMsg.Subject = string.Format("Importation d'un fichier de commandes dans SAGE 1000 le {0}",
                   DateTime.Now.ToString("G"));
               oMailMsg.Body = string.Format("Bonjour,\n\n");
               oMailMsg.Body += string.Format("Les commandes contenues dans le fichier ci-joint ont été impotées dans Sage 1000 à {0}.\n\n",
                   DateTime.Now.ToString("T"));
               oMailMsg.Body += string.Format("Cordialement.\n\n");
               oMailMsg.Body += string.Format("Interface Gelodia - Sage 1000");
               Attachment at1 = new Attachment("");
               
              // oMailMsg.Attachments = at1;

               // création de la pièce jointe                
               Attachment AttachFile = new Attachment(strAttachFile); // chemin de la pièce jointe
               // ajout de la pièce jointe au mail
               oMailMsg.Attachments.Add(AttachFile);
           }

           //Définition de l'émetteur et du destinataire
           oMailMsg.From = new MailAddress(strFrom);
           string[] Diffusion = strTo.Split(new string[] { ";" }, System.StringSplitOptions.RemoveEmptyEntries);
           for (int i = 0; i < Diffusion.GetLength(0); i++)
           {
               oMailMsg.To.Add(Diffusion[i]);
           }
           oMailMsg.Priority = MailPriority.Normal;


           // définition du serveur smtp
           smtpClient.Host = strSMTP_Server;
           try
           {
               if (int.Parse(strSMTP_Port) > 0) smtpClient.Port = int.Parse(strSMTP_Port);
           }
           catch { }

           // définition des login et pwd si smtp sécurisé
           if (!strUserPwd.Equals(""))
           {
               smtpClient.Credentials = new NetworkCredential(strFrom.Substring(0, strFrom.IndexOf("@")), strUserPwd);
           }

           // Envoi du mail
           try
           {
               smtpClient.Send(oMailMsg);
           }
           catch
           {
               bRetVal = false;
           }
           return bRetVal;
       }
    }

   public static  class ClsSentMail
   {
       [DllImport("wininet.dll")]
       private extern static bool InternetGetConnectedState(out int conn, int val);
    

       /// <summary>
       /// <param name="to">Message to address</param>
       /// <param name="body">Text of message to send</param>
       /// <param name="subject">Subject line of message</param>
       /// <param name="fromAddress">Message from address</param>
       /// <param name="fromDisplay">Display name for "message from address"</param>
       /// <param name="credentialUser">User whose credentials are used for message send</param>
       /// <param name="credentialPassword">User password used for message send</param>
       /// <param name="attachments">Optional attachments for message</param>

       /// </summary>
       public static void SenMail(string to,
                         string body,
                         string subject,
                         string fromAddress,
                         string fromDisplay,
                         string credentialUser,
                         string credentialPassword,
                         string host,
                         string mailCc,string port,
                         params Attachment[] attachments
                       )
       {
           //string host = Global.GlobalDatas.dataBasparameter.Smtp ;
          // string host = "smtp.gmail.com";
           try
           {
               int Out=0;
               if (InternetGetConnectedState(out Out, 0) == true)
               {
                   //body = UpgradeEmailFormat(body);
                   string[] tabMailCC = null;

                   string[] tableMailTo = to.Split(new char[] { ';' });

                   MailMessage mail = new MailMessage();
                   mail.Body = body;
                   mail.IsBodyHtml = true;
                   // mail.To.Add(new MailAddress(to));
                   foreach (string adressto in tableMailTo)
                   {
                       if (!string.IsNullOrEmpty(adressto))
                       {
                           if (VerifUrl(adressto))
                               mail.To.Add(new MailAddress(adressto));
                           else throw new Exception("Addresse mail incorrecte [" + adressto + "]");
                       }
                   }
                   //for (int i = 0; i < fromAddress.Length; i++)
                   mail.From = new MailAddress(fromAddress);
                   mail.Subject = subject;
                   if (!string.IsNullOrEmpty(mailCc))
                   {
                       if (mailCc.Contains(";"))
                       {
                           tabMailCC = mailCc.Split(new char[] { ';' });
                           for (int i = 0; i < tabMailCC.Length; i++)
                           {
                               if (!string.IsNullOrEmpty(tabMailCC[i]))
                               {
                                   if (VerifUrl(tabMailCC[i]))
                                       mail.CC.Add(tabMailCC[i]);
                                   else throw new Exception("Addresse mail incorrecte [" + tabMailCC[i] + "]");
                               }
                           }
                       }
                       else if (VerifUrl(mailCc))
                           mail.CC.Add(mailCc);
                   }
                   mail.SubjectEncoding = Encoding.UTF8;
                   mail.Priority = MailPriority.Normal;
                   foreach (Attachment ma in attachments)
                       mail.Attachments.Add(ma);
                   // SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                   SmtpClient smtp = new SmtpClient();
                   if (!string.IsNullOrEmpty(credentialUser))
                   smtp.Credentials = new System.Net.NetworkCredential(credentialUser, credentialPassword);
                   smtp.Host = host;
                   smtp.Port = int.Parse(port);// 587;
                   smtp.EnableSsl = true;
                   smtp.Send(mail);
               }
               else
               {

                   throw new Exception("Pas de connection internet");
               }

           }
           catch (Exception ex)
           {
               throw new Exception (ex.Message);
               //Console.WriteLine(ex.Message);
               //StringBuilder sb = new StringBuilder(1024);
               //sb.Append("\nTo:" + to);
               //sb.Append("\nbody:" + body);
               //sb.Append("\nsubject:" + subject);
               //sb.Append("\nfromAddress:" + fromAddress);
               //sb.Append("\nfromDisplay:" + fromDisplay);
               //sb.Append("\ncredentialUser:" + credentialUser);
               //sb.Append("\ncredentialPasswordto:" + credentialPassword);
               //sb.Append("\nHosting:" + host);
               // ErrorLog(sb.ToString(), ex.ToString(), ErrorLogCause.EmailSystem);
               //Console.WriteLine(sb.ToString());
           }
       }

       static bool VerifUrl(string url)
       {
           string pattern = null;
           bool values = false;
           string urlStr = "http://net-informations.com";
           pattern = "http(s)?://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?";
           if (Regex.IsMatch(urlStr, pattern))
           {
               values = true;
               // MessageBox.Show("Url exist in the string ");
               //Console.WriteLine("Url exist in the string !");
           }
           else
           {
               values = false;
               //MessageBox.Show("String does not contain url ");
               //Console.WriteLine("String does not contain url  !");
           }
           return values;
       }
   }

   public class MailAttachment
   {
       #region Fields
       private MemoryStream stream;
       private string filename;
       private string mediaType;
       #endregion

       #region Properties
       public Stream Data { get { return stream; } }
       public string Filename { get { return filename; } }
       public string MediaType { get { return mediaType; } }

       public Attachment File { get { return new Attachment(Data, Filename, MediaType); } }

       #endregion

       public MailAttachment(byte[] data, string filename)
       {
           this.stream = new MemoryStream(data);
           this.filename = filename;
           this.mediaType = MediaTypeNames.Application.Octet;
       }

       public MailAttachment(string data, string filename)
       {
           this.stream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(data));
           this.filename = filename;
           this.mediaType = MediaTypeNames.Text.Html;
       }



   }

}
