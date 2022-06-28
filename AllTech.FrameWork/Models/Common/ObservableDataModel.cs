using System.Collections.ObjectModel;

namespace AllTech.FrameWork.Models
{
    public abstract class ObservableDataModel33 : ObservableModel
    {
        protected ObservableDataModel33()
        {
            _data = new DataCollection33();
            
            _settings = new DataSettings33();
            _settings.PropertyChanged += OnSettingsPropertyChanged;
            
        }
        private void OnSettingsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.Generate();
        }
        public abstract void Generate();

        private DataCollection33 _data;
        public DataCollection33 Data
        {
            get { return _data; }
            set
            {
                if (_data == value) return;
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        private DataSettings33 _settings;
        public DataSettings33 Settings
        {
            get { return _settings; }
            set
            {
                if (_settings == value) return;
                _settings = value;
                this.Generate();
                OnPropertyChanged("Settings");
            }
        }
    }
    public class DataCollection33 : ObservableCollection<DataPoint22>
    {
        public DataCollection33()
        {

        }

    }
    public class DataSettings33 : ObservableModel
    {
        public DataSettings33()
        {
            DataPoints = 100;
        }
        public int DataPoints { get; set; }

    }
    public class DataPoint22 : ObservableModel
    {
        #region Properties
        private string _label;
        public string Label
        {
            get { return _label; }
            set
            {
                if (_label == value) return;
                _label = value;
                OnPropertyChanged("Label");
            }
        }

        private int _index;
        public int Index
        {
            get { return _index; }
            set
            {
                if (_index == value) return;
                _index = value;
                OnPropertyChanged("Index");
            }
        }
   
        public string Caption
        {
            get { return this.ToString(); }
        }

        #endregion
    }
    
}
