using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GoodCircularProgressBar.mvvmSupport;

namespace DauBe_WTF.CircularProgressBar
{
    public class CPBVM : INotifyPropertyChanged
    {
        private double _loading2Opacity;
        public double Loading2Opacity
        {
            get => _loading2Opacity;
            set
            { _loading2Opacity = value; OnPropertyChanged("Loading2Opacity"); }
        }
        private int _progressValue;
        public int ProgressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                OnPropertyChanged("ProgressValue");
                OnPropertyChanged("ProgressText");
            }
        }

        public string ProgressText
        {
            get { return string.Format("{0} %", _progressValue); }
        }


        public CPBVM()
        {
            _loading2Opacity = 0.0;
        }
        

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
