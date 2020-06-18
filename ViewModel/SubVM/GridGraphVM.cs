using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DauBe_WTF.Utility;

namespace DauBe_WTF.ViewModel.SubVM
{
    public partial class GridGraphVM : VMBase
    {
        private double _nbInput;
        public double NbInput
        {
            get => _nbInput;
            set
            {
                _nbInput = value; OnPropertyChanged("NbInput");
            }
        }
    }
}
