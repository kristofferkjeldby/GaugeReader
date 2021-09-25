namespace GaugeReader.Gauge.Models
{
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Math.Models.Maps;

    public class Tick
    {
        public Angle Angle { get; set; }

        public int Offset { get; set; }

        public Tick Next { get; set; }

        public Tick Previous { get; set; }

        public double Intensity { get; set; }

        public Tick(Angle angle)
        {
            Angle = angle;
        }

        public AngleSpan AngleSpan
        {
            get
            {
                var startAngle = Angle - ((Angle - Previous.Angle) / 2);
                var endAngle = Angle + ((Next.Angle - Angle) / 2);
                return new AngleSpan(startAngle, endAngle);
            }
        }

        public double StartTickPoints { get; set; }

        public double EndTickPoints { get; set; }

        public double MiddleTickPoints { get; set; }
    }
}
