namespace GaugeReader.Gauge.Models
{
    using GaugeReader.Images.Models;
    using GaugeReader.Math.Models.Angles;
    using System.Collections.Generic;

    public class Gauge
    {
        public Gauge()
        {
            Hand = new Hand();
            Ticks = new List<Tick>();
        }

        public Hand Hand { get; set; }

        public List<Tick> Ticks { get; set; }

        public Crop Location {get; set; }

        public Angle SymmetriAxis { get; set; }

        public AngleSpan TicksAngleSpan { get; set; }
    }
}
