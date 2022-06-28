using System;
using System.Collections.ObjectModel;
using AllTech.FrameWork.Resources;

namespace AllTech.FrameWork.Models.Series
{
    public class FinancialDataModel
    {
        
    }
    public class FinancialDataCollection : ObservableCollection<FinancialDataPoint>
    {
        public FinancialDataCollection()
        {
            this.Add(new FinancialDataPoint { Spending = 20, Budget = 60, Label = SeriesStrings.XWDC_DataModel_CompanyFinanceCategory_Administration, });
            this.Add(new FinancialDataPoint { Spending = 80, Budget = 40, Label = SeriesStrings.XWDC_DataModel_CompanyFinanceCategory_Sales, });
            this.Add(new FinancialDataPoint { Spending = 20, Budget = 60, Label = SeriesStrings.XWDC_DataModel_CompanyFinanceCategory_IT, });
            this.Add(new FinancialDataPoint { Spending = 80, Budget = 40, Label = SeriesStrings.XWDC_DataModel_CompanyFinanceCategory_Marketing, });
            this.Add(new FinancialDataPoint { Spending = 20, Budget = 60, Label = SeriesStrings.XWDC_DataModel_CompanyFinanceCategory_Development, });
            //this.Add(new FinancialDataPoint { Spending = 60, Budget = 20, Label = SeriesStrings.XWDC_DataModel_CompanyFinanceCategory_CustomerSupport, });
            this.Add(new FinancialDataPoint { Spending = 60, Budget = 20, Label ="Factures Sorties", });
        }

    }
    public class FinancialDataPoint
    {
        public string Label { get; set; }
        public double Spending { get; set; }
        public double Budget { get; set; }


        public string ToolTip { get { return String.Format("{0}, Spending {1}, Budget {2}", Label, Spending, Budget); } }

    }
}