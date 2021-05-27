namespace GaugeReader.Convolutions.Models
{
    using GaugeReader.Extensions;
    using System;
    using System.Drawing;
    using System.Linq;

    public class Convolution
    {
        private double[] map { get; set; }

        public int Length => map.Length;

        public Convolution(Bitmap image)
        {
            map = new double[image.Width];

            for (int i = 0; i < image.Width; i++)
            {
                map[i] = image.GetPixel(i, 0).GetBrightness();
            }

            map = map.Normalize();
        }

        public double this[int index]
        {
            get { return map[index]; }
            set { map[index] = value; }
        }

        public Convolution(double[] map)
        {
            this.map = map.Normalize();
        }

        public Bitmap ToImage(int height = 16)
        {
            var image = new Bitmap(map.Length, height);

            for (int x = 0; x < image.Width; x++)
            {
                var intensity = (byte.MaxValue * map[x]).ToInt();
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
            var totalDifferent = 0d;

            var steps = Math.Min(map.Length, other.Length);

            for (int step = 0; step < steps; step++)
            {
                totalDifferent += Math.Abs(this[step] - other[step]);
            }

            return 1 - (totalDifferent / steps);
        }

        public double Symmetric()
        {
            var totalDifference = 0d;

            var steps = this.Length;

            for (int step = 0; step < steps; step++)
            {
                totalDifference += Math.Abs(this[step] - this[steps - step - 1]);
            }

            return 1 - (totalDifference / steps);
        }

        public double Brightness()
        {
            return map.Sum() / map.Length;
        }

        public double Dark()
        {
            return 1 - Brightness();
        }
    }
}
