using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllTech.FrameWork.Global;

namespace AllTech.FacturationModule.Report
{
    public partial class formeExonereNonExoner : Form
    {
        DataTable tbClient;
        DataTable tbSociete;
        DataTable tbPiedPage;
        DataTable tbFacture;
        DataTable tbLigneFacture;
        DataTable tbLibelle;
        int  localType = 0;

        public formeExonereNonExoner(DataTable _tbClient, DataTable _tbsociete, DataTable _tbPiedPage,
            DataTable _tbFacture, DataTable _tbLigneFacture, DataTable _tbLibelle,int  typeClient)
        {
            InitializeComponent();
            this.Width =(int) (GlobalDatas.mainWidth * 0.90);
            this.Height = (int)(GlobalDatas.mainHeight * 0.85);

            tbClient = _tbClient;
            tbSociete = _tbsociete;
            tbPiedPage = _tbPiedPage;
            tbFacture = _tbFacture;
            tbLigneFacture = _tbLigneFacture;
            tbLibelle = _tbLibelle;
            localType = typeClient;

            Loads();
        }

        void Loads()
        {
            DataProvider.Ds.TableClient.Clear();
            DataProvider.Ds.Table_Societe.Clear();
            DataProvider.Ds.TPiedpagefacture.Clear();
            DataProvider.Ds.TableFacture.Clear();
            DataProvider.Ds.TableligneFacture.Clear();
            DataProvider.Ds.Tlibelle.Clear();

            

            //foreach (DataRow row in tbClient.Rows)
           IDataReader reader= tbClient.CreateDataReader ();
            DataProvider.Ds.TableClient.Load (reader);//.ImportRow(row);

            foreach (DataRow row in tbSociete.Rows)
                DataProvider.Ds.Table_Societe.ImportRow(row);

            foreach (DataRow row in tbPiedPage.Rows)
                DataProvider.Ds.TPiedpagefacture.ImportRow(row);

            foreach (DataRow row in tbFacture.Rows)
                DataProvider.Ds.TableFacture.ImportRow(row);

            foreach (DataRow row in tbLigneFacture.Rows)
                DataProvider.Ds.TableligneFacture.ImportRow(row);

            foreach (DataRow row in tbLibelle.Rows)
                DataProvider.Ds.Tlibelle.ImportRow(row);

            if (localType==1)
            { 
                // client non exonere, paie tva, pas de prorarat

               // reportNewNonPartiel rpt = new reportNewNonPartiel();
               // reportWithoutPorata rpt = new reportWithoutPorata();
                //NewNewExonereReport rpt = new NewNewExonereReport();
                ReportExonereNonExo rpt = new ReportExonereNonExo();
                rpt.SetDataSource(DataProvider.Ds);
                crystalReportViewer1.ReportSource = rpt;
            //}
            //else if (localType == 2)
            //{
            //    //client exonere pas de tva, mais prorata
            //   // reportExonere rpt = new reportExonere();
            //    NewReportExonere rpt = new NewReportExonere();
            //    rpt.SetDataSource(DataProvider.Ds);
            //    crystalReportViewer1.ReportSource = rpt;
            }
            else if (localType == 3)
            {
                //client partiel exonere

               // NewReportCliPartiel rpt = new NewReportCliPartiel();
                reportClientPartiel rpt = new reportClientPartiel();
                // reportNewPartiel rpt = new reportNewPartiel();

                rpt.SetDataSource(DataProvider.Ds);
                crystalReportViewer1.ReportSource = rpt;
            }
            
          
           
            crystalReportViewer1.Refresh();
        }
    }
}
