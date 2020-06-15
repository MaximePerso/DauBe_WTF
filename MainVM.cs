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

namespace DauBe_WTF

{
    public class MainVM
    {
        public WpfGraphController<DoubleDataPoint, DoubleDataPoint> MultiController { get; set; }

        public Globals measures = new Globals();

        public MainVM()
        {
            MultiController = new WpfGraphController<DoubleDataPoint, DoubleDataPoint>();
            MultiController.Range.MinimumY = 0;
            MultiController.Range.MaximumY = 1080;
            MultiController.Range.AutoY = true;

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Position",
                Stroke = Colors.Red,
            });

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Load",
                Stroke = Colors.Green,
            });

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Extend",
                Stroke = Colors.Blue,
            });

        }


        public void UpdateValues(double time, double position, double load, double extend)
        {
            measures.time.Add(time - measures.TareTime);
            measures.position.Add(position);
            measures.load.Add(load);
            measures.extend.Add(extend);
            Console.WriteLine("pouet");

            List<DoubleDataPoint> yy = new List<DoubleDataPoint>()
                    {
                        position,
                        load,
                        extend
                    };

            double x = measures.time.Last();
            List<DoubleDataPoint> xx = new List<DoubleDataPoint>()
                    {
                        time,
                        time,
                        time
                    };

            MultiController.PushData(xx, yy);

            Thread.Sleep(30);

            //UpdateEnabledSequencialPartToTrue();

        }


    }
}
