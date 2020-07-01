using DauBe_WTF.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DauBe_WTF.SecondaryWindows.Cycles
{
    class CyclesVM : VMBase
    {
        #region UC property

        private ViewModel.SubVM.DoliVM doli;
        private DoPE_Cycles_UC.MVVM.ViewModel _cycles;

        public DoPE_Cycles_UC.MVVM.ViewModel Cycles
        {
            get => _cycles;
            set
            { _cycles = value; OnPropertyChanged("Cycles"); }
        }

        #endregion

        #region Command
        public ICommand StartCycles { get; set; }
        #endregion

        #region Constructeur

        public CyclesVM(ViewModel.SubVM.DoliVM _doli)
        {
            _cycles = new DoPE_Cycles_UC.MVVM.ViewModel();
            doli = _doli;
            StartCycles = new RelayCommand(o => CyclesCommand());
        }

        #endregion

        private void CyclesCommand()
        {
            doli.Cycles(_cycles.MyCycle.MoveCtrl, _cycles.MyCycle.Speed1, _cycles.MyCycle.Dest1, _cycles.MyCycle.Halt1, _cycles.MyCycle.Speed2, _cycles.MyCycle.Dest2, _cycles.MyCycle.Halt2, _cycles.MyCycle.Cycles, _cycles.MyCycle.SpeedFinal, _cycles.MyCycle.DestFinal);
        }

    }
}
