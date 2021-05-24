namespace GaugeReader.Extensions
{
    using System;

    public static class FloatExtensions
    {
        public static bool FuzzyEquals(this float value, float otherValue)
        {
            return Math.Abs(value - otherValue) < Constants.Delta;
        }

        public static bool IsValid(this float value)
        {
            return !float.IsInfinity(value) && !float.IsNaN(value);
        }

        public static float Limit(this float a, float min, float max)
        {
            if (float.IsPositiveInfinity(a)) return max;
            if (float.IsNaN(a)) return min;
            if (float.IsNegativeInfinity(a)) return min;

            if (a < min)
                return min;
            if (a > max)
                return max;
            return a;
        }

        public static int ToInt(this float a)
        {
            return (int)Math.Round(a.Limit(int.MinValue, int.MaxValue), 0);
        }
    }
}
