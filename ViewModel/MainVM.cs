using DauBe_WTF.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DauBe_WTF.ViewModel
{
    class MainVM : VMBase
    {
        public SubVM.DoliVM doli { get; } = new SubVM.DoliVM();
        public SubVM.GraphVM graph { get; } = new SubVM.GraphVM();
    }
}
