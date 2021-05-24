namespace GaugeReader.Extensions
{
    using System;

    public static class DoubleExtensions
    {
        public static double ToRadians(this double degrees)
        {
            return degrees / 360 * Constants.PI2;
        }

        public static bool FuzzyEquals(this double value, double otherValue)
        {
            return Math.Abs(value - otherValue) < Constants.Delta;
        }

        public static bool IsValid(this double value)
        {
            return !double.IsInfinity(value) && !double.IsNaN(value);
        }

        public static double Limit(this double a, double min, double max)
        {
            if (double.IsPositiveInfinity(a)) return max;
            if (double.IsNaN(a)) return min;
            if (double.IsNegativeInfinity(a)) return min;

            if (a < min)
                return min;
            if (a > max)
                return max;
            return a;
        }

        public static int ToInt(this double a)
        {
            return (int)Math.Round(a.Limit(int.MinValue, int.MaxValue), 0, MidpointRounding.AwayFromZero);
        }
    }
}
