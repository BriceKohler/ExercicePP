using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ExercicePP.Converters
{
    class DateAndBooleanToColorConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converter To colorize a background from a date and a boolean value.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var date = (DateTime)values[0];
                var done = (bool)values[1];

                if (done || DateTime.Now.AddDays(7) < date)
                    return ColorOrBrush(Colors.Lime, targetType);
                else if (DateTime.Now > date)
                    return ColorOrBrush(Colors.Red, targetType);
                else return ColorOrBrush(Colors.Orange, targetType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private object ColorOrBrush(Color c, Type targetType)
        {
            if (targetType == typeof(Color))
                return c;
            else if (targetType == typeof(Brush))
                return new SolidColorBrush(c);
            else
                return null;
        }

        /// <summary>
        /// Not Implemented => we cannot reverse from color to couple Date/Bool
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        { 
                throw new NotImplementedException();
        }


    }
}
