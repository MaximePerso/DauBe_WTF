using DauBe_WTF.Utility;
using InteractiveGraphUserControl.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DauBe_WTF.SecondaryWindows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace DauBe_WTF.ViewModel
{
    class MainVM : VMBase
    {
        #region Sub VM Fields
        private InteractiveGraphUserControl.MVVM.ViewModel _ucVM;
        private SecondaryWindows.AutoPos.AutoPosVM _autopos;
        public SubVM.DoliVM doli { get; } = new SubVM.DoliVM();
        public SubVM.GraphVM graph { get; } = new SubVM.GraphVM();
        public CircularProgressBar.CPBVM pg { get; } = new CircularProgressBar.CPBVM();
        #endregion
        #region Sub VM Properties
        public InteractiveGraphUserControl.MVVM.ViewModel UCVM
        {
            get => _ucVM;
            set
            { _ucVM = value; OnPropertyChanged("UCVM"); }
        }
        public SecondaryWindows.AutoPos.AutoPosVM autopos
        {
            get => _autopos;
            set
            { _autopos = value; OnPropertyChanged("autopos"); }
        }
        #endregion
        #region Fields
        private bool _isDoliBusy;
        #endregion
        #region Properties
        public bool IsDoliBusy
        {
            get => _isDoliBusy;
            set
            { _isDoliBusy = value; OnPropertyChanged("IsDoliBusy"); }
        }
        #endregion 
        #region Commands
        public ICommand AutoPosCommand { get; set; }
        public IAsyncCommand OkCommand { get; set; }
        #endregion
        public IView view;

        public MainVM()
        {
            //Gridgraph usercontrol
            UCVM = new InteractiveGraphUserControl.MVVM.ViewModel(view);
            //Auto positioning windows
            autopos = new SecondaryWindows.AutoPos.AutoPosVM();
            AutoPosCommand = new RelayCommand(o => FireAutoPosWin(), o => true);
            OkCommand = new AsyncCommand(NextAction,CheckBusiness);
        }

        private void FireAutoPosWin()
        {
            //initialisation
            pg.ProgressValue = 1;

            var autoPosWin = new SecondaryWindows.AutoPos.AutoPos();
            autoPosWin.Show();
        }

        private async Task NextAction()
        {
            try
            {
                IsDoliBusy = true;
                if (autopos.IsBallOn == false)
                {
                    autopos.IsBallOn = true;
                    autopos.Loading1Opacity = 1;
                    autopos.Step1Foreground = Brushes.Green;
                    //await doli.AsyncAutoPosApproach();
                    //await doli.AsyncAutoPosBallRelease();
                    await doli.pouetpouet();
                    autopos.Instructions = "Veuillez enlever la balle en caoutchouc";
                    autopos.ArrowOpacity1 = 1;
                    autopos.Loading1Opacity = 0;
                    autopos.Step2Foreground = Brushes.Orange;
                    autopos.Opacity2 = 1;
                }
                else
                {
                    autopos.IsBallOff = true;
                    await doli.pouetpouet();
                    autopos.Step2Foreground = Brushes.Green;
                    autopos.Step3Foreground = Brushes.Orange;
                    pg.Loading2Opacity = 1;
                    //await doli.AsyncAutoPosFinal(pg.ProgressValue);
                    autopos.Instructions = "Le piston est en place !";
                    autopos.Step3Foreground = Brushes.Green;
                    pg.Loading2Opacity = 0;
                    autopos.ArrowOpacity2 = 1;
                    autopos.Opacity2 = 1;
                }
            }
            finally
            {
                IsDoliBusy = false;
            }
        }

        private bool CheckBusiness()
        {
            return !IsDoliBusy;
        }
    }
}
