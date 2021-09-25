namespace GaugeReader.Transformations
{
    using AForge.Math;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Math.Models.Maps;
    using System.Collections.Generic;
    using System.Linq;

    public class FrequencyTransformation
    {
        private List<Frequency> frequencies;

        public void ProcessMap(Mapable mapable)
        {
            var map = mapable.ToMap();

            var complexMap = map.Select(element => new Complex(element, 0.0)).ToArray();

            FourierTransform.DFT(complexMap, FourierTransform.Direction.Forward);

            frequencies = new List<Frequency>();

            var width = (map.Width == 0) ? Constants.PI2 : map.Width.Value;

            for (int i = Constants.MinTicks; i < complexMap.Length / 2; i++)
            {
                var waveLength = width / i;
                var phase = complexMap[i].Phase % waveLength;
                var intensity = complexMap[i].Magnitude * 2;

                var frequency = new Frequency(waveLength, phase, intensity);
                frequencies.Add(frequency);
            }
        }

        public List<Frequency> GetMostInvensiveFrequencies(int count)
        {
            return frequencies?.OrderByDescending(f => f.Intensity).Take(count).ToList();
        }
    }
}
