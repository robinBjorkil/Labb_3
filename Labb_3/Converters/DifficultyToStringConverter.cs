using Labb_3.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Labb_3.Converters
{
    public class DifficultyToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var difficulty = (Difficulty)value;
            return (int)difficulty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var difficulty = (Difficulty)value;
            return difficulty.ToString();
        }
    }
}
