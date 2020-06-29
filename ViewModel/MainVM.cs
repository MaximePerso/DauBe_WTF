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
using DauBe_WTF.CircularProgressBar;

namespace DauBe_WTF.ViewModel
{
    public class MainVM : VMBase
    {
        #region Sub VM Fields
        private InteractiveGraphUserControl.MVVM.ViewModel _ucVM;
        private SecondaryWindows.AutoPos.AutoPosVM _autopos;
        private SecondaryWindows.InputList.InputListVM _inputList;
        private CPBVM _pg;
        private SubVM.DoliVM _doli;
        public SubVM.GraphVM graph { get; } = new SubVM.GraphVM();
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
        public SecondaryWindows.InputList.InputListVM inputList
        {
            get => _inputList;
            set
            { _inputList = value; OnPropertyChanged("inputList"); }
        }
        public SubVM.DoliVM Doli
        {
            get => _doli;
            set
            { _doli = value; OnPropertyChanged("Doli"); }
        }

        public CPBVM pg
        {
            get => _pg;
            set
            { _pg = value; OnPropertyChanged("pg"); }
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
        public ICommand MoveUpCommand { get; set; }
        public ICommand MoveDownCommand { get; set; }
        #endregion
        public IView view;

        public MainVM()
        {
            //DOli
            Doli = new SubVM.DoliVM();
            Doli.ConnectToEdc();
            //Gridgraph usercontrol
            UCVM = new InteractiveGraphUserControl.MVVM.ViewModel(view);
            //Auto positioning windows
            autopos = new SecondaryWindows.AutoPos.AutoPosVM(pg,Doli);
            //inputList = new SecondaryWindows.InputList.InputListVM(Doli, UCVM);
            pg = new CPBVM();
            AutoPosCommand = new RelayCommand(o => FireAutoPosWin());
            InputListCommand = new RelayCommand(o => FireInputListWin());
        }

        private void FireAutoPosWin()
        {
            var autoPosWin = new SecondaryWindows.AutoPos.AutoPos(Doli, pg);
            autoPosWin.Show();
        }

        private void FireInputListWin()
        {
            var inputListWin = new SecondaryWindows.InputList.InputLitst(Doli);
            inputListWin.Show();
        }
    }
}
