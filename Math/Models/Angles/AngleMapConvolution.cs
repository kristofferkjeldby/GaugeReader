namespace GaugeReader.Math.Models.Angles
{
    using GaugeReader.Extensions;
    using System.Drawing;

    public class AngleMapConvolution
    {
        public AngleMapConvolution(Angle angle)
        {
            Angle = angle;
        }

        public Angle Angle { get; set; }

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
    }
}
