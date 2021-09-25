namespace GaugeReader.Math.Models.Angles
{
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Math.Models.Maps;
    using System;

    public class Frequency : Mapable
    {
        public Angle WaveLength { get; set; }

        public Angle Phase { get; set; }

        public double Intensity { get; set; }

        public Frequency(Angle waveLength, Angle phase, double intensity)
        {
            WaveLength = waveLength;
            Phase = phase;
            Intensity = intensity;
        }

        public override Map ToMap()
        {
            var map = new Map(Constants.AngleResolution);
            var stepSize = Constants.AngleStepSize;
            var frequency = Constants.PI2 / WaveLength;

            for (int x = 0; x < map.Length; x++)
            {
                for (int y = 0; y < Constants.ImageMapHeight; y++)
                {
                    var value = Math.Cos((x * stepSize * frequency + Phase));
                    map[x] = value;
                }
            }

            return map;
        }
    }
}
