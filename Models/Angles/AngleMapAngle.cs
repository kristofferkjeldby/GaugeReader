using GaugeReader.Extensions;
using System.Drawing;

namespace GaugeReader.Models.Angles
{
    public class AngleMapAngle : Angle
    {
        public AngleMapAngle(Angle angle, double intensity, double relativeIntensity) : base(angle.Value)
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
                var intensity = (byte.MaxValue * RelativeIntensity.Limit(0, 1)).ToInt();
                return Color.FromArgb(byte.MaxValue, intensity, intensity, intensity);
            }
        }
    }
}
