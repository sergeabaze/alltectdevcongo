using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using AllTech.FrameWork.Command;
using AllTech.FacturationModule.ViewModel;
using System.Windows.Input;
using AllTech.FrameWork.Model;
using AllTech.FrameWork.Global;
using AllTech.FrameWork.Utils;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Net.Mail;
using AllTech.FrameWork.Views;
using System.Windows;


namespace AllTech.FacturationModule.Views.Modal
{
    

    public class MailViewModel : ViewModelBase
    {
       

        #region FIELDS
       // private extern static bool InternetGetConnectedState(out int conn, int val);
        private RelayCommand sendMailCommand;
        SocieteModel SocieteCourante;
        UtilisateurModel UserConnected = null;
        ParametresModel ParametersDatabase;
        DroitModel CurrentDroit;
         List<LignesFichiers> ListeFichiersDossiers;
         List<LignesFichiers> actualListeFichiersDossiers;
        string messagebody;
        string messagefinal;
        string toMail;
        string frommail;
        string mailCopy;
        string titreMail;
        bool isBusy;
        bool isSendMAil;
        Window localwindow;
       
        #endregion
        public MailViewModel(DroitModel _currentDroit, List<LignesFichiers> fichiersAttache,Window window)
        {
            localwindow = window;
            SocieteCourante = GlobalDatas.DefaultCompany;
            UserConnected = GlobalDatas.currentUser;
            CurrentDroit = _currentDroit;
            ParametersDatabase = GlobalDatas.dataBasparameter;
            ListeFichiersDossiers = fichiersAttache ;
            LoadDatas();

            //Messagebody = "mail à transféré le plutot possible";
        }

        #region PROPERTIES

        public bool IsSendMAil
        {
            get { return isSendMAil; }
            set { isSendMAil = value;
            OnPropertyChanged("IsSendMAil");
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value;
            OnPropertyChanged("IsBusy");
            }
        }

        public List<LignesFichiers> ActualListeFichiersDossiers
        {
            get { return actualListeFichiersDossiers; }
            set { actualListeFichiersDossiers = value;
            OnPropertyChanged("ActualListeFichiersDossiers");
            }
        }

        public string ToMail
        {
            get { return toMail; }
            set { toMail = value;
            OnPropertyChanged("ToMail");
            }
        }

        public string Messagebody
        {
            get { return messagebody; }
            set { messagebody = value;
            OnPropertyChanged("Messagebody");
            }
        }

        public string MailCopy
        {
            get { return mailCopy; }
            set { mailCopy = value;
            OnPropertyChanged("MailCopy");
            }
        }

        public string TitreMail
        {
            get { return titreMail; }
            set { titreMail = value;
            OnPropertyChanged("TitreMail");
            }
        }

        public string Messagefinal
        {
            get { return messagefinal; }
            set { messagefinal = value;
            OnPropertyChanged("Messagefinal");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand SendMailCommand
        {
            get
            {
                if (this.sendMailCommand == null)
                {
                    this.sendMailCommand = new RelayCommand(param => this.canSend(), param => this.canExecuteSend());
                }
                return this.sendMailCommand;
            }
        }
        #endregion

        #region METHODS
        void LoadDatas()
        {
            ToMail = ParametersDatabase.MailTo;
            ActualListeFichiersDossiers = ListeFichiersDossiers;
        }
        void canSend()
        {

            IsBusy = true;
            int Out;
            try
            {
                //if (InternetGetConnectedState(out Out, 0) == true)
                //{
                if (string.IsNullOrEmpty(TitreMail))
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "MESSAGE ";
                    view.ViewModel.Message = "renseignez le titre pour ce mail ";
                    view.ShowDialog();
                    IsBusy = false;
                    IsSendMAil = false;
                    return;
                }

               
                if (string.IsNullOrEmpty(ParametersDatabase.PortSmtp))
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "MESSAGE ";
                    view.ViewModel.Message = "renseignez le port SMTP ";
                    view.ShowDialog();
                    IsBusy = false;
                    IsSendMAil = false;
                    return;
                }
                if (ListeFichiersDossiers.Count == 0)
                {


                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "MESSAGE ";
                    view.ViewModel.Message = "Vous n'avez pas rattacher de fichier d'extraction SVP !";
                    view.ShowDialog();
                    IsBusy = false;
                    IsSendMAil = false;
                    return;
                }
                if (!string.IsNullOrEmpty(ParametersDatabase.LogginSmtp) && string.IsNullOrEmpty(ParametersDatabase.PasswordSmtp))
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "MESSAGE ";
                    view.ViewModel.Message = "Veillez renseignez le mot de passe pour ce serveur SMTP !";
                    view.ShowDialog();
                    IsSendMAil = false;
                    IsBusy = false;
                    return;
                }
                    Attachment[] attachListes = new Attachment[ListeFichiersDossiers.Count];

                   int i=0;
                    foreach (LignesFichiers attach in ListeFichiersDossiers)
                    {
                        Attachment newattache =new Attachment(attach.url);
                        attachListes[i] = newattache;
                        i++;
                    }

                    ClsSentMail.SenMail(ToMail, Messagebody, TitreMail,
                        ParametersDatabase.MailFrom, "filsserge@gmail.com", ParametersDatabase.LogginSmtp, ParametersDatabase.PasswordSmtp, ParametersDatabase.Smtp,
                         MailCopy,ParametersDatabase.PortSmtp , attachListes);
                    Messagefinal = "Document transférés";

                    Utils.logUserActions(string .Format ( "<--Envoie Fichier par mail par {0} à  {1} ", UserConnected.Loggin,ToMail), "");
                    IsBusy = false;
                    IsSendMAil = true;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                IsSendMAil = true ;
                Messagefinal = "Document non  transférés";
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "MESSAGE ENVOI MAIL";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

                Utils.logUserActions(string.Format("<--Erreure lors de l'envoie Fichier par mail par {0} à  {1} ", UserConnected.Loggin, ToMail), "");
            }
        }

        bool canExecuteSend()
        {
            return true;
        }
        #endregion
    }
}
