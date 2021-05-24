namespace GaugeReader.Extensions
{
    using AForge.Imaging;
    using GaugeReader.Models.Angles;
    using GaugeReader.Models.Lines;

    public static class HoughLineExtensions
    {
        public static Line ToLine(this HoughLine houghLine)
        {
            return new Line(houghLine.Radius, new Angle(houghLine.Theta.ToRadians()));
        }
    }
}
