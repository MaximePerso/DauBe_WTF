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

        public AutoPos()
        {
            InitializeComponent();
            this.DataContext = new AutoPosVM();
        }

        #region TBC
        public AutoPos(Edc edc, short myTan, Globals measures)
        {
            InitializeComponent();
            this.DataContext = new AutoPosVM();
            MyEdc = edc;
            MyTan = myTan;
            this.measures = measures;
        }

        private async void btnBall_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("La balle est-elle en place ?", "déconne pas Maurice", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // compress the ball
                double lim = -100.0;
                double destination = -1000;
                moveToDest(DoPE.CTRL.POS, destination, 100, lim, 1);
                await Task.Run(() =>
                {
                    while (Math.Abs(measures.OnDataLoad) < Math.Abs(lim) * 0.8)
                    {
                        load.Dispatcher.Invoke(new Action(() => load.Content += "."));
                        System.Threading.Thread.Sleep(250);
                    }
                });

                // realease the ball
                lim += 250;
                destination = measures.OnDataPosition + offsett;
                moveToDest(DoPE.CTRL.POS, destination, 100, lim, 1);
                Console.WriteLine(Math.Abs(measures.OnDataPosition));
                Console.WriteLine(Math.Abs(destination));
                Console.WriteLine(Math.Abs(Math.Abs(measures.OnDataPosition) - Math.Abs(destination)));
                await Task.Run(() =>
                {
                    while (Math.Abs(Math.Abs(measures.OnDataPosition) - Math.Abs(destination)) > 0.2)
                    {
                        load.Dispatcher.Invoke(new Action(() => load.Content += "."));
                        System.Threading.Thread.Sleep(250);
                    }
                });
                tempPos = measures.OnDataPosition;
                btnBall.Visibility = Visibility.Hidden;
                btnRemoveBall.Visibility = Visibility.Visible;
                lblRemoveBall.Visibility = Visibility.Visible;
                load.Content = "Envelevez la balle";
            }


        }

        private void moveToDest(DoPE.CTRL controlMove, double destination, double velLim = 1.0, double limit = 0.0, int flag = 0)
        {
            DoPE.ERR error;
            // in this command, the limit and the destination have been inverted so the x-head is piloted using movement speed instead of loading speed (much
            // faster). However, it means that if the limit load is reached, OnPosMsg will proc instead of OnLPosMsg
            error = MyEdc.Move.PosExt(controlMove, velLim, DoPE.LIMITMODE.ABSOLUTE, destination, DoPE.CTRL.LOAD, limit, DoPE.DESTMODE.APPROACH, ref MyTan);

        }

        private async void btnRemoveBall_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("La balle enlevée ?", "déconne pas Maurice", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // set final position
                double lim = -100.0;
                double destination = tempPos - (offsett + squishedBall);
                moveToDest(DoPE.CTRL.POS, destination, 5, lim, 1);
                double curpos = measures.OnDataPosition;
                double distance = Math.Abs(curpos - destination);
                await Task.Run(() =>
                {
                    System.Threading.Thread.Sleep(25);
                    while (measures.OnDataPosition != curpos)
                    {
                        curpos = measures.OnDataPosition;
                        lblRemoveBall.Dispatcher.Invoke(new Action(() => lblRemoveBall.Content = Math.Round(((1 - Math.Abs(Math.Abs(curpos) - Math.Abs(destination)) / distance) * 100), 1).ToString() + "%"));
                        System.Threading.Thread.Sleep(25);
                    }
                });
                if (Math.Round(measures.OnDataPosition, 2) == Math.Round(destination, 2))
                {
                    MessageBox.Show("Mise en place réussie !");
                    btnRemoveBall.Visibility = Visibility.Hidden;
                    lblRemoveBall.Content = "Mise en place réussie !";
                }
            }
        }
        #endregion
    }
}
