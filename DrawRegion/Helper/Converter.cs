using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace DrawRegion.Helper
{
    public class DoubleFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double d = (double)value;
            return Math.Round(d);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DoubleRadioConverter : IValueConverter
    {
        public double RadioWidth { get; set; }
        public double RadioHeight { get; set; }

        public DoubleRadioConverter()
        {
            RadioWidth = 656;
            RadioHeight = 488;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double d = (double)value;
            bool isWidth = (bool)parameter;

            if (isWidth)
                return d * RadioHeight / RadioWidth;
            else
                return d * RadioWidth / RadioHeight;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
