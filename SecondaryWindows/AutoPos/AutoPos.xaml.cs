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
using Doli.DoPE;

namespace DauBe_WTF.SecondaryWindows.AutoPos
{
    /// <summary>
    /// Interaction logic for Calibration.xaml
    /// </summary>
    public partial class AutoPos : Window
    {
        private ViewModel.SubVM.DoliVM _doli;
        private CircularProgressBar.CPBVM _pg;

        public AutoPos()
        {
            InitializeComponent();
            this.DataContext = new AutoPosVM(_pg,_doli);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}
