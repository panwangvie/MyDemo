using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MyPing
{
 
    public class BgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value!=null&&value.ToString().Contains("不"))
            {
                Brush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#44BB8C"));
                return brush;
            }
           return  new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E6E6E6"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
