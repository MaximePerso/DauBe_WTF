using System;
using System.Collections.Generic;

namespace DauBe_WTF
{
    public class Globals
    {
        public Dictionary<String, List<Double>> Value = new Dictionary<string, List<Double>>();
        public List<Double> time = new List<Double>();
        public List<Double> load = new List<Double>();
        public List<Double> position = new List<Double>();
        public List<Double> extend = new List<Double>();

        private Double onDataTime;
        private Double onDataLoad;
        private Double onDataPosition;
        private Double onDataExtend;
        private Double tareTime;
        private Double tareLoad;
        private Double tarePosition;
        private Double tareExtend;
        private Double xMax;

        public double OnDataTime { get => onDataTime; set => onDataTime = value - tareTime; }
        public double OnDataLoad { get => onDataLoad; set => onDataLoad = value - tareLoad; }
        public double OnDataPosition { get => onDataPosition; set => onDataPosition = value - tarePosition; }
        public double OnDataExtend { get => onDataExtend; set => onDataExtend = value - tareExtend; }
        public double TareTime { get => tareTime; set => tareTime = value; }
        public double TareLoad { get => tareLoad; set => tareLoad = value; }
        public double TarePosition { get => tarePosition; set => tarePosition = value; }
        public double TareExtend { get => tareExtend; set => tareExtend = value; }
        public double XMax { get => xMax; set => xMax = value; }


        public Globals()
        {
            tareTime = 0;
            tarePosition = 0;
            tareLoad = 0;
            tareExtend = 0;
            onDataTime = 0;
            onDataLoad = 0;
            onDataPosition = 0;
            onDataExtend = 0;
            Value.Add("Time", time);
            Value.Add("Position", position);
            Value.Add("Load", load);
            Value.Add("Extend", extend);
            xMax = 0;
        }
    }
}
