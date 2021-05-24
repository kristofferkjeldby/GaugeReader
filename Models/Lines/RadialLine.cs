namespace GaugeReader.Models.Lines
{
    using GaugeReader.Models.Angles;

    public class RadialLine : Line
    {
        public RadialLine(Angle angle) : base(0, angle.ToNormal())
        {

        }

        public static implicit operator RadialLine(Angle a) => new RadialLine(a);
    }
}
