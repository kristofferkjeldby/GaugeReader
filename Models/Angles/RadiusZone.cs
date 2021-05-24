namespace GaugeReader.Models.Angles
{
    using AForge.Imaging;
    using GaugeReader.Extensions;
    using GaugeReader.Models.Lines;
    using System;

    public class RadiusZone
    {
        public RadiusZone(double start, double end)
        {
            Start = start;
            End = end;
        }

        public double Start { get; set; }

        public double End { get; set; }

        public bool Contains(Line line, double radiusZone)
        {
            var relativeRadius = Math.Abs(line.R) / radiusZone;

            return (relativeRadius >= Start && relativeRadius <= End); 
        }

        public bool Contains(HoughLine line, double radiusZone)
        {
            return Contains(line.ToLine(), radiusZone);
        }
    }
}
