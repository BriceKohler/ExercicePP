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
    public class DateAndBooleanToColorConverter : IMultiValueConverter
    {

        static int DayLimit = 7;
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
                Color col = ColorFromDateAndDone(date, done);

                return ColorOrBrush(col, targetType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Color ColorFromDateAndDone(DateTime date, bool done)
        {
            if (done || DateTime.Now.AddDays(DayLimit) < date)
                return Colors.Lime;
            else if (DateTime.Now > date)
                return Colors.Red;
            else return Colors.Orange;

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
