namespace GaugeReader.Gauge.Models
{
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Math.Models.Lines;

    public class Hand
    {
        public Line Line1 { get; set; }
        
        public Line Line2 { get; set; }

        public Angle Angle { get; set; }
    }
}
