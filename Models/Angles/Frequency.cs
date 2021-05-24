namespace GaugeReader.Models.Angles
{
    public class Frequency
    {
        public Angle WaveLength { get; set; }

        public Angle Phase { get; set; }

        public double Magninute { get; set; }

        public Frequency(Angle waveLength, Angle phase, double magnitude)
        {
            WaveLength = waveLength;
            Phase = phase;
            Magninute = magnitude;
        }


    }
}
