namespace GaugeReader.Processors.Models
{
    using GaugeReader.Gauge.Models;
    using GaugeReader.Images.Models;
    using GaugeReader.Profiles.Models;
    using System.Drawing;

    public class ProcessorArgs
    {
        public ProcessorArgs()
        {
            Aborted = false;
            Gauge = new Gauge();
        }

        // This will need to be set before the pipeline is run
        public ImageFile ImageFile { get; set; }

        public IProfile Profile { get; set; }

        public ImageSet ImageSet { get; set; }

        public Bitmap ResultImage { get; set; }

        public string Path { get; set; }

        public bool Aborted { get; set; }

        public int DialDiameter => DialRadius * 2;

        public int DialRadius { get; set; }

        public Gauge Gauge { get; set; }

        public int? ExpectedValue { get; set; }

        public int? ActualValue { get; set; }

        public bool? Passed { get; set; }

        public void Abort()
        {
            Aborted = true;
        }
    }
}
