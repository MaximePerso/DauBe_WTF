using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using RealTimeGraphX.WPF;
using RealTimeGraphX.DataPoints;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Windows.Threading;
using System.Windows.Forms;
using DauBe_WTF.Utility;
using System.Windows.Input;

namespace DauBe_WTF.ViewModel.SubVM

{
    public class GraphVM : VMBase
    {
        #region Fields
        #endregion

        #region Properties
        #endregion


        public WpfGraphController<DoubleDataPoint, DoubleDataPoint> MultiController { get; set; }


        public GraphVM()
        {
            CommandInitialisation();

            MultiController = new WpfGraphController<DoubleDataPoint, DoubleDataPoint>();
            MultiController.Range.MaximumX = 600; // en secondes
            MultiController.Range.MinimumY = 0;
            MultiController.Range.MaximumY = 1080;
            MultiController.Range.AutoY = true;


            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Position",
                Stroke = Colors.Red,
                StrokeThickness = 4
            });

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Load",
                Stroke = Colors.Green,
                StrokeThickness = 4
            });

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Extend",
                Stroke = Colors.Blue,
                StrokeThickness = 4
            });


        }

        public void UpdateGraph(double time, double position, double load, double extend)
        {
            List<DoubleDataPoint> yy = new List<DoubleDataPoint>()
                    {
                        position,
                        load,
                        extend
                    };

            List<DoubleDataPoint> xx = new List<DoubleDataPoint>()
                    {
                        time,
                        time,
                        time
                    };

            MultiController.PushData(xx, yy);

            Thread.Sleep(30);


        }

        public ICommand DoliOn;
        private void CommandInitialisation()
        {
            //DoliOn = new RelayCommand(o => )
        }
    }
}
