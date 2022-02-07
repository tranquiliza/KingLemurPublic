using System;
using System.Globalization;

namespace KingLemurJulian.Core.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToInvarientStringWith2Decimals(this double input)
            => Math.Round(input, 2).ToString(CultureInfo.InvariantCulture);
    }
}
