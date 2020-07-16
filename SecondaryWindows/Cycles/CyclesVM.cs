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
            StartCycles = new RelayCommand(o => CyclesCommand(), o=> { return !doli.isDoliBusy; });
        }

        #endregion

        private void CyclesCommand()
        {
            //doli.Cycles(Cycles.MyCycle.MoveCtrl, Cycles.MyCycle.Speed1, Cycles.MyCycle.Dest1, Cycles.MyCycle.Halt1, Cycles.MyCycle.Speed2, Cycles.MyCycle.Dest2, Cycles.MyCycle.Halt2, Cycles.MyCycle.Cycles, Cycles.MyCycle.SpeedFinal, Cycles.MyCycle.DestFinal);
            Console.WriteLine(Cycles.MyCycle.MoveCtrl + " " + Cycles.MyCycle.Speed1 + " " + _cycles.MyCycle.Dest1 + " " + Cycles.MyCycle.Halt1 + " " + Cycles.MyCycle.Speed2 + " " + Cycles.MyCycle.Dest2 + " " + Cycles.MyCycle.Halt2 + " " + Cycles.MyCycle.Cycles + " " + Cycles.MyCycle.SpeedFinal + " " + Cycles.MyCycle.DestFinal);
        }

    }
}
