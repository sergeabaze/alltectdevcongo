using System.ComponentModel;
using System.Windows;

namespace AllTech.FrameWork.Models
{
    public abstract class ObservableModel : INotifyPropertyChanged
    {
        protected ObservableModel()
        {

        }

        #region INotifyPropertyChanged  

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

       
        #endregion
    }
}
