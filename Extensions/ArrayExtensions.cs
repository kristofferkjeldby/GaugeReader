namespace GaugeReader.Extensions
{
    using System.Linq;

    public static class ArrayExtensions
    {
        public static double[] Normalize(this double[] array)
        {
            var max = array.Max();
            var min = array.Min();
            var span = max - min;

            for (int step = 0; step < array.Length; step++)
            {
                array[step] = ((array[step] - min) / span).Limit(0, 1);
            }

            return array;
        }
    }
}
