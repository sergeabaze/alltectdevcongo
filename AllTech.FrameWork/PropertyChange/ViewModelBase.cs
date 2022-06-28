﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;

namespace AllTech.FrameWork.PropertyChange
{
    public abstract  class ViewModelBase : IDisposable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected ViewModelBase()
        {
        }

        ~ViewModelBase()
        {
            this.Dispose(false);
            //Debug.Fail("Dispose not called on ViewModel class.");
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.EnsureProperty(propertyName);
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        private void EnsureProperty(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                throw new ArgumentException("Property does not exist.", "propertyName");
            }
        }
    }
}
