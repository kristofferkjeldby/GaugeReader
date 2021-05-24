namespace GaugeReader.Extensions
{
    using AForge.Math;
    using System.Drawing;
    using System.Linq;

    public static class ArrayExtensions
    {
        public static Bitmap ToImage(this Complex[] array, int height = 16)
        {
            var maxMagnitude = array.OrderByDescending(c => c.Magnitude).First().Magnitude;

            var image = new Bitmap(array.Length, height);

            for (int x = 0; x < image.Width; x++)
            {
                var relativeIntensity = array[x].Magnitude / maxMagnitude;
                var intensity = ((byte.MaxValue) * relativeIntensity).ToInt();
                var color = Color.FromArgb(byte.MaxValue, intensity, intensity, intensity);

                for (int y = 0; y < height; y++)
                {
                    image.SetPixel(x, y, color);
                }
            }

            return image;
        }

        public static Complex[] ToComplex(this double[] array)
        {
            var maxIntensity = array.Max();

            var complex = new Complex[array.Length];

            for (int x = 0; x < array.Length; x++)
            {
                complex[x] = new Complex(array[x], 0);
            }

            return complex;
        }

        public static double[] Normalize(this double[] array, int skip = 0)
        {
            var max = array.Skip(skip).Max();
            var min = array.Skip(skip).Min();
            var span = max - min;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = ((array[i] - min) / span).Limit(0, 1);
            }

            return array;
        }
    }
}
