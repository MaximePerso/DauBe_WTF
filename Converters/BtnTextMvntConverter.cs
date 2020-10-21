using Doli.DoPE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DauBe_WTF.Converters
{
    class BtnTextMvntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var ManualCTRL = value as DoPE.CTRL?;
            switch (ManualCTRL)
            {
                case DoPE.CTRL.POS:
                    return "Apply \n Displacement";
                case DoPE.CTRL.LOAD:
                    return "Apply \n Pressure";
                default:
                    return "Go";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
