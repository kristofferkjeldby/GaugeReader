namespace GaugeReader.Models.Angles
{
    using GaugeReader.Extensions;
    using System;
    using System.Drawing;


    public class AngleMapFrequency : Frequency
    {
        public AngleMapFrequency(Angle waveLength, Angle phase, double magnitude, double intensity, double relativeIntensity) : base(waveLength, phase, magnitude)
        {
            Intensity = intensity;
            RelativeIntensity = relativeIntensity;
        }

        public double Intensity { get; set; }

        public double RelativeIntensity { get; set; }

        public Color Color
        {
            get
            {
                var intensity = (byte.MaxValue * RelativeIntensity).ToInt();
                return Color.FromArgb(byte.MaxValue, intensity, intensity, intensity);
            }
        }

        public Bitmap ToFrequencyImage(int height = 16)
        {
            var image = new Bitmap(Constants.AngleResolution, height);
            var stepSize = Constants.PI2 / Constants.AngleResolution;

            for (int x = 0; x < image.Width; x++)
            {
                var f = Constants.PI2 / WaveLength;
                var i = (x + 1) * stepSize;


                var intensity = (RelativeIntensity * (byte.MaxValue * (Math.Cos(i * f + Phase) + 1) / 2d)).ToInt();

                try
                {

                    var color = Color.FromArgb(byte.MaxValue, intensity, intensity, intensity);

                    for (int y = 0; y < height; y++)
                    {
                        image.SetPixel(x, y, color);
                    }
                }
                catch
                { }
            }

            return image;
        }
    }
}
