using System;
using System.Linq;

namespace KingLemurJulian.Core
{
    public class FancyDateTimeFormatProvider : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;

            return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (!(arg is DateTime)) throw new NotSupportedException();

            var dt = (DateTime)arg;

            string suffix;

            if (new[] { 11, 12, 13 }.Contains(dt.Day))
            {
                suffix = "th";
            }
            else if (dt.Day % 10 == 1)
            {
                suffix = "st";
            }
            else if (dt.Day % 10 == 2)
            {
                suffix = "nd";
            }
            else if (dt.Day % 10 == 3)
            {
                suffix = "rd";
            }
            else
            {
                suffix = "th";
            }

            return $"{dt.Day}{suffix} of {arg:MMMM} {arg:yyyy}";
        }
    }
}
