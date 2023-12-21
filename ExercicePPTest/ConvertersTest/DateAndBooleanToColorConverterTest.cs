using ExercicePP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ExercicePPTest.ConvertersTest
{
    internal class DateAndBooleanToColorConverterTest
    {
        public void ColorFromDateAndDoneTest(DateTime date, bool done, Color color)
        { 
            Assert.That(Colors.Red,  Is.EqualTo(ExercicePP.Converters.DateAndBooleanToColorConverter. ColorFromDateAndDone(DateTime.Now.AddDays(-2),false)));
            Assert.That(Colors.Orange,  Is.EqualTo(ExercicePP.Converters.DateAndBooleanToColorConverter. ColorFromDateAndDone(DateTime.Now.AddDays(2),false)));
            Assert.That(Colors.Lime,  Is.EqualTo(ExercicePP.Converters.DateAndBooleanToColorConverter. ColorFromDateAndDone(DateTime.Now.AddDays(8),false)));      
            
            Assert.That(Colors.Lime,  Is.EqualTo(ExercicePP.Converters.DateAndBooleanToColorConverter. ColorFromDateAndDone(DateTime.Now.AddDays(-2),true)));
            Assert.That(Colors.Lime,  Is.EqualTo(ExercicePP.Converters.DateAndBooleanToColorConverter. ColorFromDateAndDone(DateTime.Now.AddDays(2),true)));
            Assert.That(Colors.Lime,  Is.EqualTo(ExercicePP.Converters.DateAndBooleanToColorConverter. ColorFromDateAndDone(DateTime.Now.AddDays(8),true)));


        }
    }
}
