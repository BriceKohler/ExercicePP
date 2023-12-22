// <copyright file="DateAndBooleanToColorConverterTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExercicePPTest.ConvertersTest
{
    using System;
    using System.Windows.Media;

    /// <summary>
    /// Testing converter.
    /// </summary>
    internal class DateAndBooleanToColorConverterTest
    {
        /// <summary>
        /// Tests color converter.
        /// </summary>
        [Test]
        public void ColorFromDateAndDoneTest()
        {
            Assert.That(Colors.Red, Is.EqualTo(ExercicePP.Converters.DateAndBooleanToColorConverter.ColorFromDateAndDone(DateTime.Now.AddDays(-2), false)));
            Assert.That(Colors.Orange, Is.EqualTo(ExercicePP.Converters.DateAndBooleanToColorConverter.ColorFromDateAndDone(DateTime.Now.AddDays(2), false)));
            Assert.That(Colors.Lime, Is.EqualTo(ExercicePP.Converters.DateAndBooleanToColorConverter.ColorFromDateAndDone(DateTime.Now.AddDays(8), false)));

            Assert.That(Colors.Lime, Is.EqualTo(ExercicePP.Converters.DateAndBooleanToColorConverter.ColorFromDateAndDone(DateTime.Now.AddDays(-2), true)));
            Assert.That(Colors.Lime, Is.EqualTo(ExercicePP.Converters.DateAndBooleanToColorConverter.ColorFromDateAndDone(DateTime.Now.AddDays(2), true)));
            Assert.That(Colors.Lime, Is.EqualTo(ExercicePP.Converters.DateAndBooleanToColorConverter.ColorFromDateAndDone(DateTime.Now.AddDays(8), true)));
        }
    }
}
