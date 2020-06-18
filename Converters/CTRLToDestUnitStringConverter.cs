using Doli.DoPE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DauBe_WTF.Converters
{
    class CTRLToDestUnitStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var ManualCTRL = value as DoPE.CTRL?;
            switch (ManualCTRL)
            {
                case DoPE.CTRL.POS:
                    return "Destination (mm)";
                case DoPE.CTRL.LOAD:
                    return "Destination (N)";
                default:
                    return "Destination (?)";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
