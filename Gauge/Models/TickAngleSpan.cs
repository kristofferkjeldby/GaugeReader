namespace GaugeReader.Gauge.Models
{
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Math.Models.Maps;
    using System.Collections.Generic;
    using System.Linq;

    public class TickAngleSpan : AngleSpan
    {
        public TickAngleSpan(List<Tick> ticks) : base(ticks.First().Angle, ticks.Last().Angle)
        {
            this.Ticks = ticks;
        }

        public Map Map { get; set; }

        public List<Tick> Ticks { get; set; }

        public List<Tick> Middle()
        {
            return Ticks.Skip(1).Take(Ticks.Count() - 2).ToList();
        }

        public Tick First()
        {
            return Ticks.First();
        }

        public Tick Last()
        {
            return Ticks.Last();
        }

        public double Points
        {
            get
            {
                var points = 0d;
                points += First().StartTickPoints;
                points += Last().EndTickPoints;
                points += Middle().Sum(e => e.MiddleTickPoints);
                return points / (Ticks.Count);
            }
        }
    }
}
