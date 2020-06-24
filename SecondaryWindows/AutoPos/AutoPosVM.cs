using DauBe_WTF.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using DauBe_WTF.CircularProgressBar;
using GS = GalaSoft.MvvmLight.CommandWpf;
using System.Threading;
using System.Windows;

namespace DauBe_WTF.SecondaryWindows.AutoPos
{
    public class AutoPosVM : VMBase
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
        private bool _isBusy;
        #endregion

        #region Properties
        private CircularProgressBar.CPBVM _pg { get; }
        private ViewModel.SubVM.DoliVM _doli { get; }
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
        public bool IsBusy
        {
            get => _isBusy;
            set
            { _isBusy = value; OnPropertyChanged("IsBusy"); }
        }
        #endregion

        #region Command
        public ICommand OkCommand { get; set; }
        #endregion

        #region Contructor
        public AutoPosVM() { }
        public AutoPosVM(CPBVM pg, ViewModel.SubVM.DoliVM doli)
        {
            Initialisation();
            _doli = doli;
            _pg = pg;
            OkCommand = new RelayCommand(o => { Console.WriteLine(Opacity3); });
        }
        #endregion

        #region Private Methods
        private void Initialisation()
        {
            //property initialisation
            Step1Foreground = Brushes.Orange;
            Step2Foreground = Brushes.Red;
            Step3Foreground = Brushes.Red;
            Opacity2 = 0.2;
            Opacity3 = 0.2;
            ArrowOpacity1 = 0.2;
            ArrowOpacity2 = 0.2;
            Loading1Opacity = 0.0;
            Instructions = "Mettre la balle en caoutchouc en place";
            IsBallOn = false;
            IsBallOff = false;
        }

        private GS.RelayCommand _autoPosCycle;
        public GS.RelayCommand AutoPosCycle
        {
            get
            {
                return _autoPosCycle
                        ?? (_autoPosCycle = new GS.RelayCommand(
                            async () =>
                            {
                                IsBusy = true;
                                if (IsBallOn == true && IsBallOff == true)
                                {
                                    MessageBox.Show("Hophophop Maurice, qu'est-ce que tu crois que tu vas faire la ? On reprend");
                                    Initialisation();
                                    IsBusy = false;
                                    _autoPosCycle.RaiseCanExecuteChanged();
                                }
                                else if (IsBallOn == false)
                                {
                                    IsBallOn = true;
                                    Loading1Opacity = 1;
                                    Step1Foreground = Brushes.Green;
                                    await AsyncAutoPosApproach();
                                    await AsyncAutoPosBallRelease();
                                    Instructions = "Veuillez enlever la balle en caoutchouc";
                                    ArrowOpacity1 = 1;
                                    Loading1Opacity = 0;
                                    Step2Foreground = Brushes.Orange;
                                    Opacity2 = 1;
                                    IsBusy = false;
                                    //on libère le bouton
                                    _autoPosCycle.RaiseCanExecuteChanged();
                                }
                                else
                                {
                                    IsBallOff = true;
                                    Step2Foreground = Brushes.Green;
                                    Step3Foreground = Brushes.Orange;
                                    _pg.Loading2Opacity = 1;
                                    await AsyncAutoPosFinal();
                                    Instructions = "Le piston est en place !";
                                    Step3Foreground = Brushes.Green;
                                    _pg.Loading2Opacity = 0;
                                    ArrowOpacity2 = 1;
                                    Opacity3 = 1;
                                    IsBusy = false;
                                    //on libère le bouton
                                    _autoPosCycle.RaiseCanExecuteChanged();
                                }
                            }, () => { return !IsBusy; }, false));
            }
        }

        public async Task pouetpouet()
        {
            await Task.Run(() =>
            {
                int i = 0;
                while (i < 101)
                {
                    _pg.ProgressValue = i;
                    Thread.Sleep(20);
                    i++;
                }
                _pg.ProgressValue = 0;
            });
        }

        public async Task AsyncAutoPosFinal()
        {
            _pg.ProgressValue = 0;
            // On replace le piston à sa place basse
            _doli.AutoPosFinal();
            await Task.Run(() =>
            {
                while (Math.Round(_doli.DoliPosition, 2) != Math.Round(_doli.TempDestination, 2))
                {
                    _pg.ProgressValue = Math.Round((1 - (Math.Abs(Math.Abs(_doli.DoliPosition - _doli.TempDestination)) / _doli.SquishedBall) * 100), 1);
                    System.Threading.Thread.Sleep(25);
                }
            });
        }

        public async Task AsyncAutoPosApproach()
        {
            _doli.AutoPosApproach();
            await Task.Run(() =>
            {
                while (Math.Abs(_doli.DoliLoad) < Math.Abs(_doli.TempLim) * 0.8)
                {
                    System.Threading.Thread.Sleep(250);
                }
            });
        }

        public async Task AsyncAutoPosBallRelease()
        {
            _doli.AutoPosBallRelease();
            await Task.Run(() =>
            {
                while (Math.Abs(Math.Abs(_doli.DoliPosition) - Math.Abs(_doli.TempDestination)) > 0.2)
                {
                    System.Threading.Thread.Sleep(250);
                }
            });
        }

        #endregion
    }
}
