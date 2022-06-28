using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllTech.FacturationModule.Report
{
    public partial class FormreportCompta : Form
    {
        public FormreportCompta(DataTable tableJv)
        {
            InitializeComponent();

            DataProvider.Ds.TableJournalvente.Clear();
            IDataReader reader = tableJv.CreateDataReader();
            DataProvider.Ds.TableJournalvente.Load(reader);

            JournalventeReport rpt = new JournalventeReport();
            rpt.SetDataSource(DataProvider.Ds);
            crystalReportViewer1.ReportSource = rpt;

            crystalReportViewer1.Refresh();
        }
    }
}
