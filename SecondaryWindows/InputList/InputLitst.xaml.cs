using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InteractiveGraphUserControl;
using InteractiveGraphUserControl.Utility;

namespace DauBe_WTF.SecondaryWindows.InputList
{
    /// <summary>
    /// Interaction logic for InputLitst.xaml
    /// </summary>
    public partial class InputLitst : Window
    {
        private ViewModel.SubVM.DoliVM _doli;

        public InputLitst()
        {
            InitializeComponent();
            this.DataContext = new InputListVM(_doli);
        }

    }
}
