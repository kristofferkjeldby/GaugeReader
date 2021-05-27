namespace GaugeReader.Math.Models.Lines
{
    using AForge.Imaging;
    using GaugeReader.Extensions;
    using GaugeReader.Math.Models.Angles;

    public class RadialLine : Line
    {
        public RadialLine(Angle angle) : base(0, angle.ToNormal())
        {

        }

        public static implicit operator RadialLine(Angle a) => new RadialLine(a);

        public static explicit operator RadialLine(HoughLine l)
        {
            return new RadialLine(new Angle(l.Theta.ToRadians()));
        }
    }
}
