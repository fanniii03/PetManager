using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetManager
{
    class BirthDateToAgeConverter : IValueConverter
    {
        // Convert: forrás --> cél
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return (DateTime.Today - date).Days / 365;
        }

        // ConvertBack: cél --> forrás
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
