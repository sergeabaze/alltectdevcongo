using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AllTech.FrameWork.Global;
using System.Data;
using AllTech.FrameWork.Helpers;

namespace AllTech.FacturationModule.Views
{
    /// <summary>
    /// Interaction logic for AdminDatabase.xaml
    /// </summary>
    public partial class AdminDatabase : Window
    {

        DataTable dt;

        public AdminDatabase()
        {
            InitializeComponent();
            this.Height = GlobalDatas.mainHeight * 0.55;
            this.Width = GlobalDatas.mainWidth * 0.55;

            txtQuery.Text = "SHOW TABLE STATUS;";
            ExecuteSQL(1);
        }

        private void btnCloseModla_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           
        }

        void ExecuteSQL(int q)
        {

            try
            {
                string sql = txtQuery.Text.Trim();
                if (q == 2)
                {

                    dt = Helper.executeScripte2(sql);
                    BindData();
                }
                else
                {
                    string newsql=null;
                    dt = Helper.executeScripte1(ref newsql, sql);
                    if (!string.IsNullOrEmpty(newsql))
                    {
                        txtQuery.Text = newsql;
                        txtQuery.Refresh();
                    }

                }
            }
            catch (Exception ex)
            {
                WriteError(ex);
            }
        }


        void WriteError(Exception ex)
        {
            dt = new DataTable();
            dt.Columns.Add("Error");
            dt.Rows.Add(ex.Message);
            BindData();
        }

        void BindData()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<html><head><style> </style></head>");
            sb.AppendLine("<body>");

            sb.AppendFormat(ConvertDataTableToHtmlTable(dt));

            sb.AppendLine("</body>");
            sb.AppendFormat("</html>");
            webbrosergrid.Navigate(sb.ToString());
            //webBrowser1.DocumentText = sb.ToString();
        }

        public static string ConvertDataTableToHtmlTable(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<table>");

            sb.AppendLine("<tr>");
            sb.AppendFormat("\t");
            foreach (DataColumn dc in dt.Columns)
            {
                sb.AppendFormat("<td>");
                sb.AppendFormat(EscapeForHtml(dc.ColumnName));
                sb.AppendFormat("</td>");
            }
            sb.AppendLine();
            sb.AppendLine("</tr>");

            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendFormat("<tr>");
                foreach (DataColumn dc in dt.Columns)
                {
                    sb.AppendFormat("<td>");
                      
                    //string dataStr = QueryExpress.ConvertToSqlFormat(dr[dc.ColumnName], false, false, "");
                    string dataStr = dr[dc.ColumnName].ToString();
                    sb.AppendFormat(EscapeForHtml(dataStr));
                    sb.AppendFormat("</td>");
                }
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</table>");

            return sb.ToString();
        }

        public static string EscapeForHtml(string input)
        {
            input = input.Replace("\r\n", "^||||^").Replace("\n", "^||||^").Replace("\r", "^||||^");
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
            foreach (char c in input)
            {
                switch (c)
                {
                    case '&':
                        sb2.AppendFormat("&amp;");
                        break;
                    case '"':
                        sb2.AppendFormat("&quot;");
                        break;
                    case '\'':
                        sb2.AppendFormat("&#39;");
                        break;
                    case '<':
                        sb2.AppendFormat("&lt;");
                        break;
                    case ' ':
                        sb2.AppendFormat("&nbsp;");
                        break;
                    case '\t':
                        sb2.AppendFormat("&nbsp;&nbsp;&nbsp;&nbsp;");
                        break;
                    case '>':
                        sb2.AppendFormat("&gt;");
                        break;
                    default:
                        sb2.Append(c);
                        break;
                }
            }
            return sb2.ToString().Replace("^||||^", "<br />");
        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            ExecuteSQL(2);

            
        }
    }
}
