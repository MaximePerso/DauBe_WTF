using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GS = GalaSoft.MvvmLight.Command;
using IGUC = InteractiveGraphUserControl.MVVM;
using DauBe_WTF.Utility;
using InteractiveGraphUserControl.Utility;
using System.Windows.Forms;

namespace DauBe_WTF.SecondaryWindows.InputList
{
    public class InputListVM : VMBase
    {
        #region Usercontrol property
        private IGUC.ViewModel _uc;
        public IGUC.ViewModel UC
        {
            get => _uc;
            set
            { 
                _uc = value;
                OnPropertyChanged("UC");
            }
        }
        #endregion

        private double _tmpLoad = 0;
        private double _tmpPos = 0;
        private bool _isDoliBusy;

        private ViewModel.SubVM.DoliVM _doli { get; }

        private IView view;

        public InputListVM(ViewModel.SubVM.DoliVM doli )
        {
            _isDoliBusy = false;
            UC = new IGUC.ViewModel(view);
        }

        private bool ProcessSummary()
        {
            if (MessageBox.Show("Vous vous apprêtez à lancer une séquence de " + UC.DoliInputCollection.Count() + " commandes. La durée des opérations est estimée à " + UC.DestPosSeriesValues.Last().X + " secondes", "",MessageBoxButtons.OKCancel) == DialogResult.OK)
                return true;
            else
                return false;
        }

        private GS.RelayCommand _complexCycle;
        public GS.RelayCommand ComplexCycle
        {
            get
            {
                return _complexCycle
                       ?? (_complexCycle = new GS.RelayCommand(
                           async () =>
                           {
                               _isDoliBusy = true;
                               if (ProcessSummary())
                                   _doli.ComplexeCycle(UC.NbCycle, UC.DoliInputCollection);
                               await DoliMovement();
                               _complexCycle.RaiseCanExecuteChanged();
                           }, () => { return !_isDoliBusy; }));
            }
            
        }

        public async Task DoliMovement()
        {
            await Task.Run(() =>
            {
                //On regarde si le chargement ou la position continue de bouger. Il vaut mieux round, parce qu'égaliser un float ne marche pas souvent quand on mesure des grandeurs physiques.
                while (Math.Round(_tmpLoad,0) != Math.Round(_doli.DoliLoad,0) && Math.Round(_tmpPos,2) != Math.Round(_doli.DoliPosition,2))
                {
                    _tmpLoad = _doli.DoliLoad;
                    _tmpPos = _doli.DoliPosition;
                    _isDoliBusy = true;
                    System.Threading.Thread.Sleep(30000); //30 sec.
                }
            });
        }



    }
}
