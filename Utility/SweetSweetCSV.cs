﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace DauBe_WTF.Utility
{
    public class SweetSweetCSV
    {
        private string _exePath = string.Empty;
        private String _todaysdate = string.Empty;

        public SweetSweetCSV()
        {

        }

        public void WriteCSV(List<double> time, List<double> position, List<double> load, List<double> extend, string command)
        {
            CheckFolder();
            string timeNow = DateTime.Now.ToString("HH_mm");
            using (var writer = new StreamWriter(_exePath + "\\Records\\" + _todaysdate + "\\" + command + "_" + timeNow + ".csv"))
            //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                //csv.Configuration.Delimiter = ";";
                var MyList = Matrix(time, position, load, extend);
                foreach (var item in MyList)
                {
                    writer.WriteLine(item);
                }
                writer.Flush();
            }
        }

        private void CheckFolder()
        {
            _exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
            if (Directory.Exists(_exePath + "\\Records"))
            {
                if (!Directory.Exists(_exePath + "\\Records\\" + _todaysdate))
                    Directory.CreateDirectory(_exePath + "\\Records\\" + _todaysdate);
            }
            else
            {
                    Directory.CreateDirectory(_exePath + "\\Records\\" + _todaysdate);
            }
        }

        private List<string> Matrix(List<double> time, List<double> position, List<double> load, List<double> extend)
        {
            List<string> MyList = new List<string>{"Time;Position;Load;Extend"};
            for (int i =0; i<position.Count(); i++)
            {
                MyList.Add(time[i].ToString() + ";" + position[i].ToString() + ";" + load[i].ToString() + ";" + extend[i].ToString());
                //MyList.Add(new List<string> { time[i].ToString(), position[i].ToString(), load[i].ToString(), extend[i].ToString() + "\n" });
            }
            return MyList;
        }
    }
}
