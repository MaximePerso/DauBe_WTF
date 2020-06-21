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
        // offset is here to make sure all the code is updated when changing this value, especially when the part assumes its position
        private double offsett = 20.0;
        // distance between the ball and the base of the machine after it was initially squiched
        private double squishedBall = 25;
        // to avoid relative displacement (in case user hit the emergency button), the position of the cross head at the end of the first pahse is stored in this variable.
        private double tempPos;
        private short MyTan;
        private Globals measures;
        public Edc MyEdc;
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
