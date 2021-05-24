namespace GaugeReader.Models.Angles
{
    using GaugeReader.Extensions;
    using System;
    using System.Drawing;
    using System.Linq;

    public class Convolution
    {
        public double[] Map { get; set; }

        public Convolution(Bitmap image)
        {
            Map = new double[image.Width];
            var stepSize = Constants.PI2 / Constants.AngleResolution;

            for (int i = 0; i < image.Width; i++)
            {
                Map[i] = image.GetPixel(i, 0).GetBrightness();
            }

            Map = Map.Normalize();
        }

        public Convolution(double[] map)
        {
            Map = map.Normalize();
        }

        public Bitmap ToImage(int height = 16)
        {
            var image = new Bitmap(Map.Length, height);

            for (int x = 0; x < image.Width; x++)
            {
                var intensity = (byte.MaxValue * Map[x]).ToInt();
                var color = Color.FromArgb(byte.MaxValue, intensity, intensity, intensity);

                for (int y = 0; y < height; y++)
                {
                    image.SetPixel(x, y, color);
                }
            }

            return image;
        }

        public double Match(Convolution other)
        {
            var intensity = 0d;

            if (Map.Length != other.Map.Length)
            {
                throw new Exception();
            }

            for (int x = 0; x < Map.Length; x++)
            {
                var exceptedValue = other.Map[x];
                var actualValue = Map[x];

                var diff = Math.Pow(Math.Abs(actualValue - exceptedValue), 1);

                intensity -= diff;
            }

            return intensity;
        }

        public double Symmetric()
        {
            var intensity = 0d;

            for (int x = 0; x < Map.Length; x++)
            {
                var exceptedValue = Map[Map.Length - 1 - x];
                var actualValue = Map[x];

                var diff = Math.Pow(Math.Abs(actualValue - exceptedValue), 1);

                intensity -= diff;
            }

            return intensity;
        }

        public double Dark()
        {
            return -Map.Sum();
        }
    }
}
