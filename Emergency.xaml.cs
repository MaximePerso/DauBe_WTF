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

namespace DauBe_WTF
{
    /// <summary>
    /// Interaction logic for Emergency.xaml
    /// </summary>
    public partial class Emergency : Window
    {
        private Edc MyEdc;
        private short MyTan;
        public Emergency(Edc edc, short tan)
        {
            InitializeComponent();
            MyEdc = edc;
            MyTan = tan;
        }

        private void stop()
        {
            DoPE.ERR error = MyEdc.Move.Halt(DoPE.CTRL.POS, ref MyTan);
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            stop();
        }
    }
}
