using Doli.DoPE;
using System;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DauBe_WTF.Converters
{
    class MvmntToBackgroundColorConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var ManualCTRL = value as DoPE.CTRL?;
            switch (ManualCTRL)
            {
                case DoPE.CTRL.POS:
                    return  Brushes.DodgerBlue;
                case DoPE.CTRL.LOAD:
                    return Brushes.IndianRed;
                default:
                    return Brushes.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
