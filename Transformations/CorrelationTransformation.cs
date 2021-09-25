namespace GaugeReader.Transformations
{
    using GaugeReader.Math.Models.Maps;
    using System.Collections.Generic;
    using System.Linq;

    public class CorrelationTransformation
    {
        private Map map;

        private Map correlationMap;

        private List<Correlation> correlations;


        public CorrelationTransformation(Map map)
        {
            this.map = map;
        }

        public void ProcessSample(Map sample)
        {
            correlationMap = new Map(map.Length);
            correlations = new List<Correlation>(map.Length);


            for (int x = 0; x < map.Length; x ++)
            {
                correlationMap[x] = GetCorrelationIntensity(x, sample) / sample.Length;
            }

            for (int x = 0; x < map.Length; x++)
            {
                correlations.Add(new Correlation(x * map.StepSize, map, sample, correlationMap[x]));
            }

        }

        private double GetCorrelationIntensity(int lag, Map sample)
        {
            var intensity = 0d; 

            for (int x = 0; x < sample.Length; x++)
            {
                var expected = sample[x];
                var actual = map[(lag + x) % map.Length];
                intensity += expected * actual;
            }

            return intensity;
        }

        public List<Correlation> GetMostIntensiveCorrelations(int count)
        {
            return correlations?.OrderByDescending(c => c.Intensity).Take(count).ToList();
        }

        public Map GetMap()
        {
            return correlationMap;
        }
    }
}
