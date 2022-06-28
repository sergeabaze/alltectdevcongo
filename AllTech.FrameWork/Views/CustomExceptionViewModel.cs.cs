using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace AllTech.FrameWork.Views
{
    public class CustomExceptionViewModel : INotifyPropertyChanged
    {
        #region Member Data

        private string message = string.Empty;

        #endregion

        #region Constructor

        public CustomExceptionViewModel()
        {
        }

        #endregion

        #region Properties

        public string Message
        {
            get { return this.message; }
            set
            {
                this.message = value;
                this.OnPropertyChanged("Message");
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
