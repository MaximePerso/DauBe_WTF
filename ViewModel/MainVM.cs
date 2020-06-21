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
    public class MainVM : VMBase
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
        public ICommand InputListCommand { get; set; }
        public IAsyncCommand OkCommand { get; set; }
        #endregion
        public IView view;

        public MainVM()
        {
            //Gridgraph usercontrol
            UCVM = new InteractiveGraphUserControl.MVVM.ViewModel(view);
            //Auto positioning windows
            autopos = new SecondaryWindows.AutoPos.AutoPosVM(pg,doli);
            AutoPosCommand = new RelayCommand(o => FireAutoPosWin());
            InputListCommand = new RelayCommand(o => InputListWin());
        }

        private void FireAutoPosWin()
        {
            var autoPosWin = new SecondaryWindows.AutoPos.AutoPos();
            autoPosWin.Show();
        }

        private void FireInputListWin()
        {
            var inputListWin = new SecondaryWindows.InputList.InputLitst();
            inputListWin.Show();
        }
    }
}
