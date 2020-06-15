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

namespace DauBe_WTF

{
    public class MainVM : VMBase
    {
        #region Fields
        #region Doli outputs
        private Dictionary<string, double> _doliData;
        private Double _doliTime;
        private Double _doliLoad;
        private Double _doliPosition;
        private Double _doliExtend;
        #endregion
        private Double _onDataTime;
        private Double _onDataLoad;
        private Double _onDataPosition;
        private Double _onDataExtend;
        private Double _tareTime;
        private Double _tareLoad;
        private Double _tarePosition;
        private Double _tareExtend;
        private Double _xMax;
        #endregion

        #region Properties
        #region Doli outputs
        public Dictionary<string,double> DoliData
        {
            get => _doliData;
            set 
            { _doliData = value; OnPropertyChanged("DoliData"); }
        }
        public double DoliTime
        {
            get => _doliData["Time"];
            set
            { _doliData["Time"] = value - _tareTime; OnPropertyChanged("DoliTime"); }
        }
        public double DoliLoad
        {
            get => _doliLoad;
            set
            { _doliLoad = value - _tareLoad; OnPropertyChanged("DoliLoad"); }
        }
        public double DoliPosition
        {
            get => _doliPosition;
            set
            { _doliPosition = value - _tarePosition; OnPropertyChanged("DoliPosition"); }
        }
        public double DoliExtend
        {
            get => _doliExtend;
            set
            { _doliExtend = value - _tareExtend; OnPropertyChanged("DoliExtend"); }
        }
        #endregion
        public double OnDataTime 
        { 
            get => _onDataTime;
            set
            {_onDataTime = value - _tareTime; OnPropertyChanged("OnDataTime");}
        }
        public double OnDataLoad
        {
            get => _onDataLoad;
            set
            {_onDataLoad = value - _tareLoad; OnPropertyChanged("OnDataLoad");}
        }
        public double OnDataPosition
        {
            get => _onDataPosition;
            set
            {_onDataPosition = value - _tarePosition; OnPropertyChanged("OnDataPosition");}
        }
        public double OnDataExtend
        {
            get => _onDataExtend;
            set
            {_onDataExtend = value - _tareExtend; OnPropertyChanged("OnDataExtend");}
        }
        public double TareTime
        {
            get => _tareTime;
            set
            { _tareTime = value; OnPropertyChanged("TareTime");}
        }
        public double TareLoad
        {
            get => _tareLoad;
            set
            {_tareLoad = value; OnPropertyChanged("TareLoad");}
        }
        public double TarePosition
        {
            get => _tarePosition;
            set
            { _tarePosition = value; OnPropertyChanged("TarePosition"); }
        }
        public double TareExtend
        {
            get => _tareExtend;
            set
            { _tareExtend = value; OnPropertyChanged("TareExtend"); }
        }
        public double XMax
        {
            get => _xMax;
            set
            { _xMax = value; OnPropertyChanged("XMax"); } }
        #endregion


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

            DoliData = new Dictionary<string, double>();
            DoliData.Add("Time", 0.0);
            DoliData.Add("Position", 0.0);
            DoliData.Add("Load", 0.0);
            DoliData.Add("Extend", 0.0);

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
