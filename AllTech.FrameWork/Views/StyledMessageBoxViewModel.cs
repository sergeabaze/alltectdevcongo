using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;

namespace AllTech.FrameWork.Views
{
    public class StyledMessageBoxViewModel : ViewModelBase
    {

        #region Member Data

        private string message = string.Empty;

        #endregion

        #region Constructor

        public StyledMessageBoxViewModel()
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
    }
}
