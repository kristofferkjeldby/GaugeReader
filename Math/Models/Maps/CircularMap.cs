namespace GaugeReader.Math.Models.Maps
{
    using System;

    public class CircularMap : Map
    {
        public CircularMap(Map map) : base(map)
        {
        }

        public CircularMap(int capacity) : base(capacity)
        {
        }

        public override int GetIndex(int step)
        {

            if (step < 0)
                return Length - (Math.Abs(step) % Length);

            return step % Length;
        }
    }
}
