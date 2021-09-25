namespace GaugeReader.Extensions
{
    using System.Linq;

    public static class ArrayExtensions
    {
        public static void Normalize(this double[] array, double min = -1, double max = 1)
        {
            var oldMax = array.Max();
            var oldMin = array.Min();

            var oldSpan = oldMax - oldMin;      
            var newSpan = max - min;

            var factor = newSpan / oldSpan;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = ((array[i] - oldMin) * factor) + min;
            }
        }
    }
}
