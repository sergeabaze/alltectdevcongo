using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AllTech.FrameWork.Global;

namespace AllTech.FacturationModule.Report
{
    public partial class ModalReport : Form
    {
        DataTable tbFacture;
        DataTable tbLigneFacture;
        DataTable tbClient;
        DataTable tbSociete;
        DataTable tbPiedPage;
        DataTable tbLibelle;

        public ModalReport(DataTable _tbFacture, DataTable _tbLigneFacture, DataTable _tbClient, DataTable _tbsociete, DataTable _tbPiedPage, DataTable _tbLibelle)
        {
            InitializeComponent();
            this.Width = (int)(GlobalDatas.mainWidth * 0.90);
            this.Height = (int)(GlobalDatas.mainHeight * 0.85);

            tbFacture = _tbFacture;
            tbLigneFacture = _tbLigneFacture;
            tbClient = _tbClient;
            tbSociete = _tbsociete;
            tbPiedPage = _tbPiedPage;
            tbLibelle = _tbLibelle;
        }

        private void ModalReport_Load(object sender, EventArgs e)
        {
            DataProvider.Ds.TableClient.Clear();
            DataProvider.Ds.Table_Societe.Clear();
            DataProvider.Ds.TPiedpagefacture.Clear();
            DataProvider.Ds.DtblFacture.Clear();
            DataProvider.Ds.DtblLigneFacture.Clear();
            DataProvider.Ds.Tlibelle.Clear();

            try
            {
            
                foreach (DataRow row in tbFacture.Rows)
                    DataProvider.Ds.DtblFacture.ImportRow(row);

                foreach (DataRow row in tbLigneFacture.Rows)
                    DataProvider.Ds.DtblLigneFacture.ImportRow(row);

                IDataReader reader = tbClient.CreateDataReader();
                DataProvider.Ds.TableClient.Load(reader);//.ImportRow(row);

                foreach (DataRow row in tbSociete.Rows)
                    DataProvider.Ds.Table_Societe.ImportRow(row);

                foreach (DataRow row in tbPiedPage.Rows)
                    DataProvider.Ds.TPiedpagefacture.ImportRow(row);
                foreach (DataRow row in tbLibelle.Rows)
                    DataProvider.Ds.Tlibelle.ImportRow(row);

             
                    ReportExonereNonExo rpt = new ReportExonereNonExo();
                    rpt.SetDataSource(DataProvider.Ds);
                    System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                    rpt.PrintOptions.PrinterName = GlobalDatas.printerName ?? printDocument.PrinterSettings.PrinterName; 
                    crystalReportViewer1.ReportSource = rpt;
               
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
