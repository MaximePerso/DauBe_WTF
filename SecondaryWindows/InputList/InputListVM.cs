using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GS = GalaSoft.MvvmLight.Command;
using IG_gridGraphDC = InteractiveGraphUserControl.MVVM;
using DauBe_WTF.Utility;
using InteractiveGraphUserControl.Utility;
using System.Windows.Forms;

namespace DauBe_WTF.SecondaryWindows.InputList
{
    public class InputListVM : VMBase
    {
        #region Usercontrol property
        private IG_gridGraphDC.ViewModel _gridGraphDC;
        public IG_gridGraphDC.ViewModel GridGraphDC
        {
            get => _gridGraphDC;
            set
            { _gridGraphDC = value; OnPropertyChanged("GridGraphUC"); }
        }
        #endregion

        private double _tmpLoad = 0;
        private double _tmpPos = 0;
        private bool _isDoliBusy;
        private IView view;
        
        public ICommand test { get; set; }

        private ViewModel.SubVM.DoliVM _doli;

        public InputListVM(ViewModel.SubVM.DoliVM doli)
        {
            _doli = doli;
            _isDoliBusy = false;
            _gridGraphDC = new IG_gridGraphDC.ViewModel(view);
            test = new RelayCommand(o => coucou());
        }

        private bool ProcessSummary()
        {
            if (_gridGraphDC.NbCycle == 0)
                _gridGraphDC.NbCycle = 1;
            if (MessageBox.Show("Vous vous apprêtez à lancer  séquence de " + _gridGraphDC.DoliInputCollection.Count() * _gridGraphDC.NbCycle + " commandes. La durée des opérations est estimée à " + _gridGraphDC.DestPosSeriesValues.Last().X + " secondes", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
                //if (MessageBox.Show("Vous vous apprêtez à lancer une séquence de commandes. La durée des opérations est estimée à secondes", "",MessageBoxButtons.OKCancel) == DialogResult.OK)
                return true;
            else
                return false;
        }

        private void coucou()
        {
            Console.WriteLine(_gridGraphDC.DestPosSeriesValues);
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
                                   _doli.ComplexeCycle(_gridGraphDC.NbCycle, _gridGraphDC.DoliInputCollection);
                               else
                                   _isDoliBusy = false;
                               //await DoliMovement();
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
