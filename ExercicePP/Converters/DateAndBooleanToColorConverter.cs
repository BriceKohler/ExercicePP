// <copyright file="DateAndBooleanToColorConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExercicePP.Converters
{
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    /// <summary>
    /// Converter to get a color from the task's delay and done status.
    /// </summary>
    public class DateAndBooleanToColorConverter : IMultiValueConverter
    {
        private static readonly int DayLimit = 7;

        /// <summary>
        /// Converter To colorize a background from a date and a boolean value.
        /// </summary>
        /// <param name="values">Delay and Done status.</param>
        /// <param name="targetType">SolidColorBrush type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="culture">Informations about culture.</param>
        /// <returns>A SolidColorBrush object.</returns>
        /// <exception cref="Exception">throws exception about conversion.</exception>
        public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var date = (DateTime)values[0];
                var done = (bool)values[1];
                Color col = ColorFromDateAndDone(date, done);

                return this.ColorOrBrush(col, targetType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Transforms Color to SolidColorBrush.
        /// </summary>
        /// <param name="c">Color.</param>
        /// <param name="targetType">SolidColorBrush Type.</param>
        /// <returns>SolidColorBrush.</returns>
        private object? ColorOrBrush(Color c, Type targetType)
        {
            if (targetType == typeof(Color))
            {
                return c;
            }
            else if (targetType == typeof(Brush))
            {
                return new SolidColorBrush(c);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets Color from Delay and done bolean.
        /// </summary>
        /// <param name="date">Date.</param>
        /// <param name="done">Boolean.</param>
        /// <returns>Color.</returns>
        public static Color ColorFromDateAndDone(DateTime date, bool done)
        {
            if (done || DateTime.Now.AddDays(DayLimit) < date)
            {
                return Colors.Lime;
            }
            else if (DateTime.Now > date)
            {
                return Colors.Red;
            }
            else
            {
                return Colors.Orange;
            }
        }

        /// <summary>
        /// Not Implemented => We cannot reverse from color to couple Date/Bool.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="targetTypes">TargetType.</param>
        /// <param name="parameter">Parmeter.</param>
        /// <param name="culture">Culture.</param>
        /// <returns>Date and Done.</returns>
        /// <exception cref="NotImplementedException">We cannot reverse from color to couple Date/Bool.</exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
}
