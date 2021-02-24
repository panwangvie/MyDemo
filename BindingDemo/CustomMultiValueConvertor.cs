using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BindingDemo
{
    public class CustomMultiValueConvertor : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (0.Equals(values[0]))
                {
                    return "--";
                }
                else
                {
                     
                    string result = values[1].ToString()+ values[0];
                    return result;
                }
            }
            catch (Exception ex)
            {
                return "--";
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as string).Split(' ');
        }
    }
}
