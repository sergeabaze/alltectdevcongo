using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using AllTech.FrameWork.PropertyChange ;

namespace AllTech.FrameWork.Models
{
    public class SparklineData : ObservableCollection<SparklineDataItem>
    {
        public SparklineData()
        {
            Add(new SparklineDataItem { Label = "IBM", Value = 7 });
            Add(new SparklineDataItem { Label = "SONY", Value = 5 });
            Add(new SparklineDataItem { Label = "DELL", Value = 3 });
            Add(new SparklineDataItem { Label = "Apple", Value = 4 });
            Add(new SparklineDataItem { Label = "Hitachi", Value = 1 });
            Add(new SparklineDataItem { Label = "Acer", Value = -1 });
            Add(new SparklineDataItem { Label = "HP", Value = -2 });
            Add(new SparklineDataItem { Label = "Asus", Value = 2 });
            Add(new SparklineDataItem { Label = "Gateway", Value = 1 });
        }
    }

    public class SparklineDataItem : ViewModelBase
    {
        private string _label;
        public string Label
        {
            get { return _label; }
            set { _label = value; OnPropertyChanged("Label"); }
        }

        private double _value;
        public double Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged("Value"); }
        }
    }
}
