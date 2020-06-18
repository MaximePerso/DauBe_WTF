using DauBe_WTF.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using DauBe_WTF.CircularProgressBar;

namespace DauBe_WTF.SecondaryWindows.AutoPos
{
    class AutoPosVM : VMBase
    {

        #region Fields
        private string _instructions;
        private SolidColorBrush _step1Foreground;
        private SolidColorBrush _step2Foreground;
        private SolidColorBrush _step3Foreground;
        private double _arrowOpacity1;
        private double _arrowOpacity2;
        private double _opacity2;
        private double _opacity3;
        private double _loading1Opacity;
        private bool _isBallOn;
        private bool _isBallOff;
        #endregion

        #region Properties
        public string Instructions
        {
            get => _instructions;
            set
            { _instructions = value; OnPropertyChanged("Instructions"); }
        }
        public SolidColorBrush Step1Foreground
        {
            get => _step1Foreground;
            set
            { _step1Foreground = value; OnPropertyChanged("Step1Foreground"); }
        }
        public SolidColorBrush Step2Foreground
        {
            get => _step2Foreground;
            set
            { _step2Foreground = value; OnPropertyChanged("Step2Foreground"); }
        }
        public SolidColorBrush Step3Foreground
        {
            get => _step3Foreground;
            set
            { _step3Foreground = value; OnPropertyChanged("Step3Foreground"); }
        }
        public double ArrowOpacity1
        {
            get => _arrowOpacity1;
            set
            { _arrowOpacity1 = value; OnPropertyChanged("ArrowOpacity1"); }
        }
        public double ArrowOpacity2
        {
            get => _arrowOpacity2;
            set
            { _arrowOpacity2 = value; OnPropertyChanged("ArrowOpacity2"); }
        }
        public double Opacity2
        {
            get => _opacity2;
            set
            { _opacity2 = value; OnPropertyChanged("Opacity2"); }
        }
        public double Opacity3
        {
            get => _opacity3;
            set
            { _opacity3 = value; OnPropertyChanged("Opacity3"); }
        }
        public double Loading1Opacity
        {
            get => _loading1Opacity;
            set
            { _loading1Opacity = value; OnPropertyChanged("Loading1Opacity"); }
        }
        public bool IsBallOn
        {
            get => _isBallOn;
            set
            { _isBallOn = value; OnPropertyChanged("IsBallOn"); }
        }
        public bool IsBallOff
        {
            get => _isBallOff;
            set
            { _isBallOff = value; OnPropertyChanged("IsBallOff"); }
        }
        #endregion

        #region Command
        public ICommand OkCommand { get; set; }
        #endregion

        #region Contructor
        public AutoPosVM()
        {
            Initialisation();
        }
        #endregion

        #region Private Methods
        private void Initialisation()
        {
            //property initialisation
            _step1Foreground = Brushes.Orange;
            _step2Foreground = Brushes.Red;
            _step3Foreground = Brushes.Red;
            _opacity2 = 0.2;
            _opacity3 = 0.2;
            _arrowOpacity1 = 0.2;
            _arrowOpacity2 = 0.2;
            _loading1Opacity = 0.0;
            _instructions = "Mettre la balle en caoutchouc en place";
            _isBallOn = false;
            _isBallOff = false;
        }

        
        #endregion
    }
}
