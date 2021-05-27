namespace GaugeReader.Processors.Models
{
    using GaugeReader.Images.Models;
    using GaugeReader.Math.Models.Angles;
    using GaugeReader.Math.Models.Lines;
    using GaugeReader.Profiles.Models;

    public class ProcessorArgs
    {
        public ProcessorArgs()
        {
            Aborted = false;
        }

        // This will need to be set before the pipeline is run
        public ImageFile ImageFile { get; set; }

        public IProfile Profile { get; set; }

        public ImageSet ImageSet { get; set; }

        public ImageSet ResultImage { get; set; }

        public AngleMap MarkerMap { get; set; }

        public string Path { get; set; }

        public bool Aborted { get; set; }

        public int DialDiameter => DialRadius * 2;

        public int DialRadius { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public Angle Theta1 { get; set; }

        public Angle Theta2 { get; set; }

        public double R1 { get; set; }

        public double R2 { get; set; }

        public Line HandLine { get; set; }

        public Angle HandAngle { get; set; }


        public AngleSpan MarkerAngleSpan { get; set; }

        public int? ExpectedValue { get; set; }

        public int? ActualValue { get; set; }

        public bool? Passed { get; set; }

        public bool IsMarkerIsolated { get; set; }

        public void Abort()
        {
            Aborted = true;
        }
    }
}
